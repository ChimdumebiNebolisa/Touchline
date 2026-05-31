# Screenshot Validation Checklist

Target resolution: **1280×720**

## Required captures

| # | Screen | Pass criteria |
|---|--------|----------------|
| 1 | Main menu | Full card visible; slot rows readable; no vertical clip |
| 2 | New career | `FormScroll` visible; actions pinned at bottom |
| 3 | Dashboard | Summary cards above scroll; no single giant label column |
| 4 | Squad + profile | List + detail; Known/Estimated/Unknown blocks visible |
| 5 | Tactics | Formation + controls fit frame |
| 6 | Fixtures | Timeline in scroll |
| 7 | Matchday | Actions reachable |
| 8 | Live match | Pitch + controls in frame |
| 9 | Post-match | Score, stat cards, reactions, Continue |
| 10 | Training/scouting | Dashboard section with role button |
| 11 | Recruitment/contracts | Dashboard section visible |
| 12 | Job market | Dashboard section visible |

## How to capture

```powershell
cd C:\Users\Chimdumebi\Touchline
python docs/audit/active-playtest/scripts/active_desktop_playtest.py
```

## Automated gates (no screenshot)

```powershell
$godot = "$env:LOCALAPPDATA\Microsoft\WinGet\Packages\GodotEngine.GodotEngine.Mono_Microsoft.Winget.Source_8wekyb3d8bbwe\Godot_v4.6.2-stable_mono_win64\Godot_v4.6.2-stable_mono_win64_console.exe"
& $godot --headless --path game -s res://scripts/audit_ui_rebuild_layout_check.gd
& $godot --headless --path game -s res://scripts/active_playtest_user_flow_check.gd
```

## Verdict rule

Do **not** mark visual Green until PNGs show card layout and scroll regions—not monolithic debug text panels.
