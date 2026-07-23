using System.IO.Compression;
using System.Xml.Linq;

namespace DatadogNet.Mac.PackageTests;

/// <summary>
/// Locates the packed .nupkg files and describes what each one is supposed to contain.
/// </summary>
public static class Packages
{
    /// <summary>
    /// Every package this repository builds, with the native framework it wraps and the packages
    /// it must declare a dependency on.
    /// </summary>
    /// <remarks>
    /// The dependency column is the real native graph, mirrored from DatadogNet.iOS - the same
    /// frameworks, built for a different platform, link the same way. There is no Objc entry:
    /// that id is an iOS-only compatibility meta-package, and no Mac Catalyst 2.x ever existed
    /// to be compatible with.
    /// </remarks>
    public static readonly PackageSpec[] All =
    [
        new("Internal", "DatadogInternal", []),
        new("OpenTelemetryApi", "OpenTelemetryApi", []),
        new("Core", "DatadogCore", ["Internal"]),
        new("Trace", "DatadogTrace", ["Core", "Internal", "OpenTelemetryApi"]),
        new("Logs", "DatadogLogs", ["Core", "Internal"]),
        new("RUM", "DatadogRUM", ["Core", "Internal"]),
        new("SessionReplay", "DatadogSessionReplay", ["Core", "Internal"]),
        new("WebViewTracking", "DatadogWebViewTracking", ["Core", "Internal"]),
        new("CrashReporting", "DatadogCrashReporting", ["Core", "Internal"]),
        new("Flags", "DatadogFlags", ["Core", "Internal"]),
        new("Profiling", "DatadogProfiling", ["Core", "Internal"]),
    ];

    /// <summary>Target frameworks every package must carry a binding assembly for.</summary>
    public static readonly string[] ExpectedTargetFrameworks =
    [
        "net8.0-maccatalyst18.0", "net9.0-maccatalyst18.0", "net10.0-maccatalyst26.0",
    ];

    /// <summary>xunit member data: one row per package.</summary>
    public static TheoryData<string> Names
    {
        get
        {
            var data = new TheoryData<string>();
            foreach (var package in All)
            {
                data.Add(package.Name);
            }

            return data;
        }
    }

    public static PackageSpec Spec(string name) =>
        All.Single(package => package.Name == name);

    public static string PackageId(string name) => $"DatadogNet.{name}.Mac";

    /// <summary>The assembly name, which is also the prefix of the native payload entry.</summary>
    public static string AssemblyName(string name) => PackageId(name);

    public static string ResourcesEntry(string name, string tfm) =>
        $"lib/{tfm}/{AssemblyName(name)}.resources.zip";

    /// <summary>
    /// The directory packages are read from. Overridable so the tests can run against a directory
    /// other than the repository's own artifacts/ - a CI job that downloads them, for instance.
    /// </summary>
    public static string ArtifactsDirectory =>
        Environment.GetEnvironmentVariable("DATADOG_ARTIFACTS_DIR") is { Length: > 0 } configured
            ? configured
            : Path.Combine(RepositoryRoot, "artifacts");

    private static string RepositoryRoot
    {
        get
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
            {
                directory = directory.Parent;
            }

            return directory?.FullName
                ?? throw new InvalidOperationException("Could not locate the repository root.");
        }
    }

    public static ZipArchive OpenPackage(string name, string extension = ".nupkg")
    {
        var id = PackageId(name);
        var matches = Directory.GetFiles(ArtifactsDirectory, $"{id}.*{extension}");

        // Matching on the id prefix would also match a longer id that starts with it, so the
        // version is required to look like a version.
        var package = matches.SingleOrDefault(path =>
            Path.GetFileName(path).StartsWith($"{id}.", StringComparison.Ordinal) &&
            char.IsDigit(Path.GetFileName(path)[id.Length + 1]));

        if (package is null)
        {
            throw new FileNotFoundException(
                $"No {id}{extension} in {ArtifactsDirectory}. Run ./build/BuildNugets.sh first.");
        }

        return ZipFile.OpenRead(package);
    }

    /// <summary>Opens the compressed native payload inside a package as an archive of its own.</summary>
    public static ZipArchive OpenNativePayload(ZipArchive package, string name, string tfm)
    {
        var entry = package.GetEntry(ResourcesEntry(name, tfm))
            ?? throw new InvalidOperationException($"{PackageId(name)} has no {ResourcesEntry(name, tfm)}.");

        // Copied to memory first: ZipArchive needs a seekable stream, and the entry stream is not.
        var buffer = new MemoryStream();
        using (var stream = entry.Open())
        {
            stream.CopyTo(buffer);
        }

        buffer.Position = 0;
        return new ZipArchive(buffer, ZipArchiveMode.Read);
    }

    public static Stream ReadEntry(ZipArchive archive, string path)
    {
        var entry = archive.GetEntry(path)
            ?? throw new InvalidOperationException($"Archive has no entry '{path}'.");

        return entry.Open();
    }

    public static XDocument ReadNuspec(ZipArchive package, string name)
    {
        using var stream = ReadEntry(package, $"{PackageId(name)}.nuspec");
        return XDocument.Load(stream);
    }

    /// <summary>
    /// Whether a slice directory name is the single Mac Catalyst slice the packages are meant to
    /// ship.
    /// </summary>
    /// <remarks>
    /// Catalyst slices are named <c>ios-*-maccatalyst</c> - the <c>ios-</c> prefix is what makes
    /// an iOS-package slice check need the inverse of this test, and what makes asserting on the
    /// <c>maccatalyst</c> suffix rather than the prefix the meaningful check here. What must
    /// never appear is a plain iOS, simulator, tvOS, macOS, watchOS or visionOS slice.
    /// </remarks>
    public static bool IsMacCatalystSlice(string slice) =>
        slice.StartsWith("ios-", StringComparison.Ordinal) &&
        slice.Contains("maccatalyst", StringComparison.Ordinal);
}

/// <summary>What one package is expected to be.</summary>
/// <param name="Name">The middle segment of the id: <c>DatadogNet.<see cref="Name"/>.Mac</c>.</param>
/// <param name="Framework">The native xcframework the package ships.</param>
/// <param name="DependsOn">The <see cref="Name"/>s of the packages it must depend on.</param>
public sealed record PackageSpec(string Name, string Framework, string[] DependsOn);
