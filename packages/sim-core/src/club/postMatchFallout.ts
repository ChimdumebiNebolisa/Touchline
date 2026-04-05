import { computeBoardConfidenceDelta } from "./board.js";
import { computeTeamMoraleDelta } from "./morale.js";
import { deriveManagerCareerLeverageSnapshot, computeManagerReputationDelta } from "./reputation.js";
import type { PostMatchFalloutInput, PostMatchFalloutResult } from "./types.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function computeFanSentimentDelta(
  goalsFor: number,
  goalsAgainst: number,
  homeXg: number,
  awayXg: number,
  context: PostMatchFalloutInput["context"]
): number {
  const goalDiff = goalsFor - goalsAgainst;
  const resultSignal = clamp(goalDiff * 2, -6, 6);
  const xgSignal = clamp((homeXg - awayXg) * 1.6, -2.5, 2.5);
  const derbySignal = context.isDerby ? Math.sign(goalDiff) * 1.4 : 0;
  const styleSignal = (context.identityStyleFit - 0.5) * 2;
  const promisePenalty = context.brokenPromise ? -1.5 : 0;
  const mediaPenalty = context.mediaCommentTone === "provocative" ? -1.1 : 0;

  return clamp(resultSignal + xgSignal + derbySignal + styleSignal + promisePenalty + mediaPenalty, -10, 10);
}

function buildReasonSummary(
  boardDelta: number,
  fanDelta: number,
  moraleDelta: number,
  reputationDelta: number,
  careerLeverageReason: string,
  goalsFor: number,
  goalsAgainst: number
): string[] {
  const reasons: string[] = [];
  const resultLabel = goalsFor > goalsAgainst ? "win" : goalsFor < goalsAgainst ? "loss" : "draw";
  reasons.push(`Result context: ${resultLabel} shifted pressure and patience.`);

  if (boardDelta !== 0) {
    reasons.push(`Board confidence moved by ${boardDelta.toFixed(2)} based on expectation and performance context.`);
  }
  if (fanDelta !== 0) {
    reasons.push(`Fan sentiment moved by ${fanDelta.toFixed(2)} from result quality and identity fit.`);
  }
  if (moraleDelta !== 0) {
    reasons.push(`Team morale moved by ${moraleDelta.toFixed(2)} from board/fan pressure and media tone.`);
  }
  if (reputationDelta !== 0) {
    reasons.push(`Manager reputation moved by ${reputationDelta.toFixed(2)} from perception fallout.`);
  }

  reasons.push(careerLeverageReason);

  return reasons;
}

export function applyPostMatchFallout(input: PostMatchFalloutInput): PostMatchFalloutResult {
  const goalsFor = input.matchOutcome.result.homeGoals;
  const goalsAgainst = input.matchOutcome.result.awayGoals;
  const homeXg = input.matchOutcome.stats.home.xg;
  const awayXg = input.matchOutcome.stats.away.xg;

  const boardDelta = computeBoardConfidenceDelta({
    goalsFor,
    goalsAgainst,
    homeXg,
    awayXg,
    context: input.context
  });

  const fanDelta = computeFanSentimentDelta(goalsFor, goalsAgainst, homeXg, awayXg, input.context);

  const moraleDelta = computeTeamMoraleDelta({
    goalsFor,
    goalsAgainst,
    boardDelta,
    fanDelta,
    context: input.context
  });

  const reputationDelta = computeManagerReputationDelta({
    goalsFor,
    goalsAgainst,
    boardDelta,
    fanDelta,
    context: input.context
  });

  const nextState = {
    boardConfidence: clamp(input.state.boardConfidence + boardDelta, 0, 100),
    fanSentiment: clamp(input.state.fanSentiment + fanDelta, 0, 100),
    teamMorale: clamp(input.state.teamMorale + moraleDelta, 0, 100),
    managerReputation: clamp(input.state.managerReputation + reputationDelta, 0, 100)
  };

  const careerLeverage = deriveManagerCareerLeverageSnapshot({
    managerReputation: nextState.managerReputation,
    boardConfidence: nextState.boardConfidence,
    fanSentiment: nextState.fanSentiment
  });

  return {
    nextState,
    deltas: {
      boardConfidence: boardDelta,
      fanSentiment: fanDelta,
      teamMorale: moraleDelta,
      managerReputation: reputationDelta
    },
    careerLeverage,
    reasonSummary: buildReasonSummary(
      boardDelta,
      fanDelta,
      moraleDelta,
      reputationDelta,
      careerLeverage.reasonSummary,
      goalsFor,
      goalsAgainst
    )
  };
}
