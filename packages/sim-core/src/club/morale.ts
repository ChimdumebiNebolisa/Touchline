import type { MediaCommentTone, PostMatchPerceptionContext } from "./types.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function toneImpact(tone: MediaCommentTone): number {
  switch (tone) {
    case "calm":
      return 0.8;
    case "neutral":
      return 0;
    case "provocative":
      return -1.2;
    default:
      return 0;
  }
}

export interface MoraleDeltaInput {
  goalsFor: number;
  goalsAgainst: number;
  boardDelta: number;
  fanDelta: number;
  context: PostMatchPerceptionContext;
}

export function computeTeamMoraleDelta(input: MoraleDeltaInput): number {
  const goalDiff = input.goalsFor - input.goalsAgainst;
  const resultFactor = clamp(goalDiff * 1.6, -4.5, 4.5);
  const pressureFactor = -input.context.mediaHeat * 2.4;
  const boardFanFactor = input.boardDelta * 0.35 + input.fanDelta * 0.45;
  const promiseFactor = input.context.brokenPromise ? -2.2 : 0;
  const commentFactor = toneImpact(input.context.mediaCommentTone);

  return clamp(resultFactor + pressureFactor + boardFanFactor + promiseFactor + commentFactor, -10, 10);
}
