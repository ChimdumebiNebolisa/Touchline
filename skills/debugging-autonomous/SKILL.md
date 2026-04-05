# debugging-autonomous

## Purpose

Use this skill when something is broken, failing verification, behaving inconsistently, or blocking progress on the active Plan step.

This skill exists to make debugging disciplined, evidence-based, and architecture-safe.

The goal is not to randomly patch symptoms.
The goal is to:
- reproduce the issue
- isolate the failing layer
- identify the real cause
- apply the smallest correct fix
- verify the fix
- avoid scope creep while debugging

## Inputs

You need:
- the repository
- `AGENTS.md`
- `docs/PRD.md`
- `docs/Architecture.md`
- `docs/Guardrails.md`
- `docs/Plan.md`
- `AUTONOMOUS_PROGRESS.md` if present
- the failing code, logs, tests, screenshots, or command output
- terminal access

## When to use

Use this skill when:
- a test fails
- lint or typecheck fails
- build fails
- runtime behavior is wrong
- UI and domain behavior disagree
- a feature seems implemented but does not work correctly
- a regression appears
- Codex is blocked by an error during the active Plan step

Do not use this skill to justify unrelated rewrites.

## Required procedure

### 1. Reconfirm the valid target

Before debugging:
- read `AGENTS.md`
- read the four source-of-truth docs
- read `AUTONOMOUS_PROGRESS.md` if present
- confirm the issue is inside the active Plan step or directly blocks it

If the issue is outside the active step and not blocking it, do not drift into it.

### 2. Reproduce first

Do not guess.

Get a concrete reproduction:
- exact failing command
- exact failing screen/flow
- exact failing input/state
- exact error message
- exact mismatch between expected and actual behavior

Capture:
- command output
- stack trace
- failing test name
- file paths involved
- screenshots if useful

If you cannot reproduce the problem, say so clearly and do not pretend you diagnosed it.

### 3. Define expected behavior from the docs

Before editing code, state:
- what should happen according to PRD
- what layer should own that behavior according to Architecture
- what constraints Guardrails impose
- what the active Plan step actually requires

This prevents debugging toward the wrong outcome.

### 4. Localize the fault

Identify which layer is actually failing:

Possible layers:
- content/config/data
- UI rendering
- UI state wiring
- store/state management
- sim-core/domain logic
- permissions/boundary enforcement
- persistence/save-load
- test/setup/tooling

Find the narrowest failing boundary.

Do not patch UI if the real bug is in domain logic.
Do not patch domain logic if the real bug is bad state wiring.
Do not blame data until code paths are checked.

### 5. Gather evidence before changing code

Use the least invasive methods first:
- read relevant files
- inspect recent commits
- inspect tests
- add temporary logs only if needed
- run targeted tests
- run the exact failing command
- compare expected vs actual values

Prefer direct evidence over intuition.

### 6. Form a concrete hypothesis

State one specific hypothesis at a time.

Good examples:
- `board expectation logic is using league position only and ignoring club identity`
- `the component is rendering stale derived state because the selector is wrong`
- `the live 2D screen is not using the same match result object as instant sim`
- `the transfer acceptance path skips project-fit weighting when a wage match exists`

Bad examples:
- `something is wrong with state`
- `the UI is buggy`
- `maybe the engine is broken`

### 7. Test the hypothesis narrowly

Before broad edits, confirm the suspected cause with:
- a targeted test
- a log
- a narrowed repro
- a selector/value check
- a one-file inspection proving the wrong data path

If the hypothesis fails, discard it and try the next one.
Do not stack random fixes.

### 8. Apply the smallest correct fix

When fixing:
- preserve Architecture boundaries
- do not invent new systems
- do not add scope
- do not leave debug-only code behind unless explicitly useful
- do not replace a domain bug with a UI-only workaround
- do not replace a structural issue with hardcoded values

Prefer:
- minimal targeted fixes
- regression tests for the real cause
- cleanup only if required for correctness

### 9. Verify hard

After the fix, run the strongest relevant checks:
- failing test again
- related unit/integration tests
- typecheck
- lint
- build
- targeted manual validation if needed

If the issue was architectural or cross-layer, verify adjacent behavior too.

### 10. Record the result

Update `AUTONOMOUS_PROGRESS.md` with:
- the bug/debug target
- root cause
- files changed
- verification run
- remaining risk if any

If the bug exposed a blocker or spec conflict, document that clearly.

## Debugging decision tree

Use this order:

1. Can I reproduce it?
2. What should happen according to the docs?
3. Which layer owns that behavior?
4. Where is the first observable mismatch?
5. What is the smallest plausible cause?
6. How do I prove that cause before editing?
7. What is the smallest safe fix?
8. How do I prove it is fixed?

If you cannot answer one of these, stop and gather evidence first.

## Output format

Before debugging, briefly state:
- active Plan step
- bug being debugged
- repro method
- expected behavior
- suspected layer
- verification to run

After debugging, briefly state:
- root cause
- files changed
- fix applied
- verification passed
- regression coverage added or updated
- next valid task

## Hard rules

- Do not debug by random editing
- Do not claim a root cause without evidence
- Do not fix symptoms in the wrong layer
- Do not add scope while debugging
- Do not leave temporary hacks in core flow
- Do not skip regression protection when the issue is real and reproducible
- Do not close a bug just because one screen looks better
- Do not bypass architecture boundaries to get a fast green check
- Do not mark it fixed without rerunning the failing path

## When to stop

Stop and report if:
- the issue cannot be reproduced
- the source-of-truth docs conflict on expected behavior
- the bug requires a spec or architecture change first
- the environment is too broken to verify safely
- the fix would require unrelated scope expansion