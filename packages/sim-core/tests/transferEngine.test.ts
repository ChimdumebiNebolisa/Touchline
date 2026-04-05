import { describe, expect, it } from "vitest";

import { buildTransferFollowUpEvent, evaluateTransferDecision } from "../src/index.js";
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
});
