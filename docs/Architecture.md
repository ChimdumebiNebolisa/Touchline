# Architecture

## 1. Facts driving structure

The source docs support the same structural conclusions repeatedly: split-responsibility club hierarchy, one shared match engine for instant and live simulation, morale and perception as persistent force multipliers, transfer uncertainty under sporting-director control, and academy pathways driven by club identity, loans, and rare meaningful youth outcomes rather than wonderkid spam. The architecture therefore has to preserve constrained agency, keep deep simulation limited to the shipped country’s top two divisions, and avoid backend-first or service-heavy design.   

## 2. Implementation choice

Use a single-process, deterministic simulation core with an event log.

Do not:

* build services
* split instant and live match logic
* start with a backend
* let UI screens invent rules outside sim-core

Build shape:

* monorepo
* local-first
* one reusable simulation core library
* one client app that renders and drives the core  

## 3. Major system parts

### 3.1 World Simulator

Owns:

* countries
* competitions
* calendars
* fixtures
* standings
* promotion and relegation
* AI club seasonal cycles
* shadow leagues

Inputs:

* content data
* current save state

Outputs:

* fixtures
* league context
* club objectives
* world events
* shadow-league results and tables 

### 3.2 Club State Engine

Owns:

* club finances
* board profile
* club identity
* squad state
* injuries
* fatigue
* promises
* morale
* reputation context
* academy profile

Inputs:

* match results
* transfer outcomes
* media events
* board events
* world-state changes

Outputs:

* updated club state
* actionable pressures
* squad availability
* budget state
* promise timers and unrest flags 

### 3.3 Match Engine

Owns:

* chance generation
* xG resolution
* tactics interaction
* fatigue updates
* cards
* injuries
* substitutions
* match event log

Inputs:

* two squads
* tactics
* player condition
* morale modifiers
* match context

Outputs:

* result
* event log
* stats
* fatigue deltas
* injuries
* disciplinary outcomes  

### 3.4 Perception Engine

Owns:

* board confidence
* fan sentiment
* media tone
* narrative tags
* manager reputation updates
* patience buffers
* negativity loops

Inputs:

* results
* xG delta
* derby or rivalry context
* promises
* public comments
* disciplinary choices
* transfers

Outputs:

* pressure updates
* patience shifts
* reputation changes
* narrative effects
* downstream morale and transfer modifiers 

### 3.5 Transfer Engine

Owns:

* squad-needs generation
* target surfacing
* offer evaluation
* negotiation states
* decision outcomes

Inputs:

* player profile
* club state
* squad competition
* budgets
* reputation
* promises
* project fit
* role fit
* development path

Outputs:

* accept or reject decisions
* wage demands
* negotiation counters
* board blocks
* unrest triggers
* updated squad plans  

### 3.6 Academy and Pathway Engine

Owns:

* academy quality
* pathway bias
* youth intake generation
* development status
* loan suitability
* promotion readiness

Inputs:

* club identity
* board philosophy
* squad congestion
* manager risk tolerance
* loan opportunities

Outputs:

* intake players
* pathway recommendations
* loan options
* promotion candidates
* academy pressure signals   

### 3.7 Save and Persistence Layer

Owns:

* serialization
* deserialization
* local save slots
* migration-safe save schema

Inputs:

* complete world and club state

Outputs:

* restorable local saves
* explicit load errors
* versioned save payloads 

### 3.8 Presentation Client

Owns:

* menus
* squad screens
* tactics UI
* match HUD
* live 2D renderer
* event presentation
* player-facing explanations

Inputs:

* sim-core state and event logs

Outputs:

* rendered screens
* user commands
* surfaced explanations

The client renders outcomes. It does not decide outcomes.  

## 4. Ownership boundaries

### 4.1 World Simulator

Owns calendar truth, competition truth, and shadow-league progression.
Does not own match resolution, transfer acceptance, or club morale.

### 4.2 Club State Engine

Owns persistent club truth.
Does not decide match events or transfer negotiations directly.

### 4.3 Match Engine

Owns match truth.
Does not mutate global world structures directly except through returned outcomes.

### 4.4 Perception Engine

Owns pressure and narrative truth.
Does not decide raw match outcomes or set budgets directly.

### 4.5 Transfer Engine

Owns transfer negotiation logic and acceptance logic.
Does not let the manager bypass board or sporting-director constraints.

