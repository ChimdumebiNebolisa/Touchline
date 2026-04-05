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

    const topClubId = state.standings[0]?.clubId;
    const bottomClubId = state.standings[state.standings.length - 1]?.clubId;

    if (!topClubId || !bottomClubId) {
      throw new Error("Expected completed season standings to include clubs.");
    }

    const board = evaluateSeasonBoardContext(state, {
      "club-a": {
        preseasonObjectivePosition: 2,
        clubStature: 0.5,
        financialPressure: 0.4,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.62,
        derbyResult: "none"
      },
      "club-b": {
        preseasonObjectivePosition: 2,
        clubStature: 0.5,
        financialPressure: 0.4,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.62,
        derbyResult: "none"
      },
      "club-c": {
        preseasonObjectivePosition: 2,
        clubStature: 0.5,
        financialPressure: 0.4,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.62,
        derbyResult: "none"
      },
      "club-d": {
        preseasonObjectivePosition: 2,
        clubStature: 0.5,
        financialPressure: 0.4,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.62,
        derbyResult: "none"
      },
      [topClubId]: {
        preseasonObjectivePosition: 1,
        clubStature: 0.95,
        financialPressure: 1,
        recentPointsPerMatch: 0.3,
        styleAlignment: 0.2,
        derbyResult: "loss"
      },
      [bottomClubId]: {
        preseasonObjectivePosition: state.standings.length,
        clubStature: 0.1,
        financialPressure: 0.05,
        recentPointsPerMatch: 2.2,
        styleAlignment: 0.9,
        derbyResult: "win"
      }
    });

    expect(Object.keys(board)).toHaveLength(4);
    expect(board["club-a"].reasonSummary.length).toBeGreaterThan(1);
    expect(board["club-a"].sackRisk).toBeGreaterThanOrEqual(0);
    expect(board["club-a"].sackRisk).toBeLessThanOrEqual(1);
    expect(board[topClubId].sackRisk).toBeGreaterThan(board[bottomClubId].sackRisk);
  });
});
