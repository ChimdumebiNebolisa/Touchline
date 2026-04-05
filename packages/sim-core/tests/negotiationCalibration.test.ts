import { describe, expect, it } from "vitest";

import {
  buildEqualFeeNegotiationExplainabilityArtifact,
  buildTransferNegotiationLogSamples,
  summarizePromiseTrustImpactByReputationBand
} from "../src/index.js";
import {
  calibrationBaseContext,
  calibrationEqualFeeContexts,
  calibrationLogContexts,
  calibrationPromiseVariants,
  calibrationReputationBands,
  calibrationTarget
} from "./fixtures/negotiationFixtures.js";

describe("transfer negotiation calibration samples", () => {
  it("produces deterministic sample outputs for Step 3 calibration checks", () => {
    const promiseTrust = summarizePromiseTrustImpactByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationPromiseVariants
    );

    const equalFee = buildEqualFeeNegotiationExplainabilityArtifact(
      calibrationTarget,
      calibrationEqualFeeContexts.first,
      calibrationEqualFeeContexts.second
    );

    const negotiationLog = buildTransferNegotiationLogSamples(calibrationTarget, calibrationLogContexts);

    const normalizedSample = {
      promiseTrustBands: promiseTrust.bands.map((band) => ({
        managerReputation: band.managerReputation,
        attempts: band.attempts,
        intactTrustAcceptedCount: band.intactTrustAcceptedCount,
        brokenTrustAcceptedCount: band.brokenTrustAcceptedCount,
        acceptanceRateDelta: Number(band.acceptanceRateDelta.toFixed(2))
      })),
      promiseTrustSummary: promiseTrust.conciseSummary,
      equalFeeSummary: equalFee.conciseSummary,
      equalFeePrimaryDrivers: equalFee.primaryNonFeeDrivers,
      negotiationLog: negotiationLog.map((entry) => ({
        attemptId: entry.attemptId,
        accepted: entry.accepted,
        blockingActor: entry.blockingActor,
        score: Number(entry.score.toFixed(2))
      }))
    };

    expect(normalizedSample).toMatchInlineSnapshot(`
      {
        "equalFeePrimaryDrivers": [
          "Pathway clarity +0.89",
          "Squad competition +0.70",
        ],
        "equalFeeSummary": "Equal-fee comparison aligned (accepted); primary contextual deltas: Pathway clarity +0.89, Squad competition +0.70.",
        "negotiationLog": [
          {
            "accepted": true,
            "attemptId": "attempt-1",
            "blockingActor": "none",
            "score": 3.5,
          },
          {
            "accepted": true,
            "attemptId": "attempt-2",
            "blockingActor": "none",
            "score": 2.19,
          },
          {
            "accepted": false,
            "attemptId": "attempt-3",
            "blockingActor": "player",
            "score": -4.83,
          },
        ],
        "promiseTrustBands": [
          {
            "acceptanceRateDelta": 1,
            "attempts": 4,
            "brokenTrustAcceptedCount": 0,
            "intactTrustAcceptedCount": 4,
            "managerReputation": 82,
          },
          {
            "acceptanceRateDelta": 1,
            "attempts": 4,
            "brokenTrustAcceptedCount": 0,
            "intactTrustAcceptedCount": 4,
            "managerReputation": 62,
          },
          {
            "acceptanceRateDelta": 0,
            "attempts": 4,
            "brokenTrustAcceptedCount": 0,
            "intactTrustAcceptedCount": 0,
            "managerReputation": 28,
          },
        ],
        "promiseTrustSummary": [
          "Manager reputation 82: intact trust 4/4 (1.00) vs broken trust 0/4 (0.00), delta 1.00.",
          "Manager reputation 62: intact trust 4/4 (1.00) vs broken trust 0/4 (0.00), delta 1.00.",
          "Manager reputation 28: intact trust 0/4 (0.00) vs broken trust 0/4 (0.00), delta 0.00.",
        ],
      }
    `);
  });
});