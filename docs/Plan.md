# Plan

## 1. Status

### Active

- Step 51: Polish squad and player-profile management clarity

### Backlog

- Step 52: Polish tactics screen clarity
- Step 53: Polish fixtures and standings presentation
- Step 54: Full UI consistency and visual polish pass
- Step 55: Save/load and empty/error-state polish
- Step 56: Demo-ready build and release workflow
- Step 57: Final regression and manual QA checklist
- Step 58: Demo asset plan
- Step 59: Final product boundary and limitations pass
- Step 60: Rewrite README in final project format

### Blocked

- None

### Done

- Step 1: Main Menu
- Step 2: New Career
- Step 3: Choose Club
- Step 4: Club Dashboard
- Step 5: Squad Screen with named players
- Step 6: Tactics Screen
- Step 7: Matchday Scene
- Step 8: Live Match Scene with visible player movement
- Step 9: Post-Match Scene
- Step 10: Advance Date
- Step 11: Save Game
- Step 12: Establish the visual system
- Step 13: Rebuild main menu and shell framing
- Step 14: Redesign career setup and club selection
- Step 15: Rebuild the club dashboard as the real command center
- Step 16: Create the player profile scene
- Step 17: Rebuild squad management into a football workspace
- Step 18: Redesign tactics as a tactical board
- Step 19: Replace fixtures and standings placeholders with real competition surfaces
- Step 20: Redesign matchday into an event screen
- Step 21: Upgrade live match presentation substantially
- Step 22: Unify live and instant simulation around one shared engine
- Step 23: Rebuild post-match into a consequence and explainability screen
- Step 24: Refactor runtime architecture around clear services
- Step 25: Add the missing autoload systems from the architecture
- Step 26: Move hardcoded football content into real data
- Step 27: Integrate the save path with the authoritative domain model
- Step 28: Complete season continuity and progression
- Step 29: Expose board, fan, and perception context properly
- Step 30: Create a navigation and transition pass
- Step 31: Run a full polish and usability pass
- Step 32: Run a comprehensive management-shell UI/UX overhaul
- Step 33: Rewrite repository documentation and README
- Step 34: Introduce authoritative match playback frames
- Step 35: Polish live match renderer readability
- Step 36: Replace hardcoded away lineups with seeded opponent squads
- Step 37: Improve match action selection and tactical variation
- Step 38: Make post-match consequences use richer playback causes
- Step 39: Add explicit match action participant metadata
- Step 40: Add lightweight playback-derived match stats
- Step 41: Upgrade post-match report readability
- Step 42: Harden repeated matchday progression
- Step 43: Improve player condition/form/morale changes
- Step 44: Add multi-match regression checks
- Step 45: Harden season rollover
- Step 46: Add lightweight season-end player aging/development
- Step 47: Add full-season regression checks
- Step 48: Improve dashboard manager-facing context
- Step 49: Improve matchday preparation clarity
- Step 50: Add end-to-end user-flow regression coverage

## 2. Plan Rules

1. Only one active step at a time.
2. Each step must be verified before activating the next.
3. Implement only the smallest valid subtask for the active step.
4. Keep scope football-native and persistent-world oriented.
5. Presentation upgrades must not introduce fake football systems.
6. Scene scripts may present state and request actions, but may not become rules engines.
7. Every new UI surface must consume authoritative state instead of duplicating domain logic.
8. Every consequence shown to the player must remain explainable after the fact.
9. No step may bypass the one shared match engine requirement from the Architecture.
10. Do not activate deeper feature slices until the current shell and presentation slice is verified.

## 3. Step 12: Establish the visual system

### Objective

Create a shared Touchline presentation foundation so the Godot shell no longer renders as default prototype UI.

### Allowed Subtasks

- create shared theme resources for typography, spacing, colors, and primary controls
- establish reusable panel, card, table, and HUD treatments
- wire project-level or scene-level theme usage into the active game shell
- add shared visual helpers that improve consistency without adding football rules

### Verification

- primary scenes render through the shared theme rather than Godot defaults
- reusable styling primitives exist and can be applied without per-screen duplication
- dotnet build succeeds after theme integration

### Exit Criteria

- a shared visual system exists under `game/assets` or an equivalent product path
- at least the active shell scenes can consume the shared visual system
- future UI steps can build on the shared styling foundation instead of ad hoc scene styling

## 4. Step 13: Rebuild main menu and shell framing

### Objective

Make the game feel like a football title from first boot through resume and load entry points.

### Allowed Subtasks

- enrich the main menu with resume-state context and stronger entry hierarchy
- improve save-slot preview and continue language in shell entry points
- tighten shell copy, framing, and first-screen composition without changing product scope
- preserve the new shared visual system while improving first-boot clarity

### Verification

- main menu has clear Touchline identity and readable visual hierarchy
- menu supports polished new game, load, and exit flows
- shell framing matches the shared visual system from Step 12

