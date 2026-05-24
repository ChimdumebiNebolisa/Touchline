# Touchline Demo Asset Plan

This plan defines the proof assets needed for a credible Touchline demo. It does not claim assets already exist, and it does not add unsupported product scope.

## Screenshot Set

Capture these from the active Godot + C# app under `game/project.godot`.

| Asset | Screen | What It Should Prove |
|---|---|---|
| `main-menu.png` | Main Menu | Touchline is a local-first football management game with clear new/continue entry points. |
| `dashboard.png` | Club Dashboard | Manager context, selected club, matchday, pressure, next fixture, and navigation are readable. |
| `squad.png` | Squad Screen | Starting XI, non-starters, roles, condition, form, morale, fitness, and lineup readiness are visible. |
| `player-profile.png` | Player Profile | Player identity, age, role, lineup status, form, morale, fitness, and latest state are clear. |
| `tactics.png` | Tactics Screen | Formation, press, tempo, width, risk, interpretation copy, and saved setup are visible. |
| `fixtures.png` | Fixtures Screen | Completed, next, and upcoming fixtures are separated with scorelines only where results exist. |
| `standings.png` | Standings Screen | Table columns, selected-club row, points, goal difference, and season context are readable. |
| `matchday.png` | Matchday Scene | Live Match and Instant Result choices are clear and tied to the same match engine. |
| `live-match.png` | Live Match Scene | 2D playback shows teams, ball, action feed, clock, score, and match movement. |
| `post-match.png` | Post-Match Scene | Result, stats, key events, causes, pressure impact, and next-step consequences are explained. |
| `save-load.png` | Save/Load Scene | Slot state, manager, club, season/date, matchday, fixture, and load readiness are explicit. |

Recommended capture order:

1. Start a fresh career and choose `Riverton Athletic`.
2. Capture dashboard, squad, player profile, tactics, fixtures, standings, and matchday before resolving the match.
3. Start a live match and capture live playback once players, ball, score, and event feed are active.
4. Continue to post-match and capture the consequence/report screen.
5. Save from the dashboard, return to menu/save-load, and capture the populated Slot 1 state.

## Short Demo Video Structure

Target length: 90-150 seconds.

| Segment | Approx. Time | Content |
|---|---:|---|
| Opening | 0-10s | Main menu, new career or continue career, and local-first save context. |
| Club Hub | 10-25s | Dashboard with manager, club, current date, matchday, next fixture, and pressure context. |
| Manager Work | 25-50s | Squad/profile and tactics screens showing lineup clarity and tactical setup. |
| Season Context | 50-65s | Fixtures and standings showing completed/upcoming state and selected-club table position. |
| Match Flow | 65-105s | Matchday into either live match playback or instant result. If live, show 2D movement and event feed. |
| Consequences | 105-130s | Post-match report with scoreline, stats, causes, pressure, and player-state implications. |
| Continuity | 130-150s | Save/load preview proving the career can resume locally. |

## What The Demo Should Prove

- Touchline is a playable local-first single-player management loop.
- The active product path is Godot + C#, not the legacy web prototype.
- Instant result and live match use the shared match engine.
- Clubs and players come from seeded local data.
- Squad, tactics, fixtures, standings, matchday, live match, post-match, and save/load are connected.
- Match results affect table, form, morale/condition context, and post-match explanation surfaces.
- Season rollover and save/load are covered by the final regression suite, even if the short video does not show a full season.

## README References

Step 60 should reference demo proof only inside the required `## Demo` section.

Use placeholders until the assets are actually captured:

- `Placeholder: Windows executable download not published yet.`
- `Placeholder: screenshots to be added after final capture.`
- `Placeholder: demo video to be added after final capture.`

After capture, link assets from a simple repository path such as `docs/demo/` or from a release page. Do not claim a live web link, hosted backend, online mode, licensed teams, or unsupported systems.

## Capture Guardrails

- Do not show transfer, finance, scouting, injury, youth academy, or online features; they are not in scope.
- Do not use legacy web screenshots as current product proof.
- Do not edit saves or state to fake a result.
- Prefer real in-app screens over mockups.
- If the executable is not built yet, label the download as a placeholder.
