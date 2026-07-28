#!/bin/sh
# Fails when README.md pins a package version that is not the one this repository currently
# builds. The install snippets are copy-paste starting points, and a hardcoded version there goes
# stale silently on every release - it would go stale the same way the sibling repositories' snippets did. Running
# this in CI makes the version bump before a release drag the README along with it.
#
# What is checked: every <PackageReference Include="DatadogNet..." Version="..."> pin, the
# device-check example (run-simulator-tests.sh <version> ...), the dd-sdk-ios badge - which
# names the native version three times over: its label, the shields.io image path and the
# release link - and the "Built against **dd-sdk-ios N**" intro prose. Prose that explains the
# version *scheme* ("3.14.0.1 wraps dd-sdk-ios 3.14.0") is deliberately not checked -
# it describes the format, not the current release - and the anchored patterns below do not
# reach the historical "dd-sdk-ios 3.0 deleted that framework" style of sentence either.
set -eu

root="$(cd "$(dirname "$0")/.." && pwd)"
readme="$root/README.md"
props="$root/Directory.Build.props"

prop() {
  sed -n "s/.*<$1>\(.*\)<\/$1>.*/\1/p" "$props" | head -1
}

native="$(prop DatadogNativeVersion)"
version="$native.$(prop DatadogBindingRevision)"

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

# The badge and the prose, each matched by its own anchored pattern so the error names the spot
# that went stale - and each required to exist, so deleting one does not pass as up to date.
check_native() {
  pattern=$1
  what=$2
  found=0
  for token in $(grep -oE "$pattern" "$readme" | grep -oE '[0-9][0-9.]*'); do
    found=1
    if [ "$token" != "$native" ]; then
      echo "README $what says dd-sdk-ios $token, but the bound native version is $native" >&2
      bad=1
    fi
  done
  if [ "$found" -eq 0 ]; then
    echo "README has no $what to check (expected to match: $pattern)" >&2
    bad=1
  fi
}

check_native '\[!\[dd-sdk-ios [0-9][0-9.]*\]' 'badge label'
check_native 'dd--sdk--ios-[0-9][0-9.]*' 'badge image path'
check_native 'dd-sdk-ios/releases/tag/[0-9][0-9.]*' 'badge release link'
check_native 'Built against \*\*dd-sdk-ios [0-9][0-9.]*\*\*' '"Built against" prose'
IFS=$old_ifs

if [ "$bad" -ne 0 ]; then
  echo "CheckReadmeVersions: README.md is stale - update the versions above (current: $version)" >&2
  exit 1
fi

echo "CheckReadmeVersions: README.md agrees with Directory.Build.props ($version)"
