# Touchline Master Implementation Roadmap

## 1. Current Foundation Summary

Touchline has completed the 28-phase master-design implementation pass. It can start or load a career, choose role/background/license/difficulty/club, show club identity, inspect squad and player information, adjust tactics within role authority, advance the calendar, run training/scouting, process transfers/contracts/loans/promises, generate youth prospects, track finance, play shared instant/live match timelines, apply post-match consequences, run job-market movement, persist career memory, and verify an end-to-end season loop.

This roadmap now records what was implemented in the pass and the remaining depth gaps. It is not a request to add new major systems during stabilization; future work should be audit-driven fixes, balance tuning, content expansion, or a new approved roadmap.

### Real gameplay systems

- Role authority affects at least lineup/tactics/recruitment actions.
- Tactics, player condition, morale, fitness, familiarity, staff preparation, and opponent context feed the shared match simulator.
- Instant Result and Live Match consume one simulated match object and timeline.
- Training and scouting progress over time and affect state.
- Post-match consequences update morale, trust, reputation, pressure, tactical familiarity, player condition, and news.
- Save/load persists save version 26, including player identity, tactics, training/scouting, recruitment, promises, staff, youth, finance, competitions, media/events, job security, job offers, generated content, difficulty settings, and career memory.

### UI-visible foundations

- Career setup, club selection, dashboard, squad/profile, tactics, fixtures, standings, matchday, live match, post-match, and save/load screens exist.
- Recruitment/contracts, promises, job market, staff, board/fan/Director context, news, youth, finance, generated content, difficulty settings, and license progression are visible and state-backed, with foundation-level depth.
- Calendar, training, scouting, career market, and recruitment actions are currently surfaced through compact dashboard/context flows rather than full dedicated screens.

### Data-model-only foundations

- Career history exists as structured career memory plus readable summaries, but it is not yet a deep encyclopedia.
- Promises exist as records with lifecycle resolution and consequences, but not a dialogue-heavy negotiation system.
- License progression exists as opportunity text, not course scheduling or eligibility gates.
- Recruitment/contract state stores interest, fit, fee/wage ranges, board response, Director response, and promise status, but not full negotiation history.
- Reputation, pressure, and trust are separate values, but not yet deeply segmented across the full design.

### Simplified placeholders

- Finance is readable and gameplay-affecting, but not a deep accounting simulator.
- Youth academy has intake, prospects, promotion, and loan suitability, but not playable youth leagues.
- League structure includes compact tiers, cups, promotion/relegation summaries, and shadow simulation context, not a fully deep multi-country pyramid.
- Staff market, job applications/interviews, media/events, and decision events exist as playable foundations, not deep standalone simulations.
- Match simulation is a shared stat-and-event tactical engine, not a physics or broadcast-depth match engine.

### Known risks

- Transfers/contracts/loans remain foundation-depth and should not be described as complete negotiation systems.
- Youth, finance, media/events, job market, generated content, and career memory are coherent foundations with room for content and depth expansion.
- Save version 26 is the current supported shape; future persistent changes must migrate carefully.
- Role authority, shared match timeline, partial information, and separated morale/trust/reputation/pressure remain the highest-risk invariants.

## 2. Full Remaining Work Map

| System | Current state | Target state | Gap | Priority | Depends on | Risk if ignored |
|---|---|---|---|---|---|---|
| Role authority | Foundation, with key action restrictions | Every user action respects Assistant Manager, Head Coach, and Manager authority | More actions will be added and must be permissioned | High | Existing role state | Roles become cosmetic |
| Licenses | Starting effects and opportunity text | Licenses affect eligibility, information depth, scouting interpretation, media credibility, and progression | No true license gates or course timing | High | Career history, job market, scouting | Career progression becomes unrealistic |
| Manager backgrounds | Starting trust/reputation/pressure effects | Background-specific patience, fan/media response, player respect, and opportunities | Long-term effects are shallow | Medium | Reputation, trust, media | Backgrounds feel cosmetic after setup |
| Club archetypes | Seeded identity and pressure | Archetypes drive objectives, finance, youth, transfers, patience, and job risk | Effects exist but are broad | Medium | Objectives, finance, board/fans | Clubs feel interchangeable |
| Board philosophy | Visible and lightly affects pressure/recruitment | Board rewards/punishes decisions by philosophy across season | Limited decision coverage | High | Objectives, finance, transfers | Board logic becomes one-dimensional |
| Fan culture | Visible and lightly affects pressure | Fans react differently to results, style, transfers, youth, derbies, and identity | Limited reaction logic | High | News, rivalries, transfers | Fan morale collapses into board morale |
| Director of Football | Style, relationship, recruitment response | DoF controls scouting/recruitment conflict, leaks, sales, board reports, and cooperation | No conflict lifecycle | High | Transfers, staff, promises, news | Club politics stay superficial |
| Staff roles | Staff list and quality affect scouting/prep | Staff influence information, training, development, media, morale, and hiring | No staff contracts/market/deep reports | High | Staff market, training, scouting | Staff are mostly flavor text |
| Player identity | Identity fields and partial info visible | Players have true/known/estimated/unknown attributes, histories, tendencies, personalities, relationships, and development | Hidden model and discovery rules are shallow | High | Scouting, development, save history | Players regress to ratings |
| Partial information | Exact/range/? summaries visible | Visibility changes by license, staff, scouting, role, and difficulty | Rules are not systemic enough | High | Scouting, licenses, UI philosophy | The partial-information game fails |
| Tactics | Formation/style/instructions/familiarity affect match | Full tactic object with roles, instructions, risk, fit, familiarity, set pieces, and opponent preparation | Limited depth and role training | High | Player identity, training, match engine | Match outcomes feel underexplained |
| Tactical familiarity | Score and label affect match | Familiarity by team style, formation, role, player, and time | Single broad score | Medium | Training, tactics, player roles | Tactical changes lack realistic cost |
| Transfers | One basic action and target | Scouting-led market with shortlists, bids, rival bids, approvals, integration, and history | No true market or negotiation | High | Scouting, contracts, finance, DoF | Recruitment remains a button |
| Contracts | Basic wage/expiry/role | Negotiation with wage, duration, role, clauses, renewal, agent, board limits | No negotiation or clauses | High | Finance, promises, agents | Contracts lack consequence |
| Promises | Records after basic action | Full lifecycle: active, on track, at risk, broken, fulfilled, renegotiated with effects | No resolution cadence | High | Contracts, morale, trust, news | Promise UI becomes fake |
| Match simulation | Shared compact event engine | Rich stat/event engine using player attributes, tactics, role fit, fatigue, morale, staff, opponent style, momentum, cards, injuries | Limited event variety/cause model | High | Player identity, tactics, training | Core gameplay feels thin |
| Live Match Playback | Visualizes shared timeline | Clear tactical playback of the same timeline with readable events and no alternate sim | Needs richer timeline display as engine grows | Medium | Match engine | Live mode becomes decorative |
| Post-match reports | Basic consequences and explanation | Full report with stats, tactical causes, player ratings, fit/fatigue notes, morale, board/fan/media/staff/development analysis | Explanations are compact | High | Match engine, morale, staff, news | Results feel arbitrary |
| News/media/world events | Event feed with categories/reliability | News, media questions, world events, reliability, decision triggers, cooldowns, narrative variety | No interactive media/decision loop | High | Decision events, generated content | Career world feels static |
| Morale | Squad/fan/board/player values | Morale per board, fans, squad, players, staff, DoF, media context with explainable changes | Limited categories and triggers | High | Match, news, promises | Consequences feel generic |
| Trust | Board/player/staff/Director values | Trust changes slower than morale and controls authority, patience, access, cooperation | Not deeply separated in effects | High | Role authority, pressure | Trust becomes another morale score |
| Reputation | World reputation foundation | Club/world/media/tactical/youth/recruitment reputation histories affect jobs and transfers | Too broad and shallow | High | Career history, job market | Career movement lacks logic |
| Pressure | Job/media/dressing-room/transfer pressure | Pressure thresholds trigger warnings, ultimatums, events, sackings, and role-specific consequences | Thresholds exist but few events | High | Objectives, job security, news | Pressure lacks teeth |
| Objectives | Seeded objectives visible | Typed/priority objectives reviewed by board across season and end-of-season | Limited dynamic review | High | Board, league, finance, youth | Board expectations feel simplistic |
| Job security | States exist | Security changes from objectives, pressure, board philosophy, stability, role, form, fans, dressing room | No full sacking aftermath | High | Objectives, pressure | No real career risk |
| Sackings | State exists as possible label | Sacking aftermath, reputation effect, media narrative, career history, job-market recovery | Not implemented | High | Job security, career history | Failure has no real consequence |
| Career progression | Career history seed and job offer | Roles, licenses, reputation, contracts, history, trophies, sackings, job moves | No full timeline or movement | High | Job market, save history | Long career has no memory |
| Job market | Generated offer event | Manager carousel, vacancies, applications, interviews, interim routes, license-gated hiring | No active market simulation | High | Reputation, licenses, job security | Career remains single-club |
| Season/calendar | Current date, matchday, weekly advance | Full daily/weekly/monthly season schedule with windows, reviews, deadlines, courses, jobs | Small rhythm only | High | League structure, transfers, training | Long-term loop is weak |
| League structure | Small seeded competition | Multi-division fictional pyramid with top-two deep sim and shadow sim elsewhere | No broad pyramid | High | Fixture generation, finance, job market | World scale stays tiny |
| Promotion/relegation | Not implemented | Promotion/relegation across divisions with reputation, finance, objective impact | Missing | High | League structure, season rollover | Seasons lack stakes |
| Cup competitions | Not implemented | Domestic cups with draw, rounds, rotation, pressure, prize money, upsets | Missing | Medium | Calendar, league structure | Season variety suffers |
| Squad registration | Not implemented | Squad size, youth, foreign, loan, wage, homegrown-style rules with validation | Missing | Medium | Transfers, youth, league rules | Squad building lacks constraints |
| Training | Weekly focus affects condition/familiarity | Team, individual, role, intensity, recovery, youth, complaints, staff effects | No dedicated controls/deep effects | High | Staff, player development | Weekly loop lacks agency |
| Scouting | Basic assignment/report | Assignments, regions, report quality/delay, accuracy, analyst overlap, license effects, youth scouting | No regions/accuracy/deep discovery | High | Player info, transfers, staff | Transfer discovery is shallow |
| Youth academy | Academy quality only | Intake, generated prospects, hidden potential, promotion, loans, board/fan reaction | Missing | Medium | Generated players, development, training | Youth-focused clubs cannot function |
| Rivalries/derbies | Club context only | Rivals, derby importance, fan swings, media hype, player pressure, records, rival managers | Missing | Medium | League structure, news, pressure | Fan culture lacks peak events |
| Finance | Budget summary | Transfer/wage budget, debt, revenue, prize/ticket/commercial income, board injections, cuts, profit expectations | No ledger | High | Transfers, contracts, league/cups | Board/transfer logic lacks cost |
| Staff market | Staff list only | Staff contracts, wages, reputation, interest, poaching, leaving, loyalty, board approval, role authority | Missing | Medium | Finance, staff impact | Staff quality cannot evolve |
| Decision events | Not implemented | Player, board, media, agent, staff, training, fan, DoF, crisis, promise events with choices and effects | Missing | High | News, promises, morale/trust | Career lacks interactive drama |
| Difficulty/realism settings | Not implemented | Strict realism, drama, scouting, sacking, transfer, hidden info, randomness, finance settings | Missing | Medium | Balance, save/settings | Players cannot tune experience |
| Save/career history | v3 saves and shallow history | Persistent manager, club, player, transfer, trophy, rivalry, promise, sacking, reputation histories | Current history too light | High | All persistent systems | Long careers lose continuity |
| Generated content | Seeded world and simple templates | Generated clubs, names, news, reports, media questions with tone and variety controls | Limited templates/content | Medium | News, scouting, youth, league | Content becomes repetitive |
| Game balance | Basic deterministic checks | Tuned morale, randomness, rating changes, transfers, trust, sackings, tactics/player-quality balance | No broad tuning pass | High | All gameplay systems | Systems become unfair or toothless |
| UI information philosophy | Mix of exact/range/? visible | Consistent exact/range/?/language rules across all screens, no false completeness | Not fully systematic | High | Info visibility, scouting, UI pass | UI misleads players |
| Full game loop | Playable short loop exists | Daily/weekly/matchday/transfer/end-season/new-season/career-move loop | Incomplete long loop | High | Most systems | Game remains a demo loop |

