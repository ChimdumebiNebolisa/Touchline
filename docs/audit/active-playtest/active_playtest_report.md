# Touchline Active Playtest Report

## Summary

Touchline behaved coherently during active use across Assistant Manager, Head Coach, and Manager foundations.
Role authority behavior, save/load continuity, and shared instant/live match-engine contracts validated successfully.

---

## Scenario table

| Scenario | Role | Steps completed | Result | Issues found | Fix needed |
|---|---|---:|---|---|---|
| Active role flow | Assistant Manager | 35 | Pass | No product issue | No |
| Active role flow | Head Coach | 35 | Pass | No product issue | No |
| Active role flow | Manager | 35 | Pass | No product issue | No |

---

## Screen and flow observations

| Screen/Flow | Expected | Observed | Evidence | Issue | Fix |
|---|---|---|---|---|---|
| Career setup + role pick | Role-specific setup supported | All 3 roles selected and loaded | `docs/audit/active-playtest/screenshots/*-career-setup.png` | None | N/A |
| Dashboard role authority | Role-specific authority text/actions | Assistant recommend-only text; Head Coach/Manager apply controls | `docs/audit/active-playtest/screenshots/*-dashboard.png`, `game/scripts/active_playtest_user_flow_check.gd` | None | N/A |
| Squad + profile | Navigation and profile inspection works | Squad/profile flow completed in all roles | `docs/audit/active-playtest/screenshots/*-squad-screen.png` | None | N/A |
| Tactics | Role-constrained interaction labels | Assistant recommendation wording vs save-plan wording confirmed | `docs/audit/active-playtest/screenshots/*-tactics-screen.png` | None | N/A |
| Fixtures + matchday | Route works and remains stable | Fixtures/matchday reached repeatedly | `docs/audit/active-playtest/screenshots/*-fixtures-nav.png`, `*-matchday-screen.png` | None | N/A |
| Instant + Live contract | Shared engine contract must hold | Stage 5 contract passed for all roles | `docs/audit/active-playtest/logs/headless-active-playtest-run.log` | None | N/A |

---

## Save/load observations

| Role | Save created | Reload worked | State preserved | Issue |
|---|---|---|---|---|
| Assistant Manager | Yes | Yes | Yes (role + career state) | None |
| Head Coach | Yes | Yes | Yes (role + career state) | None |
| Manager | Yes | Yes | Yes (role + career state) | None |

---

## Instant Sim vs Live Match

| Role | Instant result | Live result/timeline | Matched? | Issue |
|---|---|---|---|---|
| Assistant Manager | Contract-validated | Contract-validated | Yes | None |
| Head Coach | Contract-validated | Contract-validated | Yes | None |
| Manager | Contract-validated | Contract-validated | Yes | None |

---

# Deep UI Action Assertions

All assertions below were produced by the headless active check script
(`game/scripts/active_playtest_user_flow_check.gd`) and parse-verified by
the desktop harness (`docs/audit/active-playtest/scripts/active_desktop_playtest.py`).

Log evidence: `docs/audit/active-playtest/logs/headless-active-playtest-run.log`

