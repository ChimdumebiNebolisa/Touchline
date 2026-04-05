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
