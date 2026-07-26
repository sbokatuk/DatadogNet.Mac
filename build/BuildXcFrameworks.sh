#!/bin/zsh

set -eo pipefail

# Builds the native Datadog xcframeworks for Mac Catalyst, from source, into ../libs/.
#
# DatadogNet.iOS downloads its frameworks ready-made from upstream's release page. That is not an
# option here: Datadog publishes no Mac Catalyst slices - Datadog.xcframework.zip carries iOS
# device and simulator only, and the OpenTelemetryApi binary carries every Apple platform *except*
# Catalyst. dd-sdk-ios itself compiles fine for Catalyst (upstream CI builds the whole package for
# 'platform=macOS,variant=Mac Catalyst'; the docs list macOS (Catalyst) 12+ as partially
# supported), so this script does what upstream's tools/release/build-xcframeworks.sh does, with a
# Catalyst destination.
#
# Usage:
#   ./BuildXcFrameworks.sh           # versions from Directory.Build.props
#   ./BuildXcFrameworks.sh 3.14.0    # override the dd-sdk-ios tag (otel still from props)
#
# Requires Xcode (not just the command-line tools) and network access for the two clones and the
# KSCrash SwiftPM checkout. Takes 10-20 minutes on a warm machine.
#
# Set DATADOG_BUILD_DIR to keep and reuse the source checkouts between runs; by default a
# temporary directory is used and removed on exit.
#
# Reproducibility: everything is pinned - dd-sdk-ios by git tag $DATADOG_VERSION,
# opentelemetry-swift-packages by git tag $OTEL_VERSION, and KSCrash by the exact version in the
# Datadog project's package manifest. The output still varies byte-for-byte with the Xcode used to
# compile it, which is why libs/BUILD-INFO.txt records that too.

cd "$(dirname "$0")"

ROOT="$(cd .. && pwd)"
PROPS="$ROOT/Directory.Build.props"
LIBS="$ROOT/libs"

DATADOG_VERSION="$1"
if [ -z "$DATADOG_VERSION" ]; then
    DATADOG_VERSION=$(sed -n 's:.*<DatadogNativeVersion>\(.*\)</DatadogNativeVersion>.*:\1:p' "$PROPS" | head -1)
fi
OTEL_VERSION=$(sed -n 's:.*<DatadogOtelVersion>\(.*\)</DatadogOtelVersion>.*:\1:p' "$PROPS" | head -1)

if [ -z "$DATADOG_VERSION" ] || [ -z "$OTEL_VERSION" ]; then
    echo "error: could not read DatadogNativeVersion / DatadogOtelVersion from $PROPS" >&2
    exit 1
fi

for version in "$DATADOG_VERSION" "$OTEL_VERSION"; do
    case "$version" in
        *[!A-Za-z0-9._-]*)
            echo "error: invalid version '$version'" >&2
            exit 1
            ;;
    esac
done

if [ -n "$DATADOG_BUILD_DIR" ]; then
    WORK="$DATADOG_BUILD_DIR"
    mkdir -p "$WORK"
else
    WORK="$(mktemp -d)"
    trap 'rm -rf "$WORK"' EXIT
fi

DD_REPO="$WORK/dd-sdk-ios"
OTEL_REPO="$WORK/opentelemetry-swift-packages"
ARCHIVES="$WORK/archives"
# Under libs/, not under $WORK: $WORK is a mktemp directory removed on exit, and dSYMs that only
# ever exist there cannot be attached to a release - which is the one thing they are for. Next to
# the frameworks they survive the build, ride the same CI cache, and are never packed (the binding
# projects reference libs/<Framework>.xcframework by name; nothing globs libs/).
DSYMS="$LIBS/dsyms"

echo "==> building dd-sdk-ios $DATADOG_VERSION + OpenTelemetryApi $OTEL_VERSION for Mac Catalyst"
echo "    work directory: $WORK"

# ---------------------------------------------------------------------------------------------
# Sources, pinned by tag.
# ---------------------------------------------------------------------------------------------

