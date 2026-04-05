import { describe, expect, it } from "vitest";

import { applyResultToStandings, createInitialStandings, createRoundRobinFixtures } from "../src/index.js";

describe("world season foundations", () => {
  it("creates home-and-away round robin fixtures for each pair", () => {
    const clubs = ["club-a", "club-b", "club-c", "club-d"];
    const fixtures = createRoundRobinFixtures(clubs);

    const expectedFixtureCount = clubs.length * (clubs.length - 1);
    expect(fixtures).toHaveLength(expectedFixtureCount);

    const fixturePairs = new Set(fixtures.map((fixture) => `${fixture.homeClubId}:${fixture.awayClubId}`));
    for (const home of clubs) {
      for (const away of clubs) {
        if (home === away) {
          continue;
        }
        expect(fixturePairs.has(`${home}:${away}`)).toBe(true);
      }
    }
  });

  it("updates points and ranking after a result", () => {
    const standings = createInitialStandings(["club-a", "club-b", "club-c"]);

    const updated = applyResultToStandings({
      standings,
      fixture: {
        id: "md-1-club-a-club-b",
        matchday: 1,
        homeClubId: "club-a",
        awayClubId: "club-b"
      },
      result: {
        homeGoals: 2,
        awayGoals: 0
      }
    });

    const top = updated[0];
    const bottom = updated[updated.length - 1];

    expect(top.clubId).toBe("club-a");
    expect(top.points).toBe(3);
    expect(bottom.clubId).toBe("club-b");
    expect(bottom.points).toBe(0);
  });
});
