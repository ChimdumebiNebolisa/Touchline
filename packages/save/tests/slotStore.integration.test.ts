import { mkdtemp, rm } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";

import { describe, expect, it } from "vitest";

import { advanceSeasonState, createSeasonState, getFixturesForMatchday } from "@touchline/sim-core";

import { readSaveSlot, writeSaveSlot, type SaveGameStateV1 } from "../src/index.js";

function createSaveState(worldState: SaveGameStateV1["worldState"]): SaveGameStateV1 {
  return {
    worldState,
    clubPerceptionState: {
      boardConfidence: 55,
      fanSentiment: 56,
      teamMorale: 58,
      managerReputation: 60
    },
    managerCareer: {
      managerId: "manager-slot-integration",
      currentClubId: "club-a",
      reputationHistory: [51, 56, 60],
      careerLeverageHistory: [
        {
          score: 0.61,
          band: "credible",
          reasonSummary: "Reputation trend stayed positive through recent fixtures."
        }
      ],
      sackHistory: [
        {
          clubId: "legacy-club",
          leaguePosition: 17,
          reasonSummary: ["Historical dismissal due to prolonged warning pressure."],
          sackRisk: 0.78
        }
      ]
    }
  };
}

describe("save slot store integration", () => {
  it("keeps continuation deterministic when loading from slot before a major event", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slot-integration-"));

    try {
      const preEventState = createSeasonState([
        { id: "club-a", name: "Club A", strength: 74 },
        { id: "club-b", name: "Club B", strength: 71 },
        { id: "club-c", name: "Club C", strength: 69 },
        { id: "club-d", name: "Club D", strength: 67 }
      ]);

      await writeSaveSlot({
        rootDirectory,
        slotId: "major-event",
        state: createSaveState(preEventState),
        savedAtIso: "2035-01-10T12:00:00.000Z"
      });

      const reloadedEnvelope = await readSaveSlot({ rootDirectory, slotId: "major-event" });

      const fixtureResults = Object.fromEntries(
        getFixturesForMatchday(preEventState, preEventState.currentMatchday).map((fixture, index) => [
          fixture.id,
          {
            homeGoals: (index + 1) % 3,
            awayGoals: (index + 2) % 3
          }
        ])
      );

      const nextFromOriginal = advanceSeasonState(preEventState, fixtureResults);
      const nextFromReloaded = advanceSeasonState(reloadedEnvelope.state.worldState, fixtureResults);

      expect(nextFromReloaded).toEqual(nextFromOriginal);
      expect(reloadedEnvelope.state.managerCareer.reputationHistory).toEqual([51, 56, 60]);
      expect(reloadedEnvelope.state.managerCareer.sackHistory).toHaveLength(1);
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });
});
