# Touchline

Touchline is now a Godot plus C# football management game project.

## Requirements

- Godot 4.6.2 Mono/.NET
- .NET 8 SDK or runtime

The current product direction is defined by:

- docs/PRD.md
- docs/Architecture.md
- docs/Guardrails.md
- docs/Plan.md

## Active Product Path

The active build direction is the game foundation under game:

- game/project.godot
- game/Touchline.sln
- game/scenes
- game/scripts
- game/assets
- game/data

The Godot entrypoint is `game/project.godot`, and the app starts at `game/scenes/MainMenu.tscn`.

## Run The Game

From the repository root:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64.exe).Source
& $godot --path game
```

If you want the console variant instead:

```powershell
$godot = (Get-Command Godot_v4.6.2-stable_mono_win64_console.exe).Source
& $godot --path game
```

## Verification

The strongest routine checks currently used in this repo are:

```powershell
dotnet build game/Touchline.sln
npm run test
npm run typecheck
npm run lint
npm run build
```

## Legacy Archive

The previous web prototype has been isolated and is no longer the active product path:

- legacy/web-prototype/apps/game-client

Legacy artifacts are preserved for reference and migration context only.

## Working Rules

Before implementing any subtask, read in order:

1. docs/PRD.md
2. docs/Architecture.md
3. docs/Guardrails.md
4. docs/Plan.md
5. AUTONOMOUS_PROGRESS.md

Only execute the smallest valid subtask in the active Plan step, verify it, then commit and push.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
