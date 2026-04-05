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

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function scoreWageFit(target: TransferTargetProfile, context: TransferEvaluationContext): number {
  const wageRatio = target.wageDemand / Math.max(1, context.clubWageBudget);
  const disciplinePenalty = context.boardWageDiscipline * 0.35;
  return clamp(2 - wageRatio * 2.7 - disciplinePenalty, -3, 3);
}

export function evaluateTransferDecision(
  target: TransferTargetProfile,
  context: TransferEvaluationContext
): TransferDecision {
  const wageScore = scoreWageFit(target, context);
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

  const roleScore = clamp((target.roleFit - 0.5) * 4, -2, 2);
  const projectScore = clamp((target.projectFit - 0.5) * 4, -2, 2);
  const statureScore = clamp((context.clubStature - 0.5) * 2, -1, 1);
  const competitionScore = clamp(
    -Math.abs(context.squadCompetition - target.competitionTolerance) * 2.4,
    -2,
    0.6
  );
  const pathwayScore = clamp((context.pathwayClarity * target.pathwayPreference - 0.25) * 2, -1.5, 1.5);
  const reputationScore =
    clamp((context.managerReputation / 100 - 0.5) * 4, -2, 2) * (0.6 + target.reputationSensitivity * 0.8);
  const promisePenalty = context.recentPromiseBreak
    ? -(2 + target.reputationSensitivity * 1.6 + target.pathwayPreference * 1.2)
    : 0;

  const totalScore =
    wageScore +
    roleScore +
    projectScore +
    statureScore +
    competitionScore +
    pathwayScore +
    reputationScore +
    promisePenalty;

  const accepted = totalScore >= 1.1;
  const reasons: string[] = [
    `Role fit contribution: ${roleScore.toFixed(2)}.`,
    `Project fit contribution: ${projectScore.toFixed(2)}.`,
    `Manager reputation contribution: ${reputationScore.toFixed(2)}.`
  ];

  if (context.recentPromiseBreak) {
    reasons.push("Recent broken promise reduced player trust in the manager pathway.");
  }
  if (wageScore < 0) {
    reasons.push("Wage demand pressured board and sporting-director wage discipline.");
  }

  if (!accepted && reputationScore < -0.6) {
    return {
      accepted,
      score: totalScore,
      blockingActor: "player",
      reasonSummary: [
        ...reasons,
        "Player side rejected due to weak confidence in manager project credibility."
      ]
    };
  }

  return {
    accepted,
    score: totalScore,
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
