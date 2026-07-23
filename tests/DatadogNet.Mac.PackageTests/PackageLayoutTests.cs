using System.IO.Compression;

namespace DatadogNet.Mac.PackageTests;

/// <summary>
/// Asserts the shape of the produced NuGet packages. These run against the packed .nupkg rather
/// than the build output, so they catch packaging regressions the compiler cannot see.
/// </summary>
public class PackageLayoutTests
{
    /// <summary>
    /// The payload checks decompress the native frameworks, and the payload is identical across
    /// target frameworks, so they run against one rather than all three. That the others carry the
    /// same bytes is asserted separately and cheaply by
    /// <see cref="Native_payload_is_the_same_across_target_frameworks"/>.
    /// </summary>
    private const string PayloadTargetFramework = "net8.0-maccatalyst18.0";

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Package_carries_a_binding_assembly_for_every_target_framework(string name)
    {
        using var package = Packages.OpenPackage(name);

        foreach (var tfm in Packages.ExpectedTargetFrameworks)
        {
            var expected = $"lib/{tfm}/{Packages.AssemblyName(name)}.dll";
            Assert.True(
                package.GetEntry(expected) is not null,
                $"{Packages.PackageId(name)} is missing '{expected}'.");
        }
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Package_carries_the_native_payload_for_every_target_framework(string name)
    {
        using var package = Packages.OpenPackage(name);

        foreach (var tfm in Packages.ExpectedTargetFrameworks)
        {
            var entry = package.GetEntry(Packages.ResourcesEntry(name, tfm));

            // The payload must be a single .resources.zip rather than a .resources directory -
            // and on Catalyst that is not just path-length insurance: the frameworks are
            // macOS-style versioned bundles whose symlinks only survive in the compressed form.
            Assert.True(
                entry is not null,
                $"{Packages.PackageId(name)} is missing '{Packages.ResourcesEntry(name, tfm)}'. " +
                "Has CompressBindingResourcePackage been unset?");

            // The smallest framework here compresses to a few hundred kilobytes; anything tiny
            // means an empty placeholder rather than a real payload.
            Assert.True(entry!.Length > 100_000, $"'{entry.FullName}' is only {entry.Length} bytes.");
        }
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Native_payload_is_the_same_across_target_frameworks(string name)
    {
        using var package = Packages.OpenPackage(name);

        // net8/net9 are packed by the .NET 9 SDK and net10 by the .NET 10 one, and
        // merge-packages.py then grafts the net10 lib/ tree into the other package. Nothing in
        // that flow guarantees the two passes bound the same native version, so this is where a
        // mismatched graft would be caught.
        //
        // Compared by logical content, not bytes: each pass re-zips the payload and the archives
        // embed their own timestamps, so the same frameworks legitimately produce different CRCs.
        var manifests = new List<(string Tfm, List<(string Name, long Length)> Entries)>();

        foreach (var tfm in Packages.ExpectedTargetFrameworks)
        {
            using var payload = Packages.OpenNativePayload(package, name, tfm);
            manifests.Add((tfm, payload.Entries
                .Select(entry => (entry.FullName, entry.Length))
                .OrderBy(entry => entry.FullName, StringComparer.Ordinal)
                .ToList()));
        }

        var reference = manifests[0];
        foreach (var (tfm, entries) in manifests.Skip(1))
        {
            Assert.True(
                reference.Entries.SequenceEqual(entries),
                $"The native payload for {tfm} differs from {reference.Tfm} in {Packages.PackageId(name)}.");
        }
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Native_payload_carries_exactly_its_own_xcframework(string name)
    {
        var spec = Packages.Spec(name);

        using var package = Packages.OpenPackage(name);
        using var payload = Packages.OpenNativePayload(package, name, PayloadTargetFramework);

        var present = payload.Entries
            .Select(entry => entry.FullName.Split('/')[0])
            .Where(entry => entry.EndsWith(".xcframework", StringComparison.Ordinal))
            .Distinct()
            .ToList();

        // One package, one framework. A package that shipped two would mean the shared libs/
        // directory leaked into a NativeReference glob, and consumers would end up with the same
        // framework embedded twice from two packages - a duplicate-symbol link failure.
        Assert.Equal([$"{spec.Framework}.xcframework"], present);
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Native_payload_carries_the_maccatalyst_slice_only(string name)
    {
        var spec = Packages.Spec(name);

        using var package = Packages.OpenPackage(name);
        using var payload = Packages.OpenNativePayload(package, name, PayloadTargetFramework);

        var slices = SlicesOf(payload, spec.Framework);

        // Exactly one slice, and it is the Catalyst one. These frameworks are built by
        // BuildXcFrameworks.sh with a single Catalyst destination, so a second slice appearing -
        // or an ios-arm64 device slice replacing the maccatalyst one - means the build script
        // changed what it archives, and a net*-maccatalyst consumer would fail to link.
        var slice = Assert.Single(slices);
        Assert.True(
            Packages.IsMacCatalystSlice(slice),
            $"{spec.Framework}.xcframework carries '{slice}' instead of a maccatalyst slice.");
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Native_payload_carries_no_debug_symbols(string name)
    {
        var spec = Packages.Spec(name);

        using var package = Packages.OpenPackage(name);
        using var payload = Packages.OpenNativePayload(package, name, PayloadTargetFramework);

        // dSYMs would roughly double every package for bytes no consumer can use.
        // BuildXcFrameworks.sh keeps them out of the xcframeworks and exports them next to the
        // build for a release to attach instead.
        var symbols = payload.Entries
            .Where(entry => entry.FullName.Contains("dSYM", StringComparison.OrdinalIgnoreCase))
            .Select(entry => entry.FullName)
            .ToList();

        Assert.True(symbols.Count == 0, $"{Packages.PackageId(name)} ships debug symbols: {string.Join(", ", symbols.Take(3))}");
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Xcframework_manifest_matches_the_slices_actually_shipped(string name)
    {
        var spec = Packages.Spec(name);

        using var package = Packages.OpenPackage(name);
        using var payload = Packages.OpenNativePayload(package, name, PayloadTargetFramework);

        var manifest = payload.GetEntry($"{spec.Framework}.xcframework/Info.plist");
        Assert.True(manifest is not null, $"{spec.Framework}.xcframework has no Info.plist.");

        using var reader = new StreamReader(manifest!.Open());
        var text = reader.ReadToEnd();

        // A manifest advertising a slice that is not on disk makes the consuming SDK reject the
        // whole xcframework - a failure that would only surface in a consuming app's build,
        // never here. 'ios' cannot be asserted absent the way the iOS repository asserts
        // 'maccatalyst' absent, because the Catalyst slice's own identifier starts with ios-;
        // the platform names that must not appear are the others.
        foreach (var platform in new[] { "tvos", "watchos", "xros", "simulator" })
        {
            Assert.DoesNotContain(platform, text, StringComparison.OrdinalIgnoreCase);
        }

        // Every slice present on disk must be advertised.
        foreach (var slice in SlicesOf(payload, spec.Framework))
        {
            Assert.Contains(slice, text, StringComparison.Ordinal);
        }

        // And no dangling dSYM reference, since the dSYMs themselves are kept out.
        Assert.DoesNotContain("DebugSymbolsPath", text, StringComparison.Ordinal);
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Package_declares_the_expected_dependencies_for_every_target_framework(string name)
    {
        var spec = Packages.Spec(name);
        var expected = spec.DependsOn.Select(Packages.PackageId).OrderBy(id => id).ToList();

        using var package = Packages.OpenPackage(name);
        var nuspec = Packages.ReadNuspec(package, name);

        var groups = nuspec.Descendants()
            .Where(element => element.Name.LocalName == "group")
            .ToList();

        Assert.Equal(
            Packages.ExpectedTargetFrameworks.OrderBy(tfm => tfm),
            groups.Select(group => group.Attribute("targetFramework")?.Value).OrderBy(tfm => tfm));

        // Asserted per group, not just once: the net10 group is grafted in by merge-packages.py
        // from a separately built package, and an empty or stale group there would leave net10
        // consumers restoring a package whose siblings never come with it.
        foreach (var group in groups)
        {
            var declared = group.Elements()
                .Where(element => element.Name.LocalName == "dependency")
                .Select(element => element.Attribute("id")?.Value ?? string.Empty)
                .OrderBy(id => id, StringComparer.Ordinal)
                .ToList();

            Assert.Equal(expected, declared);
        }
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Package_declares_the_expected_nuspec_metadata(string name)
    {
        using var package = Packages.OpenPackage(name);
        var nuspec = Packages.ReadNuspec(package, name);

        string Value(string element) => nuspec.Descendants()
            .FirstOrDefault(node => node.Name.LocalName == element)?.Value.Trim() ?? string.Empty;

        Assert.Equal(Packages.PackageId(name), Value("id"));
        Assert.NotEmpty(Value("version"));
        Assert.Equal("MIT AND Apache-2.0", Value("license"));
        Assert.Equal("icon.png", Value("icon"));
        Assert.Equal("README.md", Value("readme"));

        // The description names the framework the package wraps, which is what tells a reader on
        // nuget.org which of the eleven they want.
        Assert.Contains(Packages.Spec(name).Framework, Value("description"), StringComparison.Ordinal);
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Package_ships_the_icon_readme_and_both_licence_texts(string name)
    {
        using var package = Packages.OpenPackage(name);

        Assert.True(package.GetEntry("icon.png") is not null, "icon.png is referenced but not packed.");
        Assert.True(package.GetEntry("README.md") is not null, "README.md is referenced but not packed.");

        // The package declares "MIT AND Apache-2.0", so both texts have to be in it: MIT covers the
        // binding code in this repository, Apache-2.0 the native binaries built from Datadog's
        // sources.
        using var bindings = new StreamReader(Packages.ReadEntry(package, "licenses/LICENSE"));
        Assert.Contains("MIT License", bindings.ReadToEnd(), StringComparison.OrdinalIgnoreCase);

        using var native = new StreamReader(Packages.ReadEntry(package, "licenses/Apache-2.0.txt"));
        Assert.Contains("Apache License", native.ReadToEnd(), StringComparison.Ordinal);
    }

    [Theory]
    [MemberData(nameof(Packages.Names), MemberType = typeof(Packages))]
    public void Symbol_package_is_produced(string name)
    {
        using var symbols = Packages.OpenPackage(name, ".snupkg");

        foreach (var tfm in Packages.ExpectedTargetFrameworks)
        {
            var expected = $"lib/{tfm}/{Packages.AssemblyName(name)}.pdb";
            Assert.True(
                symbols.GetEntry(expected) is not null,
                $"Symbol package for {Packages.PackageId(name)} is missing '{expected}'.");
        }
    }

    [Fact]
    public void Every_package_is_packed_with_the_same_version()
    {
        var versions = Packages.All
            .Select(spec =>
            {
                using var package = Packages.OpenPackage(spec.Name);
                var nuspec = Packages.ReadNuspec(package, spec.Name);
                return nuspec.Descendants().First(node => node.Name.LocalName == "version").Value.Trim();
            })
            .Distinct()
            .ToList();

        // The packages depend on each other at an exact version, so a set built at two different
        // versions does not restore at all.
        Assert.Single(versions);
    }

    [Fact]
    public void Every_expected_package_was_built_and_nothing_else()
    {
        var found = Directory.GetFiles(Packages.ArtifactsDirectory, "*.nupkg")
            .Select(path => Path.GetFileName(path)!)
            .Select(file => file[..file.LastIndexOf(".Mac.", StringComparison.Ordinal)] + ".Mac")
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();

        var expected = Packages.All
            .Select(spec => Packages.PackageId(spec.Name))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();

        // Catches both halves of a mistake in BuildNugets.sh: a package silently dropped from the
        // list, and a stale package left in artifacts/ from an earlier version.
        Assert.Equal(expected, found);
    }

    /// <summary>The slice directory names an xcframework actually carries inside the payload.</summary>
    private static List<string> SlicesOf(ZipArchive payload, string framework) =>
        payload.Entries
            .Where(entry => entry.FullName.StartsWith($"{framework}.xcframework/", StringComparison.Ordinal))
            .Select(entry => entry.FullName.Split('/'))
            .Where(parts => parts.Length > 2)
            .Select(parts => parts[1])
            .Where(slice => !slice.EndsWith(".plist", StringComparison.Ordinal))
            .Where(slice => !slice.EndsWith(".xcprivacy", StringComparison.Ordinal))
            .Distinct()
            .OrderBy(slice => slice, StringComparer.Ordinal)
            .ToList();
}