if [ ! -d "$DD_REPO" ]; then
    git clone --depth 1 --branch "$DATADOG_VERSION" https://github.com/DataDog/dd-sdk-ios.git "$DD_REPO"
fi
if [ ! -d "$OTEL_REPO" ]; then
    git clone --depth 1 --branch "$OTEL_VERSION" https://github.com/DataDog/opentelemetry-swift-packages.git "$OTEL_REPO"
fi

# ---------------------------------------------------------------------------------------------
# Guard the second pin. DatadogOtelVersion is maintained by hand in Directory.Build.props and
# must match the OpenTelemetryApi version the checked-out dd-sdk-ios tag pins in its
# Cartfile.resolved - bumping the native version and forgetting the OTEL line would otherwise
# silently build the wrong OpenTelemetryApi. Checked here, against the actual checkout, so the
# mismatch fails the build instead of shipping.
# ---------------------------------------------------------------------------------------------

CARTFILE="$DD_REPO/Cartfile.resolved"
if [ "$DATADOG_SKIP_OTEL_CHECK" = "1" ]; then
    echo "==> skipping the DatadogOtelVersion check (DATADOG_SKIP_OTEL_CHECK=1)"
else
    resolved=""
    if [ -f "$CARTFILE" ]; then
        resolved=$(grep -i 'opentelemetry' "$CARTFILE" | grep -oE '"[0-9][A-Za-z0-9._-]*"' | tail -1 | tr -d '"')
    fi
    if [ -z "$resolved" ]; then
        echo "error: could not read the OpenTelemetryApi version from $CARTFILE." >&2
        echo "       dd-sdk-ios $DATADOG_VERSION no longer pins it there. Find where the new tag pins" >&2
        echo "       it, update DatadogOtelVersion in Directory.Build.props, and update this check." >&2
        echo "       DATADOG_SKIP_OTEL_CHECK=1 skips it if the pin has genuinely moved." >&2
        exit 1
    fi
    if [ "$resolved" != "$OTEL_VERSION" ]; then
        echo "error: DatadogOtelVersion is $OTEL_VERSION, but dd-sdk-ios $DATADOG_VERSION pins OpenTelemetryApi $resolved." >&2
        echo "       Update DatadogOtelVersion in Directory.Build.props to $resolved. Building with a" >&2
        echo "       mismatched pin links the Datadog frameworks against one OpenTelemetryApi and" >&2
        echo "       ships another." >&2
        exit 1
    fi
    echo "==> DatadogOtelVersion $OTEL_VERSION matches dd-sdk-ios $DATADOG_VERSION's Cartfile.resolved"
fi

# ---------------------------------------------------------------------------------------------
# Patch the Datadog Xcode project for Catalyst. Two things stand between the source - which
# compiles for Catalyst - and an archive that actually builds:
#
#   * SUPPORTS_MACCATALYST = NO on every framework target. A command-line override cannot fix
#     this one: xcodebuild validates the destination against the *project's* settings before any
#     override applies, and rejects 'variant=Mac Catalyst' outright.
#
#   * platformFilters on target dependencies and link phases. Several targets (WebViewTracking,
#     SessionReplay among them) declare their dependency on DatadogInternal with
#     'platformFilters = (ios, xros)'. Catalyst is its own filter value, not a flavour of 'ios',
#     so on a Catalyst build the dependency silently drops out and the archive dies with
#     "unable to resolve module dependency: 'DatadogInternal'".
#
# Both are packaging decisions in the project file, not properties of the code; patching them is
# the same move upstream itself makes when it injects DD_XCODEBUILD_PATCH into the OpenTelemetry
# package manifest to build that xcframework.
# ---------------------------------------------------------------------------------------------

PBX="$DD_REPO/Datadog/Datadog.xcodeproj/project.pbxproj"
python3 - "$PBX" <<'EOF'
import re
import sys

path = sys.argv[1]
text = open(path).read()

