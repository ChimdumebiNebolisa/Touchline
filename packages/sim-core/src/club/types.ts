import type { MatchOutcome } from "../match/types.js";

export type ExpectationBand = "must-win" | "competitive" | "underdog";
export type MediaCommentTone = "calm" | "neutral" | "provocative";

export interface ClubPerceptionState {
  boardConfidence: number;
  fanSentiment: number;
  teamMorale: number;
  managerReputation: number;
}

export type ManagerCareerLeverageBand = "fragile" | "credible" | "in-demand" | "elite";

export interface ManagerCareerLeverageSnapshot {
  score: number;
  band: ManagerCareerLeverageBand;
  reasonSummary: string;
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
  careerLeverage: ManagerCareerLeverageSnapshot;
  reasonSummary: string[];
}