## 3. Ordered Implementation Phases

1. Phase 1: Information visibility deepening
2. Phase 2: Training and scouting controls
3. Phase 3: Promise lifecycle
4. Phase 4: Tactical depth and role fit
5. Phase 5: Match engine depth
6. Phase 6: Post-match report depth
7. Phase 7: Morale, trust, reputation, pressure depth
8. Phase 8: News/media/world events
9. Phase 9: Transfer market expansion
10. Phase 10: Contract negotiation depth
11. Phase 11: Director of Football conflict depth
12. Phase 12: Staff impact and staff market
13. Phase 13: Youth academy
14. Phase 14: Player development and aging depth
15. Phase 15: Finance system
16. Phase 16: League structure and promotion/relegation
17. Phase 17: Cup competitions
18. Phase 18: Squad registration rules
19. Phase 19: Rivalries and derbies
20. Phase 20: Objectives, job security, sackings depth
21. Phase 21: Career job market and interviews
22. Phase 22: Generated content and narrative variety
23. Phase 23: Difficulty and realism settings
24. Phase 24: Save history and long-term career memory
25. Phase 25: Balance pass
26. Phase 26: UI polish and readability pass
27. Phase 27: End-to-end season simulation pass
28. Phase 28: Regression/stability pass

## 4. Phase Details

## Phase 1: Information visibility deepening

### Goal

Make partial information a systemic rule instead of mostly static text.

### Why this phase matters

The master design depends on uncertainty. Ratings, ranges, question marks, scouting language, staff quality, and license level must shape what the user knows.

### Current state

Players show known, estimated, and unknown summaries. Visibility is not yet strongly derived from role, license, staff, scouting confidence, or difficulty.

### Target state

Every player-facing and scouting-facing view uses an information visibility service that decides exact values, ranges, unknowns, and descriptive language from state.

### Scope

Classification: UI-visible, gameplay-affecting, data-model, infrastructure.

### Out of scope

- No full scouting regions.
- No transfer market.
- No deep development engine.
- No difficulty settings UI beyond fields needed for defaults.

### Likely files/areas affected

- Player identity and visibility domain logic.
- Squad/profile/scouting/recruitment UI.
- Save DTOs for discovered attributes and confidence.
- Focused Godot checks for player visibility.

### Dependencies

Stage 2 player identity foundation, license state, staff quality, scouting assignment foundation.

### Implementation steps

1. Add a player information visibility model with exact, estimated, unknown, and language fields per attribute group.
2. Derive visibility from player club status, scouting confidence, scout/data analyst quality, license, and role.
3. Persist discovered/known attributes separately from true ability.
4. Update squad and profile screens to render visibility output instead of static summaries.
5. Update recruitment/scouting summaries to show lower confidence than owned-squad players.
6. Add a focused check that low visibility shows question marks and high visibility shows better ranges/explanations.

### Verification

- `dotnet build game/Touchline.sln`
- Existing squad/profile checks.
- New information visibility check.
- Save/load compatibility check.

### Definition of done

Owned players, scouted targets, and unknown targets visibly differ in what the user knows, and save/load preserves discovered information.

### Risks

Exposing true hidden values would break the partial-information design. Too much uncertainty on owned players would make the UI frustrating.

### Next phase unlocked

Training and scouting controls can reveal information through real actions.

## Phase 2: Training and scouting controls

### Goal

Create dedicated user controls for training focus, scouting assignments, report timing, and report review.

### Why this phase matters

The weekly loop needs actionable choices before transfers, development, and tactical growth can become deep systems.

### Current state

Training and scouting progress exist in foundation state and dashboard summaries.

### Target state

Users can choose weekly team focus, individual focus, scouting target type, report depth, and review completed reports.

### Scope

Classification: UI-visible, gameplay-affecting, save-backed.

### Out of scope

- No global scouting region depth beyond simple target categories.
- No transfer negotiation.
- No youth intake.

### Likely files/areas affected

- Calendar/training/scouting domain logic.
- Dashboard or new training/scouting screens.
- Save DTOs for active plans, assignments, reports.
- Godot checks for weekly loop and scouting reports.

### Dependencies

Phase 1 visibility model, staff quality, current calendar.

### Implementation steps

