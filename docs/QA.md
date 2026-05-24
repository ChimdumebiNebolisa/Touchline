# Touchline Final QA Checklist

This checklist is the final proof pass for the local-first Godot + C# demo. It verifies the supported v1 loop only: career start/load, club selection, dashboard, squad/profile, tactics, fixtures, standings, matchday, live or instant result, post-match consequences, progression, season rollover, and save/load continuity.

## Automated Gates

Run these from the repository root. Use the Godot Mono console binary available on the machine.

### Build

```powershell
dotnet build game/Touchline.sln
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game --build-solutions --quit
```

### Runtime, Engine, Data, And Save Path

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step22_shared_engine_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step23_post_match_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step25_autoload_flow_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step26_seed_data_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step27_save_compat_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step28_season_rollover_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step29_pressure_context_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step30_navigation_flow_check.gd
```

### Match Playback And Post-Match Proof

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step34_match_playback_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step35_live_renderer_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step36_opponent_squad_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step37_match_variation_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step38_post_match_causes_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step39_action_participants_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step40_match_stats_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step41_post_match_report_check.gd
```

### Progression, Season, And Player State

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step42_matchday_progression_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step43_player_condition_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step44_multi_match_regression_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step45_season_rollover_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step46_season_development_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step47_full_season_regression_check.gd
```

### Final UI And Demo-Surface Checks

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step48_dashboard_context_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step49_matchday_preparation_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step50_end_to_end_user_flow_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step51_squad_profile_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step52_tactics_context_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step53_competition_surfaces_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step54_ui_consistency_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step55_save_error_state_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step56_release_workflow_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step57_final_regression_check.gd
```

## Manual Demo Checklist

- Main menu: verify the local save preview is clear for empty and populated Slot 1 states.
- New career flow: enter manager name, choose seed, confirm a club, and land on the dashboard.
- Dashboard: confirm manager, club, date, matchday, next fixture, pressure context, and primary navigation are readable.
- Squad and player profile: inspect Starting XI, non-starters, form, morale, fitness, age, role, and lineup status.
- Tactics: adjust formation, press, tempo, width, and risk; save; confirm the saved setup is visible.
- Fixtures: check completed, next, upcoming, scoreline, season, matchday, and selected-club context.
- Standings: check table columns, selected-club row, points, goal difference, and next fixture context.
- Matchday: confirm live match and instant result both explain that they use the same engine.
- Live match: start a live match, verify player markers, ball, event feed, score, and post-match handoff.
- Instant result: run an instant match from a fresh/open fixture and verify it reaches post-match.
- Post-match: confirm scoreline, stats, causes, key events, pressure effects, and player-state implications are readable.
- Multi-match progression: advance through at least two matchdays and confirm dates, fixtures, table, and form update.
- Season rollover: finish the short seeded season and confirm the new season returns to Matchday 1 with fresh fixture context.
- Save/load after rollover: save after rollover, return to menu, load, and confirm the same club, season, date, matchday, squad, and table state are restored.
- Visual smoke: scan every primary screen for clipped text, stale prototype wording, broken navigation, and missing state labels.

## Known Limitations

- The demo league is a small seeded four-club local competition.
- The game is single-player and local-first only.
- Live match mode is a tactical playback view, not playable football controls.
- There are no transfers, contracts, wages, finances, scouting, injuries, youth academy, promotion/relegation, multi-competition calendars, licensed teams, online services, or external APIs.
- The legacy web code is reference material only and is not part of the current product path.
- Exporting a Windows executable is a manual Godot editor step until a portable export preset is intentionally added.

## Latest Step 57 Verification

- `dotnet build game/Touchline.sln` passed.
- `Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game --build-solutions --quit` passed.
- Full headless suite passed: Step 22, 23, 25-30, and 34-57.
- Manual visual smoke checklist is documented above and should be executed during screenshot/video capture.
