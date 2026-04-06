import type {
  CountryPack,
  CountryPackValidationError,
  CountryPackValidationResult,
  DivisionDefinition
} from "../shared/types.js";

function hasTopTwoTiers(divisions: DivisionDefinition[]): boolean {
  const tiers = new Set(divisions.map((division) => division.tier));
  return tiers.has(1) && tiers.has(2);
}

export function validateCountryPack(pack: CountryPack): CountryPackValidationResult {
  const errors: CountryPackValidationError[] = [];

  if (!pack.divisions.length) {
    errors.push({
      code: "MISSING_DIVISIONS",
      message: "Country pack must include at least one division."
    });
    return { valid: false, errors };
  }

  for (const division of pack.divisions) {
    if (!Number.isInteger(division.tier) || division.tier <= 0) {
      errors.push({
        code: "MALFORMED_DIVISION",
        message: `Division ${division.id} has an invalid tier.`
      });
    }
  }

  if (!hasTopTwoTiers(pack.divisions)) {
    errors.push({
      code: "MISSING_TOP_TWO_TIERS",
      message: "Country pack must include tier 1 and tier 2 divisions."
    });
  }

  for (const division of pack.divisions) {
    const shouldBeDeep = division.tier <= 2;
    if (shouldBeDeep && division.simulationDepth !== "deep") {
      errors.push({
        code: "TOP_TWO_NOT_DEEP",
        message: `Division ${division.id} must be deep simulated.`
      });
    }

    const shouldBeShadow = division.tier > 2;
    if (shouldBeShadow && division.simulationDepth !== "shadow") {
      errors.push({
        code: "NON_TOP_TWO_NOT_SHADOW",
        message: `Division ${division.id} must be shadow simulated.`
      });
    }
  }

  const clubIds = new Set<string>();
  let playableClubCount = 0;
  for (const division of pack.divisions) {
    for (const club of division.clubs) {
      if (clubIds.has(club.id)) {
        errors.push({
          code: "DUPLICATE_CLUB_ID",
          message: `Club id ${club.id} is duplicated across divisions.`
        });
      }
      clubIds.add(club.id);

      if (club.isPlayable) {
        playableClubCount += 1;
      }
    }
  }

  if (playableClubCount === 0) {
    errors.push({
      code: "NO_PLAYABLE_CLUB",
      message: "Country pack must include at least one playable club."
    });
  }

  return {
    valid: errors.length === 0,
    errors
  };
}

export function assertValidCountryPack(pack: CountryPack): CountryPack {
  const validation = validateCountryPack(pack);
  if (!validation.valid) {
    const summary = validation.errors.map((error) => error.message).join(" | ");
    throw new Error(`Invalid country pack: ${summary}`);
  }

  return pack;
}
