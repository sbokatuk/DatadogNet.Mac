#!/bin/sh
set -eu

# Starts a Datadog SDK upgrade by rewriting every place this repository pins the native version,
# then prints the steps a script cannot do. The pins are more numerous than they look - the two
# version properties, the OTEL companion pin, the README's copy-paste install snippets, the
# release-notes file the packages embed - and every one of them has been forgotten by hand at
# least once somewhere across the DatadogNet repositories. One command, one consistent tree.
#
# Usage:
#   ./BumpNativeVersion.sh 3.15.0    # the dd-sdk-ios tag being moved to
#
# What it does:
#   * Directory.Build.props: DatadogNativeVersion to the new version, DatadogBindingRevision
#     back to 1 (first release on a new native line), DatadogOtelVersion to whatever the new
#     dd-sdk-ios tag pins in its Cartfile.resolved - fetched from the tag on GitHub, the same
#     file BuildXcFrameworks.sh cross-checks against the actual checkout at build time.
#   * README.md: every pinned package version the install snippets carry, the dd-sdk-ios badge
#     (its label, the shields.io image path and the release link) and the "Built against"
#     prose (the same spots CheckReadmeVersions.sh guards - and that check is run at the end,
#     so this script leaves the tree passing it).
#   * docs/release-notes/<new version>.1.md: scaffolded if absent. Mind the TODOs in it: the
#     file ships verbatim as PackageReleaseNotes in every package and as the GitHub release
#     body.
#
# What it deliberately does not do: sync the bindings (the matching DatadogNet.iOS release may
# not exist yet), build, test, or tag. Those remain manual and are printed at the end, in order.

cd "$(dirname "$0")"

ROOT="$(cd .. && pwd)"
PROPS="$ROOT/Directory.Build.props"
README="$ROOT/README.md"

NEW="${1:-}"
if [ -z "$NEW" ]; then
    echo "usage: $0 <dd-sdk-ios version>   e.g. $0 3.15.0" >&2
    exit 2
fi
case "$NEW" in
    *[!0-9.]* | *.*.*.* | .* | *. )
        echo "error: '$NEW' does not look like a dd-sdk-ios version (expected e.g. 3.15.0" >&2
        echo "       - three numeric components; the fourth, the binding revision, is this" >&2
        echo "       repository's and resets to 1)" >&2
        exit 2
        ;;
    *.*.* ) ;;
    * )
        echo "error: '$NEW' does not look like a dd-sdk-ios version (expected e.g. 3.15.0)" >&2
        exit 2
        ;;
esac

prop() {
    sed -n "s/.*<$1>\(.*\)<\/$1>.*/\1/p" "$PROPS" | head -1
}

OLD_NATIVE="$(prop DatadogNativeVersion)"
OLD_REVISION="$(prop DatadogBindingRevision)"
OLD_OTEL="$(prop DatadogOtelVersion)"
if [ -z "$OLD_NATIVE" ] || [ -z "$OLD_REVISION" ] || [ -z "$OLD_OTEL" ]; then
    echo "error: could not read the version properties from $PROPS" >&2
    exit 1
fi

# Same-version "bumps" are refused rather than absorbed: re-running this against the current
# native version would silently reset the binding revision to 1, and if $OLD_NATIVE.1 has
# shipped, that is a version that can never be published again. Revision bumps (binding or
# packaging changes on the same native line) are a one-property edit, not an upgrade.
if [ "$NEW" = "$OLD_NATIVE" ]; then
    echo "error: this repository already pins dd-sdk-ios $OLD_NATIVE (currently at binding" >&2
    echo "       revision $OLD_REVISION). To release again on the same native line, bump" >&2
    echo "       DatadogBindingRevision in Directory.Build.props by hand instead." >&2
    exit 1
fi

VERSION="$NEW.1"

# ---------------------------------------------------------------------------------------------
# The OTEL companion pin, read from the new tag before anything is rewritten - if the tag does
# not exist yet (upstream not released, or a typo), the tree stays untouched. The parse mirrors
# the cross-check in BuildXcFrameworks.sh, which re-verifies the same line against the actual
# source checkout at build time; this is the early copy of that late check.
# ---------------------------------------------------------------------------------------------

CARTFILE_URL="https://raw.githubusercontent.com/DataDog/dd-sdk-ios/$NEW/Cartfile.resolved"
echo "==> reading the OpenTelemetryApi pin from dd-sdk-ios $NEW's Cartfile.resolved"
if ! cartfile="$(curl -fsSL "$CARTFILE_URL")"; then
    echo "error: could not fetch $CARTFILE_URL." >&2
    echo "       Does the dd-sdk-ios tag '$NEW' exist yet? Nothing has been changed." >&2
    exit 1
fi

OTEL="$(printf '%s\n' "$cartfile" | grep -i 'opentelemetry' | grep -oE '"[0-9][A-Za-z0-9._-]*"' | tail -1 | tr -d '"')"
if [ -z "$OTEL" ]; then
    echo "error: dd-sdk-ios $NEW's Cartfile.resolved no longer names an OpenTelemetryApi" >&2
    echo "       version. Find where the new tag pins it, set DatadogOtelVersion by hand, and" >&2
    echo "       update this script and the matching cross-check in BuildXcFrameworks.sh." >&2
    exit 1
