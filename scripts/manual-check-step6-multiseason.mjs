import { mkdtemp, rm } from "node:fs/promises";
import { tmpdir } from "node:os";
import { join } from "node:path";

import {
  advanceSeasonState,
  createSeasonState,
  deriveManagerCareerLeverageSnapshot,
  getFixturesForMatchday,
  isSeasonComplete
} from "../packages/sim-core/dist/src/index.js";
import { listSaveSlots, readSaveSlot, writeSaveSlot } from "../packages/save/dist/src/index.js";

function buildFixtureResults(state, seedOffset) {
  return Object.fromEntries(
    getFixturesForMatchday(state, state.currentMatchday).map((fixture, index) => [
      fixture.id,
      {
        homeGoals: (seedOffset + index + 1) % 3,
        awayGoals: (seedOffset + index + 2) % 3
      }
    ])
  );
}

function buildSaveState(worldState, seasonIndex) {
  return {
    worldState,
    clubPerceptionState: {
      boardConfidence: 56 + seasonIndex,
      fanSentiment: 57 + seasonIndex,
      teamMorale: 58 + seasonIndex,
      managerReputation: 59 + seasonIndex
    },
    managerCareer: {
      managerId: "manager-step6-multi",
      currentClubId: "club-a",
      reputationHistory: [54, 58, 61, 63 + seasonIndex],
      careerLeverageHistory: [
        {
          score: 0.62,
          band: "credible",
          reasonSummary: "Board confidence remained above review baseline in season one."
        },
        {
          score: 0.71,
          band: "in-demand",
          reasonSummary: "Multi-season results improved manager leverage in hiring market."
        }
      ],
      sackHistory: [
        {
          clubId: "legacy-club",
          leaguePosition: 18,
          reasonSummary: ["Historical dismissal from legacy club during rebuild period."],
          sackRisk: 0.81
        }
      ]
    }
  };
}

function bandRank(band) {
  switch (band) {
    case "fragile":
      return 0;
    case "credible":
      return 1;
    case "in-demand":
      return 2;
    case "elite":
      return 3;
    default:
      throw new Error(`Unknown career leverage band '${band}'.`);
  }
}

async function main() {
  const slotRoot = await mkdtemp(join(tmpdir(), "touchline-step6-multi-"));

  try {
    let worldState = createSeasonState([
      { id: "club-a", name: "Club A", strength: 74 },
      { id: "club-b", name: "Club B", strength: 71 },
      { id: "club-c", name: "Club C", strength: 69 },
      { id: "club-d", name: "Club D", strength: 67 }
    ]);

    const seasonOneState = buildSaveState(worldState, 1);
    await writeSaveSlot({
      rootDirectory: slotRoot,
      slotId: "career-main",
      state: seasonOneState,
      savedAtIso: "2037-06-01T09:00:00.000Z"
    });

    worldState = advanceSeasonState(worldState, buildFixtureResults(worldState, 2));
    const seasonTwoState = buildSaveState(worldState, 2);
    seasonTwoState.managerCareer.reputationHistory.push(66);

    await writeSaveSlot({
      rootDirectory: slotRoot,
      slotId: "career-main",
      state: seasonTwoState,
      savedAtIso: "2038-06-01T09:00:00.000Z"
    });

    const restored = await readSaveSlot({ rootDirectory: slotRoot, slotId: "career-main" });
    const slotMetadata = await listSaveSlots(slotRoot);

    const fixtureResults = buildFixtureResults(worldState, 5);
    const continuedFromMemory = advanceSeasonState(worldState, fixtureResults);
    const continuedFromReload = advanceSeasonState(restored.state.worldState, fixtureResults);

    const reputationHistory = restored.state.managerCareer.reputationHistory;
    const earlySeasonLeverage = deriveManagerCareerLeverageSnapshot({
      managerReputation: reputationHistory[0],
      boardConfidence: restored.state.clubPerceptionState.boardConfidence,
      fanSentiment: restored.state.clubPerceptionState.fanSentiment
    });
    const latestSeasonLeverage = deriveManagerCareerLeverageSnapshot({
      managerReputation: reputationHistory[reputationHistory.length - 1],
      boardConfidence: restored.state.clubPerceptionState.boardConfidence,
      fanSentiment: restored.state.clubPerceptionState.fanSentiment
    });

    console.log("Step 6 Multi-Season Manual Check");
    console.log("- Save slot metadata");
    console.table(
      slotMetadata.map((slot) => ({
        slotId: slot.slotId,
        savedAtIso: slot.savedAtIso,
        version: slot.version
      }))
    );

    console.log("- Restored career-state summary");
    console.table([
      {
        currentMatchday: restored.state.worldState.currentMatchday,
        completedFixtureCount: restored.state.worldState.completedFixtureIds.length,
        seasonComplete: isSeasonComplete(restored.state.worldState),
        reputationHistoryCount: restored.state.managerCareer.reputationHistory.length,
        leverageHistoryCount: restored.state.managerCareer.careerLeverageHistory.length,
        sackHistoryCount: restored.state.managerCareer.sackHistory.length
      }
    ]);

    console.log("- Post-reload continuation summary");
    console.table([
      {
        memoryNextMatchday: continuedFromMemory.currentMatchday,
        reloadNextMatchday: continuedFromReload.currentMatchday,
        memoryCompletedFixtures: continuedFromMemory.completedFixtureIds.length,
        reloadCompletedFixtures: continuedFromReload.completedFixtureIds.length
      }
    ]);

    console.log("- Reputation-history leverage summary");
    console.table([
      {
        earliestReputation: reputationHistory[0],
        latestReputation: reputationHistory[reputationHistory.length - 1],
        earliestBand: earlySeasonLeverage.band,
        latestBand: latestSeasonLeverage.band,
        earliestScore: earlySeasonLeverage.score,
        latestScore: latestSeasonLeverage.score
      }
    ]);

    const checks = {
      slotUpdatedToLatestSeason: slotMetadata.length === 1 && slotMetadata[0].savedAtIso === "2038-06-01T09:00:00.000Z",
      restoredCareerHistoryPersists:
        restored.state.managerCareer.reputationHistory.length === 5 &&
        restored.state.managerCareer.careerLeverageHistory.length === 2 &&
        restored.state.managerCareer.sackHistory.length === 1,
      deterministicContinuationAfterMultiSeasonReload:
        JSON.stringify(continuedFromReload) === JSON.stringify(continuedFromMemory),
      reputationHistorySupportsHigherFutureLeverage:
        latestSeasonLeverage.score > earlySeasonLeverage.score &&
        bandRank(latestSeasonLeverage.band) >= bandRank(earlySeasonLeverage.band)
    };

    console.log("- Threshold checks");
    console.table([checks]);

    if (Object.values(checks).some((result) => !result)) {
      console.error("Step 6 multi-season manual check failed expected thresholds.");
      process.exit(1);
    }
  } finally {
    await rm(slotRoot, { recursive: true, force: true });
  }
}

await main();
