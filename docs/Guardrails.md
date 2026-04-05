# Guardrails

## 1. Enforceable rules

1. No scope additions without a PRD update. 
2. No boundary or ownership changes without an Architecture update.
3. No unrelated refactors.
4. No placeholder logic in the core flow. If the path is core, it must run with real logic.
5. No duplicate business logic across match, transfers, morale, perception, academy, or role permissions. One rule lives in one place. 
6. No silent failures. Every failed action must return a surfaced reason to the player or developer.
7. No hidden assumptions. If a rule is required for the feature to work, it must exist in the spec, config, or explicit code path.
8. Deterministic logic comes before any AI-style fuzziness in validation, routing, parsing, role boundaries, and policy checks.
9. Core paths must handle success, failure, and processing states where relevant.
10. Do not build a step unless PRD, Architecture, and Guardrails already constrain it. 
11. One underlying match engine only. Instant sim and live 2D mode may not fork logic.
12. Every major system must affect at least two other systems. If it cannot name two downstream effects, it does not belong.
13. Every player-facing outcome with hidden information must still expose a reason summary after the fact. Hidden numbers are fine. Hidden causality is not.
14. Transfers may not resolve on fee alone. Acceptance and rejection must consider role, project fit, reputation, pathway, squad competition, and budget structure.
15. Board expectations may not resolve on league position alone. Club identity, finances, preseason objective, style, derbies, and over- or under-performance context must matter.
16. Manager permissions may not include owner powers. The manager cannot directly set the club budget, overrule the board, or unilaterally force blocked transfers through.
17. Youth intake calibration must keep elite prospects rare at club level across long-run simulations.
18. Reputation must influence at least board patience, player trust, and transfer pull. If reputation does not move those systems, it is fake depth. 
19. Media choices must have persistent downstream effects, not one-screen modifiers. They must hit at least two of morale, trust, fan sentiment, board confidence, transfer willingness, or sack risk.
20. Any tactical change must route through the shared match engine, not a UI-only modifier. If the player changes press, tempo, width, or risk, the engine must produce the effect.
21. Deep simulation remains limited to the top two divisions of shipped countries. Other leagues stay shadow-simmed unless the PRD changes. 
22. No backend-first or service-based detour unless a real hard blocker exists and the Architecture is updated first.
23. No FM-style scope creep disguised as polish. Deep staff trees, scouting micro, finance ledgers, playable youth leagues, and dialogue-heavy media systems stay out unless the PRD changes.
24. Narrow, deep match control is mandatory. Adding more tactical knobs is forbidden unless each new control has a visible effect in match output and post-match explanation.

## 2. Evidence rules

Any claim that something works is unverified unless backed by at least one concrete artifact from this list:

* unit test result
* integration test result
* manual UI check result
* lint output
* typecheck output
* sample input/output artifact
* benchmark or calibration result
* screenshot, log, or CLI output

Additional evidence rules:

* A feature is not done because the screen exists. It is done only when the underlying sim-core behavior is verified.
* A subsystem is not “realistic” because it feels plausible. It must show repeatable behavior under tests, logs, or calibration runs.
* A hidden-information system is not acceptable unless its outputs can be explained after the fact with a surfaced reason summary.
* Any claim of balance, rarity, or realism must be backed by batch results, not one anecdotal run.

## 3. Required verification by subsystem

### Match engine

Required proof:

* unit tests for chance generation, cards, injuries, fatigue, and substitutions
* calibration batch showing instant and live distributions align
* sample match logs explaining why a stronger team still lost
* proof that tactical changes change chance quality, fatigue, or structural risk through the engine, not UI-only state

### Perception

Required proof:

* sample chain showing result -> media/fan/board change -> morale or transfer consequence
* unit tests for patience buffer updates
* unit tests for negativity loop updates
* proof that board judgment is not table-only

### Morale, trust, promises, and discipline

Required proof:

* sample chain showing public comment, broken promise, or discipline action affecting at least two downstream systems
* unit tests for promise timers and trust changes
* sample unrest case showing effect on performance, transfer stance, or dressing-room stability

### Transfers

Required proof:

* sample offer breakdown showing fee is insufficient without role, project, reputation, and pathway fit
* tests for manager veto, board block, and wage-structure rejection
* tests proving equal-fee offers can resolve differently
* tests proving broken promises reduce future willingness or trust

### Academy and loans

Required proof:

* batch sim showing rare high-end youth outcomes
* sample path showing intake -> loan -> first-team readiness
* long-run calibration showing academy quality and pathway bias change outcomes over time
* proof that youth use interacts with squad congestion, board pressure, or finances

### Role boundaries

Required proof:

* permission tests proving manager cannot directly set board budget
* permission tests proving manager cannot unilaterally sign blocked players
* tests showing sporting director and board remain separate actors
* tests showing UI commands cannot mutate protected state directly

### World and shadow leagues

Required proof:

* integration tests showing top-two-deep / rest-shadow contract holds
* sample outputs showing shadow leagues create transfer targets, loan destinations, and promotion-relegation continuity
* proof that adding another country pack is content work, not a sim-core rewrite

### Finances

Required proof:

* tests showing bank balance, wage budget, transfer budget, and board spending posture constrain recruiting
* proof that finances affect at least two of transfer power, youth reliance, expectations, or board strictness
* proof that there is no deep ledger dependency in core flows

### Save/load and persistence

Required proof:

* serialization tests
* deterministic reload check around a major event
* proof that morale, promises, reputation, and board pressure survive save/load without drift 

## 4. What this game must not become

* Not an omnipotent club spreadsheet
* Not an owner fantasy
* Not a full transfer-market tycoon game
* Not a deep accounting sim
* Not a dialogue-tree media RPG
* Not a wonderkid slot machine
* Not a system where random variance feels causeless
* Not a set of disconnected feature tabs
* Not a clone of Football Manager’s scope with less depth
* Not a world where head coach, sporting director, and board all behave like one person 

## 5. Hard stop conditions

Stop work and update artifacts first if any of these happen:

* a new feature cannot name two downstream systems it changes
* a coding step requires a role boundary not defined in Architecture
* a core flow uses stubbed logic
* instant and live sim start diverging
* transfer logic becomes fee-first
* youth output becomes spammy in calibration
* board evaluation ignores context
* a UI screen invents rules not present in sim-core
* a new country requires architecture changes instead of content additions
* a feature requires backend/services even though no hard blocker exists
* a tactical control changes UI state but does not change match-engine output
* hidden-information outcomes cannot produce a reason summary after the fact
* a completed step has no concrete verification artifact
* a proposed “polish” feature materially expands scope without a PRD update
