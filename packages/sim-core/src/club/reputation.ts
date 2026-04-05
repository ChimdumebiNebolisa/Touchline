import type { PostMatchPerceptionContext } from "./types.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

export interface ReputationDeltaInput {
  goalsFor: number;
  goalsAgainst: number;
  boardDelta: number;
  fanDelta: number;
  context: PostMatchPerceptionContext;
}

export function computeManagerReputationDelta(input: ReputationDeltaInput): number {
  const resultSignal = (input.goalsFor > input.goalsAgainst ? 2 : input.goalsFor < input.goalsAgainst ? -2 : 0.5);
  const boardSignal = input.boardDelta * 0.35;
  const fanSignal = input.fanDelta * 0.25;
  const expectationSignal = input.context.expectationBand === "underdog" ? 0.7 : 0;
  const mediaPenalty = input.context.mediaCommentTone === "provocative" ? -1 : 0;

  return clamp(resultSignal + boardSignal + fanSignal + expectationSignal + mediaPenalty, -8, 8);
}
