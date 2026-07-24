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
#
# The commit the copies were taken from is recorded in build/ios-bindings-source.txt, and CI's
# binding-drift job re-runs this sync against exactly that commit and fails on any difference -
# so "do not edit here" is an enforced invariant, not a convention. A sync taken from an iOS
# checkout with uncommitted changes records a dirty marker instead, which disarms the guard
# (loudly) until a clean sync records a real commit.
#
# shims/ is deliberately NOT synced. The iOS repository's shims/DatadogFlagsObjc is an unshipped
# prototype whose build recipe targets iOS; if it ever ships - giving DatadogFlags a real ObjC
# surface - this script must grow shims/ handling with a Catalyst build target at the same time,
# or the next sync will copy an ApiDefinitions.cs that references a shim this repository cannot
# build.

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

# Record where the copies came from, for the CI drift guard.
REF_FILE="$ROOT/build/ios-bindings-source.txt"
if git -C "$IOS_REPO" rev-parse HEAD >/dev/null 2>&1; then
    sha=$(git -C "$IOS_REPO" rev-parse HEAD)
    if [ -n "$(git -C "$IOS_REPO" status --porcelain -- src/ 2>/dev/null)" ]; then
        printf '%s dirty\n' "$sha" > "$REF_FILE"
        echo
        echo "WARNING: the iOS checkout has uncommitted binding changes; recorded $sha as dirty."
        echo "         The CI drift guard is disarmed until a sync from a committed tree."
    else
        printf '%s\n' "$sha" > "$REF_FILE"
        echo
        echo "Recorded DatadogNet.iOS@$sha in build/ios-bindings-source.txt."
    fi
else
    echo
    echo "WARNING: $IOS_REPO is not a git checkout; build/ios-bindings-source.txt not updated."
fi

echo "Review with 'git diff' before committing."
