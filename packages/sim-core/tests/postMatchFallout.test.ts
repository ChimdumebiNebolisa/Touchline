import { describe, expect, it } from "vitest";

import {
  applyPostMatchFallout,
  computeBoardConfidenceDelta,
  runInstantMatch
} from "../src/index.js";
import type { ClubPerceptionState, PostMatchPerceptionContext, TacticalSetup } from "../src/index.js";
import { createMatchInput } from "./fixtures/matchFixtures.js";

const balancedTactics: TacticalSetup = {
  blockHeight: 0.56,
  pressingIntensity: 0.58,
  width: 0.55,
  tempo: 0.52,
  risk: 0.5
};

const baseState: ClubPerceptionState = {
  boardConfidence: 55,
  fanSentiment: 54,
  teamMorale: 56,
  managerReputation: 53
};

describe("post-match fallout", () => {
  it("improves multiple downstream values in a positive context", () => {
    const outcome = runInstantMatch(createMatchInput(9911, balancedTactics, balancedTactics));
    const context: PostMatchPerceptionContext = {
      expectationBand: "competitive",
      identityStyleFit: 0.74,
      financialPressure: 0.3,
      mediaHeat: 0.25,
      isDerby: true,
      brokenPromise: false,
      mediaCommentTone: "calm"
    };

    const result = applyPostMatchFallout({
      state: baseState,
      matchOutcome: {
        ...outcome,
        result: {
          homeGoals: 2,
          awayGoals: 1
        },
        stats: {
          ...outcome.stats,
          home: { ...outcome.stats.home, xg: 1.9 },
          away: { ...outcome.stats.away, xg: 0.8 }
        }
      },
      context
    });

    const improvedCount = [
      result.nextState.boardConfidence > baseState.boardConfidence,
      result.nextState.fanSentiment > baseState.fanSentiment,
      result.nextState.teamMorale > baseState.teamMorale,
      result.nextState.managerReputation > baseState.managerReputation
    ].filter(Boolean).length;

    expect(improvedCount).toBeGreaterThanOrEqual(2);
    expect(result.reasonSummary.length).toBeGreaterThan(1);
  });

  it("reduces multiple downstream values with poor result and broken promise", () => {
    const outcome = runInstantMatch(createMatchInput(447, balancedTactics, balancedTactics));
    const context: PostMatchPerceptionContext = {
      expectationBand: "must-win",
      identityStyleFit: 0.3,
      financialPressure: 0.85,
      mediaHeat: 0.92,
      isDerby: false,
      brokenPromise: true,
      mediaCommentTone: "provocative"
    };

    const result = applyPostMatchFallout({
      state: baseState,
      matchOutcome: {
        ...outcome,
        result: {
          homeGoals: 0,
          awayGoals: 2
        },
        stats: {
          ...outcome.stats,
          home: { ...outcome.stats.home, xg: 0.6 },
          away: { ...outcome.stats.away, xg: 1.7 }
        }
      },
      context
    });

    const reducedCount = [
      result.nextState.boardConfidence < baseState.boardConfidence,
      result.nextState.fanSentiment < baseState.fanSentiment,
      result.nextState.teamMorale < baseState.teamMorale,
      result.nextState.managerReputation < baseState.managerReputation
    ].filter(Boolean).length;

    expect(reducedCount).toBeGreaterThanOrEqual(2);
  });

  it("evaluates board confidence contextually beyond scoreline", () => {
    const mustWinDelta = computeBoardConfidenceDelta({
      goalsFor: 1,
      goalsAgainst: 1,
      homeXg: 1.4,
      awayXg: 1.1,
      context: {
        expectationBand: "must-win",
        identityStyleFit: 0.6,
        financialPressure: 0.5,
        mediaHeat: 0.4,
        isDerby: false,
        brokenPromise: false,
        mediaCommentTone: "neutral"
      }
    });

    const underdogDelta = computeBoardConfidenceDelta({
      goalsFor: 1,
      goalsAgainst: 1,
      homeXg: 1.4,
      awayXg: 1.1,
      context: {
        expectationBand: "underdog",
        identityStyleFit: 0.6,
        financialPressure: 0.5,
        mediaHeat: 0.4,
        isDerby: false,
        brokenPromise: false,
        mediaCommentTone: "neutral"
      }
    });

    expect(underdogDelta).toBeGreaterThan(mustWinDelta);
  });
});
