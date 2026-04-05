import { describe, expect, it } from "vitest";

import { deriveSackRiskPressureState, evaluateBoardExpectationContext } from "../src/index.js";

describe("board expectation context", () => {
  it("produces different outcomes for same position under different context", () => {
    const contextualLow = evaluateBoardExpectationContext({
      leaguePosition: 8,
      totalTeams: 20,
      preseasonObjectivePosition: 8,
      clubStature: 0.5,
      financialPressure: 0.8,
      recentPointsPerMatch: 0.9,
      styleAlignment: 0.3,
      derbyResult: "loss"
    });

    const contextualHigh = evaluateBoardExpectationContext({
      leaguePosition: 8,
      totalTeams: 20,
      preseasonObjectivePosition: 8,
      clubStature: 0.5,
      financialPressure: 0.2,
      recentPointsPerMatch: 1.9,
      styleAlignment: 0.75,
      derbyResult: "win"
    });

    expect(contextualHigh.boardDelta).toBeGreaterThan(contextualLow.boardDelta);
    expect(contextualHigh.sackRisk).toBeLessThan(contextualLow.sackRisk);
  });

  it("applies stronger sack pressure for high-stature clubs under same underperformance", () => {
    const elite = evaluateBoardExpectationContext({
      leaguePosition: 11,
      totalTeams: 20,
      preseasonObjectivePosition: 4,
      clubStature: 0.9,
      financialPressure: 0.5,
      recentPointsPerMatch: 1.2,
      styleAlignment: 0.52,
      derbyResult: "none"
    });

    const smaller = evaluateBoardExpectationContext({
      leaguePosition: 11,
      totalTeams: 20,
      preseasonObjectivePosition: 4,
      clubStature: 0.2,
      financialPressure: 0.5,
      recentPointsPerMatch: 1.2,
      styleAlignment: 0.52,
      derbyResult: "none"
    });

    expect(elite.sackRisk).toBeGreaterThan(smaller.sackRisk);
  });

  it("classifies sack-risk pressure level thresholds deterministically", () => {
    expect(deriveSackRiskPressureState(0.33).level).toBe("stable");
    expect(deriveSackRiskPressureState(0.6).level).toBe("warning");
    expect(deriveSackRiskPressureState(0.82).level).toBe("critical");
  });

  it("classifies sack-risk pressure trend using previous risk", () => {
    expect(deriveSackRiskPressureState(0.62, 0.42).trend).toBe("rising");
    expect(deriveSackRiskPressureState(0.41, 0.58).trend).toBe("falling");
    expect(deriveSackRiskPressureState(0.5, 0.47).trend).toBe("steady");
  });
});