# Every patch below is a text replacement against an Xcode-generated file, and a replacement
# whose pattern has drifted out from under it is a silent no-op that only surfaces later, as a
# cryptic xcodebuild failure. So each patch asserts it either changed something or found the
# already-patched state (a reused DATADOG_BUILD_DIR checkout is patched twice without harm), and
# anything else fails loudly here, naming the patch that missed.
failures = []

supports_no = text.count("SUPPORTS_MACCATALYST = NO;")
supports_yes = text.count("SUPPORTS_MACCATALYST = YES;")
text = text.replace("SUPPORTS_MACCATALYST = NO;", "SUPPORTS_MACCATALYST = YES;")
if supports_no == 0 and supports_yes == 0:
    failures.append(
        "SUPPORTS_MACCATALYST: found neither '= NO;' to patch nor an existing '= YES;'. "
        "Upstream has moved or reformatted the setting (an .xcconfig, perhaps); without it the "
        "archive step rejects the Catalyst destination outright.")

# The singular form appears on link-phase entries and cannot carry two values.
singular = text.count("platformFilter = ios;")
singular_patched = text.count("platformFilters = (ios, maccatalyst, );")
text = text.replace("platformFilter = ios;", "platformFilters = (ios, maccatalyst, );")

# List form, both the inline and the one-value-per-line layout.
lists_patched = 0
def add_maccatalyst(match):
    global lists_patched
    body = match.group(1)
    if "maccatalyst" in body or "ios" not in body:
        return match.group(0)
    lists_patched += 1
    return match.group(0).replace("ios,", "ios, maccatalyst,", 1)

text = re.sub(r"platformFilters = \(([^)]*)\)", add_maccatalyst, text)

if singular + singular_patched + lists_patched == 0 and "maccatalyst" not in text:
    failures.append(
        "platformFilter(s): nothing was patched and no filter mentions maccatalyst. Catalyst "
        "would silently drop inter-framework dependencies and the archive would die with "
        "\"unable to resolve module dependency: 'DatadogInternal'\". If upstream has removed "
        "platform filters entirely this check can go; otherwise the patterns need updating.")

if failures:
    print("error: the pbxproj patches no longer match upstream's project file:", file=sys.stderr)
    for failure in failures:
        print("  * " + failure, file=sys.stderr)
    print("  Review the patch block in build/BuildXcFrameworks.sh against " + path, file=sys.stderr)
    sys.exit(1)

print("    patched SUPPORTS_MACCATALYST on %d target(s), %d singular and %d list platform filter(s)"
      % (supports_no, singular, lists_patched))

open(path, "w").write(text)
EOF

# ---------------------------------------------------------------------------------------------
# Post-build guards. The patches above assert they *applied*; these assert they *worked*. A
# stale patch does not always kill the archive - a target that silently loses a dependency or a
# build setting can still produce a binary, just a gutted one: fewer classes, no Objective-C
# surface, nothing for the bindings to bind. That failure would otherwise surface as a
# MissingMethodException in a consuming app, so each xcframework is checked the moment it is
# created: `nm -gU` on both architecture slices must show a canonical exported symbol.
#
# The table pins one or two load-bearing exports per framework - the Objective-C class behind
# the package's main entry point wherever the framework exports Objective-C at all.
# DatadogFlags and DatadogProfiling export no ObjC classes (no Objective-C API upstream yet),
# and OpenTelemetryApi is pure Swift; for those the anchor is the Swift nominal type descriptor
# of the module's entry type instead (mangled names verified with `xcrun swift-demangle`):
#
#   _$s12DatadogFlags0B0OMn        nominal type descriptor for DatadogFlags.Flags
#   _$s16DatadogProfiling0B0OMn    nominal type descriptor for DatadogProfiling.Profiling
#   _$s16OpenTelemetryApi0aB0VMn   nominal type descriptor for OpenTelemetryApi.OpenTelemetry
# ---------------------------------------------------------------------------------------------

