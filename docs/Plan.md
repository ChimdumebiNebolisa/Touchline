# Plan

## 1. Status

### Active

- Stage 2: Player and squad identity

### Done

- Stage 0: Documentation reconciliation
- Stage 1: Career foundation

### Blocked

- None.

## 2. Plan Rules

1. `docs/touchline_master_design_decisions.md` is the highest-level product design source of truth.
2. Only one stage or bounded subtask should be active at a time.
3. Each stage must be verified before the next stage starts.
4. Implement the smallest coherent playable slice for the active stage.
5. Do not create shallow UI stubs across many systems.
6. Scene scripts may present state and request actions, but domain systems own rules.
7. Any persistent state added by a stage must be save-compatible.
8. Instant Sim and Live Match Playback must continue to consume one shared match object.
9. Unfinished systems must be marked as planned or foundation-only.

## 3. Stage 0: Documentation Reconciliation

### Goal

Make repo docs consistent with the master design.

### What To Implement

- Update `docs/PRD.md` so Touchline is defined as a fictional club-football career simulator.
- Update `docs/Architecture.md` so it supports the master-design modules and marks current, partial, and planned systems honestly.
- Update `docs/Guardrails.md` so future work follows the master design.
- Replace the old narrow v1 plan with this staged plan.

### What Not To Implement Yet

- No gameplay code.
- No scene changes.
- No data model changes.
- No save migration.

### Verification

- Docs no longer contradict `docs/touchline_master_design_decisions.md`.
- Repo docs no longer state that master-design systems are permanently out of scope.
- `git diff --check` passes.
- Product code remains unchanged.

## 4. Stage 1: Career Foundation

### Goal

New career setup with role, license, manager background, club archetype, board philosophy, fan culture, Director of Football style, staff, objectives, and starting squad.

### What To Implement

- Add domain state for CareerProfile with manager name, selected role, manager background, starting license, current club, reputation baseline, and career history seed.
- Add role authority data for Assistant Manager, Head Coach, and Manager.
- Add license ladder: Grassroots License, National C License, National B License, National A License, Pro License.
- Add manager backgrounds from the master design and their starting effects.
- Add club identity fields: archetype, board philosophy, fan culture, Director of Football style, staff list, objectives, budget/wage summary, academy quality, and stability.
- Update career setup and club selection to choose or display the new fields.
- Update dashboard to show role-specific authority summary, board morale, fan morale, squad morale, job pressure, objectives, next fixture, and news foundation.
- Ensure save/load covers the new career foundation state.

### What Not To Implement Yet

- Full transfer negotiation.
- Full scouting report timing.
- Full job market.
- Deep youth academy.
- Deep finance ledger.
- Complete four-tier league pyramid.
- Full media/decision event system.

### Verification

- Starting a new career creates a valid state and dashboard.
- Role, license, background, club identity, objectives, staff, and pressure state persist through save/load.
- Dashboard renders from authoritative state, not hardcoded scene text.
- Existing matchday path still works or unsupported transitions are explicitly disabled.

## 5. Stage 2: Player And Squad Identity

### Goal

Players have partial information, styles, traits, personality, tactical fit, form, morale, fitness, and contract basics.

### What To Implement

- Expand Player model with identity, ability, known attributes, estimated ranges, unknown attributes, playing style, tendencies, traits, personality, tactical fit, development curve, contract basics, morale, form, fitness, fatigue, injury risk, relationship, squad status, promise history, and transfer interest foundation.
- Add information visibility rules affected by role, license, staff quality, and scouting confidence.
- Update squad and player profile screens to show exact ratings, estimated ranges, question marks, and scouting language.
- Add tactical fit language and player context summaries.
- Save/load expanded player state.

### What Not To Implement Yet

- Full scouting assignments.
- Full contract negotiation.
- Full promise resolution.
- Full injury system.
- Full transfer market.

### Verification

- Squad/profile screens show exact ratings, estimates, question marks, and scouting language.
- Player identity is visible beyond ratings.
- Save/load preserves player information state.
- Existing lineup and match preparation flows still work.

## 6. Stage 3: Tactics Foundation

### Goal

Formation, team style, instructions, player roles, player instructions, tactical familiarity, and tactical risk/fit notes.

### What To Implement

- Add Tactic model with formation, team style, team instructions, player roles, player instructions, tactical familiarity, fit analysis, and risk analysis.
- Support master-design formations and team styles.
- Add tactical familiarity scale: Excellent, Very Familiar, Familiar, Neutral, Unfamiliar, Poor, Very Poor.
- Update tactics UI to edit and explain the implemented tactic fields.
- Feed tactic fields into match simulation inputs.

### What Not To Implement Yet

- AI tactical advice engine.
- Full opponent-specific scouting.
- Deep set-piece designer.
- Complex tactical training micro-management.

### Verification

- Tactics screen updates and affects match simulation inputs.
- Tactic state persists through save/load.
- Match reports can reference tactic/familiarity causes.

## 7. Stage 4: Calendar, Training, Scouting

