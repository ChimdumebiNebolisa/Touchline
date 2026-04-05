import {
  evaluateTransferDecision,
  type TransferDecision,
  type TransferEvaluationContext,
  type TransferTargetProfile
} from "./transferEngine.js";
import { buildTransferDemandBreakdown, type TransferDemandBreakdown } from "./demandModel.js";

export interface TransferNegotiationComparison {
  firstDecision: TransferDecision;
  secondDecision: TransferDecision;
  diverged: boolean;
  changedContextFactors: string[];
  reasonSummary: string[];
}

export interface NegotiationScenarioVariant {
  pathwayClarity: number;
  squadCompetition: number;
  recentPromiseBreak: boolean;
}

export interface ReputationBandNegotiationOutcome {
  managerReputation: number;
  attempts: number;
  acceptedCount: number;
  acceptanceRate: number;
}

export interface TransferNegotiationLogEntry {
  attemptId: string;
  managerReputation: number;
  accepted: boolean;
  score: number;
  blockingActor: TransferDecision["blockingActor"];
  reasonSummary: string[];
}

export interface TransferNegotiationExplainabilityArtifact {
  comparison: TransferNegotiationComparison;
  firstDemandBreakdown: TransferDemandBreakdown;
  secondDemandBreakdown: TransferDemandBreakdown;
  demandDelta: TransferDemandBreakdown;
  primaryNonFeeDrivers: string[];
  nonFeeContextDroveDivergence: boolean;
  conciseSummary: string;
}

type NonFeeDemandFactor =
  | "roleScore"
  | "projectScore"
  | "statureScore"
  | "competitionScore"
  | "pathwayScore"
  | "reputationScore"
  | "promisePenalty";

const NON_FEE_FACTORS: NonFeeDemandFactor[] = [
  "roleScore",
  "projectScore",
  "statureScore",
  "competitionScore",
  "pathwayScore",
  "reputationScore",
  "promisePenalty"
];

const NON_FEE_FACTOR_LABELS: Record<NonFeeDemandFactor, string> = {
  roleScore: "Role fit",
  projectScore: "Project fit",
  statureScore: "Club stature",
  competitionScore: "Squad competition",
  pathwayScore: "Pathway clarity",
  reputationScore: "Manager reputation",
  promisePenalty: "Promise trust"
};

const NON_FEE_CONTEXT_FIELDS = new Set([
  "clubStature",
  "managerReputation",
  "squadCompetition",
  "pathwayClarity",
  "recentPromiseBreak"
]);

function identifyChangedContextFactors(
  firstContext: TransferEvaluationContext,
  secondContext: TransferEvaluationContext
): string[] {
  const changed: string[] = [];

  if (firstContext.clubWageBudget !== secondContext.clubWageBudget) {
    changed.push("clubWageBudget");
  }
  if (firstContext.clubStature !== secondContext.clubStature) {
    changed.push("clubStature");
  }
  if (firstContext.managerReputation !== secondContext.managerReputation) {
    changed.push("managerReputation");
  }
  if (firstContext.squadCompetition !== secondContext.squadCompetition) {
    changed.push("squadCompetition");
  }
  if (firstContext.pathwayClarity !== secondContext.pathwayClarity) {
    changed.push("pathwayClarity");
  }
  if (firstContext.recentPromiseBreak !== secondContext.recentPromiseBreak) {
    changed.push("recentPromiseBreak");
  }
  if (firstContext.boardWageDiscipline !== secondContext.boardWageDiscipline) {
    changed.push("boardWageDiscipline");
  }

  return changed;
}

function buildDemandDelta(
  first: TransferDemandBreakdown,
  second: TransferDemandBreakdown
): TransferDemandBreakdown {
  return {
    wageScore: first.wageScore - second.wageScore,
    roleScore: first.roleScore - second.roleScore,
    projectScore: first.projectScore - second.projectScore,
    statureScore: first.statureScore - second.statureScore,
    competitionScore: first.competitionScore - second.competitionScore,
    pathwayScore: first.pathwayScore - second.pathwayScore,
    reputationScore: first.reputationScore - second.reputationScore,
    promisePenalty: first.promisePenalty - second.promisePenalty,
    totalScore: first.totalScore - second.totalScore
  };
}

function formatSignedValue(value: number): string {
  return `${value >= 0 ? "+" : ""}${value.toFixed(2)}`;
}

function identifyPrimaryNonFeeDrivers(demandDelta: TransferDemandBreakdown): string[] {
  return NON_FEE_FACTORS.map((factor) => ({
    factor,
    delta: demandDelta[factor]
  }))
    .filter((entry) => Math.abs(entry.delta) >= 0.05)
    .sort((first, second) => Math.abs(second.delta) - Math.abs(first.delta))
    .slice(0, 3)
    .map((entry) => `${NON_FEE_FACTOR_LABELS[entry.factor]} ${formatSignedValue(entry.delta)}`);
}

