# DatadogNet.Mac — repository instructions

## Overview

- Eleven NuGet packages, `DatadogNet.<Module>.Mac` (Core, RUM, Logs, Trace, SessionReplay, WebViewTracking, CrashReporting, Flags, Profiling, Internal, OpenTelemetryApi): .NET for Mac Catalyst bindings for the native Datadog iOS SDK (`DataDog/dd-sdk-ios` — the SDK that runs on Catalyst).
- Treat it as the Catalyst sibling of `sbokatuk/DatadogNet.iOS`: identical API, same namespaces (`RootNamespace` is the bare framework name — `DatadogCore`, `DatadogRUM`, …), only the package ids differ. This repository owns packaging, not bindings.
- Two differences from the iOS repository: the binding sources here are **verbatim copies of DatadogNet.iOS's**, synced by script and never edited here; the native xcframeworks are **built from source**, because Datadog publishes no Catalyst slices.
- Currently bound: dd-sdk-ios **3.14.0**, package version **3.14.0.4**, OpenTelemetryApi **2.5.0** — the versions the `DatadogNet` façade pins for its Catalyst head.
- Datadog's Catalyst support is partial (macOS 12+). Session Replay links but records nothing on Catalyst; cellular/battery RUM vitals are no-ops. Do not write docs or samples claiming otherwise.

## Build and verify

- macOS only, with full Xcode (not the command-line tools) on the **26.0** line. `.github/actions/select-xcode` selects it by *iOS* SDK version — Catalyst compiles against the iOS SDK, so `net10.0-maccatalyst26.0` needs Xcode 26.0 exactly, newest patch first. Never loosen that to any 26.x.
- Install the .NET 9 and .NET 10 SDKs with the `maccatalyst` workload in each band (`global.json` pins 9.0.100); the sample also needs `maui-maccatalyst`.
- Run, in order:
  1. `./build/BuildXcFrameworks.sh [dd-sdk-ios tag]` — 10–20 minutes; set `DATADOG_BUILD_DIR` to reuse the checkouts.
  2. `./build/BuildNugets.sh [version] [native-version]` — two SDK-band passes (net8/net9, then net10 from a scratch `global.json`) merged by `build/merge-packages.py` into `artifacts/`.
  3. `dotnet test tests/DatadogNet.Mac.PackageTests`.
  4. `dotnet build samples/DatadogNet.Mac.Example/DatadogNetExample.csproj -p:RuntimeIdentifier=maccatalyst-arm64 -p:DatadogPackageVersion=<version>` — it restores the packed nupkgs from the `artifacts/` source in `NuGet.config`, so pack first.
- Run `./build/CheckReadmeVersions.sh` after touching versions; CI runs it as the very first step of the pack job.
- Nothing here builds on Linux or Windows. Without macOS and Xcode, say so rather than stubbing the native step out.

## Layout

