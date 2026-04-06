export type SimulationDepth = "deep" | "shadow";

export interface ClubDefinition {
  id: string;
  name: string;
  shortName: string;
  isPlayable: boolean;
}

export interface DivisionDefinition {
  id: string;
  name: string;
  tier: number;
  simulationDepth: SimulationDepth;
  clubs: ClubDefinition[];
}

export interface CountryPack {
  id: string;
  name: string;
  divisions: DivisionDefinition[];
}

export interface CountryPackValidationError {
  code:
    | "MISSING_DIVISIONS"
    | "MISSING_TOP_TWO_TIERS"
    | "TOP_TWO_NOT_DEEP"
    | "NON_TOP_TWO_NOT_SHADOW"
    | "NO_PLAYABLE_CLUB"
    | "DUPLICATE_CLUB_ID"
    | "MALFORMED_DIVISION";
  message: string;
}

export interface CountryPackValidationResult {
  valid: boolean;
  errors: CountryPackValidationError[];
}