1. Add training plan state for weekly focus, intensity, individual focus, role focus, and recovery priority.
2. Add scouting assignment state for target, report depth, days remaining, staff owner, confidence, and discovered fields.
3. Add UI controls for setting training and scouting without leaving unsupported buttons.
4. Apply daily and weekly effects to familiarity, fitness, fatigue, morale, injury risk, and visibility.
5. Generate report-ready news with reliability and confidence.
6. Persist active and completed reports.
7. Add checks for setting focus, progressing reports, reviewing report data, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Stage 4 weekly loop check.
- New training controls check.
- New scouting assignment/report check.
- Save/load compatibility check.

### Definition of done

Training and scouting are real weekly actions with visible, persisted outcomes and no fake controls.

### Risks

Too many controls before clear effects would create UI clutter. Keep report depth simple until the transfer phase.

### Next phase unlocked

Promise lifecycle can use player meetings and workload/minutes data.

## Phase 3: Promise lifecycle

### Goal

Turn promises from records into tracked commitments with deadlines, progress, risk, and consequences.

### Why this phase matters

Promises connect contracts, player relationships, squad trust, agents, and media pressure.

### Current state

Promise records can be created after a basic recruitment action but do not resolve meaningfully.

### Target state

Promises update over time and after matches. They can become on track, at risk, broken, fulfilled, or renegotiated.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No full contract negotiation UI.
- No full media dialogue.
- No transfer-request system beyond basic consequences.

### Likely files/areas affected

- Promise domain logic.
- Player relationship/morale/trust state.
- Dashboard/profile/recruitment UI.
- Save DTOs and migration.

### Dependencies

Training/calendar, player identity, player morale, role authority.

### Implementation steps

1. Add promise type, giver, recipient, expected action, deadline date, progress metric, status, and consequence risk.
2. Track at least playing time, squad role, preferred position/role, and renewal promises.
3. Update promise progress after match participation, selection status, training role focus, and calendar advancement.
4. Apply morale/trust/news consequences when promises become at risk, fulfilled, or broken.
5. Add renegotiation placeholder only when a promise is at risk; do not add a deep dialogue system.
6. Add UI summary for active and at-risk promises.
7. Add checks for promise creation, progress, status transition, consequences, and persistence.

### Verification

- `dotnet build game/Touchline.sln`
- Stage 7 recruitment/contracts check.
- New promise lifecycle check.
- Stage 6 consequence check.
- Save/load compatibility check.

### Definition of done

At least one promise can progress and resolve through normal career actions with visible effects.

### Risks

Broken promises should not explode instantly every time. Tune thresholds conservatively.

### Next phase unlocked

Tactical roles and player fit can support role promises and selection complaints.

## Phase 4: Tactical depth and role fit

### Goal

Deepen tactics into formation, team style, instructions, roles, player instructions, role comfort, and familiarity by layer.

### Why this phase matters

Match outcomes should be shaped by more than a few numeric sliders.

### Current state

Tactics have formation, team style, numeric instructions, broad roles summary, risk/fit notes, and team familiarity.

### Target state

Tactics include role assignments, player instructions, role fit, familiarity by formation/style/role, tactical risk, and explainable tradeoffs.

### Scope

Classification: UI-visible, gameplay-affecting, save-backed.

### Out of scope

- No set-piece designer.
- No AI tactical assistant.
- No opponent-specific deep scouting beyond current reports.

### Likely files/areas affected

- Tactic domain model.
- Tactics screen and tactical board.
- Match simulator inputs.
- Save DTOs for roles/instructions/familiarity.

### Dependencies

Phase 1 player visibility, Phase 2 training, current match simulator.

### Implementation steps

1. Add tactic state for per-position role, per-player role assignment, and player instruction.
2. Add role fit calculation from player attributes, style, tendencies, traits, personality, and fitness.
3. Split familiarity into formation, team style, and role familiarity.
4. Update training effects to improve relevant familiarity layers.
5. Update tactics UI to edit roles/instructions without creating a second match-engine path.
6. Feed role fit and familiarity layers into match simulation.
7. Add checks for role persistence, role-fit effect, and match simulator input.

### Verification

- `dotnet build game/Touchline.sln`
- Tactics context/persistence checks.
- Match variation check.
- New role-fit/familiarity check.
- Live match shared timeline check.

### Definition of done

Changing roles or player instructions changes persisted tactic state and match inputs, and the UI explains fit/risk.

### Risks

Role options can become too broad. Implement a small set per position first.

### Next phase unlocked

Match engine depth can consume richer tactical inputs.

## Phase 5: Match engine depth

### Goal

Expand the shared match engine into a richer stat-and-event simulation.

### Why this phase matters

The match is the central proof that players, tactics, morale, training, and staff matter.

### Current state

The engine produces final score, stats, events, tactical summary, player ratings summary, and notes from compact inputs.

### Target state

The engine creates a richer authoritative match object with event types, player ratings, cards, injury risk events, tactical momentum, fatigue, and explainable causes.

### Scope

Classification: gameplay-affecting, infrastructure, UI-visible.

### Out of scope

- No arcade controls.
- No pure physics engine.
- No separate live-match simulation.

### Likely files/areas affected

- Match simulator and playback models.
- Match stats service.
- Live match renderer.
- Post-match consequence inputs.

### Dependencies

Phase 4 tactics, Phase 1 player visibility, Phase 2 training/fatigue, staff quality.

### Implementation steps

1. Expand match event kinds to include chances, goals, cards, injuries, substitutions-ready moments, tactical shifts, fatigue events, and momentum swings.
2. Add player match rating model with position, role fit, contribution, fatigue, and event involvement.
3. Add opponent style and strength inputs with deterministic generated behavior.
4. Add tactical cause records to the match object.
5. Ensure Instant Result and Live Match still consume the same object.
6. Add regression checks proving a live replay does not alter the result.
7. Add save handoff only for the current/last authoritative match object needed by post-match flow.

### Verification

- `dotnet build game/Touchline.sln`
- Shared engine check.
- Live renderer check.
- Match stats check.
- Post-match report check.
- New match depth check for cards/injuries/player ratings/tactical causes.

### Definition of done

The match object includes richer events and explanations, and both instant and live paths use it unchanged.

### Risks

Adding event complexity can break determinism. Keep seeded simulation deterministic.

### Next phase unlocked

Post-match reports can expose richer causes.

## Phase 6: Post-match report depth

### Goal

Make post-match reports explain what happened across tactics, players, fatigue, morale, board, fans, media, staff, and development.

### Why this phase matters

The user needs cause-and-effect clarity after matches, especially with hidden information and pressure systems.

### Current state

Reports show score, stats, consequence summaries, tactical explanation, and pressure context.

### Target state

Reports present full master-design sections with stats, tactical explanation, player ratings, fit notes, fatigue notes, morale changes, board/fan reaction, media story, staff analysis, and development notes.

### Scope

Classification: UI-visible, gameplay-affecting.

### Out of scope

- No full media press conference.
- No deep dialogue.
- No manual report editing.

### Likely files/areas affected

- Post-match scene.
- Match report model.
- Consequence service.
- Staff analysis/report logic.

### Dependencies

Phase 5 match engine, staff quality, morale/trust/pressure state.

### Implementation steps

1. Expand match report data with sectioned explanations.
2. Map match tactical causes into report language.
3. Generate top player, weak fit, fatigue, card/injury, and development notes.
4. Generate board and fan reaction from philosophy/culture, not just score.
5. Generate media headline/story from event facts.
6. Persist last report and relevant history summary.
7. Add UI checks for all report sections.

### Verification

- `dotnet build game/Touchline.sln`
- Post-match report check.
- Consequence/pressure checks.
- New post-match depth check.
- Save/load check.

### Definition of done

Every match report explains why the result happened and what changed afterward.

### Risks

Report text can become repetitive. Use structured templates with varied facts.

### Next phase unlocked

Morale, trust, reputation, and pressure can use richer match causes.

## Phase 7: Morale, trust, reputation, pressure depth

### Goal

Make morale, trust, reputation, and pressure separate systems with distinct update speeds and consequences.

### Why this phase matters

The master design explicitly separates current feeling, belief in the user, outside perception, and closeness to consequences.

### Current state

Separate values exist, but effects and histories are shallow.

### Target state

Each category updates from specific causes, persists history, affects decisions, and is explained in UI.

### Scope

Classification: gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No full sacking aftermath yet.
- No full media dialogue.

