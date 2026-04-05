import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  createSeasonState,
  evaluateSeasonBoardContext,
  getFixturesForMatchday,
  isSeasonComplete
} from "../src/index.js";

describe("season board integration", () => {
  it("evaluates contextual board outcomes from season standings", () => {
    let state = createSeasonState([
      { id: "club-a", name: "Club A", strength: 74 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    let rolling = 30;
    while (!isSeasonComplete(state)) {
      const fixtures = getFixturesForMatchday(state, state.currentMatchday);
      const resultsByFixtureId = Object.fromEntries(
        fixtures.map((fixture) => {
          rolling += 1;
          return [
            fixture.id,
            {
              homeGoals: rolling % 3,
              awayGoals: (rolling + 2) % 3
            }
          ];
        })
      );

      state = advanceSeasonState(state, resultsByFixtureId);
    }

    const board = evaluateSeasonBoardContext(state, {
      "club-a": {
        preseasonObjectivePosition: 3,
        clubStature: 0.8,
        financialPressure: 0.45,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.62,
        derbyResult: "none"
      },
      "club-b": {
        preseasonObjectivePosition: 6,
        clubStature: 0.45,
        financialPressure: 0.35,
        recentPointsPerMatch: 1.5,
        styleAlignment: 0.66,
        derbyResult: "win"
      },
      "club-c": {
        preseasonObjectivePosition: 10,
        clubStature: 0.25,
        financialPressure: 0.2,
        recentPointsPerMatch: 1.4,
        styleAlignment: 0.58,
        derbyResult: "draw"
      },
      "club-d": {
        preseasonObjectivePosition: 12,
        clubStature: 0.2,
        financialPressure: 0.25,
        recentPointsPerMatch: 1.2,
        styleAlignment: 0.53,
        derbyResult: "loss"
      }
    });

    expect(Object.keys(board)).toHaveLength(4);
    expect(board["club-a"].reasonSummary.length).toBeGreaterThan(1);
    expect(board["club-a"].sackRisk).toBeGreaterThanOrEqual(0);
    expect(board["club-a"].sackRisk).toBeLessThanOrEqual(1);
  });
});
