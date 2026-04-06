import { describe, expect, it } from "vitest";

import type { CountryPack } from "../src/shared/types.js";
import { validateCountryPack } from "../src/world/countryPack.js";

const basePack: CountryPack = {
  id: "country-test",
  name: "Test Country",
  divisions: [
    {
      id: "div-1",
      name: "Division 1",
      tier: 1,
      simulationDepth: "deep",
      clubs: [
        {
          id: "club-a",
          name: "Club A",
          shortName: "A",
          isPlayable: true
        }
      ]
    },
    {
      id: "div-2",
      name: "Division 2",
      tier: 2,
      simulationDepth: "deep",
      clubs: [
        {
          id: "club-b",
          name: "Club B",
          shortName: "B",
          isPlayable: false
        }
      ]
    }
  ]
};

describe("validateCountryPack", () => {
  it("accepts a pack with deep top two tiers and a playable club", () => {
    const result = validateCountryPack(basePack);
    expect(result.valid).toBe(true);
    expect(result.errors).toHaveLength(0);
  });

  it("rejects a pack when tier two is not deep simulated", () => {
    const invalidPack: CountryPack = {
      ...basePack,
      divisions: basePack.divisions.map((division) =>
        division.tier === 2
          ? { ...division, simulationDepth: "shadow" }
          : division
      )
    };

    const result = validateCountryPack(invalidPack);
    expect(result.valid).toBe(false);
    expect(result.errors.some((error) => error.code === "TOP_TWO_NOT_DEEP")).toBe(true);
  });

  it("rejects a pack when non-top-two divisions are not shadow simulated", () => {
    const invalidPack: CountryPack = {
      ...basePack,
      divisions: [
        ...basePack.divisions,
        {
          id: "div-3",
          name: "Division 3",
          tier: 3,
          simulationDepth: "deep",
          clubs: [
            {
              id: "club-c",
              name: "Club C",
              shortName: "C",
              isPlayable: false
            }
          ]
        }
      ]
    };

    const result = validateCountryPack(invalidPack);
    expect(result.valid).toBe(false);
    expect(result.errors.some((error) => error.code === "NON_TOP_TWO_NOT_SHADOW")).toBe(true);
  });

  it("rejects a pack with no playable clubs", () => {
    const invalidPack: CountryPack = {
      ...basePack,
      divisions: basePack.divisions.map((division) => ({
        ...division,
        clubs: division.clubs.map((club) => ({ ...club, isPlayable: false }))
      }))
    };

    const result = validateCountryPack(invalidPack);
    expect(result.valid).toBe(false);
    expect(result.errors.some((error) => error.code === "NO_PLAYABLE_CLUB")).toBe(true);
  });

  it("rejects duplicate club ids across divisions", () => {
    const invalidPack: CountryPack = {
      ...basePack,
      divisions: [
        ...basePack.divisions,
        {
          id: "div-3",
          name: "Division 3",
          tier: 3,
          simulationDepth: "shadow",
          clubs: [
            {
              id: "club-a",
              name: "Duplicate Club",
              shortName: "DUPE",
              isPlayable: false
            }
          ]
        }
      ]
    };

    const result = validateCountryPack(invalidPack);
    expect(result.valid).toBe(false);
    expect(result.errors.some((error) => error.code === "DUPLICATE_CLUB_ID")).toBe(true);
  });
});
