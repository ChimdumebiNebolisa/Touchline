import { describe, expect, it } from "vitest";

import {
  applyPostMatchFallout,
  buildTransferFollowUpEvent,
  runInstantMatch,
  runLiveMatch
} from "../src/index.js";
import type { ClubPerceptionState, TacticalSetup, TransferTargetProfile } from "../src/index.js";
import { createMatchInput } from "./fixtures/matchFixtures.js";

const tactics: TacticalSetup = {
  blockHeight: 0.56,
  pressingIntensity: 0.61,
  width: 0.54,
  tempo: 0.57,
  risk: 0.5
};

const baselinePerception: ClubPerceptionState = {
  boardConfidence: 54,
  fanSentiment: 53,
  teamMorale: 55,
  managerReputation: 52
};

const transferTarget: TransferTargetProfile = {
  id: "target-vslice",
  name: "Niko Sarin",
  roleFit: 0.77,
  projectFit: 0.74,
  wageDemand: 180,
  pathwayPreference: 0.7,
  competitionTolerance: 0.52,
  reputationSensitivity: 0.82
};

describe("Step 1 vertical slice integration", () => {
  it("runs match, persists fallout, and produces transfer follow-up", () => {
    const input = createMatchInput(5042, tactics, tactics);

    const instant = runInstantMatch(input);
    const live = runLiveMatch(input);

    expect(live.result).toEqual(instant.result);
    expect(live.stats).toEqual(instant.stats);

    const fallout = applyPostMatchFallout({
      state: baselinePerception,
      matchOutcome: instant,
      context: {
        expectationBand: "competitive",
        identityStyleFit: 0.7,
        financialPressure: 0.5,
        mediaHeat: 0.4,
        isDerby: false,
        brokenPromise: true,
        mediaCommentTone: "neutral"
      }
    });

    const changedCount = [
      fallout.nextState.boardConfidence !== baselinePerception.boardConfidence,
      fallout.nextState.fanSentiment !== baselinePerception.fanSentiment,
      fallout.nextState.teamMorale !== baselinePerception.teamMorale,
      fallout.nextState.managerReputation !== baselinePerception.managerReputation
    ].filter(Boolean).length;
    expect(changedCount).toBeGreaterThanOrEqual(2);

    const transferEvent = buildTransferFollowUpEvent(transferTarget, {
      clubWageBudget: 225,
      clubStature: 0.56,
      managerReputation: fallout.nextState.managerReputation,
      squadCompetition: 0.52,
      pathwayClarity: 0.66,
      recentPromiseBreak: true,
      boardWageDiscipline: 0.6
    });

    expect(transferEvent.targetId).toBe("target-vslice");
    expect(transferEvent.reasonSummary.length).toBeGreaterThan(1);
    expect(transferEvent.reasonSummary.join(" ").toLowerCase()).toMatch(/promise|reputation|trust/);
  });
});
