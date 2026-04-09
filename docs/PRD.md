# PRD

## 1. Product Identity

Touchline is a football management game built with Godot and C#.

The player starts a career, chooses a club, and manages that club across matches, weeks, months, seasons, and years in a persistent football world. The target feel is between Football Manager depth and older PES-style presentation.

This is not a one-off simulation harness and not a generic dashboard app.

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
- player records include age, position, attributes, form, morale, fitness, and value
- Squad Screen supports lineup and bench management
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
- form, morale, fitness, and value fluctuate over time

## 5. Product Constraints

- Godot plus C# is the primary build direction
- single-player and local-first for v1
- no requirement for a 3D match engine in v1
- no fake UI-only football systems
- no unnamed placeholder player identities in player-facing flows

## 6. Out of Scope for v1

- deep scouting simulation tree
- legal-contract-heavy transfer systems
- full accounting ledger simulation
- playable youth leagues
- backend service architecture unless a hard blocker requires it

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

## 8. Success Test for Early Slices

The product should feel like a football game in motion, not internal tooling:

- clear club identity and matchday rhythm
- named players with continuity
- visible consequence chains after results
- a believable path from menu to season progression with save continuity