### Likely files/areas affected

- Perception/consequence systems.
- Dashboard/post-match/news.
- Career profile save data.
- Focused pressure/trust checks.

### Dependencies

Phase 6 reports, promises, board/fan/DoF/staff/player state.

### Implementation steps

1. Define separate delta reasons for morale, trust, reputation, and pressure.
2. Make trust move slower than morale.
3. Add reputation categories: club, world, media, tactical, youth, recruitment.
4. Add pressure categories: job, media, dressing room, transfer, board, fan.
5. Surface current reasons and recent trend in dashboard and post-match.
6. Persist short histories for each category.
7. Add checks for win/loss/derby/board/fan divergence once rivalry exists; for now use board/fan philosophy cases.

### Verification

- `dotnet build game/Touchline.sln`
- Pressure context check.
- Post-match causes check.
- New morale/trust/reputation/pressure separation check.
- Save/load compatibility check.

### Definition of done

The four systems behave differently, are visible, and influence at least job security, recruitment, role authority, and news.

### Risks

Too many numbers can overwhelm UI. Show reasons and trend before raw detail.

### Next phase unlocked

News/media/world events can trigger and reflect pressure states.

## Phase 8: News/media/world events

### Goal

Build structured news, media, world events, and decision-event triggers.

### Why this phase matters

The career world needs believable football drama and decisions beyond matches.

### Current state

News feed has categories, reliability labels, and event text from some state changes.

### Target state

News templates, media questions, agent/staff/board/player events, world events, reliability, cooldowns, and choice outcomes exist.

### Scope

Classification: UI-visible, gameplay-affecting, save-backed.

### Out of scope

- No dialogue-heavy media system.
- No AI-generated free text.
- No global manager carousel yet.

### Likely files/areas affected

- News/event domain models.
- Dashboard or new news/events screen.
- Save DTOs for active/resolved events.
- Generated content templates.

### Dependencies

Phase 7 pressure systems, promises, staff/DoF/player relationship state.

### Implementation steps

1. Add NewsEvent fields from master design: source type, related club/player/staff/match, effects, importance, reliability, decision options.
2. Add decision event model with options and deterministic consequences.
3. Implement player meeting, board meeting, media question, agent call, staff disagreement, training issue, fan pressure, DoF conflict, and crisis foundations.
4. Add cooldowns and thresholds to prevent spam.
5. Add UI for reviewing and resolving active events.
6. Persist active/resolved events and resulting history snippets.
7. Add checks for event generation, option resolution, consequences, cooldowns, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Dashboard/navigation checks.
- New news/event decision check.
- Morale/trust/pressure checks.
- Save/load compatibility check.

### Definition of done

At least one event in each major category can appear, be resolved, and affect state.

### Risks

Events must be football-logical and not random chaos. Use thresholds and cooldowns.

### Next phase unlocked

Transfer market can use news, agents, DoF conflict, and board events.

## Phase 9: Transfer market expansion

### Goal

Create a scouting-led recruitment market with shortlists, interest, bids, rival pressure, board approval, Director influence, and transfer history.

### Why this phase matters

Transfers are a major Touchline system and must not resolve on fee alone.

### Current state

One target can be viewed and one basic action can be requested/attempted.

### Target state

Users can identify needs, scout/shortlist targets, check interest, request/submit offers, face board/Director constraints, see rival bids, and record outcomes.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No deep contract negotiation beyond handoff to Phase 10.
- No loans unless minimal for target status.
- No staff market.

### Likely files/areas affected

- Recruitment/transfer domain models.
- Scouting reports.
- Dashboard or transfer screen.
- Finance budget hooks.
- Save DTOs and migration.

### Dependencies

Phase 2 scouting, Phase 8 events, DoF foundation, finance summary.

### Implementation steps

1. Add shortlist and transfer target collections with need, source, scout confidence, interest, estimated fee/wage, and status.
2. Add club/player/agent interest checks using role, reputation, league, wages, tactical fit, playing-time path, and board/Director stance.
3. Add offer request and board approval flow that considers fit, fee, wage, budget, age, philosophy, fan culture, and DoF style.
4. Add rival bid and hijack foundation as event-driven pressure, not random outcome.
5. Add transfer history entries for requested, approved, blocked, failed, and completed actions.
6. Add UI to view targets and attempt a bounded transfer action.
7. Add checks proving transfers do not resolve on fee alone.

### Verification

- `dotnet build game/Touchline.sln`
- Recruitment/contracts check.
- New transfer market check.
- News/event check.
- Save/load migration check.

### Definition of done

The user can progress one target through a stateful transfer flow with board, Director, player, agent, and news consequences.

### Risks

Without finance depth, budget rules must remain simple and explicit until Phase 15.

### Next phase unlocked

Contract negotiation can attach terms to approved transfer/renewal flows.

## Phase 10: Contract negotiation depth

### Goal

Add contract negotiations for wages, duration, squad role, clauses, renewal state, agent mood, and promises.

### Why this phase matters

Contracts connect transfers, promises, agents, wages, board approval, and player relationships.

### Current state

Players have wage/expiry/role basics and a basic promise can be logged.

### Target state

Contract talks are stateful and constrained by wage structure, player interest, agent type, role authority, and board approval.

### Scope

Classification: gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No full legal/financial simulator.
- No complex clause market beyond a small supported set.

### Likely files/areas affected

- Contract and agent domain models.
- Transfer/recruitment UI.
- Player profile.
- Finance hooks.
- Save DTOs.

### Dependencies

Phase 9 transfers, Phase 3 promises, Phase 15 finance foundation may be limited but budget hooks needed.

### Implementation steps

1. Add contract offer state with wage, expiry, role promise, clauses, agent mood, player interest, and board status.
2. Add agent types from master design with negotiation preferences.
3. Add renewal flow for current squad players and signing flow for transfer targets.
4. Apply board/DoF constraints before acceptance.
5. Create promises from accepted role/position/renewal terms.
6. Record contract history and news reaction.
7. Add checks for role restrictions, board rejection, agent mood, accepted contract, promise creation, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Transfer market check.
- Promise lifecycle check.
- New contract negotiation check.
- Save/load migration check.

### Definition of done

At least one transfer target and one current player can go through a bounded contract/renewal flow with persistent outcomes.

### Risks

Negotiation can sprawl. Keep options limited and deterministic first.

### Next phase unlocked

Director of Football conflict can intervene in transfers/contracts.

## Phase 11: Director of Football conflict depth

### Goal

Make the Director of Football a distinct actor with cooperation, conflict, reports, leaks, and transfer influence.

### Why this phase matters

The master design requires board, Director, and user authority to remain separate.

### Current state

DoF style and relationship are visible and influence basic recruitment responses.

### Target state

DoF style drives shortlist preferences, blocked targets, alternative targets, leaks, board reports, sales pressure, and cooperation level.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No firing/hiring Director.
- No owner-level politics.

### Likely files/areas affected

- Director/club politics domain logic.
- Transfer/contracts/events/news.
- Dashboard/recruitment UI.
- Save DTOs.

### Dependencies

Phases 8-10, trust/pressure, board philosophy.

### Implementation steps

1. Add DoF relationship trend and reason history.
2. Add style-specific transfer preferences and objections.
3. Add conflict events: blocks target, proposes alternative, pushes sale, leaks disagreement, frames failed signing.
4. Add cooperation benefits for Ally/Supportive states.
5. Connect DoF actions to board trust, media pressure, transfer pressure, and news.
6. Persist DoF events and relationship history.
7. Add checks for each relationship state affecting transfer cooperation.

### Verification

- `dotnet build game/Touchline.sln`
- Transfer/contract checks.
- News/event checks.
- New DoF conflict check.
- Save/load migration check.

### Definition of done

DoF style and relationship can materially alter at least one recruitment and one pressure/news outcome.

### Risks

DoF should constrain, not randomly sabotage. Require explainable reasons.

### Next phase unlocked

Staff impact and staff market can deepen club structure.

## Phase 12: Staff impact and staff market

### Goal

Make staff quality, traits, contracts, wages, reputation, loyalty, hiring, and leaving meaningful.

### Why this phase matters

Staff should affect information quality, training, scouting, fitness, development, morale, media, and tactical understanding.

### Current state

Staff list and quality values exist with light effects.

### Target state

