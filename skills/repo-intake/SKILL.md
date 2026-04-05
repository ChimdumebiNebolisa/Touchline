# repo-intake

## Purpose

Use this skill at the start of work in an unfamiliar repo or when resuming autonomous work after a pause.

The goal is to build a correct mental model of:
- repo structure
- entry points
- test/build/lint/typecheck commands
- source-of-truth docs
- current git state
- progress state
- active implementation step

Do not write code before completing intake.

## Inputs

You need:
- the current repository
- terminal access
- read access to the codebase
- git access
- the source-of-truth docs if present
- `AUTONOMOUS_PROGRESS.md` if present

## Required procedure

### 1. Locate source-of-truth docs

Search for:
- `docs/PRD.md`
- `docs/Architecture.md`
- `docs/Guardrails.md`
- `docs/Plan.md`

If exact names do not exist, locate the closest intended equivalents.
Read all four before touching code.

### 2. Inspect repo shape

Identify:
- package manager
- workspace/monorepo structure
- app entry points
- domain/sim-core modules
- UI modules
- test setup
- lint/typecheck/build commands
- scripts in package files
- config files that matter

### 3. Inspect git state

Check:
- current branch
- `git status`
- recent commits
- remotes
- whether push is likely available

### 4. Inspect progress state

Read `AUTONOMOUS_PROGRESS.md` if it exists.

Determine:
- current active Plan step
- last completed verified task
- current unfinished subtask
- known blockers
- likely next smallest valid task

If the file does not exist, note that and be prepared to create it when work begins.

### 5. Map the architecture onto the codebase

Figure out:
- where match logic lives
- where club/perception/transfer/academy logic lives
- whether UI is incorrectly holding business logic
- whether current code already violates the source docs

### 6. Produce a short intake summary

Summarize:
- source-of-truth files found
- current active Plan step
- relevant code locations
- verification commands
- git/push readiness
- smallest next valid task

## Output format

Return a short structured summary with these headings:

- Source docs found
- Repo shape
- Verification commands
- Git state
- Progress state
- Active Plan step
- Next smallest valid task
- Risks or blockers

## Rules

- Do not code during intake
- Do not speculate about missing architecture if you have not inspected the relevant files
- Do not choose a task from a later Plan step if the current step is unverified
- Prefer exact commands and exact file paths over vague descriptions
- If the source docs are missing or conflicting, stop and report it