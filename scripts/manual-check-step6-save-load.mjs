import {
  advanceSeasonState,
  createSeasonState,
  getFixturesForMatchday,
  isSeasonComplete
} from "../packages/sim-core/dist/src/index.js";
import {
  createSaveEnvelopeV1,
  deserializeSaveEnvelope,
  serializeSaveEnvelope
} from "../packages/save/dist/src/index.js";

function createMajorEventFixtureResults(state) {
  return Object.fromEntries(
    getFixturesForMatchday(state, state.currentMatchday).map((fixture, index) => [
      fixture.id,
      {
        homeGoals: (index + 2) % 3,
        awayGoals: (index + 1) % 3
      }
    ])
  );
}

const seasonState = createSeasonState([
  { id: "club-a", name: "Club A", strength: 74 },
  { id: "club-b", name: "Club B", strength: 71 },
  { id: "club-c", name: "Club C", strength: 69 },
  { id: "club-d", name: "Club D", strength: 67 }
]);

const saveState = {
  worldState: seasonState,
  clubPerceptionState: {
    boardConfidence: 57,
    fanSentiment: 59,
    teamMorale: 60,
    managerReputation: 61
  },
  managerCareer: {
    managerId: "manager-step6",
    currentClubId: "club-a",
    reputationHistory: [54, 58, 61],
    careerLeverageHistory: [
      {
        score: 0.64,
        band: "credible",
        reasonSummary: "Board confidence and reputation trend remained above review baseline."
      }
    ],
    sackHistory: [
      {
        clubId: "legacy-club",
        leaguePosition: 18,
        reasonSummary: ["Legacy dismissal due to sustained critical sack risk pressure."],
        sackRisk: 0.82
      }
    ]
  }
};

const envelope = createSaveEnvelopeV1(saveState, "2033-02-03T09:00:00.000Z");
const serialized = serializeSaveEnvelope(envelope);
const restoredEnvelope = deserializeSaveEnvelope(serialized);

const majorEventResults = createMajorEventFixtureResults(seasonState);
const continuedFromOriginal = advanceSeasonState(seasonState, majorEventResults);
const continuedFromRestored = advanceSeasonState(restoredEnvelope.state.worldState, majorEventResults);

const postReloadState = restoredEnvelope.state;
const savesFullCareerState =
  postReloadState.managerCareer.reputationHistory.length === saveState.managerCareer.reputationHistory.length &&
  postReloadState.managerCareer.careerLeverageHistory.length ===
    saveState.managerCareer.careerLeverageHistory.length &&
  postReloadState.managerCareer.sackHistory.length === saveState.managerCareer.sackHistory.length;

console.log("Step 6 Manual Check");
console.log("- Save envelope summary");
console.table([
  {
    version: envelope.version,
    savedAtIso: envelope.savedAtIso,
    currentMatchday: envelope.state.worldState.currentMatchday,
    completedFixtureCount: envelope.state.worldState.completedFixtureIds.length,
    isSeasonComplete: isSeasonComplete(envelope.state.worldState),
    managerReputationHistoryPoints: envelope.state.managerCareer.reputationHistory.length,
    managerSackHistoryEntries: envelope.state.managerCareer.sackHistory.length
  }
]);

console.log("- Deterministic continuation summary");
console.table([
  {
    originalNextMatchday: continuedFromOriginal.currentMatchday,
    restoredNextMatchday: continuedFromRestored.currentMatchday,
    originalCompletedFixtureCount: continuedFromOriginal.completedFixtureIds.length,
    restoredCompletedFixtureCount: continuedFromRestored.completedFixtureIds.length
  }
]);

const checks = {
  serializedPayloadIsNonEmpty: serialized.length > 0,
  saveRoundtripNoDrift: JSON.stringify(restoredEnvelope) === JSON.stringify(envelope),
  deterministicContinuationAfterReload:
    JSON.stringify(continuedFromRestored) === JSON.stringify(continuedFromOriginal),
  savesManagerCareerState: savesFullCareerState
};

console.log("- Threshold checks");
console.table([checks]);

if (Object.values(checks).some((result) => !result)) {
  console.error("Step 6 manual check failed expected save/load thresholds.");
  process.exit(1);
}