## 5. Step 14: Redesign career setup and club selection

### Objective

Turn setup and club selection into a football-native onboarding flow with meaningful club identity context.

### Allowed Subtasks

- improve career setup copy and framing so it reads like football onboarding rather than generic form entry
- enrich club selection with club identity, expectation, and upcoming-context cues derived from available state
- preserve the selected-club handoff into the dashboard while making the decision feel more informed
- add lightweight presentation context without inventing unsupported football systems

### Verification

- career setup feels cohesive with the new shell
- club selection presents identity and decision context instead of a bare list
- selected club still persists into runtime state and the dashboard flow

## 6. Step 15: Rebuild the club dashboard as the real command center

### Objective

Make the dashboard the central football hub required by the PRD.

### Allowed Subtasks

- reorganize dashboard information into clearer command-center modules
- surface next fixture, form, pressure, and save context more clearly using existing state
- improve action hierarchy so the player can decide the next football task at a glance
- preserve navigation behavior while making the hub more informative and less placeholder-like

### Verification

- dashboard communicates next fixture, recent form, pressure, and squad readiness at a glance
- dashboard navigation remains the main day-to-day control surface
- save flow remains accessible and visually integrated

## 7. Step 16: Create the player profile scene

### Objective

Add the missing player profile scene so named players feel like persistent identities rather than list entries.

### Allowed Subtasks

- add a dedicated player profile scene and script under the Godot game path
- create the smallest valid scene handoff from squad management into player inspection
- present player identity, role, age, form, morale, fitness, and current squad status from existing state
- preserve navigation back into the squad flow without introducing duplicate business logic

### Verification

- squad screen can open a player profile
- player profile presents persistent player-specific context and trajectory
- state shown matches the active career and selected player

## 8. Step 17: Rebuild squad management into a football workspace

### Objective

Convert squad management from filtered list browsing into a real lineup and selection workspace.

### Allowed Subtasks

- add clearer squad-workspace structure and selection context
- allow explicit lineup-state changes that persist into upcoming match preparation
- connect squad inspection and player profile flow without duplicating player logic
- preserve football-native language and avoid placeholder management affordances

### Verification

- starters, bench, and role context are obvious and editable
- lineup changes persist into match preparation
- squad screen remains explainable and football-native

## 9. Step 18: Redesign tactics as a tactical board

### Objective

Turn tactics into an interpretable football setup surface rather than a bare control form.

### Allowed Subtasks

- restructure tactics into a clearer board-style workspace using the existing tactical inputs
- present formation and tactical sliders with football-native role language and immediate context
- preserve tactic persistence into match preparation without duplicating decision logic in the scene
- improve screen hierarchy so saved tactical choices read as a coherent match plan

### Verification

- tactical changes are visually understandable before save
- saved tactical state still persists into match preparation and simulation inputs
- screen presentation matches the shared visual system

## 10. Step 19: Replace fixtures and standings placeholders with real competition surfaces

### Objective

Make season context visible and credible through full fixtures and standings presentation.

### Allowed Subtasks

- replace the standings stub with a state-driven competition screen under the Godot path
- enrich fixtures with chronology, current-club context, and clearer season framing using existing state
- preserve navigation back to the dashboard and avoid inventing unsupported league systems
- keep competition presentation explainable from the active runtime state rather than duplicating season logic in scene scripts

### Verification

- fixtures screen shows match chronology and current club context
- standings screen is no longer a stub
- competition surfaces reflect persistent season state

## 11. Step 20: Redesign matchday into an event screen

### Objective

Make match entry feel consequential, readable, and football-native.

### Allowed Subtasks

- restructure matchday into a fuller event surface using existing lineup, tactics, pressure, and competition state
- present opponent, kickoff context, squad readiness, and tactical intent before launch
- preserve the live-match launch path and back navigation without inventing unsupported pre-match systems
- keep the screen explainable from authoritative runtime state rather than scene-local football rules

### Verification

- matchday clearly presents opponent, competition, lineup, tactics, and pressure context
- launch flow into live match remains intact
- scene feels like an event screen rather than a placeholder

## 12. Step 21: Upgrade live match presentation substantially

### Objective

Elevate the 2D live match renderer into a readable, dramatic football broadcast surface.

### Allowed Subtasks

- improve the live-match HUD hierarchy, status messaging, and event readability around the existing playback
- add clearer presentation of momentum, score, clock, and tactical context without changing the underlying match rules
- preserve marker movement, playback timing, and post-match handoff while upgrading the visual shell
- keep live presentation as a renderer/controller of simulation playback rather than a second rules engine

### Verification

- pitch, HUD, event feed, and motion hierarchy are materially improved
- live match remains readable across the full 90-minute playback
- visual upgrades do not move simulation rules into the scene layer

## 13. Step 22: Unify live and instant simulation around one shared engine

### Objective

Satisfy the Architecture requirement that instant and live modes share one match engine.

