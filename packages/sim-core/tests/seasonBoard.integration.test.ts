import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  createSeasonState,
  deriveSackRiskPressureState,
  deriveSeasonBoardContextFromSeasonState,
  evaluateSeasonBoardContext,
  getFixturesForMatchday,
  isSeasonComplete
} from "../src/index.js";

function runSackRiskTimelineSimulation(): Array<{ risk: number; trend: string; level: string }> {
  let state = createSeasonState([
    { id: "club-a", name: "Club A", strength: 74 },
    { id: "club-b", name: "Club B", strength: 71 },
    { id: "club-c", name: "Club C", strength: 69 },
    { id: "club-d", name: "Club D", strength: 67 }
  ]);

  const staticContext = {
    "club-a": {
      preseasonObjectivePosition: 4,
      clubStature: 0.85,
      financialPressure: 0.55,
      styleAlignment: 0.56,
      derbyResult: "none" as const
    },
    "club-b": {
      preseasonObjectivePosition: 6,
      clubStature: 0.45,
      financialPressure: 0.32,
      styleAlignment: 0.62,
      derbyResult: "draw" as const
    },
    "club-c": {
      preseasonObjectivePosition: 8,
      clubStature: 0.3,
      financialPressure: 0.22,
      styleAlignment: 0.54,
      derbyResult: "win" as const
    },
    "club-d": {
      preseasonObjectivePosition: 10,
      clubStature: 0.2,
      financialPressure: 0.2,
      styleAlignment: 0.5,
      derbyResult: "loss" as const
    }
  };

  let rolling = 21;
  let previousRisk: number | undefined;
  const timeline: Array<{ risk: number; trend: string; level: string }> = [];

  while (!isSeasonComplete(state)) {
    const fixtures = getFixturesForMatchday(state, state.currentMatchday);
    const resultsByFixtureId = Object.fromEntries(
      fixtures.map((fixture) => {
        rolling += 1;
        return [
          fixture.id,
          {
            homeGoals: rolling % 4,
            awayGoals: (rolling + 1) % 4
          }
        ];
      })
    );

    state = advanceSeasonState(state, resultsByFixtureId);

    const contextByClubId = deriveSeasonBoardContextFromSeasonState(state, staticContext);
    const evaluationByClubId = evaluateSeasonBoardContext(state, contextByClubId);
    const risk = evaluationByClubId["club-a"].sackRisk;
    const pressure = deriveSackRiskPressureState(risk, previousRisk);

    timeline.push({
      risk,
      trend: pressure.trend,
      level: pressure.level
    });

    previousRisk = risk;
  }

  return timeline;
}

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
    expect(Object.values(board).every((evaluation) => evaluation.reasonSummary.length > 0)).toBe(true);
  });

  it("derives recent form context from season progression for board evaluation", () => {
    let state = createSeasonState([
      { id: "club-a", name: "Club A", strength: 74 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    let rolling = 11;
    while (!isSeasonComplete(state)) {
      const fixtures = getFixturesForMatchday(state, state.currentMatchday);
      const resultsByFixtureId = Object.fromEntries(
        fixtures.map((fixture) => {
          rolling += 2;
          return [
            fixture.id,
            {
              homeGoals: rolling % 4,
              awayGoals: (rolling + 1) % 4
            }
          ];
        })
      );

      state = advanceSeasonState(state, resultsByFixtureId);
    }

    const staticContext = {
      "club-a": {
        preseasonObjectivePosition: 4,
        clubStature: 0.8,
        financialPressure: 0.35,
        styleAlignment: 0.6,
        derbyResult: "none" as const
      },
      "club-b": {
        preseasonObjectivePosition: 6,
        clubStature: 0.45,
        financialPressure: 0.3,
        styleAlignment: 0.63,
        derbyResult: "draw" as const
      },
      "club-c": {
        preseasonObjectivePosition: 8,
        clubStature: 0.25,
        financialPressure: 0.2,
        styleAlignment: 0.55,
        derbyResult: "win" as const
      },
      "club-d": {
        preseasonObjectivePosition: 10,
        clubStature: 0.2,
        financialPressure: 0.25,
        styleAlignment: 0.5,
        derbyResult: "loss" as const
      }
    };

    const contextByClubId = deriveSeasonBoardContextFromSeasonState(state, staticContext);
    const board = evaluateSeasonBoardContext(state, contextByClubId);

    const rowForClubA = state.standings.find((row) => row.clubId === "club-a");
    if (!rowForClubA) {
      throw new Error("Expected club-a row in standings.");
    }

    expect(contextByClubId["club-a"].recentPointsPerMatch).toBeCloseTo(
      rowForClubA.points / rowForClubA.played,
      6
    );
    expect(board["club-a"].reasonSummary.length).toBeGreaterThan(1);
    expect(board["club-a"].sackRisk).toBeGreaterThanOrEqual(0);
    expect(board["club-a"].sackRisk).toBeLessThanOrEqual(1);
    expect(Object.values(board).every((evaluation) => evaluation.reasonSummary.length > 0)).toBe(true);
  });

  it("changes sack-risk ordering under matched table position when context shifts", () => {
    const state = createSeasonState([
      { id: "club-a", name: "Club A", strength: 74 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    const fixtures = getFixturesForMatchday(state, state.currentMatchday);
    const resultsByFixtureId = Object.fromEntries(
      fixtures.map((fixture) => [
        fixture.id,
        {
          homeGoals: 1,
          awayGoals: 1
        }
      ])
    );

    const progressedState = advanceSeasonState(state, resultsByFixtureId);

    const lowerContext = evaluateSeasonBoardContext(progressedState, {
      "club-a": {
        preseasonObjectivePosition: 4,
        clubStature: 0.5,
        financialPressure: 0.85,
        recentPointsPerMatch: 0.8,
        styleAlignment: 0.3,
        derbyResult: "loss"
      },
      "club-b": {
        preseasonObjectivePosition: 6,
        clubStature: 0.4,
        financialPressure: 0.3,
        recentPointsPerMatch: 1.4,
        styleAlignment: 0.65,
        derbyResult: "none"
      },
      "club-c": {
        preseasonObjectivePosition: 8,
        clubStature: 0.25,
        financialPressure: 0.2,
        recentPointsPerMatch: 1.2,
        styleAlignment: 0.56,
        derbyResult: "draw"
      },
      "club-d": {
        preseasonObjectivePosition: 10,
        clubStature: 0.2,
        financialPressure: 0.22,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.59,
        derbyResult: "win"
      }
    });

    const higherContext = evaluateSeasonBoardContext(progressedState, {
      "club-a": {
        preseasonObjectivePosition: 4,
        clubStature: 0.5,
        financialPressure: 0.15,
        recentPointsPerMatch: 2,
        styleAlignment: 0.88,
        derbyResult: "win"
      },
      "club-b": {
        preseasonObjectivePosition: 6,
        clubStature: 0.4,
        financialPressure: 0.3,
        recentPointsPerMatch: 1.4,
        styleAlignment: 0.65,
        derbyResult: "none"
      },
      "club-c": {
        preseasonObjectivePosition: 8,
        clubStature: 0.25,
        financialPressure: 0.2,
        recentPointsPerMatch: 1.2,
        styleAlignment: 0.56,
        derbyResult: "draw"
      },
      "club-d": {
        preseasonObjectivePosition: 10,
        clubStature: 0.2,
        financialPressure: 0.22,
        recentPointsPerMatch: 1.3,
        styleAlignment: 0.59,
        derbyResult: "win"
      }
    });

    expect(lowerContext["club-a"].sackRisk).toBeGreaterThan(higherContext["club-a"].sackRisk);
  });

  it("keeps multi-matchday sack-risk progression deterministic", () => {
    const firstRun = runSackRiskTimelineSimulation();
    const secondRun = runSackRiskTimelineSimulation();

    expect(firstRun).toEqual(secondRun);
    expect(firstRun.length).toBeGreaterThan(0);
    expect(firstRun.every((entry) => entry.risk >= 0 && entry.risk <= 1)).toBe(true);
    expect(firstRun.every((entry) => entry.level.length > 0 && entry.trend.length > 0)).toBe(true);
  });
});
