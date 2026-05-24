# Manual Godot Regression Coverage

This document records the interactive Godot walkthrough expected for the active Godot + C# product path. `docs/QA.md` is the final regression checklist; this file remains a supporting manual walkthrough.

Automated coverage exists for the primary route shape through:

- `game/scripts/step30_navigation_flow_check.gd`
- `dotnet build game/Touchline.sln`
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game --build-solutions --quit`
- `game/scripts/step50_end_to_end_user_flow_check.gd`
- `game/scripts/step57_final_regression_check.gd`

Legacy web/npm checks are not active product gates for the current Godot game.

The manual Godot walkthrough should confirm the visual and usability details that headless checks do not cover directly.

## Shell Walkthrough

1. Open `MainMenu` and confirm the hierarchy, save-state card, and primary actions remain readable.
2. Start `CareerSetup` and confirm field spacing, copy rhythm, and back navigation to `MainMenu`.
3. Open `ChooseClub` and confirm the club list, preview card, and confirmation flow into `ClubDashboard`.
4. Open `ClubDashboard` and confirm the command-center layout, button hierarchy, and save feedback remain legible.

## Player-Facing Walkthrough

1. Open `SquadScreen` and confirm the new split layout reads clearly at a glance.
2. Change the position filter and confirm the player list stays readable and actionable.
3. Select a player and confirm the detail card, action row, and lineup-status messaging remain visually coherent.
4. Open `PlayerProfile` from the squad workspace and confirm the return path back to `SquadScreen`.

## Competition Walkthrough

1. Open `TacticsScreen` and confirm the board summary remains understandable before and after a save.
2. Open `FixturesScreen` and confirm the timeline and context card remain readable together.
3. Open `StandingsScreen` and confirm the table plus club-summary pairing remains legible.
4. Return to `ClubDashboard` from each screen and confirm there are no dead-end routes or confusing back flows.

## Match Walkthrough

1. Open `MatchdayScene` and confirm the pre-match context still reads cleanly after the shell updates.
2. Launch `LiveMatchScene` and confirm the pitch, sidebar, and event feed remain readable throughout playback.
3. Confirm the full-time handoff into `PostMatchScene`.
4. Confirm `PostMatchScene` returns cleanly to `ClubDashboard` after the calendar advance.
