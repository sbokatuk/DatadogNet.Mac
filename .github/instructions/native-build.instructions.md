---
applyTo: "build/BuildXcFrameworks.sh,build/BumpNativeVersion.sh"
---

# Native build and version bumps — the reproducibility contract

- Datadog publishes no Mac Catalyst slices, so these binaries exist only because this script builds them. Everything a release ships must therefore be reconstructible from the tagged tree alone.
- Keep every input pinned: dd-sdk-ios by git tag `DatadogNativeVersion`, `DataDog/opentelemetry-swift-packages` by git tag `DatadogOtelVersion`, KSCrash by the version in Datadog's package manifest. Never introduce a branch, a floating `main`, a `latest` release lookup or an undated download.
- Keep the version-pair cross-check: `DatadogOtelVersion` must match what the checked-out dd-sdk-ios tag pins in its `Cartfile.resolved`, verified against the actual checkout at build time and read from the new tag by `BumpNativeVersion.sh`. Do not relax it into a warning.
- Keep the `project.pbxproj` patches (`SUPPORTS_MACCATALYST`, `platformFilters`) assertive: each must prove it changed something or found the already-patched state. A silent no-op surfaces much later as an unrelated `xcodebuild` failure.
- Keep the output shape: one `ios-arm64_x86_64-maccatalyst` slice per xcframework in `libs/`, with the slice verified after assembly.
- `libs/dsyms/` and `libs/BUILD-INFO.txt` are release artefacts, not build scratch. The dSYMs are the only symbolication data these binaries will ever have, and `BUILD-INFO.txt` records the Xcode and SDK that produced them; both are uploaded by `build.yml` and attached to the GitHub release. Never move them under the temporary work directory, and never pack them.
- Changes here invalidate the CI cache through `hashFiles('build/BuildXcFrameworks.sh')`. If a change alters the binaries but not this file — for example a new input read from elsewhere — add that input to the cache key in `.github/workflows/build.yml` in the same commit.
- `BumpNativeVersion.sh` owns every pin at once: both version properties (binding revision back to 1), the README badge, snippets and prose, and the scaffolded release note. It must end with `./CheckReadmeVersions.sh` passing, and must keep refusing a same-version bump — resetting the revision on a shipped line would name a version that can never be published.
- Test changes to these scripts by actually running them on macOS with full Xcode. If that is not possible in the current environment, say so instead of guessing.