### Allowed Subtasks

- introduce one authoritative match result model that both live and instant paths consume
- move match-outcome generation behind a shared domain entry point rather than scene-specific creation
- preserve live-match playback presentation while allowing an instant-result path to resolve through the same engine
- keep downstream post-match consequences driven by shared match outputs instead of duplicated score logic

### Verification

- live and instant match flows consume one authoritative match result model
- duplicated match rules are removed or clearly retired
- downstream post-match consequences use shared match outputs

## 14. Step 23: Rebuild post-match into a consequence and explainability screen

### Objective

Make post-match the point where result, causes, and downstream club effects are all legible.

### Allowed Subtasks

- redesign post-match around result summary, key moments, and consequence explanation using the shared match result output
- surface table movement, club pressure shifts, and tactical/context notes without inventing unsupported systems
- preserve continue flow into the season timeline while making downstream effects readable
- keep explanation text grounded in authoritative match and career state rather than scene-local heuristics

### Verification

- result, key moments, and consequence deltas are presented clearly
- board, fan, morale, and related context remain explainable
- continue flow preserves persistent state correctly

## 15. Step 24: Refactor runtime architecture around clear services

### Objective

Reduce monolithic state handling and align runtime responsibilities with the Architecture.

### Allowed Subtasks

- extract competition, match-resolution, or other domain responsibilities into clearer service classes under the Godot product path
- keep `GameState` focused on long-lived career state and scene handoff instead of owning every rule path directly
- preserve save/load and scene behavior while moving rule-heavy logic into reusable services
- avoid introducing duplicate state owners while the refactor is underway

### Verification

- scene-facing state responsibilities are split more cleanly
- football logic is moved away from presentation-oriented runtime objects where appropriate
- build and save/load flows still function after refactor

## 16. Step 25: Add the missing autoload systems from the architecture

### Objective

Introduce the missing dedicated runtime systems required by the Architecture.

### Verification

- `CalendarSystem` and `WorldGenerator` or their approved equivalents exist as first-class runtime systems
- date progression and world bootstrapping are no longer overloaded inside one object
- scene handoff context remains stable

## 17. Step 26: Move hardcoded football content into real data

### Objective

Replace embedded sample state with authored or generated game data.

### Verification

- clubs, named players, competitions, and season seed content load from product data paths
- visible football identity no longer depends on hardcoded scene-state literals
- content loading failures are surfaced explicitly

## 18. Step 27: Integrate the save path with the authoritative domain model

### Objective

Make persistence reflect the actual authoritative game state instead of a reduced parallel structure.

### Verification

- save payload covers career-critical state completely
- load validation rejects malformed or incomplete critical state explicitly
- resumed careers preserve shell, match, and season context correctly

## 19. Step 28: Complete season continuity and progression

### Objective

Make the persistent football world credible across weeks, matches, and season rollover.

### Verification

- matchdays, standings, and date progression stay consistent over multiple cycles
- season rollover preserves continuity and updates the world correctly
- player and club state evolution remains persistent and explainable

## 20. Step 29: Expose board, fan, and perception context properly

### Objective

Surface pressure systems so consequences are visible before and after key events.

### Verification

- board and fan context are visible from the main shell
- post-match perception shifts are presented with reason summaries
- pressure is not hidden behind one-line summaries alone

## 21. Step 30: Create a navigation and transition pass

### Objective

Make the full app feel cohesive instead of a set of separate prototype screens.

### Verification

- scene transitions are consistent and intentional
- navigation paths avoid dead ends and confusing back behavior
- save, resume, and post-match return flows remain coherent

## 22. Step 31: Run a full polish and usability pass

### Objective

Remove remaining prototype edges and harden the shell for repeated play.

### Verification

- primary screens meet the shared visual quality bar
- layout, copy, focus behavior, and readability are consistent across the shell
- strongest available automated checks pass and manual Godot regression coverage is documented

## 23. Step 32: Run a comprehensive management-shell UI/UX overhaul

### Objective

Transform the current Godot shell from a centered prototype-card presentation into a desktop-first football operations interface without changing the underlying management flow.

### Allowed Subtasks

- introduce reusable shell primitives for page framing, section hierarchy, summary stats, chips, action groups, and structured data surfaces
- replace centered-card defaults on management screens with a wider app-shell layout that uses desktop width intentionally
- rebuild the dashboard first, then tactics, standings, fixtures, squad, club selection, career setup, and main menu in that order unless a dependency requires a different sequence
- reduce explanatory copy and replace raw text dumps with football-native tables, rows, widgets, and status treatments derived from existing authoritative state
- keep navigation, save/load, matchday launch, and player-profile handoffs intact while presentation changes are applied

### Verification

