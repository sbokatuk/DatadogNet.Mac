#!/bin/sh

set -e

# Builds and packs all eleven Datadog Mac Catalyst binding packages. Run ./BuildXcFrameworks.sh
# first.
#
# Usage:
#   ./BuildNugets.sh                 # version from Directory.Build.props
#   ./BuildNugets.sh 3.14.0.1-rc.1   # explicit package version
#   ./BuildNugets.sh 3.15.0.1 3.15.0 # ...and bind a different dd-sdk-ios line
#
# The second argument selects which native Datadog version to bind. Run BuildXcFrameworks.sh with
# the same version first, so the matching xcframeworks are present.
#
# Packages are written to ../artifacts.
#
# Each .NET SDK's maccatalyst workload supports only two target frameworks - the .NET 9 band
# builds net8/net9, and the .NET 10 band contributes net10 (Datadog.Binding.props points its pass
# at net10.0-maccatalyst26.0 alone) - so this runs two passes and merges them,
# exactly as DatadogNet.iOS does. The repository's global.json pins the .NET 9 SDK, so the second
# pass is invoked from a scratch directory carrying its own global.json, since the SDK is resolved
# from the working directory.

cd "$(dirname "$0")"

VERSION="$1"
NATIVE_VERSION="$2"
ROOT="$(cd .. && pwd)"
OUTPUT="$ROOT/artifacts"

PASS1_BAND="net9"
PASS2_BAND="net10"
PASS2_SDK="10.0.100"

# Packed in dependency order. Nothing requires it - ProjectReference means MSBuild builds each
# package's dependencies on demand - but it keeps the log readable and means a failure in a base
# package is reported before the packages built on top of it repeat the same error.
#
# No Objc entry, unlike DatadogNet.iOS: that id is a compatibility meta-package for apps
# upgrading from the 2.x iOS bindings, and no Mac Catalyst 2.x ever existed to upgrade from.
PACKAGES="Internal OpenTelemetryApi Core Trace Logs RUM SessionReplay WebViewTracking CrashReporting Flags Profiling"

VERSION_ARG=""
if [ -n "$VERSION" ]; then
    # Validated before being interpolated into MSBuild arguments and package file names.
    case "$VERSION" in
        *[!A-Za-z0-9.+_-]*)
            echo "error: invalid version '$VERSION'" >&2
            exit 1
            ;;
    esac
    VERSION_ARG="-p:Version=$VERSION"
fi

NATIVE_ARG=""
if [ -n "$NATIVE_VERSION" ]; then
    case "$NATIVE_VERSION" in
        *[!A-Za-z0-9.+_-]*)
            echo "error: invalid native version '$NATIVE_VERSION'" >&2
            exit 1
            ;;
    esac
    NATIVE_ARG="-p:DatadogNativeVersion=$NATIVE_VERSION"
fi

# NuGet.config declares ./artifacts as a package source, and restore fails outright with NU1301 if
# a local source directory is missing - before anything here has had a chance to create it as an
# output directory. A fresh clone only has it because an empty .gitkeep is committed, so make the
# build independent of that surviving.
mkdir -p "$OUTPUT"

PASS1_DIR="$OUTPUT/.net9-pass"
PASS2_DIR="$OUTPUT/.net10-pass"
rm -rf "$PASS1_DIR" "$PASS2_DIR"

SDK10_DIR="$(mktemp -d)"
trap 'rm -rf "$SDK10_DIR"' EXIT
cat > "$SDK10_DIR/global.json" <<EOF
{ "sdk": { "version": "$PASS2_SDK", "rollForward": "latestFeature" } }
EOF

for package in $PACKAGES; do
    project="$ROOT/src/DatadogNet.$package.Mac/DatadogNet.$package.Mac.csproj"

    echo "==> packing DatadogNet.$package.Mac ($PASS1_BAND band)"
    dotnet pack "$project" \
        -c Release \
        -p:DatadogSdkBand="$PASS1_BAND" \
        $VERSION_ARG $NATIVE_ARG \
        -o "$PASS1_DIR"

    echo "==> packing DatadogNet.$package.Mac ($PASS2_BAND band)"
    (cd "$SDK10_DIR" && dotnet pack "$project" \
        -c Release \
        -p:DatadogSdkBand="$PASS2_BAND" \
        $VERSION_ARG $NATIVE_ARG \
        -o "$PASS2_DIR")
done

echo "==> merging target frameworks"
python3 "$ROOT/build/merge-packages.py" "$PASS1_DIR" "$PASS2_DIR" "$OUTPUT"

rm -rf "$PASS1_DIR" "$PASS2_DIR"
