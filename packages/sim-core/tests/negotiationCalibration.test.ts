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
import {
  buildLoanPathFixtureWindow,
  buildSeasonAcademyOutputSummaryArtifact,
  buildTransferPressureComparisonArtifact,
  buildTransferVariantsFromAcademySummary
} from "./fixtures/academyPathwayFixtures.js";

describe("transfer negotiation calibration samples", () => {
  it("builds a focused transfer-pressure comparison artifact from matched-wage pathway-bias windows", () => {
    const comparisonArtifact = buildTransferPressureComparisonArtifact({
      clubId: "club-transfer-pressure-comparison",
      seed: 844,
      startSeasonYear: 2096,
      seasons: 8,
      academyQuality: 0.69,
      intakeSize: 10,
      lowPathwayBias: 0.35,
      highPathwayBias: 0.75
    });

    const lowPathwayOutcome = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      [62],
      comparisonArtifact.lowPathwayVariants
    );

    const highPathwayOutcome = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      [62],
      comparisonArtifact.highPathwayVariants
    );

    expect(comparisonArtifact.highPathwaySummary.totalLoanRecommendations).toBeGreaterThan(
      comparisonArtifact.lowPathwaySummary.totalLoanRecommendations
    );
    expect(lowPathwayOutcome.bands[0].attempts).toBe(comparisonArtifact.lowPathwaySummary.rows.length);
    expect(highPathwayOutcome.bands[0].attempts).toBe(comparisonArtifact.highPathwaySummary.rows.length);
    expect(lowPathwayOutcome.bands[0].averageScore).not.toBe(highPathwayOutcome.bands[0].averageScore);
    expect(comparisonArtifact.conciseSummary[0]).toContain("matched wage context");

    const averagePathwayClarity = (values: Array<{ pathwayClarity: number }>) =>
      values.reduce((sum, value) => sum + value.pathwayClarity, 0) / values.length;
    const averageCompetition = (values: Array<{ squadCompetition: number }>) =>
      values.reduce((sum, value) => sum + value.squadCompetition, 0) / values.length;

    const lowBlockage = comparisonArtifact.lowPathwaySummary.averageBlockageScore;
    const highBlockage = comparisonArtifact.highPathwaySummary.averageBlockageScore;
    const lowAveragePathwayClarity = averagePathwayClarity(comparisonArtifact.lowPathwayVariants);
    const highAveragePathwayClarity = averagePathwayClarity(comparisonArtifact.highPathwayVariants);
    const lowAverageCompetition = averageCompetition(comparisonArtifact.lowPathwayVariants);
    const highAverageCompetition = averageCompetition(comparisonArtifact.highPathwayVariants);

    expect(highBlockage).not.toBe(lowBlockage);

    if (highBlockage > lowBlockage) {
      expect(highAveragePathwayClarity).toBeLessThan(lowAveragePathwayClarity);
      expect(highAverageCompetition).toBeGreaterThan(lowAverageCompetition);
    } else {
      expect(highAveragePathwayClarity).toBeGreaterThan(lowAveragePathwayClarity);
      expect(highAverageCompetition).toBeLessThan(lowAverageCompetition);
    }

    expect(comparisonArtifact.conciseSummary).toMatchInlineSnapshot(`
      [
        "Transfer-pressure comparison uses matched wage context and pathway-bias windows 0.35 vs 0.75.",
        "Loan recommendations: low-bias 0, high-bias 3.",
        "Average blockage score: low-bias 0.01, high-bias 0.66.",
      ]
    `);
  });

  it("wires academy pathway-pressure summaries into transfer calibration sample variants", () => {
    const lowPathwayBiasSummary = buildSeasonAcademyOutputSummaryArtifact(
      buildLoanPathFixtureWindow({
        clubId: "club-transfer-link",
        seed: 844,
        startSeasonYear: 2096,
        seasons: 8,
        academyQuality: 0.69,
        pathwayBias: 0.35,
        intakeSize: 10
      })
    );

    const highPathwayBiasSummary = buildSeasonAcademyOutputSummaryArtifact(
      buildLoanPathFixtureWindow({
        clubId: "club-transfer-link",
        seed: 844,
        startSeasonYear: 2096,
        seasons: 8,
        academyQuality: 0.69,
        pathwayBias: 0.75,
        intakeSize: 10
      })
    );

    const lowPathwayVariants = buildTransferVariantsFromAcademySummary(lowPathwayBiasSummary);
    const highPathwayVariants = buildTransferVariantsFromAcademySummary(highPathwayBiasSummary);

    const lowPathwayOutcome = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      [62],
      lowPathwayVariants
    );

    const highPathwayOutcome = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      [62],
      highPathwayVariants
    );

    expect(lowPathwayOutcome.bands[0].attempts).toBe(lowPathwayBiasSummary.rows.length);
    expect(highPathwayOutcome.bands[0].attempts).toBe(highPathwayBiasSummary.rows.length);
    expect(lowPathwayOutcome.bands[0].averageScore).not.toBe(highPathwayOutcome.bands[0].averageScore);
    expect(lowPathwayOutcome.conciseSummary[0]).toContain("Manager reputation 62");
    expect(highPathwayOutcome.conciseSummary[0]).toContain("Manager reputation 62");
  });

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

  it("keeps normalized calibration sample structure deterministic across repeated runs", () => {
    const buildNormalizedCalibrationSample = () => {
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

      return {
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
    };

    const firstRun = buildNormalizedCalibrationSample();
    const secondRun = buildNormalizedCalibrationSample();

    expect(secondRun).toEqual(firstRun);
    expect(Object.keys(firstRun)).toEqual([
      "promiseTrustBands",
      "promiseTrustSummary",
      "reputationOutcomeSummary",
      "reputationOutcomeDeltaSummary",
      "equalFeeSummary",
      "equalFeePrimaryDrivers",
      "negotiationLog"
    ]);

    for (const logEntry of firstRun.negotiationLog) {
      expect(Object.keys(logEntry)).toEqual(["attemptId", "accepted", "blockingActor", "score"]);
    }
  });

  it("keeps equal-fee explainability artifact field ordering and labels deterministic", () => {
    const firstArtifact = buildEqualFeeNegotiationExplainabilityArtifact(
      calibrationTarget,
      calibrationEqualFeeContexts.first,
      calibrationEqualFeeContexts.second
    );
    const secondArtifact = buildEqualFeeNegotiationExplainabilityArtifact(
      calibrationTarget,
      calibrationEqualFeeContexts.first,
      calibrationEqualFeeContexts.second
    );

    expect(secondArtifact).toEqual(firstArtifact);
    expect(Object.keys(firstArtifact)).toEqual([
      "comparison",
      "firstDemandBreakdown",
      "secondDemandBreakdown",
      "demandDelta",
      "primaryNonFeeDrivers",
      "nonFeeContextDroveDivergence",
      "conciseSummary"
    ]);
    expect(firstArtifact.comparison.changedContextFactors).toEqual(["squadCompetition", "pathwayClarity"]);

    for (const label of firstArtifact.primaryNonFeeDrivers) {
      expect(label).toMatch(/^[A-Za-z\- ]+ [+-]\d+\.\d{2}$/);
    }

    expect(firstArtifact.conciseSummary).toContain(firstArtifact.primaryNonFeeDrivers.join(", "));
  });

  it("keeps repeated-run explainability summaries stable across transfer artifacts", () => {
    const repeatedSummarySnapshots = Array.from({ length: 10 }, () => {
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

      const equalFee = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      );

      return {
        promiseTrustSummary: promiseTrust.conciseSummary.join("|"),
        outcomeSummary: outcomeSummary.conciseSummary.join("|"),
        outcomeDeltaSummary: outcomeDeltaSummary.conciseSummary,
        equalFeeSummary: equalFee.conciseSummary
      };
    });

    expect(new Set(repeatedSummarySnapshots.map((snapshot) => snapshot.promiseTrustSummary)).size).toBe(1);
    expect(new Set(repeatedSummarySnapshots.map((snapshot) => snapshot.outcomeSummary)).size).toBe(1);
    expect(new Set(repeatedSummarySnapshots.map((snapshot) => snapshot.outcomeDeltaSummary)).size).toBe(1);
    expect(new Set(repeatedSummarySnapshots.map((snapshot) => snapshot.equalFeeSummary)).size).toBe(1);
  });

  it("keeps explainability summary bundle keys and value shapes stable across runs", () => {
    const buildSummaryBundle = () => {
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
      const equalFee = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      );

      return {
        promiseTrustSummary: promiseTrust.conciseSummary,
        outcomeSummary: outcomeSummary.conciseSummary,
        outcomeDeltaSummary: outcomeDeltaSummary.conciseSummary,
        equalFeeSummary: equalFee.conciseSummary
      };
    };

    const repeatedBundles = Array.from({ length: 6 }, () => buildSummaryBundle());
    const expectedKeys = [
      "promiseTrustSummary",
      "outcomeSummary",
      "outcomeDeltaSummary",
      "equalFeeSummary"
    ];

    for (const bundle of repeatedBundles) {
      expect(Object.keys(bundle)).toEqual(expectedKeys);
      expect(Array.isArray(bundle.promiseTrustSummary)).toBe(true);
      expect(Array.isArray(bundle.outcomeSummary)).toBe(true);
      expect(typeof bundle.outcomeDeltaSummary).toBe("string");
      expect(typeof bundle.equalFeeSummary).toBe("string");
    }
  });

  it("keeps explainability summary labels stable across repeated runs", () => {
    const repeatedSummaries = Array.from({ length: 8 }, () => {
      const equalFee = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
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

      return {
        equalFeeSummary: equalFee.conciseSummary,
        deltaSummary: outcomeDeltaSummary.conciseSummary
      };
    });

    const baseline = repeatedSummaries[0];
    for (const summary of repeatedSummaries.slice(1)) {
      expect(summary).toEqual(baseline);
    }

    expect(baseline.equalFeeSummary).toMatch(
      /^Equal-fee comparison (aligned|diverged) \((accepted|rejected)( vs (accepted|rejected))?\); primary (contextual deltas|non-fee drivers): .+\.$/
    );
    expect(baseline.deltaSummary).toMatch(
      /^Reputation \d+ vs \d+: accepted delta -?\d+, rate delta -?\d+\.\d{2}, score delta -?\d+\.\d{2}, board block delta -?\d+, sporting-director block delta -?\d+, player block delta -?\d+\.$/
    );
  });

  it("keeps explainability summary phrase ordering deterministic in regression snapshots", () => {
    const equalFeeSummary = buildEqualFeeNegotiationExplainabilityArtifact(
      calibrationTarget,
      calibrationEqualFeeContexts.first,
      calibrationEqualFeeContexts.second
    ).conciseSummary;
    const deltaSummary = buildReputationBandOutcomeDeltaSummary(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      82,
      28,
      calibrationOutcomeVariants
    ).conciseSummary;

    const equalFeeSegments = [
      "Equal-fee comparison",
      "primary",
      ":"
    ];
    const deltaSegments = [
      "accepted delta",
      "rate delta",
      "score delta",
      "board block delta",
      "sporting-director block delta",
      "player block delta"
    ];

    let equalFeeCursor = -1;
    for (const segment of equalFeeSegments) {
      const nextCursor = equalFeeSummary.indexOf(segment);
      expect(nextCursor).toBeGreaterThan(equalFeeCursor);
      equalFeeCursor = nextCursor;
    }

    let deltaCursor = -1;
    for (const segment of deltaSegments) {
      const nextCursor = deltaSummary.indexOf(segment);
      expect(nextCursor).toBeGreaterThan(deltaCursor);
      deltaCursor = nextCursor;
    }
  });

  it("keeps explainability summary punctuation stable across repeated runs", () => {
    const repeatedSummaries = Array.from({ length: 6 }, () => ({
      equalFee: buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).conciseSummary,
      delta: buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      ).conciseSummary
    }));

    const baseline = repeatedSummaries[0];
    for (const summary of repeatedSummaries.slice(1)) {
      expect(summary).toEqual(baseline);
    }

    expect(baseline.equalFee.endsWith(".")).toBe(true);
    expect(baseline.equalFee.includes(": ")).toBe(true);
    expect((baseline.equalFee.match(/,/g) ?? []).length).toBeGreaterThanOrEqual(1);

    expect(baseline.delta.endsWith(".")).toBe(true);
    expect((baseline.delta.match(/delta/g) ?? []).length).toBe(6);
    expect((baseline.delta.match(/,/g) ?? []).length).toBeGreaterThanOrEqual(5);
  });

  it("keeps explainability summary token counts stable across repeated runs", () => {
    const buildSummaryTokenCounts = () => {
      const equalFeeSummary = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).conciseSummary;
      const deltaSummary = buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      ).conciseSummary;

      return {
        equalFeeTokenCount: equalFeeSummary.split(/\s+/).filter(Boolean).length,
        deltaTokenCount: deltaSummary.split(/\s+/).filter(Boolean).length
      };
    };

    const runs = Array.from({ length: 6 }, () => buildSummaryTokenCounts());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.equalFeeTokenCount).toBeGreaterThan(8);
    expect(baseline.deltaTokenCount).toBeGreaterThan(14);
  });

  it("keeps explainability summary delimiters stable across repeated runs", () => {
    const buildDelimiterProfile = () => {
      const equalFeeSummary = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).conciseSummary;
      const deltaSummary = buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      ).conciseSummary;

      return {
        equalFeeColonCount: (equalFeeSummary.match(/:/g) ?? []).length,
        equalFeeCommaCount: (equalFeeSummary.match(/,/g) ?? []).length,
        equalFeeSemicolonCount: (equalFeeSummary.match(/;/g) ?? []).length,
        deltaCommaCount: (deltaSummary.match(/,/g) ?? []).length,
        deltaColonCount: (deltaSummary.match(/:/g) ?? []).length,
        deltaSemicolonCount: (deltaSummary.match(/;/g) ?? []).length
      };
    };

    const runs = Array.from({ length: 6 }, () => buildDelimiterProfile());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.equalFeeColonCount).toBe(1);
    expect(baseline.equalFeeSemicolonCount).toBe(1);
    expect(baseline.equalFeeCommaCount).toBeGreaterThanOrEqual(1);
    expect(baseline.deltaColonCount).toBe(1);
    expect(baseline.deltaSemicolonCount).toBe(0);
    expect(baseline.deltaCommaCount).toBeGreaterThanOrEqual(5);
  });

  it("keeps explainability-summary segment counts stable in regression snapshots", () => {
    const buildSegmentCounts = () => {
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
      const equalFeeSummary = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).conciseSummary;
      const deltaSummary = buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      ).conciseSummary;

      const equalFeeDriverClause = equalFeeSummary.split(":").at(1) ?? "";

      return {
        promiseTrustLineCount: promiseTrust.conciseSummary.length,
        outcomeSummaryLineCount: outcomeSummary.conciseSummary.length,
        equalFeeSemicolonSegmentCount: equalFeeSummary.split(";").length,
        equalFeeDriverSegmentCount: equalFeeDriverClause
          .split(",")
          .map((segment) => segment.trim())
          .filter(Boolean).length,
        deltaMetricSegmentCount: deltaSummary
          .split(",")
          .map((segment) => segment.trim())
          .filter(Boolean).length
      };
    };

    const runs = Array.from({ length: 8 }, () => buildSegmentCounts());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.promiseTrustLineCount).toBe(3);
    expect(baseline.outcomeSummaryLineCount).toBe(3);
    expect(baseline.equalFeeSemicolonSegmentCount).toBe(2);
    expect(baseline.equalFeeDriverSegmentCount).toBe(2);
    expect(baseline.deltaMetricSegmentCount).toBe(6);
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

  it("keeps concise equal-fee explainability summaries deterministic across repeated runs", () => {
    const repeatedArtifacts = Array.from({ length: 8 }, () =>
      buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      )
    );

    const baseline = repeatedArtifacts[0];
    for (const artifact of repeatedArtifacts.slice(1)) {
      expect(artifact.conciseSummary).toBe(baseline.conciseSummary);
      expect(artifact.primaryNonFeeDrivers).toEqual(baseline.primaryNonFeeDrivers);
      expect(artifact.nonFeeContextDroveDivergence).toBe(baseline.nonFeeContextDroveDivergence);
    }

    expect(baseline.conciseSummary).toContain("Equal-fee comparison aligned");
    expect(baseline.primaryNonFeeDrivers.length).toBeGreaterThan(0);
  });

  it("keeps negotiation log explainability reasons deterministic under fixed contexts", () => {
    const firstRun = buildTransferNegotiationLogSamples(calibrationTarget, calibrationLogContexts);
    const secondRun = buildTransferNegotiationLogSamples(calibrationTarget, calibrationLogContexts);

    expect(secondRun).toEqual(firstRun);

    for (const entry of firstRun) {
      expect(entry.reasonSummary.length).toBeGreaterThan(0);

      if (entry.blockingActor === "player") {
        expect(entry.reasonSummary.join(" ")).toContain("Player side rejected");
      }

      if (entry.blockingActor === "sporting-director") {
        expect(entry.reasonSummary.join(" ")).toContain("Wage demand pressured");
      }
    }
  });

  it("keeps negotiation sample outputs deterministic for calibration snapshots", () => {
    const buildNegotiationSampleOutput = () =>
      buildTransferNegotiationLogSamples(calibrationTarget, calibrationLogContexts).map((entry) => ({
        attemptId: entry.attemptId,
        accepted: entry.accepted,
        blockingActor: entry.blockingActor,
        reasonCount: entry.reasonSummary.length,
        scoreBand: entry.score >= 0 ? "non-negative" : "negative"
      }));

    const runs = Array.from({ length: 8 }, () => buildNegotiationSampleOutput());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline).toHaveLength(3);
    expect(baseline.map((entry) => entry.attemptId)).toEqual(["attempt-1", "attempt-2", "attempt-3"]);
    expect(baseline.map((entry) => entry.blockingActor)).toEqual(["none", "none", "player"]);
    expect(baseline.map((entry) => entry.reasonCount)).toEqual([6, 6, 8]);
    expect(baseline.map((entry) => entry.scoreBand)).toEqual(["non-negative", "non-negative", "negative"]);
  });

  it("keeps concise transfer summaries deterministic and regression-friendly", () => {
    const firstPromiseTrust = summarizePromiseTrustImpactByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationPromiseVariants
    );
    const secondPromiseTrust = summarizePromiseTrustImpactByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationPromiseVariants
    );

    const firstOutcomeSummary = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationOutcomeVariants
    );
    const secondOutcomeSummary = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationOutcomeVariants
    );

    expect(secondPromiseTrust.conciseSummary).toEqual(firstPromiseTrust.conciseSummary);
    expect(secondOutcomeSummary.conciseSummary).toEqual(firstOutcomeSummary.conciseSummary);

    for (const line of firstPromiseTrust.conciseSummary) {
      expect(line).toMatch(
        /^Manager reputation \d+: intact trust \d+\/\d+ \(\d\.\d{2}\) vs broken trust \d+\/\d+ \(\d\.\d{2}\), delta -?\d\.\d{2}\.$/
      );
    }

    for (const line of firstOutcomeSummary.conciseSummary) {
      expect(line).toMatch(
        /^Manager reputation \d+: accepted \d+\/\d+ \(\d\.\d{2}\), average score -?\d+\.\d{2}, board blocks \d+, sporting-director blocks \d+, player blocks \d+\.$/
      );
    }
  });

  it("keeps concise transfer-summary ordering consistent by reputation band", () => {
    const buildSummaryLines = () => {
      const promiseTrust = summarizePromiseTrustImpactByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationPromiseVariants
      ).conciseSummary;

      const outcomeSummary = summarizeTransferOutcomesByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationOutcomeVariants
      ).conciseSummary;

      return {
        promiseTrust,
        outcomeSummary
      };
    };

    const runs = Array.from({ length: 6 }, () => buildSummaryLines());
    const baseline = runs[0];
    const expectedReputationOrder = [...calibrationReputationBands];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    const parseReputations = (lines: string[]) =>
      lines.map((line) => {
        const match = line.match(/^Manager reputation (\d+):/);
        expect(match).not.toBeNull();
        return Number(match![1]);
      });

    expect(baseline.promiseTrust).toHaveLength(expectedReputationOrder.length);
    expect(baseline.outcomeSummary).toHaveLength(expectedReputationOrder.length);
    expect(parseReputations(baseline.promiseTrust)).toEqual(expectedReputationOrder);
    expect(parseReputations(baseline.outcomeSummary)).toEqual(expectedReputationOrder);
  });

  it("keeps concise outcome-summary metric labels consistent across reputation bands", () => {
    const buildOutcomeSummary = () =>
      summarizeTransferOutcomesByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationOutcomeVariants
      ).conciseSummary;

    const runs = Array.from({ length: 6 }, () => buildOutcomeSummary());
    const baseline = runs[0];
    const expectedClauses = [
      "accepted",
      "average score",
      "board blocks",
      "sporting-director blocks",
      "player blocks"
    ];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline).toHaveLength(calibrationReputationBands.length);

    for (const line of baseline) {
      let cursor = -1;
      for (const clause of expectedClauses) {
        const nextCursor = line.indexOf(clause);
        expect(nextCursor).toBeGreaterThan(cursor);
        cursor = nextCursor;
      }
    }
  });

  it("keeps concise promise-trust summary metric labels consistent across reputation bands", () => {
    const buildPromiseTrustSummary = () =>
      summarizePromiseTrustImpactByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationPromiseVariants
      ).conciseSummary;

    const runs = Array.from({ length: 6 }, () => buildPromiseTrustSummary());
    const baseline = runs[0];
    const expectedClauses = ["intact trust", "broken trust", "delta"];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline).toHaveLength(calibrationReputationBands.length);

    for (const line of baseline) {
      let cursor = -1;
      for (const clause of expectedClauses) {
        const nextCursor = line.indexOf(clause);
        expect(nextCursor).toBeGreaterThan(cursor);
        cursor = nextCursor;
      }
    }
  });

  it("keeps concise transfer calibration artifacts serialization-stable across runs", () => {
    const buildSerializedArtifact = () => {
      const promiseTrustSummary = summarizePromiseTrustImpactByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationPromiseVariants
      ).conciseSummary;

      const outcomeSummary = summarizeTransferOutcomesByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationOutcomeVariants
      ).conciseSummary;

      const equalFeeSummary = buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).conciseSummary;

      const deltaSummary = buildReputationBandOutcomeDeltaSummary(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        82,
        28,
        calibrationOutcomeVariants
      ).conciseSummary;

      return JSON.stringify(
        {
          promiseTrustSummary,
          outcomeSummary,
          equalFeeSummary,
          deltaSummary
        },
        null,
        2
      );
    };

    const runs = Array.from({ length: 6 }, () => buildSerializedArtifact());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toBe(baseline);
    }

    expect(baseline).toContain("\"promiseTrustSummary\"");
    expect(baseline).toContain("\"outcomeSummary\"");
    expect(baseline).toContain("\"equalFeeSummary\"");
    expect(baseline).toContain("\"deltaSummary\"");
  });

  it("keeps reputation-band acceptance and block profiles stable across repeated runs", () => {
    const buildBandProfile = () =>
      summarizeTransferOutcomesByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationOutcomeVariants
      ).bands.map((band) => ({
        managerReputation: band.managerReputation,
        acceptedCount: band.acceptedCount,
        sportingDirectorBlockCount: band.sportingDirectorBlockCount,
        playerBlockCount: band.playerBlockCount
      }));

    const runs = Array.from({ length: 8 }, () => buildBandProfile());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline).toEqual([
      {
        managerReputation: 82,
        acceptedCount: 2,
        sportingDirectorBlockCount: 2,
        playerBlockCount: 0
      },
      {
        managerReputation: 62,
        acceptedCount: 2,
        sportingDirectorBlockCount: 2,
        playerBlockCount: 0
      },
      {
        managerReputation: 28,
        acceptedCount: 0,
        sportingDirectorBlockCount: 0,
        playerBlockCount: 4
      }
    ]);
  });

  it("keeps per-attempt negotiation reason-summary profiles stable across repeated runs", () => {
    const buildReasonProfiles = () =>
      buildTransferNegotiationLogSamples(calibrationTarget, calibrationLogContexts).map((entry) => ({
        attemptId: entry.attemptId,
        blockingActor: entry.blockingActor,
        reasonCount: entry.reasonSummary.length,
        reasonFingerprint: entry.reasonSummary.join(" | ")
      }));

    const runs = Array.from({ length: 8 }, () => buildReasonProfiles());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.map((entry) => entry.attemptId)).toEqual(["attempt-1", "attempt-2", "attempt-3"]);
    expect(baseline.map((entry) => entry.blockingActor)).toEqual(["none", "none", "player"]);
    expect(new Set(baseline.map((entry) => entry.reasonFingerprint)).size).toBe(baseline.length);

    for (const entry of baseline) {
      expect(entry.reasonCount).toBeGreaterThan(0);
      expect(entry.reasonFingerprint.length).toBeGreaterThan(20);
    }
  });

  it("keeps equal-fee primary driver ordering stable by impact across repeated runs", () => {
    const buildPrimaryDrivers = () =>
      buildEqualFeeNegotiationExplainabilityArtifact(
        calibrationTarget,
        calibrationEqualFeeContexts.first,
        calibrationEqualFeeContexts.second
      ).primaryNonFeeDrivers;

    const runs = Array.from({ length: 8 }, () => buildPrimaryDrivers());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.length).toBeGreaterThan(0);

    const parseImpact = (driver: string) => {
      const match = driver.match(/([+-]\d+\.\d{2})$/);
      expect(match).not.toBeNull();
      return Math.abs(Number(match![1]));
    };

    const impactMagnitudes = baseline.map((driver) => parseImpact(driver));
    for (let index = 1; index < impactMagnitudes.length; index += 1) {
      expect(impactMagnitudes[index]).toBeLessThanOrEqual(impactMagnitudes[index - 1]);
    }
  });

  it("keeps promise-trust acceptance deltas monotonic by reputation in fixed calibration runs", () => {
    const buildPromiseDeltaProfile = () =>
      summarizePromiseTrustImpactByReputationBand(
        calibrationTarget,
        {
          clubWageBudget: calibrationBaseContext.clubWageBudget,
          clubStature: calibrationBaseContext.clubStature,
          boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
        },
        calibrationReputationBands,
        calibrationPromiseVariants
      ).bands.map((band) => ({
        managerReputation: band.managerReputation,
        acceptanceRateDelta: band.acceptanceRateDelta
      }));

    const runs = Array.from({ length: 8 }, () => buildPromiseDeltaProfile());
    const baseline = runs[0];

    for (const run of runs.slice(1)) {
      expect(run).toEqual(baseline);
    }

    expect(baseline.map((band) => band.managerReputation)).toEqual([82, 62, 28]);

    for (let index = 1; index < baseline.length; index += 1) {
      expect(baseline[index].acceptanceRateDelta).toBeLessThanOrEqual(
        baseline[index - 1].acceptanceRateDelta
      );
    }
  });

  it("keeps reputation-band transfer outcome artifacts invariant under fixed fixture reordering", () => {
    const forwardSummary = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      calibrationOutcomeVariants
    );
    const reversedSummary = summarizeTransferOutcomesByReputationBand(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      calibrationReputationBands,
      [...calibrationOutcomeVariants].reverse()
    );

    const forwardDelta = buildReputationBandOutcomeDeltaSummary(
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
    const reversedDelta = buildReputationBandOutcomeDeltaSummary(
      calibrationTarget,
      {
        clubWageBudget: calibrationBaseContext.clubWageBudget,
        clubStature: calibrationBaseContext.clubStature,
        boardWageDiscipline: calibrationBaseContext.boardWageDiscipline
      },
      82,
      28,
      [...calibrationOutcomeVariants].reverse()
    );

    expect(reversedSummary.bands).toHaveLength(forwardSummary.bands.length);
    for (const [index, forwardBand] of forwardSummary.bands.entries()) {
      const reversedBand = reversedSummary.bands[index];
      expect(reversedBand.managerReputation).toBe(forwardBand.managerReputation);
      expect(reversedBand.attempts).toBe(forwardBand.attempts);
      expect(reversedBand.acceptedCount).toBe(forwardBand.acceptedCount);
      expect(reversedBand.acceptanceRate).toBeCloseTo(forwardBand.acceptanceRate, 12);
      expect(reversedBand.averageScore).toBeCloseTo(forwardBand.averageScore, 12);
      expect(reversedBand.boardBlockCount).toBe(forwardBand.boardBlockCount);
      expect(reversedBand.sportingDirectorBlockCount).toBe(forwardBand.sportingDirectorBlockCount);
      expect(reversedBand.playerBlockCount).toBe(forwardBand.playerBlockCount);
    }

    expect(reversedSummary.conciseSummary).toEqual(forwardSummary.conciseSummary);
    expect(reversedDelta.baselineReputation).toBe(forwardDelta.baselineReputation);
    expect(reversedDelta.comparisonReputation).toBe(forwardDelta.comparisonReputation);
    expect(reversedDelta.acceptedCountDelta).toBe(forwardDelta.acceptedCountDelta);
    expect(reversedDelta.acceptanceRateDelta).toBeCloseTo(forwardDelta.acceptanceRateDelta, 12);
    expect(reversedDelta.averageScoreDelta).toBeCloseTo(forwardDelta.averageScoreDelta, 12);
    expect(reversedDelta.boardBlockDelta).toBe(forwardDelta.boardBlockDelta);
    expect(reversedDelta.sportingDirectorBlockDelta).toBe(forwardDelta.sportingDirectorBlockDelta);
    expect(reversedDelta.playerBlockDelta).toBe(forwardDelta.playerBlockDelta);
    expect(reversedDelta.conciseSummary).toBe(forwardDelta.conciseSummary);
  });
});