- management screens no longer default to a narrow centered-card composition
- dashboard, tactics, standings, fixtures, and squad read as football-native control surfaces rather than debug or placeholder views
- the redesign uses reusable visual primitives instead of screen-specific styling duplication
- strongest available checks pass for the touched areas, including `dotnet build game/Touchline.sln` and the relevant headless Godot route checks; manual shell walkthrough updates are documented if automation cannot cover the visual regression directly

### Exit Criteria

- post-menu screens use a coherent desktop-first shell with clear hierarchy and restrained football-native styling
- dashboard acts as the clear club command center required by the PRD
- standings, fixtures, tactics, and squad present structured football information rather than plain text blocks
- copy across the shell is materially tighter and easier to scan

## 24. Immediate Next Subtask

- Active Step 51: polish squad and player-profile management clarity without adding transfers, training, injuries, or new player systems.

## 25. Step 33: Rewrite repository documentation and README

### Objective

Replace the thin transitional repository README with a proper project-facing guide that matches the current Godot football-management game, repository structure, and verification workflow.

### Allowed Subtasks

- rewrite `README.md` around the actual product identity, current feature set, repository layout, setup requirements, run instructions, and verification workflow
- remove stale web-prototype guidance that no longer reflects the active product path
- document the source-of-truth docs and repo operating model clearly for contributors without changing product scope
- keep the README grounded in the shipped Godot/C# architecture and current repo tooling rather than aspirational or unsupported workflows

### Verification

- README instructions match the active Godot/C# product path described in `docs/PRD.md` and `docs/Architecture.md`
- README verification commands do not advertise stale or unsupported web-stack checks as the primary workflow
- `docs/PRD.md`, `docs/Architecture.md`, `docs/Guardrails.md`, `docs/Plan.md`, and `README.md` remain internally consistent after the rewrite

### Exit Criteria

- the repo has a clear, accurate top-level README for players, developers, and contributors
- setup and run instructions reflect the real local workflow
- documentation no longer implies the legacy web prototype is the active product

## 26. Step 34: Introduce authoritative match playback frames

### Objective

Replace the decorative match marker model with an authoritative frame-based playback contract that can drive both live rendering and instant resolution from one shared C# match engine.

### Allowed Subtasks

- add C# domain models for match playback, timeline frames, ball state, player agent state, actions, events, and tactical shape
- refactor `MatchSimulator` so it emits deterministic renderable football state instead of marker swing data
- generate simple possession phases with pass, carry, shot, save, clearance, interception, goal, reset, and kickoff actions
- keep `LiveMatchScene` as a light renderer of engine output, with only minimal compatibility changes needed to keep the app working
- preserve instant-result, post-match, save/load, fixtures, standings, and dashboard flows

### Verification

- `dotnet build game/Touchline.sln`
- relevant headless Godot route checks still compatible with the current scene tree
- playback output contains frames with ball state, possession, all 22 player states, action labels, and events tied to actions or frame ranges

### Exit Criteria

- live and instant match paths consume the same `MatchPlaybackResult`
- frame data contains enough football state for future live-renderer interpolation
- the old marker swing/sway model is no longer the authoritative movement source
- no unrelated screen rebuild or advanced physics scope is introduced

## 27. Step 35: Polish live match renderer readability

### Objective

Make the frame-based live match playback readable as football action by clarifying ball movement, ball carrier, possession, current action, event emphasis, and full-time handoff without changing the match engine.

### Allowed Subtasks

- improve ball contrast, carrier highlighting, action-line/trail presentation, and marker affordances from `MatchFrame` data
- make the live status area clearly show current action, possession, ball movement state, and carrier from the playback model
- visually emphasize active event summaries while keeping the feed synchronized to `MatchPlaybackResult.EventFeed`
- preserve post-match handoff and shared live/instant engine behavior

### Verification

- `dotnet build game/Touchline.sln`
- existing headless Godot route checks for match playback, shared engine, post-match, and navigation still pass
- optional renderer check confirms the live scene exposes the polished playback affordances

### Exit Criteria

- the ball and ball carrier are easy to identify during playback
- pass, carry, shot, save, clearance, interception, and goal moments read from engine frames instead of scene-generated events
- full-time status and post-match continuation remain clear
- no match rules are moved into `LiveMatchScene`

## 28. Step 36: Replace hardcoded away lineups with seeded opponent squads

### Objective

Make opponent XIs come from authoritative world club data instead of hardcoded simulator names.

### Allowed Subtasks

- expose deterministic club squad lookup from seeded world data
- keep the selected club lineup sourced from current squad/starters
- generate explicit deterministic fallback squads only when seed data is unavailable or incomplete
- preserve 22-player playback frames and shared live/instant simulation

### Verification

- opponent playback players are sourced from the resolved opponent club squad
- the old hardcoded away XI is no longer authoritative
- same seed and opponent produce stable opponent names and ids

### Exit Criteria

- `MatchSimulator` no longer owns hardcoded away-player content
- every available club can resolve a stable match squad
- save/load and career flow continue to work without storing duplicate squad blobs

