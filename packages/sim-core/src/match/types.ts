export type MatchMode = "instant" | "live";

export interface TacticalSetup {
  blockHeight: number;
  pressingIntensity: number;
  width: number;
  tempo: number;
  risk: number;
}

export interface MatchPlayer {
  id: string;
  name: string;
  finishing: number;
  creativity: number;
  defending: number;
  discipline: number;
  durability: number;
  condition: number;
}

export interface PlannedSubstitution {
  minute: number;
  playerOutId: string;
  playerIn: MatchPlayer;
}

export interface MatchTeamInput {
  teamId: string;
  name: string;
  morale: number;
  lineup: MatchPlayer[];
  bench: MatchPlayer[];
  tactics: TacticalSetup;
  substitutions: PlannedSubstitution[];
}

export interface MatchInput {
  seed: number;
  home: MatchTeamInput;
  away: MatchTeamInput;
}

export interface MatchEvent {
  minute: number;
  teamId: string;
  type: "chance" | "goal" | "yellow-card" | "red-card" | "injury" | "substitution";
  playerId?: string;
  description: string;
  xg?: number;
}

export interface TeamMatchStats {
  goals: number;
  xg: number;
  shots: number;
  shotsOnTarget: number;
  yellowCards: number;
  redCards: number;
  injuries: number;
  averageFatigue: number;
}

export interface MatchOutcome {
  mode: MatchMode;
  seed: number;
  result: {
    homeGoals: number;
    awayGoals: number;
  };
  stats: {
    home: TeamMatchStats;
    away: TeamMatchStats;
  };
  eventLog: MatchEvent[];
  reasonSummary: string[];
}

export interface LiveMatchFrame {
  frame: number;
  minute: number;
  event: MatchEvent;
}

export interface LiveMatchOutcome extends MatchOutcome {
  timeline: LiveMatchFrame[];
}
