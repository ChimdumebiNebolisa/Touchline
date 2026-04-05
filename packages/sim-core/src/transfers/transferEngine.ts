import { buildTransferDemandBreakdown, scoreTransferWageFit } from "./demandModel.js";

export interface TransferTargetProfile {
  id: string;
  name: string;
  roleFit: number;
  projectFit: number;
  wageDemand: number;
  pathwayPreference: number;
  competitionTolerance: number;
  reputationSensitivity: number;
}

export interface TransferEvaluationContext {
  clubWageBudget: number;
  clubStature: number;
  managerReputation: number;
  squadCompetition: number;
  pathwayClarity: number;
  recentPromiseBreak: boolean;
  boardWageDiscipline: number;
}

export interface TransferDecision {
  accepted: boolean;
  score: number;
  blockingActor: "none" | "board" | "sporting-director" | "player";
  reasonSummary: string[];
}

export interface TransferFollowUpEvent {
  targetId: string;
  targetName: string;
  accepted: boolean;
  reasonSummary: string[];
}

export function evaluateTransferDecision(
  target: TransferTargetProfile,
  context: TransferEvaluationContext
): TransferDecision {
  const wageScore = scoreTransferWageFit(target, context);
  if (wageScore <= -2.5) {
    return {
      accepted: false,
      score: wageScore,
      blockingActor: "board",
      reasonSummary: [
        "Board blocked the move because wage demand exceeds wage-structure discipline.",
        `Target wage demand ${target.wageDemand} is outside allowed budget profile.`
      ]
    };
  }

  const demand = buildTransferDemandBreakdown(target, context);

  const accepted = demand.totalScore >= 1.1;
  const reasons: string[] = [
    `Role fit contribution: ${demand.roleScore.toFixed(2)}.`,
    `Project fit contribution: ${demand.projectScore.toFixed(2)}.`,
    `Pathway clarity contribution: ${demand.pathwayScore.toFixed(2)}.`,
    `Squad competition contribution: ${demand.competitionScore.toFixed(2)}.`,
    `Manager reputation contribution: ${demand.reputationScore.toFixed(2)}.`
  ];

  if (context.recentPromiseBreak) {
    reasons.push("Recent broken promise reduced player trust in the manager pathway.");
  }
  if (demand.wageScore < 0) {
    reasons.push("Wage demand pressured board and sporting-director wage discipline.");
  }

  if (!accepted && demand.reputationScore < -0.6) {
    return {
      accepted,
      score: demand.totalScore,
      blockingActor: "player",
      reasonSummary: [
        ...reasons,
        "Player side rejected due to weak confidence in manager project credibility."
      ]
    };
  }

  return {
    accepted,
    score: demand.totalScore,
    blockingActor: accepted ? "none" : "sporting-director",
    reasonSummary: reasons
  };
}

export function buildTransferFollowUpEvent(
  target: TransferTargetProfile,
  context: TransferEvaluationContext
): TransferFollowUpEvent {
  const decision = evaluateTransferDecision(target, context);
  return {
    targetId: target.id,
    targetName: target.name,
    accepted: decision.accepted,
    reasonSummary: decision.reasonSummary
  };
}