export function compareTransferNegotiationContexts(
  target: TransferTargetProfile,
  firstContext: TransferEvaluationContext,
  secondContext: TransferEvaluationContext
): TransferNegotiationComparison {
  const firstDecision = evaluateTransferDecision(target, firstContext);
  const secondDecision = evaluateTransferDecision(target, secondContext);
  const changedContextFactors = identifyChangedContextFactors(firstContext, secondContext);
  const diverged = firstDecision.accepted !== secondDecision.accepted;

  const reasonSummary = [
    `First context decision: ${firstDecision.accepted ? "accepted" : "rejected"} (score ${firstDecision.score.toFixed(2)}).`,
    `Second context decision: ${secondDecision.accepted ? "accepted" : "rejected"} (score ${secondDecision.score.toFixed(2)}).`,
    changedContextFactors.length
      ? `Changed context factors: ${changedContextFactors.join(", ")}.`
      : "No context factors changed between negotiations."
  ];

  if (diverged) {
    reasonSummary.push("Comparable-fee negotiations diverged due to contextual factors beyond wage terms.");
  }

  return {
    firstDecision,
    secondDecision,
    diverged,
    changedContextFactors,
    reasonSummary
  };
}

export function summarizeNegotiationAcceptanceByReputationBand(
  target: TransferTargetProfile,
  baseContext: Omit<TransferEvaluationContext, "managerReputation" | "pathwayClarity" | "squadCompetition" | "recentPromiseBreak">,
  managerReputationBands: number[],
  variants: NegotiationScenarioVariant[]
): ReputationBandNegotiationOutcome[] {
  if (!variants.length) {
    throw new Error("At least one negotiation scenario variant is required.");
  }

  return managerReputationBands.map((managerReputation) => {
    let acceptedCount = 0;

    for (const variant of variants) {
      const decision = evaluateTransferDecision(target, {
        ...baseContext,
        managerReputation,
        pathwayClarity: variant.pathwayClarity,
        squadCompetition: variant.squadCompetition,
        recentPromiseBreak: variant.recentPromiseBreak
      });

      if (decision.accepted) {
        acceptedCount += 1;
      }
    }

    return {
      managerReputation,
      attempts: variants.length,
      acceptedCount,
      acceptanceRate: acceptedCount / variants.length
    };
  });
}

export function buildTransferNegotiationLogSamples(
  target: TransferTargetProfile,
  contexts: TransferEvaluationContext[]
): TransferNegotiationLogEntry[] {
  return contexts.map((context, index) => {
    const decision = evaluateTransferDecision(target, context);
    return {
      attemptId: `attempt-${index + 1}`,
      managerReputation: context.managerReputation,
      accepted: decision.accepted,
      score: decision.score,
      blockingActor: decision.blockingActor,
      reasonSummary: decision.reasonSummary
    };
  });
}

export function buildEqualFeeNegotiationExplainabilityArtifact(
  target: TransferTargetProfile,
  firstContext: TransferEvaluationContext,
  secondContext: TransferEvaluationContext
): TransferNegotiationExplainabilityArtifact {
  const comparison = compareTransferNegotiationContexts(target, firstContext, secondContext);
  const firstDemandBreakdown = buildTransferDemandBreakdown(target, firstContext);
  const secondDemandBreakdown = buildTransferDemandBreakdown(target, secondContext);
  const demandDelta = buildDemandDelta(firstDemandBreakdown, secondDemandBreakdown);

  const nonFeeContextChanged = comparison.changedContextFactors.some((field) =>
    NON_FEE_CONTEXT_FIELDS.has(field)
  );
  const primaryNonFeeDrivers = identifyPrimaryNonFeeDrivers(demandDelta);
  const nonFeeContextDroveDivergence =
    comparison.diverged && nonFeeContextChanged && primaryNonFeeDrivers.length > 0;

  const conciseSummary = comparison.diverged
    ? `Equal-fee comparison diverged (${comparison.firstDecision.accepted ? "accepted" : "rejected"} vs ${comparison.secondDecision.accepted ? "accepted" : "rejected"}); primary non-fee drivers: ${primaryNonFeeDrivers.join(", ") || "none"}.`
    : `Equal-fee comparison aligned (${comparison.firstDecision.accepted ? "accepted" : "rejected"}); primary contextual deltas: ${primaryNonFeeDrivers.join(", ") || "none"}.`;

  return {
    comparison,
    firstDemandBreakdown,
    secondDemandBreakdown,
    demandDelta,
    primaryNonFeeDrivers,
    nonFeeContextDroveDivergence,
    conciseSummary
  };
}
