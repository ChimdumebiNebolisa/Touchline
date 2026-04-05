# spec-driven-implementation

## Purpose

Use this skill whenever implementing or editing code in this repo.

This skill forces work to stay inside:
- PRD
- Architecture
- Guardrails
- Plan

It exists to stop drift, premature scope expansion, fake progress, and disconnected code changes.

## Inputs

You need:
- the repository
- `docs/PRD.md`
- `docs/Architecture.md`
- `docs/Guardrails.md`
- `docs/Plan.md`
- `AUTONOMOUS_PROGRESS.md` if present
- current codebase state
- current git state

## Required procedure

### 1. Re-read the governing docs

Before changing code, re-read:
- PRD
- Architecture
- Guardrails
- Plan

Use them as binding instructions.

### 2. Determine the active step

From the Plan and progress state, identify:
- the current active step
- what counts as done for that step
- what remains unverified
- the smallest valid next subtask

### 3. Check task validity

Before coding, explicitly verify:
- the task belongs to the active Plan step
- the task is required by the PRD
- the task respects Architecture boundaries
- the task does not violate Guardrails
- the task is small enough to verify cleanly

If any answer is no, do not do the task.

### 4. Implement narrowly

Make only the minimum change needed for the chosen subtask.

Prefer:
- narrow changes
- real working logic
- sim-core/domain changes before UI glue when business behavior is involved
- explicit tests where relevant

Avoid:
- unrelated refactors
- cleanup for its own sake
- optional abstractions
- speculative features
- “while I’m here” edits

### 5. Verify immediately

Run the strongest relevant checks for the changed area.

Possible checks:
- unit tests
- integration tests
- typecheck
- lint
- build
- targeted manual validation
- calibration/regression checks if required

If verification fails, fix or revert.

### 6. Update progress state

Update `AUTONOMOUS_PROGRESS.md` with:
- active Plan step
- completed verified task
- current next subtask
- blocker if any
- verification run summary

## Decision filter

Use this filter before every change:

1. Is this in the active Plan step?
2. Is this required by the PRD?
3. Does this preserve Architecture boundaries?
4. Does this obey Guardrails?
5. Can I verify it now?
6. Is it the smallest useful increment?

If any answer is no, stop.

## Hard rules

- Never work from vague “improve this” instincts
- Never implement features not clearly supported by the four docs
- Never move to a later Plan step early
- Never leave placeholder logic in core flow
- Never duplicate business rules across layers
- Never bypass the one-engine rule
- Never give manager code owner-level powers
- Never treat UI completion as feature completion

## Output format

Before coding, briefly state:
- Active Plan step
- Chosen subtask
- Why it is valid
- Verification to run

After coding, briefly state:
- Files changed
- What behavior was added/fixed
- Verification passed
- Remaining next subtask

## When to stop

Stop and report if:
- docs conflict
- a boundary change is required first
- verification cannot be run
- the next change would require scope expansion
- the active step cannot be completed without a human decision