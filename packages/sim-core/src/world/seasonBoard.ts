import { evaluateBoardExpectationContext } from "../club/board.js";
import type { BoardExpectationEvaluation } from "../club/board.js";

import type { SeasonState } from "./season.js";

export interface SeasonBoardContext {
  preseasonObjectivePosition: number;
  clubStature: number;
  financialPressure: number;
  recentPointsPerMatch: number;
  styleAlignment: number;
  derbyResult: "none" | "win" | "draw" | "loss";
}

export interface SeasonBoardStaticContext {
  preseasonObjectivePosition: number;
  clubStature: number;
  financialPressure: number;
  styleAlignment: number;
  derbyResult: "none" | "win" | "draw" | "loss";
}

export function deriveSeasonBoardContextFromSeasonState(
  state: SeasonState,
  staticContextByClubId: Record<string, SeasonBoardStaticContext>
): Record<string, SeasonBoardContext> {
  const contextByClubId: Record<string, SeasonBoardContext> = {};

  for (const row of state.standings) {
    const staticContext = staticContextByClubId[row.clubId];
    if (!staticContext) {
      throw new Error(`Missing static board context for club ${row.clubId}.`);
    }

    const recentPointsPerMatch = row.played > 0 ? row.points / row.played : 1.25;
    contextByClubId[row.clubId] = {
      ...staticContext,
      recentPointsPerMatch
    };
  }

  return contextByClubId;
}

export function evaluateSeasonBoardContext(
  state: SeasonState,
  contextByClubId: Record<string, SeasonBoardContext>
): Record<string, BoardExpectationEvaluation> {
  const evaluations: Record<string, BoardExpectationEvaluation> = {};

  for (let index = 0; index < state.standings.length; index += 1) {
    const row = state.standings[index];
    const context = contextByClubId[row.clubId];
    if (!context) {
      throw new Error(`Missing board context for club ${row.clubId}.`);
    }

    evaluations[row.clubId] = evaluateBoardExpectationContext({
      leaguePosition: index + 1,
      totalTeams: state.standings.length,
      preseasonObjectivePosition: context.preseasonObjectivePosition,
      clubStature: context.clubStature,
      financialPressure: context.financialPressure,
      recentPointsPerMatch: context.recentPointsPerMatch,
      styleAlignment: context.styleAlignment,
      derbyResult: context.derbyResult
    });
  }

  return evaluations;
}
