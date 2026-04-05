import { describe, expect, it } from "vitest";

import { applyMatchPreparationCommand } from "../src/index.js";
import type { MatchPreparationState, TacticalSetup } from "../src/index.js";
import { createTeam } from "./fixtures/matchFixtures.js";

const baselineTactics: TacticalSetup = {
  blockHeight: 0.5,
  pressingIntensity: 0.5,
  width: 0.5,
  tempo: 0.5,
  risk: 0.5
};

function createState(): MatchPreparationState {
  return {
    team: createTeam("Harbor FC", 74, baselineTactics, 0.62)
  };
}

describe("applyMatchPreparationCommand", () => {
  it("applies valid tactics updates", () => {
    const state = createState();
    const result = applyMatchPreparationCommand(state, {
      type: "set-tactics",
      tactics: {
        blockHeight: 0.65,
        pressingIntensity: 0.72,
        width: 0.43,
        tempo: 0.58,
        risk: 0.54
      }
    });

    expect(result.applied).toBe(true);
    expect(result.state.team.tactics.pressingIntensity).toBe(0.72);
  });

  it("rejects out-of-range tactical values with explicit reason", () => {
    const state = createState();
    const result = applyMatchPreparationCommand(state, {
      type: "set-tactics",
      tactics: {
        blockHeight: 0.65,
        pressingIntensity: 1.2,
        width: 0.43,
        tempo: 0.58,
        risk: 0.54
      }
    });

    expect(result.applied).toBe(false);
    expect(result.reason).toContain("pressingIntensity");
  });

  it("rejects lineup updates with duplicate player ids", () => {
    const state = createState();
    const duplicated = Array.from({ length: 11 }, () => state.team.lineup[0].id);
    const result = applyMatchPreparationCommand(state, {
      type: "set-lineup",
      lineupPlayerIds: duplicated
    });

    expect(result.applied).toBe(false);
    expect(result.reason).toContain("duplicate");
  });

  it("accepts valid substitution plans from lineup and bench", () => {
    const state = createState();
    const playerOutId = state.team.lineup[10].id;
    const playerIn = state.team.bench[0];

    const result = applyMatchPreparationCommand(state, {
      type: "plan-substitution",
      substitution: {
        minute: 70,
        playerOutId,
        playerIn
      }
    });

    expect(result.applied).toBe(true);
    expect(result.state.team.substitutions).toHaveLength(state.team.substitutions.length + 1);
  });
});
