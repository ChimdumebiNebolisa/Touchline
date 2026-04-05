# frontend-implementation

## Purpose

Use this skill when implementing frontend screens, components, flows, or UI wiring that are already required by the active Plan step.

This skill exists to make frontend work:
- spec-driven
- architecture-safe
- state-complete
- consistent
- verifiable

The goal is not just to render something. The goal is to build UI that correctly reflects the product spec and real underlying state.

## Inputs

You need:
- the repository
- `AGENTS.md`
- `docs/PRD.md`
- `docs/Architecture.md`
- `docs/Guardrails.md`
- `docs/Plan.md`
- `AUTONOMOUS_PROGRESS.md` if present
- the relevant existing frontend files
- the relevant state/domain modules

## When to use

Use this skill when:
- building a new screen required by the current Plan step
- wiring a screen to real state
- adding a component needed by a valid scoped feature
- adding loading/error/empty states
- connecting controls to existing app logic
- improving a screen so it matches the source-of-truth docs

Do not use this skill for speculative UI work.

## Required procedure

### 1. Re-read the governing docs

Before writing code:
- read `AGENTS.md`
- read `docs/PRD.md`
- read `docs/Architecture.md`
- read `docs/Guardrails.md`
- read `docs/Plan.md`
- read `AUTONOMOUS_PROGRESS.md` if present

Identify:
- active Plan step
- exact frontend task allowed
- relevant acceptance constraints
- relevant Architecture boundaries

### 2. Define the UI contract before coding

For the target screen/component, explicitly identify:
- purpose
- user-visible inputs
- user-visible outputs
- primary action
- secondary actions
- required states
- real data source
- domain actions it triggers
- files/modules that should own the logic

Required states to consider where relevant:
- loading
- empty
- error
- success
- disabled
- selected/active
- processing/submitting

Do not code before this is clear.

### 3. Map the UI to real ownership boundaries

Before implementing, decide:
- what belongs in UI
- what belongs in shared state/store
- what belongs in sim-core/domain logic
- what must not cross layers

Rules:
- UI renders state and sends intent
- domain logic decides business behavior
- UI must not invent match, transfer, morale, perception, or role-permission rules
- if a control affects domain behavior, the actual rule must live outside the component

### 4. Implement from structure first

Build in this order:
1. screen/frame structure
2. information hierarchy
3. required states
4. event handlers and wiring
5. refinement and cleanup

Prefer:
- small components
- predictable props
- reuse of existing shared components
- explicit state rendering
- minimal conditional chaos

Avoid:
- giant all-in-one components
- duplicated layout patterns
- UI-only fake state for core flows
- hidden side effects
- business logic embedded in JSX/templates

### 5. Wire only to real state

When connecting controls:
- use real stores/actions/selectors/hooks already approved by the architecture
- if missing, add the minimum necessary state plumbing in the correct layer
- never fake completion by hardcoding behavior in the component
- never bypass permissions, constraints, or shared engine behavior

### 6. Cover states explicitly

For any screen or flow, handle the relevant states in code.
Do not assume the happy path is enough.

At minimum, check whether the screen needs:
- initial loading
- no data / empty state
- invalid state
- error state
- in-progress state
- result state

If a state matters and is not implemented, note it and either implement it or document why it is out of scope for the current subtask.

### 7. Verify immediately

Run the strongest relevant checks, such as:
- lint
- typecheck
- build
- component tests if they exist
- integration tests if they exist
- targeted manual validation

Also verify:
- no business logic leaked into UI
- the screen reflects the actual data/state lifecycle
- the screen matches the active Plan step and does not sneak in scope

## Implementation checklist

Before finishing, confirm:
- the task is in the active Plan step
- the screen/component has clear hierarchy
- states are covered
- events are wired to real logic
- architecture boundaries are preserved
- no placeholder logic remains in the core path
- verification passed

## Output format

Before coding, briefly state:
- active Plan step
- screen/component to implement
- real data/state it depends on
- states you will cover
- verification to run

After coding, briefly state:
- files changed
- what was implemented
- what states are covered
- what logic was wired
- verification passed
- next smallest valid frontend task

## Hard rules

- Do not invent scope
- Do not work on a later Plan step
- Do not treat visual completion as behavioral completion
- Do not put core business rules into frontend files
- Do not hardcode fake success paths for core behavior
- Do not skip empty/loading/error states where they matter
- Do not duplicate the same UI logic across multiple screens if a shared component fits
- Do not add design-system churn unless required for the current task
- Do not bypass the one-engine rule through UI-specific shortcuts

## Decision filter

Before making a frontend change, ask:
1. Is this required by the active Plan step?
2. Is the ownership boundary clear?
3. Am I wiring to real state and real actions?
4. Are required states covered?
5. Can I verify this now?
6. Is this the smallest useful implementation step?

If any answer is no, stop.

## When to stop

Stop and report if:
- the frontend task depends on missing domain/state work not yet implemented
- the needed change would violate Architecture boundaries
- the UI requirement is not actually supported by the four docs
- the screen cannot be completed honestly without inventing placeholder logic