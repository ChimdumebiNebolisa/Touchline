import { describe, expect, it } from "vitest";

import {
  buildTransferNegotiationLogSamples,
  compareTransferNegotiationContexts,
  summarizeNegotiationAcceptanceByReputationBand
} from "../src/index.js";
import type { TransferEvaluationContext, TransferTargetProfile } from "../src/index.js";

const target: TransferTargetProfile = {
  id: "target-negotiation-1",
  name: "Noel Varga",
  roleFit: 0.7,
  projectFit: 0.66,
  wageDemand: 190,
  pathwayPreference: 0.72,
  competitionTolerance: 0.5,
  reputationSensitivity: 0.82
};

const baseContext: TransferEvaluationContext = {
  clubWageBudget: 240,
  clubStature: 0.58,
  managerReputation: 62,
  squadCompetition: 0.52,
  pathwayClarity: 0.7,
  recentPromiseBreak: false,
  boardWageDiscipline: 0.55
};

describe("transfer negotiation comparison", () => {
  it("shows equal-fee context divergence through non-fee factors", () => {
    const equalFeeTarget: TransferTargetProfile = {
      ...target,
      roleFit: 0.65,
      projectFit: 0.61,
      pathwayPreference: 0.7
    };

    const comparison = compareTransferNegotiationContexts(
      equalFeeTarget,
      {
        ...baseContext,
        managerReputation: 68,
        pathwayClarity: 0.88,
        squadCompetition: 0.48
      },
      {
        ...baseContext,
        managerReputation: 68,
        pathwayClarity: 0.28,
        squadCompetition: 0.85
      }
    );

    expect(comparison.diverged).toBe(true);
    expect(comparison.firstDecision.accepted).toBe(true);
    expect(comparison.secondDecision.accepted).toBe(false);
    expect(comparison.changedContextFactors).toContain("pathwayClarity");
    expect(comparison.changedContextFactors).toContain("squadCompetition");
  });

  it("shows low reputation context scoring worse under comparable financial terms", () => {
    const comparison = compareTransferNegotiationContexts(
      target,
      {
        ...baseContext,
        managerReputation: 82
      },
      {
        ...baseContext,
        managerReputation: 28
      }
    );

    expect(comparison.firstDecision.score).toBeGreaterThan(comparison.secondDecision.score);
    expect(comparison.changedContextFactors).toContain("managerReputation");
  });

  it("shows lower acceptance rate for low-reputation manager under comparable scenarios", () => {
    const variants = [
      { pathwayClarity: 0.82, squadCompetition: 0.5, recentPromiseBreak: false },
      { pathwayClarity: 0.64, squadCompetition: 0.56, recentPromiseBreak: false },
      { pathwayClarity: 0.48, squadCompetition: 0.63, recentPromiseBreak: false },
      { pathwayClarity: 0.52, squadCompetition: 0.58, recentPromiseBreak: true }
    ];

    const firstRun = summarizeNegotiationAcceptanceByReputationBand(
      target,
      {
        clubWageBudget: baseContext.clubWageBudget,
        clubStature: baseContext.clubStature,
        boardWageDiscipline: baseContext.boardWageDiscipline
      },
      [82, 28],
      variants
    );
    const secondRun = summarizeNegotiationAcceptanceByReputationBand(
      target,
      {
        clubWageBudget: baseContext.clubWageBudget,
        clubStature: baseContext.clubStature,
        boardWageDiscipline: baseContext.boardWageDiscipline
      },
      [82, 28],
      variants
    );

    expect(firstRun).toEqual(secondRun);
    expect(firstRun[0].acceptanceRate).toBeGreaterThan(firstRun[1].acceptanceRate);
    expect(firstRun[0].attempts).toBe(variants.length);
    expect(firstRun[1].attempts).toBe(variants.length);
  });

  it("rejects reputation-band summary with empty scenario variants", () => {
    expect(() =>
      summarizeNegotiationAcceptanceByReputationBand(
        target,
        {
          clubWageBudget: baseContext.clubWageBudget,
          clubStature: baseContext.clubStature,
          boardWageDiscipline: baseContext.boardWageDiscipline
        },
        [82, 28],
        []
      )
    ).toThrow("At least one negotiation scenario variant is required.");
  });

  it("builds deterministic and explainable negotiation log samples", () => {
    const contexts: TransferEvaluationContext[] = [
      {
        ...baseContext,
        managerReputation: 78,
        pathwayClarity: 0.82,
        squadCompetition: 0.5,
        recentPromiseBreak: false
      },
      {
        ...baseContext,
        managerReputation: 32,
        pathwayClarity: 0.35,
        squadCompetition: 0.8,
        recentPromiseBreak: true
      }
    ];

    const firstRun = buildTransferNegotiationLogSamples(target, contexts);
    const secondRun = buildTransferNegotiationLogSamples(target, contexts);

    expect(firstRun).toEqual(secondRun);
    expect(firstRun).toHaveLength(2);
    expect(firstRun[0].attemptId).toBe("attempt-1");
    expect(firstRun[1].attemptId).toBe("attempt-2");
    expect(firstRun.every((entry) => entry.reasonSummary.length > 0)).toBe(true);
  });
});
