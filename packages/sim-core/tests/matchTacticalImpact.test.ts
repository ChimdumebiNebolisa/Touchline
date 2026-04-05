import { describe, expect, it } from "vitest";

import { runInstantMatch } from "../src/index.js";
import type { TacticalSetup } from "../src/match/types.js";
import { createMatchInput } from "./fixtures/matchFixtures.js";

const conservative: TacticalSetup = {
  blockHeight: 0.62,
  pressingIntensity: 0.25,
  width: 0.4,
  tempo: 0.4,
  risk: 0.28
};

const aggressive: TacticalSetup = {
  blockHeight: 0.48,
  pressingIntensity: 0.9,
  width: 0.68,
  tempo: 0.86,
  risk: 0.82
};

describe("shared match engine tactical impact", () => {
  it("high press and tempo produce higher fatigue over a seeded sample", () => {
    let conservativeFatigueTotal = 0;
    let aggressiveFatigueTotal = 0;

    for (let seed = 1; seed <= 18; seed += 1) {
      const conservativeRun = runInstantMatch(createMatchInput(seed, conservative, conservative));
      const aggressiveRun = runInstantMatch(createMatchInput(seed, aggressive, aggressive));
      conservativeFatigueTotal += conservativeRun.stats.home.averageFatigue;
      aggressiveFatigueTotal += aggressiveRun.stats.home.averageFatigue;
    }

    expect(aggressiveFatigueTotal).toBeGreaterThan(conservativeFatigueTotal);
  });

  it("aggressive tactics alter chance generation profile", () => {
    const conservativeRun = runInstantMatch(createMatchInput(144, conservative, conservative));
    const aggressiveRun = runInstantMatch(createMatchInput(144, aggressive, aggressive));

    expect(aggressiveRun.stats.home.shots + aggressiveRun.stats.away.shots).not.toBe(
      conservativeRun.stats.home.shots + conservativeRun.stats.away.shots
    );
  });
});
