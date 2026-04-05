import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  applyResultToStandings,
  createInitialStandings,
  createRoundRobinFixtures,
  createSeasonState,
  getFixturesForMatchday,
  isSeasonComplete
} from "../src/index.js";

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

  it("creates season state and advances by one matchday", () => {
    const state = createSeasonState([
      { id: "club-a", name: "Club A", strength: 72 },
      { id: "club-b", name: "Club B", strength: 70 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    expect(state.currentMatchday).toBe(1);
    expect(isSeasonComplete(state)).toBe(false);

    const matchdayFixtures = getFixturesForMatchday(state, 1);
    const resultsByFixtureId = Object.fromEntries(
      matchdayFixtures.map((fixture, index) => [
        fixture.id,
        {
          homeGoals: 1 + (index % 2),
          awayGoals: index % 2
        }
      ])
    );

    const next = advanceSeasonState(state, resultsByFixtureId);

    expect(next.currentMatchday).toBe(2);
    expect(next.completedFixtureIds.length).toBe(matchdayFixtures.length);
    expect(next.standings.every((row) => row.played <= 1)).toBe(true);
  });
});
