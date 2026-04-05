import type { PostMatchPerceptionContext } from "./types.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function matchPoints(goalsFor: number, goalsAgainst: number): number {
  if (goalsFor > goalsAgainst) {
    return 3;
  }
  if (goalsFor === goalsAgainst) {
    return 1;
  }
  return 0;
}

function expectedPoints(expectationBand: PostMatchPerceptionContext["expectationBand"]): number {
  switch (expectationBand) {
    case "must-win":
      return 2.5;
    case "competitive":
      return 1.5;
    case "underdog":
      return 0.9;
    default:
      return 1.5;
  }
}

export interface BoardDeltaInput {
  goalsFor: number;
  goalsAgainst: number;
  homeXg: number;
  awayXg: number;
  context: PostMatchPerceptionContext;
}

export interface BoardExpectationContext {
  leaguePosition: number;
  totalTeams: number;
  preseasonObjectivePosition: number;
  clubStature: number;
  financialPressure: number;
  recentPointsPerMatch: number;
  styleAlignment: number;
  derbyResult: "none" | "win" | "draw" | "loss";
}

export interface BoardExpectationEvaluation {
  boardDelta: number;
  sackRisk: number;
  reasonSummary: string[];
}

export function computeBoardConfidenceDelta(input: BoardDeltaInput): number {
  const points = matchPoints(input.goalsFor, input.goalsAgainst);
  const expectationGap = points - expectedPoints(input.context.expectationBand);

  const xgDelta = input.homeXg - input.awayXg;
  const styleFactor = (input.context.identityStyleFit - 0.5) * 2.4;
  const pressureFactor = -input.context.financialPressure * 1.8;
  const derbyFactor = input.context.isDerby ? 0.8 * Math.sign(input.goalsFor - input.goalsAgainst) : 0;
  const promiseFactor = input.context.brokenPromise ? -1.4 : 0;

  const delta =
    expectationGap * 2.2 + xgDelta * 1.2 + styleFactor + pressureFactor + derbyFactor + promiseFactor;

  return clamp(delta, -8, 8);
}

export function evaluateBoardExpectationContext(
  context: BoardExpectationContext
): BoardExpectationEvaluation {
  const expectedGap = context.preseasonObjectivePosition - context.leaguePosition;
  const positionScore = expectedGap * 1.05;
  const staturePenalty = Math.max(0, context.leaguePosition - context.preseasonObjectivePosition) * context.clubStature * 0.8;
  const financePenalty = context.financialPressure * 2.2;
  const formScore = (context.recentPointsPerMatch - 1.25) * 2.6;
  const styleScore = (context.styleAlignment - 0.5) * 2;
  const derbyScore = context.derbyResult === "win" ? 0.9 : context.derbyResult === "loss" ? -1.1 : 0;

  const boardDelta = clamp(
    positionScore + formScore + styleScore + derbyScore - financePenalty - staturePenalty,
    -10,
    10
  );

  const positionRisk = clamp(
    (context.leaguePosition - context.preseasonObjectivePosition) / Math.max(1, context.totalTeams),
    -1,
    1
  );
  const sackRisk = clamp(
    0.45 + positionRisk * 0.6 + context.financialPressure * 0.22 + context.clubStature * 0.15 - context.recentPointsPerMatch * 0.09,
    0,
    1
  );

  const reasons = [
    `Position context contributed ${positionScore.toFixed(2)} and stature pressure contributed -${staturePenalty.toFixed(2)}.`,
    `Recent form contribution: ${formScore.toFixed(2)} PPM-context points.`,
    `Finance/style/derby combined contribution: ${(styleScore + derbyScore - financePenalty).toFixed(2)}.`
  ];

  return {
    boardDelta,
    sackRisk,
    reasonSummary: reasons
  };
}
