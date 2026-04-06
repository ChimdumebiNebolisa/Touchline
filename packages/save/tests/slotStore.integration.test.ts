import { mkdtemp, rm } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";

import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  createSeasonState,
  deriveManagerCareerLeverageSnapshot,
  getFixturesForMatchday
} from "@touchline/sim-core";

import { readSaveSlot, writeSaveSlot, type SaveGameStateV1 } from "../src/index.js";

function bandRank(band: "fragile" | "credible" | "in-demand" | "elite"): number {
  switch (band) {
    case "fragile":
      return 0;
    case "credible":
      return 1;
    case "in-demand":
      return 2;
    case "elite":
      return 3;
  }
}

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
      recentPromiseBreak: false,
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
      expect(reloadedEnvelope.state.managerCareer.recentPromiseBreak).toBe(false);
      expect(reloadedEnvelope.state.managerCareer.sackHistory).toHaveLength(1);
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });

  it("preserves latest long-save timeline state when a slot is overwritten", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slot-timeline-"));

    try {
      const firstSeasonState = createSeasonState([
        { id: "club-a", name: "Club A", strength: 74 },
        { id: "club-b", name: "Club B", strength: 71 },
        { id: "club-c", name: "Club C", strength: 69 },
        { id: "club-d", name: "Club D", strength: 67 }
      ]);

      await writeSaveSlot({
        rootDirectory,
        slotId: "career-timeline",
        state: createSaveState(firstSeasonState),
        savedAtIso: "2035-06-01T08:00:00.000Z"
      });

      const secondSeasonState = advanceSeasonState(
        firstSeasonState,
        Object.fromEntries(
          getFixturesForMatchday(firstSeasonState, firstSeasonState.currentMatchday).map((fixture, index) => [
            fixture.id,
            {
              homeGoals: (index + 2) % 3,
              awayGoals: (index + 1) % 3
            }
          ])
        )
      );

      const secondState = createSaveState(secondSeasonState);
      secondState.managerCareer.reputationHistory = [51, 56, 60, 63];
      secondState.managerCareer.careerLeverageHistory = [
        ...secondState.managerCareer.careerLeverageHistory,
        {
          score: 0.7,
          band: "in-demand",
          reasonSummary: "Second-season results increased board confidence and transfer pull."
        }
      ];

      await writeSaveSlot({
        rootDirectory,
        slotId: "career-timeline",
        state: secondState,
        savedAtIso: "2036-06-01T08:00:00.000Z"
      });

      const restored = await readSaveSlot({ rootDirectory, slotId: "career-timeline" });

      expect(restored.savedAtIso).toBe("2036-06-01T08:00:00.000Z");
      expect(restored.state.managerCareer.reputationHistory).toEqual([51, 56, 60, 63]);
      expect(restored.state.managerCareer.careerLeverageHistory).toHaveLength(2);
      expect(restored.state.managerCareer.careerLeverageHistory[1].band).toBe("in-demand");
      expect(restored.state.worldState.currentMatchday).toBe(secondSeasonState.currentMatchday);
      expect(restored.state.worldState.completedFixtureIds).toEqual(secondSeasonState.completedFixtureIds);
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });

  it("preserves reputation and sack history that drives future career leverage after reload", async () => {
    const rootDirectory = await mkdtemp(join(tmpdir(), "touchline-save-slot-career-"));

    try {
      const seasonState = createSeasonState([
        { id: "club-a", name: "Club A", strength: 74 },
        { id: "club-b", name: "Club B", strength: 71 },
        { id: "club-c", name: "Club C", strength: 69 },
        { id: "club-d", name: "Club D", strength: 67 }
      ]);

      const saveState = createSaveState(seasonState);
      saveState.managerCareer.recentPromiseBreak = true;
      saveState.managerCareer.reputationHistory = [48, 53, 59, 66];

      await writeSaveSlot({
        rootDirectory,
        slotId: "career-continuity",
        state: saveState,
        savedAtIso: "2037-08-15T10:00:00.000Z"
      });

      const restored = await readSaveSlot({ rootDirectory, slotId: "career-continuity" });

      const earliestLeverage = deriveManagerCareerLeverageSnapshot({
        managerReputation: restored.state.managerCareer.reputationHistory[0],
        boardConfidence: restored.state.clubPerceptionState.boardConfidence,
        fanSentiment: restored.state.clubPerceptionState.fanSentiment
      });
      const latestLeverage = deriveManagerCareerLeverageSnapshot({
        managerReputation:
          restored.state.managerCareer.reputationHistory[
            restored.state.managerCareer.reputationHistory.length - 1
          ],
        boardConfidence: restored.state.clubPerceptionState.boardConfidence,
        fanSentiment: restored.state.clubPerceptionState.fanSentiment
      });

      expect(restored.state.managerCareer.reputationHistory).toEqual([48, 53, 59, 66]);
      expect(restored.state.managerCareer.recentPromiseBreak).toBe(true);
      expect(restored.state.managerCareer.sackHistory).toEqual(saveState.managerCareer.sackHistory);
      expect(latestLeverage.score).toBeGreaterThan(earliestLeverage.score);
      expect(bandRank(latestLeverage.band)).toBeGreaterThanOrEqual(bandRank(earliestLeverage.band));
    } finally {
      await rm(rootDirectory, { recursive: true, force: true });
    }
  });
});
