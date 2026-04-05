import { deriveSackRiskPressureState, evaluateBoardExpectationContext } from "../club/board.js";
import type { BoardExpectationEvaluation } from "../club/board.js";
import type { SackRiskPressureLevel, SackRiskTrend } from "../club/board.js";

import { summarizeCompletedSeason } from "./season.js";
import type { CompletedSeasonSummary, SeasonState } from "./season.js";

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

export interface SeasonSackRiskPressureSummary {
  samplesProcessed: number;
  currentLevel: SackRiskPressureLevel;
  currentTrend: SackRiskTrend;
  warningOrHigherStreak: number;
  criticalStreak: number;
  maxWarningOrHigherStreak: number;
  maxCriticalStreak: number;
}

export type SeasonSackDecision = "retain" | "review" | "sack";

export interface SeasonSackDecisionResult {
  decision: SeasonSackDecision;
  reasonSummary: string[];
}

export interface SeasonBoardDecisionSnapshot {
  boardEvaluation: BoardExpectationEvaluation;
  pressureSummary: SeasonSackRiskPressureSummary;
  sackDecision: SeasonSackDecisionResult;
}

export interface CompletedSeasonBoardSummary {
  completedSeason: CompletedSeasonSummary;
  decisionSnapshotsByClubId: Record<string, SeasonBoardDecisionSnapshot>;
  resolutionStatus: SeasonBoardResolutionStatus;
}

export interface SeasonSackOutcome {
  clubId: string;
  leaguePosition: number;
  reasonSummary: string[];
  sackRisk: number;
}

export interface SeasonBoardResolutionStatus {
  retainedClubIds: string[];
  reviewClubIds: string[];
  sackedClubIds: string[];
}

export interface SeasonBoardActionCounts {
  retainCount: number;
  reviewCount: number;
  sackCount: number;
  totalClubs: number;
}

interface SeasonBoardDecisionSnapshotSource {
  completedSeason: CompletedSeasonSummary;
  decisionSnapshotsByClubId: Record<string, SeasonBoardDecisionSnapshot>;
}

export function summarizeSeasonSackRiskPressureTimeline(
  sackRiskTimeline: number[]
): SeasonSackRiskPressureSummary {
  if (!sackRiskTimeline.length) {
    return {
      samplesProcessed: 0,
      currentLevel: "stable",
      currentTrend: "steady",
      warningOrHigherStreak: 0,
      criticalStreak: 0,
      maxWarningOrHigherStreak: 0,
      maxCriticalStreak: 0
    };
  }

  let previousRisk: number | undefined;
  let warningOrHigherStreak = 0;
  let criticalStreak = 0;
  let maxWarningOrHigherStreak = 0;
  let maxCriticalStreak = 0;
  let currentLevel: SackRiskPressureLevel = "stable";
  let currentTrend: SackRiskTrend = "steady";

  for (const risk of sackRiskTimeline) {
    const pressure = deriveSackRiskPressureState(risk, previousRisk);
    currentLevel = pressure.level;
    currentTrend = pressure.trend;

    if (pressure.level === "critical") {
      warningOrHigherStreak += 1;
      criticalStreak += 1;
    } else if (pressure.level === "warning") {
      warningOrHigherStreak += 1;
      criticalStreak = 0;
    } else {
      warningOrHigherStreak = 0;
      criticalStreak = 0;
    }

    maxWarningOrHigherStreak = Math.max(maxWarningOrHigherStreak, warningOrHigherStreak);
    maxCriticalStreak = Math.max(maxCriticalStreak, criticalStreak);
    previousRisk = risk;
  }

  return {
    samplesProcessed: sackRiskTimeline.length,
    currentLevel,
    currentTrend,
    warningOrHigherStreak,
    criticalStreak,
    maxWarningOrHigherStreak,
    maxCriticalStreak
  };
}

