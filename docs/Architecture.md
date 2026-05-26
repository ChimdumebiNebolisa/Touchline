# Architecture

## 1. Source Of Truth

`docs/touchline_master_design_decisions.md` is the highest-level product design source of truth. This architecture describes how the Godot/C# codebase should support that design.

If Architecture conflicts with `docs/touchline_master_design_decisions.md`, `docs/PRD.md`, `docs/Guardrails.md`, or `docs/Plan.md`, stop and reconcile the docs before changing product code.

## 2. Technical Direction

Touchline is implemented as a Godot .NET game with C# domain logic.

Core principles:

- scene-based UI and flow orchestration in Godot
- simulation and business rules in C# domain systems
- local-first single-player runtime
- autoload singletons for persistent runtime state and scene handoff
- authored or generated fictional data under `game/data`
- local save/load for career continuity
- no backend, external API, account system, or online dependency unless the source-of-truth docs change

## 3. Current Product Path

Primary product path:

- `game/project.godot`
- `game/Touchline.sln`
- `game/scenes`
- `game/scripts`
- `game/assets`
- `game/data`

Legacy or archived material is reference-only and not the active product path.

## 4. Runtime Layers

### 4.1 Presentation Layer

Godot scenes own user flow, navigation, input collection, and football-facing visualization.

Scenes may present state and request domain actions. They must not own core simulation, transfer, scouting, calendar, permission, or consequence rules.

Current scenes include:

- MainMenu
- CareerSetup
- ChooseClub
- ClubDashboard
- SquadScreen
- PlayerProfile
- TacticsScreen
- FixturesScreen
- StandingsScreen
- MatchdayScene
- LiveMatchScene
- PostMatchScene
- SaveLoadScene

### 4.2 Application State Layer

Autoloads own long-lived runtime state and scene handoff context.

Current or expected autoloads:

- GameState: current career state and scene-facing state queries
- SaveSystem: save slot operations and validation
- CalendarSystem: date and season progression
- WorldGenerator: fictional career/world bootstrap
- TouchlineTheme: shared presentation styling

As the master design is implemented, GameState should become a coordinator over clearer domain models rather than a container for every rule.

### 4.3 Domain Layer

Domain models and systems own deterministic game logic and explainable outcomes.

Rules belong here:

- role authority and permissions
- license eligibility and information quality
- club identity and objective generation
- player information visibility
- staff, scouting, training, transfers, contracts, and promises
- match simulation and post-match consequences
- morale, trust, reputation, and pressure changes
- job security, sackings, job offers, and career history

## 5. Conceptual Modules

The table marks architectural intent, not current implementation claims.

| Module | Responsibility | Current status |
|---|---|---|
| Career profile | Manager name, role, background, license, reputation, history, job offers, sackings, promises, trophies | Implemented as a playable foundation: role/background/license, reputation, job security, job movement, promises, sackings, trophy/history summaries, and structured career memory |
| Roles and licenses | Assistant Manager, Head Coach, Manager authority; license ladder and access/information effects | Implemented as a playable foundation: role restrictions, license effects, opportunity/course summaries, and job-market eligibility checks |
| Clubs | Club identity, archetype, board philosophy, fan culture, Director of Football, staff, objectives, budgets, squad, academy, rivals, history | Implemented as a playable foundation: identity, archetype, board/fan/Director/staff, objectives, budgets, squad, academy, rivalries, morale, pressure, and history |
| Leagues and calendar | Fictional league pyramid, fixtures, standings, season calendar, windows, cups, rollover | Implemented as a compact foundation: league tiers, fixtures, standings, cups, promotion/relegation summaries, weekly advance, season rollover, and shadow-league context |
| Players | Identity, ability, known/estimated/unknown attributes, style, traits, personality, fit, development, contract, morale, form, fitness, relationships | Implemented as a playable foundation: identity, partial information, style, traits, personality, fit, development/aging, contracts, morale, form, fitness, fatigue, injury risk, relationships, and transfer-interest text |
| Staff | Staff roles, ratings, loyalty, ambition, reports, training/scouting/media effects | Implemented as a playable foundation: staff roster, reports, quality effects, staff market, hiring authority, wages, loyalty, ambition, and finance impact |
| Tactics | Formation, team style, instructions, player roles, player instructions, tactical familiarity, fit/risk notes | Implemented as a playable foundation: formation, style, instructions, roles, set pieces, opponent preparation, familiarity, fit/risk notes, and match inputs |
| Match simulation | Shared stat-and-event-driven engine using ability, tactics, familiarity, morale, form, fitness, staff prep, opponent style | Implemented: shared result/timeline engine uses player, tactic, familiarity, morale, fitness, staff, difficulty, and opponent context |
| Live playback | Visual renderer for the same simulated match timeline used by Instant Sim | Implemented as playback: live scene renders the shared match result/timeline and does not create a second simulation path |
| Transfers/contracts | Scouting-based recruitment, interest, fees, wages, agents, promises, board approval, loans, renewals, integration | Implemented as a playable foundation: shortlist, recruitment targets, fee/wage ranges, agents, board/Director stances, rival pressure, loans, renewals, promises, history, and consequences |
| Scouting | Assignments, regions, report timing, confidence, partial discovery, scout accuracy, analyst support | Implemented as a playable foundation: assignment timing, report depth/quality, partial discovery, staff/license/difficulty effects, and recruitment information |
| Training | Weekly focus, tactical familiarity, development, fatigue, morale, injury risk | Implemented as a playable foundation: training focus/intensity affects familiarity, development, condition, morale, injury risk, scouting cadence, and weekly loop state |
| Youth academy | Academy quality, youth intake, generated prospects, promotion, loans, hidden potential, reactions | Implemented as a playable foundation: academy quality, intake, generated prospects, hidden potential bands, promotion, loan suitability, board/fan reactions, and history |
| Finance | Transfer budget, wage budget, debt, revenue, prize money, ticket income, commercial growth, financial rules | Implemented as a readable foundation: budget, wage bill, debt, revenue, expenses, prize/ticket/commercial income, financial pressure, board actions, and difficulty effects |
| News/media/world events | News categories, reliability labels, templates, media pressure, decision events, downstream effects | Implemented as a playable foundation: news feed, reliability/source labels, generated templates, decision events, cooldowns, media pressure, and persistent downstream effects |
| Morale/trust/reputation/pressure | Separate state systems for mood, belief, world view, and consequence risk | Implemented: morale, trust, reputation, and pressure remain separate and update through matches, events, promises, finance, and job security |
| Objectives/job security | Objectives by priority/type, board reviews, job security states, sackings | Implemented as a playable foundation: typed objectives, board reviews, warnings, ultimatums, sackings, pressure, and aftermath summaries |
| Career job market | Club manager states, offers, applications, interim routes, license-gated hiring, aftermath | Implemented as a playable foundation: offers, applications, interviews, interim/emergency paths, license/reputation gates, job moves, and history |
| Save/load | Complete persistent career state, versioning, migration, validation, history | Implemented for save version 26 with migration to current shape, malformed-save rejection, and persisted state for all 28-phase foundation systems |

