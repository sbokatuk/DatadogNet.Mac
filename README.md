# DatadogNet.Mac

.NET for Mac Catalyst bindings for the native [Datadog iOS SDK](https://github.com/DataDog/dd-sdk-ios)
(`dd-sdk-ios`) - RUM, Logs, Trace, crash reporting and feature flags for the Mac Catalyst head of a
.NET MAUI (or plain .NET for Mac Catalyst) app.

This is the Mac Catalyst sibling of [DatadogNet.iOS](https://github.com/sbokatuk/DatadogNet.iOS)
and [DatadogNet.Android](https://github.com/sbokatuk/DatadogNet.Android), and slots under the
[DatadogNet](https://github.com/sbokatuk/DatadogNet) cross-platform façade. The API is *identical*
to DatadogNet.iOS - same types, same namespaces (`DatadogCore`, `DatadogRUM`, `DatadogLogs`, ...) -
because the binding definitions are verbatim copies of that repository's and Catalyst is the same
UIKit platform underneath. Code written against the iOS packages compiles against these unchanged;
only the package ids differ.

> **Support status.** Datadog lists macOS (Catalyst) 12+ as *partial* support: the SDK compiles
> and its features work, but Catalyst is not a first-class tested platform upstream. Session
> Replay is iOS/iPadOS-only and records nothing on Catalyst (the package still ships so shared
> code links). Cellular/battery-derived RUM vitals are no-ops on a Mac. Test your telemetry
> end-to-end before relying on it.

## Contents

- [Packages](#packages)
- [Installing](#installing)
- [Usage](#usage)
- [How this repository works](#how-this-repository-works)
- [Building locally](#building-locally)
- [Upgrading the Datadog SDK](#upgrading-the-datadog-sdk)
- [Releasing](#releasing)
- [Verifying a build](#verifying-a-build)
- [Licence](#licence)

## Packages

Versioned `<dd-sdk-ios version>.<binding revision>`, exactly like DatadogNet.iOS: `3.14.0.1` wraps
dd-sdk-ios 3.14.0, and the fourth component advances when the bindings or packaging change while
the native version stays put. The iOS and Mac revisions advance independently - match on the first
three components, not all four.

| Package | Wraps | Notes |
| --- | --- | --- |
| `DatadogNet.Core.Mac` | `DatadogCore` | SDK initialization; start here |
| `DatadogNet.RUM.Mac` | `DatadogRUM` | Real User Monitoring |
| `DatadogNet.Logs.Mac` | `DatadogLogs` | Log collection |
| `DatadogNet.Trace.Mac` | `DatadogTrace` | APM tracing |
| `DatadogNet.SessionReplay.Mac` | `DatadogSessionReplay` | Links, but records nothing on Catalyst |
| `DatadogNet.WebViewTracking.Mac` | `DatadogWebViewTracking` | WKWebView instrumentation |
| `DatadogNet.CrashReporting.Mac` | `DatadogCrashReporting` | KSCrash-based crash reporting |
| `DatadogNet.Flags.Mac` | `DatadogFlags` | Native framework only; no Objective-C API upstream yet |
| `DatadogNet.Profiling.Mac` | `DatadogProfiling` | Native framework only; no Objective-C API upstream yet |
| `DatadogNet.Internal.Mac` | `DatadogInternal` | Shared plumbing; arrives transitively |
| `DatadogNet.OpenTelemetryApi.Mac` | `OpenTelemetryApi` | Trace dependency; arrives transitively |

There is no `DatadogNet.Objc.Mac`: that id exists on iOS only as a compatibility meta-package for
apps upgrading from the 2.x bindings, and no Mac Catalyst 2.x ever existed.

Every package targets `net8.0-maccatalyst18.0`, `net9.0-maccatalyst18.0` and
`net10.0-maccatalyst26.0`, with `SupportedOSPlatformVersion` 15.0 (macOS 12) - the floor both the
.NET 9 maccatalyst workload and Datadog's own Catalyst support statement impose.

> **net8 sunset.** The net8 head is already past its platform support window — the net8 mobile
> workloads left support with MAUI 8 on 14 May 2025 — and ships for the apps that still target
> it. So the decision does not persist by inertia: **the net8 head is dropped in the first
> release after .NET 8 itself leaves support on 10 November 2026**, in step with DatadogNet.iOS.

## Installing

```sh
dotnet add package DatadogNet.Core.Mac
dotnet add package DatadogNet.RUM.Mac      # and the other features you use
```

In a multi-headed MAUI app, guard the references by platform so each head restores its own
bindings:

```xml
<ItemGroup Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'maccatalyst'">
  <PackageReference Include="DatadogNet.Core.Mac" Version="3.14.0.3" />
  <PackageReference Include="DatadogNet.RUM.Mac" Version="3.14.0.3" />
</ItemGroup>
```

Or skip the per-platform wiring entirely and use the
[DatadogNet](https://github.com/sbokatuk/DatadogNet) façade, which fronts the iOS, Android and Mac
Catalyst bindings behind one cross-platform API.

## Usage

Identical to [DatadogNet.iOS](https://github.com/sbokatuk/DatadogNet.iOS#usage) - same types, same
namespaces, same convenience layer - so that README is the reference. The short version:

```csharp
using DatadogCore;
using DatadogInternal;
using DatadogRUM;

var configuration = new DDConfiguration(clientToken: "<CLIENT_TOKEN>", env: "prod")
{
    Site = DDSite.Us1(),
};
DDDatadog.InitializeWithConfiguration(configuration, DDTrackingConsent.Granted());

DDRUM.EnableWith(new DDRUMConfiguration(applicationID: "<RUM_APPLICATION_ID>")
{
    UiKitViewsPredicate = new DDDefaultUIKitRUMViewsPredicate(),
    UiKitActionsPredicate = new DDDefaultUIKitRUMActionsPredicate(),
});
```

[samples/DatadogNet.Mac.Example](samples/DatadogNet.Mac.Example) is a runnable MAUI Catalyst app
showing RUM, Logs, Trace and crash reporting - the honest Catalyst feature set.

## How this repository works

The one structural difference from DatadogNet.iOS: **the native frameworks are built here, not
downloaded**. Datadog publishes no Mac Catalyst binaries - `Datadog.xcframework.zip` carries iOS
device and simulator slices only, and the prebuilt `OpenTelemetryApi` covers every Apple platform
*except* Catalyst - so [build/BuildXcFrameworks.sh](build/BuildXcFrameworks.sh) compiles
dd-sdk-ios at the pinned tag for `platform=macOS,variant=Mac Catalyst` and assembles
single-slice (`ios-arm64_x86_64-maccatalyst`) xcframeworks into `libs/`. Two project-file patches
make that possible; the script documents both.

The binding definitions (`ApiDefinitions.cs`, `StructsAndEnums.cs`, `Additions/`) are **verbatim
copies from DatadogNet.iOS**, refreshed by
[build/SyncBindingsFromiOS.sh](build/SyncBindingsFromiOS.sh). Do not edit them here: fix them in
the iOS repository and re-sync, so the two platforms cannot drift. That is enforced, not asked
politely: the sync records the iOS commit it copied from in `build/ios-bindings-source.txt`, and
CI's `binding-drift` job re-runs the sync against exactly that commit and fails on any
difference. (`shims/` is deliberately outside the sync — see the script's header for what must
happen if the iOS Flags shim ever ships.)

### Layout

```
build/    BuildXcFrameworks.sh (native build), BuildNugets.sh (pack), SyncBindingsFromiOS.sh,
          merge-packages.py (grafts the net10 pass into the net9-band packages)
libs/     the Catalyst xcframeworks (built, not committed) + BUILD-INFO.txt
src/      one binding project per package + Datadog.Binding.props (everything they share)
tests/    package layout tests, run against the packed .nupkg files
samples/  MAUI Mac Catalyst example app, built against the packed packages
```

## Building locally

```sh
./build/BuildXcFrameworks.sh   # needs Xcode; ~15 minutes, cached by DATADOG_BUILD_DIR
./build/BuildNugets.sh         # packs all eleven packages into ./artifacts
dotnet test tests/DatadogNet.Mac.PackageTests
dotnet build samples/DatadogNet.Mac.Example/DatadogNetExample.csproj -p:RuntimeIdentifier=maccatalyst-arm64
```

Needs the .NET 9 and .NET 10 SDKs with the `maccatalyst` workload in each (BuildNugets.sh packs
twice and merges - no single SDK can build all three target frameworks), plus `maui-maccatalyst`
for the sample.

## Upgrading the Datadog SDK

1. `./build/BumpNativeVersion.sh <new dd-sdk-ios version>` - one command for every pin:
   `DatadogNativeVersion` (revision reset to 1), `DatadogOtelVersion` (read from the new tag's
   `Cartfile.resolved` on GitHub), the README's package pins, and a scaffolded
   `docs/release-notes/<version>.md`. It refuses versions whose tag does not exist yet, and
   prints the rest of this list when it is done.
2. Wait for (or produce) the matching DatadogNet.iOS release, then run
   `./build/SyncBindingsFromiOS.sh` against it and review the diff.
3. `./build/BuildXcFrameworks.sh && ./build/BuildNugets.sh && dotnet test tests/DatadogNet.Mac.PackageTests`
4. Update the README package table if the feature set moved, and finish the scaffolded release
   notes - they ship verbatim as every package's `PackageReleaseNotes`.

## Releasing

Push a tag `v<version>`, e.g. `v3.14.0.1`. The [release workflow](.github/workflows/release.yml)
builds the xcframeworks, packs, validates, publishes to nuget.org via trusted publishing, and
creates a GitHub release; a curated `docs/release-notes/<version>.md` replaces the generated
commit list when present. Pull requests publish `-beta.<pr>.<run>` prereleases the same way.

## Verifying a build

"Built from source in CI" is a claim worth being able to check, so every release attests what it
publishes: the `.nupkg` files and the `dsyms-<version>.zip` carry
[build provenance attestations](https://docs.github.com/en/actions/security-for-github-actions/using-artifact-attestations)
— a signed, public statement that these exact bytes came out of this repository's release
workflow, at a named tag and commit, on GitHub's runners. Verify the file you actually
downloaded with the `gh` CLI:

```sh
gh attestation verify DatadogNet.Core.Mac.<version>.nupkg --repo sbokatuk/DatadogNet.Mac
```

That proves *where the bytes were built* — not on someone's laptop, not swapped after the run —
and names the commit they were built from. It does not prove the source does what it says; for
that, the repository is small enough to read.

The stronger check is rebuilding it yourself. Byte-identity is not achievable — as
[build/BuildXcFrameworks.sh](build/BuildXcFrameworks.sh)'s header says, the output varies with
the Xcode that compiled it, and Mach-O embeds fresh UUIDs regardless — but the exported surface
is stable and comparable:

1. Read `BUILD-INFO.txt` from the release's `dsyms-<version>.zip`: it records the Xcode and SDK
   the release binaries were compiled with.
2. Check out the release tag, install that Xcode, run `./build/BuildXcFrameworks.sh`.
3. Compare exported symbols per framework, your build against the one inside the shipped package
   (the payload is `lib/<tfm>/<id>.resources.zip` inside the `.nupkg`):

   ```sh
   nm -gU <Framework>.framework/<Framework> | awk '{print $3}' | sort
   ```

Identical symbol lists from the pinned source, at the tag, under the recorded Xcode, is the
strongest reproducibility statement an Xcode toolchain leaves available.

## Licence

The binding code in this repository is [MIT](LICENSE). The native binaries the packages embed are
built from Datadog's Apache-2.0 sources (dd-sdk-ios, KSCrash, opentelemetry-swift); every package
carries both texts under `licenses/`. Not an official Datadog product.
