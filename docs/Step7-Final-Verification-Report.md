# Step 7 Final Verification Report

## Scope

This report closes Step 7 by capturing calibration evidence, regression evidence, and rule-check evidence for the current release-candidate baseline.

Baseline commit during report generation: `ff9ae0b`.

## Commands Executed

Executed from repository root on 2026-04-06:

- `npm run manual:step7:regression`
- `npm test`
- `npm run typecheck`
- `npm run lint`
- `npm run build`

All commands completed successfully.

## Calibration Metrics Snapshot

Source: `npm run manual:step7:calibration`.

| Metric | Value | Threshold | Result |
| --- | --- | --- | --- |
| Average goals per match | 3.082 | 1.8 to 4.2 | PASS |
| Upset rate (decisive matches) | 0.276 | 0.08 to 0.45 | PASS |
| Late-goal share (>=75 minute) | 0.135 | 0.08 to 0.5 | PASS |
| Red-card samples | 213 | >=20 | PASS |
| Red-card impact (points penalty) | 0.171 | >0 | PASS |
| Elite youth rate | 0.0249 | <=0.06 | PASS |
| High-potential youth rate | 0.1635 | <=0.35 | PASS |

## Regression Suite Coverage

Source: `npm run manual:step7:regression`.

The regression suite reruns these bounded checks:

- Step 1 vertical-slice manual check
- Step 2 board-context manual check
- Step 4 academy/pathway manual check
- Step 5 shadow-world manual check
- Step 6 save/load manual check
- Step 6 multi-season save/load manual check
- Step 7 calibration manual check

Result: PASS.

## Must-Have Flow Evidence Map

| Must-have flow | Evidence artifact(s) | Status |
| --- | --- | --- |
| Shared instant/live engine parity | `packages/sim-core/tests/matchModeParity.test.ts` | PASS |
| Tactical controls affect engine outputs | `packages/sim-core/tests/matchTacticalImpact.test.ts`, `packages/sim-core/tests/matchPreparationCommands.test.ts` | PASS |
| Contextual board evaluation and sack pressure | `packages/sim-core/tests/boardContext.test.ts`, `packages/sim-core/tests/seasonBoard.integration.test.ts` | PASS |
| Non-fee-only transfer logic | `packages/sim-core/tests/transferEngine.test.ts`, `packages/sim-core/tests/negotiation.test.ts` | PASS |
| Reputation downstream impact | `packages/sim-core/tests/reputation.test.ts`, `packages/sim-core/tests/negotiationCalibration.test.ts` | PASS |
| Academy rarity and pathway pressure | `packages/sim-core/tests/academyIntake.test.ts`, `scripts/manual-check-step4.mjs` | PASS |
| Shadow world top-two-deep and rest-shadow contract | `packages/sim-core/tests/shadowLeagues.test.ts`, `packages/sim-core/tests/shadowWorld.integration.test.ts`, `scripts/manual-check-step5-shadow-world.mjs` | PASS |
| Save/load continuity without drift | `packages/save/tests/saveCodec.test.ts`, `packages/save/tests/slotStore.integration.test.ts`, `scripts/manual-check-step6-save-load.mjs`, `scripts/manual-check-step6-multiseason.mjs` | PASS |

## Guardrail Outcome

No guardrail violations were observed in the current verification run:

- one-engine rule preserved
- no fee-only transfer resolution
- contextual board behavior retained
- youth rarity maintained in calibration
- save/load continuity verified under deterministic progression

## Step 7 Exit Decision

Step 7 exit criteria are satisfied for the current baseline:

- calibration suite artifact exists and passes
- regression suite artifact exists and passes
- final verification report artifact exists
- full test/typecheck/lint/build gates pass

Recommendation: mark Step 7 complete.
