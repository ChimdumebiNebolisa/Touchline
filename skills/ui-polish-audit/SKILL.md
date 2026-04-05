# ui-polish-audit

## Purpose

Use this skill when working on frontend screens, components, layout, or visual refinements.

This skill exists to improve UI quality without causing scope creep. It focuses on:
- hierarchy
- spacing
- alignment
- state clarity
- responsiveness
- accessibility basics
- consistency with the repo's existing product and architecture rules

This is not a license to redesign the product. It is an audit-and-fix workflow for bounded UI improvements.

## Inputs

You need:
- the repository
- the current screen/component code
- the source-of-truth docs:
  - `docs/PRD.md`
  - `docs/Architecture.md`
  - `docs/Guardrails.md`
  - `docs/Plan.md`
- `AGENTS.md`
- any existing design tokens, shared components, layout primitives, or style conventions in the repo

## When to use

Use this skill when:
- a screen exists but looks rough
- layout feels cluttered or weak
- UI states are missing or unclear
- responsiveness is weak
- interactions are visually inconsistent
- a frontend task is already within the active Plan step and needs polish

Do not use this skill to invent new features or flows.

## Required procedure

### 1. Re-read the constraints

Before touching UI:
- read `AGENTS.md`
- read the four docs
- confirm the current active Plan step
- confirm the screen or component is part of valid scoped work

If the requested polish would add product scope, stop.

### 2. Inspect the UI in context

Review the relevant files and identify:
- screen purpose
- primary action
- secondary action
- information hierarchy
- repeated layout patterns
- existing component system
- error/loading/empty states
- responsive behavior
- accessibility issues that are obvious from code

### 3. Audit using this checklist

#### Hierarchy
Check:
- Is the most important action visually obvious?
- Is the page title/subtitle structure clear?
- Are primary and secondary actions visually distinct?
- Is information grouped logically?

#### Spacing
Check:
- Is spacing consistent between sections?
- Are paddings and gaps using repeatable values?
- Is the screen too dense or too loose?
- Are cards, panels, and form groups visually separated correctly?

#### Alignment
Check:
- Are edges lined up?
- Are labels, fields, and controls aligned consistently?
- Are actions placed where users expect them?

#### State clarity
Check:
- loading state
- empty state
- error state
- disabled state
- selected/active state
- hover/focus state if relevant

If a state matters and is missing, note it.

#### Readability
Check:
- text size hierarchy
- contrast issues visible in code/styling
- line length
- cramped tables/forms/lists
- excessive visual noise

#### Responsiveness
Check:
- narrow viewport stacking
- overflow risk
- clipping/truncation risk
- action placement on smaller screens
- tables/lists/forms collapsing poorly

#### Interaction quality
Check:
- button priority
- click target size
- form affordance clarity
- destructive actions being too prominent or too hidden
- repeated actions competing visually

### 4. Propose only bounded fixes

Prefer fixes like:
- spacing cleanup
- hierarchy cleanup
- button priority cleanup
- layout cleanup
- consistent sectioning
- missing state components
- responsive fixes
- accessibility-safe semantic improvements
- reuse of existing shared components

Avoid:
- redesigning workflows
- creating net-new product features
- rewriting the design system
- introducing large animation work
- changing domain behavior from the UI layer

### 5. Implement carefully

When editing:
- preserve Architecture boundaries
- do not move business logic into UI
- do not add fake UI state disconnected from real app state
- keep the change small enough to verify
- reuse existing components/tokens where possible
- prefer consistency over novelty

### 6. Verify

Run the strongest relevant checks, such as:
- lint
- typecheck
- build
- targeted manual validation
- UI tests if present

Also verify:
- the UI still reflects real underlying app state
- no screen-only business rules were introduced
- no required state became inaccessible on smaller layouts

## Output format

Before editing, briefly state:
- audited screen/component
- issues found
- bounded fixes planned
- verification to run

After editing, briefly state:
- files changed
- improvements made
- states covered
- verification passed
- remaining UI issues, if any

## Hard rules

- Do not invent new product flows
- Do not add optional scope
- Do not use polish as an excuse to rewrite unrelated code
- Do not place domain rules in UI components
- Do not add styling inconsistency when shared patterns already exist
- Do not call the UI “done” if important states are missing
- Do not make the screen prettier at the cost of clarity
- Do not break accessibility basics for visual style

## Decision filter

Before making a UI change, ask:
1. Is this UI work inside the active Plan step?
2. Does it improve clarity, consistency, responsiveness, or state coverage?
3. Can I do it without adding scope?
4. Can I verify it right now?
5. Am I preserving Architecture boundaries?

If any answer is no, stop.

## When to stop

Stop and report if:
- the needed fix requires product-scope changes
- the needed fix requires Architecture changes first
- the screen depends on missing real state/data
- the existing component system is too inconsistent to polish safely without wider design decisions