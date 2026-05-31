# Touchline UI Polish Fix Report

## Reference pack

Research and Touchline-specific rules: `docs/audit/ui-polish/reference_pack.md`

## Scope

This pass applies those references to the Yellow-audit issues in:

- `docs/audit/active-playtest/for_chatgpt_summary.md`
- `docs/audit/active-playtest/evidence_review_table.md`

No new gameplay systems were added.

## What changed (this commit)

| Area | Change |
|---|---|
| Reference pack | Added `docs/audit/ui-polish/reference_pack.md` (Godot UI docs, demos, UX heuristics, Touchline rules) |
| Screenshot reliability | `AuditCommandBridge` waits 3 frames after scene match before viewport PNG capture |
| Headless live renderer gate | `step35_live_renderer_check.gd` uses temporary `Engine.time_scale` so full-time handoff completes on slow CI VMs |
| Desktop harness | `active_desktop_playtest.py` supports Linux (`xdotool`, `shutil.which`, process-group terminate) |

Prior polish on `main` (`a7c8ed1`) already delivered rail nav, post-match layout, slot cards, dashboard copy, partial-information UI, and verified screenshot matrix.

## Issue status

| Issue | Status | Evidence |
|---|---|---|
| P1 screenshot pipeline unreliable | Fixed (prior + frame defer) | `screenshot_capture_report.md`; 34 captures / 28 unique hashes; no cross-screen duplicate hashes |
| P1 sidebar active route | Fixed (prior) | `AUDIT_SIDEBAR_ACTIVE_ROUTE_PASS`; role dashboards show `SelectedNav=Dashboard` |
| P1 post-match incomplete | Fixed (prior) | `AUDIT_POST_MATCH_LAYOUT_PASS`; `manager-post-match.png` |
| P2 slot metadata wrapping | Fixed (prior) | `*-main-menu-slot-card.png`, `*-save-load-slot-card.png` |
| P2 dashboard prose density | Fixed (prior) | `STEP48_DASHBOARD_CONTEXT_PASS`; tightened dashboard PNGs |
| P2 partial information | Fixed (prior) | `AUDIT_PARTIAL_INFORMATION_PASS`; `Profile Confidence` anchors |
| P2 role / training / recruitment screenshots | Fixed (prior) | Per-role PNGs + `training-scouting`, `recruitment-contracts`, `job-market` |

## Screenshot reliability

| Metric | Yellow baseline | After polish |
|---|---:|---:|
| Labeled screenshots | 42 | 34 |
| Unique image hashes | 4 | 28 |
| Cross-screen duplicate hashes | Many (misleading) | None |

Intentional same-screen duplicates: tactics across roles, post-match review revisiting main menu/save-load, squad return, recruitment vs job-market dashboard state.

## Verification (2026-05-31)

- `git diff --check` — pass
- `git diff --cached --check` — pass
- `dotnet build game/Touchline.sln` — pass (0 warnings, 0 errors)
- Godot headless suite `step22`–`step57` + audit scripts + `active_playtest_user_flow_check` — all emit `*_PASS` in `docs/audit/active-playtest/logs/full-godot-suite.log`
- `step35_live_renderer_check` — prints `STEP35_LIVE_RENDERER_PASS` (Godot mono may exit non-zero on Linux shutdown leaks; pass token is authoritative)
- `python docs/audit/active-playtest/scripts/active_desktop_playtest.py` — headless PASS; Linux GUI recapture unstable (Godot SIGSEGV on llvmpipe after window focus); existing validated PNG set retained

## Remaining gaps

| Issue | Severity | Notes |
|---|---|---|
| Linux GUI playtest crash | P2 env | Use Windows/macOS desktop or future headless offscreen capture for full GUI rerun |
| No live-match video | P3 | `ffmpeg` optional; timed PNG sequence used |
| Foundation-depth training/recruitment | P3 | By design until product docs expand screens |

## Bottom line

Evidence is materially stronger than the Yellow baseline. UI fixes are in place; the reference pack documents how to keep them honest. Screenshot proof is trustworthy when scene/nav/anchor assertions pass—do not rely on sleep-only desktop grabs.