| Role | Area | Before | Action | After | Expected | Passed? | Evidence |
|---|---|---|---|---|---|---|---|
| Assistant Manager | Training | Team cohesion | Request Pressing/Demanding | Team cohesion (unchanged) | Recommend only; focus unchanged | Yes | TrainingFocusName + NewsFeedSummary |
| Assistant Manager | Training | familiarity 59 | Recommend Pressing + advance week | familiarity 64 | Weekly effects run even without finalized plan | Yes | TacticalFamiliarityScore |
| Assistant Manager | Scouting | versatile midfielder | Recommend Full report | assignment unchanged | Recommend only; assignment unchanged | Yes | TrainingScoutingSummary |
| Assistant Manager | Tactics | 4-3-3 / Balanced | TryApply 3-5-2 High Press | 4-3-3 / Balanced (unchanged) | Recommend only; saved plan unchanged | Yes | TacticalFormation / TeamStyleName |
| Assistant Manager | Recruitment/contracts | Shortlisted target (no negotiation) | AttemptBasicRecruitmentAction | "Recommended by Assistant Manager; final authority sits with senior staff." | Recommend only | Yes | RecruitmentFoundationSummary |
| Assistant Manager | Post-match consequences | board 62 fan 66 squad 74 pressure 29 | ResolveCurrentMatchInstantly | board 56 fan 54 squad 71 pressure 36; 0-1 logged | Morale/pressure delta with explanation; news updates | Yes | BuildCareerPhaseSummary |
| Assistant Manager | Save/load persistence | focus=Team cohesion formation=4-3-3 | Save, reload, advance week | role=Assistant Manager; state preserved | Changed state survives reload; week continues | Yes | SaveSystem |
| Assistant Manager | Promises | active promises | Phase3 lifecycle contract | Fulfilled + Broken statuses in PromiseSummary | Lifecycle: fulfilled/broken with trust/pressure delta | Yes | ValidatePhase3PromiseLifecycleContract + PromiseSummary |
| Assistant Manager | Live Match consistency | instant result | ValidateStage5 contract | same match object | No split result or stale timeline | Yes | ValidateStage5MatchEngineAlignmentContract |
| Assistant Manager | Job market/career state | Job security: Stable | Stage8 contract + save/load | career/job state generated and persisted | Job market state generated and persisted | Yes | CareerMarketSummary |
| Head Coach | Training | Team cohesion | Apply Pressing/Demanding + advance week | Pressing; familiarity 59→67 | Football-side control; state delta | Yes | TacticalFamiliarityScore |
| Head Coach | Scouting | versatile midfielder (3 days) | Request Full report + 3 days | pressing winger (15 days; progress recorded) | Request/recommend scouting; progress visible | Yes | TrainingScoutingSummary |
| Head Coach | Tactics | 4-3-3 / Balanced | Apply 3-5-2 High Press | 3-5-2 / High Press | Football-side tactical control finalizes plan | Yes | TacticalFormation / TeamStyleName |
| Head Coach | Recruitment/contracts | Shortlisted target | AttemptBasicRecruitmentAction | "Requested by Head Coach; Director and board review required." | Request/recommend; not unilateral | Yes | RecruitmentFoundationSummary |
| Head Coach | Post-match consequences | board 62 fan 66 squad 74 pressure 40 | ResolveCurrentMatchInstantly | board 68 fan 77 squad 79 pressure 33; 1-0 logged | Morale/pressure delta with explanation; news updates | Yes | BuildCareerPhaseSummary |
| Head Coach | Save/load persistence | focus=Pressing formation=3-5-2 | Save, reload, advance week | role=Head Coach; state preserved | Changed state survives reload; week continues | Yes | SaveSystem |
| Head Coach | Promises | active promises | Phase3 lifecycle contract | Fulfilled + Broken in PromiseSummary | Lifecycle contract OK with trust/pressure delta | Yes | ValidatePhase3PromiseLifecycleContract + PromiseSummary |
| Head Coach | Live Match consistency | instant result | ValidateStage5 contract | same match object | No split result or stale timeline | Yes | ValidateStage5MatchEngineAlignmentContract |
| Head Coach | Job market/career state | Job security: Watched | Stage8 contract + save/load | career/job state generated and persisted | Job market state generated and persisted | Yes | CareerMarketSummary |
| Manager | Training | Team cohesion | Apply Pressing/Demanding + advance week | Pressing; familiarity 59→67 | Football-side control; state delta | Yes | TacticalFamiliarityScore |
| Manager | Scouting | versatile midfielder (3 days) | Request Full report + 3 days | pressing winger (15 days; progress recorded) | Open scouting assignment; progress visible | Yes | TrainingScoutingSummary |
| Manager | Tactics | 4-3-3 / Balanced | Apply 3-5-2 High Press | 3-5-2 / High Press | Football-side tactical control finalizes plan | Yes | TacticalFormation / TeamStyleName |
| Manager | Recruitment/contracts | Shortlisted target | AttemptBasicRecruitmentAction | Board blocks after agent/rival/Director/finance review | Attempt within board limits; not unilateral above budget | Yes | RecruitmentFoundationSummary |
| Manager | Post-match consequences | board 62 fan 66 squad 74 pressure 45 | ResolveCurrentMatchInstantly | board 69 fan 77 squad 80 pressure 38; 1-0 logged | Morale/pressure delta with explanation; news updates | Yes | BuildCareerPhaseSummary |
| Manager | Save/load persistence | focus=Pressing formation=3-5-2 | Save, reload, advance week | role=Manager; state preserved | Changed state survives reload; week continues | Yes | SaveSystem |
| Manager | Promises | active promises | Phase3 lifecycle contract | Fulfilled + Broken in PromiseSummary | Lifecycle contract OK with trust/pressure delta | Yes | ValidatePhase3PromiseLifecycleContract + PromiseSummary |
| Manager | Live Match consistency | instant result | ValidateStage5 contract | same match object | No split result or stale timeline | Yes | ValidateStage5MatchEngineAlignmentContract |
| Manager | Job market/career state | Job security: Watched | Stage8 contract + save/load | career/job state generated and persisted | Job market state generated and persisted | Yes | CareerMarketSummary |

