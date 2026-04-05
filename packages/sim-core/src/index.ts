export type {
  ClubDefinition,
  CountryPack,
  CountryPackValidationError,
  CountryPackValidationResult,
  DivisionDefinition,
  SimulationDepth
} from "./shared/types.js";
export type {
  LiveMatchFrame,
  LiveMatchOutcome,
  MatchEvent,
  MatchInput,
  MatchMode,
  MatchOutcome,
  MatchPlayer,
  MatchTeamInput,
  PlannedSubstitution,
  TacticalSetup,
  TeamMatchStats
} from "./match/types.js";
export type {
  MatchPreparationCommand,
  MatchPreparationCommandResult,
  MatchPreparationState
} from "./match/commands.js";
export type {
  ClubPerceptionState,
  ExpectationBand,
  MediaCommentTone,
  PostMatchFalloutInput,
  PostMatchFalloutResult,
  PostMatchPerceptionContext
} from "./club/types.js";
export type {
  BoardExpectationContext,
  BoardExpectationEvaluation,
  SackRiskPressureLevel,
  SackRiskPressureState,
  SackRiskTrend
} from "./club/board.js";
export type {
  TransferDecision,
  TransferEvaluationContext,
  TransferFollowUpEvent,
  TransferTargetProfile
} from "./transfers/transferEngine.js";
export type { TransferDemandBreakdown } from "./transfers/demandModel.js";
export { buildTransferDemandBreakdown, scoreTransferWageFit } from "./transfers/demandModel.js";
export type {
  NegotiationScenarioVariant,
  PromiseTrustImpactBandSummary,
  PromiseTrustImpactSummary,
  ReputationBandTransferOutcomeCalibrationSummary,
  ReputationBandTransferOutcomeSummary,
  ReputationBandNegotiationOutcome,
  TransferNegotiationExplainabilityArtifact,
  TransferNegotiationLogEntry,
  TransferNegotiationComparison
} from "./transfers/negotiation.js";
export {
  buildEqualFeeNegotiationExplainabilityArtifact,
  buildTransferNegotiationLogSamples,
  compareTransferNegotiationContexts,
  summarizePromiseTrustImpactByReputationBand,
  summarizeTransferOutcomesByReputationBand,
  summarizeNegotiationAcceptanceByReputationBand
} from "./transfers/negotiation.js";
export type {
  CompletedSeasonSummary,
  Fixture,
  FixtureResult,
  PromotionRelegationOutcome,
  SeasonState,
  StandingsRow,
  StandingsUpdateInput,
  WorldClub
} from "./world/season.js";
export type {
  SeasonBoardActionCounts,
  CompletedSeasonBoardSummary,
  SeasonBoardResolutionStatus,
  SeasonSackOutcome,
  SeasonBoardDecisionSnapshot,
  SeasonSackDecision,
  SeasonSackDecisionResult,
  SeasonBoardContext,
  SeasonBoardStaticContext,
  SeasonSackRiskPressureSummary
} from "./world/seasonBoard.js";
export {
  applyMatchPreparationCommand
} from "./match/commands.js";
export { computeBoardConfidenceDelta } from "./club/board.js";
export { deriveSackRiskPressureState } from "./club/board.js";
export { evaluateBoardExpectationContext } from "./club/board.js";
export { computeTeamMoraleDelta } from "./club/morale.js";
export { computeManagerReputationDelta } from "./club/reputation.js";
export { applyPostMatchFallout } from "./club/postMatchFallout.js";
export { buildTransferFollowUpEvent, evaluateTransferDecision } from "./transfers/transferEngine.js";
export {
  advanceSeasonState,
  applyResultToStandings,
  createInitialStandings,
  createRoundRobinFixtures,
  createSeasonState,
  getFixturesForMatchday,
  isSeasonComplete,
  resolvePromotionRelegation,
  summarizeCompletedSeason
} from "./world/season.js";
export {
  deriveSeasonSackDecisionFromPressureSummary,
  deriveSeasonBoardContextFromSeasonState,
  extractSeasonSackOutcomes,
  evaluateSeasonBoardDecisions,
  evaluateSeasonBoardContext,
  summarizeSeasonBoardActionCounts,
  summarizeSeasonBoardResolutionStatus,
  summarizeCompletedSeasonBoardOutcomes,
  summarizeSeasonSackRiskPressureTimeline
} from "./world/seasonBoard.js";

export { parseCountryPackJson } from "./content/countryPackLoader.js";
export { runInstantMatch, runLiveMatch } from "./match/modes.js";
export { assertValidCountryPack, validateCountryPack } from "./world/countryPack.js";
