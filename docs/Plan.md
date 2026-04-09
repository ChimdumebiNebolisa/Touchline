# Plan

## 1. Status

### Active

- Step 7: Matchday Scene

### Backlog

- Step 8: Live Match Scene with visible player movement
- Step 9: Post-Match Scene
- Step 10: Advance Date
- Step 11: Save Game

### Blocked

- None

### Done

- Step 1: Main Menu
- Step 2: New Career
- Step 3: Choose Club
- Step 4: Club Dashboard
- Step 5: Squad Screen with named players
- Step 6: Tactics Screen

## 2. Plan Rules

1. Only one active step at a time.
2. Each step must be verified before activating the next.
3. Implement only the smallest valid subtask for the active step.
4. Do not jump ahead to deeper systems while shell flow is incomplete.
5. Keep scope football-native and persistent-world oriented.

## 3. Step 1: Main Menu

### Objective

Create a Godot main menu as the entry point with explicit actions:

- New Career
- Load Game
- Exit

### Allowed Subtasks

- establish Godot project entry scene wiring
- create MainMenu scene and script
- add button flow placeholders that route to scene stubs

### Verification

- project opens with MainMenu as startup scene
- buttons trigger expected scene transitions or explicit temporary notices
- no web shell remains the active entry path

### Exit Criteria

- MainMenu scene is active entry point
- navigation targets for New Career and Load Game exist

## 4. Step 2: New Career

### Objective

Implement career creation flow with manager profile basics and initial world seed creation.

### Verification

- player can start a new career session
- created career state is persisted in runtime GameState

## 5. Step 3: Choose Club

### Objective

Allow club selection from generated or seeded data as part of new career.

### Verification

- selected club is stored in ManagerCareer and GameState
- club choice leads to Club Dashboard

## 6. Step 4: Club Dashboard

### Objective

Build club hub scene with clear football navigation and current state summary.

### Verification

- dashboard shows club identity, next fixture context, and key squad status
- navigation to Squad, Tactics, Fixtures, Standings, and Matchday works

## 7. Step 5: Squad Screen with Named Players

### Objective

Present full named squad with role, age, form, morale, fitness, and selection state.

### Verification

- no placeholder player names in visible list
- player selection changes lineup state for upcoming matchday

## 8. Step 6: Tactics Screen

### Objective

Allow tactical setup changes tied to persistent team configuration.

### Verification

- tactics changes persist when returning to dashboard
- tactics feed into match preparation state

## 9. Step 7: Matchday Scene

### Objective

Provide pre-match context and launch path into live match simulation.

### Verification

- scene displays opponent, competition, lineup summary, and tactical setup
- match start transitions to LiveMatchScene

## 10. Step 8: Live Match Scene with Visible Player Movement

### Objective

Display lightweight live football simulation with moving players and match context.

### Verification

- players visibly move on a pitch view
- score, time, and key events update during simulation

## 11. Step 9: Post-Match Scene

### Objective

Show match result and apply persistent consequences.

### Verification

- post-match scene shows outcome, key events, and consequence deltas
- consequence state is persisted into ongoing career

## 12. Step 10: Advance Date

### Objective

Advance calendar and world state between matches.

### Verification

- date progression updates fixture timeline and form context
- season continuity remains consistent after advancement

## 13. Step 11: Save Game

### Objective

Persist full game state to local save slot and reload safely.

### Verification

- save files are created and reload correctly
- reloaded state matches expected squad, fixtures, and progression data

## 14. Immediate Next Subtask

For active Step 7, smallest valid next subtask is:

- replace MatchdayScene stub with pre-match context and transition into LiveMatchScene.
