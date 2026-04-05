import { describe, expect, it } from "vitest";

import {
  buildTransferDemandBreakdown,
  buildTransferFollowUpEvent,
  evaluateTransferDecision
} from "../src/index.js";
import type { TransferEvaluationContext, TransferTargetProfile } from "../src/index.js";

const target: TransferTargetProfile = {
  id: "target-1",
  name: "Luca Marin",
  roleFit: 0.78,
  projectFit: 0.75,
  wageDemand: 190,
  pathwayPreference: 0.72,
  competitionTolerance: 0.52,
  reputationSensitivity: 0.8
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

describe("transfer engine", () => {
  it("resolves equal-fee offers differently when project and pathway context diverge", () => {
    const equalFeeTarget: TransferTargetProfile = {
      ...target,
      wageDemand: 190,
      roleFit: 0.65,
      projectFit: 0.61,
      pathwayPreference: 0.7
    };

    const highPathDecision = evaluateTransferDecision(equalFeeTarget, {
      ...baseContext,
      pathwayClarity: 0.88,
      squadCompetition: 0.48,
      managerReputation: 68
    });

    const blockedPathDecision = evaluateTransferDecision(equalFeeTarget, {
      ...baseContext,
      pathwayClarity: 0.28,
      squadCompetition: 0.85,
      managerReputation: 68
    });

    expect(highPathDecision.accepted).toBe(true);
    expect(blockedPathDecision.accepted).toBe(false);
    expect(highPathDecision.score).toBeGreaterThan(blockedPathDecision.score);
  });

  it("changes outcome when promise context changes under similar financial terms", () => {
    const trustedDecision = evaluateTransferDecision(target, {
      ...baseContext,
      recentPromiseBreak: false,
      managerReputation: 70
    });

    const brokenPromiseDecision = evaluateTransferDecision(target, {
      ...baseContext,
      recentPromiseBreak: true,
      managerReputation: 70
    });

    expect(trustedDecision.accepted).not.toBe(brokenPromiseDecision.accepted);
  });

  it("reduces acceptance when manager reputation is low", () => {
    const highRep = evaluateTransferDecision(target, {
      ...baseContext,
      managerReputation: 82
    });
    const lowRep = evaluateTransferDecision(target, {
      ...baseContext,
      managerReputation: 28
    });

    expect(highRep.score).toBeGreaterThan(lowRep.score);
  });

  it("surfaces transfer follow-up reasons tied to reputation or promises", () => {
    const event = buildTransferFollowUpEvent(target, {
      ...baseContext,
      managerReputation: 35,
      recentPromiseBreak: true
    });

    expect(event.reasonSummary.join(" ").toLowerCase()).toMatch(/reputation|promise|trust/);
  });

  it("produces higher demand breakdown score for stronger pathway and competition fit", () => {
    const fitBreakdown = buildTransferDemandBreakdown(target, {
      ...baseContext,
      pathwayClarity: 0.86,
      squadCompetition: target.competitionTolerance,
      managerReputation: 66
    });
    const poorBreakdown = buildTransferDemandBreakdown(target, {
      ...baseContext,
      pathwayClarity: 0.22,
      squadCompetition: 0.9,
      managerReputation: 66
    });

    expect(fitBreakdown.totalScore).toBeGreaterThan(poorBreakdown.totalScore);
    expect(fitBreakdown.pathwayScore).toBeGreaterThan(poorBreakdown.pathwayScore);
  });
});