Staff provide reports/effects, have contracts and wages, can be hired/poached/leave, and are restricted by role authority and board approval.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No deep staff personalities beyond concise traits.
- No large staff market database yet.

### Likely files/areas affected

- Staff domain models.
- Training/scouting/media/match prep.
- Finance hooks.
- Staff UI or dashboard panels.
- Save DTOs.

### Dependencies

Training/scouting, finance hooks, role authority, board approval.

### Implementation steps

1. Expand staff members with contract, wage, reputation, loyalty, ambition, preferred style, and relationship.
2. Map each staff role to concrete effects from the master design.
3. Add staff report generation for training, scouting, injury risk, tactics, morale, media, and recruitment.
4. Add limited staff market actions with role authority and board approval.
5. Add poaching/leaving events driven by reputation/loyalty.
6. Persist staff contracts and history.
7. Add checks for staff effects and role-based hiring restrictions.

### Verification

- `dotnet build game/Touchline.sln`
- Training/scouting checks.
- Role authority check.
- New staff market check.
- Save/load migration check.

### Definition of done

Staff quality changes at least training, scouting, injury risk, media, and tactical report outputs, and one staff hire/reject flow works.

### Risks

Staff market can become too big. Start with a small generated candidate pool.

### Next phase unlocked

Youth academy can use youth coach, scout, and staff development effects.

## Phase 13: Youth academy

### Goal

Implement youth intake, academy quality, prospects, hidden potential, promotion, loans, and reactions.

### Why this phase matters

Youth clubs, youth coaches, development, promises, and fan/board reactions need a real academy loop.

### Current state

Academy quality exists as club context only.

### Target state

Academy produces rare meaningful prospects with hidden potential, development paths, scouting uncertainty, and promotion/loan decisions.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No playable youth leagues.
- No full reserve-team simulation.

### Likely files/areas affected

- Player generation.
- Youth academy domain.
- Training/development/scouting.
- Club/fan/board reactions.
- Save DTOs and history.

### Dependencies

Staff impact, generated content, player development, training/scouting.

### Implementation steps

1. Add youth academy state: quality, intake date, prospect list, staff influence, and club pathway.
2. Generate prospects with age, position, region, style, visible info, hidden potential, personality, and rarity.
3. Add scouting/review actions for youth prospects.
4. Add promotion action into senior squad with registration/contract placeholder.
5. Add loan development placeholder if registration/transfer timing supports it.
6. Apply board/fan reactions for youth-focused and win-now clubs.
7. Persist youth prospects and history.
8. Add youth intake and promotion checks.

### Verification

- `dotnet build game/Touchline.sln`
- Player identity checks.
- Training/scouting checks.
- New youth academy check.
- Save/load migration check.

### Definition of done

A youth intake can generate prospects, reveal partial info, promote a player, and trigger board/fan/news reaction.

### Risks

Youth quality must remain rare and meaningful. Avoid flooding squads with high-potential players.

### Next phase unlocked

Player development and aging can handle youth and senior growth.

## Phase 14: Player development and aging depth

### Goal

Deepen growth curves, potential, late bloomers, decline, injury impact, form vs true ability, temporary boosts, and permanent changes.

### Why this phase matters

Long careers require players to change believably over time.

### Current state

Development curve text and basic condition/form changes exist.

### Target state

Players have hidden potential, development trend, role growth, workload effects, injury impact, age decline, and history.

### Scope

Classification: gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No playable training mini-game.
- No full medical simulation.

### Likely files/areas affected

- Development system.
- Player model/save data.
- Training/match/player profile.
- Youth academy.

### Dependencies

Youth, training, match engine, player history.

### Implementation steps

1. Add true ability, potential range, development trend, and hidden potential certainty.
2. Update development from minutes, performance, age, training, morale, role fit, staff, and injuries.
3. Separate form changes from permanent ability changes.
4. Add decline logic for older players and injury setbacks.
5. Record player development history.
6. Surface development notes in profile and post-match/season review.
7. Add checks for young growth, senior decline, injury impact, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Player condition/development checks.
- Season rollover checks.
- New development aging check.
- Save/load migration check.

### Definition of done

Player ability and development trends change over time for explainable reasons and are persisted.

### Risks

Rating changes should be slow. Avoid volatile permanent ability swings.

### Next phase unlocked

Finance can value players, wages, and club strategy more credibly.

## Phase 15: Finance system

### Goal

Add readable finances: transfer budget, wage budget, debt, revenue, prize money, ticket income, commercial growth, board injections, cuts, and profit expectations.

### Why this phase matters

Transfers, contracts, board philosophy, club archetypes, and job pressure need financial constraints.

### Current state

Budget and wage summaries are visible but not a real ledger.

### Target state

Finance state affects transfers, contracts, objectives, board trust, pressure, and season review without becoming an accounting simulator.

### Scope

Classification: gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No detailed accounting UI.
- No real-world FFP.
- No backend/economy service.

### Likely files/areas affected

- Finance domain model.
- Transfers/contracts/board objectives.
- Club dashboard and season review.
- Save DTOs.

### Dependencies

Transfers, contracts, league prize money, club archetypes.

### Implementation steps

1. Add finance state for transfer budget, wage budget, current wages, debt, revenue, expense, projected balance, and board constraints.
2. Connect wage offers and transfer fees to budget checks.
3. Add prize money, ticket income, and commercial income hooks.
4. Add board philosophy reactions for profit/loss/wage control.
5. Add budget cuts/injections from performance and board events.
6. Surface finance summary and warnings.
7. Add checks for budget rejection, wage pressure, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Transfer/contract checks.
- New finance check.
- Save/load migration check.

### Definition of done

Financial state constrains at least transfers/contracts and affects board confidence/pressure.

### Risks

Keep finance readable. Avoid excessive ledger detail.

### Next phase unlocked

League structure and competitions can add prize money, revenue, and promotion/relegation stakes.

## Phase 16: League structure and promotion/relegation

### Goal

Build the fictional league pyramid, deep/shadow simulation split, fixture generation, promotion, relegation, reputation, and prize hooks.

### Why this phase matters

The world must support long careers, job movement, promotion/relegation stakes, and club reputation tiers.

### Current state

A small seeded competition exists with fixtures, standings, and rollover.

### Target state

Top two divisions of shipped countries simulate deeply; lower/other leagues shadow simulate. Promotion/relegation changes club state.

### Scope

Classification: infrastructure, gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No full multi-country playable release scope unless docs are updated.
- No cup competitions until Phase 17.

### Likely files/areas affected

- Competition runtime service.
- World seed/generated league data.
- Fixtures/standings screens.
- Save/migration.
- Job market and finance hooks.

### Dependencies

Finance, career/job market, generated content.

### Implementation steps

1. Define league/division data with reputation, club tiers, and deep/shadow sim flags.
2. Generate fixtures for each deep-sim division.
3. Shadow simulate non-focused leagues with summary results and manager vacancies.
4. Add promotion/relegation rules and season rollover application.
5. Update standings/fixtures UI to navigate relevant competitions.
6. Connect league reputation and prize hooks to finance/job market.
7. Add checks for fixture generation, table integrity, promotion, relegation, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Fixture/standings checks.
- Full season regression check.
- New league pyramid promotion/relegation check.
- Save/load migration check.

### Definition of done

A full season can promote/relegate clubs and persist league state without breaking the playable loop.

### Risks

Large fixture sets can slow tests. Keep checks targeted and deterministic.

### Next phase unlocked

Cup competitions can be added to the calendar.

## Phase 17: Cup competitions

### Goal

Add domestic cup rounds, draws, rotation pressure, prize money, upsets, and cup-specific news/objectives.

### Why this phase matters

Cups create fixture variety, smaller-club drama, rotation choices, and board/fan pressure.

### Current state

Cup competitions are not implemented.

### Target state

Cups have rounds, generated draws, fixtures, results, prize hooks, and post-match consequences.

### Scope

Classification: gameplay-affecting, save-backed, UI-visible.

### Out of scope

- No continental cups.
- No complex replay rules unless specified later.

### Likely files/areas affected

- Competition runtime service.
- Calendar/fixtures/matchday/post-match.
- Objectives/news/finance.
- Save DTOs.

### Dependencies

League structure, calendar, finance, match engine.

### Implementation steps

1. Add cup competition model with entrants, round, draw, fixture, and status.
2. Schedule cup fixtures into the calendar without breaking league matchdays.
3. Simulate cup matches through the shared match engine for user matches and deterministic results for others.
4. Add prize money and objective/fan/board reactions.
5. Show cup fixtures/results in fixtures and dashboard.
6. Persist cup state.
7. Add cup round/draw/result checks.

