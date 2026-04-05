import type {
  ManagerCareerLeverageBand,
  ManagerCareerLeverageSnapshot,
  PostMatchPerceptionContext
} from "./types.js";

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

export interface ManagerCareerLeverageInput {
  managerReputation: number;
  boardConfidence: number;
  fanSentiment: number;
}

function deriveCareerLeverageBand(score: number): ManagerCareerLeverageBand {
  if (score >= 80) {
    return "elite";
  }
  if (score >= 62) {
    return "in-demand";
  }
  if (score >= 45) {
    return "credible";
  }
  return "fragile";
}

export function computeManagerReputationDelta(input: ReputationDeltaInput): number {
  const resultSignal = (input.goalsFor > input.goalsAgainst ? 2 : input.goalsFor < input.goalsAgainst ? -2 : 0.5);
  const boardSignal = input.boardDelta * 0.35;
  const fanSignal = input.fanDelta * 0.25;
  const expectationSignal = input.context.expectationBand === "underdog" ? 0.7 : 0;
  const mediaPenalty = input.context.mediaCommentTone === "provocative" ? -1 : 0;

  return clamp(resultSignal + boardSignal + fanSignal + expectationSignal + mediaPenalty, -8, 8);
}

export function deriveManagerCareerLeverageSnapshot(
  input: ManagerCareerLeverageInput
): ManagerCareerLeverageSnapshot {
  const rawScore = clamp(
    input.managerReputation * 0.6 + input.boardConfidence * 0.25 + input.fanSentiment * 0.15,
    0,
    100
  );
  const score = Number(rawScore.toFixed(2));
  const band = deriveCareerLeverageBand(score);

  return {
    score,
    band,
    reasonSummary:
      `Career leverage is ${band} (${score.toFixed(1)}) from manager reputation ` +
      `${input.managerReputation.toFixed(1)}, board confidence ${input.boardConfidence.toFixed(1)}, ` +
      `and fan sentiment ${input.fanSentiment.toFixed(1)}.`
  };
}