### Goal

Weekly loop with training focus, scouting report timing, fitness/recovery, tactical familiarity updates, and news.

### What To Implement

- Add daily and weekly calendar actions.
- Add weekly training focus options from the master design.
- Add training effects on tactical familiarity, development, fatigue, morale, and injury risk foundation.
- Add scouting assignments with report timing and partial discovery.
- Add a news feed with category and reliability labels.
- Add simple world event generation tied to state changes.

### What Not To Implement Yet

- Full global scouting network.
- Full youth intake.
- Full transfer negotiation.
- Full media press-conference system.
- Complete 38-match season if the current fixture structure needs a separate league-stage refactor first.

### Verification

- Advancing time updates training, scouting, and news.
- Scouting reports reveal exact, estimated, and unknown information according to confidence.
- Training focus affects tactical familiarity and player condition.
- Save/load preserves training, scouting, and news state.

## 8. Stage 5: Match Engine Alignment

### Goal

Shared match result/timeline for Instant Sim and Live Playback, with post-match explanations.

### What To Implement

- Align match simulation inputs with player ability, current context, tactical setup, tactical familiarity, player tactical fit, team morale, match momentum, staff preparation, opponent strength, and opponent style.
- Expand match event types toward the master-design list.
- Ensure Instant Sim and Live Playback consume the same Match object.
- Keep live playback as a visualization of the simulated timeline.
- Improve post-match explanation fields based on the shared match data.

### What Not To Implement Yet

- Playable football controls.
- Pure physics simulation.
- 3D match engine.
- Full broadcast presentation beyond what is needed to read the match.

### Verification

- Instant Sim and Live Playback consume the same match object.
- Replaying live playback does not generate a different result.
- Post-match report explains tactical and player causes from the match object.

## 9. Stage 6: Consequences And Pressure

### Goal

Post-match updates to board morale, fan morale, squad morale, player form, tactical familiarity, news, job pressure, and reputation.

### What To Implement

- Separate morale, trust, reputation, and pressure state.
- Add board morale, fan morale, squad morale, player morale, board trust, player trust, staff trust, Director trust, job pressure, media pressure, dressing-room pressure, and transfer pressure foundations.
- Add job security states.
- Add objective priority and objective type support.
- Update post-match and calendar consequences to use board philosophy, fan culture, role authority, and match report causes.
- Generate news from consequence changes.

### What Not To Implement Yet

- Full sacking aftermath.
- Full job market.
- Deep media interviews.
- Complex agent promise disputes.

### Verification

- Match results visibly change game state.
- Board, fan, squad, player, tactical, news, pressure, and reputation changes are explainable.
- Morale, trust, reputation, and pressure are stored separately.
- Save/load preserves consequence state.

## 10. Stage 7: Transfers/Contracts Foundation

### Goal

Scouting-based recruitment, player interest, board approval, Director influence, contracts, promises, and simple transfer flow.

### What To Implement

- Add transfer targets, shortlist, player interest, club interest, board approval, Director of Football influence, rival bid foundation, and transfer history.
- Add contract basics: wage, duration, role, promises, clauses, renewal state, and agent foundation.
- Add a simple transfer or contract interaction that changes state and triggers board/fan/player/media reactions.
- Add promise tracking with statuses from the master design.

### What Not To Implement Yet

- Fully featured negotiation UI.
- Complete loan market.
- Deep wage-structure optimization.
- Full agent personality system.
- Complete staff market unless needed for the transfer slice.

### Verification

- User can complete one basic transfer/contract interaction with consequences.
- Transfer does not resolve on fee alone.
- Board, fan, Director, player, and news consequences are recorded.
- Save/load preserves transfer, contract, and promise state.

## 11. Stage 8: Career/Job Market Foundation

### Goal

Job security, sackings, renewals, interim opportunities, job offers, and license progression.

### What To Implement

- Add job security evaluation using objectives, pressure, board philosophy, club stability, role authority, recent results, fan pressure, and dressing-room control.
- Add license progression opportunities.
- Add club manager states and basic job offers.
- Add applying for jobs and interim opportunity foundation.
- Add sacking aftermath and career history entries.
- Add end-of-season review with objective, player, transfer, financial, staff, job security, job offer, and license opportunity summaries as implemented systems allow.

### What Not To Implement Yet

- Fully simulated global manager carousel.
- Deep multi-country world simulation.
- Dialogue-heavy media system.
- Playable youth leagues.
- Owner-level political simulation.

### Verification

- Career state can change based on performance and pressure.
- Job security and license progression are explainable.
- A basic job offer or interim event can be generated from state.
- Save/load preserves career history and job market state.

## 12. Ongoing Verification Baseline

Every stage should use the narrowest strong checks available:

- documentation consistency scan for doc-only changes
- `dotnet build game/Touchline.sln` for C# changes
- Godot headless route/domain checks for scene or runtime changes
- focused save/load checks for persistent state changes
- manual smoke tests for visual or navigation changes that automation cannot prove
