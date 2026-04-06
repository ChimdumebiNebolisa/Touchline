import type {
  ClubPerceptionState,
  ManagerCareerLeverageSnapshot,
  SeasonSackOutcome,
  SeasonState
} from "@touchline/sim-core";

export const CURRENT_SAVE_VERSION = 1;

export type SaveLoadErrorCode = "INVALID_JSON" | "UNSUPPORTED_VERSION" | "MALFORMED_PAYLOAD";

export interface ManagerCareerSaveState {
  managerId: string;
  currentClubId: string;
  reputationHistory: number[];
  careerLeverageHistory: ManagerCareerLeverageSnapshot[];
  sackHistory: SeasonSackOutcome[];
}

export interface SaveGameStateV1 {
  worldState: SeasonState;
  clubPerceptionState: ClubPerceptionState;
  managerCareer: ManagerCareerSaveState;
}

export interface SaveEnvelopeV1 {
  version: 1;
  savedAtIso: string;
  state: SaveGameStateV1;
}
