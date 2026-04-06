import type { CountryPack, DivisionDefinition } from "../shared/types.js";

export interface SimulationDepthContractEvaluation {
  holds: boolean;
  deepDivisionIds: string[];
  shadowDivisionIds: string[];
  violations: string[];
}

export interface ShadowLeagueContinuityLink {
  higherTierDivisionId: string;
  lowerTierDivisionId: string;
}

export interface ShadowLeagueContextSnapshot {
  countryId: string;
  deepDivisionIds: string[];
  shadowDivisionIds: string[];
  transferSupplyClubIds: string[];
  loanDestinationClubIds: string[];
  continuityLinks: ShadowLeagueContinuityLink[];
  reasonSummary: string[];
}

function sortDivisionsByTier(divisions: DivisionDefinition[]): DivisionDefinition[] {
  return [...divisions].sort((first, second) => first.tier - second.tier || first.id.localeCompare(second.id));
}

export function evaluateSimulationDepthContract(pack: CountryPack): SimulationDepthContractEvaluation {
  const sortedDivisions = sortDivisionsByTier(pack.divisions);

  const deepDivisionIds = sortedDivisions
    .filter((division) => division.simulationDepth === "deep")
    .map((division) => division.id);
  const shadowDivisionIds = sortedDivisions
    .filter((division) => division.simulationDepth === "shadow")
    .map((division) => division.id);

  const violations: string[] = [];

  for (const division of sortedDivisions) {
    if (division.tier <= 2 && division.simulationDepth !== "deep") {
      violations.push(`Division ${division.id} tier ${division.tier} must be deep simulated.`);
    }

    if (division.tier > 2 && division.simulationDepth !== "shadow") {
      violations.push(`Division ${division.id} tier ${division.tier} must be shadow simulated.`);
    }
  }

  return {
    holds: violations.length === 0,
    deepDivisionIds,
    shadowDivisionIds,
    violations
  };
}

export function buildShadowLeagueContextSnapshot(pack: CountryPack): ShadowLeagueContextSnapshot {
  const contract = evaluateSimulationDepthContract(pack);
  if (!contract.holds) {
    throw new Error(`Shadow-league contract violation: ${contract.violations.join(" | ")}`);
  }

  const sortedDivisions = sortDivisionsByTier(pack.divisions);
  const shadowDivisions = sortedDivisions.filter((division) => division.simulationDepth === "shadow");

  const transferSupplyClubIds = shadowDivisions.flatMap((division) => division.clubs.map((club) => club.id));
  const loanDestinationClubIds = [...transferSupplyClubIds];

  const continuityLinks: ShadowLeagueContinuityLink[] = [];
  for (let index = 0; index < sortedDivisions.length - 1; index += 1) {
    continuityLinks.push({
      higherTierDivisionId: sortedDivisions[index].id,
      lowerTierDivisionId: sortedDivisions[index + 1].id
    });
  }

  return {
    countryId: pack.id,
    deepDivisionIds: contract.deepDivisionIds,
    shadowDivisionIds: contract.shadowDivisionIds,
    transferSupplyClubIds,
    loanDestinationClubIds,
    continuityLinks,
    reasonSummary: [
      `Deep divisions: ${contract.deepDivisionIds.length}; shadow divisions: ${contract.shadowDivisionIds.length}.`,
      `Shadow leagues contribute ${transferSupplyClubIds.length} transfer targets and ${loanDestinationClubIds.length} loan destinations.`,
      `Promotion-relegation continuity links across tiers: ${continuityLinks.length}.`
    ]
  };
}
