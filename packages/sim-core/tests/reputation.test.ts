import { describe, expect, it } from "vitest";

import { deriveManagerCareerLeverageSnapshot } from "../src/index.js";

describe("manager career leverage", () => {
  it("derives deterministic leverage snapshots for identical inputs", () => {
    const firstRun = deriveManagerCareerLeverageSnapshot({
      managerReputation: 64,
      boardConfidence: 58,
      fanSentiment: 61
    });
    const secondRun = deriveManagerCareerLeverageSnapshot({
      managerReputation: 64,
      boardConfidence: 58,
      fanSentiment: 61
    });

    expect(secondRun).toEqual(firstRun);
    expect(firstRun.reasonSummary).toContain("Career leverage is");
  });

  it("maps leverage score bands at deterministic threshold boundaries", () => {
    const fragile = deriveManagerCareerLeverageSnapshot({
      managerReputation: 30,
      boardConfidence: 30,
      fanSentiment: 30
    });
    const credible = deriveManagerCareerLeverageSnapshot({
      managerReputation: 45,
      boardConfidence: 45,
      fanSentiment: 45
    });
    const inDemand = deriveManagerCareerLeverageSnapshot({
      managerReputation: 62,
      boardConfidence: 62,
      fanSentiment: 62
    });
    const elite = deriveManagerCareerLeverageSnapshot({
      managerReputation: 80,
      boardConfidence: 80,
      fanSentiment: 80
    });

    expect(fragile.score).toBeCloseTo(30, 10);
    expect(fragile.band).toBe("fragile");
    expect(credible.score).toBeCloseTo(45, 10);
    expect(credible.band).toBe("credible");
    expect(inDemand.score).toBeCloseTo(62, 10);
    expect(inDemand.band).toBe("in-demand");
    expect(elite.score).toBeCloseTo(80, 10);
    expect(elite.band).toBe("elite");
  });

  it("increases career leverage with stronger reputation and patience context", () => {
    const lowLeverage = deriveManagerCareerLeverageSnapshot({
      managerReputation: 32,
      boardConfidence: 38,
      fanSentiment: 36
    });
    const highLeverage = deriveManagerCareerLeverageSnapshot({
      managerReputation: 84,
      boardConfidence: 72,
      fanSentiment: 70
    });

    expect(highLeverage.score).toBeGreaterThan(lowLeverage.score);
    expect(["fragile", "credible", "in-demand", "elite"].indexOf(highLeverage.band)).toBeGreaterThan(
      ["fragile", "credible", "in-demand", "elite"].indexOf(lowLeverage.band)
    );
  });
});