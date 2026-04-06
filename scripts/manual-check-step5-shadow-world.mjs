import { readFileSync } from "node:fs";
import { resolve } from "node:path";

import {
  buildShadowLeagueContextSnapshot,
  evaluateSimulationDepthContract,
  parseCountryPackJson,
  validateCountryPack
} from "../packages/sim-core/dist/src/index.js";

function loadFirstCountryPack() {
  const jsonPath = resolve(
    import.meta.dirname,
    "..",
    "packages",
    "sim-core",
    "content",
    "countries",
    "first-country.json"
  );
  const rawJson = readFileSync(jsonPath, "utf-8");
  return parseCountryPackJson(rawJson);
}

function createSecondCountryPack() {
  return {
    id: "country-avalon",
    name: "Avalon",
    divisions: [
      {
        id: "avalon-premier",
        name: "Avalon Premier",
        tier: 1,
        simulationDepth: "deep",
        clubs: [
          { id: "ava-a", name: "Avalon A", shortName: "AA", isPlayable: true },
          { id: "ava-b", name: "Avalon B", shortName: "AB", isPlayable: false }
        ]
      },
      {
        id: "avalon-championship",
        name: "Avalon Championship",
        tier: 2,
        simulationDepth: "deep",
        clubs: [
          { id: "ava-c", name: "Avalon C", shortName: "AC", isPlayable: false },
          { id: "ava-d", name: "Avalon D", shortName: "AD", isPlayable: false }
        ]
      },
      {
        id: "avalon-league-one",
        name: "Avalon League One",
        tier: 3,
        simulationDepth: "shadow",
        clubs: [
          { id: "ava-e", name: "Avalon E", shortName: "AE", isPlayable: false },
          { id: "ava-f", name: "Avalon F", shortName: "AF", isPlayable: false }
        ]
      },
      {
        id: "avalon-league-two",
        name: "Avalon League Two",
        tier: 4,
        simulationDepth: "shadow",
        clubs: [
          { id: "ava-g", name: "Avalon G", shortName: "AG", isPlayable: false },
          { id: "ava-h", name: "Avalon H", shortName: "AH", isPlayable: false }
        ]
      }
    ]
  };
}

function evaluatePack(pack, label) {
  const validation = validateCountryPack(pack);
  const contract = evaluateSimulationDepthContract(pack);
  const snapshot = buildShadowLeagueContextSnapshot(pack);

  return {
    label,
    validation,
    contract,
    snapshot
  };
}

const firstCountry = evaluatePack(loadFirstCountryPack(), "first-country");
const secondCountry = evaluatePack(createSecondCountryPack(), "second-country-smoke");

console.log("Step 5 Manual Check");
console.log("- Shadow league context summaries");
console.table(
  [firstCountry, secondCountry].map((entry) => ({
    label: entry.label,
    countryId: entry.snapshot.countryId,
    deepDivisionCount: entry.snapshot.deepDivisionIds.length,
    shadowDivisionCount: entry.snapshot.shadowDivisionIds.length,
    transferSupplyCount: entry.snapshot.transferSupplyClubIds.length,
    loanDestinationCount: entry.snapshot.loanDestinationClubIds.length,
    continuityLinkCount: entry.snapshot.continuityLinks.length,
    contractHolds: entry.contract.holds,
    validationValid: entry.validation.valid
  }))
);

console.log("- First-country transfer and continuity sample");
console.table([
  {
    transferSupplyClubIds: firstCountry.snapshot.transferSupplyClubIds.join(", "),
    loanDestinationClubIds: firstCountry.snapshot.loanDestinationClubIds.join(", "),
    continuityLinks: firstCountry.snapshot.continuityLinks
      .map((link) => `${link.higherTierDivisionId}->${link.lowerTierDivisionId}`)
      .join(" | ")
  }
]);

const checks = {
  firstCountryValidationPasses: firstCountry.validation.valid,
  secondCountryValidationPasses: secondCountry.validation.valid,
  topTwoDeepRestShadowContractHolds:
    firstCountry.contract.holds && secondCountry.contract.holds,
  shadowLeaguesCreateTransferSupply:
    firstCountry.snapshot.transferSupplyClubIds.length > 0 &&
    secondCountry.snapshot.transferSupplyClubIds.length > 0,
  shadowLeaguesCreateLoanDestinations:
    firstCountry.snapshot.loanDestinationClubIds.length > 0 &&
    secondCountry.snapshot.loanDestinationClubIds.length > 0,
  shadowLeaguesProvidePromotionContinuity:
    firstCountry.snapshot.continuityLinks.length >= firstCountry.snapshot.shadowDivisionIds.length &&
    secondCountry.snapshot.continuityLinks.length >= secondCountry.snapshot.shadowDivisionIds.length,
  secondCountryIsContentOnlyCompatible:
    secondCountry.snapshot.countryId === "country-avalon" &&
    secondCountry.snapshot.shadowDivisionIds.length === 2
};

console.log("- Threshold checks");
console.table([checks]);

if (Object.values(checks).some((result) => !result)) {
  console.error("Step 5 manual check failed expected shadow-world thresholds.");
  process.exit(1);
}
