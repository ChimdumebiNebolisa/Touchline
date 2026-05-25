# Guardrails

## 1. Source Of Truth Guardrails

1. `touchline_master_design_decisions.md` is the highest-level product design source of truth.
2. `docs/PRD.md`, `docs/Architecture.md`, `docs/Guardrails.md`, and `docs/Plan.md` must not contradict the master design.
3. If repo docs conflict with each other or with the master design, stop and reconcile the docs before changing product code.
4. Only one Plan stage or subtask should be active at a time.
5. Future implementation should prefer coherent playable slices over shallow stubs across every system.

## 2. Product Direction Guardrails

1. Touchline is a fictional club-football career simulator.
2. The game is not an arcade soccer game.
3. The game is not a generic dashboard app.
4. Godot plus C# remains the active product path unless the source-of-truth docs change.
5. The product should remain local-first and single-player unless the architecture changes.
6. Do not imply real licensed clubs, real licensed players, online services, or playable on-pitch football controls.

## 3. Football Authenticity Guardrails

1. Drama must come from football logic, not random chaos.
2. Board, fan, Director of Football, staff, player, agent, and media reactions must be explainable after the fact.
3. Board approval and fan approval may disagree and should not be collapsed into one approval score.
4. Transfers may not resolve on fee alone.
5. Board expectations may not resolve on league position alone.
6. Media and perception choices must have persistent downstream effects when implemented.
7. Youth quality must remain rare and meaningful when the academy system exists.
8. Hidden information is allowed, but outcomes must still be explainable after the fact.

## 4. Role And Authority Guardrails

1. The playable roles are Assistant Manager, Head Coach, and Manager.
2. Role authority must affect what the user can control.
3. Assistant Manager gameplay is suggestion and influence led unless responsibilities are explicitly delegated.
4. Head Coach controls the football side but has limited recruitment and structural authority.
5. Manager has broad football-project control within board and ownership limits.
6. More authority means more accountability.
7. Sporting Director or Director of Football authority must remain distinct from board authority and user authority.

## 5. Information And Player Identity Guardrails

1. Player identity must not collapse into ratings only.
2. Players need identity, context, playing style, tendencies, traits, personality, tactical fit, development, morale, form, fitness, and relationship state as the relevant stages are implemented.
3. Partial information must remain part of the design.
4. The UI should mix exact values, estimated ranges, unknown question marks, and scouting language.
5. The user should never fully know hidden personality numbers, exact future potential certainty, exact rival intentions, exact player future behavior, or exact board internal politics.
6. No visible placeholder player names such as `Player 12` in player-facing flows.

## 6. Match And Tactics Guardrails

1. One shared match engine must serve Instant Sim and Live Match Playback.
2. Live Match Playback must visualize the same simulated match timeline used by Instant Sim.
3. Live Match Playback is a renderer/controller of simulation playback, not a second rules engine.
4. Do not build a pure physics game.
5. Match simulation should be stat-and-event driven, with believable tactical visual playback.
6. Tactics must stay connected to match outcomes through domain logic, not UI-only labels.
7. Tactical familiarity, player fit, morale, form, fitness, staff preparation, opponent style, and match momentum should affect the match as the relevant systems are implemented.

## 7. Morale, Trust, Reputation, And Pressure Guardrails

1. Morale, trust, reputation, and pressure must remain separate systems.
2. Morale is how people feel right now.
3. Trust is how much they believe in the user.
4. Reputation is how the football world sees the user.
5. Pressure is how close the situation is to consequences.
6. Trust changes slower than morale.
7. Morale may tilt outcomes but must not dominate long-term quality, tactics, and decisions.

## 8. Architecture Guardrails

1. Scene flow lives in Godot scenes.
2. Simulation and business rules live in C# domain systems.
3. Scene scripts may request actions but may not define core simulation, transfer, scouting, permission, calendar, or consequence rules.
4. Autoload singletons own persistent runtime state and scene handoff.
5. Save/load must persist complete career-critical state for every implemented system.
6. No silent failures: invalid state, failed loads, and unsupported actions must be surfaced clearly.
7. Deterministic logic comes before fuzzy heuristics in validation, permissions, routing, and critical consequences.

## 9. Scope And Delivery Guardrails

1. Do not add features outside the active Plan stage.
2. Do not create UI that claims a system is functional before it changes authoritative state.
3. Foundation-only systems must be labeled honestly in UI, docs, tests, and progress notes.
4. Do not overbuild deep versions of later systems while an earlier stage is still incomplete.
5. If a feature cannot name at least two downstream systems it affects, challenge whether it belongs in the current slice.
6. Keep finance readable; do not turn it into an accounting simulator.
7. Keep media drama believable; avoid constant scandals or random betrayal events.

## 10. Verification Guardrails

1. No meaningful commit without verification evidence.
2. For documentation changes, verify PRD, Architecture, Guardrails, and Plan consistency.
3. For code changes, run the strongest available checks for the changed area.
4. If checks fail, fix and rerun or revert and document.
5. Never claim behavior works without concrete command output or validation artifact.

## 11. Hard Stop Conditions

Stop and document a blocker if any occur:

1. source-of-truth docs conflict
2. the next valid step requires changing scope or architecture first
3. the environment cannot support Godot .NET work needed for the active stage
4. save/load or persistent-career assumptions would be violated
5. push/auth/remote failure blocks the required workflow
