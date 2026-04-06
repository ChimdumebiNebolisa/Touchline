import { readFileSync } from "node:fs";
import { resolve } from "node:path";

import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  buildShadowLeagueContextSnapshot,
  createSeasonState,
  getFixturesForMatchday,
  isSeasonComplete,
  parseCountryPackJson,
  summarizeCompletedSeason
} from "../src/index.js";

function loadFirstCountryPack() {
  const jsonPath = resolve(import.meta.dirname, "..", "content", "countries", "first-country.json");
  const rawJson = readFileSync(jsonPath, "utf-8");
  return parseCountryPackJson(rawJson);
}

describe("shadow world integration", () => {
  it("combines completed season outcomes with shadow transfer-loan context", () => {
    const pack = loadFirstCountryPack();
    const topDivision = pack.divisions.find((division) => division.tier === 1);

    if (!topDivision) {
      throw new Error("Expected first-country pack to include a top division.");
    }

    let state = createSeasonState(
      topDivision.clubs.map((club, index) => ({
        id: club.id,
        name: club.name,
        strength: 72 - index
      }))
    );

    let rolling = 5;
    while (!isSeasonComplete(state)) {
      const fixtures = getFixturesForMatchday(state, state.currentMatchday);
      const resultsByFixtureId = Object.fromEntries(
        fixtures.map((fixture) => {
          rolling += 1;
          return [
            fixture.id,
            {
              homeGoals: rolling % 3,
              awayGoals: (rolling + 1) % 3
            }
          ];
        })
      );

      state = advanceSeasonState(state, resultsByFixtureId);
    }

    const completedSeason = summarizeCompletedSeason(state, 1, 1);
    const shadowContext = buildShadowLeagueContextSnapshot(pack);

    const integrationArtifact = {
      countryId: pack.id,
      completedSeasonTopClubId: completedSeason.finalStandings[0]?.clubId,
      completedSeasonBottomClubId:
        completedSeason.finalStandings[completedSeason.finalStandings.length - 1]?.clubId,
      promotedClubIds: completedSeason.promotionRelegation.promotedClubIds,
      relegatedClubIds: completedSeason.promotionRelegation.relegatedClubIds,
      shadowTransferSupplyCount: shadowContext.transferSupplyClubIds.length,
      shadowLoanDestinationCount: shadowContext.loanDestinationClubIds.length,
      shadowContinuityLinkCount: shadowContext.continuityLinks.length,
      shadowReasonSummary: shadowContext.reasonSummary
    };

    expect(integrationArtifact.promotedClubIds).toHaveLength(1);
    expect(integrationArtifact.relegatedClubIds).toHaveLength(1);
    expect(integrationArtifact.promotedClubIds[0]).not.toBe(integrationArtifact.relegatedClubIds[0]);

    expect(integrationArtifact.shadowTransferSupplyCount).toBeGreaterThan(0);
    expect(integrationArtifact.shadowLoanDestinationCount).toBeGreaterThan(0);
    expect(integrationArtifact.shadowContinuityLinkCount).toBeGreaterThan(0);

    expect(integrationArtifact).toMatchInlineSnapshot(`
      {
        "completedSeasonBottomClubId": "nvr-old-town-athletic",
        "completedSeasonTopClubId": "nvr-harbor-fc",
        "countryId": "country-novara",
        "promotedClubIds": [
          "nvr-harbor-fc",
        ],
        "relegatedClubIds": [
          "nvr-old-town-athletic",
        ],
        "shadowContinuityLinkCount": 2,
        "shadowLoanDestinationCount": 2,
        "shadowReasonSummary": [
          "Deep divisions: 2; shadow divisions: 1.",
          "Shadow leagues contribute 2 transfer targets and 2 loan destinations.",
          "Promotion-relegation continuity links across tiers: 2.",
        ],
        "shadowTransferSupplyCount": 2,
      }
    `);
  });
});