typeset -A CANONICAL_EXPORTS
CANONICAL_EXPORTS=(
    DatadogInternal         '_OBJC_CLASS_$_DDInternalLogger'
    DatadogCore             '_OBJC_CLASS_$_DDDatadog _OBJC_CLASS_$_DDConfiguration'
    DatadogLogs             '_OBJC_CLASS_$_DDLogs _OBJC_CLASS_$_DDLogger'
    DatadogTrace            '_OBJC_CLASS_$_DDTrace _OBJC_CLASS_$_DDTracer'
    DatadogRUM              '_OBJC_CLASS_$_DDRUM _OBJC_CLASS_$_DDRUMMonitor'
    DatadogCrashReporting   '_OBJC_CLASS_$_DDCrashReporter'
    DatadogWebViewTracking  '_OBJC_CLASS_$_DDWebViewTracking'
    DatadogSessionReplay    '_OBJC_CLASS_$_DDSessionReplay _OBJC_CLASS_$_DDSessionReplayConfiguration'
    DatadogFlags            '_$s12DatadogFlags0B0OMn'
    DatadogProfiling        '_$s16DatadogProfiling0B0OMn'
    OpenTelemetryApi        '_$s16OpenTelemetryApi0aB0VMn'
)

assert_canonical_exports() {
    local framework="$1"
    local symbols="${CANONICAL_EXPORTS[$framework]:-}"

    if [ -z "$symbols" ]; then
        echo "error: no canonical exports are recorded for $framework." >&2
        echo "       A new framework needs an entry in CANONICAL_EXPORTS: pick a stable exported" >&2
        echo "       symbol with 'nm -gU' on the built binary, so a gutted build of it fails here" >&2
        echo "       like the others do." >&2
        exit 1
    fi

    # The path both asserts and documents the one slice this build produces - the exact
    # directory name the README and the package tests promise. $framework.framework/$framework
    # is the bundle-root symlink into Versions/, which nm follows.
    local slice="$LIBS/$framework.xcframework/ios-arm64_x86_64-maccatalyst"
    local binary="$slice/$framework.framework/$framework"
    if [ ! -f "$binary" ]; then
        echo "error: $framework.xcframework has no ios-arm64_x86_64-maccatalyst slice ($binary)." >&2
        echo "       That name is a shipped promise - the bindings and the package tests expect" >&2
        echo "       exactly it - so if -create-xcframework started naming the slice differently," >&2
        echo "       the change has to be deliberate, here and there together." >&2
        exit 1
    fi

    # Both slices checked separately: a universal binary with one healthy and one gutted
    # architecture would pass a single fat-file scan. The export list is captured once per arch
    # and grepped from a herestring - `nm | grep -q` would let grep exit before nm finishes
    # writing, and under this script's pipefail that SIGPIPE turns a *found* symbol into a
    # failure on the larger binaries.
    local arch symbol exports
    for arch in arm64 x86_64; do
        exports=$(nm -gU -arch "$arch" "$binary" | awk '{print $3}')
        for symbol in ${=symbols}; do
            if ! grep -qxF "$symbol" <<< "$exports"; then
                echo "error: $framework ($arch) no longer exports $symbol." >&2
                echo "       The archive succeeded but produced a gutted module - the usual cause" >&2
                echo "       is a pbxproj patch above matching on text but no longer doing what it" >&2
                echo "       did (a dropped dependency or build setting still archives). If" >&2
                echo "       upstream genuinely renamed or removed the symbol, update" >&2
                echo "       CANONICAL_EXPORTS - after checking the bindings survive the change." >&2
                exit 1
            fi
        done
    done
    echo "    canonical exports present in both slices: $symbols"
}

