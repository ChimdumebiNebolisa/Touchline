# Touchline UI Polish Fix Report

## Scope

This pass fixed only the Yellow-audit issues already evidenced in:

- `docs/audit/active-playtest/for_chatgpt_summary.md`
- `docs/audit/active-playtest/evidence_review_table.md`

No new gameplay systems were added. Training/scouting, recruitment/contracts, and job-market evidence remain grounded in the existing dashboard-driven foundation.

## What Changed

| Issue | Before evidence | Fix | After evidence | Status |
|---|---|---|---|---|
| Screenshot capture was unreliable and could save stale screens | Yellow audit summary reported 42 labeled screenshots collapsing to 4 unique images; `active_desktop_playtest.py` relied on sleeps and untargeted capture | Added Godot-side UI-state export, command bridge screenshot capture, expected-screen/anchor assertions, metadata logging, duplicate-hash review, and screenshot archiving | `docs/audit/active-playtest/logs/screenshot-capture-validation.json`, `docs/audit/active-playtest/screenshot_capture_report.md`, `docs/audit/active-playtest/logs/active-playtest-run-20260530-170551.json` | Fixed |
| Sidebar active state could mislead by leaving `Matchday` highlighted | Yellow audit cited dashboard, squad, tactics, and fixtures screenshots showing a stale active route | Added shared rail-navigation helper and replaced per-screen selection wiring so exactly one route is active and `Matchday` is a CTA unless the current scene is matchday | `AUDIT_SIDEBAR_ACTIVE_ROUTE_PASS`; corrected dashboard, squad, tactics, fixtures, and standings screenshots in `docs/audit/active-playtest/screenshots/` | Fixed |
| Post-match looked incomplete and clipped | `docs/audit/football-feel/post-match.png` showed a large empty top area and clipped consequence content | Rebuilt the post-match layout into a denser summary with key stats, tactical explanation, consequence sections, scroll-safe long text, and a stronger next action | `AUDIT_POST_MATCH_LAYOUT_PASS`; `docs/audit/active-playtest/screenshots/manager-post-match.png` | Fixed |
| Main menu and save/load slot cards wrapped metadata badly | `docs/audit/football-feel/main-menu.png` and `save-load.png` showed cramped fixture/date wrapping | Reflowed slot metadata into labeled stacked rows with clearer placeholders and preserved obvious resume/load actions | `docs/audit/active-playtest/screenshots/*main-menu-slot-card.png`; `docs/audit/active-playtest/screenshots/*save-load-slot-card.png`; `STEP55_SAVE_ERROR_STATE_PASS` | Fixed |
| Dashboard copy was too prose-heavy | Yellow audit flagged repeated long `Next Decision` and `Club Notes` text | Tightened dashboard copy, shortened repeated guidance, and made the next best action more direct while keeping morale, pressure, and authority visible | `docs/audit/active-playtest/screenshots/*dashboard.png`; `STEP48_DASHBOARD_CONTEXT_PASS`; `ACTIVE_PLAYTEST_USER_FLOW_PASS` | Fixed |
| Partial player information was not visually obvious | Yellow audit cited squad/profile captures that did not visibly prove uncertainty or scouting confidence | Surfaced `Profile Confidence`, `Known`, `Estimated`, `Unknown`, visibility rationale, tactical fit, personality, development, and risk on squad detail and player profile | `AUDIT_PARTIAL_INFORMATION_PASS`; `docs/audit/active-playtest/screenshots/manager-squad.png`; `docs/audit/active-playtest/screenshots/manager-player-profile.png` | Fixed |
| Role-specific visual proof was weak | Previous visual set was manager-centric and the role-labeled screenshot harness was not trustworthy | Added role-specific verified captures for Assistant Manager, Head Coach, and Manager across dashboard, tactics, training/scouting, and recruitment/contracts | `docs/audit/active-playtest/screenshots/assistant-manager-*.png`, `head-coach-*.png`, `manager-*.png`; screenshot validation metadata | Fixed |
| Training/scouting, recruitment/contracts, and job-market lacked direct screenshot coverage | Yellow audit said these areas were supported by logs more than by visuals | Added verified dashboard-section captures for training/scouting, recruitment/contracts, and career/job-market with role anchors and scene assertions | `docs/audit/active-playtest/screenshots/*training-scouting.png`, `*recruitment-contracts.png`, `manager-job-market.png`, `manager-career-job-market.png` | Fixed |
| Live match pacing had no video evidence | `docs/audit/active-playtest/videos/` was empty | Captured a verified timed screenshot sequence for kickoff, mid-match, full time, and corrected post-match follow-through; documented why video was skipped | `manager-live-match-kickoff.png`, `manager-live-match-mid.png`, `manager-live-match-full-time.png`, `manager-post-match.png`; `screenshot_capture_report.md` | Partially fixed |

## Screenshot Reliability

- Yellow audit baseline: `42` labeled screenshots collapsed to `4` unique images.
- Immediate local set archived before the repaired rerun: `29` screenshots, `25` unique hashes.
- New validated set: `34` screenshots, `28` unique hashes.
- Duplicate hashes remain only where the expected screen is intentionally the same:
  - all three role-specific tactics captures
  - main-menu/save-load recaptures during the deterministic post-match review pass
  - recruitment/contracts and job-market from the same dashboard state
  - squad and squad-return from the same squad state
- No duplicate hash spans different expected screens, so the repaired pipeline no longer silently collapses distinct steps into misleading proof.

## Verification

- `git diff --check`
- `git diff --cached --check`
- `dotnet build game/Touchline.sln`
- Full clean Godot headless suite through `step22` to `step57`
- `res://scripts/active_playtest_user_flow_check.gd`
- `res://scripts/audit_sidebar_active_route_check.gd`
- `res://scripts/audit_post_match_layout_check.gd`
- `res://scripts/audit_partial_information_check.gd`
- `python docs/audit/active-playtest/scripts/active_desktop_playtest.py`

## Remaining Gaps

| Issue | Severity | Why it remains | Recommended next step |
|---|---|---|---|
| No captured video for live-match pacing | P3 | The environment has no local `ffmpeg` binary, so motion quality is still inferred from timed screenshots rather than a real clip | Add a no-dependency recorder or install a supported local video tool before the next evidence audit |
| Live-match to post-match is not shown as one continuous visual artifact | P2 | The repaired evidence uses a timed live sequence plus a deterministic instant-result post-match pass to guarantee an honest corrected post-match capture | Add a continuous visual capture once a reliable video path exists |
| Training/scouting, recruitment/contracts, and job-market remain dashboard-section evidence | P3 | The current product intentionally keeps these systems in the dashboard foundation and this pass did not add new screens | Keep the dashboard sections honest, or add standalone screens only if the product docs later require them |

## Bottom Line

The evidence is materially stronger than the Yellow audit baseline. The UI problems the audit verified are fixed, the screenshot harness now validates what it captures, and the new artifacts are honest about what is still foundation-depth.
