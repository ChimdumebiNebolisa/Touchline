# Step 7 Design Rule Checklist

## Purpose

This checklist is the Step 7 gate for preventing simulation drift, causeless outcomes, and scope creep.

Use it as a release gate artifact with concrete evidence links to tests and manual scripts already present in this repository.

## Verification Baseline Commands

Run these from repository root:

- `npm test`
- `npm run typecheck`
- `npm run lint`
- `npm run build`

If any command fails, Step 7 is not complete.

## Design Rule Checklist

| Rule | Required Evidence | Current Artifact(s) | Status |
| --- | --- | --- | --- |
| One shared match engine powers instant and live modes | Parity test and deterministic outputs | `packages/sim-core/tests/matchModeParity.test.ts` | PASS |
| Tactical controls change match outcomes through sim-core, not UI-only state | Tactical impact + command validation | `packages/sim-core/tests/matchTacticalImpact.test.ts`, `packages/sim-core/tests/matchPreparationCommands.test.ts` | PASS |
| Board evaluation is context-sensitive, not table-only | Board context and season board integration | `packages/sim-core/tests/boardContext.test.ts`, `packages/sim-core/tests/seasonBoard.integration.test.ts` | PASS |
| Transfers are not fee-only and include human factors | Transfer engine and negotiation behavior | `packages/sim-core/tests/transferEngine.test.ts`, `packages/sim-core/tests/negotiation.test.ts`, `packages/sim-core/tests/negotiationCalibration.test.ts` | PASS |
| Reputation influences downstream systems | Reputation behavior tests + negotiation calibration | `packages/sim-core/tests/reputation.test.ts`, `packages/sim-core/tests/negotiationCalibration.test.ts` | PASS |
| Youth quality remains rare and pathways are meaningful | Academy intake calibration and pathway tests | `packages/sim-core/tests/academyIntake.test.ts`, `scripts/manual-check-step4.mjs` | PASS |
| Top-two-deep and rest-shadow contract holds | Shadow league integration and contract checks | `packages/sim-core/tests/shadowLeagues.test.ts`, `packages/sim-core/tests/shadowWorld.integration.test.ts`, `scripts/manual-check-step5-shadow-world.mjs` | PASS |
| Save/load preserves long-run continuity and deterministic continuation | Codec tests, slot integration tests, multi-season manual checks | `packages/save/tests/saveCodec.test.ts`, `packages/save/tests/slotStore.integration.test.ts`, `scripts/manual-check-step6-save-load.mjs`, `scripts/manual-check-step6-multiseason.mjs` | PASS |
| Country content pack contract remains valid and scalable without core rewrites | Country pack contract and integration tests | `packages/sim-core/tests/countryPack.test.ts`, `packages/sim-core/tests/firstCountryPack.integration.test.ts` | PASS |
| No silent persistence failures | Explicit error-path coverage for malformed/unsupported/missing payloads | `packages/save/tests/saveCodec.test.ts`, `packages/save/tests/slotStore.test.ts` | PASS |

## Step 7 Required Additions Before Completion

The following Step 7 outputs are still required by `docs/Plan.md` and are not complete yet:

1. Calibration suite artifact with tracked metrics for:
   - goal rates
   - upset rates
   - red-card impact
   - late-goal distribution
   - youth rarity
2. Regression suite artifact that can be rerun after balancing passes.
3. Final verification report artifact that summarizes all must-have flows and evidence.

## Gate Rule

Do not mark Step 7 complete until all required additions above are implemented and verified with reproducible artifacts.
