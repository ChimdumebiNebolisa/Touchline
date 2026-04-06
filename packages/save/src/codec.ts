import type {
  ClubPerceptionState,
  ManagerCareerLeverageSnapshot,
  SeasonSackOutcome,
  SeasonState
} from "@touchline/sim-core";

import {
  CURRENT_SAVE_VERSION,
  SUPPORTED_SAVE_VERSIONS,
  type ManagerCareerSaveState,
  type SaveEnvelopeV1,
  type SaveGameStateV1,
  type SaveLoadErrorCode
} from "./types.js";

export class SaveLoadError extends Error {
  readonly code: SaveLoadErrorCode;

  constructor(code: SaveLoadErrorCode, message: string) {
    super(message);
    this.name = "SaveLoadError";
    this.code = code;
  }
}

export function isSupportedSaveVersion(version: number): boolean {
  return SUPPORTED_SAVE_VERSIONS.includes(version as (typeof SUPPORTED_SAVE_VERSIONS)[number]);
}

export function assertSupportedSaveVersion(version: number): void {
  if (!isSupportedSaveVersion(version)) {
    throw new SaveLoadError("UNSUPPORTED_VERSION", `Unsupported save payload version ${String(version)}.`);
  }
}

function isRecord(value: unknown): value is Record<string, unknown> {
  return typeof value === "object" && value !== null;
}

function isNumber(value: unknown): value is number {
  return typeof value === "number" && Number.isFinite(value);
}

function isString(value: unknown): value is string {
  return typeof value === "string";
}

function isStringArray(value: unknown): value is string[] {
  return Array.isArray(value) && value.every(isString);
}

function isSeasonState(value: unknown): value is SeasonState {
  if (!isRecord(value)) {
    return false;
  }

  return (
    Array.isArray(value.clubs) &&
    Array.isArray(value.fixtures) &&
    Array.isArray(value.standings) &&
    isNumber(value.currentMatchday) &&
    isStringArray(value.completedFixtureIds)
  );
}

function isClubPerceptionState(value: unknown): value is ClubPerceptionState {
  if (!isRecord(value)) {
    return false;
  }

  return (
    isNumber(value.boardConfidence) &&
    isNumber(value.fanSentiment) &&
    isNumber(value.teamMorale) &&
    isNumber(value.managerReputation)
  );
}

function isManagerCareerLeverageSnapshot(value: unknown): value is ManagerCareerLeverageSnapshot {
  if (!isRecord(value)) {
    return false;
  }

  return (
    isNumber(value.score) &&
    isString(value.band) &&
    isString(value.reasonSummary)
  );
}

function isSeasonSackOutcome(value: unknown): value is SeasonSackOutcome {
  if (!isRecord(value)) {
    return false;
  }

  return (
    isString(value.clubId) &&
    isNumber(value.leaguePosition) &&
    Array.isArray(value.reasonSummary) &&
    value.reasonSummary.every(isString) &&
    isNumber(value.sackRisk)
  );
}

function isManagerCareerSaveState(value: unknown): value is ManagerCareerSaveState {
  if (!isRecord(value)) {
    return false;
  }

  return (
    isString(value.managerId) &&
    isString(value.currentClubId) &&
    Array.isArray(value.reputationHistory) &&
    value.reputationHistory.every(isNumber) &&
    Array.isArray(value.careerLeverageHistory) &&
    value.careerLeverageHistory.every(isManagerCareerLeverageSnapshot) &&
    Array.isArray(value.sackHistory) &&
    value.sackHistory.every(isSeasonSackOutcome)
  );
}

function assertValidSaveGameState(state: unknown): asserts state is SaveGameStateV1 {
  if (!isRecord(state)) {
    throw new SaveLoadError("MALFORMED_PAYLOAD", "Save payload state must be an object.");
  }

  if (!isSeasonState(state.worldState)) {
    throw new SaveLoadError("MALFORMED_PAYLOAD", "Save payload is missing a valid world state.");
  }

  if (!isClubPerceptionState(state.clubPerceptionState)) {
    throw new SaveLoadError(
      "MALFORMED_PAYLOAD",
      "Save payload is missing a valid club perception state."
    );
  }

  if (!isManagerCareerSaveState(state.managerCareer)) {
    throw new SaveLoadError(
      "MALFORMED_PAYLOAD",
      "Save payload is missing a valid manager career state."
    );
  }
}

function assertValidSaveEnvelope(envelope: unknown): asserts envelope is SaveEnvelopeV1 {
  if (!isRecord(envelope)) {
    throw new SaveLoadError("MALFORMED_PAYLOAD", "Save payload must be an object.");
  }

  if (!isNumber(envelope.version)) {
    throw new SaveLoadError("MALFORMED_PAYLOAD", "Save payload version must be numeric.");
  }

  assertSupportedSaveVersion(envelope.version);

  if (!isString(envelope.savedAtIso) || Number.isNaN(Date.parse(envelope.savedAtIso))) {
    throw new SaveLoadError(
      "MALFORMED_PAYLOAD",
      "Save payload must include a valid ISO timestamp."
    );
  }

  assertValidSaveGameState(envelope.state);
}

export function createSaveEnvelopeV1(
  state: SaveGameStateV1,
  savedAtIso: string = new Date().toISOString()
): SaveEnvelopeV1 {
  if (Number.isNaN(Date.parse(savedAtIso))) {
    throw new SaveLoadError("MALFORMED_PAYLOAD", "Saved timestamp must be a valid ISO string.");
  }

  assertValidSaveGameState(state);

  return {
    version: CURRENT_SAVE_VERSION,
    savedAtIso,
    state
  };
}

export function serializeSaveEnvelope(envelope: SaveEnvelopeV1): string {
  assertValidSaveEnvelope(envelope);
  return JSON.stringify(envelope);
}

export function deserializeSaveEnvelope(raw: string): SaveEnvelopeV1 {
  let parsed: unknown;
  try {
    parsed = JSON.parse(raw);
  } catch {
    throw new SaveLoadError("INVALID_JSON", "Save payload is not valid JSON.");
  }

  assertValidSaveEnvelope(parsed);
  return parsed;
}