### Verification

- `dotnet build game/Touchline.sln`
- Matchday/post-match checks.
- Fixture/calendar checks.
- New cup competition check.
- Save/load migration check.

### Definition of done

The user can play or simulate a cup fixture and see it affect calendar, finance, objectives, and news.

### Risks

Calendar conflicts can break navigation. Validate next fixture resolution carefully.

### Next phase unlocked

Squad registration rules can apply per competition.

## Phase 18: Squad registration rules

### Goal

Implement squad size, youth, foreign, loan, wage, contract expiry, and homegrown-style registration rules.

### Why this phase matters

Squad building needs constraints beyond ability and budget.

### Current state

Lineup status and contract basics exist; no registration rules.

### Target state

Registration rules validate squads for league/cup play and transfer windows with clear warnings and role authority.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No real-world legal rules.
- No complex country-specific exceptions.

### Likely files/areas affected

- Squad/player/competition domain.
- Squad screen and transfer flows.
- Save DTOs.

### Dependencies

League/cup structure, transfers/contracts, youth academy.

### Implementation steps

1. Add registration state per competition and season.
2. Add simple rules: max squad size, youth exemptions, foreign count, loan count, wage budget check, contract expiry warning, homegrown-style count.
3. Add validation output with blocking and warning levels.
4. Add UI to view registration status and submit/update registration.
5. Connect transfer completion and youth promotion to registration needs.
6. Persist registration state.
7. Add checks for valid/invalid registration and matchday gating.

### Verification

- `dotnet build game/Touchline.sln`
- Squad/profile checks.
- Matchday preparation check.
- New squad registration check.
- Save/load migration check.

### Definition of done

Invalid registration is surfaced clearly and prevents only the actions it should prevent.

### Risks

Registration rules can frustrate if too strict. Start with simple fictional rules.

### Next phase unlocked

Rivalries and derbies can use league/cup fixtures and registration context.

## Phase 19: Rivalries and derbies

### Goal

Add rivals, derby importance, fan morale swings, media hype, player pressure, board reaction, historical records, and rival managers.

### Why this phase matters

Derbies are major emotional pressure points and deepen fan culture.

### Current state

Rivalry data is not meaningfully implemented.

### Target state

Derbies are detected from fixtures and affect match buildup, consequences, news, history, and job pressure.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No full rival manager personality system beyond foundation.
- No special match engine path.

### Likely files/areas affected

- Club/competition/news/post-match/career history.
- Matchday and dashboard UI.
- Save history.

### Dependencies

League/cup fixtures, fan culture, media/news, match reports.

### Implementation steps

1. Add rival club lists and derby importance values.
2. Detect derby fixtures in calendar/matchday.
3. Add media hype and player pressure before derby matches.
4. Apply fan, board, morale, pressure, and reputation consequences after derby results.
5. Record rivalry history and user derby record.
6. Surface derby context in dashboard/matchday/post-match.
7. Add checks for derby detection, win/loss consequences, and history persistence.

### Verification

- `dotnet build game/Touchline.sln`
- Matchday/post-match checks.
- New rivalry/derby check.
- Save/load migration check.

### Definition of done

Derbies visibly matter before and after matches and persist in career history.

### Risks

Derby effects should be significant but not overpower every other system.

### Next phase unlocked

Objectives/job security can use derbies and rivalry records.

## Phase 20: Objectives, job security, and sackings depth

### Goal

Deepen objectives, board reviews, ultimatums, job security, sackings, and sacking aftermath.

### Why this phase matters

Career risk is central to the master design.

### Current state

Objectives and job security states exist; sacking aftermath is not implemented.

### Target state

Objectives have type, priority, review cadence, explanations, consequences, ultimatums, sackings, and aftermath history.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No full job interview system until Phase 21.
- No owner-level politics.

### Likely files/areas affected

- Objectives/job security domain.
- Career profile/history.
- Dashboard/news/post-match/end-season.
- Save DTOs.

### Dependencies

Morale/trust/pressure, finance, league/cup, rivalries, career history.

### Implementation steps

1. Expand objective model with type, priority, metric, review date, current status, and consequence.
2. Add board review events at scheduled dates and end of season.
3. Add ultimatum state with deadline and required outcome.
4. Implement sacking trigger and aftermath: reason, media narrative, reputation impact, career history, and job-market availability.
5. Ensure Assistant Manager has lower blame and different consequence routes.
6. Surface objective review and job security reasons.
7. Add checks for objective review, ultimatum, sacking, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Pressure/job market checks.
- Full season regression check.
- New objectives/sacking check.
- Save/load migration check.

### Definition of done

The user can reach an ultimatum or sacking state through explainable pressure/objective failure, and the career continues coherently.

### Risks

Sacking must not occur without clear warning and explanation.

### Next phase unlocked

Career job market and interviews can handle post-sacking movement.

## Phase 21: Career job market and interviews

### Goal

Implement job market states, applications, interviews, offers, interim opportunities, license-gated hiring, and manager movement.

### Why this phase matters

Touchline is a career simulator, not only a single-club season sim.

### Current state

A basic job offer event can be generated and career history has a seed.

### Target state

Jobs open, clubs evaluate managers, the user can apply/interview/accept/reject, and career history tracks movement.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed.

### Out of scope

- No deep global manager carousel beyond supported fictional world.
- No dialogue-heavy interviews.

### Likely files/areas affected

- Career/job market domain.
- Club manager state.
- News/events/dashboard.
- Save history.

### Dependencies

Job security/sackings, licenses, reputation, league structure, career history.

### Implementation steps

1. Add club manager state: secure, unstable, vacant, interim, newly hired.
2. Generate job openings from sackings, departures, season reviews, and emergency approaches.
3. Add application and interview invitation state with license/reputation/role fit.
4. Add simple interview decision options and outcomes.
5. Add offer acceptance/rejection and club change flow.
6. Update career history, current club, role, objectives, staff/board/fan context, and save state.
7. Add checks for job opening, application, interview, offer, acceptance, and save/load.

### Verification

- `dotnet build game/Touchline.sln`
- Career/job market checks.
- Save/load migration check.
- New job movement end-to-end check.

### Definition of done

The user can move clubs or roles through a deterministic, explainable job-market flow.

### Risks

Changing club touches many systems. Keep transition state explicit and heavily tested.

### Next phase unlocked

Generated content can broaden the world and narrative variety.

## Phase 22: Generated content and narrative variety

### Goal

Expand generated players, clubs, news headlines, scout reports, media questions, and templates.

### Why this phase matters

Touchline is fictional, so believable generated content is part of the core product.

### Current state

World seed data and structured text templates exist in limited form.

### Target state

Generated content is varied, football-logical, tone-consistent, and constrained by state.

### Scope

Classification: infrastructure, UI-visible.

### Out of scope

- No unconstrained AI text generation.
- No real clubs or players.

### Likely files/areas affected

- World seed/generation data.
- News/report/template services.
- Player/club/job/youth generation.
- Data consistency checks.

### Dependencies

News/events, scouting, youth, league, job market.

### Implementation steps

1. Add template libraries for news, reports, media questions, job offers, and pressure events.
2. Add generated name pools by fictional region and style.
3. Add club generation fields: colors, archetype, board, fans, DoF, rivals, budget, staff, objectives, academy.
4. Add variation/cooldown rules to avoid repetition.
5. Add data consistency validation for generated content.
6. Add checks for no placeholder names and state-constrained text.

### Verification

- `dotnet build game/Touchline.sln`
- Seed data check.
- News/event checks.
- New generated content consistency check.

### Definition of done

Generated content reads like the Touchline world, avoids repetition, and does not invent unsupported consequences.

### Risks

Template variety can hide contradictions. Keep outputs tied to event facts.

### Next phase unlocked

Difficulty settings can tune the now-broader system set.

## Phase 23: Difficulty and realism settings

### Goal

Add realism, drama, scouting, sacking, transfer, hidden information, match randomness, and finance difficulty settings.

### Why this phase matters

Players need control over pressure and realism without changing the core design.

### Current state

No difficulty settings are implemented.

### Target state

Settings are saved and applied consistently to scouting uncertainty, event frequency, sacking risk, transfer difficulty, finance pressure, and match randomness.

### Scope