# Strict on purpose, where this used to be a silent `2>/dev/null || true`: these dSYMs are the
# only symbolication data the binaries will ever have - Datadog publishes no Catalyst builds, so
# no one else holds them - and a build that quietly drops one produces a release whose crashes
# can never be symbolicated. An archive without a dSYM means the archive settings changed
# (DEBUG_INFORMATION_FORMAT, most likely); that is a build failure, not a shrug.
copy_dsym() {
    local dsym="$1"
    if [ ! -d "$dsym" ]; then
        echo "error: the archive produced no dSYM at $dsym." >&2
        echo "       The release attaches these for crash symbolication and they exist nowhere" >&2
        echo "       else. Fix whatever stopped the archive emitting dSYMs rather than shipping" >&2
        echo "       binaries that can never be symbolicated." >&2
        exit 1
    fi
    cp -R "$dsym" "$DSYMS/"
}

# ---------------------------------------------------------------------------------------------
# OpenTelemetryApi for Mac Catalyst, following upstream's scripts/build.sh for that repository:
# archive the SwiftPM library scheme (the product lands in usr/local/lib), then graft the
# swiftmodule in by hand, because BUILD_LIBRARY_FOR_DISTRIBUTION only puts it in the build tree.
# DD_XCODEBUILD_PATCH makes the package build a dynamic library and gives it a second scheme,
# both of which the archive needs - see the trailer of that repository's Package.swift.
# ---------------------------------------------------------------------------------------------

OTEL_FRAMEWORK="$ARCHIVES/OpenTelemetryApi/catalyst.xcarchive/Products/usr/local/lib/OpenTelemetryApi.framework"

if [ ! -d "$OTEL_FRAMEWORK" ]; then
    echo "==> archiving OpenTelemetryApi for Mac Catalyst"
    (cd "$OTEL_REPO" && DD_XCODEBUILD_PATCH=1 xcodebuild archive \
        -workspace . \
        -scheme OpenTelemetryApi \
        -destination 'generic/platform=macOS,variant=Mac Catalyst' \
        -archivePath "$ARCHIVES/OpenTelemetryApi/catalyst" \
        -derivedDataPath "$WORK/otel-derived" \
        SKIP_INSTALL=NO \
        BUILD_LIBRARY_FOR_DISTRIBUTION=YES \
        ARCHS="x86_64 arm64" \
        ONLY_ACTIVE_ARCH=NO \
        -quiet)

    # Into Versions/A with a root symlink, not a real root directory as upstream's build.sh does:
    # a Catalyst framework is a macOS-style versioned bundle, and codesign refuses to embed one
    # with "unsealed contents present in the root directory" - which is exactly what a real
    # Modules/ at the root is. Upstream gets away with it because nobody embeds its macOS slice.
    mkdir -p "$OTEL_FRAMEWORK/Versions/A/Modules"
    cp -R "$WORK/otel-derived/Build/Intermediates.noindex/ArchiveIntermediates/OpenTelemetryApi/BuildProductsPath/Release-maccatalyst/OpenTelemetryApi.swiftmodule" \
        "$OTEL_FRAMEWORK/Versions/A/Modules/"
    ln -sfn Versions/Current/Modules "$OTEL_FRAMEWORK/Modules"
fi

rm -rf "$LIBS"
mkdir -p "$LIBS" "$DSYMS"

echo "==> creating OpenTelemetryApi.xcframework"
xcodebuild -create-xcframework \
    -framework "$OTEL_FRAMEWORK" \
    -output "$LIBS/OpenTelemetryApi.xcframework"
assert_canonical_exports OpenTelemetryApi
copy_dsym "$ARCHIVES/OpenTelemetryApi/catalyst.xcarchive/dSYMs/OpenTelemetryApi.framework.dSYM"

# The Datadog project links OpenTelemetryApi as the Carthage binary at this exact path; give it
# one that actually has a Catalyst slice.
rm -rf "$DD_REPO/Carthage/Build/OpenTelemetryApi.xcframework"
mkdir -p "$DD_REPO/Carthage/Build"
cp -R "$LIBS/OpenTelemetryApi.xcframework" "$DD_REPO/Carthage/Build/"

