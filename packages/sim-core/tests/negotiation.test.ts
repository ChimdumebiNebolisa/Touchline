import { describe, expect, it } from "vitest";

import { compareTransferNegotiationContexts } from "../src/index.js";
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
});