Classification: gameplay-affecting, UI-visible, save-backed, infrastructure.

### Out of scope

- No accessibility settings unless added later.
- No online profile/settings sync.

### Likely files/areas affected

- Career setup/settings UI.
- Game state/save data.
- Scouting, news, sacking, transfer, finance, match systems.

### Dependencies

Most gameplay systems should exist before tuning settings.

### Implementation steps

1. Add difficulty settings model with defaults from master design.
2. Add setup/load UI for settings.
3. Apply settings to scouting confidence, hidden info, event cooldowns, sacking thresholds, transfer acceptance, match randomness, and finance pressure.
4. Persist settings and show them in save preview.
5. Add checks for setting persistence and at least one effect per setting category.

### Verification

- `dotnet build game/Touchline.sln`
- Career setup/save checks.
- New difficulty settings check.
- Key system checks for settings effects.

### Definition of done

Difficulty settings are visible, saved, and demonstrably affect relevant systems.

### Risks

Settings can make balance hard to reason about. Keep default balanced and tests deterministic.

### Next phase unlocked

Save history can stabilize the full long-career state.

## Phase 24: Save history and long-term career memory

### Goal

Implement long-term manager, club, player, transfer, trophy, rivalry, promise, sacking, and reputation histories.

### Why this phase matters

Long careers need memory.

### Current state

Save/load covers current foundation state and a shallow career history.

### Target state

Career memory persists across seasons and club moves, and every major system records durable history.

### Scope

Classification: infrastructure, save-backed, UI-visible.

### Out of scope

- No cloud saves.
- No database backend.

### Likely files/areas affected

- Save system/migration.
- History domain models.
- Dashboard/history UI.
- Season rollover/job/transfer/player systems.

### Dependencies

Most major systems should have stable state shapes.

### Implementation steps

1. Add structured history objects for manager career, clubs, players, transfers, trophies, rivalries, promises, sackings, and reputation.
2. Add append-only history updates from system events.
3. Add migration from shallow text history into structured history defaults.
4. Add history summary UI.
5. Add validation to reject malformed critical history state.
6. Add long-career save/load and migration checks.

### Verification

- `dotnet build game/Touchline.sln`
- Save compatibility/error checks.
- Full season regression check.
- New long-term history migration check.

### Definition of done

Major career events are persisted and can be reviewed after save/load and season rollover.

### Risks

Migration mistakes can corrupt saves. Keep version increments and validation explicit.

### Next phase unlocked

Balance pass can tune mature systems with history available.

## Phase 25: Balance pass

### Goal

Tune morale effects, randomness, rating change speed, transfer difficulty, trust speed, sacking likelihood, and tactics vs player quality.

### Why this phase matters

A coherent game can still be unfair, toothless, or chaotic without balance.

### Current state

Basic deterministic logic exists, but no broad tuning pass has happened.

### Target state

Systems produce believable long-term patterns while preserving football unpredictability.

### Scope

Classification: gameplay-affecting.

### Out of scope

- No new major systems.
- No UI redesign.

### Likely files/areas affected

- Match, development, morale/trust/pressure, transfers, finance, sacking, difficulty settings.
- Regression checks and test fixtures.

### Dependencies

All major gameplay systems through Phase 24.

### Implementation steps

1. Define balance baselines for morale, randomness, rating changes, trust, sacking, transfers, and tactics/player quality.
2. Add deterministic simulation samples for short run, full season, and stress cases.
3. Tune formulas to meet target ranges.
4. Add balance notes to comments/docs only where formulas are non-obvious.
5. Add regression checks that catch extreme outcomes.

### Verification

- `dotnet build game/Touchline.sln`
- Full season regression check.
- Multi-match regression check.
- New balance sample check.

### Definition of done

Repeated deterministic samples show plausible outcomes and no single secondary system dominates.

### Risks

Overfitting tests can make the game too predictable. Test ranges, not exact narratives.

### Next phase unlocked

UI polish can present the now-balanced systems clearly.

## Phase 26: UI polish and readability pass

### Goal

Polish all screens for information hierarchy, readability, navigation, no false claims, and consistent UI information philosophy.

### Why this phase matters

The game has many systems; bad UI will make them feel incoherent even if logic works.

### Current state

Core screens are readable and tested, but many future systems will add density.

### Target state

All screens show exact values, estimates, question marks, and scouting language consistently, with clear role authority and no misleading labels.

### Scope

Classification: UI-visible.

### Out of scope

- No new gameplay systems.
- No visual redesign from scratch unless a screen is broken.

### Likely files/areas affected

- All scenes/screens.
- Theme helpers.
- UI readability checks.

### Dependencies

Most system UI should exist.

### Implementation steps

1. Audit each screen against UI information philosophy.
2. Remove misleading labels and foundation claims that now have deeper systems or remain incomplete.
3. Normalize navigation labels, status chips, empty states, save/load messages, and role authority wording.
4. Verify mobile/desktop-like viewport readability if Godot checks support it.
5. Add or update UI readability checks for new screens.

### Verification

- `dotnet build game/Touchline.sln`
- Existing UI consistency/layout/readability checks.
- Navigation flow check.
- Manual smoke test for dense screens if automation cannot prove readability.

### Definition of done

Every screen is navigable, readable, role-aware, and honest about implemented behavior.

### Risks

Polish can drift into redesign. Keep changes scoped to clarity and correctness.

### Next phase unlocked

End-to-end season simulation can validate the complete loop.

## Phase 27: End-to-end season simulation pass

### Goal

Verify and stabilize the complete daily, weekly, matchday, transfer, cup, season, review, and new-season loop.

### Why this phase matters

The full game must work as a continuous career, not isolated systems.

### Current state

Short end-to-end flow and some season checks exist.

### Target state

A complete season can run with training, scouting, transfers, matches, cups, objectives, job security, finances, history, and new-season setup.

### Scope

Classification: gameplay-affecting, infrastructure, regression.

### Out of scope

- No new major features except fixes needed to complete the loop.

### Likely files/areas affected

- Calendar, competition, save, season rollover, dashboard, matchday, transfer windows, objectives.
- End-to-end checks.

### Dependencies

Phases 1-26.

### Implementation steps

1. Build an automated end-to-end season script that exercises main career actions.
2. Include save/load mid-season and after season rollover.
3. Include at least one transfer window, cup match, objective review, and job security update.
4. Fix broken transitions, missing state resets, or misleading UI found by the run.
5. Add resume instructions and regression documentation.

### Verification

- `dotnet build game/Touchline.sln`
- Full season regression check.
- End-to-end user flow check.
- Save compatibility/error checks.
- New complete season simulation check.

### Definition of done

A deterministic full-season run completes and verifies continuity across all implemented systems.

### Risks

This pass may uncover dependency bugs. Fix root state transitions, not test shortcuts.

### Next phase unlocked

Final regression/stability pass.

## Phase 28: Regression/stability pass

### Goal

Perform final stabilization across build, saves, navigation, live playback, UI readability, data consistency, and long-career regressions.

### Why this phase matters

After all major systems exist, the project needs a broad truth audit before calling the design complete.

### Current state

Stage 1-8 stabilization exists, but future phases will introduce new risk.

### Target state

All systems align with the master design, unsupported scope is documented, and no screen or save path claims false completeness.

### Scope

Classification: regression, infrastructure, UI-visible.

### Out of scope

- No new major features.
- No deep refactors unless required to fix bugs.

### Likely files/areas affected

- Tests/checks.
- Save validation.
- Docs/progress.
- Any bugfix areas found during audit.

### Dependencies

Phases 1-27.

### Implementation steps

1. Re-audit against `docs/touchline_master_design_decisions.md`.
2. Run full build and all Godot headless checks.
3. Run save migration/error checks across old and current save versions.
4. Run long-season and career movement scenarios.
5. Fix only bugs, misleading UI, broken saves, navigation failures, or contradictions.
6. Update docs and `AUTONOMOUS_PROGRESS.md` with final status.

### Verification

- `git diff --check`
- `dotnet build game/Touchline.sln`
- Full Godot headless suite.
- Save/load migration suite.
- Full season and long-career regression checks.

### Definition of done

The app can honestly be described as implementing the full master-design scope at a playable depth, with any intentional limitations documented.

### Risks

Final stabilization can turn into new feature work. Treat scope expansion as a roadmap update, not a bugfix.

### Next phase unlocked

Release or new roadmap planning beyond the current master design.