---

### Authority summary

| Area | Assistant Manager | Head Coach | Manager |
|---|---|---|---|
| Training | Recommend only; focus unchanged | Applied directly; familiarity delta confirmed | Applied directly; familiarity delta confirmed |
| Scouting | Recommend only; assignment unchanged | Request opened; progress tracked | Assignment opened; progress tracked |
| Tactics | Recommend only; saved plan unchanged | Finalized; formation/style updated | Finalized; formation/style updated |
| Recruitment/contracts | Recommend only | Director/board review required | Attempt within board/budget limits |
| Promises | Foundation depth; lifecycle contract passed | Foundation depth; lifecycle contract passed | Foundation depth; lifecycle contract passed |

---

## Screenshots captured

- `docs/audit/active-playtest/screenshots/assistant-manager-*.png`
- `docs/audit/active-playtest/screenshots/head-coach-*.png`
- `docs/audit/active-playtest/screenshots/manager-*.png`

---

## Logs captured

- `docs/audit/active-playtest/logs/headless-active-playtest-run.log`
- `docs/audit/active-playtest/logs/headless-active-playtest-20260527-044157.log`
- `docs/audit/active-playtest/logs/full-godot-suite.log`
- `docs/audit/active-playtest/logs/active-playtest-run-20260527-044036.json`
- `docs/audit/active-playtest/logs/active-playtest-run-20260527-044322.json`

---

## Issues found

1. Audit automation path bug (first run): desktop script resolved repo root one level too shallow.
2. Duplicate `docs/docs/audit/...` artifacts from the initial bad path run.
3. Promise lifecycle contract's news-feed check used a hard count against a capped-at-8 feed — reported false failure. Fixed.

## Fixes made

1. Added active headless role-flow check script at `game/scripts/active_playtest_user_flow_check.gd`.
2. Added desktop automation harness at `docs/audit/active-playtest/scripts/active_desktop_playtest.py`.
3. Fixed script repo root resolution and screenshot backend call.
4. Moved/cleaned misplaced artifacts into `docs/audit/active-playtest/`.
5. Deepened `active_playtest_user_flow_check.gd` with 10 role-aware areas and real before/after state assertions.
6. Updated `active_desktop_playtest.py` to parse structured ACTIVE_PLAYTEST_ASSERT rows and include them in run JSON.
7. Fixed `ValidatePhase3PromiseLifecycleContract` news-check guard to also accept news already in `NewsFeedSummary` when the feed is at capacity, so a realistic playtest order (match, then promise check) does not false-fail.

---

## Final verdict

Green: active playtest passed with deep UI action assertions across all three roles.
