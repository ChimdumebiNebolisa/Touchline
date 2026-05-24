# Touchline Release Workflow

This checklist is for preparing a local demo build of the active Godot + C# game. It is intentionally small: Touchline does not use a backend service, hosted web app, online account system, or npm-based product path.

## Active Project Path

- Godot project: `game/project.godot`
- C# solution: `game/Touchline.sln`
- Main scene: `res://scenes/MainMenu.tscn`
- Runtime scripts: `game/scripts`
- Seed data: `game/data/world-seed.json`

Legacy web code is reference material only. Do not use npm commands as release gates for the current Godot game.

## Run Locally

From the repository root:

```powershell
Godot_v4.6.2-stable_mono_win64.exe --path game
```

If only the console binary is on `PATH`, this also opens the project:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --path game
```

In the editor, open `game/project.godot` and run the main scene. The active main scene should be `res://scenes/MainMenu.tscn`.

## Build Checks

Run the C# build:

```powershell
dotnet build game/Touchline.sln
```

Ask Godot to rebuild C# project glue/solutions:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game --build-solutions --quit
```

Run the release-workflow smoke check:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step56_release_workflow_check.gd
```

## Headless Verification Groups

Use focused checks while working, then run the final grouped suite from `docs/QA.md` once Step 57 exists.

Core smoke:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step22_shared_engine_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step30_navigation_flow_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step50_end_to_end_user_flow_check.gd
```

Demo-critical screen checks:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step51_squad_profile_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step52_tactics_context_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step53_competition_surfaces_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step54_ui_consistency_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step55_save_error_state_check.gd
```

Season/save confidence:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step27_save_compat_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step42_matchday_progression_check.gd
Godot_v4.6.2-stable_mono_win64_console.exe --headless --path game -s res://scripts/step47_full_season_regression_check.gd
```

## Windows Demo Export

Use the Godot editor for the first Windows export so the export templates and local SDK paths stay machine-local.

1. Open `game/project.godot` in Godot Mono.
2. Confirm the main scene is `res://scenes/MainMenu.tscn`.
3. Open `Project > Export`.
4. Add a Windows Desktop preset if none exists.
5. Install export templates if Godot prompts for them.
6. Export to a local folder outside the repository, for example `C:\TouchlineDemo\Touchline.exe`.
7. Run the executable and complete the manual smoke path: main menu, new career, choose club, dashboard, squad, tactics, fixtures, standings, matchday, live or instant result, post-match, save/load.

Do not commit machine-specific export paths. Only add `export_presets.cfg` later if it is portable and reviewed.

## Demo Proof

For a submission or portfolio demo, collect:

- A screenshot set covering the main menu, dashboard, squad, tactics, fixtures, standings, matchday, live match, post-match, and save/load.
- A short video showing one career loop from start/load through match result and save/load continuity.
- The exact build/check commands that passed before capture.

Step 58 owns the detailed asset list and script; Step 60 owns the final README references.