## 29. Step 37: Improve match action selection and tactical variation

### Objective

Make the frame-based match engine produce less formulaic action chains while staying deterministic and lightweight.

### Allowed Subtasks

- vary possession phases by press, tempo, width, risk, and role selection
- add controlled variation in pass lanes, attacking side, shot/save/clearance/interception outcomes, and pressure turnovers
- keep the existing simple action set and authoritative playback contract
- avoid complex AI, advanced ratings, physics, or playable controls

### Verification

- playback includes varied action kinds and pass lanes across a match
- different tactical inputs produce different deterministic action signatures
- live and instant paths still consume the same playback result

### Exit Criteria

- match actions respond visibly to tactical inputs
- player roles influence passer/carrier/shooter/keeper/defender selection
- playback still contains valid ball/player/action/event state

## 30. Step 38: Make post-match consequences use richer playback causes

### Objective

Use the authoritative playback timeline to explain post-match morale, fan, and board consequences beyond final scoreline alone.

### Allowed Subtasks

- evaluate simple playback signals such as shots, late goals, saves, clearances, interceptions, comeback/collapse, press, and risk
- keep consequence deltas bounded, simple, and explainable
- store cause text in the match report and save payload safely
- preserve `PostMatchScene` as a renderer of `LastMatchReport`

### Verification

- post-match report includes cause reasoning beyond scoreline
- consequence deltas remain simple and explainable
- old save payloads without cause text restore with a safe default

### Exit Criteria

- `GameState.ApplyMatchResult` uses playback-cause analysis
- post-match and dashboard consequence copy can explain why deltas changed
- no large analytics/xG system is introduced

## 31. Step 39: Add explicit match action participant metadata

### Objective

Expose who participated in each authoritative match action so renderers and reports do not infer involvement from labels.

### Allowed Subtasks

- add optional participant ids for passers, receivers, carriers, shooters, keepers, defenders, interceptors, clearers, and scorers
- populate participant metadata during match action generation
- validate major action kinds have sensible participants when applicable
- keep legacy action positioning fields available for playback compatibility

### Verification

- pass, shot, save, clearance, interception, and goal actions include expected participant metadata
- playback frames and events remain valid
- no participant decision logic is added to scenes

### Exit Criteria

- `MatchAction` exposes explicit participant metadata
- match simulator populates participants deterministically from selected runtime players
- live and instant match paths still share the same result

## 32. Step 40: Add lightweight playback-derived match stats

### Objective

Attach deterministic match stats to playback results by deriving them from authoritative actions.

### Allowed Subtasks

- count shots, goals, saves, clearances, interceptions, possession phases, and completed passes from `MatchTimeline.Actions`
- attach the stats model to `MatchPlaybackResult`
- validate stats are internally consistent with the action list and final score
- avoid invented stats, xG, or scene-side stat computation

### Verification

- stat totals match playback actions
- final score agrees with goal stats
- post-match systems consume stats from playback rather than recomputing in UI

### Exit Criteria

- `MatchPlaybackResult` includes a `MatchStats` summary
- stats remain deterministic for the same seed, fixture, and tactics
- no fake report numbers are introduced

## 33. Step 41: Upgrade post-match report readability

### Objective

Make the post-match report explain how the match unfolded using concrete stats, causes, and key participant moments.

### Allowed Subtasks

- extend `LastMatchReport` with stats summary, tactical explanation, and key player moments
- derive report copy from playback stats/actions in domain services
- render the richer report data in `PostMatchScene`
- keep save/load compatible with older reports missing the new fields

### Verification

- post-match report displays final score, cause summary, stat comparison, key moments, and consequence deltas
- old saves restore with safe report defaults
- live, instant, post-match, and navigation checks still pass

### Exit Criteria

- manager-facing report copy references concrete playback causes
- `PostMatchScene` remains a renderer of `LastMatchReport`
- no complex analytics model or unrelated UI rebuild is added

## 34. Step 42: Harden repeated matchday progression

### Objective

Keep the career loop stable across repeated matchdays so fixtures, standings, reports, form, and next-match context update exactly once per resolved match.

### Allowed Subtasks

- guard against replaying an already completed current fixture
- ensure match resolution updates completed fixtures, standings, recent form, report state, and dashboard summaries
- preserve instant and live result paths through the same career-state application
- keep end-of-fixture-list behavior safe through the existing season rollover path

### Verification

- resolving one match completes the current round and advances the selected club table row once
- reapplying the same completed result does not duplicate table, fixture, or form effects
- post-match calendar advance moves to the next valid open match context

### Exit Criteria

- the current fixture cannot duplicate career effects after completion
- next opponent and fixture summary refresh after post-match advance
- no season rollover rewrite or unrelated UI rebuild is introduced

## 35. Step 43: Improve player condition/form/morale changes

### Objective

Apply simple, explainable player-state changes after matches using the authoritative playback and selected squad state.

