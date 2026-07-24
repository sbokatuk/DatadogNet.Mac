#!/bin/sh
# Fails when README.md pins a package version that is not the one this repository currently
# builds. The install snippets are copy-paste starting points, and a hardcoded version there goes
# stale silently on every release - it would go stale the same way the sibling repositories' snippets did. Running
# this in CI makes the version bump before a release drag the README along with it.
#
# What is checked: every <PackageReference Include="DatadogNet..." Version="..."> pin, and the
# device-check example (run-simulator-tests.sh <version> ...). Prose that explains the version
# *scheme* ("3.14.0.1 wraps dd-sdk-ios 3.14.0") is deliberately not checked -
# it describes the format, not the current release.
set -eu

root="$(cd "$(dirname "$0")/.." && pwd)"
readme="$root/README.md"
props="$root/Directory.Build.props"

prop() {
  sed -n "s/.*<$1>\(.*\)<\/$1>.*/\1/p" "$props" | head -1
}

version="$(prop DatadogNativeVersion).$(prop DatadogBindingRevision)"

bad=0

old_ifs=$IFS
IFS='
'
for pin in $(grep -oE 'Include="DatadogNet[^"]*" +Version="[0-9][^"]*"' "$readme"); do
  id=$(printf '%s' "$pin" | sed -E 's/Include="([^"]*)".*/\1/')
  ver=$(printf '%s' "$pin" | sed -E 's/.*Version="([^"]*)"/\1/')
  if [ "$ver" != "$version" ]; then
    echo "README pins $id $ver, but the current version is $version" >&2
    bad=1
  fi
done

for token in $(grep -oE 'run-(simulator|emulator)-tests\.sh +[0-9][0-9.]*' "$readme" | grep -oE '[0-9][0-9.]*$'); do
  if [ "$token" != "$version" ]; then
    echo "README runs the device checks at $token, but the current version is $version" >&2
    bad=1
  fi
done
IFS=$old_ifs

if [ "$bad" -ne 0 ]; then
  echo "CheckReadmeVersions: README.md is stale - update the versions above (current: $version)" >&2
  exit 1
fi

echo "CheckReadmeVersions: README.md agrees with Directory.Build.props ($version)"
