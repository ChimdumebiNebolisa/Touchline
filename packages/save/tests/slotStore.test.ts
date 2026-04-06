import { mkdtemp, rm } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";

import { describe, expect, it } from "vitest";

import { createSeasonState } from "@touchline/sim-core";

import {
  deleteSaveSlot,
  listSaveSlots,
  readSaveSlot,
  SaveLoadError,
  type SaveGameStateV1,
  writeSaveSlot
} from "../src/index.js";

function createSampleState(): SaveGameStateV1 {
  return {
    worldState: createSeasonState([
      { id: "club-a", name: "Club A", strength: 74 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]),
    clubPerceptionState: {
      boardConfidence: 57,
      fanSentiment: 59,
      teamMorale: 60,
      managerReputation: 61
    },
    managerCareer: {
      managerId: "manager-slot-store",
      currentClubId: "club-a",
      recentPromiseBreak: false,
      reputationHistory: [54, 58, 61],
      careerLeverageHistory: [
        {
          score: 0.64,
          band: "credible",
          reasonSummary: "Board confidence remained stable over recent matches."
        }
      ],
      sackHistory: [
        {
          clubId: "legacy-club",
          leaguePosition: 18,
          reasonSummary: ["Sustained critical pressure triggered dismissal."],
          sackRisk: 0.81
        }
      ]
    }
  };
}

describe("save slot store", () => {
  it("writes, reads, lists, and deletes save slots deterministically", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slots-"));

    try {
      const state = createSampleState();
      await writeSaveSlot({
        rootDirectory,
        slotId: "slot-alpha",
        state,
        savedAtIso: "2034-03-01T12:00:00.000Z"
      });

      await writeSaveSlot({
        rootDirectory,
        slotId: "slot-beta",
        state,
        savedAtIso: "2034-03-02T12:00:00.000Z"
      });

      const restored = await readSaveSlot({ rootDirectory, slotId: "slot-alpha" });
      const listed = await listSaveSlots(rootDirectory);

      expect(restored.state).toEqual(state);
      expect(listed.map((entry) => entry.slotId)).toEqual(["slot-beta", "slot-alpha"]);

      await deleteSaveSlot({ rootDirectory, slotId: "slot-alpha" });
      const listedAfterDelete = await listSaveSlots(rootDirectory);
      expect(listedAfterDelete.map((entry) => entry.slotId)).toEqual(["slot-beta"]);
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });

  it("raises explicit slot-not-found errors for missing slots", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slots-"));

    try {
      await expect(readSaveSlot({ rootDirectory, slotId: "slot-missing" })).rejects.toMatchObject({
        code: "SLOT_NOT_FOUND"
      });
      await expect(deleteSaveSlot({ rootDirectory, slotId: "slot-missing" })).rejects.toMatchObject({
        code: "SLOT_NOT_FOUND"
      });
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });

  it("rejects malformed slot ids with explicit errors", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slots-"));

    try {
      await expect(
        writeSaveSlot({
          rootDirectory,
          slotId: "INVALID SLOT",
          state: createSampleState(),
          savedAtIso: "2034-03-01T12:00:00.000Z"
        })
      ).rejects.toBeInstanceOf(SaveLoadError);

      await expect(
        writeSaveSlot({
          rootDirectory,
          slotId: "INVALID SLOT",
          state: createSampleState(),
          savedAtIso: "2034-03-01T12:00:00.000Z"
        })
      ).rejects.toMatchObject({ code: "MALFORMED_PAYLOAD" });
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });
});