### Allowed Subtasks

- reduce starting player fitness after match participation
- keep non-starter fitness loss minimal or absent
- reward scorers, keepers with saves, and defensive contributors with small form/morale gains
- apply modest team-result morale effects and clamp all values safely

### Verification

- starter fitness decreases after a resolved match
- player form, morale, and fitness remain inside valid bounds
- at least one player state changes after match resolution

### Exit Criteria

- post-match player changes are deterministic and lightweight
- save/load preserves changed squad condition
- no injuries, training, contracts, transfers, or complex development model is added

## 36. Step 44: Add multi-match regression checks

### Objective

Add automated coverage for multiple matchdays so save/load, calendar, fixtures, standings, reports, and player condition remain continuous.

### Allowed Subtasks

- add a progression check for one resolved match and post-match calendar advance
- add a player condition check for post-match squad state changes
- add a multi-match save/load regression check covering two resolved matches
- keep checks focused on existing career-loop behavior

### Verification

- multi-match checks pass alongside existing match-engine, live, post-match, and navigation checks
- save/load preserves date, matchday, table, fixtures, squad player state, and latest report after multiple matches
- resolving multiple matches does not duplicate the same result

### Exit Criteria

- repeated career-loop automation covers the current hardened path
- failures report explicit continuity issues
- no product scope is added beyond the hardening pass

## 37. Step 45: Harden season rollover

### Objective

Make season rollover safe and explicit once the current fixture list has been completed.

### Allowed Subtasks

- detect whether every fixture in the current season is complete before allowing rollover
- increment season year and reset matchday to the first new-season matchday
- regenerate fixtures and reset standings for the new season
- preserve selected club, squad, manager, pressure context, and career identity
- clear season-specific match report state before the new campaign opens

### Verification

- completing the fixture list and advancing the calendar rolls into the next season
- season label, date, matchday, fixtures, standings, next opponent, and report state are reset correctly
- attempting rollover before fixture completion fails explicitly instead of silently skipping the season

### Exit Criteria

- dashboard, fixtures, and standings receive coherent new-season state
- selected club and squad persist through rollover
- no promotion, relegation, transfers, contracts, finances, or deep league systems are introduced

## 38. Step 46: Add lightweight season-end player aging/development

### Objective

Apply deterministic season-end player aging and small development changes during rollover.

### Allowed Subtasks

- age every squad player by one year during season rollover
- give younger players small form/morale/fitness upside where deterministic rules allow
- apply small form/fitness decline pressure to older players
- clamp form, morale, and fitness safely

### Verification

- every squad player ages by one year at rollover
- at least one player value changes across the season-end development pass
- all player values remain inside valid bounds after rollover and save/load

### Exit Criteria

- season-end development remains lightweight and explainable
- changes are deterministic from seed, season, and player identity
- no injuries, potential ratings, retirements, training plans, contracts, wages, or transfers are added

## 39. Step 47: Add full-season regression checks

### Objective

Cover long-run career continuity by simulating a full season into rollover and through save/load.

### Allowed Subtasks

- add a season rollover contract check
- add a season development contract check
- add a full-season save/load regression check
- preserve existing match-engine, post-match, progression, and navigation checks

### Verification

- all fixtures can be completed without duplicate application
- season rollover resets fixtures and standings while preserving selected club and squad
- save/load after rollover preserves season year, date, matchday, club, squad, fixtures, standings, and next opponent context

### Exit Criteria

- full-season regression automation passes with existing route checks
- failures identify the broken long-run continuity assumption directly
- no unrelated product scope is added

## 40. Step 48: Improve dashboard manager-facing context

### Objective

Make the dashboard explain the current career state clearly from authoritative runtime state.

### Allowed Subtasks

- surface season, date, matchday, next fixture, league position, recent form, pressure, squad condition, tactics, and latest report context
- expose small formatted summaries from `GameState` where scene copy needs shared authoritative state
- keep the dashboard as a renderer and navigation hub rather than a domain-rule owner

### Verification

- dashboard context check confirms season/date, next fixture, pressure, lineup, tactics, table, and status context render
- existing navigation and career-flow checks still pass

### Exit Criteria

- dashboard clearly communicates whether the club is entering a new season, ready for matchday, post-match, or between matches
- no table rules, match rules, or consequence rules are moved into the scene

## 41. Step 49: Improve matchday preparation clarity

### Objective

Make matchday read as a clear pre-match decision screen before live or instant resolution.

### Allowed Subtasks

- show opponent, competition, season, date, lineup readiness, tactical setup, pressure, form, and seeded opponent context
- clarify that live match and instant result share the same engine and produce the same career-state handoff
- warn clearly when a fixture is already recorded or unavailable

### Verification

- matchday preparation check confirms opponent, tactics, lineup, pressure, and action-choice context render
- live and instant result checks still pass through the shared engine

### Exit Criteria