fi

# ---------------------------------------------------------------------------------------------
# Rewrite the pins. sed into a temporary file and move as two separate statements, for two
# reasons: the originals are never half-written, and set -e actually catches a failing sed -
# in `sed ... && mv ...` a left-hand failure is exempt from errexit by POSIX rule, and the
# script would carry on with the file unmodified.
# ---------------------------------------------------------------------------------------------

echo "==> Directory.Build.props: $OLD_NATIVE.$OLD_REVISION -> $VERSION, OpenTelemetryApi $OLD_OTEL -> $OTEL"
sed \
    -e "s|<DatadogNativeVersion>[^<]*</DatadogNativeVersion>|<DatadogNativeVersion>$NEW</DatadogNativeVersion>|" \
    -e "s|<DatadogBindingRevision>[^<]*</DatadogBindingRevision>|<DatadogBindingRevision>1</DatadogBindingRevision>|" \
    -e "s|<DatadogOtelVersion>[^<]*</DatadogOtelVersion>|<DatadogOtelVersion>$OTEL</DatadogOtelVersion>|" \
    "$PROPS" > "$PROPS.tmp"
mv "$PROPS.tmp" "$PROPS"

# The same shapes CheckReadmeVersions.sh greps for: package pins in install snippets, any
# device-check invocation, the dd-sdk-ios badge (label, image path, release link) and the
# "Built against" prose. Prose describing the version *scheme* stays put, exactly as that check
# deliberately ignores it. (@ as the delimiter on the second expression: its pattern needs a
# literal ERE alternation |, which cannot also be the delimiter.)
echo "==> README.md: pinned package versions -> $VERSION, dd-sdk-ios -> $NEW"
sed -E \
    -e "s|(Include=\"DatadogNet[^\"]*\" +Version=\")[0-9][^\"]*(\")|\\1$VERSION\\2|g" \
    -e "s@(run-(simulator|emulator)-tests\.sh +)[0-9][0-9.]*@\\1$VERSION@g" \
    -e "s|(\[!\[dd-sdk-ios )[0-9][0-9.]*|\\1$NEW|g" \
    -e "s|(dd--sdk--ios-)[0-9][0-9.]*|\\1$NEW|g" \
    -e "s|(dd-sdk-ios/releases/tag/)[0-9][0-9.]*|\\1$NEW|g" \
    -e "s|(Built against \*\*dd-sdk-ios )[0-9][0-9.]*|\\1$NEW|g" \
    "$README" > "$README.tmp"
mv "$README.tmp" "$README"

# ---------------------------------------------------------------------------------------------
# Release-notes scaffold. Only ever created, never overwritten: a re-run must not clobber
# half-written notes.
# ---------------------------------------------------------------------------------------------

NOTES="$ROOT/docs/release-notes/$VERSION.md"
if [ -f "$NOTES" ]; then
    echo "==> docs/release-notes/$VERSION.md already exists; leaving it alone"
else
    echo "==> scaffolding docs/release-notes/$VERSION.md"
    mkdir -p "$ROOT/docs/release-notes"
    cat > "$NOTES" <<EOF
# $VERSION

<!-- TODO before tagging: this file ships VERBATIM - as PackageReleaseNotes in all eleven
     packages and as the GitHub release body. Every TODO below must go. -->

First release on [dd-sdk-ios $NEW](https://github.com/DataDog/dd-sdk-ios/releases/tag/$NEW),
built from source for Mac Catalyst. Package ids, namespaces and the version scheme are
unchanged; the fourth component is this repository's binding revision, starting again at 1 on
the new native line.

## What's new upstream

TODO: the dd-sdk-ios $NEW changes that matter to Catalyst consumers, from
https://github.com/DataDog/dd-sdk-ios/releases/tag/$NEW - and whether any of them are
iOS/iPadOS-only the way Session Replay is.

## Binding changes

TODO: what the re-sync from DatadogNet.iOS changed in the managed surface, or state that the
API is unchanged.

## Upgrading from $OLD_NATIVE.x

TODO: breaking changes and required app-side edits, or state there is nothing to change.
EOF
fi

# Leaves the tree agreeing with itself - the same check CI runs first.
./CheckReadmeVersions.sh

cat <<EOF

Pinned: dd-sdk-ios $NEW (was $OLD_NATIVE), OpenTelemetryApi $OTEL (was $OLD_OTEL), package
version $VERSION.

What a script cannot do, in order:

 1. Wait for (or produce) the DatadogNet.iOS release that binds dd-sdk-ios $NEW, then
    ./build/SyncBindingsFromiOS.sh /path/to/DatadogNet.iOS and review the diff.
 2. ./build/BuildXcFrameworks.sh (needs Xcode, ~15 minutes - watch the pbxproj patch and
    canonical-export guards: a new native line is exactly when they earn their keep).
 3. ./build/BuildNugets.sh && dotnet test tests/DatadogNet.Mac.PackageTests
 4. Build the sample. Update the README package table if the feature set moved.
 5. Finish docs/release-notes/$VERSION.md - it ships verbatim, TODOs and all.
 6. Commit, PR, and once green: git tag v$VERSION && git push origin v$VERSION
EOF
