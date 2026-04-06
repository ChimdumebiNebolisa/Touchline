import { createSeededRandom } from "../match/rng.js";
import type {
  AcademyIntakeInput,
  AcademyIntakeResult,
  AcademyPathwayRecommendation,
  AcademyProspect,
  AcademyProspectTier
} from "./types.js";

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function hashString(value: string): number {
  let hash = 0;
  for (let index = 0; index < value.length; index += 1) {
    hash = (hash * 31 + value.charCodeAt(index)) >>> 0;
  }
  return hash;
}

function deriveTier(randomRoll: number, academyQuality: number): AcademyProspectTier {
  const eliteChance = 0.01 + academyQuality * 0.03;
  const highChance = 0.08 + academyQuality * 0.12;

  if (randomRoll < eliteChance) {
    return "elite";
  }

  if (randomRoll < eliteChance + highChance) {
    return "high";
  }

  return "regular";
}

function buildPathwayRecommendation(
  readiness: number,
  potential: number,
  pathwayBias: number
): AcademyPathwayRecommendation {
  const firstTeamSignal = readiness * 0.7 + potential * 0.2 + pathwayBias * 18;

  if (firstTeamSignal >= 58) {
    return "first-team-minutes";
  }

  if (potential >= 62 && pathwayBias >= 0.45) {
    return "loan-pathway";
  }

  return "academy-development";
}

function buildProspect(
  clubId: string,
  seasonYear: number,
  index: number,
  academyQuality: number,
  pathwayBias: number,
  nextRandom: () => number
): AcademyProspect {
  const tier = deriveTier(nextRandom(), academyQuality);
  const tierPotentialBoost =
    tier === "elite"
      ? 22 + nextRandom() * 6
      : tier === "high"
        ? 10 + nextRandom() * 6
        : 0;

  const potential = Math.round(
    clamp(
      30 + academyQuality * 36 + (nextRandom() - 0.5) * 24 + tierPotentialBoost,
      20,
      99
    )
  );

  const readiness = Math.round(
    clamp(
      22 + academyQuality * 20 + pathwayBias * 16 + (nextRandom() - 0.5) * 14,
      10,
      90
    )
  );

  return {
    id: `${clubId}-${seasonYear}-intake-${index + 1}`,
    potential,
    readiness,
    tier,
    pathwayRecommendation: buildPathwayRecommendation(readiness, potential, pathwayBias)
  };
}

export function generateAcademyIntake(input: AcademyIntakeInput): AcademyIntakeResult {
  if (input.intakeSize <= 0) {
    throw new Error("Academy intake size must be greater than zero.");
  }

  const academyQuality = clamp(input.academyQuality, 0, 1);
  const pathwayBias = clamp(input.pathwayBias, 0, 1);
  const randomSeed =
    (input.seed >>> 0) ^
    ((input.seasonYear * 2654435761) >>> 0) ^
    hashString(input.clubId);
  const random = createSeededRandom(randomSeed);

  const prospects = Array.from({ length: input.intakeSize }, (_, index) =>
    buildProspect(
      input.clubId,
      input.seasonYear,
      index,
      academyQuality,
      pathwayBias,
      random.next
    )
  );

  const eliteCount = prospects.filter((prospect) => prospect.tier === "elite").length;
  const highPotentialCount = prospects.filter((prospect) => prospect.tier !== "regular").length;
  const averagePotential = prospects.reduce((sum, prospect) => sum + prospect.potential, 0) / prospects.length;

  return {
    clubId: input.clubId,
    seasonYear: input.seasonYear,
    prospects,
    eliteCount,
    highPotentialCount,
    averagePotential,
    conciseSummary: [
      `Intake ${input.seasonYear}: ${eliteCount} elite, ${highPotentialCount} high-potential from ${prospects.length} prospects.`,
      `Average potential ${averagePotential.toFixed(2)} with pathway bias ${pathwayBias.toFixed(2)}.`
    ]
  };
}
