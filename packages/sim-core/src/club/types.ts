import type { MatchOutcome } from "../match/types.js";

export type ExpectationBand = "must-win" | "competitive" | "underdog";
export type MediaCommentTone = "calm" | "neutral" | "provocative";

export interface ClubPerceptionState {
  boardConfidence: number;
  fanSentiment: number;
  teamMorale: number;
  managerReputation: number;
}

export interface PostMatchPerceptionContext {
  expectationBand: ExpectationBand;
  identityStyleFit: number;
  financialPressure: number;
  mediaHeat: number;
  isDerby: boolean;
  brokenPromise: boolean;
  mediaCommentTone: MediaCommentTone;
}

export interface PostMatchFalloutInput {
  state: ClubPerceptionState;
  matchOutcome: MatchOutcome;
  context: PostMatchPerceptionContext;
}

export interface PostMatchFalloutResult {
  nextState: ClubPerceptionState;
  deltas: {
    boardConfidence: number;
    fanSentiment: number;
    teamMorale: number;
    managerReputation: number;
  };
  reasonSummary: string[];
}
