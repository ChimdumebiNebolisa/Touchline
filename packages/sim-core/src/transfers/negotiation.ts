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
