# Touchline Active Playtest Report

## Summary

Touchline behaved coherently during active use across Assistant Manager, Head Coach, and Manager foundations.
Role authority behavior, save/load continuity, and shared instant/live match-engine contracts validated successfully.

## Scenario table

| Scenario | Role | Steps completed | Result | Issues found | Fix needed |
|---|---|---:|---|---|---|
| Active role flow | Assistant Manager | 35 | Pass | No product issue | No |
| Active role flow | Head Coach | 35 | Pass | No product issue | No |
| Active role flow | Manager | 35 | Pass | No product issue | No |

## Screen and flow observations

| Screen/Flow | Expected | Observed | Evidence | Issue | Fix |
|---|---|---|---|---|---|
| Career setup + role pick | Role-specific setup supported | All 3 roles selected and loaded | `docs/audit/active-playtest/screenshots/*-career-setup.png` | None | N/A |
| Dashboard role authority | Role-specific authority text/actions | Assistant recommend-only text; Head Coach/Manager apply controls | `docs/audit/active-playtest/screenshots/*-dashboard.png`, `game/scripts/active_playtest_user_flow_check.gd` | None | N/A |
| Squad + profile | Navigation and profile inspection works | Squad/profile flow completed in all roles | `docs/audit/active-playtest/screenshots/*-squad-screen.png` | None | N/A |
| Tactics | Role-constrained interaction labels | Assistant recommendation wording vs save-plan wording confirmed | `docs/audit/active-playtest/screenshots/*-tactics-screen.png` | None | N/A |
| Fixtures + matchday | Route works and remains stable | Fixtures/matchday reached repeatedly | `docs/audit/active-playtest/screenshots/*-fixtures-nav.png`, `*-matchday-screen.png` | None | N/A |
| Instant + Live contract | Shared engine contract must hold | Stage 5 contract passed for all roles | `docs/audit/active-playtest/logs/headless-active-playtest-run.log` | None | N/A |

## Save/load observations

| Role | Save created | Reload worked | State preserved | Issue |
|---|---|---|---|---|
| Assistant Manager | Yes | Yes | Yes (role + career state) | None |
| Head Coach | Yes | Yes | Yes (role + career state) | None |
| Manager | Yes | Yes | Yes (role + career state) | None |

## Instant Sim vs Live Match

| Role | Instant result | Live result/timeline | Matched? | Issue |
|---|---|---|---|---|
| Assistant Manager | Contract-validated | Contract-validated | Yes | None |
| Head Coach | Contract-validated | Contract-validated | Yes | None |
| Manager | Contract-validated | Contract-validated | Yes | None |

## Screenshots captured

- `docs/audit/active-playtest/screenshots/assistant-manager-*.png`
- `docs/audit/active-playtest/screenshots/head-coach-*.png`
- `docs/audit/active-playtest/screenshots/manager-*.png`

## Logs captured

- `docs/audit/active-playtest/logs/headless-active-playtest-run.log`
- `docs/audit/active-playtest/logs/headless-active-playtest-20260527-044157.log`
- `docs/audit/active-playtest/logs/full-godot-suite.log`
- `docs/audit/active-playtest/logs/active-playtest-run-20260527-044036.json`
- `docs/audit/active-playtest/logs/active-playtest-run-20260527-044322.json`

## Issues found

1. Audit automation path bug: desktop script resolved repo root one level too shallow, causing invalid Godot `--path`.
2. Duplicate `docs/docs/audit/...` artifacts from the initial bad path run.

## Fixes made

1. Added active headless role-flow check script at `game/scripts/active_playtest_user_flow_check.gd`.
2. Added desktop automation harness at `docs/audit/active-playtest/scripts/active_desktop_playtest.py`.
3. Fixed script repo root resolution and screenshot backend call.
4. Moved/cleaned misplaced artifacts into `docs/audit/active-playtest/`.

## Final verdict

Green: active playtest passed, playable foundation is coherent
