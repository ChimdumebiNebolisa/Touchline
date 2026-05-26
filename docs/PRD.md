# PRD

## 1. Source Of Truth

`docs/touchline_master_design_decisions.md` is the highest-level product design source of truth for Touchline.

This PRD summarizes that direction for implementation. If this PRD conflicts with `docs/touchline_master_design_decisions.md`, stop and reconcile the docs before changing product code.

## 2. Core Game Identity

Touchline is a fictional club-football career simulator built with Godot and C#.

The user starts a career at a chosen fictional club as an Assistant Manager, Head Coach, or Manager. The game focuses on tactical identity, partial player knowledge, licensing progression, staff influence, transfers, player personalities, board and fan pressure, dynamic development, and match outcomes shown through instant simulation or tactical live playback.

The tone is serious and realistic football world first, with believable football politics second. Drama should come from football logic: agent leaks, board pressure, Director of Football conflict, fan unrest, player frustration, transfer hijacks, media narratives, and interim-job uncertainty.

## 3. What The App Is

Touchline is:

- a desktop-first, single-player football management game
- a fictional club-football world with generated clubs, players, staff, leagues, and news
- a persistent career simulator with roles, licenses, reputation, pressure, and job movement
- a tactical football management game where decisions affect matches and club politics
- a partial-information game where the user never knows everything with certainty
- a local-first Godot/C# application unless the architecture is explicitly changed
- an incrementally built product where coherent playable slices are preferred over shallow stubs

## 4. What The App Is Not

Touchline is not:

- an arcade soccer game
- a playable on-pitch football controls game
- a pure physics match engine
- a real licensed team or real player database
- a generic dashboard app
- a backend-first or online-service product
- a game where players collapse into rating bundles only
- a game where every master-design system must be fully deep in the first implementation pass

## 5. Playable Roles

Touchline has three playable roles. Role selection is not cosmetic; it determines authority, information access, pressure, job risk, and career path.

- Assistant Manager: low direct authority, suggestion-based influence, lower blame, route toward interim opportunities.
- Head Coach: controls the football side, including tactics, training focus, lineup, and matchday decisions, but has limited recruitment and structural authority.
- Manager: broad football-project control across tactics, training, transfers, contracts, squad planning, staff recommendations, and media direction within board limits.

Core rule: more authority means more accountability.

## 6. Core Game Loop

Main menu -> new career or load -> choose role, background, license, and club -> club dashboard -> review news, squad, staff, objectives, pressure, tactics, training, scouting, and next fixture -> advance days or weeks -> handle decision events -> prepare matchday -> instant sim or live playback from the same simulated match timeline -> post-match report -> morale, trust, reputation, pressure, player form, tactical familiarity, news, objectives, and job security update -> repeat across seasons and career moves.

## 7. Main Systems

The target product includes these systems from `docs/touchline_master_design_decisions.md`:

- career profile, manager background, licenses, reputation, and career history
- Assistant Manager, Head Coach, and Manager role authority
- fictional clubs with archetype, board philosophy, fan culture, Director of Football style, staff, squad, objectives, budgets, wage data, academy quality, rivalries, and history
- fictional league structure, calendar, fixtures, cups, promotion/relegation, and season rollover
- players with identity, known/estimated/unknown attributes, playing style, tendencies, traits, personality, tactical fit, development curve, contracts, morale, form, fitness, relationship, promises, and transfer interest
- staff roles that affect information quality, reports, training, scouting, fitness, development, morale, media risk, and tactical analysis
- tactics with formation, team style, team instructions, player roles, player instructions, tactical familiarity, fit analysis, and risk analysis
- one shared match simulation model for Instant Sim and Live Match Playback
- post-match reports that explain stats, tactical causes, player ratings, fit notes, fatigue, morale, board reaction, fan reaction, media story, staff analysis, and development notes
- news, media, world events, decision events, and reliability labels
- morale, trust, reputation, and pressure as separate systems
- objectives, job security, sackings, board reviews, and job market progression
- scouting, transfer, contract, promise, staff market, youth academy, and finance foundations
- save/load and long-term career history

## 8. Incremental Scope Model

The full design is the target direction. Implementation must still happen in coherent slices.

Each implementation slice must:

- name the active Plan stage
- build the smallest playable system that advances that stage
- keep simulation and business rules in domain code, not scene scripts
- update save/load when persistent state changes
- verify the new behavior with the strongest relevant checks
- avoid UI-only claims for systems that do not affect state yet

## 9. Current Implementation Baseline

The current playable baseline is the completed 28-phase master-design implementation pass. It is a broad playable foundation, not a claim that every master-design system has full simulation depth.

The app should now provide a coherent local career loop with role, starting license, manager background, selected club, club archetype, board philosophy, fan culture, Director of Football style, staff, squad identity, partial information, tactics, tactical familiarity, weekly calendar/training/scouting, shared match simulation/live playback, post-match consequences, transfers/contracts/loans foundations, promises, youth academy, finance, media/events, job security, job-market movement, career memory, generated content, difficulty settings, and save/load coverage.

The completed pass should be honest about depth. Transfers/contracts, loans, youth, finance, scouting, media/events, job market, license progression, generated content, and career history are implemented as playable foundations with persistent effects, not as fully deep standalone simulations. UI and docs must not describe simplified foundations as complete negotiation, full global scouting, deep finance ledger, playable youth leagues, dialogue-heavy media, or a fully simulated global manager carousel.

## 10. Acceptance Criteria For The Reconciled Direction

The product direction is valid when:

- the master design and repo docs do not contradict each other
- the app remains Godot/C# and desktop-first
- the user can start or load a persistent career
- the selected role changes what the user can control
- club identity, board philosophy, fan culture, Director of Football, staff, objectives, and pressure are visible and state-backed
- players show identity plus partial information, not ratings only
- tactics affect the shared match simulation inputs
- Instant Sim and Live Match Playback consume the same match object
- post-match consequences update ongoing career state
- morale, trust, reputation, and pressure remain separate concepts
- unfinished systems are clearly marked as planned or foundation-only, not presented as complete
