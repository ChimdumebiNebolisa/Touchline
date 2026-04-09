# Guardrails

## 1. Product Direction Guardrails

1. Godot plus C# is the only active product path.
2. Do not drift back to a web dashboard as the main game shell.
3. Do not keep sandbox-only one-off simulation behavior as the core loop.
4. Source-of-truth docs drive implementation order; update docs before changing direction.
5. Only one active Plan step at a time.

## 2. Football Authenticity Guardrails

1. The game must feel football-native, not internal tooling.
2. Use named player identities in player-facing flows.
3. Forbid placeholder visible names such as Player 12.
4. Club Dashboard is the central hub for day-to-day career flow.
5. Live match view must show visible player movement, score, clock, and key events.

## 3. Architecture Guardrails

1. Scene flow lives in Godot scenes; simulation logic lives in C# domain systems.
2. Autoload singletons own persistent runtime state.
3. Scene scripts may request actions but may not define core simulation rules.
4. Save and load must persist complete career-critical state.
5. No backend-first detour unless a hard blocker is documented and approved.

## 4. Persistence and Continuity Guardrails

1. Core progress assumptions must include persistent career state.
2. Any feature that mutates career state must be save-compatible.
3. Date advancement must affect fixtures, standings, player state, and season timeline.
4. Post-match outcomes must carry forward into subsequent screens and weeks.

## 5. Scope Guardrails

1. No deep scouting tree in current scope.
2. No deep finance ledger in current scope.
3. No playable youth leagues in current scope.
4. No fake completion by shipping shell-only scenes without working state transitions.
5. Build only the smallest valid subtask for the active Plan step.

## 6. Verification Guardrails

1. No meaningful commit without verification evidence.
2. For doc resets, verify PRD, Architecture, Guardrails, and Plan consistency.
3. For code changes, run strongest available checks for the changed area.
4. If checks fail, fix and rerun or revert and document.
5. Do not claim behavior works without concrete command output or validation artifact.

## 7. Legacy Isolation Guardrails

1. Old web or sandbox shell must not remain the active entry path.
2. Reusable old material may be preserved under legacy.
3. Keep history intact while making new game path explicit.
4. Avoid deleting useful reference content when clean isolation can preserve it.

## 8. Hard Stop Conditions

Stop and document a blocker if any occur:

1. environment cannot support Godot .NET work
2. docs conflict in a way that changes scope boundaries
3. push or auth failure blocks required workflow
4. next change requires inventing scope beyond active Plan step
5. save/load or persistent-world assumptions would be violated by the current step
