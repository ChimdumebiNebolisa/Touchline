# Touchline agent rules

These instructions apply to the entire repository unless a deeper AGENTS.md overrides them.

## Source of truth

The only product source of truth files are:

- `docs/PRD.md`
- `docs/Architecture.md`
- `docs/Guardrails.md`
- `docs/Plan.md`

Read all four before making any code changes.

If they conflict, stop and report the conflict. Do not guess.
If a requested change is not supported by those docs, do not implement it.

## Operating model

This repo is spec-driven.

Work in this order:
1. Read `docs/PRD.md`
2. Read `docs/Architecture.md`
3. Read `docs/Guardrails.md`
4. Read `docs/Plan.md`
5. Read `AUTONOMOUS_PROGRESS.md` if it exists
6. Inspect current git status, recent commits, and the relevant code
7. Identify the active Plan step
8. Implement only the smallest valid subtask for that active step
9. Verify it
10. Commit it
11. Push it
12. Update `AUTONOMOUS_PROGRESS.md`
13. Repeat

Do not skip verification.
Do not skip commit and push after a meaningful verified change.

## Scope control

Do not:
- invent features
- add optional systems early
- change product scope
- add backend/services unless required by the Architecture
- fork the match engine
- collapse manager, sporting director, board, and owner permissions into one role
- add deep staff sim, scouting micro, finance ledger depth, playable youth leagues, or dialogue-heavy media systems unless the PRD changes

If a feature cannot name at least two downstream systems it affects, it probably does not belong.

## Architecture rules

Follow the Architecture exactly.

Hard requirements:
- one shared match engine for instant sim and live 2D mode
- sim rules live in sim-core/domain logic, not UI
- sporting director and board remain separate constraint actors
- deep simulation only for the top two divisions of shipped countries
- other leagues remain shadow simulations
- local-first unless the Architecture changes

Do not move business logic into screens/components.
Do not create duplicate logic in multiple layers.

## Guardrail rules

Enforce these always:
- no placeholder logic in core flow
- no silent failures
- no hidden assumptions
- deterministic logic before fuzzy heuristics in validation, permissions, and routing
- transfers may not resolve on fee alone
- board expectations may not resolve on league position alone
- media/perception choices must have persistent downstream effects
- youth quality must remain rare and meaningful
- hidden information is allowed, but outcomes must still be explainable after the fact

## Verification rules

Before every meaningful commit, run the strongest relevant checks available.

At minimum, use whatever exists in the repo for:
- tests
- typecheck
- lint
- build

If you change core logic, add or update tests.
If checks fail, fix them before committing or revert the change.

Never say something works without evidence.

## Git rules

If you modify files:
- use git
- keep commits scoped and clean
- do not create a new branch unless explicitly asked
- do not amend old commits
- do not leave the worktree dirty when you stop

After each meaningful verified change:
1. stage only intended files
2. commit with a clear message
3. push to GitHub immediately

Commit format:
- `step-N: <actual bounded change>`

Examples:
- `step-1: wire shared match engine into instant and live flow`
- `step-1: persist post-match morale board and fan updates`

If push fails:
- record the exact failure in `AUTONOMOUS_PROGRESS.md`
- do not pretend it succeeded
- stop if remote/auth prevents continued push-based workflow

## Progress tracking

Maintain a repo-root file named `AUTONOMOUS_PROGRESS.md`.

Keep it updated with:
- current active Plan step
- last completed verified task
- current subtask in progress
- next 1 to 3 queued subtasks
- known blockers
- last verification run
- last commit hash
- resume instructions

Update this file whenever a meaningful unit of work finishes.

## When blocked

A real blocker is one of:
- source-of-truth docs conflict
- the next valid step requires changing scope/boundaries first
- the environment is missing something required to continue
- push/auth/remote failure blocks the required workflow

When blocked:
1. stop coding further product changes
2. write the blocker clearly in `AUTONOMOUS_PROGRESS.md`
3. include exact failing command/output if relevant
4. include the smallest decision needed from a human
5. commit and push the blocker note if possible
6. stop

Do not work around a blocker by inventing scope.

## Output style while working

Be brief and operational.

Before a work cycle, state:
- active Plan step
- subtask
- verification to run

After a work cycle, state:
- what changed
- what verification passed
- commit hash
- push result
- next subtask

## Cursor Cloud specific instructions

Touchline is a **local Godot 4.6 Mono + C#** desktop game (`game/`). There is no backend or Docker stack. TypeScript workspaces (`packages/sim-core`, `packages/save`) support Vitest and manual step scripts; they are **not** the active product path.

### One-time VM tools (not in the update script)

Install once per Cloud Agent VM (paths below match a typical Linux setup):

- **.NET 8 SDK** — `curl -fsSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 8.0 --install-dir "$HOME/.dotnet"` then `export PATH="$HOME/.dotnet:$PATH"`.
- **Godot 4.6.2 Mono (Linux x86_64)** — download [Godot_v4.6.2-stable_mono_linux_x86_64.zip](https://github.com/godotengine/godot/releases/download/4.6.2-stable/Godot_v4.6.2-stable_mono_linux_x86_64.zip), unzip under `$HOME/godot/`. On Linux there is a single binary (no separate `_console` exe); use it for both GUI and `--headless`.

Set for every shell session (already in `~/.bashrc` on provisioned VMs):

```bash
export PATH="$HOME/.dotnet:$PATH"
export GODOT_CONSOLE="$HOME/godot/Godot_v4.6.2-stable_mono_linux_x86_64/Godot_v4.6.2-stable_mono_linux.x86_64"
export GODOT="$GODOT_CONSOLE"
```

### Services

| Component | Required? | How to run |
|-----------|-----------|------------|
| Godot app (`game/`) | Yes (product) | GUI: `"$GODOT" --path game` |
| .NET build | Yes (gate) | `dotnet build game/Touchline.sln` from repo root |
| npm workspaces | Optional for product E2E | See root `package.json` scripts |

### Commands (repo root)

| Goal | Command |
|------|---------|
| TS lint / typecheck / build / test | `npm run lint`, `npm run typecheck`, `npm run build`, `npm test` |
| C# build | `dotnet build game/Touchline.sln` |
| Godot + C# warm build | `"$GODOT" --headless --path game --build-solutions --quit` |
| Product E2E (headless) | `"$GODOT" --headless --path game -s res://scripts/step50_end_to_end_user_flow_check.gd` |
| Full regression (headless) | `"$GODOT" --headless --path game -s res://scripts/step57_final_regression_check.gd` |

More headless checks: `docs/QA.md`. Legacy Vite client is under `legacy/web-prototype/` (archived; not required for the Godot product).

### Gotchas

- README examples use Windows `.exe` names; on Linux use `$GODOT` as above.
- Headless scripts exit with `STEP*_PASS` lines on success; non-zero exit means failure.
- GUI playtests need `DISPLAY` (Desktop pane / X11). Python GUI harness in `docs/audit/active-playtest/` is Windows-oriented; prefer Godot headless checks on Linux.