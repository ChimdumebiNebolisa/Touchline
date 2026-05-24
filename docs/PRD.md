# PRD

## 1. Product Identity

Touchline is a football management game built with Godot and C#.

The player starts a career, chooses a club, and manages that club across matches, weeks, months, seasons, and years in a persistent football world. The target feel is between Football Manager depth and older PES-style presentation.

This is not a one-off simulation harness and not a generic dashboard app.

Final v1 boundary: Touchline is a local-first, single-player Godot + C# football management demo focused on the squad, tactics, matchday, post-match, save/load, and short seeded season loop. The shipped scale is intentionally small and local so the app can be demonstrated honestly instead of implying a broader management sim.

## 2. Core Fantasy

The player is a manager living with consequences:

- named players with evolving stories
- meaningful tactical and squad decisions
- season context with fixtures and standings
- visible live match football action
- pressure from form, morale, fitness, results, and expectations

## 3. Core Loop

Main menu -> new career or load -> choose club -> club dashboard -> prepare squad and tactics -> matchday -> live or instant result -> post-match consequences -> advance date -> repeat across season and years.

## 4. Must-Have v1 Features

### 4.1 Career and Persistence

- New Career flow
- Save Game flow
- Load Game flow
- Persistent world state across sessions

### 4.2 Club-Centered Shell

- Club Dashboard as the main hub
- navigation to squad, player profile, tactics, fixtures, standings, and matchday
- football-native framing in all primary scenes

### 4.3 Named Players and Squad Management

- all visible players have real names and identities
- player records include age, position, role, form, morale, fitness, and lineup status
- Squad Screen supports lineup clarity across starters, bench/rotation players, and reserve depth
- Player Profile screen exposes player-specific context and trajectory

### 4.4 Match and Season Flow

- Fixtures and Standings screens
- calendar advancement by day or week
- seasonal progression with rollover into new season
- post-match consequences applied to an ongoing career

### 4.5 Match Presentation

- lightweight Live Match view with visible player movement
- football context on screen: score, time, key events, shape pressure
- live view presents simulation state, not raw data forms

### 4.6 Player Development Over Time

- player aging each season
- improvement, stagnation, and decline arcs
- form, morale, and fitness fluctuate over time

## 5. Product Constraints

- Godot plus C# is the primary build direction
- single-player and local-first for v1
- no requirement for a 3D match engine in v1
- no fake UI-only football systems
- no unnamed placeholder player identities in player-facing flows
- legacy web/TypeScript code is archived or reference-only, not the active product path
- v1 must finish the demonstrable management loop rather than expand into new simulation pillars

## 6. Out of Scope for v1

- backend services
- online multiplayer
- playable football controls
- 3D match engine
- transfer market
- contracts, wages, or finances
- scouting
- injuries
- promotion or relegation
- youth academy or playable youth leagues
- deep training systems
- complex xG model
- multi-competition calendar
- real licensed teams
- external APIs
- tactical advice engine or AI assistant
- deep staff, media, board, or owner simulation beyond the current local demo surfaces

## 7. Acceptance Criteria

v1 direction is valid only if all are true:

- the player can start, save, load, and continue a career
- the main product path is Godot plus C# with a real game shell
- the club dashboard is the central navigation hub
- named players persist across fixtures and time advancement
- fixtures, standings, and date progression are visible and interactive
- live match view shows moving players and football context
- post-match outcomes change ongoing career state
- players age and their development state changes across seasons
- unsupported systems are not implied in UI or docs

## 8. Success Test for Early Slices

The product should feel like a football game in motion, not internal tooling:

- clear club identity and matchday rhythm
- named players with continuity
- visible consequence chains after results
- a believable path from menu to season progression with save continuity

## 9. Supported Final Demo Flows

- start a local career
- choose one seeded club
- inspect dashboard, squad, player profile, tactics, fixtures, standings, and matchday
- resolve a match through live playback or instant result using the same match engine
- review post-match consequences and player-state effects
- advance through multiple matchdays and short-season rollover
- save and load the local career without fake success states