### 4.6 Academy and Pathway Engine

Owns youth generation and readiness logic.
Does not let academy output ignore squad congestion, identity, or board philosophy.

### 4.7 Presentation Client

Owns rendering and user input only.
Does not contain business rules for transfers, tactics effects, reputation shifts, or board evaluation.

### 4.8 Role boundary inside the sim

* **Manager/head coach** controls lineups, tactics, substitutions, sporting discipline, media responses, and transfer influence.
* **Sporting director AI** controls target generation, negotiation flow, wage-structure discipline, and long-term squad planning.
* **Board AI** controls budgets, strategic expectations, patience, and exceptional approvals or blocks.

This boundary is enforced in sim-core, not in UI convention.   

## 5. Interfaces between parts

### 5.1 `WorldSimulator -> MatchEngine`

Provides:

* fixture context
* opponent state snapshot
* competition rules
* derby or stakes tags

Receives:

* match result package
* disciplinary events
* injuries
* standings deltas

### 5.2 `MatchEngine -> ClubStateEngine`

Provides:

* fatigue deltas
* injuries
* cards
* result
* tactical event summary

Receives:

* player availability
* morale modifiers
* current promises and unrest flags

### 5.3 `MatchEngine -> PerceptionEngine`

Provides:

* result
* xG summary
* game-state swings
* derby context
* substitutions and discipline summary

Receives:

* none during the match
* only pre-match pressure modifiers if needed

### 5.4 `ClubStateEngine -> TransferEngine`

Provides:

* budgets
* squad needs
* wage structure
* manager reputation
* promise state
* congestion by position

Receives:

* negotiation outcomes
* signed or lost targets
* unrest triggers
* spending commitments

### 5.5 `ClubStateEngine -> AcademyEngine`

Provides:

* club identity
* squad congestion
* promotion needs
* board philosophy

Receives:

* intake players
* pathway recommendations
* loan candidates
* readiness flags

### 5.6 `PerceptionEngine -> ClubStateEngine`

Provides:

* board confidence change
* fan sentiment change
* media tone change
* manager reputation change

Receives:

* promise state
* current identity and expectations
* disciplinary actions

### 5.7 `sim-core -> PresentationClient`

Provides:

* immutable snapshots
* event logs
* reason summaries
* command schemas

Receives:

* user intent only, such as set lineup, change press intensity, answer media, shortlist target

No interface may allow the UI to directly mutate protected sim state.  

## 6. Core entities

### 6.1 World entities

* `Country`
* `Competition`
* `SeasonCalendar`
* `Fixture`
* `LeagueTable`
* `ShadowLeagueState`

### 6.2 Club entities

* `Club`
* `BoardProfile`
* `IdentityProfile`
* `FinanceState`
* `ExpectationModel`
* `ManagerProfile`
* `ReputationState`

### 6.3 Squad entities

* `Player`
* `PlayerAttributes`
* `PlayerCondition`
* `PlayerMorale`
* `Promise`
* `ContractSummary`
* `InjuryRecord`
* `CardState`
* `SquadRole`

### 6.4 Match entities

* `TacticSetup`
* `MatchContext`
* `ChanceEvent`
* `ShotEvent`
* `SubstitutionEvent`
* `MatchStatLine`
* `MatchResult`
* `MatchEventLog`

### 6.5 Transfer entities

* `SquadNeed`
* `TransferTarget`
* `TransferOffer`
* `NegotiationState`
* `AcceptanceBreakdown`
* `BoardDecision`

### 6.6 Academy entities

* `AcademyProfile`
* `YouthIntakePlayer`
* `PathwayState`
* `LoanOption`
* `PromotionReadiness`

### 6.7 Persistence entities

* `SaveGame`
* `SaveMeta`
* `SchemaVersion`  

## 7. Causal contract for every major system

### 7.1 World Simulator

Must affect at least:

* transfers
* loans
* expectations
* career movement

If it only generates fixtures, it is too weak. 

### 7.2 Club State Engine

Must affect at least:

* match availability and morale
* transfer attractiveness
* academy pathway pressure
* board patience

If it is only a storage bucket, it fails. 

### 7.3 Match Engine

Must affect at least:

* results and standings
* fatigue and injuries
* morale and trust
* perception pressure

If it only outputs a scoreline, it fails.  

