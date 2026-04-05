import type { TransferEvaluationContext, TransferTargetProfile } from "./transferEngine.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

export interface TransferDemandBreakdown {
  wageScore: number;
  roleScore: number;
  projectScore: number;
  statureScore: number;
  competitionScore: number;
  pathwayScore: number;
  reputationScore: number;
  promisePenalty: number;
  totalScore: number;
}

export function scoreTransferWageFit(
  target: TransferTargetProfile,
  context: TransferEvaluationContext
): number {
  const wageRatio = target.wageDemand / Math.max(1, context.clubWageBudget);
  const disciplinePenalty = context.boardWageDiscipline * 0.35;
  return clamp(2 - wageRatio * 2.7 - disciplinePenalty, -3, 3);
}

export function buildTransferDemandBreakdown(
  target: TransferTargetProfile,
  context: TransferEvaluationContext
): TransferDemandBreakdown {
  const wageScore = scoreTransferWageFit(target, context);
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

  return {
    wageScore,
    roleScore,
    projectScore,
    statureScore,
    competitionScore,
    pathwayScore,
    reputationScore,
    promisePenalty,
    totalScore
  };
}
