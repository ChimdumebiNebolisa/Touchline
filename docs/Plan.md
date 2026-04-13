# Plan

## 1. Status

### Active

- None

### Backlog

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

- No active Plan step. Update the source-of-truth docs before beginning any new product scope.

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