- `src/` — eleven binding projects (identity, description, dependencies only) plus `Datadog.Binding.props`, which holds everything they share: import it, never copy settings into a project. `ApiDefinitions.cs`, `StructsAndEnums.cs` and `Additions/` are synced. `src/DatadogNet.Core.Mac/buildTransitive/` carries the one consumer-facing MSBuild fix this repository owns (`Registrar=static`, for dotnet/macios#21636).
- `build/` — `BuildXcFrameworks.sh` (native build), `BuildNugets.sh` (pack), `merge-packages.py` (merges the two SDK bands), `SyncBindingsFromiOS.sh` + `ios-bindings-source.txt` (the pinned iOS commit), `BumpNativeVersion.sh` (SDK upgrades), `CheckReadmeVersions.sh`, `check-upstream.sh` + `upstream.tsv`.
- `libs/` — built xcframeworks, `dsyms/` and `BUILD-INFO.txt`. Gitignored, produced at build time, cached in CI.
- `tests/DatadogNet.Mac.PackageTests` (xunit over the packed `artifacts/`) and `samples/DatadogNet.Mac.Example` (MAUI Catalyst app); unlike the sibling repositories, both are in `DatadogNet.sln`.
- `docs/` — one `release-notes/<version>.md` per released version, plus `known-issue-managed-static-registrar-trimmode-partial.md`.

## Conventions

- Bindings are downstream copies: fix them in DatadogNet.iOS, re-run `./build/SyncBindingsFromiOS.sh [path]`, and commit the synced files with the rewritten `build/ios-bindings-source.txt`.
- `build/merge-packages.py`, `.github/actions/select-xcode/action.yml` and `build/CheckReadmeVersions.sh` are hand-carried copies of DatadogNet.iOS's, marked "keep in sync". Land functional changes in both repositories; only comments may differ.
- Versions are four-part, `<dd-sdk-ios version>.<binding revision>`: the first three match the dd-sdk-ios release the same-numbered `.iOS` package wraps, the fourth advances independently of the iOS repository's.
- Every released version needs `docs/release-notes/<4-part version>.md` — it ships verbatim as `PackageReleaseNotes` and as the GitHub release body.
- Keep `SupportedOSPlatformVersion` at 15.0, the target-framework lists in `src/Datadog.Binding.props` intact, and licence metadata at `MIT AND Apache-2.0` (binding code MIT, native binaries Apache-2.0).
- Write British spelling, to match the README, and keep the existing comment register: comments here explain *why*, and that rationale must not be stripped.
- The sample targets net8 and net9 only; net10 is deliberately omitted to avoid a second SDK and workload install for the same API.

## CI and release flow

- `pr.yml` resolves `<version>-beta.<pr>.<run>`, calls `build.yml`, and publishes to nuget.org by trusted publishing (OIDC `NuGet/login@v1`, environment `nuget.org`, `NUGET_USER` the only secret); fork PRs build but skip the publish.
- `build.yml` (reusable; inputs `verify`, `version`, `native-version`) — `pack` on macos-15: README version check, `select-xcode`, .NET 9+10 workloads, `libs/` cache keyed on the native versions, the resolved Xcode and the build script's hash, native build, dSYM upload, pack, package tests; then `sample` (Debug and Release) and `binding-drift`.
- Merging a PR that **adds** `docs/release-notes/<version>.md` makes `auto-release.yml` tag the merge and dispatch `release.yml`.
- `release.yml` — `guard` (the tag must be an ancestor of the default branch) plus a tag-versus-`Directory.Build.props` check, then `build.yml` with `verify: false` (the commit was verified on its PR), then nuget.org, provenance attestation and `gh release create` with `dsyms-<version>.zip`.
- `upstream-drift.yml` runs daily over the two `build/upstream.tsv` rows (dd-sdk-ios and the Otel tag — separate decisions). `weekly-drift.yml` runs the drift guard every Monday and opens one issue when DatadogNet.iOS's `src/` has moved past the pinned commit.

## Testing

- Run `dotnet test tests/DatadogNet.Mac.PackageTests` after every pack: it asserts the dependency graph, the three target frameworks, the single Catalyst slice and the payload layout against the real `.nupkg` files.
- Building the sample against the packed packages is the consumer-level check. There is no device or e2e tier — Catalyst has no simulator story on a runner — so anything that can only fail at runtime must be verified on a real Mac and written up in `docs/`.
- `binding-drift` must stay green. The fix for a failure is a clean re-sync, never an edit to the copies here.

## Hard rules

- Never edit `ApiDefinitions.cs`, `StructsAndEnums.cs` or anything under `Additions/` in this repository — change them in DatadogNet.iOS and re-sync. `binding-drift.yml` re-runs the sync at the recorded commit and fails on any difference.
- Never hand-edit `build/ios-bindings-source.txt`; only `SyncBindingsFromiOS.sh` writes it.
- Never commit anything under `libs/`. The natives are built from pinned tags at build time.
- Never bump `DatadogNativeVersion` or `DatadogOtelVersion` alone: move them together with `./build/BumpNativeVersion.sh <tag>`, which reads the Otel pin from the new tag's `Cartfile.resolved`.
- Keep `CompressBindingResourcePackage` set. Catalyst frameworks are versioned bundles whose symlinks only survive packaging in the compressed form.
- Keep README pins in step with any version bump — `CheckReadmeVersions.sh` is CI's first step and fails the build otherwise.
- Release only through the workflows: never bypass or weaken the `guard` job, and never break the dSYM upload path — nobody else holds these symbols, so a release without dSYMs strands consumers' crash symbolication.

## References

- Upstream sources compiled here: [DataDog/dd-sdk-ios](https://github.com/DataDog/dd-sdk-ios) and [DataDog/opentelemetry-swift-packages](https://github.com/DataDog/opentelemetry-swift-packages).
- Siblings: [DatadogNet.iOS](https://github.com/sbokatuk/DatadogNet.iOS) (source of the bindings — consult it first), [DatadogNet.Android](https://github.com/sbokatuk/DatadogNet.Android), the [DatadogNet](https://github.com/sbokatuk/DatadogNet) façade.
- In-repo: `README.md`, `Directory.Build.props` and `src/Datadog.Binding.props` (every packaging decision, with its reasoning), `docs/known-issue-managed-static-registrar-trimmode-partial.md`, `docs/release-notes/`.

Trust these instructions and search the codebase only when something here is incomplete or wrong.