- matchday presents manager preparation context without adding scouting, advice, or football decision logic to the scene
- no match engine or unrelated screen rewrite is introduced

## 42. Step 50: Add end-to-end user-flow regression coverage

### Objective

Cover the manager-facing route from career setup through dashboard, matchday, post-match, dashboard return, and save/load context.

### Allowed Subtasks

- add dashboard context regression coverage
- add matchday preparation regression coverage
- add an end-to-end flow check that verifies post-match continue and save/load preserve manager-facing context
- keep existing match, report, season, save/load, and navigation checks compatible

### Verification

- new Step 48, 49, and 50 checks pass alongside compatible existing checks from Step 22 through Step 47
- save/load after the flow preserves manager identity, next opponent, and dashboard context

### Exit Criteria

- end-to-end route automation covers the polished manager-facing flow
- no unrelated product scope is added

## 43. Step 51: Polish squad and player-profile management clarity

### Objective

Make the squad and player profile screens feel like a real manager workspace while keeping lineup and player-state rules in domain/runtime state.

### Allowed Subtasks

- present starters, non-starters, bench, and reserve depth clearly from existing squad state
- improve condition, form, morale, fitness, role, position, and lineup-status readability
- surface post-match player-state visibility when a latest match report exists
- tighten profile summary copy around identity, role, age, condition, and lineup status
- add focused headless coverage for squad/profile clarity and lineup action continuity

### Verification

- `dotnet build game/Touchline.sln`
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step5_squad_named_players_check.gd`
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step30_navigation_flow_check.gd`
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step43_player_condition_check.gd`
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step50_end_to_end_user_flow_check.gd`
- new `step51_squad_profile_check.gd`

### Exit Criteria

- squad screen clearly shows starters and non-starters
- player profile clearly shows identity, role, age, form, morale, fitness, and lineup status
- lineup actions still work
- no business rules move into UI scenes
- no transfer, training, injury, contract, or player-system expansion is introduced

## 44. Step 52: Polish tactics screen clarity

### Objective

Make tactics readable and visibly connected to shared match behavior without adding a tactical advice engine.

### Allowed Subtasks

- show formation, press, tempo, width, and risk values clearly in the tactical board
- add simple interpretation copy for press line, ball speed, pitch use, and risk commitment
- clarify unsaved preview, saved plan, and reset-to-saved behavior
- reuse existing tactical summary wording where it helps dashboard or matchday consistency
- add focused headless coverage that saved tactics still affect shared match simulation

### Verification

- `dotnet build game/Touchline.sln`
- Step 6 tactics persistence check
- Step 22 shared engine check
- Step 37 match variation check
- Step 49 matchday preparation check
- new `step52_tactics_context_check.gd`

### Exit Criteria

- all tactical values are visible and understandable
- saved tactics persist and still feed the shared match engine
- UI explains broad meaning but does not compute match rules
- no tactical advice, scouting, or AI assistant system is added

## 45. Step 53: Polish fixtures and standings presentation

### Objective

Make season context easier to read through clearer fixtures, standings, current matchday, and rollover presentation.

### Allowed Subtasks

- distinguish completed, next, and upcoming fixtures from existing competition state
- show completed scorelines only for completed fixtures
- highlight selected club context in fixtures and standings
- clarify current season, matchday, date, next fixture, and table state
- show coherent new-season state after existing rollover logic runs

### Verification

- `dotnet build game/Touchline.sln`
- Step 28 season rollover check
- Step 30 navigation flow check
- Step 45 season rollover check
- Step 47 full-season regression check
- new `step53_competition_surfaces_check.gd`

### Exit Criteria

- fixtures screen clearly distinguishes completed and upcoming fixtures
- standings screen clearly shows table columns and selected club context
- rollover state is coherent
- no new competition formats, cups, promotion, or relegation are added

## 46. Step 54: Full UI consistency and visual polish pass

### Objective

Make all primary screens feel like one cohesive product through consistent layout, spacing, copy, buttons, chips, and navigation.

### Allowed Subtasks

- normalize section titles, chips, button language, and navigation labels across primary scenes
- reduce debug-looking or prototype-looking normal-state copy
- keep explicit unavailable/error states clear
- use existing `TouchlineTheme` and current scene structure instead of large rewrites
- add focused headless coverage for route-level consistency

### Verification

- `dotnet build game/Touchline.sln`
- Step 30 navigation flow check
- Step 35 live renderer check
- Step 48 dashboard context check
- Step 49 matchday preparation check
- Step 50 end-to-end user-flow check
- new `step54_ui_consistency_check.gd`

### Exit Criteria

- primary screens use consistent layout and copy conventions
- navigation labels are clear
- obvious prototype or debug wording is removed from populated screens
- existing flow checks still pass

## 47. Step 55: Save/load and empty/error-state polish

### Objective

Make the app safer and clearer when state is missing, unavailable, incomplete, or loaded from older saves.

### Allowed Subtasks

