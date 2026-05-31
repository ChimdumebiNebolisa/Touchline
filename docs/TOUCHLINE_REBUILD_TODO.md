# Touchline Rebuild TODO

## 1. Rebuild Goal

Touchline was reset to a PRD-only state. The rebuild goal is to create a clean, focused fictional football career management sim from scratch, guided entirely by `docs/TOUCHLINE_PRODUCT_PRD.md`.

The new build must prove the core product fantasy end to end: role-based authority, partial player knowledge, tactical decisions, match consequences, and persistent career progress. It is not a revival of the deleted implementation — no old code, scenes, or architecture should be treated as a template.

The first rebuild target is a coherent MVP slice: a player can start a career, understand their role, manage within their limits, play through matchday, read consequences, and save/load with confidence. Foundation systems may exist at honest, labeled depth; full Football Manager-scale depth is not the goal.

Success means a readable 30-minute play session, a trustworthy post-match loop, and a product that can be shown in a 2-minute demo — not feature count for its own sake.

## 2. Product Principles

The rebuild must follow these principles:

- Role limits matter
- Football decisions matter
- The player does not directly control footballers
- The dashboard must make the club situation clear
- Every screen must be readable at 1280x720
- No AI-slop UI
- No fake system depth
- Foundation systems must be labeled honestly
- The 30-minute personal play loop matters more than feature count

## 3. MVP Must-Haves

- [ ] Start new career
- [ ] Choose role
- [ ] Choose background
- [ ] Choose license
- [ ] Choose club
- [ ] See club dashboard
- [ ] Inspect squad
- [ ] Open player profile
- [ ] Review partial player information
- [ ] Review tactics
- [ ] Prepare for matchday
- [ ] Run instant sim
- [ ] Watch basic live match timeline
- [ ] Review post-match report
- [ ] See board/fan/squad/job pressure consequences
- [ ] Advance time
- [ ] Save career
- [ ] Load career

## 4. MVP Must-Not-Haves

- [ ] Direct player control
- [ ] Real licensed clubs or players
- [ ] Full transfer market simulation
- [ ] Full youth academy
- [ ] Full finance ledger
- [ ] Full media dialogue tree
- [ ] Full job interview system
- [ ] Multiplayer
- [ ] Online services
- [ ] Decorative dashboard UI that does not improve readability

## 5. Build Order

### Phase 0: Repo Foundation

- [ ] README
- [ ] License
- [ ] Gitignore
- [ ] basic project setup
- [ ] contribution notes if needed

### Phase 1: Playable Skeleton

- [ ] main menu
- [ ] new career flow
- [ ] role/background/license choice
- [ ] club selection
- [ ] basic save/load

### Phase 2: Club Dashboard

- [ ] board status
- [ ] fan status
- [ ] squad status
- [ ] job pressure
- [ ] next fixture
- [ ] current objective
- [ ] next recommended action

### Phase 3: Squad and Player Information

- [ ] player list
- [ ] player profile
- [ ] known attributes
- [ ] estimated attributes
- [ ] unknown markers
- [ ] morale/form/fitness
- [ ] tactical fit
- [ ] traits/personality clues

### Phase 4: Tactics

- [ ] formation
- [ ] team style
- [ ] instructions
- [ ] role authority behavior
- [ ] tactical familiarity
- [ ] tactics impact summary

### Phase 5: Matchday Loop

- [ ] match preview
- [ ] instant sim
- [ ] live match timeline
- [ ] match events
- [ ] final score
- [ ] post-match report

### Phase 6: Consequences

- [ ] board reaction
- [ ] fan reaction
- [ ] squad reaction
- [ ] player morale/form changes
- [ ] job pressure changes
- [ ] news/event summary

### Phase 7: Foundation Systems

- [ ] training
- [ ] scouting
- [ ] recruitment/contracts
- [ ] promises
- [ ] job market
- [ ] youth/finance/media placeholders with honest depth labels

### Phase 8: Personal Play Loop

- [ ] 30-minute play session
- [ ] top 5 boring moments
- [ ] top 5 confusing moments
- [ ] fixes based on actual play
- [ ] 2-minute demo path

## 6. Screen Acceptance Checklist

- [ ] Fits 1280x720
- [ ] No text clipped or cut off
- [ ] Long content scrolls
- [ ] One obvious primary action
- [ ] Clear screen title
- [ ] Clear active navigation
- [ ] No wall-of-text debug panels
- [ ] Dashboard understandable in 15 seconds
- [ ] Post-match explains why the match happened
- [ ] Player profile shows partial information clearly

## 7. Gameplay Acceptance Checklist

- [ ] Assistant Manager feels limited but useful
- [ ] Head Coach controls football-side decisions
- [ ] Manager has broader authority but still has board limits
- [ ] Club identity affects expectations
- [ ] Tactics affect match outcomes
- [ ] Player information is not always fully known
- [ ] Post-match consequences affect pressure
- [ ] Save/load preserves career state
- [ ] Player wants to continue one more week

## 8. Research Guardrails

These references from the PRD should guide the rebuild. They are for research and inspiration only — do not copy code or assets without licensing review.

- **Uncodixfy** — Avoid generic AI UI patterns: oversized cards, gradient-heavy dashboards, decorative labels, glass panels, and fake-polished layouts. Touchline UI must be practical, readable, and football-management focused.
- **Godot Demo Projects** — Study practical scene and container UI patterns before building screens. UI should use real layout structure, not dumped text in panels.
- **Game Programming Patterns** — Keep game system thinking clear: separate product logic from presentation, use clear state and update patterns, and avoid tangled screen logic.
- **OpenRCT2 / LinCity-NG** — Management sim clarity: readable objectives, status, consequences, and success/failure framing. The player should always know what they are trying to achieve this week, this season, and in their career.
- **OpenTTD / Battle for Wesnoth** — Strong player-facing documentation and help. Roles, club archetypes, player traits, rivalries, and career stories need clear explanations. Save compatibility and career stability matter.
- **FreeOrion** — Fictional world and long-term strategy depth. Clubs, leagues, players, staff, and career reputation should feel like a living fictional football world.

## 9. First Rebuild Milestone

The first rebuild milestone is complete when:

A player can start a career, choose role/background/license/club, reach the dashboard, inspect squad/player/tactics, simulate one match, read post-match consequences, save, load, and understand what happened.

## 10. Done Definition

The first rebuild is **done** when:

- The game is playable for 30 minutes
- The UI is readable
- The loop is understandable
- Save/load works
- Matchday has consequences
- Role authority is visible
- No screen looks like a debug dump
- The project can be shown in a 2-minute portfolio demo
