import { describe, expect, it } from "vitest";

import { advanceSeasonState, createSeasonState, getFixturesForMatchday, isSeasonComplete } from "../src/index.js";

describe("season integration", () => {
  it("completes a full mini-season loop and produces final standings", () => {
    let state = createSeasonState([
      { id: "club-a", name: "Club A", strength: 73 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    let seed = 13;
    while (!isSeasonComplete(state)) {
      const fixtures = getFixturesForMatchday(state, state.currentMatchday);
      if (!fixtures.length) {
        break;
      }

      const resultsByFixtureId = Object.fromEntries(
        fixtures.map((fixture) => {
          seed += 1;
          const homeGoals = seed % 3;
          const awayGoals = (seed + 1) % 3;
          return [fixture.id, { homeGoals, awayGoals }];
        })
      );

      state = advanceSeasonState(state, resultsByFixtureId);
    }

    expect(isSeasonComplete(state)).toBe(true);
    expect(state.completedFixtureIds.length).toBe(state.fixtures.length);
    expect(state.standings.every((row) => row.played === 6)).toBe(true);
  });
});
