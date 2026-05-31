# Touchline Windows Capture Report

- Run date: `2026-05-30` (local Windows desktop)
- Git base: `e5c3c4b` + local harness fixes for club-selection and live-match anchors
- Final capture run: `docs/audit/active-playtest/logs/active-playtest-run-20260530-232523.json`
- Platform: Windows 10 native Godot 4.6.2 Mono GUI at `1280x720`
- Headless gate: `ACTIVE_PLAYTEST_USER_FLOW_PASS`

## Cloud vs Windows evidence

| Metric | Cloud after polish (`e5c3c4b`) | Windows local (this pass) |
|---|---:|---:|
| Screenshot count | 34 | 35 |
| Unique hashes | 28 | 29 |
| Mislabeled (cross-screen duplicate hashes) | 0 | 0 |
| Club-selection capture | No | Yes (`manager-club-selection.png`) |
| Live-match sequence | Partial (Linux GUI unstable) | Yes (kickoff, mid, FT) |
| Video | No | No (ffmpeg unavailable) |
| Timed sequence | No dedicated folder | Yes (`screenshots/live_to_post_match_sequence/`) |

Windows hashes differ from Cloud (expected: llvmpipe vs native D3D11). Trust comes from scene/nav/anchor assertions — all 35 captures `PASS`.

## Harness changes (Windows-only fixes)

1. **Club-selection flow** — New Career → CareerSetup (Manager role) → ChooseClub with anchor assertion before save.
2. **MainMenu wait anchors** — Use slot/resume anchors (`Slot 1`, `Riverton`, etc.) instead of button text not exported to audit state.
3. **Live-match kickoff anchors** — Accept `00'`, `01'`, fixture ` vs `, and `Kickoff` (clock advances on first frame).

## Coverage summary

| Area | Captures | Status |
|---|---|---|
| Assistant Manager dashboard/tactics/training/recruitment | 6 | PASS |
| Head Coach dashboard/tactics/training/recruitment | 6 | PASS |
| Manager role surfaces + squad/profile/fixtures/standings/matchday | 18 | PASS |
| Club selection (new career) | 1 | PASS |
| Live match timed sequence | 3 | PASS |
| Post-match review path | 5 | PASS |
| Main menu / save-load slot cards (all roles) | 6 | PASS |

## Visual verification (8 UI fix areas)

| Check | Evidence | Result |
|---|---|---|
| Sidebar active route correct | `manager-fixtures.png` highlights Fixtures; Matchday is green CTA | Pass |
| Post-match not clipped/empty | `manager-post-match.png` — dense stats, consequences, next action | Pass |
| Slot metadata no bad wrap | `manager-main-menu-slot-card.png` — labeled stacked rows | Pass |
| Dashboard copy action-oriented | Role dashboards show next-action bullets, not prose blocks | Pass |
| Partial-info cues visible | `manager-player-profile.png` — Profile Confidence, Known/Estimated/Unknown | Pass |
| Role-specific context | AM/HC/Manager training-scouting button labels differ per role | Pass |
| Training/scouting readable | `*-training-scouting.png` for all three roles | Pass |
| Recruitment/job-market readable | `*-recruitment-contracts.png`, `manager-job-market.png` | Pass |
| Club selection | `manager-club-selection.png` — shortlist + brief panel | Pass |

No product UI code changes were required; only harness anchor fixes.

## Video / motion proof

- **Video:** Skipped — `ffmpeg` not installed on this Windows host.
- **Fallback:** `docs/audit/active-playtest/screenshots/live_to_post_match_sequence/` with 5 numbered frames and `README.txt` mapping sources.

## Archive traceability

Prior sets archived at:

- `docs/audit/active-playtest/archive/screenshots-20260530-231356`
- `docs/audit/active-playtest/archive/screenshots-20260530-232332`
- `docs/audit/active-playtest/archive/screenshots-20260530-232523`

## Bottom line

Windows native capture completes the evidence gap left by the Cloud Agent's unstable Linux GUI session. Screenshot pipeline is trustworthy (35 labeled / 29 unique, zero cross-screen hash collisions). UI fixes from the Yellow audit hold on Windows.