- strengthen save-slot preview clarity in main menu and load screen
- improve load failure messaging for missing, corrupt, future-version, and incomplete saves
- validate critical current-save fields before restore
- preserve old-save compatibility through the existing migration path
- clarify unavailable fixture and completed-season states without enabling replay

### Verification

- `dotnet build game/Touchline.sln`
- Step 27 save compatibility check
- Step 30 navigation flow check
- Step 42 matchday progression check
- Step 47 full-season regression check
- Step 50 end-to-end user-flow check
- new `step55_save_error_state_check.gd`

### Exit Criteria

- save/load screens clearly communicate slot state
- old, missing, corrupt, or incomplete data does not crash core flows
- fallback states are explicit and honest
- no silent failures or fake successful loads are introduced

## 48. Step 56: Demo-ready build and release workflow

### Objective

Make the project easier to run, verify, and package for demo review without overbuilt release infrastructure.

### Allowed Subtasks

- document Godot GUI and console/headless run commands
- document .NET build, Godot solution build, and grouped headless verification commands
- document practical Windows export guidance through Godot
- add a small project-settings release workflow check if useful
- avoid treating stale npm or web commands as the active product path

### Verification

- `dotnet build game/Touchline.sln`
- Godot headless `--build-solutions --quit`
- optional `step56_release_workflow_check.gd`
- manual review of release workflow docs

### Exit Criteria

- a developer can run the Godot project
- a developer can run build checks
- a developer can understand how to produce a demo executable
- no CI or packaging infrastructure is added beyond the practical checklist

## 49. Step 57: Final regression and manual QA checklist

### Objective

Create a final proof pass that verifies the app is demo-safe.

### Allowed Subtasks

- group all automated checks by build, engine, route, save/load, progression, rollover, player state, and final flow
- document manual QA for new career, load/resume, live match, instant result, post-match, multi-match progression, season rollover, save/load after rollover, and visual smoke
- record known limitations honestly
- add a small final smoke check only if it complements existing coverage

### Verification

- full automated suite from Step 22 onward
- `dotnet build game/Touchline.sln`
- Godot build-solutions
- manual QA checklist review

### Exit Criteria

- final regression list exists
- automated checks are grouped clearly
- manual demo checklist is clear
- known limitations are honest

## 50. Step 58: Demo asset plan

### Objective

Plan the screenshots and short demo video proof needed to make the project credible.

### Allowed Subtasks

- document screenshots for main menu, dashboard, squad, tactics, fixtures, standings, matchday, live match, post-match, and save/load
- define a short demo video structure covering the real playable loop
- state what the demo proves from existing app behavior
- define where final assets should be referenced from the README
- use placeholders only when assets are not yet captured

### Verification

- manual review of demo asset plan
- ensure planned assets map to actual Godot scenes and supported features

### Exit Criteria

- demo proof plan is specific
- screenshots and video list map to actual app features
- no fake claims or unsupported features are planned

## 51. Step 59: Final product boundary and limitations pass

### Objective

Prevent scope creep and make the project boundary honest across source-of-truth docs.

### Allowed Subtasks

- define what Touchline is as a local-first single-player Godot/C# football management v1
- define what Touchline is not, including backend, multiplayer, playable controls, 3D, transfers, contracts, wages, finances, scouting, injuries, promotion/relegation, youth academy, deep training, xG, multi-competition calendar, licensed teams, and external APIs
- document supported flows, known limitations, and technical boundaries
- clarify legacy web/TypeScript artifacts as reference-only
- keep PRD, Architecture, Guardrails, and Plan consistent

### Verification

- doc consistency review across source-of-truth docs
- stale active-path claim scan with `rg`
- core build or route smoke if any code is touched

### Exit Criteria

- docs clearly state current product boundary
- unsupported systems are not implied
- legacy web code is not presented as active
- final scope is demo-ready and honest

## 52. Step 60: Rewrite README in final project format

### Objective

Rewrite `README.md` into the final project format exactly, with no extra sections.

### Required README Structure

- `# Project Name`
- one-line description
- `## Problem It Solves`
- `## Demo`
- `## Features`
- `## Tech Stack`
- `## Architecture`
- `## Setup`
- `## How to Use`
- `## Key Technical Decisions`
- `## Limitations`
- `## License`
- `MIT License`

### Allowed Subtasks

- replace the README with exactly the required section list
- mention Godot + C# as the active product path
- mention local-first single-player
- include setup and verification commands
- include an architecture diagram in text or Mermaid
- mention legacy web code only inside an allowed section and only as archived/reference material
- mark demo placeholders clearly if final assets are not present

### Verification

- README heading-order review
- stale unsupported-claim scan
- `dotnet build game/Touchline.sln`
- representative final headless regression suite

### Exit Criteria

- README follows exactly the requested section list
- README is accurate to the current app
- README helps someone run, understand, and evaluate the project
- no extra sections are added
