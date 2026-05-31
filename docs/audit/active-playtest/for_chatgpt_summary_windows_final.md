# Touchline Windows Evidence Final Summary for ChatGPT

## Bottom line

**Green.**

Windows native desktop capture confirms the UI/UX polish pass on real hardware. All 35 verified screenshots pass scene/nav/anchor gates with 29 unique hashes and zero mislabeled cross-screen duplicates. The Cloud Agent's Yellow verdict was driven by Linux llvmpipe instability and missing local proof — not by open P1 UI defects.

## What Windows local capture proved

- Native Windows Godot GUI runs the full active-playtest matrix without llvmpipe SIGSEGV.
- Viewport PNG capture via `AuditCommandBridge` produces distinct, correctly labeled evidence per role and screen.
- Club selection is now visually evidenced (`manager-club-selection.png`) via New Career → CareerSetup → ChooseClub.
- Live-match pacing is evidenced by a timed screenshot sequence (kickoff, mid-match, full-time) plus post-match follow-through.
- Sidebar active-route, post-match layout, slot cards, dashboard copy, and partial player information all look correct on Windows PNGs.

## Screenshot reliability

| Metric | Cloud after polish | Windows local |
|---|---:|---:|
| Screenshot count | 34 | 35 |
| Unique hashes | 28 | 29 |
| Mislabeled screenshots | 0 | 0 |
| Role-specific captures | Yes | Yes |
| Training/scouting captures | Yes | Yes |
| Recruitment/job-market captures | Yes | Yes |
| Video captured | No | No |

Timed sequence: `docs/audit/active-playtest/screenshots/live_to_post_match_sequence/` (video skipped — no ffmpeg).

## Remaining UI issues

| Issue | Evidence path | Severity | Fix needed? |
|---|---|---|---|
| No true video for live-match pacing | `live_to_post_match_sequence/README.txt` | P3 | No (timed sequence sufficient for now) |
| Live→post-match not one continuous clip | Same sequence folder | P2 | No for private playtest; add ffmpeg later |
| Training/recruitment remain dashboard sections | `manager-training-scouting.png`, `manager-recruitment-contracts.png` | P3 | No (foundation depth per PRD) |
| Minor FORM card text density on player profile | `manager-player-profile.png` | P3 | Optional polish later |

## Final verdict

Touchline is **Green: ready for private human playtest** on Windows. Logic gates pass headlessly; visual evidence is now trustworthy on the target platform. Remaining gaps are motion capture tooling and foundation-depth management screens — not blockers for a focused human UX pass.

## Paste-back summary

Touchline moved from Yellow (Cloud) to Green on Windows. A full native desktop capture run produced 35 verified screenshots (29 unique hashes, zero mislabels) including new club-selection proof and a live-to-post-match timed sequence. All Yellow-audit UI fixes hold: sidebar routing, post-match density, slot cards, dashboard copy, and partial player information. Video was skipped (no ffmpeg); motion is covered by numbered sequence frames. No product code changes were needed beyond small Windows harness anchor fixes. Ready for private human playtest.
