# Touchline Windows Evidence Final Summary for ChatGPT

## Bottom line

**Yellow — layout rebuild landed; refresh desktop PNGs to claim Green.**

The prior **Green** verdict reflected passing headless/token checks and old screenshots, not viewport-bounded card layout at 1280×720. This pass rebuilt the presentation layer (scroll regions, dashboard sections, player partial-info rows). Headless gates pass; **visual Green requires a new `active_desktop_playtest.py` capture** showing card hierarchy instead of debug-style label dumps.

## What changed in the UI rebuild

- Shared UI kit under `game/scripts/ui/` and reference shell `game/scenes/ui/TouchlineManagementShell.tscn`
- `MainScroll` on management screens; `FormScroll` on career setup; `InsightScroll` on dashboard notes
- Dashboard summary grid (3 columns) fixed above scrollable detail
- Player profile: dedicated Profile Confidence / Known / Estimated / Unknown labels
- Post-match: scrollable consequences + pinned Continue action card
- Narrower nav rail (220px); removed decorative full-screen bands on main menu / dashboard / post-match

## Headless verification (post-rebuild)

| Check | Result |
|-------|--------|
| `dotnet build game/Touchline.sln` | Pass |
| `audit_ui_rebuild_layout_check.gd` | Pass |
| `audit_post_match_layout_check.gd` | Pass |
| `audit_partial_information_check.gd` | Pass |
| `audit_sidebar_active_route_check.gd` | Pass |
| `active_playtest_user_flow_check.gd` | Pass |
| `step2_career_setup_check.gd` | Pass |
| `step48_dashboard_context_check.gd` | Pass |

## Screenshot refresh required

Re-run:

```powershell
python docs/audit/active-playtest/scripts/active_desktop_playtest.py
```

Validate against `docs/audit/ui-rebuild/screenshot_validation.md`.

## Remaining UI issues

| Issue | Severity | Notes |
|-------|----------|-------|
| Desktop PNGs predate layout rebuild | P1 | Re-capture before human playtest sign-off |
| Training/recruitment remain dashboard sections | P3 | Per PRD foundation scope |
| No ffmpeg live-match video | P3 | Timed sequence still acceptable |

## Paste-back summary

Touchline UI layout was rebuilt from research rules: scroll-safe 1280×720 shells, card-first dashboard, career form scroll, and explicit partial-information rows on player profile. Headless audits pass. Prior Windows Green is revoked until new screenshots prove the layout is designed—not merely documented. See `docs/audit/ui-rebuild/` for plan, report, and validation checklist.
