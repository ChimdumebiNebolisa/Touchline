# Touchline

Touchline is a desktop-first football management game built with Godot and C#.

The player starts a career, chooses a club, manages squad and tactics, plays through fixtures and standings, watches a live 2D match presentation or resolves matches instantly through the same shared engine, then carries the consequences forward across weeks, matchdays, and seasons.

This repository is no longer a web-dashboard prototype. The active product path is the Godot/.NET game under [`game/`](game).

## What the game currently includes

- New Career flow
- Load and resume flow
- Club selection with seeded club identity
- Club dashboard as the main command center
- Squad workspace with named players and player profiles
- Tactical board
- Fixtures and standings screens
- Matchday entry screen
- Live 2D match view with visible player movement
- Instant result path using the same match engine
- Post-match consequence screen
- Calendar advancement and season continuity
- Local save/load persistence

## Product path

The active product path is:

- [`game/project.godot`](game/project.godot)
- [`game/Touchline.sln`](game/Touchline.sln)
- [`game/scenes`](game/scenes)
- [`game/scripts`](game/scripts)
- [`game/data`](game/data)

The main entry scene is [`game/scenes/MainMenu.tscn`](game/scenes/MainMenu.tscn).

## Requirements

- Windows machine with Godot Mono installed
- Godot 4.6.x Mono/.NET
- .NET 8 SDK

The repository has been exercised against the WinGet-installed Godot Mono console binary:

```powershell
Godot_v4.6.2-stable_mono_win64_console.exe
```

If your local Godot binary uses a different filename, adjust the examples below.

## Run the game

From the repository root:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64.exe).Source
& $godot --path game
```

Console/headless-capable variant:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64_console.exe).Source
& $godot --path game
```

You can also open [`game/project.godot`](game/project.godot) directly in the Godot editor.

## Build and verification

Primary .NET build:

```powershell
dotnet build game/Touchline.sln
```

Godot solution rebuild:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64_console.exe).Source
& $godot --headless --path game --build-solutions --quit
```

Representative headless route checks used in the current repo:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64_console.exe).Source
& $godot --headless --path game -s res://scripts/step23_post_match_check.gd
& $godot --headless --path game -s res://scripts/step30_navigation_flow_check.gd
```

There is legacy web code preserved under [`legacy/`](legacy), but `npm` checks are not the active product verification path for the current game.

## Repository structure

```text
Touchline/
|- docs/                 Source-of-truth product docs
|- game/                 Active Godot/.NET game
|  |- scenes/            Godot scenes and screen layouts
|  |- scripts/           C# and GDScript runtime logic
|  |- data/              Seed data
|  `- project.godot      Godot project entrypoint
|- legacy/               Archived web prototype/reference material
`- AUTONOMOUS_PROGRESS.md
```

## Source of truth

The repo is spec-driven. The authoritative docs are:

- [`docs/PRD.md`](docs/PRD.md)
- [`docs/Architecture.md`](docs/Architecture.md)
- [`docs/Guardrails.md`](docs/Guardrails.md)
- [`docs/Plan.md`](docs/Plan.md)

Read them in that order before changing product behavior.

## Development rules

- Godot plus C# is the only active product path.
- Scene flow lives in Godot scenes; simulation logic lives in C# domain systems.
- Do not invent scope outside the active Plan step.
- Do not move business logic into UI scenes.
- Verify meaningful changes before committing.
- Keep [`AUTONOMOUS_PROGRESS.md`](AUTONOMOUS_PROGRESS.md) current when working autonomously.

## Legacy path

The previous web prototype remains under [`legacy/`](legacy) for reference only. It is not the active game shell and should not be treated as the main product path.

## License

This project is licensed under the MIT License. See [`LICENSE`](LICENSE).
