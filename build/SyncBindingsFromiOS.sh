#!/bin/sh

set -e

# Copies the binding definitions from a DatadogNet.iOS checkout into this repository.
#
# The Mac Catalyst bindings are deliberately not written here: Catalyst is UIKit-based, so the
# Objective-C surface Objective Sharpie produced for iOS - with all the hand-applied fixes the
# DatadogNet.iOS repository carries on top - compiles unchanged for net*-maccatalyst. What this
# repository owns is the packaging: the Catalyst-built native frameworks and the .Mac package
# identities. Keeping the sources as verbatim copies of the iOS repository's means a binding fix
# lands once, there, and arrives here by re-running this script.
#
# Usage:
#   ./SyncBindingsFromiOS.sh                     # sibling checkout at ../../DatadogNet.iOS
#   ./SyncBindingsFromiOS.sh /path/to/DatadogNet.iOS
#
# Copies ApiDefinitions.cs, StructsAndEnums.cs and Additions/ for every module. Run it after
# bumping DatadogNativeVersion to whatever the iOS repository binds, diff, and commit. Do not
# edit the copied files here - edit them in DatadogNet.iOS and re-sync.

cd "$(dirname "$0")"

ROOT="$(cd .. && pwd)"
IOS_REPO="${1:-$ROOT/../DatadogNet.iOS}"

if [ ! -d "$IOS_REPO/src" ]; then
    echo "error: no DatadogNet.iOS checkout at $IOS_REPO" >&2
    echo "       pass the path to one as the first argument" >&2
    exit 1
fi

MODULES="Core CrashReporting Flags Internal Logs OpenTelemetryApi Profiling RUM SessionReplay Trace WebViewTracking"

for module in $MODULES; do
    src="$IOS_REPO/src/DatadogNet.$module.iOS"
    dst="$ROOT/src/DatadogNet.$module.Mac"

    if [ ! -d "$src" ]; then
        echo "error: $src does not exist - has the iOS repository restructured?" >&2
        exit 1
    fi
    mkdir -p "$dst"

    for file in ApiDefinitions.cs StructsAndEnums.cs; do
        if [ -f "$src/$file" ]; then
            cp "$src/$file" "$dst/$file"
        fi
    done

    if [ -d "$src/Additions" ]; then
        rm -rf "$dst/Additions"
        cp -R "$src/Additions" "$dst/Additions"
    fi

    echo "synced DatadogNet.$module.Mac"
done

echo
echo "Review with 'git diff' before committing."