export function deriveSeasonSackDecisionFromPressureSummary(
  summary: SeasonSackRiskPressureSummary
): SeasonSackDecisionResult {
  if (summary.samplesProcessed === 0) {
    return {
      decision: "retain",
      reasonSummary: [
        "No season sack-risk samples were processed, so no dismissal decision can be justified."
      ]
    };
  }

  const reasons: string[] = [];

  const sustainedCriticalPressure =
    summary.currentLevel === "critical" && summary.criticalStreak >= 2 && summary.currentTrend !== "falling";
  const repeatedCriticalRuns = summary.maxCriticalStreak >= 3;
  const prolongedWarningOrHigherPressure =
    summary.warningOrHigherStreak >= 5 && summary.currentTrend !== "falling";

  if (sustainedCriticalPressure) {
    reasons.push(
      "Current risk pressure is critical for at least two consecutive samples without a falling trend."
    );
  }
  if (repeatedCriticalRuns) {
    reasons.push("Season timeline includes a sustained critical run of three or more samples.");
  }
  if (prolongedWarningOrHigherPressure) {
    reasons.push("Warning-or-higher pressure persisted for at least five consecutive samples.");
  }

  if (reasons.length > 0) {
    return {
      decision: "sack",
      reasonSummary: reasons
    };
  }

  const reviewPressure =
    summary.currentLevel !== "stable" ||
    summary.warningOrHigherStreak >= 3 ||
    summary.maxWarningOrHigherStreak >= 4;

  if (reviewPressure) {
    const reviewReasons = [
      "Current or recent pressure indicates the board should run a formal manager review.",
      `Current level is ${summary.currentLevel} with ${summary.currentTrend} trend.`
    ];

    return {
      decision: "review",
      reasonSummary: reviewReasons
    };
  }

  return {
    decision: "retain",
    reasonSummary: [
      "Pressure remains below dismissal and review thresholds, so the manager is retained."
    ]
  };
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

export function evaluateSeasonBoardDecisions(
  state: SeasonState,
  contextByClubId: Record<string, SeasonBoardContext>,
  sackRiskTimelineByClubId: Record<string, number[]>
): Record<string, SeasonBoardDecisionSnapshot> {
  const boardEvaluations = evaluateSeasonBoardContext(state, contextByClubId);
  const snapshots: Record<string, SeasonBoardDecisionSnapshot> = {};

  for (const row of state.standings) {
    const sackRiskTimeline = sackRiskTimelineByClubId[row.clubId];
    if (!sackRiskTimeline) {
      throw new Error(`Missing sack-risk timeline for club ${row.clubId}.`);
    }

    const pressureSummary = summarizeSeasonSackRiskPressureTimeline(sackRiskTimeline);
    snapshots[row.clubId] = {
      boardEvaluation: boardEvaluations[row.clubId],
      pressureSummary,
      sackDecision: deriveSeasonSackDecisionFromPressureSummary(pressureSummary)
    };
  }

  return snapshots;
}

export function summarizeCompletedSeasonBoardOutcomes(
  state: SeasonState,
  contextByClubId: Record<string, SeasonBoardContext>,
  sackRiskTimelineByClubId: Record<string, number[]>,
  promotionSpots: number,
  relegationSpots: number
): CompletedSeasonBoardSummary {
  const completedSeason = summarizeCompletedSeason(state, promotionSpots, relegationSpots);
  const seasonStateWithFinalStandings: SeasonState = {
    ...state,
    standings: completedSeason.finalStandings
  };

  const decisionSnapshotsByClubId = evaluateSeasonBoardDecisions(
    seasonStateWithFinalStandings,
    contextByClubId,
    sackRiskTimelineByClubId
  );

  const summaryBase = {
    completedSeason,
    decisionSnapshotsByClubId
  };

  return {
    ...summaryBase,
    resolutionStatus: summarizeSeasonBoardResolutionStatus(summaryBase)
  };
}

export function extractSeasonSackOutcomes(summary: CompletedSeasonBoardSummary): SeasonSackOutcome[] {
  const outcomes: SeasonSackOutcome[] = [];

  for (let index = 0; index < summary.completedSeason.finalStandings.length; index += 1) {
    const row = summary.completedSeason.finalStandings[index];
    const snapshot = summary.decisionSnapshotsByClubId[row.clubId];
    if (!snapshot) {
      throw new Error(`Missing board decision snapshot for club ${row.clubId}.`);
    }

    if (snapshot.sackDecision.decision !== "sack") {
      continue;
    }

    outcomes.push({
      clubId: row.clubId,
      leaguePosition: index + 1,
      reasonSummary: snapshot.sackDecision.reasonSummary,
      sackRisk: snapshot.boardEvaluation.sackRisk
    });
  }

  return outcomes;
}

export function summarizeSeasonBoardResolutionStatus(
  summary: SeasonBoardDecisionSnapshotSource
): SeasonBoardResolutionStatus {
  const retainedClubIds: string[] = [];
  const reviewClubIds: string[] = [];
  const sackedClubIds: string[] = [];

  for (const row of summary.completedSeason.finalStandings) {
    const snapshot = summary.decisionSnapshotsByClubId[row.clubId];
    if (!snapshot) {
      throw new Error(`Missing board decision snapshot for club ${row.clubId}.`);
    }

    switch (snapshot.sackDecision.decision) {
      case "retain":
        retainedClubIds.push(row.clubId);
        break;
      case "review":
        reviewClubIds.push(row.clubId);
        break;
      case "sack":
        sackedClubIds.push(row.clubId);
        break;
      default:
        throw new Error(`Unknown sack decision status for club ${row.clubId}.`);
    }
  }

  return {
    retainedClubIds,
    reviewClubIds,
    sackedClubIds
  };
}

export function summarizeSeasonBoardActionCounts(
  status: SeasonBoardResolutionStatus
): SeasonBoardActionCounts {
  const retainCount = status.retainedClubIds.length;
  const reviewCount = status.reviewClubIds.length;
  const sackCount = status.sackedClubIds.length;

  return {
    retainCount,
    reviewCount,
    sackCount,
    totalClubs: retainCount + reviewCount + sackCount
  };
}
