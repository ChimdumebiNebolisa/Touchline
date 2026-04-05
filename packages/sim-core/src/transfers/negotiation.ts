import {
  evaluateTransferDecision,
  type TransferDecision,
  type TransferEvaluationContext,
  type TransferTargetProfile
} from "./transferEngine.js";

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
