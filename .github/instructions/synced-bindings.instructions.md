---
applyTo: "src/**/ApiDefinitions.cs,src/**/StructsAndEnums.cs,src/**/Additions/**"
---

# Synced binding sources — do not edit here

- These files are verbatim copies of `sbokatuk/DatadogNet.iOS`'s `src/DatadogNet.<Module>.iOS/` sources, written by `build/SyncBindingsFromiOS.sh`. Catalyst is UIKit-based, so the same Objective-C surface compiles for both platforms; keeping the copies byte-identical is what lets shared code compile unchanged against either package set.
- Refuse direct edits, including "harmless" ones — renames, formatting, doc comments, `#if` guards. Route the change to DatadogNet.iOS instead, then bring it here by re-running the sync.
- To land a binding change:
  1. Make and merge it in DatadogNet.iOS.
  2. Run `./build/SyncBindingsFromiOS.sh [path to DatadogNet.iOS]` (defaults to the sibling checkout `../DatadogNet.iOS`).
  3. Review `git diff`, then commit the synced files together with the `build/ios-bindings-source.txt` the script rewrote.
- `build/ios-bindings-source.txt` records the iOS commit the copies came from. Only the sync script writes it. A sync taken from an iOS checkout with uncommitted changes records a `dirty` marker, which disarms the CI guard with a warning — replace it with a clean sync before releasing.
- `binding-drift.yml` checks out DatadogNet.iOS at the recorded commit, re-runs the sync and fails on any difference, on every pull request, every release and every Monday. Editing here does not "work locally"; it breaks CI by design.
- `shims/` is deliberately outside the sync. If DatadogNet.iOS ever ships its Flags shim, the sync script needs a Catalyst build target for it before the next sync — see the script's header.
- What this repository *does* own next to these files: each project's identity and description, `src/Datadog.Binding.props`, and `src/DatadogNet.Core.Mac/buildTransitive/`. Those are editable normally.
