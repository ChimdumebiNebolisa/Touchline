import { describe, expect, it } from "vitest";

import {
  advanceSeasonState,
  createSeasonState,
  getFixturesForMatchday,
  type ClubPerceptionState,
  type ManagerCareerLeverageSnapshot,
  type SeasonSackOutcome
} from "@touchline/sim-core";

import {
  createSaveEnvelopeV1,
  CURRENT_SAVE_VERSION,
  deserializeSaveEnvelope,
  SaveLoadError,
  serializeSaveEnvelope,
  type SaveGameStateV1
} from "../src/index.js";

function createSampleState(): SaveGameStateV1 {
  const initialSeason = createSeasonState([
    { id: "club-a", name: "Club A", strength: 74 },
    { id: "club-b", name: "Club B", strength: 71 },
    { id: "club-c", name: "Club C", strength: 69 },
    { id: "club-d", name: "Club D", strength: 67 }
  ]);

  const fixtures = getFixturesForMatchday(initialSeason, 1);
  const resultsByFixtureId = Object.fromEntries(
    fixtures.map((fixture, index) => [
      fixture.id,
      {
        homeGoals: 1 + (index % 2),
        awayGoals: index % 2
      }
    ])
  );
  const advancedSeason = advanceSeasonState(initialSeason, resultsByFixtureId);

  const clubPerceptionState: ClubPerceptionState = {
    boardConfidence: 56,
    fanSentiment: 58,
    teamMorale: 61,
    managerReputation: 60
  };

  const leverageSnapshot: ManagerCareerLeverageSnapshot = {
    score: 0.62,
    band: "credible",
    reasonSummary: "Board confidence and reputation trend stayed above review baseline."
  };

  const sackOutcome: SeasonSackOutcome = {
    clubId: "legacy-club",
    leaguePosition: 18,
    reasonSummary: ["Critical sack-risk pressure persisted for multiple matchdays."],
    sackRisk: 0.84
  };

  return {
    worldState: advancedSeason,
    clubPerceptionState,
    managerCareer: {
      managerId: "manager-1",
      currentClubId: "club-a",
      reputationHistory: [53, 57, 60],
      careerLeverageHistory: [leverageSnapshot],
      sackHistory: [sackOutcome]
    }
  };
}

describe("save codec", () => {
  it("serializes and deserializes a save envelope without state drift", () => {
    const state = createSampleState();
    const envelope = createSaveEnvelopeV1(state, "2032-05-01T10:30:00.000Z");

    const serializedFirst = serializeSaveEnvelope(envelope);
    const serializedSecond = serializeSaveEnvelope(envelope);
    const deserialized = deserializeSaveEnvelope(serializedFirst);

    expect(serializedSecond).toBe(serializedFirst);
    expect(deserialized).toEqual(envelope);
    expect(deserialized.version).toBe(CURRENT_SAVE_VERSION);
    expect(deserialized.state.clubPerceptionState.teamMorale).toBe(61);
    expect(deserialized.state.managerCareer.sackHistory).toHaveLength(1);
  });

  it("raises an explicit error when loading invalid JSON", () => {
    expect(() => deserializeSaveEnvelope("not-json")).toThrowError(SaveLoadError);

    try {
      deserializeSaveEnvelope("not-json");
      throw new Error("Expected invalid JSON to throw.");
    } catch (error) {
      expect(error).toBeInstanceOf(SaveLoadError);
      if (error instanceof SaveLoadError) {
        expect(error.code).toBe("INVALID_JSON");
      }
    }
  });

  it("rejects unsupported save versions", () => {
    const state = createSampleState();
    const envelope = createSaveEnvelopeV1(state, "2032-05-01T10:30:00.000Z");
    const unsupportedPayload = JSON.stringify({
      ...envelope,
      version: 99
    });

    expect(() => deserializeSaveEnvelope(unsupportedPayload)).toThrowError(SaveLoadError);

    try {
      deserializeSaveEnvelope(unsupportedPayload);
      throw new Error("Expected unsupported version to throw.");
    } catch (error) {
      expect(error).toBeInstanceOf(SaveLoadError);
      if (error instanceof SaveLoadError) {
        expect(error.code).toBe("UNSUPPORTED_VERSION");
      }
    }
  });

  it("rejects malformed payloads with missing manager career state", () => {
    const state = createSampleState();
    const envelope = createSaveEnvelopeV1(state, "2032-05-01T10:30:00.000Z");

    const malformed = JSON.stringify({
      ...envelope,
      state: {
        ...envelope.state,
        managerCareer: null
      }
    });

    expect(() => deserializeSaveEnvelope(malformed)).toThrowError(SaveLoadError);

    try {
      deserializeSaveEnvelope(malformed);
      throw new Error("Expected malformed payload to throw.");
    } catch (error) {
      expect(error).toBeInstanceOf(SaveLoadError);
      if (error instanceof SaveLoadError) {
        expect(error.code).toBe("MALFORMED_PAYLOAD");
      }
    }
  });
});