## 6. Core Data Objects

The target domain should represent these conceptual objects from the master design:

- Player
- Club
- Staff
- Match
- NewsEvent
- CareerProfile
- Contract
- Tactic
- Objective
- Promise
- ScoutingReport
- TrainingPlan
- FinanceState
- JobOffer

Implementation may introduce these incrementally. Do not create empty shell objects unless a stage needs them for real state or verified behavior.

## 7. Match Simulation Contract

There is one shared match engine.

The match is simulated first into an authoritative match object containing:

- home and away clubs
- lineups and tactics
- pre-match context
- event timeline
- match stats
- player ratings or performance notes
- injuries/cards/goals when implemented
- tactical analysis
- morale/reputation/news outputs or inputs for consequence systems

Instant Sim resolves the authoritative match object immediately.

Live Match Playback visualizes the same match event timeline. It must not create an alternate result, alternate event sequence, or separate rules path.

## 8. State And Save Strategy

Any state that affects career continuity must be save-compatible before it is exposed as gameplay.

Save/load must eventually cover:

- career profile, role, background, license, reputation, history
- current club, role authority, objectives, board/fan/director/staff relationships
- players, contracts, promises, morale, form, fitness, partial information, development
- tactics, training, scouting, transfers, finance, news, decisions
- leagues, fixtures, standings, calendar, season state
- match history, post-match reports, job security, job market

Until all modules exist, save payloads should only claim support for implemented state and should reject malformed critical state explicitly.

## 9. Data Strategy

Touchline uses fictional content.

Data can be authored, generated, or both, but generated content must remain constrained by game state and the master design. Generated names, clubs, headlines, scout reports, and media text should use structured templates first.

Seed data should eventually include:

- clubs with archetype, board philosophy, fan culture, Director of Football style, rivals, budget, staff, objectives, and academy quality
- players with identity, attributes, partial-information state, style, traits, personality, contracts, and fit
- staff roles and ratings
- competitions, fixtures, calendars, and regions

## 10. Scene Flow Target

Target flow:

MainMenu -> CareerSetup -> ClubSelection -> ClubDashboard -> Squad/Profile/Tactics/Training/Scouting/Transfers/Fixtures/Standings/News -> Matchday -> InstantSim or LivePlayback -> PostMatchReport -> Dashboard -> CalendarAdvance -> repeat across seasons and career moves.

Stage implementations may expose only the screens needed by the active stage, but they must not contradict the target flow.

## 11. Build And Tooling

- Godot .NET project files are first-class.
- C# source lives under `game/scripts` unless the project is intentionally reorganized.
- Local save data uses Godot `user://` paths.
- Verification uses `dotnet build`, Godot headless checks, and focused route/domain checks.
- Web/npm workflows are not active product gates unless the architecture is explicitly changed.
