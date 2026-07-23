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
DSYMS="$WORK/dsyms"

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

text = text.replace("SUPPORTS_MACCATALYST = NO;", "SUPPORTS_MACCATALYST = YES;")

# The singular form appears on link-phase entries and cannot carry two values.
text = text.replace("platformFilter = ios;", "platformFilters = (ios, maccatalyst, );")

# List form, both the inline and the one-value-per-line layout. Idempotent, so a reused
# DATADOG_BUILD_DIR checkout is patched twice without harm.
def add_maccatalyst(match):
    body = match.group(1)
    if "maccatalyst" in body or "ios" not in body:
        return match.group(0)
    return match.group(0).replace("ios,", "ios, maccatalyst,", 1)

text = re.sub(r"platformFilters = \(([^)]*)\)", add_maccatalyst, text)

open(path, "w").write(text)
EOF

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
cp -R "$ARCHIVES/OpenTelemetryApi/catalyst.xcarchive/dSYMs/OpenTelemetryApi.framework.dSYM" "$DSYMS/" 2>/dev/null || true

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
    cp -R "$archive.xcarchive/dSYMs/$scheme.framework.dSYM" "$DSYMS/" 2>/dev/null || true
done

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
echo "    dSYMs (for a GitHub release / Datadog symbolication) are in:"
echo "    $DSYMS"
