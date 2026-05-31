# UI Rebuild Report

## Summary

Applied the UI rebuild plan: shared layout kit, scroll-safe management screens at 1280×720, card-based dashboard and player profile partial-information blocks, and updated headless audit paths.

## Shared components

| File | Purpose |
|------|---------|
| `game/scripts/ui/TouchlineUiShell.cs` | Viewport budget, scroll discovery, footer helper |
| `game/scripts/ui/TouchlineUiCard.cs` | Card factory (eyebrow/value/meta) |
| `game/scripts/ui/TouchlineUiSection.cs` | Section header + body |
| `game/scripts/ui/TouchlineUiChip.cs` | Status chip |
| `game/scripts/ui/TouchlineUiScrollPanel.cs` | ScrollContainer + content VBox |
| `game/scripts/ui/TouchlineUiLabeledRow.cs` | Label/value row for save slots |
| `game/scenes/ui/TouchlineManagementShell.tscn` | Reference shell scene |
| `game/scripts/TouchlineManagementShell.cs` | Stable path constants |

## Screens updated

- Main menu (removed decorative bands, tighter card)
- Career setup (`FormScroll` + pinned actions)
- Club dashboard (`MainScroll`, `InsightScroll`, 3-column summary grid, section eyebrows)
- Post-match (`MainScroll`, pinned action card)
- Squad, tactics, fixtures, standings, player profile, matchday (rail `MainScroll`)
- Player profile: dedicated Profile Confidence / Known / Estimated / Unknown labels

## Verification (headless)

| Check | Result |
|-------|--------|
| `dotnet build game/Touchline.sln` | Pass |
| `audit_ui_rebuild_layout_check.gd` | Pass |
| `audit_post_match_layout_check.gd` | Pass |
| `audit_partial_information_check.gd` | Pass |
| `audit_sidebar_active_route_check.gd` | Pass |
| `step2_career_setup_check.gd` | Pass |
| `step48_dashboard_context_check.gd` | Pass |
| `active_playtest_user_flow_check.gd` | Pass |

## Desktop screenshots

Run after merge:

```powershell
python docs/audit/active-playtest/scripts/active_desktop_playtest.py
```

Evidence lands in `docs/audit/active-playtest/screenshots/` and `docs/audit/ui-rebuild/screenshot_validation.md`.

## Status

Layout system rebuild is implemented in product code. Visual Green depends on refreshed 1280×720 screenshots showing card hierarchy (not single-label debug dumps).