# ---------------------------------------------------------------------------------------------
# The Datadog frameworks themselves. Same archive settings as upstream's release script; KSCrash
# arrives through the project's own SwiftPM reference and is compiled for Catalyst along the way,
# then linked into DatadogCrashReporting - no separate handling needed.
#
# The frameworks the archive produces are macOS-style versioned bundles (Versions/A + symlinks),
# unlike the shallow bundles iOS archives produce. That is correct for Catalyst; the binding
# projects compress the payload (CompressBindingResourcePackage) so the symlinks survive NuGet.
# ---------------------------------------------------------------------------------------------

SCHEMES="DatadogInternal DatadogCore DatadogLogs DatadogTrace DatadogRUM DatadogCrashReporting DatadogFlags DatadogProfiling DatadogWebViewTracking DatadogSessionReplay"

for scheme in ${=SCHEMES}; do
    archive="$ARCHIVES/$scheme/catalyst"
    if [ ! -d "$archive.xcarchive" ]; then
        echo "==> archiving $scheme for Mac Catalyst"
        (cd "$DD_REPO" && xcodebuild archive \
            -workspace Datadog.xcworkspace \
            -scheme "$scheme" \
            -destination 'generic/platform=macOS,variant=Mac Catalyst' \
            -archivePath "$archive" \
            SKIP_INSTALL=NO \
            BUILD_LIBRARY_FOR_DISTRIBUTION=YES \
            ONLY_ACTIVE_ARCH=NO \
            -quiet)
    fi

    fwk="$archive.xcarchive/Products/Library/Frameworks/$scheme.framework"
    if [ ! -d "$fwk" ]; then
        echo "error: archiving $scheme produced no framework" >&2
        exit 1
    fi

    echo "==> creating $scheme.xcframework"
    # dSYMs are deliberately not baked into the xcframeworks: they would roughly double every
    # package for bytes no consumer can use. They are kept next to the build so a release can
    # attach them for crash symbolication.
    xcodebuild -create-xcframework \
        -framework "$fwk" \
        -output "$LIBS/$scheme.xcframework"
    assert_canonical_exports "$scheme"
    copy_dsym "$archive.xcarchive/dSYMs/$scheme.framework.dSYM"
done

# ---------------------------------------------------------------------------------------------
# One dSYM per framework, counted before anything gets recorded as done. copy_dsym already fails
# on a missing bundle, so this is the structural complement: it catches the copies and the
# scheme list drifting apart - a framework added to SCHEMES whose dSYM path changed shape, or a
# stray extra bundle from an earlier layout - rather than any single copy going missing.
# ---------------------------------------------------------------------------------------------

expected_dsyms=$(( $(echo "$SCHEMES" | wc -w) + 1 ))   # every scheme, plus OpenTelemetryApi
actual_dsyms=$(ls -d "$DSYMS"/*.dSYM 2>/dev/null | wc -l | tr -d ' ')
if [ "$actual_dsyms" -ne "$expected_dsyms" ]; then
    echo "error: expected $expected_dsyms dSYMs in $DSYMS, found $actual_dsyms:" >&2
    ls "$DSYMS" >&2
    echo "       The release attaches exactly one dSYM per shipped framework; a mismatch means" >&2
    echo "       the scheme list and the dSYM export in this script have drifted apart." >&2
    exit 1
fi
echo "==> all $actual_dsyms dSYMs exported to $DSYMS"

# ---------------------------------------------------------------------------------------------
# Record what was built with what. The binding packages' version already pins the Datadog
# version; this file is for the human diffing two builds that claim the same version.
# ---------------------------------------------------------------------------------------------

cat > "$LIBS/BUILD-INFO.txt" <<EOF
dd-sdk-ios: $DATADOG_VERSION
opentelemetry-swift-packages: $OTEL_VERSION
xcode: $(xcodebuild -version | tr '\n' ' ')
macos-sdk: $(xcrun --sdk macosx --show-sdk-version)
built: $(date -u +%Y-%m-%dT%H:%M:%SZ)
EOF

echo
echo "==> built into $LIBS:"
ls "$LIBS"
echo
echo "    dSYMs (attached to the GitHub release, for Datadog crash symbolication) are in:"
echo "    $DSYMS"
