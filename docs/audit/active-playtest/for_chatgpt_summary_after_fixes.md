# Touchline UI/UX Fix Summary for ChatGPT

## Bottom line

**Yellow, improved** (not Green).

Logic and headless evidence are strong. Visual proof is now **trustworthy** for the active-playtest matrix (28 unique hashes from 34 captures). Remaining gap: motion/video and occasional Linux GUI harness instability—not the core UI defects from the Yellow audit.

## Reference pack

`docs/audit/ui-polish/reference_pack.md` — Godot container/theme rules, UX heuristics, Touchline implementation patterns, anti-patterns, and ordered fix plan.

## Issues fixed

| Issue | Before | After | Status |
|---|---|---|---|
| Post-match incomplete / clipped | `football-feel/post-match.png` | Dense summary + scroll; `manager-post-match.png` | Fixed |
| Sidebar stuck on Matchday | Multiple football-feel screens | `ApplyRailNavigation` per scene; audit pass | Fixed |
| Slot metadata wrapping | `main-menu.png`, `save-load.png` | Labeled stacked rows; role slot-card PNGs | Fixed |
| Dashboard prose-heavy | Yellow summary | Shorter bullets; `STEP48` pass | Fixed |
| Partial info not visible | squad/profile samples | `Profile Confidence`, Known/Estimated/Unknown | Fixed |
| Screenshot pipeline (42→4 hashes) | Mislabeled duplicates | Scene/nav/anchor gates + hash review | Fixed |
| Missing role/training/recruitment shots | Logs only | AM/HC/Manager PNGs + section captures | Fixed |
| Capture before paint | Sleep-only harness | 3-frame deferred viewport capture in bridge | Fixed (this pass) |

## Screenshot reliability

| Metric | Old (Yellow) | New (validated) |
|---|---:|---:|
| Screenshot count | 42 | 34 |
| Unique hashes | 4 | 28 |

No duplicate hash spans different expected screens. See `docs/audit/active-playtest/screenshot_capture_report.md`.

## Verification

- `dotnet build game/Touchline.sln` — pass
- Full headless suite — `*_PASS` lines in `full-godot-suite.log`
- `ACTIVE_PLAYTEST_USER_FLOW_PASS`
- `AUDIT_SIDEBAR_ACTIVE_ROUTE_PASS`, `AUDIT_POST_MATCH_LAYOUT_PASS`, `AUDIT_PARTIAL_INFORMATION_PASS`

## Remaining issues

| Issue | Severity | Next step |
|---|---|---|
| No video for live-match pacing | P3 | Add ffmpeg or engine movie writer when available |
| Linux GUI playtest SIGSEGV | P2 env | Rerun desktop harness on Windows or stabilize llvmpipe session |
| Foundation-only training/recruitment screens | P3 | Expand only if PRD/Plan require dedicated scenes |

## Paste-back summary

Touchline moved from **untrustworthy** role-labeled screenshots to a **validated** capture set while fixing the Yellow UI issues (nav, post-match, slots, dashboard density, partial info). Call it **Yellow improved**: honest screenshots and fixed layouts, but still no continuous video proof and some systems remain dashboard-depth.