## 5. Dependency Graph

- Information visibility must come before deep scouting, transfers, contracts, youth, and UI information polish.
- Training and scouting controls must come before deeper promise, development, transfer, and match-preparation systems.
- Promise lifecycle depends on player identity, calendar, selection, contracts, morale, trust, and news.
- Tactical depth depends on player identity, information visibility, role fit, and training familiarity.
- Match engine depth depends on player identity, tactics, training, staff, fatigue, morale, opponent style, and tactical familiarity.
- Post-match report depth depends on match engine depth and consequence systems.
- Morale, trust, reputation, and pressure depth depends on match reports, promises, board/fan/DoF/staff/player state, and news.
- News/media/world events depend on pressure, promises, player relationships, staff/DoF conflict, and generated content templates.
- Transfer depth depends on scouting, contracts, Director of Football, finance, promises, role authority, and news.
- Contract depth depends on transfer interest, agent behavior, wage budgets, promises, and board approval.
- Director of Football conflict depends on transfer/contracts, news/events, board philosophy, and trust.
- Staff market depends on finance, role authority, and staff impact rules.
- Youth academy depends on generated players, player development, training, scouting, staff, and board/fan reactions.
- Player development and aging depends on training, match performance, injuries, youth, and player history.
- Finance depends on transfers, wages, contracts, league structure, cup prize money, ticket income, commercial income, and board philosophy.
- League structure must precede promotion/relegation, cups, rivalry schedules, prize money, job market depth, and full season simulation.
- Squad registration depends on transfers, contracts, youth promotion, league/cup rules, and wage budgets.
- Rivalries and derbies depend on league/cup fixtures, fan culture, media, and career history.
- Objectives/job security/sackings depend on league/cup outcomes, board philosophy, pressure, finance, derbies, and career history.
- Career job market depends on reputation, licenses, job security, sackings, club manager states, league reputation, and career history.
- Difficulty settings should come after major systems exist, so settings can tune real behavior rather than placeholder switches.
- Save history must expand alongside every persistent phase; final long-term memory depends on stable state shapes from major systems.
- Balance, UI polish, full-season simulation, and regression must happen after the systems they validate exist.

## 6. Recommended Execution Strategy

- Execute one phase per Codex run unless the phase is explicitly tiny.
- At the start of each run, read `docs/touchline_master_design_decisions.md`, `docs/PRD.md`, `docs/Architecture.md`, `docs/Guardrails.md`, `docs/Plan.md`, this roadmap, and `AUTONOMOUS_PROGRESS.md`.
- Inspect current code before editing; do not assume this roadmap still exactly matches implementation after later phases.
- Update `AUTONOMOUS_PROGRESS.md` before stopping and after each verified meaningful unit.
- Run the narrowest relevant checks during the phase and the strongest relevant checks before commit.
- Commit and push only after verification passes.
- If a phase discovers a dependency problem, fix the dependency or update this roadmap instead of hacking around it.
- Do not invent scope outside this roadmap. If new scope is necessary, update this roadmap first in a documentation-only commit.
- Keep domain rules in C# domain/application logic. Scenes should present state and request actions, not own simulation, transfer, scouting, calendar, permission, or consequence rules.
- Every exposed gameplay state must be save-backed before it is treated as a real system.
- Role authority, shared match timeline, partial information, and morale/trust/reputation/pressure separation are non-negotiable guardrails.

## 7. Verification Strategy

- Always run `git diff --check` before commit.
- Run `dotnet build game/Touchline.sln` for any product code change.
- Run Godot headless checks for the touched system and adjacent flows.
- For save shape changes, increment save version when needed, add migration, add malformed-save validation, and run save compatibility/error checks.
- For player/tactics/match changes, run squad/profile, tactics, match variation, shared engine, live renderer, matchday, and post-match checks.
- For calendar/training/scouting changes, run weekly loop, fixtures/calendar, save/load, and end-to-end flow checks.
- For transfer/contract/promise changes, run recruitment/contracts, promise lifecycle, news/consequence, role authority, and save/load checks.
- For career/job/security changes, run job market, pressure, objective, season rollover, and save/load checks.
- For UI changes, run navigation flow, UI consistency, layout/readability, and relevant scene checks.
- For generated data changes, run seed data and data consistency checks.
- After major phase groups, run full end-to-end and full-season regression checks.
- Before final completion, run the full Godot headless suite, long-career save/load, live playback, post-match, UI readability, and data consistency checks.

## 8. Roadmap Status Table

| Phase | Status | Last commit | Verification | Notes |
|---|---|---|---|---|
| Phase 1: Information visibility deepening | Complete | See git history | phase1_information_visibility_check.gd | Implemented in the 28-phase pass |
| Phase 2: Training and scouting controls | Complete | See git history | phase2_training_scouting_controls_check.gd | Implemented in the 28-phase pass |
| Phase 3: Promise lifecycle | Complete | See git history | phase3_promise_lifecycle_check.gd | Implemented in the 28-phase pass |
| Phase 4: Tactical depth and role fit | Complete | See git history | phase4_tactical_depth_check.gd | Implemented in the 28-phase pass |
| Phase 5: Match engine depth | Complete | See git history | phase5_match_engine_depth_check.gd | Implemented in the 28-phase pass |
| Phase 6: Post-match report depth | Complete | See git history | phase6_post_match_report_depth_check.gd | Implemented in the 28-phase pass |
| Phase 7: Morale, trust, reputation, pressure depth | Complete | See git history | phase7_perception_depth_check.gd | Implemented in the 28-phase pass |
| Phase 8: News/media/world events | Complete | See git history | phase8_news_decision_events_check.gd | Implemented in the 28-phase pass |
| Phase 9: Transfer market expansion | Complete | See git history | phase9_transfer_market_check.gd | Implemented in the 28-phase pass |
| Phase 10: Contract negotiation depth | Complete | See git history | phase10_contract_negotiation_check.gd | Implemented in the 28-phase pass |
| Phase 11: Director of Football conflict depth | Complete | See git history | phase11_director_conflict_check.gd | Implemented in the 28-phase pass |
| Phase 12: Staff impact and staff market | Complete | See git history | phase12_staff_market_check.gd | Implemented in the 28-phase pass |
| Phase 13: Youth academy | Complete | See git history | phase13_youth_academy_check.gd | Implemented in the 28-phase pass |
| Phase 14: Player development and aging depth | Complete | See git history | phase14_player_development_check.gd | Implemented in the 28-phase pass |
| Phase 15: Finance system | Complete | See git history | phase15_finance_check.gd | Implemented in the 28-phase pass |
| Phase 16: League structure and promotion/relegation | Complete | See git history | phase16_league_structure_check.gd | Implemented in the 28-phase pass |
| Phase 17: Cup competitions | Complete | See git history | phase17_cup_competition_check.gd | Implemented in the 28-phase pass |
| Phase 18: Squad registration rules | Complete | See git history | phase18_squad_registration_check.gd | Implemented in the 28-phase pass |
| Phase 19: Rivalries and derbies | Complete | See git history | phase19_rivalry_derby_check.gd | Implemented in the 28-phase pass |
| Phase 20: Objectives, job security, sackings depth | Complete | See git history | phase20_objectives_sacking_check.gd | Implemented in the 28-phase pass |
| Phase 21: Career job market and interviews | Complete | ed83668 | phase21_career_job_market_check.gd | Implemented in the 28-phase pass |
| Phase 22: Generated content and narrative variety | Complete | a2a5dfe | phase22_generated_content_check.gd | Implemented in the 28-phase pass |
| Phase 23: Difficulty and realism settings | Complete | 879e581 | phase23_difficulty_settings_check.gd | Implemented in the 28-phase pass |
| Phase 24: Save history and long-term career memory | Complete | 879e581 | phase24_career_memory_check.gd | Implemented in the 28-phase pass |
| Phase 25: Balance pass | Complete | 879e581 | phase25_balance_pass_check.gd | Implemented in the 28-phase pass |
| Phase 26: UI polish and readability pass | Complete | 879e581 | phase26_ui_readability_check.gd | Implemented in the 28-phase pass |
| Phase 27: End-to-end season simulation pass | Complete | 879e581 | phase27_end_to_end_season_check.gd | Implemented in the 28-phase pass |
| Phase 28: Regression/stability pass | Complete | 879e581 | phase28_final_stability_audit_check.gd | Final audit pass complete before acceptance audit |