### 7.4 Perception Engine

Must affect at least:

* board patience
* transfer pull
* player trust
* media pressure loops

If it only changes flavor text, it fails. 

### 7.5 Transfer Engine

Must affect at least:

* squad quality
* morale
* finances
* pathway congestion
* reputation

If it resolves on fee alone, it fails.  

### 7.6 Academy and Pathway Engine

Must affect at least:

* finances
* transfer need
* fan identity
* squad registration and congestion pressure

If it only spawns random youth players once per year, it fails.  

### 7.7 Finances inside Club State

Must affect at least:

* transfer power
* board strictness
* youth reliance
* expectations

If it only displays numbers, it fails. 

### 7.8 Manager Reputation

Must affect at least:

* board patience
* player trust
* transfer pull
* future job leverage

If it only changes a label, it fails. 

## 8. Folder and file structure

```text
repo/
  apps/
    game-client/
      src/
        app/
        screens/
        components/
        hooks/
        state/
        render2d/
  packages/
    sim-core/
      src/
        world/
          advanceWorld.ts
          calendar.ts
          competitions.ts
          standings.ts
          shadowLeagues.ts
        club/
          clubState.ts
          finances.ts
          board.ts
          identity.ts
          morale.ts
          promises.ts
          reputation.ts
        match/
          simulateMatch.ts
          chanceModel.ts
          tacticsModel.ts
          fatigueModel.ts
          cardModel.ts
          injuryModel.ts
          eventLog.ts
        transfers/
          transferEngine.ts
          demandModel.ts
          negotiation.ts
          squadNeeds.ts
        academy/
          academyEngine.ts
          youthIntake.ts
          pathway.ts
          loans.ts
        shared/
          types.ts
          rng.ts
          config.ts
          rules.ts
    content/
      countries/
        <country-slug>/
          competitions.json
          clubs.json
          players.json
          boardProfiles.json
          identityProfiles.json
          academyProfiles.json
    save/
      src/
        serialize.ts
        deserialize.ts
        localSave.ts
    tests/
      unit/
      integration/
      calibration/
      fixtures/
```

This layout keeps responsibilities explicit, keeps sim logic reusable, and prevents UI code from becoming the real rule engine.  

## 9. Chosen stack

* **Language:** TypeScript
* **UI:** React + Vite
* **2D match rendering:** PixiJS
* **State management:** Zustand or equivalent light store
* **Persistence:** local JSON save plus IndexedDB wrapper
* **Testing:** Vitest for unit and integration, Playwright for UI checks
* **Build shape:** monorepo, no backend in v1

Why this stack:

* shared TypeScript makes one engine for instant and live mode practical
* local-first keeps scope down
* PixiJS is enough for a readable 2D match without pretending to be a physics-heavy action game
* monorepo keeps the AI coding workflow and module boundaries simpler  

## 10. Rejected complexity

### 10.1 Separate engines for instant sim and live mode

Rejected because it creates drift, balance bugs, and duplicated logic.
Simpler choice: one engine, two presentations. 

### 10.2 Microservices or backend-first architecture

Rejected because this is single-player and local-first.
Simpler choice: deterministic core library plus UI shell.  

### 10.3 Full Football Manager style scouting and staff tree

Rejected because the source docs consistently push against scope bloat.
Simpler choice: sporting director AI plus a short surfaced target list and light staff influence later.  

### 10.4 Detailed finance ledger

Rejected because finances are supposed to constrain decisions, not become bookkeeping.
Simpler choice: bank balance, wage budget, transfer budget, spending posture. 

### 10.5 Playable youth leagues and deep academy micromanagement

Rejected because the design target is light but real pathways, not youth spam.
Simpler choice: academy quality, pathway bias, annual intake, loans, promotion readiness.  

### 10.6 Dialogue-tree media system

Rejected because media matters, but long branching dialogue trees are bloat.
Simpler choice: light event-driven media with persistent consequences. 

### 10.7 God-role club control

Rejected because it breaks the core fantasy of constrained agency and conflicts with real football structures.
Simpler choice: explicit permission matrix for manager, sporting director, and board.  

### 10.8 Deep shadow-league simulation

Rejected because it burns complexity on areas the player does not directly experience.
Simpler choice: light shadow leagues that preserve transfers, loans, and world plausibility without deep per-club simulation.  
