# Architecture

## 1. Technical Direction

Touchline is implemented as a Godot .NET game with C# domain logic.

Core principles:

- scene-based UI and flow orchestration in Godot
- simulation and business rules in C# domain systems
- autoload singletons for persistent runtime state
- local save and load for career continuity
- one project path focused on football game flow, not web UI tooling

## 2. Repository Shape After Revamp

Primary product path:

- game/project.godot
- game/Touchline.sln
- game/scenes
- game/scripts
- game/assets
- game/data

Optional historical isolation path:

- legacy (contains old web prototype and transitional artifacts)

## 3. Runtime Layers

### 3.1 Presentation Layer (Godot Scenes)

Owns user flow, navigation, and football-facing visualization.

Target scenes:

- MainMenu
- CareerSetup
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

### 3.2 Application State Layer (Autoload Singletons)

Owns long-lived runtime state and scene handoff context.

Expected singletons:

- GameState singleton for current career state
- SaveSystem singleton for save slot operations
- CalendarSystem singleton for date progression
- WorldGenerator singleton for initial world creation

### 3.3 Domain Layer (C# Models and Systems)

Owns deterministic game logic.

Core models:

- Player
- Club
- Squad
- Fixture
- Competition
- Season
- ManagerCareer
- MatchResult
- GameState

Core systems:

- MatchSimulator
- DevelopmentSystem
- TransferSystem
- PerceptionSystem
- CalendarSystem
- SaveSystem
- WorldGenerator

## 4. Ownership Boundaries

### 4.1 Scene Layer

- reads and presents state
- sends commands to domain systems
- does not contain simulation rules

### 4.2 Domain Systems

- evaluate football outcomes
- mutate game state through explicit methods
- return explainable results and reason summaries

### 4.3 Save Layer

- serializes and restores complete career state
- validates payload version and structure
- never silently drops critical state

## 5. Data Strategy

### 5.1 Seed Data

- use generated or authored seed files in game/data
- include clubs, named players, competitions, and season start state

### 5.2 Runtime State

- keep mutable career state in memory via GameState singleton
- persist to local save files on explicit save actions

### 5.3 Determinism

- use seeded random streams where practical for reproducibility
- keep deterministic logic in domain systems before heuristic tuning

## 6. Scene Flow Contract

Primary user path:

MainMenu -> CareerSetup -> ClubDashboard -> Squad or Tactics or Fixtures or Standings -> MatchdayScene -> LiveMatchScene -> PostMatchScene -> ClubDashboard -> Advance Date -> SaveLoadScene.

All transitions must preserve and reflect persistent career state.

## 7. Live Match Presentation Contract

LiveMatchScene presents domain simulation state with visible player movement.

Required outputs:

- pitch with moving player markers or sprites
- scoreline and match clock
- key event feed (chances, goals, cards, injuries, substitutions)
- tactical context display sufficient to feel football-native

LiveMatchScene is a renderer and controller of simulation playback, not a rules engine.

## 8. Build and Tooling Assumptions

- Godot .NET project files are first-class
- C# source lives under game/scripts
- no requirement that old web stack remains main entry point
- legacy stack may remain archived for reference only

## 9. Non-Goals for Current Foundation

- no backend or service split
- no 3D match renderer requirement
- no deep contract-law simulation
- no broad feature expansion beyond active Plan step
