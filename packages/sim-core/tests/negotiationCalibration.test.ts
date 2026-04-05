import { describe, expect, it } from "vitest";

import {
  buildReputationBandOutcomeDeltaSummary,
  buildEqualFeeNegotiationExplainabilityArtifact,
  buildTransferNegotiationLogSamples,
  summarizePromiseTrustImpactByReputationBand,
  summarizeTransferOutcomesByReputationBand
} from "../src/index.js";
import {
  calibrationBaseContext,
  calibrationEqualFeeContexts,
  calibrationLogContexts,
  calibrationOutcomeVariants,
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
    const outcomeSummary = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationOutcomeVariants
    );
    const outcomeDeltaSummary = buildReputationBandOutcomeDeltaSummary(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      82,
      28,
      calibrationOutcomeVariants
    );

    const highReputationBand = outcomeSummary.bands.find((band) => band.managerReputation === 82);
    const lowReputationBand = outcomeSummary.bands.find((band) => band.managerReputation === 28);

    expect(highReputationBand).toBeDefined();
    expect(lowReputationBand).toBeDefined();
    expect(highReputationBand!.boardBlockCount).toBe(0);
    expect(lowReputationBand!.boardBlockCount).toBe(0);
    expect(highReputationBand!.sportingDirectorBlockCount).toBeGreaterThan(0);
    expect(highReputationBand!.playerBlockCount).toBe(0);
    expect(lowReputationBand!.playerBlockCount).toBeGreaterThan(highReputationBand!.playerBlockCount);
    expect(lowReputationBand!.sportingDirectorBlockCount).toBeLessThan(highReputationBand!.sportingDirectorBlockCount);

    const normalizedSample = {
      promiseTrustBands: promiseTrust.bands.map((band) => ({
        managerReputation: band.managerReputation,
        attempts: band.attempts,
        intactTrustAcceptedCount: band.intactTrustAcceptedCount,
        brokenTrustAcceptedCount: band.brokenTrustAcceptedCount,
        acceptanceRateDelta: Number(band.acceptanceRateDelta.toFixed(2))
      })),
      promiseTrustSummary: promiseTrust.conciseSummary,
      reputationOutcomeSummary: outcomeSummary.conciseSummary,
      reputationOutcomeDeltaSummary: outcomeDeltaSummary.conciseSummary,
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
        "reputationOutcomeDeltaSummary": "Reputation 82 vs 28: accepted delta 2, rate delta 0.50, score delta 2.75, board block delta 0, sporting-director block delta 2, player block delta -4.",
        "reputationOutcomeSummary": [
          "Manager reputation 82: accepted 2/4 (0.50), average score 1.06, board blocks 0, sporting-director blocks 2, player blocks 0.",
          "Manager reputation 62: accepted 2/4 (0.50), average score 0.04, board blocks 0, sporting-director blocks 2, player blocks 0.",
          "Manager reputation 28: accepted 0/4 (0.00), average score -1.69, board blocks 0, sporting-director blocks 0, player blocks 4.",
        ],
      }
    `);
  });

  it("keeps reputation-band delta artifacts deterministic across repeated fixed fixture evaluations", () => {
    const repeatedRuns = Array.from({ length: 12 }, () =>
      buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      )
    );

    const baseline = repeatedRuns[0];
    for (const run of repeatedRuns.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(new Set(repeatedRuns.map((run) => run.conciseSummary)).size).toBe(1);
    expect(baseline.boardBlockDelta).toBe(0);
    expect(baseline.acceptedCountDelta).toBeGreaterThan(0);
    expect(baseline.sportingDirectorBlockDelta).toBeGreaterThan(0);
    expect(baseline.playerBlockDelta).toBeLessThan(0);
  });
});