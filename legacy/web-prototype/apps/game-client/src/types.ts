import type {
  ClubPerceptionState,
  MatchOutcome,
  MediaCommentTone,
  PostMatchFalloutResult,
  TacticalSetup,
  TransferFollowUpEvent
} from "@touchline/sim-core";

export type ViewStep = "squad" | "match" | "post-match";

export interface SquadConfig {
  clubId: string;
  clubName: string;
  tactics: TacticalSetup;
  lineupPlayerIds: string[];
}

export interface MatchConfig {
  mode: "instant" | "live";
  promiseRisk: boolean;
  mediaTone: MediaCommentTone;
}

export interface MatchRunResult {
  match: MatchOutcome;
  fallout: PostMatchFalloutResult;
  transferEvent: TransferFollowUpEvent;
  nextPerception: ClubPerceptionState;
}
