import { readFileSync } from "node:fs";
import { resolve } from "node:path";

import { describe, expect, it } from "vitest";

import {
  buildShadowLeagueContextSnapshot,
  evaluateSimulationDepthContract,
  parseCountryPackJson
} from "../src/index.js";
import type { CountryPack } from "../src/shared/types.js";

describe("shadow leagues", () => {
  it("flags top-two-deep and rest-shadow contract violations", () => {
    const invalidPack: CountryPack = {
      id: "country-invalid",
      name: "Invalid Country",
      divisions: [
        {
          id: "invalid-premier",
          name: "Invalid Premier",
          tier: 1,
          simulationDepth: "deep",
          clubs: [
            { id: "invalid-a", name: "Invalid A", shortName: "IA", isPlayable: true },
            { id: "invalid-b", name: "Invalid B", shortName: "IB", isPlayable: false }
          ]
        },
        {
          id: "invalid-championship",
          name: "Invalid Championship",
          tier: 2,
          simulationDepth: "deep",
          clubs: [
            { id: "invalid-c", name: "Invalid C", shortName: "IC", isPlayable: false },
            { id: "invalid-d", name: "Invalid D", shortName: "ID", isPlayable: false }
          ]
        },
        {
          id: "invalid-third-tier",
          name: "Invalid Third Tier",
          tier: 3,
          simulationDepth: "deep",
          clubs: [
            { id: "invalid-e", name: "Invalid E", shortName: "IE", isPlayable: false },
            { id: "invalid-f", name: "Invalid F", shortName: "IF", isPlayable: false }
          ]
        }
      ]
    };

    const contract = evaluateSimulationDepthContract(invalidPack);

    expect(contract.holds).toBe(false);
    expect(contract.violations).toContain(
      "Division invalid-third-tier tier 3 must be shadow simulated."
    );
  });

  it("builds deterministic shadow-league context snapshot from first-country content", () => {
    const jsonPath = resolve(import.meta.dirname, "..", "content", "countries", "first-country.json");
    const rawJson = readFileSync(jsonPath, "utf-8");
    const pack = parseCountryPackJson(rawJson);

    const firstSnapshot = buildShadowLeagueContextSnapshot(pack);
    const secondSnapshot = buildShadowLeagueContextSnapshot(pack);

    expect(secondSnapshot).toEqual(firstSnapshot);
    expect(firstSnapshot.countryId).toBe("country-novara");
    expect(firstSnapshot.deepDivisionIds).toEqual(["novara-premier", "novara-championship"]);
    expect(firstSnapshot.shadowDivisionIds).toEqual(["novara-league-one"]);
    expect(firstSnapshot.transferSupplyClubIds).toEqual([
      "nvr-lakeside-rovers",
      "nvr-crestfield"
    ]);
    expect(firstSnapshot.loanDestinationClubIds).toEqual(firstSnapshot.transferSupplyClubIds);
    expect(firstSnapshot.continuityLinks).toEqual([
      {
        higherTierDivisionId: "novara-premier",
        lowerTierDivisionId: "novara-championship"
      },
      {
        higherTierDivisionId: "novara-championship",
        lowerTierDivisionId: "novara-league-one"
      }
    ]);
    expect(firstSnapshot.reasonSummary.length).toBeGreaterThanOrEqual(2);
  });

  it("supports a second country pack as content-only shadow context input", () => {
    const secondCountryPack: CountryPack = {
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

    const snapshot = buildShadowLeagueContextSnapshot(secondCountryPack);

    expect(snapshot.countryId).toBe("country-avalon");
    expect(snapshot.shadowDivisionIds).toEqual(["avalon-league-one", "avalon-league-two"]);
    expect(snapshot.transferSupplyClubIds).toEqual(["ava-e", "ava-f", "ava-g", "ava-h"]);
    expect(snapshot.continuityLinks).toEqual([
      {
        higherTierDivisionId: "avalon-premier",
        lowerTierDivisionId: "avalon-championship"
      },
      {
        higherTierDivisionId: "avalon-championship",
        lowerTierDivisionId: "avalon-league-one"
      },
      {
        higherTierDivisionId: "avalon-league-one",
        lowerTierDivisionId: "avalon-league-two"
      }
    ]);
  });
});
