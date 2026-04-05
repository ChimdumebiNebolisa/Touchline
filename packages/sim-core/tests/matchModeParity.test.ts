import { describe, expect, it } from "vitest";

import { runInstantMatch, runLiveMatch } from "../src/index.js";
import type { TacticalSetup } from "../src/match/types.js";
import { createMatchInput } from "./fixtures/matchFixtures.js";

const balancedTactics: TacticalSetup = {
  blockHeight: 0.55,
  pressingIntensity: 0.58,
  width: 0.52,
  tempo: 0.56,
  risk: 0.5
};

describe("shared match engine parity", () => {
  it("produces matching results for instant and live from the same seed", () => {
    const input = createMatchInput(20260405, balancedTactics, balancedTactics);

    const instantOutcome = runInstantMatch(input);
    const liveOutcome = runLiveMatch(input);

    expect(liveOutcome.result).toEqual(instantOutcome.result);
    expect(liveOutcome.stats).toEqual(instantOutcome.stats);
    expect(liveOutcome.eventLog).toEqual(instantOutcome.eventLog);
    expect(liveOutcome.timeline).toHaveLength(instantOutcome.eventLog.length);
  });

  it("is deterministic for repeated instant runs with the same seed", () => {
    const input = createMatchInput(777, balancedTactics, balancedTactics);
    const firstRun = runInstantMatch(input);
    const secondRun = runInstantMatch(input);

    expect(secondRun).toEqual(firstRun);
  });
});
