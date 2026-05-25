using System;
using System.Collections.Generic;

public sealed class SaveSlotStageFoundationData
{
    public string TeamStyleName { get; set; } = "Balanced";
    public int PassingDirectness { get; set; } = 52;
    public int DefensiveLine { get; set; } = 55;
    public int Tackling { get; set; } = 52;
    public int TacticalFamiliarityScore { get; set; } = 58;
    public string TeamInstructionsSummary { get; set; } = string.Empty;
    public string PlayerRolesSummary { get; set; } = string.Empty;
    public string PlayerInstructionsSummary { get; set; } = string.Empty;
    public int TacticalRoleFitScore { get; set; } = 60;
    public string TacticalRoleFitSummary { get; set; } = string.Empty;
    public string PlayerFamiliaritySummary { get; set; } = string.Empty;
    public string SetPieceApproachName { get; set; } = "Balanced set pieces";
    public string SetPieceSummary { get; set; } = string.Empty;
    public string OpponentPreparationFocusName { get; set; } = "Balanced brief";
    public string OpponentPreparationSummary { get; set; } = string.Empty;
    public string TacticalFitNotes { get; set; } = string.Empty;
    public string TacticalRiskNotes { get; set; } = string.Empty;
    public string TrainingFocusName { get; set; } = "Team cohesion";
    public string TrainingIntensityName { get; set; } = "Standard";
    public string TrainingStatusSummary { get; set; } = string.Empty;
    public string ScoutingReportDepthName { get; set; } = "Standard report";
    public SaveSlotScoutingAssignmentData? ScoutingAssignment { get; set; }
    public SaveSlotNewsEventData[]? NewsEvents { get; set; }
    public SaveSlotDecisionEventData[]? ActiveDecisionEvents { get; set; }
    public SaveSlotDecisionEventData[]? ResolvedDecisionEvents { get; set; }
    public SaveSlotRecruitmentTargetData? RecruitmentTarget { get; set; }
    public SaveSlotRecruitmentTargetData[]? RecruitmentShortlist { get; set; }
    public string[]? TransferHistory { get; set; }
    public SaveSlotContractOfferData? TransferContractOffer { get; set; }
    public SaveSlotContractOfferData? RenewalContractOffer { get; set; }
    public string[]? ContractHistory { get; set; }
    public SaveSlotPromiseRecordData[]? PromiseRecords { get; set; }
    public string JobSecurityName { get; set; } = "Stable";
    public SaveSlotJobOfferEventData? JobOffer { get; set; }
    public string[]? CareerHistory { get; set; }
    public string LicenseOpportunitySummary { get; set; } = string.Empty;
    public string ObjectiveReviewSummary { get; set; } = string.Empty;
    public int FanTrust { get; set; } = 55;
    public int MediaTrust { get; set; } = 52;
    public int WorldReputation { get; set; } = 45;
    public int ClubReputation { get; set; } = 45;
    public int MediaReputation { get; set; } = 45;
    public int TacticalReputation { get; set; } = 45;
    public int YouthReputation { get; set; } = 45;
    public int RecruitmentReputation { get; set; } = 45;
    public int BoardPressure { get; set; } = 35;
    public int FanPressure { get; set; } = 35;
    public int DressingRoomPressure { get; set; } = 35;
    public int TransferPressure { get; set; } = 25;
    public int FinancialPressure { get; set; } = 25;
    public string[]? PerceptionHistory { get; set; }
    public int DirectorCooperation { get; set; } = 55;
    public int DirectorConflict { get; set; } = 25;
    public string DirectorScoutingPriority { get; set; } = string.Empty;
    public string DirectorTransferPreference { get; set; } = string.Empty;
    public string DirectorSalesPressureSummary { get; set; } = string.Empty;
    public string DirectorBoardReportSummary { get; set; } = string.Empty;
    public string[]? DirectorActionHistory { get; set; }
    public SaveSlotStaffMarketCandidateData? StaffMarketCandidate { get; set; }
    public string StaffReportSummary { get; set; } = string.Empty;
    public string StaffMarketSummary { get; set; } = string.Empty;
    public string[]? StaffHistory { get; set; }
    public int YouthAcademyQuality { get; set; }
    public int YouthRecruitmentReach { get; set; }
    public int YouthCoachingQuality { get; set; }
    public string YouthFacilitiesSummary { get; set; } = string.Empty;
    public string YouthIntakeDateSummary { get; set; } = string.Empty;
    public string YouthBoardExpectation { get; set; } = string.Empty;
    public string YouthFanExpectation { get; set; } = string.Empty;
    public SaveSlotYouthProspectData[]? YouthProspects { get; set; }
    public string[]? YouthHistory { get; set; }
    public string PlayerDevelopmentSummary { get; set; } = string.Empty;
    public string[]? PlayerDevelopmentHistory { get; set; }
    public int FinanceTransferBudgetRemaining { get; set; }
    public int FinanceWageBudget { get; set; }
    public int FinanceCurrentWageBill { get; set; }
    public int FinanceTransferCommitments { get; set; }
    public int FinanceDebt { get; set; }
    public int FinanceRevenue { get; set; }
    public int FinanceExpenses { get; set; }
    public int FinanceProjectedBalance { get; set; }
    public int FinanceTicketIncome { get; set; }
    public int FinanceCommercialIncome { get; set; }
    public int FinancePrizeMoney { get; set; }
    public int FinanceBoardInjection { get; set; }
    public int FinanceBudgetCut { get; set; }
    public int WageStructurePressure { get; set; }
    public string FinanceSummary { get; set; } = string.Empty;
    public string ProfitExpectationSummary { get; set; } = string.Empty;
    public string BoardFinanceActionSummary { get; set; } = string.Empty;
    public string[]? FinanceHistory { get; set; }
}

public sealed class SaveSlotStaffMarketCandidateData
{
    public string Name { get; set; } = string.Empty;
    public string RoleName { get; set; } = "First-Team Coach";
    public int Quality { get; set; }
    public int Wage { get; set; }
    public int ContractExpiryYear { get; set; }
    public int Reputation { get; set; }
    public int Loyalty { get; set; }
    public int Ambition { get; set; }
    public string PreferredStyle { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string InterestSummary { get; set; } = string.Empty;
    public string BoardApproval { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string OutcomeSummary { get; set; } = string.Empty;
}

public sealed class SaveSlotYouthProspectData
{
    public string ProspectId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Position { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string PlayingStyle { get; set; } = string.Empty;
    public string Personality { get; set; } = string.Empty;
    public string VisibleInfo { get; set; } = string.Empty;
    public string HiddenPotentialBand { get; set; } = string.Empty;
    public int PotentialCertainty { get; set; }
    public string DevelopmentCurve { get; set; } = string.Empty;
    public string LoanSuitability { get; set; } = string.Empty;
    public bool IsPromoted { get; set; }
    public string Status { get; set; } = string.Empty;
}
public sealed class SaveSlotScoutingAssignmentData
{
    public string Target { get; set; } = string.Empty;
    public int DaysRemaining { get; set; }
    public int ReportQuality { get; set; }
    public string DiscoverySummary { get; set; } = string.Empty;
    public bool ReportReady { get; set; }
}

public sealed class SaveSlotNewsEventData
{
    public string Title { get; set; } = string.Empty;
    public string CategoryName { get; set; } = "Club";
    public string Reliability { get; set; } = "Confirmed";
    public string Text { get; set; } = string.Empty;
    public int Importance { get; set; }
    public string SourceType { get; set; } = "Club source";
    public string RelatedEntity { get; set; } = string.Empty;
    public string EffectSummary { get; set; } = string.Empty;
    public string CooldownKey { get; set; } = string.Empty;
}

public sealed class SaveSlotDecisionEventData
{
    public string EventId { get; set; } = string.Empty;
    public string EventTypeName { get; set; } = "Media question";
    public string Title { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public string Reliability { get; set; } = "Confirmed";
    public string RelatedEntity { get; set; } = string.Empty;
    public int Importance { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string PrimaryOption { get; set; } = string.Empty;
    public string SecondaryOption { get; set; } = string.Empty;
    public string PrimaryEffectSummary { get; set; } = string.Empty;
    public string SecondaryEffectSummary { get; set; } = string.Empty;
    public string CooldownKey { get; set; } = string.Empty;
    public int DaysUntilRepeat { get; set; }
    public bool IsResolved { get; set; }
    public string OutcomeSummary { get; set; } = string.Empty;
}

public sealed class SaveSlotRecruitmentTargetData
{
    public string PlayerName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string InformationSummary { get; set; } = string.Empty;
    public string InterestSummary { get; set; } = string.Empty;
    public string TacticalFitSummary { get; set; } = string.Empty;
    public string EstimatedFeeRange { get; set; } = string.Empty;
    public string EstimatedWageRange { get; set; } = string.Empty;
    public string DirectorResponse { get; set; } = string.Empty;
    public string BoardResponse { get; set; } = string.Empty;
    public string TargetStatus { get; set; } = string.Empty;
    public string ClubValuation { get; set; } = string.Empty;
    public string AgentMood { get; set; } = string.Empty;
    public string RivalInterest { get; set; } = string.Empty;
    public string BoardStance { get; set; } = string.Empty;
    public string DirectorStance { get; set; } = string.Empty;
    public string OutcomeState { get; set; } = string.Empty;
    public bool IsLoanCandidate { get; set; }
    public string LoanDirection { get; set; } = string.Empty;
    public string DevelopmentLoanSuitability { get; set; } = string.Empty;
    public string PlayingTimeExpectation { get; set; } = string.Empty;
    public string LoanClubFit { get; set; } = string.Empty;
    public string LoanReviewSummary { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public sealed class SaveSlotContractOfferData
{
    public string OfferId { get; set; } = string.Empty;
    public string PlayerName { get; set; } = string.Empty;
    public bool IsRenewal { get; set; }
    public string SourceType { get; set; } = string.Empty;
    public string AgentArchetype { get; set; } = string.Empty;
    public string WageSummary { get; set; } = string.Empty;
    public int ProposedWage { get; set; }
    public string DurationSummary { get; set; } = string.Empty;
    public int DurationYears { get; set; }
    public string ExpirySummary { get; set; } = string.Empty;
    public string SquadRole { get; set; } = string.Empty;
    public string ClausesSummary { get; set; } = string.Empty;
    public string RenewalStatus { get; set; } = string.Empty;
    public string AgentMood { get; set; } = string.Empty;
    public string PlayerInterest { get; set; } = string.Empty;
    public string BoardApproval { get; set; } = string.Empty;
    public string PromiseSummary { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string OutcomeSummary { get; set; } = string.Empty;
    public bool IsAccepted { get; set; }
}

public sealed class SaveSlotPromiseRecordData
{
    public string PromiseType { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public string ExpectedAction { get; set; } = string.Empty;
    public string DeadlineSummary { get; set; } = string.Empty;
    public int DaysRemaining { get; set; }
    public string StatusName { get; set; } = "Active";
    public string CurrentEvidence { get; set; } = string.Empty;
    public string AgentMood { get; set; } = string.Empty;
    public string ConsequenceRisk { get; set; } = string.Empty;
}

public sealed class SaveSlotJobOfferEventData
{
    public string OfferTypeName { get; set; } = "Interview invitation";
    public string ClubName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string InterestSummary { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

public partial class GameState
{
    private readonly List<NewsEvent> _foundationNewsEvents = new();
    private readonly List<DecisionEvent> _activeDecisionEvents = new();
    private readonly List<DecisionEvent> _resolvedDecisionEvents = new();
    private readonly List<RecruitmentTarget> _recruitmentShortlist = new();
    private readonly List<PromiseRecord> _promiseRecords = new();
    private readonly List<string> _careerHistory = new();
    private readonly List<string> _perceptionHistory = new();
    private readonly List<string> _transferHistory = new();
    private readonly List<string> _contractHistory = new();
    private readonly List<string> _directorActionHistory = new();
    private readonly List<string> _staffHistory = new();
    private readonly List<YouthProspect> _youthProspects = new();
    private readonly List<string> _youthHistory = new();
    private readonly List<string> _playerDevelopmentHistory = new();
    private readonly List<string> _financeHistory = new();

    private readonly record struct ContractResolution(
        ContractOffer Offer,
        int PressureDelta,
        int FinancialDelta,
        int ReputationDelta,
        int TrustDelta);

    public TacticalTeamStyle TeamStyle { get; private set; } = TacticalTeamStyle.Balanced;
    public int PassingDirectness { get; private set; } = 52;
    public int DefensiveLine { get; private set; } = 55;
    public int Tackling { get; private set; } = 52;
    public int TacticalFamiliarityScore { get; private set; } = 58;
    public string TeamInstructionsSummary { get; private set; } = "Balanced tactical foundation.";
    public string PlayerRolesSummary { get; private set; } = "Player roles pending until a squad is selected.";
    public string PlayerInstructionsSummary { get; private set; } = "Player instructions pending until tactics are selected.";
    public int TacticalRoleFitScore { get; private set; } = 60;
    public string TacticalRoleFitSummary { get; private set; } = "Role fit pending.";
    public string PlayerFamiliaritySummary { get; private set; } = "Player familiarity pending.";
    public TacticalSetPieceApproach SetPieceApproach { get; private set; } = TacticalSetPieceApproach.BalancedSetPieces;
    public string SetPieceSummary { get; private set; } = "Set-piece approach pending.";
    public OpponentPreparationFocus CurrentOpponentPreparationFocus { get; private set; } = OpponentPreparationFocus.BalancedBrief;
    public string OpponentPreparationSummary { get; private set; } = "Opponent preparation pending.";
    public string TacticalFitNotes { get; private set; } = "Fit notes pending.";
    public string TacticalRiskNotes { get; private set; } = "Risk notes pending.";
    public TrainingFocus CurrentTrainingFocus { get; private set; } = TrainingFocus.TeamCohesion;
    public TrainingIntensity CurrentTrainingIntensity { get; private set; } = TrainingIntensity.Standard;
    public ScoutingReportDepth CurrentScoutingReportDepth { get; private set; } = ScoutingReportDepth.StandardReport;
    public string TrainingStatusSummary { get; private set; } = "Training foundation ready: team cohesion is the default weekly focus.";
    public ScoutingAssignment? CurrentScoutingAssignment { get; private set; }
    public RecruitmentTarget? CurrentRecruitmentTarget { get; private set; }
    public ContractOffer? CurrentTransferContractOffer { get; private set; }
    public ContractOffer? CurrentRenewalContractOffer { get; private set; }
    public JobSecurityState JobSecurity { get; private set; } = JobSecurityState.Stable;
    public JobOfferEvent? CurrentJobOffer { get; private set; }
    public string LicenseOpportunitySummary { get; private set; } = "License progression will be reviewed after sustained progress.";
    public string ObjectiveReviewSummary { get; private set; } = "Objective review pending first run of matches.";
    public int FanTrust { get; private set; } = 55;
    public int MediaTrust { get; private set; } = 52;
    public int WorldReputation { get; private set; } = 45;
    public int ClubReputation { get; private set; } = 45;
    public int MediaReputation { get; private set; } = 45;
    public int TacticalReputation { get; private set; } = 45;
    public int YouthReputation { get; private set; } = 45;
    public int RecruitmentReputation { get; private set; } = 45;
    public int BoardPressure { get; private set; } = 35;
    public int FanPressure { get; private set; } = 35;
    public int DressingRoomPressure { get; private set; } = 35;
    public int TransferPressure { get; private set; } = 25;
    public int FinancialPressure { get; private set; } = 25;
    public int DirectorCooperation { get; private set; } = 55;
    public int DirectorConflict { get; private set; } = 25;
    public string DirectorScoutingPriority { get; private set; } = "Director scouting priority pending.";
    public string DirectorTransferPreference { get; private set; } = "Director transfer preference pending.";
    public string DirectorSalesPressureSummary { get; private set; } = "Director sales pressure pending.";
    public string DirectorBoardReportSummary { get; private set; } = "Director board report pending.";
    public StaffMarketCandidate? CurrentStaffMarketCandidate { get; private set; }
    public string StaffReportSummary { get; private set; } = "Staff reports pending.";
    public string StaffMarketSummary { get; private set; } = "Staff market pending.";
    public int YouthAcademyQuality { get; private set; }
    public int YouthRecruitmentReach { get; private set; }
    public int YouthCoachingQuality { get; private set; }
    public string YouthFacilitiesSummary { get; private set; } = "Youth facilities pending.";
    public string YouthIntakeDateSummary { get; private set; } = "Youth intake pending.";
    public string YouthBoardExpectation { get; private set; } = "Youth board expectation pending.";
    public string YouthFanExpectation { get; private set; } = "Youth fan expectation pending.";
    public string PlayerDevelopmentSummary { get; private set; } = "Player development cadence pending.";
    public int FinanceTransferBudgetRemaining { get; private set; }
    public int FinanceWageBudget { get; private set; }
    public int FinanceCurrentWageBill { get; private set; }
    public int FinanceTransferCommitments { get; private set; }
    public int FinanceDebt { get; private set; }
    public int FinanceRevenue { get; private set; }
    public int FinanceExpenses { get; private set; }
    public int FinanceProjectedBalance { get; private set; }
    public int FinanceTicketIncome { get; private set; }
    public int FinanceCommercialIncome { get; private set; }
    public int FinancePrizeMoney { get; private set; }
    public int FinanceBoardInjection { get; private set; }
    public int FinanceBudgetCut { get; private set; }
    public int WageStructurePressure { get; private set; }
    public string FinanceSummary { get; private set; } = "Finance foundation pending.";
    public string ProfitExpectationSummary { get; private set; } = "Profit expectation pending.";
    public string BoardFinanceActionSummary { get; private set; } = "Board finance action pending.";

    public string TeamStyleName => StageFoundationText.GetDisplayName(TeamStyle);
    public string TacticalFamiliarityName => StageFoundationText.GetDisplayName(TacticsFoundation.FamiliarityFromScore(TacticalFamiliarityScore));
    public string SetPieceApproachName => StageFoundationText.GetDisplayName(SetPieceApproach);
    public string OpponentPreparationFocusName => StageFoundationText.GetDisplayName(CurrentOpponentPreparationFocus);
    public string TrainingFocusName => StageFoundationText.GetDisplayName(CurrentTrainingFocus);
    public string TrainingIntensityName => StageFoundationText.GetDisplayName(CurrentTrainingIntensity);
    public string ScoutingReportDepthName => StageFoundationText.GetDisplayName(CurrentScoutingReportDepth);
    public string JobSecurityName => StageFoundationText.GetDisplayName(JobSecurity);
    public string CareerHistorySummary => _careerHistory.Count == 0 ? "Career history starts when a club is selected." : string.Join("\n", _careerHistory);
    public string PromiseSummary => _promiseRecords.Count == 0 ? "No active promises." : BuildPromiseSummary();
    public string TrustSummary => $"Trust | board {CareerProfile.BoardTrust}, fans {FanTrust}, players {CareerProfile.PlayerTrust}, staff {CareerProfile.StaffTrust}, Director {CareerProfile.DirectorTrust}, media {MediaTrust}";
    public string ReputationSummary => $"Reputation | world {WorldReputation}, club {ClubReputation}, tactical {TacticalReputation}, youth {YouthReputation}, recruitment {RecruitmentReputation}, media {MediaReputation}";
    public string PressureCategorySummary => $"Pressure | job {JobPressure}, board {BoardPressure}, fans {FanPressure}, media {CareerProfile.MediaPressure}, dressing room {DressingRoomPressure}, transfer {TransferPressure}, financial {FinancialPressure}";
    public string PerceptionHistorySummary => _perceptionHistory.Count == 0 ? "Perception history starts after matches, promises, or recruitment decisions." : string.Join("\n", _perceptionHistory);
    public string DecisionEventSummary => BuildDecisionEventSummary();
    public string RecruitmentShortlistSummary => BuildRecruitmentShortlistSummary();
    public string TransferHistorySummary => _transferHistory.Count == 0 ? "Transfer history starts when a recommendation, request, approach, or loan review is recorded." : string.Join("\n", _transferHistory);
    public string ContractFoundationSummary => BuildContractFoundationSummary();
    public string DirectorInfluenceSummary => BuildDirectorInfluenceSummary();
    public string StaffImpactSummary => BuildStaffImpactSummary();
    public string YouthAcademySummary => BuildYouthAcademySummary();
    public string PlayerDevelopmentHistorySummary => _playerDevelopmentHistory.Count == 0 ? "Player development history starts after weekly training, match minutes, loan review, or season aging." : string.Join("\n", _playerDevelopmentHistory);
    public string FinanceHistorySummary => _financeHistory.Count == 0 ? "Finance history starts after weekly revenue, transfer, contract, staff, board cut, or board injection events." : string.Join("\n", _financeHistory);
    public string RecruitmentFoundationSummary => CurrentRecruitmentTarget == null
        ? "Recruitment foundation pending scouting target."
        : $"{CurrentRecruitmentTarget.PlayerName} ({CurrentRecruitmentTarget.Position}) | {CurrentRecruitmentTarget.InformationSummary} | {CurrentRecruitmentTarget.InterestSummary} | {CurrentRecruitmentTarget.TacticalFitSummary} | Fee {CurrentRecruitmentTarget.EstimatedFeeRange} | Wage {CurrentRecruitmentTarget.EstimatedWageRange} | Status {CurrentRecruitmentTarget.TargetStatus} | Valuation {CurrentRecruitmentTarget.ClubValuation} | Agent {CurrentRecruitmentTarget.AgentMood} | Rival {CurrentRecruitmentTarget.RivalInterest} | Board {CurrentRecruitmentTarget.BoardStance} | Director {CurrentRecruitmentTarget.DirectorStance} | Outcome {CurrentRecruitmentTarget.OutcomeState} | {CurrentRecruitmentTarget.Status}\nDirector of Football\n{DirectorInfluenceSummary}\nShortlist\n{RecruitmentShortlistSummary}\nContracts\n{ContractFoundationSummary}\nTransfer history\n{TransferHistorySummary}";
    public string TrainingScoutingSummary => $"{TrainingFocusName} ({TrainingIntensityName}): {TrainingStatusSummary}\nScouting depth: {ScoutingReportDepthName}\nScouting: {BuildScoutingSummary()}\nDevelopment\n{PlayerDevelopmentSummary}\nDevelopment history\n{PlayerDevelopmentHistorySummary}\nStaff effects\n{StaffImpactSummary}";
    public string CareerMarketSummary => $"Job security: {JobSecurityName}\n{TrustSummary}\n{ReputationSummary}\n{PressureCategorySummary}\nFinance\n{FinanceSummary}\nFinance history\n{FinanceHistorySummary}\nLicense: {LicenseOpportunitySummary}\nJob market: {BuildJobOfferSummary()}";
    public string TacticsFoundationSummary => $"{TeamStyleName} | {TeamInstructionsSummary}\n{SetPieceSummary}\n{OpponentPreparationSummary}\n{PlayerRolesSummary}\n{PlayerInstructionsSummary}\n{TacticalRoleFitSummary}\n{PlayerFamiliaritySummary}\n{TacticalFitNotes}\n{TacticalRiskNotes}";

    public void UpdateTactics(string formation, string teamStyle, int pressIntensity, int tempo, int width, int risk)
    {
        var previousFormation = TacticalFormation;
        var previousStyle = TeamStyle;
        TacticalFormation = formation;
        TeamStyle = StageFoundationText.ParseTeamStyle(teamStyle);
        PressIntensity = pressIntensity;
        Tempo = tempo;
        Width = width;
        Risk = risk;
        RefreshTacticFoundation(previousFormation, previousStyle);
    }

    public string TryApplyTacticsFromUser(string formation, string teamStyle, int pressIntensity, int tempo, int width, int risk)
    {
        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            AddNews(
                "Tactical recommendation filed",
                NewsCategory.Club,
                "Internal",
                $"{ManagerName} recommended {formation} {teamStyle}, but Assistant Manager authority cannot finalize the match plan.",
                2);
            return "Assistant Manager tactical recommendation logged; final tactical authority sits with senior staff. Saved match plan unchanged.";
        }

        UpdateTactics(formation, teamStyle, pressIntensity, tempo, width, risk);
        return $"Saved tactical setup applied to the shared match engine: {formation} | {teamStyle} | Pressing {pressIntensity} | Tempo {tempo} | Width {width} | Mentality {risk}";
    }

    public void SetTrainingFocusByName(string trainingFocusName)
    {
        SetTrainingPlanByName(trainingFocusName, TrainingIntensityName);
    }

    public void SetTrainingPlanByName(string trainingFocusName, string trainingIntensityName)
    {
        CurrentTrainingFocus = StageFoundationText.ParseTrainingFocus(trainingFocusName);
        CurrentTrainingIntensity = StageFoundationText.ParseTrainingIntensity(trainingIntensityName);
        TrainingStatusSummary = $"Next weekly block set to {TrainingFocusName.ToLowerInvariant()} at {TrainingIntensityName.ToLowerInvariant()} intensity.";
        AddNews(
            "Training focus updated",
            NewsCategory.Training,
            "Confirmed",
            $"{SelectedClubName} staff prepare a {TrainingFocusName.ToLowerInvariant()} block at {TrainingIntensityName.ToLowerInvariant()} intensity.",
            3);
    }

    public void StartBasicScoutingAssignment(string target)
    {
        StartScoutingAssignment(target, ScoutingReportDepthName);
    }

    public void StartScoutingAssignment(string target, string reportDepthName)
    {
        var quality = Math.Clamp(GetStaffQuality(StaffRole.Scout) + GetStaffQuality(StaffRole.DataAnalyst) / 4, 35, 95);
        CurrentScoutingReportDepth = StageFoundationText.ParseScoutingReportDepth(reportDepthName);
        var delay = CurrentScoutingReportDepth switch
        {
            ScoutingReportDepth.QuickLook => 5,
            ScoutingReportDepth.FullReport => 18,
            _ => 10
        };
        var qualityModifier = CurrentScoutingReportDepth switch
        {
            ScoutingReportDepth.QuickLook => -10,
            ScoutingReportDepth.FullReport => 10,
            _ => 0
        };
        CurrentScoutingAssignment = new ScoutingAssignment
        {
            Target = string.IsNullOrWhiteSpace(target) ? "Position need: versatile midfielder" : target,
            DaysRemaining = delay,
            ReportQuality = Math.Clamp(quality + qualityModifier, 25, 98),
            DiscoverySummary = $"Initial {ScoutingReportDepthName.ToLowerInvariant()}: exact current role unknown, attribute ranges pending, personality ?.",
            ReportReady = false
        };
        StaffReportSummary = BuildStaffReportSummary();
        AddNews(
            "Scouting assignment opened",
            NewsCategory.Scouting,
            "Confirmed",
            $"Recruitment staff started a {ScoutingReportDepthName.ToLowerInvariant()} on {CurrentScoutingAssignment.Target}.",
            3);
    }

    public bool AdvanceOneCareerDay()
    {
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return false;
        }

        CurrentDate = CurrentDate.AddDays(1);
        TickDecisionEventCooldowns(1);
        ApplyScoutingProgress(1);
        TrainingStatusSummary = $"Daily work completed under {TrainingFocusName.ToLowerInvariant()} focus.";
        AddNews(
            "Daily update",
            NewsCategory.Club,
            "Confirmed",
            $"{SelectedClubName} moved one day forward to {CurrentDateLabel}.",
            1);
        return true;
    }

    public void ApplyWeeklyFoundationProgress()
    {
        TickDecisionEventCooldowns(7);
        ApplyTrainingEffects();
        ApplyPlayerDevelopmentProgress();
        ApplyWeeklyFinanceProgress();
        ApplyScoutingProgress(7);
        ReviewPromiseLifecycle("Weekly review", 7);
        EvaluateCareerFoundationState();
        GenerateContextDecisionEvent("Weekly review");
        AddNews(
            "Weekly football report",
            NewsCategory.Training,
            "Staff report",
            $"{TrainingFocusName} affected player condition and tactical familiarity.",
            3);
    }

    public bool AdvanceOneCareerWeek()
    {
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return false;
        }

        CurrentDate = CurrentDate.AddDays(7);
        ApplyWeeklyFoundationProgress();
        AddNews(
            "Weekly advance",
            NewsCategory.Club,
            "Confirmed",
            $"{SelectedClubName} advanced one week to {CurrentDateLabel}.",
            2);
        return true;
    }

    public string AttemptBasicRecruitmentAction()
    {
        EnsureFinanceState();
        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Recruitment target unavailable.";
        }

        var target = CurrentRecruitmentTarget;
        var role = CareerProfile.Role;
        if (role == ManagerRole.AssistantManager)
        {
            CurrentRecruitmentTarget = CloneRecruitmentTarget(
                target,
                "Recommended by Assistant Manager; final authority sits with senior staff.",
                "Recommended",
                "Recommended only");
            SyncCurrentRecruitmentTargetToShortlist();
            TransferPressure = Math.Clamp(TransferPressure + 1, 0, 100);
            RefreshPressureCategories();
            ApplyDirectorRecruitmentInfluence(target, false, "Assistant recommendation");
            RecordTransferHistory($"{target.PlayerName}: Assistant Manager recommendation filed; board and Director approval still required.");
            RecordPerceptionHistory("Recruitment recommendation", $"role authority limited action; transfer pressure {TransferPressure}, recruitment reputation {RecruitmentReputation}");
            AddNews(
                "Recruitment recommendation filed",
                NewsCategory.Transfer,
                "Internal",
                $"{ManagerName} recommended {target.PlayerName}, but role authority prevents a formal approach.",
                2);
            return CurrentRecruitmentTarget.Status;
        }

        if (role == ManagerRole.HeadCoach)
        {
            CurrentRecruitmentTarget = CloneRecruitmentTarget(
                target,
                "Requested by Head Coach; Director of Football and board review required.",
                "Requested",
                "Requested for review");
            SyncCurrentRecruitmentTargetToShortlist();
            TransferPressure = Math.Clamp(TransferPressure + 2, 0, 100);
            RefreshPressureCategories();
            ApplyDirectorRecruitmentInfluence(target, false, "Head Coach request");
            RecordTransferHistory($"{target.PlayerName}: Head Coach request submitted; Director stance {target.DirectorStance}; board stance {target.BoardStance}.");
            RecordPerceptionHistory("Recruitment request", $"Head Coach authority created review pressure; transfer pressure {TransferPressure}, board trust {CareerProfile.BoardTrust}");
            AddNews(
                "Head Coach submits recruitment request",
                NewsCategory.Transfer,
                "Internal",
                $"{ManagerName} requested {target.PlayerName}; recruitment control remains shared with the Director of Football.",
                3);
            return CurrentRecruitmentTarget.Status;
        }

        var trustSupport = CareerProfile.BoardTrust >= 62 || CareerProfile.DirectorTrust >= 62;
        var lowTrustBlock = CareerProfile.BoardTrust < 42 && !target.TacticalFitSummary.Contains("Strong", StringComparison.Ordinal);
        var marketScore = BuildRecruitmentMarketScore(target);
        var financeBlock = !CanFinanceRecruitmentTarget(target);
        var approved = !financeBlock && !lowTrustBlock && (marketScore >= 62 ||
            trustSupport ||
            CurrentClub?.DirectorRelationshipState is DirectorRelationshipState.Ally or DirectorRelationshipState.Supportive);
        var status = approved
            ? "Board approval granted for a basic approach after fit, agent, rival, board, and Director review."
            : financeBlock
                ? "Board blocks the basic approach after agent, rival, Director, and finance review: transfer budget, wage budget, or wage structure cannot support the commitment."
                : "Board rejects the basic approach: fit, wage, agent mood, rival pressure, and Director confidence do not align.";
        var outcomeState = approved ? "Approach approved" : financeBlock ? "Blocked by finance" : "Blocked by review";
        CurrentRecruitmentTarget = CloneRecruitmentTarget(target, status, approved ? "Approved" : "Blocked", outcomeState);
        SyncCurrentRecruitmentTargetToShortlist();
        if (approved)
        {
            ApplyRecruitmentFinanceImpact(target);
        }

        if (approved && !target.IsLoanCandidate)
        {
            _promiseRecords.Add(new PromiseRecord
            {
                PromiseType = "Squad role",
                Recipient = target.PlayerName,
                Source = "Recruitment approach",
                IsPublic = false,
                ExpectedAction = "Offer a clear rotation pathway before any agreement.",
                DeadlineSummary = "Before contract completion",
                DaysRemaining = 21,
                Status = PromiseStatus.Active,
                CurrentEvidence = "Promise created from recruitment action; no review yet.",
                AgentMood = "Watching role pathway",
                ConsequenceRisk = "Agent concern if the promised role is ignored."
            });
        }

        TransferPressure = Math.Clamp(TransferPressure + (approved ? 4 : 7), 0, 100);
        RecruitmentReputation = Math.Clamp(RecruitmentReputation + (approved ? 1 : -1), 0, 100);
        RefreshPressureCategories();
        ApplyDirectorRecruitmentInfluence(target, approved, "Manager approach");
        RecordTransferHistory($"{target.PlayerName}: {outcomeState}; market score {marketScore}; agent {target.AgentMood}; rival {target.RivalInterest}; board {target.BoardStance}; Director {target.DirectorStance}.");
        RecordPerceptionHistory("Recruitment decision", $"approved {approved}; board trust {CareerProfile.BoardTrust}, Director trust {CareerProfile.DirectorTrust}, transfer pressure {TransferPressure}, recruitment reputation {RecruitmentReputation}; market score {marketScore}");
        AddNews(
            approved ? "Transfer approach approved" : "Transfer approach blocked",
            NewsCategory.Transfer,
            "Club sources",
            $"{target.PlayerName}: {status}",
            approved ? 4 : 5,
            sourceType: "Recruitment desk",
            relatedEntity: target.PlayerName,
            effectSummary: $"Outcome {outcomeState}; transfer pressure {TransferPressure}; recruitment reputation {RecruitmentReputation}.",
            cooldownKey: "transfer-approach");
        return status;
    }

    public string AttemptBasicContractNegotiation()
    {
        EnsureContractOffers();
        if (CurrentTransferContractOffer == null || CurrentRenewalContractOffer == null)
        {
            return "Contract offer unavailable.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            CurrentTransferContractOffer = CloneContractOffer(
                CurrentTransferContractOffer,
                "Recommended",
                "Assistant Manager recommended terms; final authority remains with senior staff.",
                "Terms recommended, not offered.");
            CurrentRenewalContractOffer = CloneContractOffer(
                CurrentRenewalContractOffer,
                "Recommended",
                "Assistant Manager recommended a renewal structure; senior staff must approve.",
                "Renewal recommended, not offered.");
            RecordContractHistory("Assistant Manager recommended transfer and renewal terms without authority to offer.");
            AddNews(
                "Contract recommendation filed",
                NewsCategory.Contract,
                "Internal",
                $"{ManagerName} recommended contract terms, but role authority prevents formal negotiation.",
                2);
            return "Assistant Manager contract recommendation logged; no formal wage or role promise was made.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach)
        {
            CurrentTransferContractOffer = CloneContractOffer(
                CurrentTransferContractOffer,
                "Requested",
                "Head Coach requested contract terms; board and Director review required.",
                "Terms requested for review.");
            CurrentRenewalContractOffer = CloneContractOffer(
                CurrentRenewalContractOffer,
                "Requested",
                "Head Coach requested renewal talks; board wage control still applies.",
                "Renewal requested for review.");
            TransferPressure = Math.Clamp(TransferPressure + 2, 0, 100);
            RefreshPressureCategories();
            RecordContractHistory("Head Coach requested contract and renewal reviews; board/Director approval pending.");
            AddNews(
                "Contract request submitted",
                NewsCategory.Contract,
                "Internal",
                $"{ManagerName} requested contract and renewal talks; final terms remain above Head Coach authority.",
                3);
            return "Head Coach contract request submitted; board and Director approval required.";
        }

        var transferResult = ResolveContractOffer(CurrentTransferContractOffer);
        CurrentTransferContractOffer = transferResult.Offer;
        var renewalResult = ResolveContractOffer(CurrentRenewalContractOffer);
        CurrentRenewalContractOffer = renewalResult.Offer;
        var financialDelta = transferResult.FinancialDelta + renewalResult.FinancialDelta;
        TransferPressure = Math.Clamp(TransferPressure + transferResult.PressureDelta + renewalResult.PressureDelta, 0, 100);
        FinancialPressure = Math.Clamp(FinancialPressure + financialDelta, 0, 100);
        RecruitmentReputation = Math.Clamp(RecruitmentReputation + transferResult.ReputationDelta, 0, 100);
        CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + renewalResult.TrustDelta, 0, 100);
        RefreshPressureCategories();
        FinancialPressure = Math.Clamp(FinancialPressure + financialDelta, 0, 100);
        ApplyDirectorContractInfluence(CurrentTransferContractOffer, CurrentRenewalContractOffer);
        ApplyContractFinanceImpact(CurrentTransferContractOffer);
        ApplyContractFinanceImpact(CurrentRenewalContractOffer);
        RecordContractHistory($"{CurrentTransferContractOffer.PlayerName}: {CurrentTransferContractOffer.Status}; {CurrentTransferContractOffer.OutcomeSummary}");
        RecordContractHistory($"{CurrentRenewalContractOffer.PlayerName}: {CurrentRenewalContractOffer.Status}; {CurrentRenewalContractOffer.OutcomeSummary}");
        AddContractPromiseIfAccepted(CurrentTransferContractOffer);
        AddContractPromiseIfAccepted(CurrentRenewalContractOffer);
        AddNews(
            "Contract talks updated",
            NewsCategory.Contract,
            "Agent briefing",
            $"{CurrentTransferContractOffer.PlayerName}: {CurrentTransferContractOffer.Status}. {CurrentRenewalContractOffer.PlayerName}: {CurrentRenewalContractOffer.Status}.",
            4,
            sourceType: "Agent briefing",
            relatedEntity: $"{CurrentTransferContractOffer.PlayerName}; {CurrentRenewalContractOffer.PlayerName}",
            effectSummary: $"Transfer pressure {TransferPressure}; financial pressure {FinancialPressure}; player trust {CareerProfile.PlayerTrust}.",
            cooldownKey: "contract-negotiation");
        return $"Contract negotiation resolved: {CurrentTransferContractOffer.PlayerName} {CurrentTransferContractOffer.Status}; {CurrentRenewalContractOffer.PlayerName} {CurrentRenewalContractOffer.Status}.";
    }

    public string AttemptStaffMarketAction()
    {
        EnsureStaffImpactState();
        if (CurrentStaffMarketCandidate == null)
        {
            return "Staff market candidate unavailable.";
        }

        var candidate = CurrentStaffMarketCandidate;
        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            CurrentStaffMarketCandidate = CloneStaffMarketCandidate(candidate, "Recommended", "Assistant Manager recommended the staff upgrade; no hiring authority.");
            RecordStaffHistory($"{candidate.Name}: recommended by Assistant Manager; hiring authority sits above role.");
            AddNews(
                "Staff recommendation filed",
                NewsCategory.Club,
                "Internal",
                $"{ManagerName} recommended {candidate.Name} for {CareerFoundation.GetDisplayName(candidate.Role)}.",
                2);
            return "Assistant Manager staff recommendation logged; no hire made.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach)
        {
            CurrentStaffMarketCandidate = CloneStaffMarketCandidate(candidate, "Requested", "Head Coach requested staff change; board approval required.");
            CareerProfile.StaffTrust = Math.Clamp(CareerProfile.StaffTrust + 1, 0, 100);
            RecordStaffHistory($"{candidate.Name}: staff change requested by Head Coach; board approval pending.");
            AddNews(
                "Staff change requested",
                NewsCategory.Club,
                "Internal",
                $"{ManagerName} requested {candidate.Name}; staff authority remains limited.",
                3);
            return "Head Coach staff request submitted; board approval required.";
        }

        var approvalScore = BuildStaffHiringApprovalScore(candidate);
        if (approvalScore < 50)
        {
            CurrentStaffMarketCandidate = CloneStaffMarketCandidate(candidate, "Board rejected", $"Board rejected the staff hire: wage, reputation, and trust score {approvalScore}/100 do not align.");
            FinancialPressure = Math.Clamp(FinancialPressure + 2, 0, 100);
            RecordStaffHistory($"{candidate.Name}: board rejected staff hire; score {approvalScore}/100.");
            AddNews(
                "Staff hire blocked",
                NewsCategory.Club,
                "Board report",
                $"{candidate.Name} was rejected as a staff hire because board approval was insufficient.",
                4);
            return CurrentStaffMarketCandidate.OutcomeSummary;
        }

        HireStaffCandidate(candidate);
        ApplyStaffFinanceImpact(candidate);
        CurrentStaffMarketCandidate = CloneStaffMarketCandidate(candidate, "Hired", $"Board approved staff hire with score {approvalScore}/100; staff reports and quality updated.");
        CareerProfile.StaffTrust = Math.Clamp(CareerProfile.StaffTrust + 2, 0, 100);
        FinancialPressure = Math.Clamp(FinancialPressure + 2, 0, 100);
        StaffReportSummary = BuildStaffReportSummary();
        StaffMarketSummary = $"Hired {candidate.Name} as {CareerFoundation.GetDisplayName(candidate.Role)}; wage impact {candidate.Wage}/w.";
        RecordStaffHistory($"{candidate.Name}: hired as {CareerFoundation.GetDisplayName(candidate.Role)}; quality {candidate.Quality}, wage {candidate.Wage}/w, approval {approvalScore}/100.");
        AddNews(
            "Staff hire completed",
            NewsCategory.Club,
            "Club announcement",
            $"{SelectedClubName} hired {candidate.Name} as {CareerFoundation.GetDisplayName(candidate.Role)}.",
            4,
            sourceType: "Club announcement",
            relatedEntity: candidate.Name,
            effectSummary: $"Staff trust {CareerProfile.StaffTrust}; financial pressure {FinancialPressure}; {StaffReportSummary}",
            cooldownKey: "staff-market");
        return CurrentStaffMarketCandidate.OutcomeSummary;
    }

    public string AdvanceYouthAcademyAction()
    {
        EnsureYouthAcademyState();
        if (_youthProspects.Count == 0)
        {
            GenerateYouthIntake();
            return "Youth intake generated; prospects are available for academy review.";
        }

        return PromoteYouthProspect();
    }

    public void GenerateJobMarketEvent()
    {
        var otherClub = ResolveDifferentClub(SelectedClubName);
        if (string.IsNullOrWhiteSpace(otherClub))
        {
            otherClub = "Riverton Athletic";
        }

        var offerType = CareerProfile.Role switch
        {
            ManagerRole.AssistantManager => JobOfferType.InterimManagerOffer,
            ManagerRole.HeadCoach => JobOfferType.ManagerOffer,
            _ => JobPressure >= 70 ? JobOfferType.EmergencyApproach : JobOfferType.InterviewInvitation
        };
        var roleName = offerType switch
        {
            JobOfferType.InterimManagerOffer => "Interim Manager",
            JobOfferType.ManagerOffer => "Manager",
            JobOfferType.EmergencyApproach => "Manager",
            _ => CareerFoundation.GetDisplayName(CareerProfile.Role)
        };
        CurrentJobOffer = new JobOfferEvent
        {
            OfferType = offerType,
            ClubName = otherClub,
            RoleName = roleName,
            InterestSummary = $"Reputation {WorldReputation} and license {LicenseName} make this plausible, not guaranteed.",
            Reason = JobPressure >= 70
                ? "Market interest is cautious because pressure is high at the current club."
                : "Market interest follows stable early-career visibility."
        };
        AddNews(
            "Job market movement",
            NewsCategory.Career,
            "Agent briefing",
            $"{otherClub} register {StageFoundationText.GetDisplayName(offerType).ToLowerInvariant()} interest in {ManagerName}.",
            4);
    }

    public SaveSlotStageFoundationData BuildStageFoundationSaveData()
    {
        return new SaveSlotStageFoundationData
        {
            TeamStyleName = TeamStyleName,
            PassingDirectness = PassingDirectness,
            DefensiveLine = DefensiveLine,
            Tackling = Tackling,
            TacticalFamiliarityScore = TacticalFamiliarityScore,
            TeamInstructionsSummary = TeamInstructionsSummary,
            PlayerRolesSummary = PlayerRolesSummary,
            PlayerInstructionsSummary = PlayerInstructionsSummary,
            TacticalRoleFitScore = TacticalRoleFitScore,
            TacticalRoleFitSummary = TacticalRoleFitSummary,
            PlayerFamiliaritySummary = PlayerFamiliaritySummary,
            SetPieceApproachName = SetPieceApproachName,
            SetPieceSummary = SetPieceSummary,
            OpponentPreparationFocusName = OpponentPreparationFocusName,
            OpponentPreparationSummary = OpponentPreparationSummary,
            TacticalFitNotes = TacticalFitNotes,
            TacticalRiskNotes = TacticalRiskNotes,
            TrainingFocusName = TrainingFocusName,
            TrainingIntensityName = TrainingIntensityName,
            TrainingStatusSummary = TrainingStatusSummary,
            ScoutingReportDepthName = ScoutingReportDepthName,
            ScoutingAssignment = CurrentScoutingAssignment == null
                ? null
                : new SaveSlotScoutingAssignmentData
                {
                    Target = CurrentScoutingAssignment.Target,
                    DaysRemaining = CurrentScoutingAssignment.DaysRemaining,
                    ReportQuality = CurrentScoutingAssignment.ReportQuality,
                    DiscoverySummary = CurrentScoutingAssignment.DiscoverySummary,
                    ReportReady = CurrentScoutingAssignment.ReportReady
                },
            NewsEvents = Array.ConvertAll(
                _foundationNewsEvents.ToArray(),
                newsEvent => new SaveSlotNewsEventData
                {
                    Title = newsEvent.Title,
                    CategoryName = StageFoundationText.GetDisplayName(newsEvent.Category),
                    Reliability = newsEvent.Reliability,
                    Text = newsEvent.Text,
                    Importance = newsEvent.Importance,
                    SourceType = newsEvent.SourceType,
                    RelatedEntity = newsEvent.RelatedEntity,
                    EffectSummary = newsEvent.EffectSummary,
                    CooldownKey = newsEvent.CooldownKey
                }),
            ActiveDecisionEvents = Array.ConvertAll(_activeDecisionEvents.ToArray(), BuildDecisionEventSaveData),
            ResolvedDecisionEvents = Array.ConvertAll(_resolvedDecisionEvents.ToArray(), BuildDecisionEventSaveData),
            RecruitmentTarget = CurrentRecruitmentTarget == null
                ? null
                : BuildRecruitmentTargetSaveData(CurrentRecruitmentTarget),
            RecruitmentShortlist = Array.ConvertAll(_recruitmentShortlist.ToArray(), BuildRecruitmentTargetSaveData),
            TransferHistory = _transferHistory.ToArray(),
            TransferContractOffer = CurrentTransferContractOffer == null ? null : BuildContractOfferSaveData(CurrentTransferContractOffer),
            RenewalContractOffer = CurrentRenewalContractOffer == null ? null : BuildContractOfferSaveData(CurrentRenewalContractOffer),
            ContractHistory = _contractHistory.ToArray(),
            PromiseRecords = Array.ConvertAll(
                _promiseRecords.ToArray(),
                promise => new SaveSlotPromiseRecordData
                {
                    PromiseType = promise.PromiseType,
                    Recipient = promise.Recipient,
                    Source = promise.Source,
                    IsPublic = promise.IsPublic,
                    ExpectedAction = promise.ExpectedAction,
                    DeadlineSummary = promise.DeadlineSummary,
                    DaysRemaining = promise.DaysRemaining,
                    StatusName = StageFoundationText.GetDisplayName(promise.Status),
                    CurrentEvidence = promise.CurrentEvidence,
                    AgentMood = promise.AgentMood,
                    ConsequenceRisk = promise.ConsequenceRisk
                }),
            JobSecurityName = JobSecurityName,
            JobOffer = CurrentJobOffer == null
                ? null
                : new SaveSlotJobOfferEventData
                {
                    OfferTypeName = StageFoundationText.GetDisplayName(CurrentJobOffer.OfferType),
                    ClubName = CurrentJobOffer.ClubName,
                    RoleName = CurrentJobOffer.RoleName,
                    InterestSummary = CurrentJobOffer.InterestSummary,
                    Reason = CurrentJobOffer.Reason
                },
            CareerHistory = _careerHistory.ToArray(),
            LicenseOpportunitySummary = LicenseOpportunitySummary,
            ObjectiveReviewSummary = ObjectiveReviewSummary,
            FanTrust = FanTrust,
            MediaTrust = MediaTrust,
            WorldReputation = WorldReputation,
            ClubReputation = ClubReputation,
            MediaReputation = MediaReputation,
            TacticalReputation = TacticalReputation,
            YouthReputation = YouthReputation,
            RecruitmentReputation = RecruitmentReputation,
            BoardPressure = BoardPressure,
            FanPressure = FanPressure,
            DressingRoomPressure = DressingRoomPressure,
            TransferPressure = TransferPressure,
            FinancialPressure = FinancialPressure,
            PerceptionHistory = _perceptionHistory.ToArray(),
            DirectorCooperation = DirectorCooperation,
            DirectorConflict = DirectorConflict,
            DirectorScoutingPriority = DirectorScoutingPriority,
            DirectorTransferPreference = DirectorTransferPreference,
            DirectorSalesPressureSummary = DirectorSalesPressureSummary,
            DirectorBoardReportSummary = DirectorBoardReportSummary,
            DirectorActionHistory = _directorActionHistory.ToArray(),
            StaffMarketCandidate = CurrentStaffMarketCandidate == null ? null : BuildStaffMarketCandidateSaveData(CurrentStaffMarketCandidate),
            StaffReportSummary = StaffReportSummary,
            StaffMarketSummary = StaffMarketSummary,
            StaffHistory = _staffHistory.ToArray(),
            YouthAcademyQuality = YouthAcademyQuality,
            YouthRecruitmentReach = YouthRecruitmentReach,
            YouthCoachingQuality = YouthCoachingQuality,
            YouthFacilitiesSummary = YouthFacilitiesSummary,
            YouthIntakeDateSummary = YouthIntakeDateSummary,
            YouthBoardExpectation = YouthBoardExpectation,
            YouthFanExpectation = YouthFanExpectation,
            YouthProspects = Array.ConvertAll(_youthProspects.ToArray(), BuildYouthProspectSaveData),
            YouthHistory = _youthHistory.ToArray(),
            PlayerDevelopmentSummary = PlayerDevelopmentSummary,
            PlayerDevelopmentHistory = _playerDevelopmentHistory.ToArray(),
            FinanceTransferBudgetRemaining = FinanceTransferBudgetRemaining,
            FinanceWageBudget = FinanceWageBudget,
            FinanceCurrentWageBill = FinanceCurrentWageBill,
            FinanceTransferCommitments = FinanceTransferCommitments,
            FinanceDebt = FinanceDebt,
            FinanceRevenue = FinanceRevenue,
            FinanceExpenses = FinanceExpenses,
            FinanceProjectedBalance = FinanceProjectedBalance,
            FinanceTicketIncome = FinanceTicketIncome,
            FinanceCommercialIncome = FinanceCommercialIncome,
            FinancePrizeMoney = FinancePrizeMoney,
            FinanceBoardInjection = FinanceBoardInjection,
            FinanceBudgetCut = FinanceBudgetCut,
            WageStructurePressure = WageStructurePressure,
            FinanceSummary = FinanceSummary,
            ProfitExpectationSummary = ProfitExpectationSummary,
            BoardFinanceActionSummary = BoardFinanceActionSummary,
            FinanceHistory = _financeHistory.ToArray()
        };
    }

    public void RestoreStageFoundationState(SaveSlotStageFoundationData? data)
    {
        if (data == null)
        {
            InitializeStageFoundationsForClub();
            return;
        }

        TeamStyle = StageFoundationText.ParseTeamStyle(data.TeamStyleName);
        PassingDirectness = data.PassingDirectness <= 0 ? 52 : data.PassingDirectness;
        DefensiveLine = data.DefensiveLine <= 0 ? 55 : data.DefensiveLine;
        Tackling = data.Tackling <= 0 ? 52 : data.Tackling;
        TacticalFamiliarityScore = Math.Clamp(data.TacticalFamiliarityScore <= 0 ? 58 : data.TacticalFamiliarityScore, 0, 100);
        TeamInstructionsSummary = string.IsNullOrWhiteSpace(data.TeamInstructionsSummary) ? "Tactic state restored with default instructions." : data.TeamInstructionsSummary;
        PlayerRolesSummary = string.IsNullOrWhiteSpace(data.PlayerRolesSummary) ? "Player roles restored from saved tactic state." : data.PlayerRolesSummary;
        PlayerInstructionsSummary = string.IsNullOrWhiteSpace(data.PlayerInstructionsSummary) ? "Player instructions restored from saved tactic state." : data.PlayerInstructionsSummary;
        TacticalRoleFitScore = Math.Clamp(data.TacticalRoleFitScore <= 0 ? 60 : data.TacticalRoleFitScore, 0, 100);
        TacticalRoleFitSummary = string.IsNullOrWhiteSpace(data.TacticalRoleFitSummary) ? "Role fit restored from saved tactic state." : data.TacticalRoleFitSummary;
        PlayerFamiliaritySummary = string.IsNullOrWhiteSpace(data.PlayerFamiliaritySummary) ? "Player familiarity restored from saved tactic state." : data.PlayerFamiliaritySummary;
        SetPieceApproach = StageFoundationText.ParseSetPieceApproach(data.SetPieceApproachName);
        SetPieceSummary = string.IsNullOrWhiteSpace(data.SetPieceSummary) ? TacticsFoundation.BuildSetPieceSummary(SetPieceApproach, CurrentTrainingFocus) : data.SetPieceSummary;
        CurrentOpponentPreparationFocus = StageFoundationText.ParseOpponentPreparationFocus(data.OpponentPreparationFocusName);
        OpponentPreparationSummary = string.IsNullOrWhiteSpace(data.OpponentPreparationSummary) ? TacticsFoundation.BuildOpponentPreparationSummary(CurrentOpponentPreparationFocus, CurrentOpponentName) : data.OpponentPreparationSummary;
        TacticalFitNotes = string.IsNullOrWhiteSpace(data.TacticalFitNotes) ? "Fit notes restored from saved tactic state." : data.TacticalFitNotes;
        TacticalRiskNotes = string.IsNullOrWhiteSpace(data.TacticalRiskNotes) ? "Risk notes restored from saved tactic state." : data.TacticalRiskNotes;
        CurrentTrainingFocus = StageFoundationText.ParseTrainingFocus(data.TrainingFocusName);
        CurrentTrainingIntensity = StageFoundationText.ParseTrainingIntensity(data.TrainingIntensityName);
        TrainingStatusSummary = string.IsNullOrWhiteSpace(data.TrainingStatusSummary) ? "Training state restored." : data.TrainingStatusSummary;
        CurrentScoutingReportDepth = StageFoundationText.ParseScoutingReportDepth(data.ScoutingReportDepthName);
        CurrentScoutingAssignment = data.ScoutingAssignment == null
            ? null
            : new ScoutingAssignment
            {
                Target = data.ScoutingAssignment.Target,
                DaysRemaining = data.ScoutingAssignment.DaysRemaining,
                ReportQuality = data.ScoutingAssignment.ReportQuality,
                DiscoverySummary = data.ScoutingAssignment.DiscoverySummary,
                ReportReady = data.ScoutingAssignment.ReportReady
            };

        _foundationNewsEvents.Clear();
        if (data.NewsEvents != null)
        {
            foreach (var newsEvent in data.NewsEvents)
            {
                _foundationNewsEvents.Add(new NewsEvent
                {
                    Title = string.IsNullOrWhiteSpace(newsEvent.Title) ? "Saved news" : newsEvent.Title,
                    Category = StageFoundationText.ParseNewsCategory(newsEvent.CategoryName),
                    Reliability = string.IsNullOrWhiteSpace(newsEvent.Reliability) ? "Confirmed" : newsEvent.Reliability,
                    Text = string.IsNullOrWhiteSpace(newsEvent.Text) ? "Saved news text unavailable." : newsEvent.Text,
                    Importance = newsEvent.Importance,
                    SourceType = string.IsNullOrWhiteSpace(newsEvent.SourceType) ? "Club source" : newsEvent.SourceType,
                    RelatedEntity = newsEvent.RelatedEntity,
                    EffectSummary = newsEvent.EffectSummary,
                    CooldownKey = newsEvent.CooldownKey
                });
            }
        }

        _activeDecisionEvents.Clear();
        if (data.ActiveDecisionEvents != null)
        {
            foreach (var decisionEvent in data.ActiveDecisionEvents)
            {
                _activeDecisionEvents.Add(RestoreDecisionEvent(decisionEvent, false));
            }
        }

        _resolvedDecisionEvents.Clear();
        if (data.ResolvedDecisionEvents != null)
        {
            foreach (var decisionEvent in data.ResolvedDecisionEvents)
            {
                _resolvedDecisionEvents.Add(RestoreDecisionEvent(decisionEvent, true));
            }
        }

        CurrentRecruitmentTarget = data.RecruitmentTarget == null
            ? null
            : RestoreRecruitmentTarget(data.RecruitmentTarget);
        _recruitmentShortlist.Clear();
        if (data.RecruitmentShortlist != null)
        {
            foreach (var target in data.RecruitmentShortlist)
            {
                _recruitmentShortlist.Add(RestoreRecruitmentTarget(target));
            }
        }

        _transferHistory.Clear();
        if (data.TransferHistory != null)
        {
            _transferHistory.AddRange(data.TransferHistory);
        }

        CurrentTransferContractOffer = data.TransferContractOffer == null
            ? null
            : RestoreContractOffer(data.TransferContractOffer);
        CurrentRenewalContractOffer = data.RenewalContractOffer == null
            ? null
            : RestoreContractOffer(data.RenewalContractOffer);
        _contractHistory.Clear();
        if (data.ContractHistory != null)
        {
            _contractHistory.AddRange(data.ContractHistory);
        }

        _promiseRecords.Clear();
        if (data.PromiseRecords != null)
        {
            foreach (var promise in data.PromiseRecords)
            {
                _promiseRecords.Add(new PromiseRecord
                {
                    PromiseType = string.IsNullOrWhiteSpace(promise.PromiseType) ? "Squad role" : promise.PromiseType,
                    Recipient = string.IsNullOrWhiteSpace(promise.Recipient) ? "Unknown player" : promise.Recipient,
                    Source = string.IsNullOrWhiteSpace(promise.Source) ? "Saved career" : promise.Source,
                    IsPublic = promise.IsPublic,
                    ExpectedAction = string.IsNullOrWhiteSpace(promise.ExpectedAction) ? "Keep the promised pathway credible." : promise.ExpectedAction,
                    DeadlineSummary = string.IsNullOrWhiteSpace(promise.DeadlineSummary) ? "Next promise review" : promise.DeadlineSummary,
                    DaysRemaining = promise.DaysRemaining <= 0 ? 14 : promise.DaysRemaining,
                    Status = StageFoundationText.ParsePromiseStatus(promise.StatusName),
                    CurrentEvidence = string.IsNullOrWhiteSpace(promise.CurrentEvidence) ? "Saved promise awaits next review." : promise.CurrentEvidence,
                    AgentMood = string.IsNullOrWhiteSpace(promise.AgentMood) ? "Neutral" : promise.AgentMood,
                    ConsequenceRisk = string.IsNullOrWhiteSpace(promise.ConsequenceRisk) ? "Trust and morale may move at review." : promise.ConsequenceRisk
                });
            }
        }

        JobSecurity = StageFoundationText.ParseJobSecurity(data.JobSecurityName);
        CurrentJobOffer = data.JobOffer == null
            ? null
            : new JobOfferEvent
            {
                OfferType = StageFoundationText.ParseJobOfferType(data.JobOffer.OfferTypeName),
                ClubName = data.JobOffer.ClubName,
                RoleName = data.JobOffer.RoleName,
                InterestSummary = data.JobOffer.InterestSummary,
                Reason = data.JobOffer.Reason
            };
        _careerHistory.Clear();
        if (data.CareerHistory != null)
        {
            _careerHistory.AddRange(data.CareerHistory);
        }

        LicenseOpportunitySummary = string.IsNullOrWhiteSpace(data.LicenseOpportunitySummary) ? "License progression will be reviewed after sustained progress." : data.LicenseOpportunitySummary;
        ObjectiveReviewSummary = string.IsNullOrWhiteSpace(data.ObjectiveReviewSummary) ? "Objective review restored." : data.ObjectiveReviewSummary;
        FanTrust = Math.Clamp(data.FanTrust <= 0 ? 55 : data.FanTrust, 0, 100);
        MediaTrust = Math.Clamp(data.MediaTrust <= 0 ? 52 : data.MediaTrust, 0, 100);
        WorldReputation = Math.Clamp(data.WorldReputation <= 0 ? CareerProfile.Reputation : data.WorldReputation, 0, 100);
        ClubReputation = Math.Clamp(data.ClubReputation <= 0 ? WorldReputation : data.ClubReputation, 0, 100);
        MediaReputation = Math.Clamp(data.MediaReputation <= 0 ? WorldReputation : data.MediaReputation, 0, 100);
        TacticalReputation = Math.Clamp(data.TacticalReputation <= 0 ? 45 : data.TacticalReputation, 0, 100);
        YouthReputation = Math.Clamp(data.YouthReputation <= 0 ? 45 : data.YouthReputation, 0, 100);
        RecruitmentReputation = Math.Clamp(data.RecruitmentReputation <= 0 ? 45 : data.RecruitmentReputation, 0, 100);
        BoardPressure = Math.Clamp(data.BoardPressure <= 0 ? 35 : data.BoardPressure, 0, 100);
        FanPressure = Math.Clamp(data.FanPressure <= 0 ? 35 : data.FanPressure, 0, 100);
        DressingRoomPressure = Math.Clamp(data.DressingRoomPressure <= 0 ? 35 : data.DressingRoomPressure, 0, 100);
        TransferPressure = Math.Clamp(data.TransferPressure <= 0 ? 25 : data.TransferPressure, 0, 100);
        FinancialPressure = Math.Clamp(data.FinancialPressure <= 0 ? 25 : data.FinancialPressure, 0, 100);
        _perceptionHistory.Clear();
        if (data.PerceptionHistory != null)
        {
            _perceptionHistory.AddRange(data.PerceptionHistory);
        }

        DirectorCooperation = Math.Clamp(data.DirectorCooperation <= 0 ? BuildInitialDirectorCooperation() : data.DirectorCooperation, 0, 100);
        DirectorConflict = Math.Clamp(data.DirectorConflict <= 0 ? BuildInitialDirectorConflict() : data.DirectorConflict, 0, 100);
        DirectorScoutingPriority = string.IsNullOrWhiteSpace(data.DirectorScoutingPriority) ? BuildDirectorScoutingPriority() : data.DirectorScoutingPriority;
        DirectorTransferPreference = string.IsNullOrWhiteSpace(data.DirectorTransferPreference) ? BuildDirectorTransferPreference() : data.DirectorTransferPreference;
        DirectorSalesPressureSummary = string.IsNullOrWhiteSpace(data.DirectorSalesPressureSummary) ? BuildDirectorSalesPressureSummary() : data.DirectorSalesPressureSummary;
        DirectorBoardReportSummary = string.IsNullOrWhiteSpace(data.DirectorBoardReportSummary) ? BuildDirectorBoardReportSummary("Saved state restored") : data.DirectorBoardReportSummary;
        _directorActionHistory.Clear();
        if (data.DirectorActionHistory != null)
        {
            _directorActionHistory.AddRange(data.DirectorActionHistory);
        }

        CurrentStaffMarketCandidate = data.StaffMarketCandidate == null
            ? null
            : RestoreStaffMarketCandidate(data.StaffMarketCandidate);
        StaffReportSummary = string.IsNullOrWhiteSpace(data.StaffReportSummary) ? BuildStaffReportSummary() : data.StaffReportSummary;
        StaffMarketSummary = string.IsNullOrWhiteSpace(data.StaffMarketSummary) ? "Staff market restored; no action yet." : data.StaffMarketSummary;
        _staffHistory.Clear();
        if (data.StaffHistory != null)
        {
            _staffHistory.AddRange(data.StaffHistory);
        }

        YouthAcademyQuality = Math.Clamp(data.YouthAcademyQuality <= 0 ? BuildYouthAcademyQuality() : data.YouthAcademyQuality, 0, 100);
        YouthRecruitmentReach = Math.Clamp(data.YouthRecruitmentReach <= 0 ? BuildYouthRecruitmentReach() : data.YouthRecruitmentReach, 0, 100);
        YouthCoachingQuality = Math.Clamp(data.YouthCoachingQuality <= 0 ? GetStaffQuality(StaffRole.YouthCoach) : data.YouthCoachingQuality, 0, 100);
        YouthFacilitiesSummary = string.IsNullOrWhiteSpace(data.YouthFacilitiesSummary) ? BuildYouthFacilitiesSummary() : data.YouthFacilitiesSummary;
        YouthIntakeDateSummary = string.IsNullOrWhiteSpace(data.YouthIntakeDateSummary) ? BuildYouthIntakeDateSummary() : data.YouthIntakeDateSummary;
        YouthBoardExpectation = string.IsNullOrWhiteSpace(data.YouthBoardExpectation) ? BuildYouthBoardExpectation() : data.YouthBoardExpectation;
        YouthFanExpectation = string.IsNullOrWhiteSpace(data.YouthFanExpectation) ? BuildYouthFanExpectation() : data.YouthFanExpectation;
        _youthProspects.Clear();
        if (data.YouthProspects != null)
        {
            foreach (var prospect in data.YouthProspects)
            {
                _youthProspects.Add(RestoreYouthProspect(prospect));
            }
        }

        _youthHistory.Clear();
        if (data.YouthHistory != null)
        {
            _youthHistory.AddRange(data.YouthHistory);
        }

        PlayerDevelopmentSummary = string.IsNullOrWhiteSpace(data.PlayerDevelopmentSummary) ? "Player development cadence restored; weekly history pending." : data.PlayerDevelopmentSummary;
        _playerDevelopmentHistory.Clear();
        if (data.PlayerDevelopmentHistory != null)
        {
            _playerDevelopmentHistory.AddRange(data.PlayerDevelopmentHistory);
        }

        FinanceTransferBudgetRemaining = data.FinanceTransferBudgetRemaining;
        FinanceWageBudget = data.FinanceWageBudget;
        FinanceCurrentWageBill = data.FinanceCurrentWageBill;
        FinanceTransferCommitments = data.FinanceTransferCommitments;
        FinanceDebt = data.FinanceDebt;
        FinanceRevenue = data.FinanceRevenue;
        FinanceExpenses = data.FinanceExpenses;
        FinanceProjectedBalance = data.FinanceProjectedBalance;
        FinanceTicketIncome = data.FinanceTicketIncome;
        FinanceCommercialIncome = data.FinanceCommercialIncome;
        FinancePrizeMoney = data.FinancePrizeMoney;
        FinanceBoardInjection = data.FinanceBoardInjection;
        FinanceBudgetCut = data.FinanceBudgetCut;
        WageStructurePressure = data.WageStructurePressure;
        FinanceSummary = string.IsNullOrWhiteSpace(data.FinanceSummary) ? "Finance foundation restored; current figures pending refresh." : data.FinanceSummary;
        ProfitExpectationSummary = string.IsNullOrWhiteSpace(data.ProfitExpectationSummary) ? "Profit expectation restored from club profile." : data.ProfitExpectationSummary;
        BoardFinanceActionSummary = string.IsNullOrWhiteSpace(data.BoardFinanceActionSummary) ? "Board finance action restored with no current intervention." : data.BoardFinanceActionSummary;
        _financeHistory.Clear();
        if (data.FinanceHistory != null)
        {
            _financeHistory.AddRange(data.FinanceHistory);
        }

        EnsureFinanceState();
        EnsureRecruitmentTarget();
        EnsureJobMarketFoundation();
        RefreshPressureCategories();
    }

    public void ResetStageFoundations()
    {
        TeamStyle = TacticalTeamStyle.Balanced;
        PassingDirectness = 52;
        DefensiveLine = 55;
        Tackling = 52;
        TacticalFamiliarityScore = 58;
        TeamInstructionsSummary = "Balanced tactical foundation.";
        PlayerRolesSummary = "Player roles pending until a squad is selected.";
        PlayerInstructionsSummary = "Player instructions pending until tactics are selected.";
        TacticalRoleFitScore = 60;
        TacticalRoleFitSummary = "Role fit pending.";
        PlayerFamiliaritySummary = "Player familiarity pending.";
        SetPieceApproach = TacticalSetPieceApproach.BalancedSetPieces;
        SetPieceSummary = "Set-piece approach pending.";
        CurrentOpponentPreparationFocus = OpponentPreparationFocus.BalancedBrief;
        OpponentPreparationSummary = "Opponent preparation pending.";
        TacticalFitNotes = "Fit notes pending.";
        TacticalRiskNotes = "Risk notes pending.";
        CurrentTrainingFocus = TrainingFocus.TeamCohesion;
        CurrentTrainingIntensity = TrainingIntensity.Standard;
        CurrentScoutingReportDepth = ScoutingReportDepth.StandardReport;
        TrainingStatusSummary = "Training foundation ready: team cohesion is the default weekly focus.";
        CurrentScoutingAssignment = null;
        CurrentRecruitmentTarget = null;
        CurrentTransferContractOffer = null;
        CurrentRenewalContractOffer = null;
        JobSecurity = JobSecurityState.Stable;
        CurrentJobOffer = null;
        LicenseOpportunitySummary = "License progression will be reviewed after sustained progress.";
        ObjectiveReviewSummary = "Objective review pending first run of matches.";
        FanTrust = 55;
        MediaTrust = 52;
        WorldReputation = CareerProfile.Reputation;
        ClubReputation = CareerProfile.Reputation;
        MediaReputation = CareerProfile.Reputation;
        TacticalReputation = 45;
        YouthReputation = 45;
        RecruitmentReputation = 45;
        BoardPressure = 35;
        FanPressure = 35;
        DressingRoomPressure = 35;
        TransferPressure = 25;
        FinancialPressure = 25;
        DirectorCooperation = 55;
        DirectorConflict = 25;
        DirectorScoutingPriority = "Director scouting priority pending.";
        DirectorTransferPreference = "Director transfer preference pending.";
        DirectorSalesPressureSummary = "Director sales pressure pending.";
        DirectorBoardReportSummary = "Director board report pending.";
        CurrentStaffMarketCandidate = null;
        StaffReportSummary = "Staff reports pending.";
        StaffMarketSummary = "Staff market pending.";
        YouthAcademyQuality = 0;
        YouthRecruitmentReach = 0;
        YouthCoachingQuality = 0;
        YouthFacilitiesSummary = "Youth facilities pending.";
        YouthIntakeDateSummary = "Youth intake pending.";
        YouthBoardExpectation = "Youth board expectation pending.";
        YouthFanExpectation = "Youth fan expectation pending.";
        PlayerDevelopmentSummary = "Player development cadence pending.";
        FinanceTransferBudgetRemaining = 0;
        FinanceWageBudget = 0;
        FinanceCurrentWageBill = 0;
        FinanceTransferCommitments = 0;
        FinanceDebt = 0;
        FinanceRevenue = 0;
        FinanceExpenses = 0;
        FinanceProjectedBalance = 0;
        FinanceTicketIncome = 0;
        FinanceCommercialIncome = 0;
        FinancePrizeMoney = 0;
        FinanceBoardInjection = 0;
        FinanceBudgetCut = 0;
        WageStructurePressure = 0;
        FinanceSummary = "Finance foundation pending.";
        ProfitExpectationSummary = "Profit expectation pending.";
        BoardFinanceActionSummary = "Board finance action pending.";
        _foundationNewsEvents.Clear();
        _activeDecisionEvents.Clear();
        _resolvedDecisionEvents.Clear();
        _recruitmentShortlist.Clear();
        _promiseRecords.Clear();
        _careerHistory.Clear();
        _perceptionHistory.Clear();
        _transferHistory.Clear();
        _contractHistory.Clear();
        _directorActionHistory.Clear();
        _staffHistory.Clear();
        _youthProspects.Clear();
        _youthHistory.Clear();
        _playerDevelopmentHistory.Clear();
        _financeHistory.Clear();
    }

    public void InitializeStageFoundationsForClub()
    {
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return;
        }

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            SquadPlayers[index] = PlayerIdentityFoundation.EnsureIdentity(SquadPlayers[index], SelectedClubName, WorldSeed, index);
        }

        RefreshTacticFoundation(TacticalFormation, TeamStyle);
        EnsureDirectorConflictState();
        EnsureStaffImpactState();
        EnsureYouthAcademyState();
        EnsurePlayerDevelopmentState();
        EnsureFinanceState();
        if (CurrentScoutingAssignment == null)
        {
            StartBasicScoutingAssignment("Position need: versatile midfielder");
        }

        EnsureRecruitmentTarget();
        EnsureContractOffers();
        EnsureJobMarketFoundation();
        if (_careerHistory.Count == 0)
        {
            _careerHistory.Add($"{CurrentDateLabel}: appointed {CurrentRoleName} of {SelectedClubName} with {LicenseName}.");
        }

        EvaluateCareerFoundationState();
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    public void ApplyStageFoundationPostMatch(MatchPlaybackResult result, PostMatchConsequenceResult consequence)
    {
        var goalDifference = result.FinalHomeScore - result.FinalAwayScore;
        var boardTrustDelta = ResolveSlowTrustDelta(consequence.BoardDelta);
        var playerTrustDelta = ResolveSlowTrustDelta(consequence.MoraleDelta);
        var fanTrustDelta = ResolveSlowTrustDelta(consequence.FanDelta);
        CareerProfile.BoardTrust = Math.Clamp(CareerProfile.BoardTrust + boardTrustDelta, 0, 100);
        CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + playerTrustDelta, 0, 100);
        CareerProfile.StaffTrust = Math.Clamp(CareerProfile.StaffTrust + ResolveSlowTrustDelta(consequence.MoraleDelta + (TacticalRoleFitScore >= 65 ? 1 : -1)), 0, 100);
        CareerProfile.DirectorTrust = Math.Clamp(CareerProfile.DirectorTrust + ResolveSlowTrustDelta(goalDifference >= 0 ? 2 : -2), 0, 100);
        FanTrust = Math.Clamp(FanTrust + fanTrustDelta, 0, 100);
        MediaTrust = Math.Clamp(MediaTrust + ResolveSlowTrustDelta(goalDifference >= 0 ? 1 : -2), 0, 100);
        WorldReputation = Math.Clamp(WorldReputation + (goalDifference > 0 ? 2 : goalDifference == 0 ? 0 : -1), 0, 100);
        ClubReputation = Math.Clamp(ClubReputation + Math.Sign(consequence.BoardDelta + consequence.FanDelta), 0, 100);
        MediaReputation = Math.Clamp(MediaReputation + (goalDifference > 0 ? 1 : goalDifference < 0 ? -1 : 0), 0, 100);
        TacticalReputation = Math.Clamp(TacticalReputation + ResolveTacticalReputationDelta(result, goalDifference), 0, 100);
        YouthReputation = Math.Clamp(YouthReputation + ResolveYouthReputationDelta(result), 0, 100);
        RecruitmentReputation = Math.Clamp(RecruitmentReputation + ResolveRecruitmentReputationDelta(goalDifference), 0, 100);
        CareerProfile.Reputation = WorldReputation;
        CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + (goalDifference < 0 ? 3 : -1), 0, 100);
        TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + (goalDifference >= 0 ? 2 : -1), 0, 100);
        TransferPressure = Math.Clamp(TransferPressure + (goalDifference < 0 ? 3 : -1), 0, 100);
        RefreshPressureCategories();
        RecordPerceptionHistory(
            "Post-match",
            $"morale squad {TeamMorale} ({consequence.MoraleDelta:+0;-0;0}); trust board {boardTrustDelta:+0;-0;0}, players {playerTrustDelta:+0;-0;0}, fans {fanTrustDelta:+0;-0;0}; reputation world {WorldReputation}, tactical {TacticalReputation}; pressure job {JobPressure}, board {BoardPressure}, fans {FanPressure}, dressing room {DressingRoomPressure}");
        EvaluateCareerFoundationState();
        RefreshTacticFoundation(TacticalFormation, TeamStyle);
        ObjectiveReviewSummary = BuildObjectiveReviewSummary(goalDifference);
        LicenseOpportunitySummary = BuildLicenseOpportunitySummary();
        AddNews(
            "Post-match consequences logged",
            NewsCategory.Pressure,
            "Confirmed",
            $"{result.FinalResultSummary}: morale moved quickly, trust moved slowly, and reputation/pressure categories were updated separately.",
            5);
        GenerateContextDecisionEvent("Post-match");
        ReviewPromiseLifecycle("Post-match review", 0);
    }

    public string BuildPlayerDossier(GameState.SquadPlayer player)
    {
        var report = BuildPlayerInformationReport(player, PlayerKnowledgeContext.OwnSquad);
        return $"{PlayerIdentityFoundation.BuildProfileSummary(player)}\n{report.FullSummary}\n{PlayerIdentityFoundation.BuildContractSummary(player)}\nRelationship: {player.Relationship} | Transfer: {player.TransferInterest}";
    }

    public PlayerInformationReport BuildPlayerInformationReport(GameState.SquadPlayer player, PlayerKnowledgeContext context)
    {
        var scoutQuality = GetStaffQuality(StaffRole.Scout);
        var analystQuality = GetStaffQuality(StaffRole.DataAnalyst);
        var staffQuality = context == PlayerKnowledgeContext.OwnSquad
            ? (GetStaffQuality(StaffRole.FirstTeamCoach) + GetStaffQuality(StaffRole.AssistantManager)) / 2
            : GetStaffQuality(StaffRole.HeadOfRecruitment);
        var reportQuality = context == PlayerKnowledgeContext.ScoutedTarget
            ? CurrentScoutingAssignment?.ReportQuality ?? player.ScoutingConfidence
            : player.ScoutingConfidence;
        return PlayerInformationVisibility.BuildReport(
            player,
            context,
            CareerProfile.Role,
            CareerProfile.License,
            scoutQuality,
            analystQuality,
            staffQuality,
            reportQuality);
    }

    public string BuildPlayerInformationSummary(GameState.SquadPlayer player)
    {
        return BuildPlayerInformationReport(player, PlayerKnowledgeContext.OwnSquad).FullSummary;
    }

    public string ValidateStage2PlayerIdentityContract()
    {
        InitializeStageFoundationsForClub();
        if (SquadPlayers.Length == 0)
        {
            return "No squad players available for player identity validation.";
        }

        var player = SquadPlayers[0];
        var ownedReport = BuildPlayerInformationReport(player, PlayerKnowledgeContext.OwnSquad);
        var lowVisibilityReport = PlayerInformationVisibility.BuildReport(
            player.With(playerFamiliarity: 0, scoutingConfidence: 0),
            PlayerKnowledgeContext.UnknownTarget,
            ManagerRole.AssistantManager,
            ManagerLicense.GrassrootsLicense,
            35,
            35,
            35,
            0);
        var highVisibilityReport = PlayerInformationVisibility.BuildReport(
            player.With(playerFamiliarity: 95, scoutingConfidence: 95),
            PlayerKnowledgeContext.OwnSquad,
            ManagerRole.Manager,
            ManagerLicense.ProLicense,
            90,
            90,
            90,
            95);
        if (string.IsNullOrWhiteSpace(player.KnownAttributesSummary) ||
            string.IsNullOrWhiteSpace(player.EstimatedAttributesSummary) ||
            !player.UnknownAttributesSummary.Contains("?", StringComparison.Ordinal) ||
            string.IsNullOrWhiteSpace(player.PlayingStyle) ||
            string.IsNullOrWhiteSpace(player.Traits) ||
            string.IsNullOrWhiteSpace(player.Personality) ||
            string.IsNullOrWhiteSpace(player.TacticalFit) ||
            string.IsNullOrWhiteSpace(player.KnownAttributeGroups) ||
            string.IsNullOrWhiteSpace(player.EstimatedAttributeGroups) ||
            string.IsNullOrWhiteSpace(player.UnknownAttributeGroups) ||
            ownedReport.KnowledgeScore <= 0 ||
            !lowVisibilityReport.UnknownAttributesSummary.Contains("?", StringComparison.Ordinal) ||
            lowVisibilityReport.KnownAttributesSummary.Contains("Technical", StringComparison.Ordinal) ||
            highVisibilityReport.KnownAttributesSummary.Contains("?", StringComparison.Ordinal) ||
            highVisibilityReport.KnowledgeScore <= lowVisibilityReport.KnowledgeScore ||
            player.Wage <= 0 ||
            player.ContractExpiryYear <= 0)
        {
            return "Player identity foundation is missing systemic known/estimated/unknown visibility, style, trait, personality, fit, or contract data.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for player identity validation.";
        }

        var expectedStyle = player.PlayingStyle;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (SquadPlayers.Length == 0 || SquadPlayers[0].PlayingStyle != expectedStyle)
        {
            return "Save/load did not preserve player identity state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase1InformationVisibilityContract()
    {
        InitializeStageFoundationsForClub();
        if (SquadPlayers.Length == 0)
        {
            return "No squad players available for information visibility validation.";
        }

        var player = SquadPlayers[0];
        var lowReport = PlayerInformationVisibility.BuildReport(
            player.With(playerFamiliarity: 0, scoutingConfidence: 0),
            PlayerKnowledgeContext.UnknownTarget,
            ManagerRole.AssistantManager,
            ManagerLicense.GrassrootsLicense,
            35,
            35,
            35,
            0);
        var ownedReport = BuildPlayerInformationReport(player, PlayerKnowledgeContext.OwnSquad);
        var highReport = PlayerInformationVisibility.BuildReport(
            player.With(playerFamiliarity: 95, scoutingConfidence: 95),
            PlayerKnowledgeContext.OwnSquad,
            ManagerRole.Manager,
            ManagerLicense.ProLicense,
            90,
            90,
            90,
            95);

        if (!lowReport.UnknownAttributesSummary.Contains("?", StringComparison.Ordinal) ||
            lowReport.KnownAttributesSummary.Contains("Technical", StringComparison.Ordinal) ||
            !ownedReport.KnownAttributesSummary.Contains("Known:", StringComparison.Ordinal) ||
            !ownedReport.EstimatedAttributesSummary.Contains("Estimated:", StringComparison.Ordinal) ||
            ownedReport.KnowledgeScore <= lowReport.KnowledgeScore ||
            highReport.KnowledgeScore <= lowReport.KnowledgeScore)
        {
            return "Information visibility did not produce distinct low, owned, and high-knowledge reports.";
        }

        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null ||
            !CurrentRecruitmentTarget.InformationSummary.Contains("Knowledge:", StringComparison.Ordinal) ||
            !CurrentRecruitmentTarget.InformationSummary.Contains("Unknown:", StringComparison.Ordinal))
        {
            return "Recruitment target does not use the shared information visibility summary.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for information visibility validation.";
        }

        var expectedFamiliarity = player.PlayerFamiliarity;
        var expectedConfidence = player.ScoutingConfidence;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (SquadPlayers.Length == 0 ||
            SquadPlayers[0].PlayerFamiliarity != expectedFamiliarity ||
            SquadPlayers[0].ScoutingConfidence != expectedConfidence)
        {
            return "Save/load did not preserve player familiarity and scouting confidence.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateStage3TacticsFoundationContract()
    {
        InitializeStageFoundationsForClub();
        UpdateTactics("4-2-3-1", "High Press", 78, 70, 58, 66);
        if (TeamStyle != TacticalTeamStyle.HighPress ||
            PassingDirectness <= 0 ||
            DefensiveLine <= 0 ||
            string.IsNullOrWhiteSpace(PlayerRolesSummary) ||
            string.IsNullOrWhiteSpace(TacticalFitNotes) ||
            string.IsNullOrWhiteSpace(TacticalRiskNotes))
        {
            return "Tactics foundation did not store style, instructions, roles, fit, or risk notes.";
        }

        var result = PrepareCurrentMatchResult(true);
        if (!result.TacticalSummary.Contains("High Press", StringComparison.Ordinal) ||
            !result.TacticalSummary.Contains(TacticalFamiliarityName, StringComparison.Ordinal))
        {
            return "Match simulator did not receive tactic style or familiarity input.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase4TacticalDepthContract()
    {
        InitializeStageFoundationsForClub();
        UpdateTactics("3-5-2", "High Press", 82, 74, 66, 72);
        if (TacticalRoleFitScore <= 0 ||
            string.IsNullOrWhiteSpace(TacticalRoleFitSummary) ||
            string.IsNullOrWhiteSpace(PlayerFamiliaritySummary) ||
            string.IsNullOrWhiteSpace(SetPieceSummary) ||
            string.IsNullOrWhiteSpace(OpponentPreparationSummary))
        {
            return "Tactical depth did not compute role fit, familiarity, set pieces, or opponent preparation.";
        }

        if (!TacticsFoundationSummary.Contains("Role fit", StringComparison.Ordinal) ||
            !TacticsFoundationSummary.Contains("Set pieces", StringComparison.Ordinal) ||
            !TacticsFoundationSummary.Contains("Opponent prep", StringComparison.Ordinal))
        {
            return "Tactics summary does not expose Phase 4 tactical depth fields.";
        }

        var result = PrepareCurrentMatchResult(true);
        if (!result.TacticalSummary.Contains("Role fit", StringComparison.Ordinal) ||
            !result.TacticalSummary.Contains("Set pieces", StringComparison.Ordinal) ||
            !result.TacticalSummary.Contains("Opponent prep", StringComparison.Ordinal) ||
            !result.TacticalExplanation.Contains("role fit", StringComparison.OrdinalIgnoreCase) ||
            !result.PostMatchNotes.Contains("Role-fit effect", StringComparison.Ordinal))
        {
            return "Shared match simulator did not receive Phase 4 tactical depth inputs.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for tactical depth validation.";
        }

        var expectedRoleFit = TacticalRoleFitScore;
        var expectedSetPiece = SetPieceApproachName;
        var expectedOpponentPrep = OpponentPreparationFocusName;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        UpdateTactics("4-3-3", "Low Block", 35, 42, 44, 32);
        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (TacticalRoleFitScore != expectedRoleFit ||
            SetPieceApproachName != expectedSetPiece ||
            OpponentPreparationFocusName != expectedOpponentPrep)
        {
            return "Save/load did not preserve tactical role fit, set-piece approach, or opponent preparation.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateStage4CalendarTrainingScoutingContract()
    {
        InitializeStageFoundationsForClub();
        SetTrainingFocusByName("Pressing");
        StartBasicScoutingAssignment("Specific player: central midfielder");
        var startingDate = CurrentDate;
        var startingFamiliarity = TacticalFamiliarityScore;
        var startingFitness = SquadPlayers.Length == 0 ? 0 : SquadPlayers[0].Fitness;
        if (!AdvanceOneCareerDay())
        {
            return "Daily advancement failed.";
        }

        if (CurrentDate != startingDate.AddDays(1))
        {
            return "Daily advancement did not change the career date.";
        }

        ApplyWeeklyFoundationProgress();
        if (TacticalFamiliarityScore <= startingFamiliarity)
        {
            return "Training focus did not improve tactical familiarity.";
        }

        if (SquadPlayers.Length > 0 && SquadPlayers[0].Fitness == startingFitness && SquadPlayers[0].Fatigue == 0)
        {
            return "Training focus did not affect player condition.";
        }

        if (CurrentScoutingAssignment == null ||
            CurrentScoutingAssignment.DaysRemaining >= 14 ||
            string.IsNullOrWhiteSpace(CurrentScoutingAssignment.DiscoverySummary))
        {
            return "Scouting assignment did not progress.";
        }

        if (_foundationNewsEvents.Count == 0)
        {
            return "News feed did not update from calendar, training, or scouting events.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase2TrainingScoutingControlsContract()
    {
        InitializeStageFoundationsForClub();
        SetTrainingPlanByName("Pressing", "Demanding");
        StartScoutingAssignment("Specific player: pressing winger", "Full report");
        var startingDate = CurrentDate;
        var startingFamiliarity = TacticalFamiliarityScore;
        var startingFatigue = SquadPlayers.Length == 0 ? 0 : SquadPlayers[0].Fatigue;
        var startingFamiliarityWithPlayer = SquadPlayers.Length == 0 ? 0 : SquadPlayers[0].PlayerFamiliarity;

        if (TrainingFocusName != "Pressing" ||
            TrainingIntensityName != "Demanding" ||
            ScoutingReportDepthName != "Full report" ||
            CurrentScoutingAssignment == null ||
            CurrentScoutingAssignment.DaysRemaining != 18)
        {
            return "Training/scouting controls did not store focus, intensity, report depth, or assignment delay.";
        }

        if (!AdvanceOneCareerDay())
        {
            return "Daily advancement failed from training/scouting controls.";
        }

        if (!AdvanceOneCareerWeek())
        {
            return "Weekly advancement failed from training/scouting controls.";
        }

        if (CurrentDate != startingDate.AddDays(8))
        {
            return "Daily plus weekly advancement did not update the date correctly.";
        }

        if (TacticalFamiliarityScore <= startingFamiliarity ||
            SquadPlayers.Length == 0 ||
            SquadPlayers[0].Fatigue <= startingFatigue ||
            SquadPlayers[0].PlayerFamiliarity <= startingFamiliarityWithPlayer)
        {
            return "Demanding pressing training did not affect familiarity, fatigue, and player familiarity.";
        }

        if (CurrentScoutingAssignment == null ||
            CurrentScoutingAssignment.DaysRemaining >= 18 ||
            !CurrentScoutingAssignment.DiscoverySummary.Contains("confidence", StringComparison.OrdinalIgnoreCase))
        {
            return "Scouting report depth did not progress with a confidence summary.";
        }

        if (!NewsFeedSummary.Contains("Training", StringComparison.OrdinalIgnoreCase) ||
            !NewsFeedSummary.Contains("Scouting", StringComparison.OrdinalIgnoreCase))
        {
            return "Training/scouting controls did not create visible news updates.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for training/scouting validation.";
        }

        var expectedFocus = TrainingFocusName;
        var expectedIntensity = TrainingIntensityName;
        var expectedDepth = ScoutingReportDepthName;
        var expectedDaysRemaining = CurrentScoutingAssignment.DaysRemaining;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (TrainingFocusName != expectedFocus ||
            TrainingIntensityName != expectedIntensity ||
            ScoutingReportDepthName != expectedDepth ||
            CurrentScoutingAssignment == null ||
            CurrentScoutingAssignment.DaysRemaining != expectedDaysRemaining)
        {
            return "Save/load did not preserve training intensity or scouting report depth state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateStage5MatchEngineAlignmentContract()
    {
        InitializeStageFoundationsForClub();
        var first = PrepareCurrentMatchResult(true);
        var second = PrepareCurrentMatchResult();
        if (first.FinalResultSummary != second.FinalResultSummary ||
            first.Timeline.Actions.Length != second.Timeline.Actions.Length)
        {
            return "Instant sim and live playback cache are not sharing the same simulated match object.";
        }

        if (string.IsNullOrWhiteSpace(first.PlayerRatingsSummary) ||
            string.IsNullOrWhiteSpace(first.PostMatchNotes) ||
            !first.TacticalSummary.Contains(TeamStyleName, StringComparison.Ordinal))
        {
            return "Match result is missing player ratings, post-match notes, or tactic context.";
        }

        return MatchPlaybackContractValidator.Validate(first);
    }

    public string ValidatePhase5MatchEngineDepthContract()
    {
        InitializeStageFoundationsForClub();
        UpdateTactics("4-2-3-1", "High Press", 88, 82, 62, 78);
        var result = PrepareCurrentMatchResult(true);
        var cached = PrepareCurrentMatchResult();
        if (!ReferenceEquals(result, cached))
        {
            return "Match depth validation generated a second match object instead of reusing the shared result.";
        }

        if (result.PlayerRatings.Length != 22 ||
            result.TacticalCauses.Length < 4 ||
            string.IsNullOrWhiteSpace(result.MomentumSummary) ||
            string.IsNullOrWhiteSpace(result.DisciplineSummary) ||
            string.IsNullOrWhiteSpace(result.OpponentStyleSummary))
        {
            return "Match result is missing player ratings, tactical causes, momentum, discipline, or opponent style.";
        }

        if (!HasActionKind(result, MatchActionKind.BigChance) ||
            !HasActionKind(result, MatchActionKind.TacticalShift) ||
            !HasActionKind(result, MatchActionKind.FatigueWarning) ||
            !HasActionKind(result, MatchActionKind.YellowCard) ||
            !HasActionKind(result, MatchActionKind.InjuryConcern))
        {
            return "Richer match event timeline is missing big chance, tactical shift, fatigue, card, or injury-concern events.";
        }

        if (result.Stats.HomeBigChances + result.Stats.AwayBigChances <= 0 ||
            result.Stats.HomeYellowCards + result.Stats.AwayYellowCards <= 0 ||
            result.Stats.HomeFatigueWarnings + result.Stats.AwayFatigueWarnings <= 0 ||
            result.Stats.HomeInjuryConcerns + result.Stats.AwayInjuryConcerns <= 0)
        {
            return "Expanded match stats did not count richer event types.";
        }

        if (!result.TacticalExplanation.Contains("Tactical causes", StringComparison.Ordinal) ||
            !result.PlayerRatingsSummary.Contains("Ratings reflect", StringComparison.Ordinal))
        {
            return "Match explanation does not describe tactical causes or player rating basis.";
        }

        ApplyMatchResult(result);
        if (LastMatchReport == null ||
            !LastMatchReport.StatsSummary.Contains("Big chances", StringComparison.Ordinal) ||
            !LastMatchReport.TacticalExplanation.Contains("Tactical cause records", StringComparison.Ordinal))
        {
            return "Post-match report did not receive expanded match stats or tactical cause records.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateStage6ConsequencesPressureContract()
    {
        InitializeStageFoundationsForClub();
        var startingFanTrust = FanTrust;
        var startingReputation = WorldReputation;
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);
        if (LastMatchReport == null)
        {
            return "Post-match consequences did not create a report.";
        }

        if (FanTrust == startingFanTrust && WorldReputation == startingReputation)
        {
            return "Trust and reputation did not change after match consequences.";
        }

        if (JobSecurityName.Length == 0 ||
            !LastMatchReport.PressureSummary.Contains("board", StringComparison.OrdinalIgnoreCase))
        {
            return "Job security or pressure explanation missing after consequences.";
        }

        if (CurrentClub == null || CurrentClub.NewsFeed.Length == 0)
        {
            return "Post-match consequences did not update news.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase6PostMatchReportDepthContract()
    {
        InitializeStageFoundationsForClub();
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);
        if (LastMatchReport == null)
        {
            return "Phase 6 post-match depth did not create a report.";
        }

        var requiredSections = new[]
        {
            LastMatchReport.TacticalSection,
            LastMatchReport.PlayerFitSection,
            LastMatchReport.FatigueSection,
            LastMatchReport.MoraleSection,
            LastMatchReport.BoardReactionSection,
            LastMatchReport.FanReactionSection,
            LastMatchReport.MediaStorySection,
            LastMatchReport.StaffAnalysisSection,
            LastMatchReport.DevelopmentNotesSection
        };

        foreach (var section in requiredSections)
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                return "Phase 6 post-match report is missing a required section.";
            }
        }

        if (!LastMatchReport.TacticalSection.Contains(TacticalFormation, StringComparison.Ordinal) ||
            !LastMatchReport.TacticalSection.Contains("Causes:", StringComparison.Ordinal))
        {
            return "Tactical report section is not tied to formation and tactical causes.";
        }

        if (!LastMatchReport.PlayerFitSection.Contains("Top note:", StringComparison.Ordinal) ||
            !LastMatchReport.PlayerFitSection.Contains("Watch note:", StringComparison.Ordinal))
        {
            return "Player fit report section is missing top and watch notes.";
        }

        var fatigueScore = $"{result.Stats.HomeFatigueWarnings}-{result.Stats.AwayFatigueWarnings}";
        if (!LastMatchReport.FatigueSection.Contains(fatigueScore, StringComparison.Ordinal) ||
            !LastMatchReport.FatigueSection.Contains("injury concerns", StringComparison.OrdinalIgnoreCase))
        {
            return "Fatigue report section is not tied to stored fatigue and injury facts.";
        }

        var moraleDelta = LastMatchReport.MoraleDelta >= 0
            ? $"+{LastMatchReport.MoraleDelta}"
            : LastMatchReport.MoraleDelta.ToString();
        if (!LastMatchReport.MoraleSection.Contains(moraleDelta, StringComparison.Ordinal) ||
            !LastMatchReport.MoraleSection.Contains("fans", StringComparison.OrdinalIgnoreCase))
        {
            return "Morale report section is not tied to stored deltas.";
        }

        if (!LastMatchReport.BoardReactionSection.Contains(BoardPhilosophyName, StringComparison.Ordinal) ||
            !LastMatchReport.FanReactionSection.Contains(FanCultureName, StringComparison.Ordinal))
        {
            return "Board or fan reaction sections do not use club philosophy and fan culture.";
        }

        if (!LastMatchReport.MediaStorySection.Contains(result.HomeClubName, StringComparison.Ordinal) ||
            !LastMatchReport.StaffAnalysisSection.Contains("coach", StringComparison.OrdinalIgnoreCase) ||
            !LastMatchReport.DevelopmentNotesSection.Contains("tactical familiarity", StringComparison.OrdinalIgnoreCase))
        {
            return "Media, staff, or development sections are not grounded in match and career state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase6StoredPostMatchReportContract()
    {
        if (LastMatchReport == null)
        {
            return "Saved phase 6 report did not restore.";
        }

        if (!LastMatchReport.TacticalSection.Contains("Tactical section", StringComparison.Ordinal) ||
            !LastMatchReport.PlayerFitSection.Contains("Player fit section", StringComparison.Ordinal) ||
            !LastMatchReport.FatigueSection.Contains("Fatigue section", StringComparison.Ordinal) ||
            !LastMatchReport.MoraleSection.Contains("Morale section", StringComparison.Ordinal))
        {
            return "Saved phase 6 report restored without core analysis sections.";
        }

        if (!LastMatchReport.BoardReactionSection.Contains("Board reaction", StringComparison.Ordinal) ||
            !LastMatchReport.FanReactionSection.Contains("Fan reaction", StringComparison.Ordinal) ||
            !LastMatchReport.MediaStorySection.Contains("Media story", StringComparison.Ordinal) ||
            !LastMatchReport.StaffAnalysisSection.Contains("Staff analysis", StringComparison.Ordinal) ||
            !LastMatchReport.DevelopmentNotesSection.Contains("Development notes", StringComparison.Ordinal))
        {
            return "Saved phase 6 report restored without reaction, media, staff, or development sections.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase7PerceptionDepthContract()
    {
        InitializeStageFoundationsForClub();
        var startingSquadMorale = TeamMorale;
        var startingFanMorale = FanSentiment;
        var startingBoardMorale = BoardConfidence;
        var startingBoardTrust = CareerProfile.BoardTrust;
        var startingPlayerTrust = CareerProfile.PlayerTrust;
        var startingWorldReputation = WorldReputation;
        var startingTacticalReputation = TacticalReputation;

        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);
        if (LastMatchReport == null)
        {
            return "Phase 7 perception check did not create a post-match report.";
        }

        var moraleMove = Math.Abs(TeamMorale - startingSquadMorale) +
            Math.Abs(FanSentiment - startingFanMorale) +
            Math.Abs(BoardConfidence - startingBoardMorale);
        var boardTrustMove = Math.Abs(CareerProfile.BoardTrust - startingBoardTrust);
        var playerTrustMove = Math.Abs(CareerProfile.PlayerTrust - startingPlayerTrust);
        if (moraleMove == 0 ||
            boardTrustMove >= moraleMove ||
            playerTrustMove >= moraleMove ||
            boardTrustMove > 2 ||
            playerTrustMove > 2)
        {
            return $"Trust did not move slower than morale after the match. Morale move {moraleMove}, board trust move {boardTrustMove}, player trust move {playerTrustMove}.";
        }

        if (WorldReputation == startingWorldReputation && TacticalReputation == startingTacticalReputation)
        {
            return "Reputation categories did not update separately from morale.";
        }

        if (BoardPressure <= 0 || FanPressure <= 0 || DressingRoomPressure <= 0 || FinancialPressure <= 0)
        {
            return "Pressure categories were not recalculated.";
        }

        if (!TrustSummary.Contains("media", StringComparison.OrdinalIgnoreCase) ||
            !ReputationSummary.Contains("tactical", StringComparison.OrdinalIgnoreCase) ||
            !PressureCategorySummary.Contains("financial", StringComparison.OrdinalIgnoreCase))
        {
            return "Perception summaries do not expose trust, reputation, and pressure categories.";
        }

        if (_perceptionHistory.Count == 0 ||
            !PerceptionHistorySummary.Contains("Post-match", StringComparison.Ordinal))
        {
            return "Perception history did not record the post-match change.";
        }

        var transferPressureBefore = TransferPressure;
        var recruitmentReputationBefore = RecruitmentReputation;
        var recruitmentResult = AttemptBasicRecruitmentAction();
        if (string.IsNullOrWhiteSpace(recruitmentResult) ||
            TransferPressure <= transferPressureBefore ||
            RecruitmentReputation == recruitmentReputationBefore ||
            !_perceptionHistory[0].Contains("Recruitment", StringComparison.Ordinal))
        {
            return "Recruitment action did not affect pressure, recruitment reputation, and perception history.";
        }

        EvaluateCareerFoundationState();
        if (string.IsNullOrWhiteSpace(JobSecurityName) ||
            !CareerMarketSummary.Contains("Trust |", StringComparison.Ordinal) ||
            !CareerMarketSummary.Contains("Pressure |", StringComparison.Ordinal))
        {
            return "Job security or career market UI does not use perception categories.";
        }

        if (!NewsFeedSummary.Contains("Post-match consequences", StringComparison.OrdinalIgnoreCase) &&
            !NewsFeedSummary.Contains("Transfer", StringComparison.OrdinalIgnoreCase))
        {
            return "Perception changes did not surface in news.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase7StoredPerceptionContract()
    {
        if (_perceptionHistory.Count == 0)
        {
            return "Saved perception history did not restore.";
        }

        if (!TrustSummary.Contains("board", StringComparison.OrdinalIgnoreCase) ||
            !ReputationSummary.Contains("recruitment", StringComparison.OrdinalIgnoreCase) ||
            !PressureCategorySummary.Contains("dressing room", StringComparison.OrdinalIgnoreCase))
        {
            return "Saved perception summaries did not restore all category groups.";
        }

        if (BoardPressure <= 0 || FanPressure <= 0 || FinancialPressure <= 0)
        {
            return "Saved pressure categories did not restore.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase8DecisionEventsContract()
    {
        InitializeStageFoundationsForClub();
        _activeDecisionEvents.Clear();
        _resolvedDecisionEvents.Clear();

        var eventTypes = new[]
        {
            DecisionEventType.PlayerMeeting,
            DecisionEventType.BoardMeeting,
            DecisionEventType.MediaQuestion,
            DecisionEventType.AgentCall,
            DecisionEventType.StaffDisagreement,
            DecisionEventType.TrainingIssue,
            DecisionEventType.FanPressureMoment,
            DecisionEventType.DirectorConflict,
            DecisionEventType.CrisisEvent
        };

        foreach (var eventType in eventTypes)
        {
            if (!TryCreateDecisionEvent(eventType, "Phase 8 validation", out var decisionEvent))
            {
                return $"Could not generate decision event type {StageFoundationText.GetDisplayName(eventType)}.";
            }

            _activeDecisionEvents.Add(decisionEvent);
        }

        if (_activeDecisionEvents.Count != eventTypes.Length ||
            !DecisionEventSummary.Contains("A:", StringComparison.Ordinal) ||
            !DecisionEventSummary.Contains("B:", StringComparison.Ordinal))
        {
            return "Decision event summary does not expose active choices.";
        }

        var startingNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var startingBoardTrust = CareerProfile.BoardTrust;
        var startingMediaPressure = CareerProfile.MediaPressure;
        var playerOutcome = ResolveActiveDecisionEvent(0);
        var boardOutcome = ResolveActiveDecisionEvent(1);
        var mediaOutcome = ResolveActiveDecisionEvent(0);
        if (string.IsNullOrWhiteSpace(playerOutcome) ||
            string.IsNullOrWhiteSpace(boardOutcome) ||
            string.IsNullOrWhiteSpace(mediaOutcome) ||
            _resolvedDecisionEvents.Count < 3)
        {
            return "Decision event choices did not resolve into stored outcomes.";
        }

        if (CareerProfile.BoardTrust == startingBoardTrust &&
            CareerProfile.MediaPressure == startingMediaPressure)
        {
            return "Decision event outcomes did not affect trust or pressure.";
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= startingNewsCount ||
            !NewsFeedSummary.Contains("Decision resolved", StringComparison.OrdinalIgnoreCase))
        {
            return "Decision event resolution did not update structured news.";
        }

        if (TryCreateDecisionEvent(DecisionEventType.PlayerMeeting, "Cooldown validation", out _))
        {
            return "Decision event cooldown allowed immediate repeat.";
        }

        if (!PerceptionHistorySummary.Contains("Decision event", StringComparison.Ordinal))
        {
            return "Decision event did not record perception history.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase8StoredDecisionEventsContract()
    {
        if (_resolvedDecisionEvents.Count == 0)
        {
            return "Saved resolved decision events did not restore.";
        }

        if (string.IsNullOrWhiteSpace(_resolvedDecisionEvents[0].OutcomeSummary) ||
            !_resolvedDecisionEvents[0].IsResolved)
        {
            return "Saved decision event outcome did not restore.";
        }

        if (string.IsNullOrWhiteSpace(DecisionEventSummary) ||
            (!DecisionEventSummary.Contains("Last resolved", StringComparison.Ordinal) &&
             !DecisionEventSummary.Contains("A:", StringComparison.Ordinal)))
        {
            return "Saved decision event summary is unavailable.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    private static bool HasActionKind(MatchPlaybackResult result, MatchActionKind kind)
    {
        foreach (var action in result.Timeline.Actions)
        {
            if (action.Kind == kind)
            {
                return true;
            }
        }

        return false;
    }

    public string ValidateStage7RecruitmentContract()
    {
        InitializeStageFoundationsForClub();
        EnsureRecruitmentTarget();
        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var result = AttemptBasicRecruitmentAction();
        if (string.IsNullOrWhiteSpace(result) || CurrentRecruitmentTarget == null)
        {
            return "Recruitment action did not produce an outcome.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager &&
            !CurrentRecruitmentTarget.Status.Contains("Recommended", StringComparison.Ordinal))
        {
            return "Assistant Manager role restriction was not respected.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach &&
            !CurrentRecruitmentTarget.Status.Contains("Requested", StringComparison.Ordinal))
        {
            return "Head Coach recruitment authority was not respected.";
        }

        if (CareerProfile.Role == ManagerRole.Manager &&
            !CurrentRecruitmentTarget.Status.Contains("Board", StringComparison.Ordinal))
        {
            return "Manager recruitment action did not route through board approval.";
        }

        if (!CurrentRecruitmentTarget.TacticalFitSummary.Contains("fit", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(CurrentRecruitmentTarget.DirectorResponse) ||
            string.IsNullOrWhiteSpace(CurrentRecruitmentTarget.BoardResponse))
        {
            return "Recruitment target did not include tactical fit, Director response, and board response.";
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
        {
            return "Recruitment action did not update the news feed.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase9TransferMarketContract()
    {
        InitializeStageFoundationsForClub();
        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Transfer market foundation did not create a current recruitment target.";
        }

        if (_recruitmentShortlist.Count < 2)
        {
            return "Transfer market foundation did not create a shortlist.";
        }

        var hasLoanTarget = false;
        var hasIncomingLoan = false;
        var hasOutgoingLoan = false;
        foreach (var target in _recruitmentShortlist)
        {
            if (target.IsLoanCandidate)
            {
                hasLoanTarget = true;
                hasIncomingLoan = hasIncomingLoan || target.LoanDirection == "Incoming loan";
                hasOutgoingLoan = hasOutgoingLoan || target.LoanDirection == "Outgoing loan";
            }
        }

        if (!hasLoanTarget || !hasIncomingLoan || !hasOutgoingLoan)
        {
            return "Loan foundation did not expose incoming and outgoing loan target support.";
        }

        var current = CurrentRecruitmentTarget;
        if (string.IsNullOrWhiteSpace(current.TargetStatus) ||
            string.IsNullOrWhiteSpace(current.ClubValuation) ||
            string.IsNullOrWhiteSpace(current.AgentMood) ||
            string.IsNullOrWhiteSpace(current.RivalInterest) ||
            string.IsNullOrWhiteSpace(current.BoardStance) ||
            string.IsNullOrWhiteSpace(current.DirectorStance) ||
            string.IsNullOrWhiteSpace(current.OutcomeState))
        {
            return "Transfer target is missing status, valuation, agent, rival, board, Director, or outcome state.";
        }

        if (!RecruitmentFoundationSummary.Contains("Shortlist", StringComparison.Ordinal) ||
            !RecruitmentFoundationSummary.Contains("Agent", StringComparison.Ordinal) ||
            !RecruitmentFoundationSummary.Contains("Rival", StringComparison.Ordinal) ||
            !RecruitmentFoundationSummary.Contains("Board", StringComparison.Ordinal) ||
            !RecruitmentFoundationSummary.Contains("Director", StringComparison.Ordinal))
        {
            return "Recruitment UI summary does not expose market state.";
        }

        var beforeHistoryCount = _transferHistory.Count;
        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var result = AttemptBasicRecruitmentAction();
        if (string.IsNullOrWhiteSpace(result) || CurrentRecruitmentTarget == null)
        {
            return "Transfer market action did not produce a result.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager &&
            !CurrentRecruitmentTarget.Status.Contains("Recommended", StringComparison.Ordinal))
        {
            return "Assistant Manager transfer role restriction was not preserved.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach &&
            !CurrentRecruitmentTarget.Status.Contains("Requested", StringComparison.Ordinal))
        {
            return "Head Coach transfer role restriction was not preserved.";
        }

        if (CareerProfile.Role == ManagerRole.Manager)
        {
            if (!CurrentRecruitmentTarget.Status.Contains("Board", StringComparison.Ordinal) ||
                !CurrentRecruitmentTarget.Status.Contains("agent", StringComparison.OrdinalIgnoreCase) ||
                !CurrentRecruitmentTarget.Status.Contains("rival", StringComparison.OrdinalIgnoreCase) ||
                !CurrentRecruitmentTarget.Status.Contains("Director", StringComparison.Ordinal))
            {
                return "Manager transfer review did not consider board, agent, rival, and Director factors.";
            }
        }

        if (_transferHistory.Count <= beforeHistoryCount)
        {
            return "Transfer market action did not record transfer history.";
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
        {
            return "Transfer market action did not update the news feed.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase9StoredTransferMarketContract()
    {
        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Saved transfer market target did not restore.";
        }

        if (_recruitmentShortlist.Count < 2 || !_recruitmentShortlist[0].OutcomeState.Contains("Recommended", StringComparison.Ordinal) && !_recruitmentShortlist[0].OutcomeState.Contains("Requested", StringComparison.Ordinal) && !_recruitmentShortlist[0].OutcomeState.Contains("approved", StringComparison.OrdinalIgnoreCase) && !_recruitmentShortlist[0].OutcomeState.Contains("Blocked", StringComparison.Ordinal))
        {
            return "Saved transfer shortlist did not preserve target outcomes.";
        }

        if (_transferHistory.Count == 0 || !TransferHistorySummary.Contains(":", StringComparison.Ordinal))
        {
            return "Saved transfer history did not restore.";
        }

        if (!RecruitmentShortlistSummary.Contains("Loan", StringComparison.Ordinal) ||
            !RecruitmentFoundationSummary.Contains("Transfer history", StringComparison.Ordinal))
        {
            return "Saved transfer market summaries did not restore loan and history visibility.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase10ContractNegotiationContract()
    {
        InitializeStageFoundationsForClub();
        EnsureContractOffers();
        if (CurrentTransferContractOffer == null || CurrentRenewalContractOffer == null)
        {
            return "Contract negotiation foundation did not create transfer and renewal offers.";
        }

        if (!ContractFoundationSummary.Contains("Transfer signing", StringComparison.Ordinal) ||
            !ContractFoundationSummary.Contains("Current-player renewal", StringComparison.Ordinal) ||
            !ContractFoundationSummary.Contains("Agent", StringComparison.Ordinal) ||
            !ContractFoundationSummary.Contains("Board", StringComparison.Ordinal))
        {
            return "Contract summary does not expose transfer, renewal, agent, and board terms.";
        }

        var beforePromiseCount = _promiseRecords.Count;
        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var beforeFinancialPressure = FinancialPressure;
        var result = AttemptBasicContractNegotiation();
        if (string.IsNullOrWhiteSpace(result) ||
            CurrentTransferContractOffer == null ||
            CurrentRenewalContractOffer == null)
        {
            return "Contract negotiation did not produce an outcome.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager &&
            (!CurrentTransferContractOffer.Status.Contains("Recommended", StringComparison.Ordinal) ||
             CurrentTransferContractOffer.IsAccepted))
        {
            return "Assistant Manager contract authority was not limited to recommendation.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach &&
            (!CurrentTransferContractOffer.Status.Contains("Requested", StringComparison.Ordinal) ||
             CurrentTransferContractOffer.IsAccepted))
        {
            return "Head Coach contract authority was not limited to request/recommendation.";
        }

        if (CareerProfile.Role == ManagerRole.Manager)
        {
            if (!CurrentTransferContractOffer.Status.Contains("Accepted", StringComparison.Ordinal))
            {
                return "Manager transfer contract did not reach an accepted state.";
            }

            if (!CurrentRenewalContractOffer.Status.Contains("countered", StringComparison.OrdinalIgnoreCase))
            {
                return "Manager renewal contract did not expose an agent counter state.";
            }

            var highRiskOffer = CloneContractOfferWithWage(
                CurrentRenewalContractOffer,
                GetHighestSquadWage() * 3,
                $"{FormatWeeklyWage(GetHighestSquadWage() * 3)} proposed as wage-structure stress test.");
            var rejected = ResolveContractOffer(highRiskOffer).Offer;
            if (!rejected.Status.Contains("Board rejected", StringComparison.Ordinal))
            {
                return "Contract approval logic did not produce a board rejection for excessive wages.";
            }

            if (_promiseRecords.Count <= beforePromiseCount)
            {
                return "Accepted contract terms did not create a promise.";
            }

            if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
            {
                return "Contract negotiation did not update the news feed.";
            }

            if (FinancialPressure <= beforeFinancialPressure)
            {
                return "Contract negotiation did not register wage-budget pressure.";
            }
        }

        if (_contractHistory.Count == 0)
        {
            return "Contract negotiation did not record contract history.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase10StoredContractNegotiationContract()
    {
        EnsureContractOffers();
        if (CurrentTransferContractOffer == null ||
            CurrentRenewalContractOffer == null ||
            string.IsNullOrWhiteSpace(CurrentTransferContractOffer.AgentArchetype) ||
            string.IsNullOrWhiteSpace(CurrentRenewalContractOffer.BoardApproval))
        {
            return "Saved contract offers did not restore.";
        }

        if (_contractHistory.Count == 0 ||
            !ContractFoundationSummary.Contains("Contract history", StringComparison.Ordinal) ||
            !ContractFoundationSummary.Contains("Accepted", StringComparison.Ordinal))
        {
            return "Saved contract history or accepted terms did not restore.";
        }

        if (_promiseRecords.Count == 0)
        {
            return "Saved contract-created promise did not restore.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase11DirectorConflictContract()
    {
        InitializeStageFoundationsForClub();
        EnsureDirectorConflictState();
        if (!DirectorInfluenceSummary.Contains("cooperation", StringComparison.OrdinalIgnoreCase) ||
            !DirectorInfluenceSummary.Contains("conflict", StringComparison.OrdinalIgnoreCase) ||
            !DirectorInfluenceSummary.Contains("Scouting priority", StringComparison.Ordinal) ||
            !DirectorInfluenceSummary.Contains("Board report", StringComparison.Ordinal))
        {
            return "Director influence summary is missing cooperation, conflict, scouting priority, or board report.";
        }

        var beforeDirectorTrust = CareerProfile.DirectorTrust;
        var beforeBoardTrust = CareerProfile.BoardTrust;
        var beforeHistoryCount = _directorActionHistory.Count;
        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Director conflict validation could not create a recruitment target.";
        }

        ApplyDirectorRecruitmentInfluence(CurrentRecruitmentTarget, false, "Phase 11 validation");
        if (_directorActionHistory.Count <= beforeHistoryCount)
        {
            return "Director recruitment influence did not record action history.";
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
        {
            return "Director recruitment influence did not update news.";
        }

        if (CareerProfile.DirectorTrust == beforeDirectorTrust)
        {
            return "Director influence did not change Director trust.";
        }

        if (CareerProfile.BoardTrust != beforeBoardTrust)
        {
            return "Director influence changed board trust directly instead of keeping authority separate.";
        }

        EnsureContractOffers();
        if (CurrentTransferContractOffer == null || CurrentRenewalContractOffer == null)
        {
            return "Director conflict validation could not create contract offers.";
        }

        var beforeContractHistoryCount = _directorActionHistory.Count;
        ApplyDirectorContractInfluence(CurrentTransferContractOffer, CurrentRenewalContractOffer);
        if (_directorActionHistory.Count <= beforeContractHistoryCount ||
            !DirectorInfluenceSummary.Contains("Contract negotiation", StringComparison.Ordinal))
        {
            return "Director contract influence did not record a contract action.";
        }

        if (DirectorConflict < 0 || DirectorCooperation < 0 ||
            string.IsNullOrWhiteSpace(DirectorBoardReportSummary))
        {
            return "Director cooperation/conflict state is invalid.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase11StoredDirectorConflictContract()
    {
        EnsureDirectorConflictState();
        if (_directorActionHistory.Count == 0 ||
            !DirectorInfluenceSummary.Contains("Director actions", StringComparison.Ordinal) ||
            !DirectorBoardReportSummary.Contains("Director reports", StringComparison.Ordinal))
        {
            return "Saved Director conflict state did not restore.";
        }

        if (DirectorCooperation <= 0 || DirectorConflict <= 0 ||
            string.IsNullOrWhiteSpace(DirectorScoutingPriority) ||
            string.IsNullOrWhiteSpace(DirectorTransferPreference))
        {
            return "Saved Director cooperation, conflict, priority, or preference state is invalid.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase12StaffImpactMarketContract()
    {
        InitializeStageFoundationsForClub();
        EnsureStaffImpactState();
        if (CurrentStaffMarketCandidate == null)
        {
            return "Staff market candidate was not generated.";
        }

        if (!StaffImpactSummary.Contains("training", StringComparison.OrdinalIgnoreCase) ||
            !StaffImpactSummary.Contains("scouting", StringComparison.OrdinalIgnoreCase) ||
            !StaffImpactSummary.Contains("injury risk", StringComparison.OrdinalIgnoreCase) ||
            !StaffImpactSummary.Contains("media", StringComparison.OrdinalIgnoreCase) ||
            !StaffImpactSummary.Contains("Staff market", StringComparison.Ordinal))
        {
            return "Staff impact summary does not cover training, scouting, injury, media, and market effects.";
        }

        var beforeStaffTrust = CareerProfile.StaffTrust;
        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var candidateName = CurrentStaffMarketCandidate.Name;
        var highWageCandidate = CloneStaffMarketCandidateWithWage(CurrentStaffMarketCandidate, 60000);
        if (BuildStaffHiringApprovalScore(highWageCandidate) >= 50)
        {
            return "Staff board approval logic did not reject an excessive wage case.";
        }

        var result = AttemptStaffMarketAction();
        if (string.IsNullOrWhiteSpace(result) || CurrentStaffMarketCandidate == null)
        {
            return "Staff market action did not produce a result.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager &&
            !CurrentStaffMarketCandidate.Status.Contains("Recommended", StringComparison.Ordinal))
        {
            return "Assistant Manager staff role restriction was not respected.";
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach &&
            !CurrentStaffMarketCandidate.Status.Contains("Requested", StringComparison.Ordinal))
        {
            return "Head Coach staff role restriction was not respected.";
        }

        if (CareerProfile.Role == ManagerRole.Manager)
        {
            if (!CurrentStaffMarketCandidate.Status.Contains("Hired", StringComparison.Ordinal))
            {
                return "Manager staff market action did not complete a hire.";
            }

            var hired = false;
            foreach (var staff in CurrentClub?.Staff ?? Array.Empty<StaffMember>())
            {
                hired = hired || staff.Name == candidateName;
            }

            if (!hired)
            {
                return "Completed staff hire did not update the staff list.";
            }

            if (CareerProfile.StaffTrust <= beforeStaffTrust)
            {
                return "Completed staff hire did not affect staff trust.";
            }

            if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
            {
                return "Staff market action did not update news.";
            }
        }

        if (_staffHistory.Count == 0 || !StaffImpactSummary.Contains("Staff history", StringComparison.Ordinal))
        {
            return "Staff market action did not record staff history.";
        }

        SetTrainingPlanByName("Pressing", "Demanding");
        ApplyWeeklyFoundationProgress();
        if (!TrainingStatusSummary.Contains("Staff modifiers", StringComparison.Ordinal))
        {
            return "Training consequences do not expose staff modifiers.";
        }

        StartScoutingAssignment("Specific player: staff-influenced target", "Full report");
        if (CurrentScoutingAssignment == null ||
            CurrentScoutingAssignment.ReportQuality <= 0 ||
            !StaffReportSummary.Contains("scouting", StringComparison.OrdinalIgnoreCase))
        {
            return "Scouting assignment did not retain staff report influence.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase12StoredStaffImpactMarketContract()
    {
        EnsureStaffImpactState();
        if (CurrentStaffMarketCandidate == null ||
            _staffHistory.Count == 0 ||
            string.IsNullOrWhiteSpace(StaffReportSummary) ||
            !StaffImpactSummary.Contains("Staff history", StringComparison.Ordinal))
        {
            return "Saved staff impact/market state did not restore.";
        }

        if (CurrentClub == null || CurrentClub.Staff.Length == 0 ||
            CurrentClub.Staff[0].Wage <= 0 ||
            string.IsNullOrWhiteSpace(CurrentClub.Staff[0].Relationship))
        {
            return "Saved staff contract details did not restore.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase13YouthAcademyContract()
    {
        InitializeStageFoundationsForClub();
        EnsureYouthAcademyState();
        if (!YouthAcademySummary.Contains("Academy quality", StringComparison.Ordinal) ||
            !YouthAcademySummary.Contains("Board:", StringComparison.Ordinal) ||
            !YouthAcademySummary.Contains("Fans:", StringComparison.Ordinal))
        {
            return "Youth academy summary does not expose quality, board expectation, and fan expectation.";
        }

        var beforeNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        var beforeYouthReputation = YouthReputation;
        var beforeSquadCount = SquadPlayers.Length;
        var intakeResult = AdvanceYouthAcademyAction();
        if (!intakeResult.Contains("Youth intake generated", StringComparison.Ordinal) ||
            _youthProspects.Count == 0 ||
            !YouthAcademySummary.Contains("hidden potential", StringComparison.OrdinalIgnoreCase) ||
            !YouthAcademySummary.Contains("loan:", StringComparison.OrdinalIgnoreCase))
        {
            return "Youth intake did not generate prospects with hidden potential and loan suitability.";
        }

        if (YouthReputation <= beforeYouthReputation)
        {
            return "Youth intake did not affect youth reputation.";
        }

        var firstProspectName = _youthProspects[0].Name;
        var beforeBoardMorale = BoardMorale;
        var beforeFanMorale = FanMorale;
        var actionResult = AdvanceYouthAcademyAction();
        if (string.IsNullOrWhiteSpace(actionResult))
        {
            return "Youth academy action did not return a result.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            if (SquadPlayers.Length != beforeSquadCount ||
                !_youthProspects[0].Status.Contains("recommended", StringComparison.OrdinalIgnoreCase))
            {
                return "Assistant Manager youth authority was not respected.";
            }
        }
        else
        {
            if (SquadPlayers.Length <= beforeSquadCount)
            {
                return "Youth promotion did not add a senior squad player.";
            }

            var promoted = SquadPlayers[^1];
            if (promoted.Name != firstProspectName ||
                !promoted.UnknownAttributeGroups.Contains("hidden potential", StringComparison.OrdinalIgnoreCase) ||
                !promoted.TransferInterest.Contains("Loan", StringComparison.OrdinalIgnoreCase))
            {
                return "Promoted academy player did not retain partial information, hidden potential, and loan suitability.";
            }

            if (BoardMorale <= beforeBoardMorale || FanMorale <= beforeFanMorale)
            {
                return "Youth promotion did not affect board and fan reaction.";
            }
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= beforeNewsCount)
        {
            return "Youth academy action did not update news.";
        }

        if (_youthHistory.Count == 0 || !YouthAcademySummary.Contains("Youth history", StringComparison.Ordinal))
        {
            return "Youth academy action did not record history.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase13StoredYouthAcademyContract()
    {
        EnsureYouthAcademyState();
        if (_youthProspects.Count == 0 ||
            _youthHistory.Count == 0 ||
            string.IsNullOrWhiteSpace(YouthFacilitiesSummary) ||
            !YouthAcademySummary.Contains("Prospects", StringComparison.Ordinal) ||
            !YouthAcademySummary.Contains("Youth history", StringComparison.Ordinal))
        {
            return "Saved youth academy state did not restore.";
        }

        if (YouthAcademyQuality <= 0 || YouthRecruitmentReach <= 0 || YouthCoachingQuality <= 0)
        {
            return "Saved youth academy quality, reach, or coaching state is invalid.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase14PlayerDevelopmentContract()
    {
        InitializeStageFoundationsForClub();
        EnsurePlayerDevelopmentState();
        if (SquadPlayers.Length == 0)
        {
            return "No squad players available for player development validation.";
        }

        var before = SquadPlayers;
        var beforeFirstAge = before[0].Age;
        SetTrainingPlanByName("Youth integration", "Demanding");
        ApplyWeeklyFoundationProgress();
        if (!PlayerDevelopmentSummary.Contains("Development cadence", StringComparison.Ordinal) ||
            !PlayerDevelopmentSummary.Contains("staff score", StringComparison.OrdinalIgnoreCase) ||
            !PlayerDevelopmentSummary.Contains("Ability changes", StringComparison.Ordinal))
        {
            return "Weekly development summary does not explain focus, staff, and ability/condition changes.";
        }

        var changed = false;
        for (var index = 0; index < before.Length; index++)
        {
            var previous = before[index];
            var current = SquadPlayers[index];
            if (current.TrueAbility != previous.TrueAbility ||
                current.Form != previous.Form ||
                current.Morale != previous.Morale ||
                current.Fitness != previous.Fitness ||
                current.Fatigue != previous.Fatigue ||
                current.InjuryRisk != previous.InjuryRisk ||
                current.DevelopmentCurve != previous.DevelopmentCurve)
            {
                changed = true;
                break;
            }
        }

        if (!changed)
        {
            return "Weekly development did not change any player state or development notes.";
        }

        if (_playerDevelopmentHistory.Count == 0 ||
            !PlayerDevelopmentHistorySummary.Contains("Weekly development", StringComparison.Ordinal) ||
            !TrainingScoutingSummary.Contains("Development history", StringComparison.Ordinal))
        {
            return "Player development history was not recorded or surfaced.";
        }

        var rolloverMessage = CompleteCurrentSeason();
        if (rolloverMessage != MatchPlaybackContractValidator.PassMessage)
        {
            return rolloverMessage;
        }

        if (SquadPlayers.Length == 0 || SquadPlayers[0].Age <= beforeFirstAge)
        {
            return "Season development did not age the squad.";
        }

        if (!PlayerDevelopmentSummary.Contains("Season development review", StringComparison.Ordinal) ||
            !_playerDevelopmentHistory.Exists(entry => entry.Contains("Season development review", StringComparison.Ordinal)))
        {
            return "Season development review was not recorded.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase14StoredPlayerDevelopmentContract()
    {
        EnsurePlayerDevelopmentState();
        if (string.IsNullOrWhiteSpace(PlayerDevelopmentSummary) ||
            _playerDevelopmentHistory.Count == 0 ||
            !TrainingScoutingSummary.Contains("Development", StringComparison.Ordinal))
        {
            return "Saved player development summary/history did not restore.";
        }

        var hasDevelopmentCurve = false;
        foreach (var player in SquadPlayers)
        {
            hasDevelopmentCurve = hasDevelopmentCurve ||
                player.DevelopmentCurve.Contains("development", StringComparison.OrdinalIgnoreCase);
        }

        if (!hasDevelopmentCurve)
        {
            return "Saved squad did not retain development curve notes.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase15FinanceContract()
    {
        InitializeStageFoundationsForClub();
        EnsureFinanceState();
        if (!FinanceSummary.Contains("Transfer budget remaining", StringComparison.Ordinal) ||
            !FinanceSummary.Contains("wage bill", StringComparison.OrdinalIgnoreCase) ||
            !FinanceSummary.Contains("debt", StringComparison.OrdinalIgnoreCase) ||
            !FinanceSummary.Contains("ticket income", StringComparison.OrdinalIgnoreCase) ||
            !FinanceSummary.Contains("commercial growth", StringComparison.OrdinalIgnoreCase) ||
            !FinanceSummary.Contains("profit expectation", StringComparison.OrdinalIgnoreCase))
        {
            return "Finance summary does not expose budget, wages, debt, revenue, and profit expectations.";
        }

        var beforeRevenue = FinanceRevenue;
        var beforeHistory = _financeHistory.Count;
        ApplyWeeklyFinanceProgress();
        if (FinanceRevenue <= beforeRevenue ||
            _financeHistory.Count <= beforeHistory ||
            !FinanceHistorySummary.Contains("Weekly finance", StringComparison.Ordinal))
        {
            return "Weekly finance progress did not add revenue and history.";
        }

        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Finance validation could not create a recruitment target.";
        }

        var stressTarget = CloneRecruitmentTarget(CurrentRecruitmentTarget, CurrentRecruitmentTarget.Status, CurrentRecruitmentTarget.TargetStatus, CurrentRecruitmentTarget.OutcomeState);
        stressTarget = CloneRecruitmentTargetWithFinanceStress(stressTarget);
        if (CanFinanceRecruitmentTarget(stressTarget))
        {
            return "Finance approval did not reject an excessive transfer/wage stress target.";
        }

        var beforeTransferBudget = FinanceTransferBudgetRemaining;
        if (CanFinanceRecruitmentTarget(CurrentRecruitmentTarget))
        {
            ApplyRecruitmentFinanceImpact(CurrentRecruitmentTarget);
            if (!CurrentRecruitmentTarget.IsLoanCandidate && FinanceTransferBudgetRemaining >= beforeTransferBudget)
            {
                return "Recruitment finance impact did not reserve transfer budget.";
            }
        }

        EnsureContractOffers();
        if (CurrentTransferContractOffer == null)
        {
            return "Finance validation could not create a contract offer.";
        }

        var highWageOffer = CloneContractOfferWithWage(
            CurrentTransferContractOffer,
            FinanceWageBudget * 2,
            $"{FormatFinanceMoney(FinanceWageBudget * 2)} proposed as wage budget stress test.");
        if (BuildFinanceContractPenalty(highWageOffer) <= 0)
        {
            return "Finance contract penalty did not detect wage-budget stress.";
        }

        var beforeWagePressure = WageStructurePressure;
        CurrentTransferContractOffer = CloneContractOffer(CurrentTransferContractOffer, "Accepted", "Validation accepted finance-linked terms.", "Accepted", isAccepted: true);
        ApplyContractFinanceImpact(CurrentTransferContractOffer);
        if (WageStructurePressure < beforeWagePressure ||
            !FinanceHistorySummary.Contains("accepted", StringComparison.OrdinalIgnoreCase))
        {
            return "Accepted contract did not affect wage structure pressure or finance history.";
        }

        EnsureStaffImpactState();
        if (CurrentStaffMarketCandidate == null)
        {
            return "Finance validation could not create a staff market candidate.";
        }

        var beforeExpenses = FinanceExpenses;
        ApplyStaffFinanceImpact(CurrentStaffMarketCandidate);
        if (FinanceExpenses <= beforeExpenses ||
            !FinanceHistorySummary.Contains("staff hire", StringComparison.OrdinalIgnoreCase))
        {
            return "Staff finance impact did not affect expenses and finance history.";
        }

        if (!CareerMarketSummary.Contains("Finance", StringComparison.Ordinal) ||
            FinancialPressure < 0 ||
            WageStructurePressure < 0)
        {
            return "Finance state is not surfaced or pressure state is invalid.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase15StoredFinanceContract()
    {
        EnsureFinanceState();
        if (string.IsNullOrWhiteSpace(FinanceSummary) ||
            _financeHistory.Count == 0 ||
            FinanceWageBudget <= 0 ||
            FinanceCurrentWageBill <= 0 ||
            !CareerMarketSummary.Contains("Finance history", StringComparison.Ordinal))
        {
            return "Saved finance state did not restore.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePhase3PromiseLifecycleContract()
    {
        InitializeStageFoundationsForClub();
        if (SquadPlayers.Length == 0)
        {
            return "No squad player available for promise lifecycle validation.";
        }

        _promiseRecords.Clear();
        var startingTrust = CareerProfile.PlayerTrust;
        var startingNewsCount = CurrentClub?.NewsFeed.Length ?? 0;
        _promiseRecords.Add(new PromiseRecord
        {
            PromiseType = "Playing time",
            Recipient = SquadPlayers[0].Name,
            Source = "Player meeting",
            IsPublic = false,
            ExpectedAction = "Keep the player involved this week.",
            DeadlineSummary = "Next weekly review",
            DaysRemaining = 7,
            Status = PromiseStatus.Active,
            CurrentEvidence = "Promise created for validation.",
            AgentMood = "Neutral",
            ConsequenceRisk = "Player trust moves if this is ignored."
        });
        ApplyWeeklyFoundationProgress();
        if (_promiseRecords.Count == 0 ||
            _promiseRecords[0].Status != PromiseStatus.Fulfilled ||
            CareerProfile.PlayerTrust <= startingTrust)
        {
            return "Promise lifecycle did not fulfill a credible short promise or improve player trust.";
        }

        _promiseRecords.Add(new PromiseRecord
        {
            PromiseType = "Contract renewal",
            Recipient = SquadPlayers[0].Name,
            Source = "Agent call",
            IsPublic = true,
            ExpectedAction = "Open renewal talks before pressure spills into the dressing room.",
            DeadlineSummary = "Next weekly review",
            DaysRemaining = 7,
            Status = PromiseStatus.Active,
            CurrentEvidence = "Agent has gone public with the expectation.",
            AgentMood = "Concerned",
            ConsequenceRisk = "Public failure affects player trust, squad trust, media pressure, and job pressure."
        });
        TransferPressure = 92;
        DressingRoomPressure = 80;
        var trustBeforeBrokenPromise = CareerProfile.PlayerTrust;
        ApplyWeeklyFoundationProgress();
        if (_promiseRecords.Count < 2 ||
            _promiseRecords[1].Status != PromiseStatus.Broken ||
            CareerProfile.PlayerTrust >= trustBeforeBrokenPromise ||
            TransferPressure <= 92)
        {
            return "Promise lifecycle did not break a public high-pressure promise with trust and pressure consequences.";
        }

        if ((CurrentClub?.NewsFeed.Length ?? 0) <= startingNewsCount ||
            !PromiseSummary.Contains("agent", StringComparison.OrdinalIgnoreCase) ||
            !PromiseSummary.Contains("Broken", StringComparison.Ordinal))
        {
            return "Promise lifecycle did not update news or expose status, evidence, and agent mood.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for promise lifecycle validation.";
        }

        var expectedSummary = PromiseSummary;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (!PromiseSummary.Contains("Broken", StringComparison.Ordinal) ||
            !PromiseSummary.Contains("Fulfilled", StringComparison.Ordinal) ||
            PromiseSummary == "No active promises." ||
            PromiseSummary.Length < Math.Min(40, expectedSummary.Length))
        {
            return "Save/load did not preserve promise lifecycle state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateStage8CareerJobMarketContract()
    {
        InitializeStageFoundationsForClub();
        EvaluateCareerFoundationState();
        GenerateJobMarketEvent();
        if (CurrentJobOffer == null)
        {
            return "Job market event was not generated.";
        }

        if (_careerHistory.Count == 0 ||
            string.IsNullOrWhiteSpace(LicenseOpportunitySummary) ||
            string.IsNullOrWhiteSpace(JobSecurityName))
        {
            return "Career history, license progression, or job security foundation is missing.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable for career/job market validation.";
        }

        var expectedOfferClub = CurrentJobOffer.ClubName;
        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (CurrentJobOffer == null || CurrentJobOffer.ClubName != expectedOfferClub)
        {
            return "Save/load did not preserve career job market state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateRoleAuthorityStabilizationContract()
    {
        InitializeStageFoundationsForClub();
        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            if (SquadPlayers.Length == 0)
            {
                return "No squad players available for Assistant Manager authority validation.";
            }

            var playerName = SquadPlayers[0].Name;
            var wasStarting = SquadPlayers[0].IsStarting;
            var originalFormation = TacticalFormation;
            var originalStyle = TeamStyleName;
            var lineupStatus = TogglePlayerLineupStatus(playerName);
            if (!lineupStatus.Contains("Assistant Manager recommendation", StringComparison.Ordinal))
            {
                return $"Assistant lineup action was not treated as a recommendation: {lineupStatus}";
            }

            if (SquadPlayers[0].IsStarting != wasStarting)
            {
                return "Assistant lineup recommendation changed the final XI.";
            }

            var tacticStatus = TryApplyTacticsFromUser("3-5-2", "High Press", 82, 76, 58, 68);
            if (!tacticStatus.Contains("Assistant Manager tactical recommendation", StringComparison.Ordinal))
            {
                return $"Assistant tactics action was not treated as a recommendation: {tacticStatus}";
            }

            if (TacticalFormation != originalFormation || TeamStyleName != originalStyle)
            {
                return "Assistant tactical recommendation changed the saved match plan.";
            }

            if (!NewsFeedSummary.Contains("recommendation", StringComparison.OrdinalIgnoreCase))
            {
                return "Assistant recommendations did not leave a visible news trail.";
            }

            return MatchPlaybackContractValidator.PassMessage;
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach)
        {
            var tacticStatus = TryApplyTacticsFromUser("3-5-2", "High Press", 82, 76, 58, 68);
            if (!tacticStatus.Contains("Saved tactical setup", StringComparison.Ordinal) ||
                TacticalFormation != "3-5-2" ||
                TeamStyleName != "High Press")
            {
                return $"Head Coach tactic control did not update saved tactic state: {tacticStatus}";
            }

            return MatchPlaybackContractValidator.PassMessage;
        }

        var benchIndex = Array.FindIndex(SquadPlayers, player => !player.IsStarting);
        if (benchIndex < 0)
        {
            return "No bench player found for Manager lineup-control validation.";
        }

        var targetName = SquadPlayers[benchIndex].Name;
        var managerStatus = TogglePlayerLineupStatus(targetName);
        if (!managerStatus.Contains("enters the XI", StringComparison.Ordinal) &&
            !managerStatus.Contains("promoted into the XI", StringComparison.Ordinal))
        {
            return $"Manager lineup action did not apply a direct XI change: {managerStatus}";
        }

        return SquadPlayers[benchIndex].IsStarting
            ? MatchPlaybackContractValidator.PassMessage
            : "Manager lineup control did not update the XI.";
    }

    private void RefreshTacticFoundation(string previousFormation, TacticalTeamStyle previousStyle)
    {
        PassingDirectness = Math.Clamp((Tempo + Risk) / 2, 0, 100);
        DefensiveLine = Math.Clamp((PressIntensity + Risk / 2 + 25), 0, 100);
        Tackling = Math.Clamp(45 + PressIntensity / 3 + Risk / 5, 0, 100);

        if (previousFormation != TacticalFormation || previousStyle != TeamStyle)
        {
            TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore - 5, 0, 100);
        }
        else
        {
            TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + 1, 0, 100);
        }

        var familiarity = TacticsFoundation.FamiliarityFromScore(TacticalFamiliarityScore);
        TeamInstructionsSummary = TacticsFoundation.BuildTeamInstructions(
            TeamStyle,
            Tempo,
            PassingDirectness,
            PressIntensity,
            DefensiveLine,
            Width,
            Risk,
            Tackling);
        PlayerRolesSummary = TacticsFoundation.BuildPlayerRolesSummary(SquadPlayers, TacticalFormation, TeamStyle);
        PlayerInstructionsSummary = TacticsFoundation.BuildPlayerInstructionsSummary(TeamStyle);
        TacticalRoleFitScore = TacticsFoundation.CalculateRoleFitScore(SquadPlayers, TacticalFormation, TeamStyle);
        TacticalRoleFitSummary = TacticsFoundation.BuildRoleFitDepthSummary(SquadPlayers, TacticalFormation, TeamStyle, TacticalRoleFitScore);
        PlayerFamiliaritySummary = TacticsFoundation.BuildPlayerFamiliaritySummary(SquadPlayers, familiarity);
        SetPieceApproach = TacticsFoundation.ResolveSetPieceApproach(TeamStyle, CurrentTrainingFocus, Risk);
        SetPieceSummary = TacticsFoundation.BuildSetPieceSummary(SetPieceApproach, CurrentTrainingFocus);
        CurrentOpponentPreparationFocus = TacticsFoundation.ResolveOpponentPreparationFocus(TeamStyle, PressIntensity, Width, Risk);
        OpponentPreparationSummary = TacticsFoundation.BuildOpponentPreparationSummary(CurrentOpponentPreparationFocus, CurrentOpponentName);
        TacticalFitNotes = TacticsFoundation.BuildFitNotes(SquadPlayers, TeamStyle, familiarity);
        TacticalRiskNotes = TacticsFoundation.BuildRiskNotes(TeamStyle, PressIntensity, Tempo, Risk, familiarity);
    }

    private void ApplyTrainingEffects()
    {
        var baseFamiliarityDelta = CurrentTrainingFocus switch
        {
            TrainingFocus.Pressing or TrainingFocus.Possession or TrainingFocus.Counterattack or TrainingFocus.DefensiveShape or TrainingFocus.AttackingMovement => 4,
            TrainingFocus.TeamCohesion => 3,
            TrainingFocus.Recovery => 1,
            _ => 2
        };
        var intensityFamiliarityDelta = CurrentTrainingIntensity switch
        {
            TrainingIntensity.Controlled => -1,
            TrainingIntensity.Demanding => 2,
            _ => 0
        };
        var coachingModifier = Math.Clamp((GetStaffQuality(StaffRole.FirstTeamCoach) + GetStaffQuality(StaffRole.AssistantManager) - 110) / 20, -2, 4);
        var familiarityDelta = Math.Max(0, baseFamiliarityDelta + intensityFamiliarityDelta + coachingModifier);
        TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + familiarityDelta, 0, 100);
        var fitnessStaffModifier = Math.Clamp((GetStaffQuality(StaffRole.FitnessCoach) + GetStaffQuality(StaffRole.Physio) - 110) / 25, -2, 3);

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            var player = SquadPlayers[index];
            var baseFitnessDelta = CurrentTrainingFocus switch
            {
                TrainingFocus.Fitness => 2,
                TrainingFocus.Recovery => 6,
                _ => -1
            };
            var baseFatigueDelta = CurrentTrainingFocus switch
            {
                TrainingFocus.Fitness or TrainingFocus.Pressing => 5,
                TrainingFocus.Recovery => -12,
                _ => 2
            };
            var fitnessDelta = baseFitnessDelta + fitnessStaffModifier + (CurrentTrainingIntensity == TrainingIntensity.Controlled ? 2 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? -2 : 0);
            var fatigueDelta = baseFatigueDelta - fitnessStaffModifier + (CurrentTrainingIntensity == TrainingIntensity.Controlled ? -4 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? 6 : 0);
            var moraleDelta = CurrentTrainingFocus == TrainingFocus.TeamCohesion ? 2 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? -1 : 0;
            var injuryDelta = fatigueDelta > 0 ? 1 + (CurrentTrainingIntensity == TrainingIntensity.Demanding ? 2 : 0) - fitnessStaffModifier : -2 - fitnessStaffModifier;
            SquadPlayers[index] = player.With(
                fitness: Math.Clamp(player.Fitness + fitnessDelta, 35, 99),
                morale: Math.Clamp(player.Morale + moraleDelta, 0, 100),
                fatigue: Math.Clamp(player.Fatigue + fatigueDelta, 0, 100),
                injuryRisk: Math.Clamp(player.InjuryRisk + injuryDelta, 0, 100),
                tacticalFitScore: Math.Clamp(player.TacticalFitScore + familiarityDelta / 2, 0, 100),
                playerFamiliarity: Math.Clamp(player.PlayerFamiliarity + 2 + familiarityDelta / 2, 0, 100));
        }

        RefreshTacticFoundation(TacticalFormation, TeamStyle);
        StaffReportSummary = BuildStaffReportSummary();
        TrainingStatusSummary = $"{TrainingFocusName} at {TrainingIntensityName.ToLowerInvariant()} intensity changed tactical familiarity to {TacticalFamiliarityName}, updated condition, and raised staff familiarity with the squad. Staff modifiers: coaching {coachingModifier}, fitness/physio {fitnessStaffModifier}.";
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    private void ApplyScoutingProgress(int days)
    {
        if (CurrentScoutingAssignment == null)
        {
            return;
        }

        var remaining = Math.Max(0, CurrentScoutingAssignment.DaysRemaining - days);
        var ready = remaining == 0;
        var projectedQuality = Math.Clamp(CurrentScoutingAssignment.ReportQuality + days / 2, 0, 100);
        var discovery = ready
            ? BuildScoutingDiscoverySummary(projectedQuality, true)
            : BuildScoutingDiscoverySummary(projectedQuality, false);
        CurrentScoutingAssignment = new ScoutingAssignment
        {
            Target = CurrentScoutingAssignment.Target,
            DaysRemaining = remaining,
            ReportQuality = projectedQuality,
            DiscoverySummary = discovery,
            ReportReady = ready
        };
        RefreshRecruitmentMarketInformation();

        if (ready)
        {
            AddNews(
                "Scouting report ready",
                NewsCategory.Scouting,
                "Scout report",
                CurrentScoutingAssignment.DiscoverySummary,
                4);
        }
    }

    private void EnsureRecruitmentTarget()
    {
        if (CurrentRecruitmentTarget != null)
        {
            EnsureRecruitmentShortlist();
            return;
        }

        var sourceClub = string.IsNullOrWhiteSpace(CurrentOpponentName) ? ResolveDifferentClub(SelectedClubName) : CurrentOpponentName;
        var sourceSquad = GetClubSquad(sourceClub);
        var candidate = sourceSquad.Length == 0
            ? ClubSquadFactory.BuildFallbackSquad(sourceClub, WorldSeed)[0]
            : sourceSquad[Math.Min(8, sourceSquad.Length - 1)];
        CurrentRecruitmentTarget = BuildRecruitmentTarget(candidate, "Shortlisted", "Identified by scouting", false, "Not a loan target");
        EnsureRecruitmentShortlist();
    }

    private void EnsureRecruitmentShortlist()
    {
        if (_recruitmentShortlist.Count > 0)
        {
            return;
        }

        if (CurrentRecruitmentTarget != null)
        {
            _recruitmentShortlist.Add(CurrentRecruitmentTarget);
        }

        var sourceClub = string.IsNullOrWhiteSpace(CurrentOpponentName) ? ResolveDifferentClub(SelectedClubName) : CurrentOpponentName;
        var sourceSquad = GetClubSquad(sourceClub);
        if (sourceSquad.Length == 0)
        {
            sourceSquad = ClubSquadFactory.BuildFallbackSquad(sourceClub, WorldSeed);
        }

        var incomingLoan = FindLoanCandidate(sourceSquad, preferExternal: true);
        if (incomingLoan != null)
        {
            _recruitmentShortlist.Add(BuildRecruitmentTarget(incomingLoan, "Loan shortlist", "Incoming loan review", true, "Incoming loan"));
        }

        var outgoingLoan = FindLoanCandidate(Array.ConvertAll(SquadPlayers, ConvertSquadPlayerToClubSquadPlayer), preferExternal: false);
        if (outgoingLoan != null)
        {
            _recruitmentShortlist.Add(BuildRecruitmentTarget(outgoingLoan, "Loan shortlist", "Outgoing development loan review", true, "Outgoing loan"));
        }
    }

    private ClubSquadPlayer? FindLoanCandidate(ClubSquadPlayer[] squad, bool preferExternal)
    {
        ClubSquadPlayer? fallback = null;
        foreach (var player in squad)
        {
            if (player.Age > 24)
            {
                continue;
            }

            fallback ??= player;
            if (preferExternal || !player.IsStarting)
            {
                return player;
            }
        }

        return fallback;
    }

    private void RefreshRecruitmentMarketInformation()
    {
        if (CurrentRecruitmentTarget == null)
        {
            return;
        }

        var candidate = FindRecruitmentCandidateByName(CurrentRecruitmentTarget.PlayerName);
        if (candidate == null)
        {
            return;
        }

        var refreshed = BuildRecruitmentTarget(
            candidate,
            CurrentRecruitmentTarget.TargetStatus,
            CurrentRecruitmentTarget.OutcomeState,
            CurrentRecruitmentTarget.IsLoanCandidate,
            CurrentRecruitmentTarget.LoanDirection);
        CurrentRecruitmentTarget = CloneRecruitmentTarget(refreshed, CurrentRecruitmentTarget.Status, CurrentRecruitmentTarget.TargetStatus, CurrentRecruitmentTarget.OutcomeState);
        SyncCurrentRecruitmentTargetToShortlist();
    }

    private ClubSquadPlayer? FindRecruitmentCandidateByName(string playerName)
    {
        foreach (var player in SquadPlayers)
        {
            if (player.Name == playerName)
            {
                return ConvertSquadPlayerToClubSquadPlayer(player);
            }
        }

        var sourceClub = string.IsNullOrWhiteSpace(CurrentOpponentName) ? ResolveDifferentClub(SelectedClubName) : CurrentOpponentName;
        foreach (var player in GetClubSquad(sourceClub))
        {
            if (player.Name == playerName)
            {
                return player;
            }
        }

        foreach (var clubName in AvailableClubs)
        {
            foreach (var player in GetClubSquad(clubName))
            {
                if (player.Name == playerName)
                {
                    return player;
                }
            }
        }

        return null;
    }

    private static ClubSquadPlayer ConvertSquadPlayerToClubSquadPlayer(SquadPlayer player)
    {
        return new ClubSquadPlayer
        {
            PlayerId = player.PlayerId,
            ClubName = string.Empty,
            Name = player.Name,
            Position = player.Position,
            Age = player.Age,
            TrueAbility = player.TrueAbility,
            TacticalFitScore = player.TacticalFitScore,
            PlayingStyle = player.PlayingStyle,
            TacticalFit = player.TacticalFit,
            Form = player.Form,
            Morale = player.Morale,
            Fitness = player.Fitness,
            Fatigue = player.Fatigue,
            IsStarting = player.IsStarting
        };
    }

    private RecruitmentTarget BuildRecruitmentTarget(ClubSquadPlayer candidate, string targetStatus, string outcomeState, bool isLoanCandidate, string loanDirection)
    {
        var strongFit = candidate.TacticalFitScore >= 68;
        var information = BuildRecruitmentInformationSummary(candidate);
        var feeRange = isLoanCandidate
            ? loanDirection == "Outgoing loan" ? "Wage coverage 40%-70%" : "Loan fee $0.0m-$0.3m"
            : BuildFeeRange(candidate.TrueAbility, candidate.Age);
        var wageRange = BuildWageRange(candidate.TrueAbility);
        var boardStance = BuildBoardRecruitmentStance(candidate, isLoanCandidate, loanDirection);
        var directorStance = BuildDirectorRecruitmentStance(candidate, isLoanCandidate, loanDirection);
        return new RecruitmentTarget
        {
            PlayerName = candidate.Name,
            Position = candidate.Position,
            InformationSummary = information,
            InterestSummary = BuildPlayerInterestSummary(candidate, isLoanCandidate, loanDirection),
            TacticalFitSummary = strongFit ? $"Strong fit for {TeamStyleName}." : $"Partial fit for {TeamStyleName}; scouting recommends caution.",
            EstimatedFeeRange = feeRange,
            EstimatedWageRange = wageRange,
            DirectorResponse = BuildDirectorRecruitmentResponse(candidate),
            BoardResponse = BuildBoardRecruitmentResponse(candidate),
            TargetStatus = targetStatus,
            ClubValuation = isLoanCandidate ? BuildLoanValuation(candidate, loanDirection) : $"Selling club valuation tracks {feeRange}, with interest and rival pressure still material.",
            AgentMood = BuildAgentMood(candidate, isLoanCandidate),
            RivalInterest = BuildRivalInterest(candidate, isLoanCandidate),
            BoardStance = boardStance,
            DirectorStance = directorStance,
            OutcomeState = outcomeState,
            IsLoanCandidate = isLoanCandidate,
            LoanDirection = loanDirection,
            DevelopmentLoanSuitability = isLoanCandidate ? BuildDevelopmentLoanSuitability(candidate, loanDirection) : "Not assessed: permanent transfer target.",
            PlayingTimeExpectation = isLoanCandidate ? BuildLoanPlayingTimeExpectation(candidate, loanDirection) : "Contract role promise to be handled in contract phase.",
            LoanClubFit = isLoanCandidate ? BuildLoanClubFit(candidate, loanDirection) : "Not a loan pathway.",
            LoanReviewSummary = isLoanCandidate ? "Loan review placeholder: suitability, minutes, and recall/review timing must be checked before completion." : "No loan review opened.",
            Status = isLoanCandidate ? $"{loanDirection} target listed for review; no loan agreement started." : "Shortlisted foundation target; no negotiation started."
        };
    }

    private void EnsureJobMarketFoundation()
    {
        if (CurrentJobOffer == null)
        {
            LicenseOpportunitySummary = BuildLicenseOpportunitySummary();
        }

        if (_careerHistory.Count == 0 && !string.IsNullOrWhiteSpace(SelectedClubName))
        {
            _careerHistory.Add($"{CurrentDateLabel}: appointed {CurrentRoleName} of {SelectedClubName} with {LicenseName}.");
        }
    }

    private void EvaluateCareerFoundationState()
    {
        RefreshPressureCategories();
        JobSecurity = EvaluateJobSecurity();
        LicenseOpportunitySummary = BuildLicenseOpportunitySummary();
    }

    private JobSecurityState EvaluateJobSecurity()
    {
        var pressure = JobPressure +
            BoardPressure / 5 +
            FanPressure / 6 +
            DressingRoomPressure / 6 +
            GetRolePressureWeight();
        return pressure switch
        {
            >= 95 => JobSecurityState.Sacked,
            >= 84 => JobSecurityState.NearSacking,
            >= 74 => JobSecurityState.Ultimatum,
            >= 64 => JobSecurityState.UnderPressure,
            >= 52 => JobSecurityState.Watched,
            >= 36 => JobSecurityState.Stable,
            _ => JobSecurityState.Secure
        };
    }

    private int GetRolePressureWeight()
    {
        return CareerProfile.Role switch
        {
            ManagerRole.AssistantManager => -8,
            ManagerRole.HeadCoach => 3,
            _ => 8
        };
    }

    private void RefreshPressureCategories()
    {
        BoardPressure = Math.Clamp(100 - BoardMorale + Math.Max(0, 55 - CareerProfile.BoardTrust) / 2 + GetRolePressureWeight(), 0, 100);
        FanPressure = Math.Clamp(100 - FanMorale + Math.Max(0, CareerProfile.MediaPressure - 45) / 2, 0, 100);
        DressingRoomPressure = Math.Clamp(100 - SquadMorale + Math.Max(0, JobPressure - 55) / 2 + Math.Max(0, 55 - CareerProfile.PlayerTrust) / 3, 0, 100);
        var strictBoardPressure = CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard ? 15 : 0;
        FinancialPressure = Math.Clamp(25 + strictBoardPressure + Math.Max(0, TransferPressure - 55) / 3 + Math.Max(0, 55 - BoardMorale) / 4, 0, 100);
    }

    private static int ResolveSlowTrustDelta(int fastDelta)
    {
        return fastDelta switch
        {
            >= 6 => 2,
            >= 2 => 1,
            <= -6 => -2,
            <= -2 => -1,
            _ => 0
        };
    }

    private int ResolveTacticalReputationDelta(MatchPlaybackResult result, int goalDifference)
    {
        var delta = goalDifference > 0 ? 1 : goalDifference < 0 ? -1 : 0;
        if (TacticalRoleFitScore >= 72 && result.Stats.HomeBigChances >= result.Stats.AwayBigChances)
        {
            delta += 1;
        }
        else if (TacticalRoleFitScore <= 52 && result.Stats.HomeBigChances <= result.Stats.AwayBigChances)
        {
            delta -= 1;
        }

        return Math.Clamp(delta, -2, 2);
    }

    private int ResolveYouthReputationDelta(MatchPlaybackResult result)
    {
        foreach (var rating in result.PlayerRatings)
        {
            if (rating.Team != result.HomeClubName)
            {
                continue;
            }

            foreach (var player in SquadPlayers)
            {
                if (player.PlayerId == rating.PlayerId && player.Age <= 23 && rating.Rating >= 6.8)
                {
                    return 1;
                }
            }
        }

        return 0;
    }

    private int ResolveRecruitmentReputationDelta(int goalDifference)
    {
        if (goalDifference >= 0 && TransferPressure < 45)
        {
            return 1;
        }

        if (goalDifference < 0 && TransferPressure >= 65)
        {
            return -1;
        }

        return 0;
    }

    private void RecordPerceptionHistory(string trigger, string detail)
    {
        _perceptionHistory.Insert(0, $"{CurrentDateLabel}: {trigger} - {detail}");
        if (_perceptionHistory.Count > 10)
        {
            _perceptionHistory.RemoveAt(_perceptionHistory.Count - 1);
        }
    }

    private void RecordTransferHistory(string detail)
    {
        _transferHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_transferHistory.Count > 12)
        {
            _transferHistory.RemoveAt(_transferHistory.Count - 1);
        }
    }

    private void SyncCurrentRecruitmentTargetToShortlist()
    {
        if (CurrentRecruitmentTarget == null)
        {
            return;
        }

        for (var index = 0; index < _recruitmentShortlist.Count; index++)
        {
            if (_recruitmentShortlist[index].PlayerName == CurrentRecruitmentTarget.PlayerName)
            {
                _recruitmentShortlist[index] = CurrentRecruitmentTarget;
                return;
            }
        }

        _recruitmentShortlist.Insert(0, CurrentRecruitmentTarget);
    }

    private void EnsureDirectorConflictState()
    {
        if (DirectorCooperation <= 0 || DirectorCooperation == 55 && DirectorConflict == 25)
        {
            DirectorCooperation = BuildInitialDirectorCooperation();
            DirectorConflict = BuildInitialDirectorConflict();
        }

        if (string.IsNullOrWhiteSpace(DirectorScoutingPriority) || DirectorScoutingPriority.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            DirectorScoutingPriority = BuildDirectorScoutingPriority();
        }

        if (string.IsNullOrWhiteSpace(DirectorTransferPreference) || DirectorTransferPreference.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            DirectorTransferPreference = BuildDirectorTransferPreference();
        }

        if (string.IsNullOrWhiteSpace(DirectorSalesPressureSummary) || DirectorSalesPressureSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            DirectorSalesPressureSummary = BuildDirectorSalesPressureSummary();
        }

        if (string.IsNullOrWhiteSpace(DirectorBoardReportSummary) || DirectorBoardReportSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            DirectorBoardReportSummary = BuildDirectorBoardReportSummary("Initial Director briefing");
        }
    }

    private string BuildDirectorInfluenceSummary()
    {
        EnsureDirectorConflictState();
        var history = _directorActionHistory.Count == 0
            ? "Director action history starts after scouting, transfer, contract, or pressure conflict."
            : string.Join("\n", _directorActionHistory);
        return $"Style {DirectorOfFootballStyleName} | relationship {DirectorRelationshipName} | trust {CareerProfile.DirectorTrust} | cooperation {DirectorCooperation} | conflict {DirectorConflict}\nScouting priority: {DirectorScoutingPriority}\nTransfer preference: {DirectorTransferPreference}\nSales pressure: {DirectorSalesPressureSummary}\nBoard report: {DirectorBoardReportSummary}\nDirector actions\n{history}";
    }

    private int BuildInitialDirectorCooperation()
    {
        var relationship = CurrentClub?.DirectorRelationshipState ?? DirectorRelationshipState.Neutral;
        var relationshipBase = relationship switch
        {
            DirectorRelationshipState.Ally => 72,
            DirectorRelationshipState.Supportive => 64,
            DirectorRelationshipState.Tense => 42,
            DirectorRelationshipState.Hostile => 28,
            _ => 55
        };
        return Math.Clamp((relationshipBase + CareerProfile.DirectorTrust) / 2, 0, 100);
    }

    private int BuildInitialDirectorConflict()
    {
        var relationship = CurrentClub?.DirectorRelationshipState ?? DirectorRelationshipState.Neutral;
        var relationshipBase = relationship switch
        {
            DirectorRelationshipState.Ally => 12,
            DirectorRelationshipState.Supportive => 22,
            DirectorRelationshipState.Tense => 52,
            DirectorRelationshipState.Hostile => 72,
            _ => 32
        };
        return Math.Clamp(relationshipBase + Math.Max(0, 55 - CareerProfile.DirectorTrust) / 2, 0, 100);
    }

    private string BuildDirectorScoutingPriority()
    {
        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.TalentTrader => "Prioritizes resale value and market timing.",
            DirectorOfFootballStyle.StarChaser => "Prioritizes visible reputation and high-status targets.",
            DirectorOfFootballStyle.AcademyBuilder => "Prioritizes academy pathways and young high-upside players.",
            DirectorOfFootballStyle.BargainHunter => "Prioritizes undervalued players and low wage risk.",
            DirectorOfFootballStyle.ControlFreak => "Prioritizes Director-led shortlist control and process compliance.",
            DirectorOfFootballStyle.ClubLoyalist => "Prioritizes club identity, dressing-room continuity, and fan trust.",
            DirectorOfFootballStyle.PoliticalSurvivor => "Prioritizes board optics and blame protection.",
            _ => "Prioritizes data evidence and tactical value."
        };
    }

    private string BuildDirectorTransferPreference()
    {
        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.TalentTrader => "Will push profitable sales and younger replacements.",
            DirectorOfFootballStyle.StarChaser => "Will push marquee signings over quiet tactical fits.",
            DirectorOfFootballStyle.AcademyBuilder => "Will resist signings that block academy minutes.",
            DirectorOfFootballStyle.BargainHunter => "Will block expensive wages without value proof.",
            DirectorOfFootballStyle.ControlFreak => "Will challenge targets not sourced through his process.",
            DirectorOfFootballStyle.ClubLoyalist => "Will resist selling respected dressing-room figures.",
            DirectorOfFootballStyle.PoliticalSurvivor => "Will protect himself through board reports and leaks.",
            _ => "Will support evidence-led targets with clean fit and value."
        };
    }

    private string BuildDirectorSalesPressureSummary()
    {
        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.TalentTrader => "High sale pressure if a player exceeds market value.",
            DirectorOfFootballStyle.BargainHunter => "Moderate sale pressure to protect wage structure.",
            DirectorOfFootballStyle.ClubLoyalist => "Low sale pressure for popular or loyal players.",
            DirectorOfFootballStyle.PoliticalSurvivor => "Sale pressure follows board optics and media cover.",
            _ => "Sale pressure depends on fit, age, contract, and board trust."
        };
    }

    private string BuildDirectorBoardReportSummary(string trigger)
    {
        return $"{trigger}: Director reports cooperation {DirectorCooperation}/100, conflict {DirectorConflict}/100, transfer pressure {TransferPressure}/100, and Director trust {CareerProfile.DirectorTrust}/100 separately from board trust {CareerProfile.BoardTrust}/100.";
    }

    private void ApplyDirectorRecruitmentInfluence(RecruitmentTarget target, bool approved, string trigger)
    {
        EnsureDirectorConflictState();
        var action = ResolveDirectorRecruitmentAction(target, approved);
        var supportive = action.Contains("supports", StringComparison.OrdinalIgnoreCase) ||
            action.Contains("proposes", StringComparison.OrdinalIgnoreCase);
        DirectorCooperation = Math.Clamp(DirectorCooperation + (supportive ? 3 : -4), 0, 100);
        DirectorConflict = Math.Clamp(DirectorConflict + (supportive ? -2 : 7), 0, 100);
        CareerProfile.DirectorTrust = Math.Clamp(CareerProfile.DirectorTrust + (supportive ? 1 : -2), 0, 100);
        TransferPressure = Math.Clamp(TransferPressure + (supportive ? -1 : 3), 0, 100);
        if (action.Contains("leaks", StringComparison.OrdinalIgnoreCase))
        {
            CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + 3, 0, 100);
        }

        DirectorBoardReportSummary = BuildDirectorBoardReportSummary(trigger);
        RecordDirectorAction($"{trigger}: {action} Target {target.PlayerName}; board stance remains separate: {target.BoardStance}");
        AddNews(
            "Director recruitment influence",
            NewsCategory.Transfer,
            "Internal",
            $"{DirectorOfFootballStyleName}: {action}",
            4,
            sourceType: "Director of Football",
            relatedEntity: target.PlayerName,
            effectSummary: $"Director cooperation {DirectorCooperation}; conflict {DirectorConflict}; Director trust {CareerProfile.DirectorTrust}.",
            cooldownKey: "director-recruitment");
        TryRaiseDirectorDecisionEvent(trigger);
    }

    private void ApplyDirectorContractInfluence(ContractOffer transferOffer, ContractOffer renewalOffer)
    {
        EnsureDirectorConflictState();
        var action = ResolveDirectorContractAction(transferOffer, renewalOffer);
        var supportive = action.Contains("supports", StringComparison.OrdinalIgnoreCase);
        DirectorCooperation = Math.Clamp(DirectorCooperation + (supportive ? 2 : -3), 0, 100);
        DirectorConflict = Math.Clamp(DirectorConflict + (supportive ? -1 : 5), 0, 100);
        CareerProfile.DirectorTrust = Math.Clamp(CareerProfile.DirectorTrust + (supportive ? 1 : -1), 0, 100);
        TransferPressure = Math.Clamp(TransferPressure + (supportive ? -1 : 2), 0, 100);
        DirectorBoardReportSummary = BuildDirectorBoardReportSummary("Contract negotiation");
        RecordDirectorAction($"Contract negotiation: {action} Transfer offer {transferOffer.Status}; renewal offer {renewalOffer.Status}.");
        AddNews(
            "Director contract report",
            NewsCategory.Contract,
            "Internal",
            $"{DirectorOfFootballStyleName}: {action}",
            3,
            sourceType: "Director of Football",
            relatedEntity: $"{transferOffer.PlayerName}; {renewalOffer.PlayerName}",
            effectSummary: $"Director conflict {DirectorConflict}; transfer pressure {TransferPressure}.",
            cooldownKey: "director-contract");
        TryRaiseDirectorDecisionEvent("Contract negotiation");
    }

    private string ResolveDirectorRecruitmentAction(RecruitmentTarget target, bool approved)
    {
        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.TalentTrader when target.RivalInterest.Contains("likely", StringComparison.OrdinalIgnoreCase) => "pushes sale-market logic and warns the board about rival pressure.",
            DirectorOfFootballStyle.StarChaser when !target.TacticalFitSummary.Contains("Strong", StringComparison.Ordinal) => "proposes a higher-profile alternative before accepting a quiet tactical fit.",
            DirectorOfFootballStyle.AcademyBuilder when target.InterestSummary.Contains("development", StringComparison.OrdinalIgnoreCase) => "supports the target because the pathway fits academy logic.",
            DirectorOfFootballStyle.BargainHunter when target.EstimatedWageRange.Contains("$7", StringComparison.Ordinal) || target.EstimatedWageRange.Contains("$8", StringComparison.Ordinal) => "blocks the target until wage value is proven.",
            DirectorOfFootballStyle.ControlFreak => "blocks the target unless the approach runs through his shortlist process.",
            DirectorOfFootballStyle.ClubLoyalist when approved => "supports the move but warns against damaging dressing-room balance.",
            DirectorOfFootballStyle.PoliticalSurvivor when !approved => "leaks disagreement and frames the failed signing as a process issue.",
            _ => approved ? "supports the approach with evidence-led caveats." : "questions the approach and asks for stronger scouting evidence."
        };
    }

    private string ResolveDirectorContractAction(ContractOffer transferOffer, ContractOffer renewalOffer)
    {
        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.BargainHunter when transferOffer.ProposedWage > GetHighestSquadWage() => "blocks wage escalation and asks the board for a tighter structure.",
            DirectorOfFootballStyle.ControlFreak => "challenges contract authority and wants all agent contact routed through his office.",
            DirectorOfFootballStyle.PoliticalSurvivor when renewalOffer.Status.Contains("countered", StringComparison.OrdinalIgnoreCase) => "frames the agent counter as proof that contract expectations need board backing.",
            DirectorOfFootballStyle.ClubLoyalist when renewalOffer.IsRenewal => "supports renewal stability and dressing-room continuity.",
            DirectorOfFootballStyle.StarChaser when transferOffer.Status == "Accepted" => "supports the signing because it signals ambition.",
            _ => "supports contract talks if wage, role, and scouting evidence remain explainable."
        };
    }

    private void TryRaiseDirectorDecisionEvent(string trigger)
    {
        if (DirectorConflict < 55 || _activeDecisionEvents.Count > 0)
        {
            return;
        }

        if (TryCreateDecisionEvent(DecisionEventType.DirectorConflict, trigger, out var decisionEvent))
        {
            _activeDecisionEvents.Add(decisionEvent);
        }
    }

    private void RecordDirectorAction(string detail)
    {
        _directorActionHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_directorActionHistory.Count > 12)
        {
            _directorActionHistory.RemoveAt(_directorActionHistory.Count - 1);
        }
    }

    private int BuildRecruitmentMarketScore(RecruitmentTarget target)
    {
        var score = 42;
        score += target.TacticalFitSummary.Contains("Strong", StringComparison.Ordinal) ? 16 : 4;
        score += target.AgentMood.Contains("receptive", StringComparison.OrdinalIgnoreCase) || target.AgentMood.Contains("curious", StringComparison.OrdinalIgnoreCase) ? 9 : 2;
        score += target.RivalInterest.Contains("likely", StringComparison.OrdinalIgnoreCase) ? -7 : 4;
        score += target.BoardStance.Contains("support", StringComparison.OrdinalIgnoreCase) || target.BoardStance.Contains("open", StringComparison.OrdinalIgnoreCase) ? 10 : -5;
        score += target.DirectorStance.Contains("support", StringComparison.OrdinalIgnoreCase) || target.DirectorStance.Contains("acceptable", StringComparison.OrdinalIgnoreCase) ? 10 : -5;
        score += CareerProfile.BoardTrust >= 60 ? 5 : CareerProfile.BoardTrust < 45 ? -6 : 0;
        score += CareerProfile.DirectorTrust >= 60 ? 5 : CareerProfile.DirectorTrust < 45 ? -6 : 0;
        score += target.IsLoanCandidate ? 5 : 0;
        return Math.Clamp(score, 0, 100);
    }

    private static SaveSlotDecisionEventData BuildDecisionEventSaveData(DecisionEvent decisionEvent)
    {
        return new SaveSlotDecisionEventData
        {
            EventId = decisionEvent.EventId,
            EventTypeName = StageFoundationText.GetDisplayName(decisionEvent.EventType),
            Title = decisionEvent.Title,
            SourceType = decisionEvent.SourceType,
            Reliability = decisionEvent.Reliability,
            RelatedEntity = decisionEvent.RelatedEntity,
            Importance = decisionEvent.Importance,
            Prompt = decisionEvent.Prompt,
            PrimaryOption = decisionEvent.PrimaryOption,
            SecondaryOption = decisionEvent.SecondaryOption,
            PrimaryEffectSummary = decisionEvent.PrimaryEffectSummary,
            SecondaryEffectSummary = decisionEvent.SecondaryEffectSummary,
            CooldownKey = decisionEvent.CooldownKey,
            DaysUntilRepeat = decisionEvent.DaysUntilRepeat,
            IsResolved = decisionEvent.IsResolved,
            OutcomeSummary = decisionEvent.OutcomeSummary
        };
    }

    private static DecisionEvent RestoreDecisionEvent(SaveSlotDecisionEventData data, bool forceResolved)
    {
        var eventType = StageFoundationText.ParseDecisionEventType(data.EventTypeName);
        var title = string.IsNullOrWhiteSpace(data.Title)
            ? StageFoundationText.GetDisplayName(eventType)
            : data.Title;
        var cooldownKey = string.IsNullOrWhiteSpace(data.CooldownKey)
            ? StageFoundationText.GetDisplayName(eventType)
            : data.CooldownKey;
        return new DecisionEvent
        {
            EventId = string.IsNullOrWhiteSpace(data.EventId) ? cooldownKey : data.EventId,
            EventType = eventType,
            Title = title,
            SourceType = string.IsNullOrWhiteSpace(data.SourceType) ? "Club source" : data.SourceType,
            Reliability = string.IsNullOrWhiteSpace(data.Reliability) ? "Confirmed" : data.Reliability,
            RelatedEntity = data.RelatedEntity,
            Importance = data.Importance <= 0 ? 3 : data.Importance,
            Prompt = string.IsNullOrWhiteSpace(data.Prompt) ? title : data.Prompt,
            PrimaryOption = string.IsNullOrWhiteSpace(data.PrimaryOption) ? "Handle privately" : data.PrimaryOption,
            SecondaryOption = string.IsNullOrWhiteSpace(data.SecondaryOption) ? "Set a firm line" : data.SecondaryOption,
            PrimaryEffectSummary = string.IsNullOrWhiteSpace(data.PrimaryEffectSummary) ? "Trust improves slightly; pressure falls slightly." : data.PrimaryEffectSummary,
            SecondaryEffectSummary = string.IsNullOrWhiteSpace(data.SecondaryEffectSummary) ? "Authority improves slightly; relationship pressure rises slightly." : data.SecondaryEffectSummary,
            CooldownKey = cooldownKey,
            DaysUntilRepeat = Math.Clamp(data.DaysUntilRepeat, 0, 60),
            IsResolved = forceResolved || data.IsResolved,
            OutcomeSummary = data.OutcomeSummary
        };
    }

    private static SaveSlotRecruitmentTargetData BuildRecruitmentTargetSaveData(RecruitmentTarget target)
    {
        return new SaveSlotRecruitmentTargetData
        {
            PlayerName = target.PlayerName,
            Position = target.Position,
            InformationSummary = target.InformationSummary,
            InterestSummary = target.InterestSummary,
            TacticalFitSummary = target.TacticalFitSummary,
            EstimatedFeeRange = target.EstimatedFeeRange,
            EstimatedWageRange = target.EstimatedWageRange,
            DirectorResponse = target.DirectorResponse,
            BoardResponse = target.BoardResponse,
            TargetStatus = target.TargetStatus,
            ClubValuation = target.ClubValuation,
            AgentMood = target.AgentMood,
            RivalInterest = target.RivalInterest,
            BoardStance = target.BoardStance,
            DirectorStance = target.DirectorStance,
            OutcomeState = target.OutcomeState,
            IsLoanCandidate = target.IsLoanCandidate,
            LoanDirection = target.LoanDirection,
            DevelopmentLoanSuitability = target.DevelopmentLoanSuitability,
            PlayingTimeExpectation = target.PlayingTimeExpectation,
            LoanClubFit = target.LoanClubFit,
            LoanReviewSummary = target.LoanReviewSummary,
            Status = target.Status
        };
    }

    private static SaveSlotContractOfferData BuildContractOfferSaveData(ContractOffer offer)
    {
        return new SaveSlotContractOfferData
        {
            OfferId = offer.OfferId,
            PlayerName = offer.PlayerName,
            IsRenewal = offer.IsRenewal,
            SourceType = offer.SourceType,
            AgentArchetype = offer.AgentArchetype,
            WageSummary = offer.WageSummary,
            ProposedWage = offer.ProposedWage,
            DurationSummary = offer.DurationSummary,
            DurationYears = offer.DurationYears,
            ExpirySummary = offer.ExpirySummary,
            SquadRole = offer.SquadRole,
            ClausesSummary = offer.ClausesSummary,
            RenewalStatus = offer.RenewalStatus,
            AgentMood = offer.AgentMood,
            PlayerInterest = offer.PlayerInterest,
            BoardApproval = offer.BoardApproval,
            PromiseSummary = offer.PromiseSummary,
            Status = offer.Status,
            OutcomeSummary = offer.OutcomeSummary,
            IsAccepted = offer.IsAccepted
        };
    }

    private static ContractOffer RestoreContractOffer(SaveSlotContractOfferData data)
    {
        var playerName = string.IsNullOrWhiteSpace(data.PlayerName) ? "Unknown player" : data.PlayerName;
        var source = string.IsNullOrWhiteSpace(data.SourceType)
            ? data.IsRenewal ? "Current-player renewal" : "Transfer signing"
            : data.SourceType;
        var wage = data.ProposedWage <= 0 ? 52000 : data.ProposedWage;
        var duration = data.DurationYears <= 0 ? data.IsRenewal ? 2 : 3 : data.DurationYears;
        return new ContractOffer
        {
            OfferId = string.IsNullOrWhiteSpace(data.OfferId) ? BuildOfferId(playerName, source) : data.OfferId,
            PlayerName = playerName,
            IsRenewal = data.IsRenewal,
            SourceType = source,
            AgentArchetype = string.IsNullOrWhiteSpace(data.AgentArchetype) ? "Pragmatic agent" : data.AgentArchetype,
            WageSummary = string.IsNullOrWhiteSpace(data.WageSummary) ? $"{FormatWeeklyWage(wage)} proposed." : data.WageSummary,
            ProposedWage = wage,
            DurationSummary = string.IsNullOrWhiteSpace(data.DurationSummary) ? $"{duration} years" : data.DurationSummary,
            DurationYears = duration,
            ExpirySummary = string.IsNullOrWhiteSpace(data.ExpirySummary) ? $"Expires after {duration} years" : data.ExpirySummary,
            SquadRole = string.IsNullOrWhiteSpace(data.SquadRole) ? "Squad Player" : data.SquadRole,
            ClausesSummary = string.IsNullOrWhiteSpace(data.ClausesSummary) ? "Standard bonus and role review." : data.ClausesSummary,
            RenewalStatus = string.IsNullOrWhiteSpace(data.RenewalStatus) ? "Saved contract terms restored." : data.RenewalStatus,
            AgentMood = string.IsNullOrWhiteSpace(data.AgentMood) ? "Neutral" : data.AgentMood,
            PlayerInterest = string.IsNullOrWhiteSpace(data.PlayerInterest) ? "Player interest pending." : data.PlayerInterest,
            BoardApproval = string.IsNullOrWhiteSpace(data.BoardApproval) ? "Board approval pending." : data.BoardApproval,
            PromiseSummary = string.IsNullOrWhiteSpace(data.PromiseSummary) ? "Role promise pending." : data.PromiseSummary,
            Status = string.IsNullOrWhiteSpace(data.Status) ? "Draft" : data.Status,
            OutcomeSummary = string.IsNullOrWhiteSpace(data.OutcomeSummary) ? "Saved contract terms restored." : data.OutcomeSummary,
            IsAccepted = data.IsAccepted
        };
    }

    private static RecruitmentTarget RestoreRecruitmentTarget(SaveSlotRecruitmentTargetData data)
    {
        var information = string.IsNullOrWhiteSpace(data.InformationSummary)
            ? "Knowledge: saved target visibility unavailable; scout confidence should be rebuilt by a new report."
            : data.InformationSummary;
        var fee = string.IsNullOrWhiteSpace(data.EstimatedFeeRange) ? "Fee estimate pending" : data.EstimatedFeeRange;
        var wage = string.IsNullOrWhiteSpace(data.EstimatedWageRange) ? "Wage estimate pending" : data.EstimatedWageRange;
        var targetStatus = string.IsNullOrWhiteSpace(data.TargetStatus) ? "Shortlisted" : data.TargetStatus;
        var boardStance = string.IsNullOrWhiteSpace(data.BoardStance) ? "Board stance pending evidence review." : data.BoardStance;
        var directorStance = string.IsNullOrWhiteSpace(data.DirectorStance) ? "Director stance pending evidence review." : data.DirectorStance;
        var loanDirection = string.IsNullOrWhiteSpace(data.LoanDirection) ? "Not a loan target" : data.LoanDirection;
        return new RecruitmentTarget
        {
            PlayerName = string.IsNullOrWhiteSpace(data.PlayerName) ? "Unknown target" : data.PlayerName,
            Position = string.IsNullOrWhiteSpace(data.Position) ? "Unknown" : data.Position,
            InformationSummary = information,
            InterestSummary = string.IsNullOrWhiteSpace(data.InterestSummary) ? "Interest pending scouting confidence." : data.InterestSummary,
            TacticalFitSummary = string.IsNullOrWhiteSpace(data.TacticalFitSummary) ? "Fit pending tactical review." : data.TacticalFitSummary,
            EstimatedFeeRange = fee,
            EstimatedWageRange = wage,
            DirectorResponse = string.IsNullOrWhiteSpace(data.DirectorResponse) ? directorStance : data.DirectorResponse,
            BoardResponse = string.IsNullOrWhiteSpace(data.BoardResponse) ? boardStance : data.BoardResponse,
            TargetStatus = targetStatus,
            ClubValuation = string.IsNullOrWhiteSpace(data.ClubValuation) ? fee : data.ClubValuation,
            AgentMood = string.IsNullOrWhiteSpace(data.AgentMood) ? "Agent mood unknown until contact." : data.AgentMood,
            RivalInterest = string.IsNullOrWhiteSpace(data.RivalInterest) ? "No rival pressure confirmed." : data.RivalInterest,
            BoardStance = boardStance,
            DirectorStance = directorStance,
            OutcomeState = string.IsNullOrWhiteSpace(data.OutcomeState) ? targetStatus : data.OutcomeState,
            IsLoanCandidate = data.IsLoanCandidate,
            LoanDirection = loanDirection,
            DevelopmentLoanSuitability = string.IsNullOrWhiteSpace(data.DevelopmentLoanSuitability) ? "Loan suitability not assessed." : data.DevelopmentLoanSuitability,
            PlayingTimeExpectation = string.IsNullOrWhiteSpace(data.PlayingTimeExpectation) ? "Playing-time expectation pending." : data.PlayingTimeExpectation,
            LoanClubFit = string.IsNullOrWhiteSpace(data.LoanClubFit) ? "Loan club fit pending." : data.LoanClubFit,
            LoanReviewSummary = string.IsNullOrWhiteSpace(data.LoanReviewSummary) ? "Loan review not opened." : data.LoanReviewSummary,
            Status = string.IsNullOrWhiteSpace(data.Status) ? "Shortlisted foundation target; no negotiation started." : data.Status
        };
    }

    private string BuildDecisionEventSummary()
    {
        if (_activeDecisionEvents.Count == 0)
        {
            var resolved = _resolvedDecisionEvents.Count == 0
                ? "No resolved decision events yet."
                : $"Last resolved: {_resolvedDecisionEvents[0].Title} - {_resolvedDecisionEvents[0].OutcomeSummary}";
            return $"No active decision event. {resolved}";
        }

        var activeEvent = _activeDecisionEvents[0];
        return $"{StageFoundationText.GetDisplayName(activeEvent.EventType)} | {activeEvent.Reliability} | {activeEvent.SourceType} | {activeEvent.Title}\n{activeEvent.Prompt}\nA: {activeEvent.PrimaryOption} ({activeEvent.PrimaryEffectSummary})\nB: {activeEvent.SecondaryOption} ({activeEvent.SecondaryEffectSummary})";
    }

    private string BuildRecruitmentShortlistSummary()
    {
        EnsureRecruitmentTarget();
        if (_recruitmentShortlist.Count == 0)
        {
            return "No active recruitment shortlist.";
        }

        var lines = new List<string>();
        foreach (var target in _recruitmentShortlist)
        {
            var loan = target.IsLoanCandidate
                ? $" | Loan {target.LoanDirection}: {target.DevelopmentLoanSuitability}; {target.PlayingTimeExpectation}; {target.LoanClubFit}"
                : string.Empty;
            lines.Add($"{target.PlayerName} ({target.Position}) - {target.TargetStatus}; {target.OutcomeState}; {target.AgentMood}; {target.RivalInterest}{loan}");
        }

        return string.Join("\n", lines);
    }

    private string BuildContractFoundationSummary()
    {
        EnsureContractOffers();
        var transfer = CurrentTransferContractOffer == null ? "Transfer contract: not prepared." : BuildContractOfferLine(CurrentTransferContractOffer);
        var renewal = CurrentRenewalContractOffer == null ? "Renewal contract: not prepared." : BuildContractOfferLine(CurrentRenewalContractOffer);
        var history = _contractHistory.Count == 0
            ? "Contract history starts when terms are recommended, requested, countered, accepted, or rejected."
            : string.Join("\n", _contractHistory);
        return $"{transfer}\n{renewal}\nContract history\n{history}";
    }

    private static string BuildContractOfferLine(ContractOffer offer)
    {
        return $"{offer.SourceType}: {offer.PlayerName} | {offer.Status} | {offer.AgentArchetype} | {offer.WageSummary} | {offer.DurationSummary} | {offer.SquadRole} | {offer.ClausesSummary} | Agent {offer.AgentMood} | Player {offer.PlayerInterest} | Board {offer.BoardApproval} | Promise {offer.PromiseSummary} | {offer.OutcomeSummary}";
    }

    private void EnsureContractOffers()
    {
        EnsureRecruitmentTarget();
        if (CurrentTransferContractOffer == null && CurrentRecruitmentTarget != null)
        {
            CurrentTransferContractOffer = BuildTransferContractOffer(CurrentRecruitmentTarget);
        }

        if (CurrentRenewalContractOffer == null)
        {
            var player = FindRenewalCandidate();
            if (player != null)
            {
                CurrentRenewalContractOffer = BuildRenewalContractOffer(player);
            }
        }
    }

    private ContractOffer BuildTransferContractOffer(RecruitmentTarget target)
    {
        var wage = EstimateWageFromSummary(target.EstimatedWageRange, 52000);
        var durationYears = target.AgentMood.Contains("ambitious", StringComparison.OrdinalIgnoreCase) ? 4 : 3;
        var role = target.TacticalFitSummary.Contains("Strong", StringComparison.Ordinal)
            ? "Important Player"
            : "Rotation Player";
        var agent = BuildAgentArchetype(target.PlayerName, target.AgentMood);
        return new ContractOffer
        {
            OfferId = BuildOfferId(target.PlayerName, "transfer"),
            PlayerName = target.PlayerName,
            IsRenewal = false,
            SourceType = "Transfer signing",
            AgentArchetype = agent,
            WageSummary = $"{FormatWeeklyWage(wage)} proposed within target range {target.EstimatedWageRange}",
            ProposedWage = wage,
            DurationSummary = $"{durationYears} years",
            DurationYears = durationYears,
            ExpirySummary = $"Expires {CurrentDate.Year + durationYears}",
            SquadRole = role,
            ClausesSummary = BuildContractClauseSummary(agent, false),
            RenewalStatus = "New signing terms",
            AgentMood = target.AgentMood,
            PlayerInterest = target.InterestSummary,
            BoardApproval = BuildContractBoardApproval(wage, role, false),
            PromiseSummary = $"{role} pathway before completion.",
            Status = "Draft",
            OutcomeSummary = "Offer prepared; no terms accepted yet.",
            IsAccepted = false
        };
    }

    private ContractOffer BuildRenewalContractOffer(SquadPlayer player)
    {
        var wage = Math.Clamp(player.Wage + Math.Max(5000, player.Wage / 5), player.Wage + 3000, player.Wage + 22000);
        var durationYears = player.Age >= 30 ? 1 : 2;
        var agent = player.IsStarting ? "Wage maximizer" : BuildAgentArchetype(player.Name, player.Personality);
        return new ContractOffer
        {
            OfferId = BuildOfferId(player.Name, "renewal"),
            PlayerName = player.Name,
            IsRenewal = true,
            SourceType = "Current-player renewal",
            AgentArchetype = agent,
            WageSummary = $"{FormatWeeklyWage(wage)} proposed; current wage {FormatWeeklyWage(player.Wage)}",
            ProposedWage = wage,
            DurationSummary = $"{durationYears} years",
            DurationYears = durationYears,
            ExpirySummary = $"Extends to {player.ContractExpiryYear + durationYears}",
            SquadRole = player.ContractRole,
            ClausesSummary = BuildContractClauseSummary(agent, true),
            RenewalStatus = "Renewal draft",
            AgentMood = player.Morale >= 66 ? "Open but watching squad direction." : "Concerned; wants trust rebuilt before signing.",
            PlayerInterest = player.Morale >= 66 ? "Player open to renewal if role is respected." : "Player interest is fragile because morale is low.",
            BoardApproval = BuildContractBoardApproval(wage, player.ContractRole, true),
            PromiseSummary = $"Contract renewal and {player.ContractRole.ToLowerInvariant()} role clarity.",
            Status = "Draft",
            OutcomeSummary = "Renewal terms prepared; no agreement yet.",
            IsAccepted = false
        };
    }

    private SquadPlayer? FindRenewalCandidate()
    {
        if (SquadPlayers.Length == 0)
        {
            return null;
        }

        var candidate = SquadPlayers[0];
        foreach (var player in SquadPlayers)
        {
            if (player.ContractExpiryYear < candidate.ContractExpiryYear ||
                (player.ContractExpiryYear == candidate.ContractExpiryYear && player.IsStarting && !candidate.IsStarting))
            {
                candidate = player;
            }
        }

        return candidate;
    }

    private ContractResolution ResolveContractOffer(ContractOffer offer)
    {
        var score = BuildContractApprovalScore(offer);
        if (score < 42)
        {
            var rejected = CloneContractOffer(
                offer,
                "Board rejected",
                $"Board rejected the terms: wage structure, role, and trust score {score}/100 are not aligned.",
                "Rejected by board",
                "Board rejected because wage structure risk is too high.");
            return new ContractResolution(rejected, 5, 4, -1, offer.IsRenewal ? -1 : 0);
        }

        if (offer.IsRenewal &&
            (offer.AgentArchetype == "Wage maximizer" || offer.AgentArchetype == "Release-clause specialist"))
        {
            var countered = CloneContractOffer(
                offer,
                "Agent countered",
                $"Agent countered: {offer.AgentArchetype.ToLowerInvariant()} wants stronger wage or clause protection despite board score {score}/100.",
                "Countered by agent",
                "Board allows a revised offer inside wage structure.");
            return new ContractResolution(countered, 3, 2, 0, offer.IsRenewal ? 0 : 0);
        }

        var accepted = CloneContractOffer(
            offer,
            "Accepted",
            $"Terms accepted: wage, duration, role, clauses, player interest, and board approval score {score}/100 aligned.",
            "Accepted",
            "Board approved within wage structure.",
            isAccepted: true);
        return new ContractResolution(accepted, offer.IsRenewal ? -1 : 2, 1, offer.IsRenewal ? 0 : 1, offer.IsRenewal ? 2 : 0);
    }

    private int BuildContractApprovalScore(ContractOffer offer)
    {
        var score = 50 + CareerProfile.BoardTrust / 5 + CareerProfile.PlayerTrust / 10;
        var highestWage = GetHighestSquadWage();
        if (offer.ProposedWage > highestWage * 13 / 10)
        {
            score -= 18;
        }

        if (offer.ProposedWage > highestWage * 2)
        {
            score -= 35;
        }

        if (CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard)
        {
            score -= offer.ProposedWage > highestWage ? 10 : 2;
        }

        if (offer.SquadRole.Contains("Important", StringComparison.Ordinal) && !offer.PlayerInterest.Contains("open", StringComparison.OrdinalIgnoreCase))
        {
            score -= 8;
        }

        if (!offer.IsRenewal && CurrentRecruitmentTarget?.TargetStatus == "Approved")
        {
            score += 8;
        }

        if (offer.IsRenewal && offer.AgentMood.Contains("Open", StringComparison.OrdinalIgnoreCase))
        {
            score += 6;
        }

        score -= BuildFinanceContractPenalty(offer);
        return Math.Clamp(score, 0, 100);
    }

    private int GetHighestSquadWage()
    {
        var highest = 1;
        foreach (var player in SquadPlayers)
        {
            highest = Math.Max(highest, player.Wage);
        }

        return highest;
    }

    private string BuildContractBoardApproval(int wage, string role, bool isRenewal)
    {
        var highestWage = GetHighestSquadWage();
        var wageLine = wage > highestWage * 13 / 10
            ? "breaks current wage structure"
            : "fits current wage structure";
        var boardLine = CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard
            ? "strict board scrutiny applies"
            : "board approval depends on trust and role logic";
        var source = isRenewal ? "renewal" : "signing";
        return $"{source} {wageLine}; {role}; {boardLine}.";
    }

    private static string BuildContractClauseSummary(string agentArchetype, bool isRenewal)
    {
        return agentArchetype switch
        {
            "Wage maximizer" => "Appearance bonus and wage-review clause.",
            "Release-clause specialist" => "Release-clause request and loyalty bonus placeholder.",
            "Career planner" => isRenewal ? "Role review and optional renewal year." : "Development review and role pathway.",
            "Loyalty builder" => "Modest loyalty bonus and squad-role review.",
            _ => "Standard bonus and role review."
        };
    }

    private static string BuildAgentArchetype(string playerName, string context)
    {
        if (context.Contains("ambitious", StringComparison.OrdinalIgnoreCase))
        {
            return "Release-clause specialist";
        }

        if (context.Contains("pathway", StringComparison.OrdinalIgnoreCase) || context.Contains("development", StringComparison.OrdinalIgnoreCase))
        {
            return "Career planner";
        }

        var hash = BuildStableTextHash(playerName);
        return (hash % 4) switch
        {
            0 => "Pragmatic agent",
            1 => "Wage maximizer",
            2 => "Career planner",
            _ => "Loyalty builder"
        };
    }

    private static int EstimateWageFromSummary(string wageSummary, int fallback)
    {
        var digits = string.Empty;
        foreach (var character in wageSummary)
        {
            if (char.IsDigit(character))
            {
                digits += character;
            }
            else if (digits.Length > 0)
            {
                break;
            }
        }

        return int.TryParse(digits, out var wageInThousands) && wageInThousands > 0
            ? wageInThousands * 1000
            : fallback;
    }

    private static int BuildStableTextHash(string value)
    {
        var hash = 17;
        foreach (var character in value)
        {
            hash = hash * 31 + character;
        }

        return Math.Abs(hash);
    }

    private static string BuildOfferId(string playerName, string source)
    {
        return $"{source}-{BuildStableTextHash(playerName):x8}";
    }

    private static string FormatWeeklyWage(int wage)
    {
        return $"${wage / 1000}k/w";
    }

    private void RecordContractHistory(string detail)
    {
        _contractHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_contractHistory.Count > 12)
        {
            _contractHistory.RemoveAt(_contractHistory.Count - 1);
        }
    }

    private void AddContractPromiseIfAccepted(ContractOffer offer)
    {
        if (!offer.IsAccepted)
        {
            return;
        }

        _promiseRecords.Add(new PromiseRecord
        {
            PromiseType = offer.IsRenewal ? "Contract renewal" : "Squad role",
            Recipient = offer.PlayerName,
            Source = offer.SourceType,
            IsPublic = false,
            ExpectedAction = offer.PromiseSummary,
            DeadlineSummary = offer.IsRenewal ? "Next contract review" : "Before integration review",
            DaysRemaining = offer.IsRenewal ? 28 : 21,
            Status = PromiseStatus.Active,
            CurrentEvidence = $"Promise created from accepted contract terms: {offer.SquadRole}.",
            AgentMood = offer.AgentMood,
            ConsequenceRisk = "Broken contract promises affect player trust, agent mood, squad trust, and pressure."
        });
    }

    private static ContractOffer CloneContractOffer(
        ContractOffer offer,
        string status,
        string outcomeSummary,
        string renewalStatus,
        string? boardApproval = null,
        bool? isAccepted = null)
    {
        return new ContractOffer
        {
            OfferId = offer.OfferId,
            PlayerName = offer.PlayerName,
            IsRenewal = offer.IsRenewal,
            SourceType = offer.SourceType,
            AgentArchetype = offer.AgentArchetype,
            WageSummary = offer.WageSummary,
            ProposedWage = offer.ProposedWage,
            DurationSummary = offer.DurationSummary,
            DurationYears = offer.DurationYears,
            ExpirySummary = offer.ExpirySummary,
            SquadRole = offer.SquadRole,
            ClausesSummary = offer.ClausesSummary,
            RenewalStatus = renewalStatus,
            AgentMood = status == "Agent countered" ? "Countering for stronger terms." : offer.AgentMood,
            PlayerInterest = offer.PlayerInterest,
            BoardApproval = boardApproval ?? offer.BoardApproval,
            PromiseSummary = offer.PromiseSummary,
            Status = status,
            OutcomeSummary = outcomeSummary,
            IsAccepted = isAccepted ?? offer.IsAccepted
        };
    }

    private static ContractOffer CloneContractOfferWithWage(ContractOffer offer, int proposedWage, string wageSummary)
    {
        return new ContractOffer
        {
            OfferId = offer.OfferId,
            PlayerName = offer.PlayerName,
            IsRenewal = offer.IsRenewal,
            SourceType = offer.SourceType,
            AgentArchetype = offer.AgentArchetype,
            WageSummary = wageSummary,
            ProposedWage = proposedWage,
            DurationSummary = offer.DurationSummary,
            DurationYears = offer.DurationYears,
            ExpirySummary = offer.ExpirySummary,
            SquadRole = offer.SquadRole,
            ClausesSummary = offer.ClausesSummary,
            RenewalStatus = offer.RenewalStatus,
            AgentMood = offer.AgentMood,
            PlayerInterest = offer.PlayerInterest,
            BoardApproval = offer.BoardApproval,
            PromiseSummary = offer.PromiseSummary,
            Status = offer.Status,
            OutcomeSummary = offer.OutcomeSummary,
            IsAccepted = offer.IsAccepted
        };
    }

    private void EnsureStaffImpactState()
    {
        if (string.IsNullOrWhiteSpace(StaffReportSummary) || StaffReportSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            StaffReportSummary = BuildStaffReportSummary();
        }

        if (CurrentStaffMarketCandidate == null)
        {
            CurrentStaffMarketCandidate = BuildStaffMarketCandidate();
        }

        if (string.IsNullOrWhiteSpace(StaffMarketSummary) || StaffMarketSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            StaffMarketSummary = CurrentStaffMarketCandidate == null
                ? "Staff market unavailable."
                : $"{CurrentStaffMarketCandidate.Name}, {CareerFoundation.GetDisplayName(CurrentStaffMarketCandidate.Role)} | quality {CurrentStaffMarketCandidate.Quality} | wage {CurrentStaffMarketCandidate.Wage}/w | {CurrentStaffMarketCandidate.InterestSummary} | {CurrentStaffMarketCandidate.BoardApproval} | {CurrentStaffMarketCandidate.Status}";
        }
    }

    private string BuildStaffImpactSummary()
    {
        EnsureStaffImpactState();
        var history = _staffHistory.Count == 0
            ? "Staff history starts when reports, requests, hires, rejections, poaching, or leaving events are recorded."
            : string.Join("\n", _staffHistory);
        return $"{StaffReportSummary}\nStaff market: {StaffMarketSummary}\nStaff history\n{history}";
    }

    private string BuildStaffReportSummary()
    {
        var training = (GetStaffQuality(StaffRole.FirstTeamCoach) + GetStaffQuality(StaffRole.AssistantManager)) / 2;
        var scouting = (GetStaffQuality(StaffRole.Scout) + GetStaffQuality(StaffRole.HeadOfRecruitment) + GetStaffQuality(StaffRole.DataAnalyst)) / 3;
        var risk = (GetStaffQuality(StaffRole.FitnessCoach) + GetStaffQuality(StaffRole.Physio)) / 2;
        var tactics = (GetStaffQuality(StaffRole.FirstTeamCoach) + GetStaffQuality(StaffRole.DataAnalyst)) / 2;
        var morale = (CareerProfile.StaffTrust + GetStaffQuality(StaffRole.AssistantManager)) / 2;
        var media = (GetStaffQuality(StaffRole.MediaOfficer) + MediaTrust) / 2;
        var recruitment = (GetStaffQuality(StaffRole.HeadOfRecruitment) + GetStaffQuality(StaffRole.Scout) + CareerProfile.DirectorTrust) / 3;
        return $"Staff report | training {training}, scouting {scouting}, injury risk control {risk}, tactical analysis {tactics}, morale support {morale}, media risk control {media}, recruitment support {recruitment}.";
    }

    private StaffMarketCandidate? BuildStaffMarketCandidate()
    {
        if (CurrentClub == null || CurrentClub.Staff.Length == 0)
        {
            return null;
        }

        var targetRole = FindWeakestStaffRole();
        var currentQuality = GetStaffQuality(targetRole);
        var quality = Math.Clamp(currentQuality + 8 + CareerProfile.Reputation / 12, 45, 88);
        var wage = 6000 + quality * 210 + (CareerProfile.Reputation * 60);
        var name = targetRole switch
        {
            StaffRole.FitnessCoach => "Mira Voss",
            StaffRole.Scout => "Tomas Iliev",
            StaffRole.DataAnalyst => "Lea Novak",
            StaffRole.HeadOfRecruitment => "Anika Sato",
            StaffRole.MediaOfficer => "Nora Vale",
            _ => "Dario Kelm"
        };
        var preferredStyle = targetRole switch
        {
            StaffRole.DataAnalyst => "Evidence-led",
            StaffRole.Scout => "Scouting network",
            StaffRole.FitnessCoach => "Recovery discipline",
            StaffRole.HeadOfRecruitment => "Value recruitment",
            StaffRole.MediaOfficer => "Calm communication",
            _ => TeamStyleName
        };
        return new StaffMarketCandidate
        {
            Name = name,
            Role = targetRole,
            Quality = quality,
            Wage = wage,
            ContractExpiryYear = CurrentDate.Year + 2,
            Reputation = Math.Clamp(quality + 4, 0, 100),
            Loyalty = Math.Clamp(48 + CareerProfile.StaffTrust / 4, 0, 100),
            Ambition = Math.Clamp(42 + quality / 3, 0, 100),
            PreferredStyle = preferredStyle,
            Relationship = "Interested but needs role clarity.",
            InterestSummary = $"Interest depends on club reputation {ClubReputation}, staff trust {CareerProfile.StaffTrust}, and wage fit.",
            BoardApproval = BuildStaffBoardApproval(wage, quality),
            Status = "Available",
            OutcomeSummary = "Candidate available for a bounded staff market action."
        };
    }

    private StaffRole FindWeakestStaffRole()
    {
        var targetRole = StaffRole.FirstTeamCoach;
        var weakestQuality = 101;
        foreach (var staff in CurrentClub?.Staff ?? Array.Empty<StaffMember>())
        {
            if (staff.Role is StaffRole.AssistantManager)
            {
                continue;
            }

            if (staff.Quality < weakestQuality)
            {
                weakestQuality = staff.Quality;
                targetRole = staff.Role;
            }
        }

        return targetRole;
    }

    private string BuildStaffBoardApproval(int wage, int quality)
    {
        var strict = CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard ? "strict board wage review applies" : "board checks wage against staff quality";
        var value = quality >= 68 ? "quality upgrade is credible" : "upgrade case is modest";
        return $"{value}; wage {wage}/w; {strict}; board trust {CareerProfile.BoardTrust}/100.";
    }

    private int BuildStaffHiringApprovalScore(StaffMarketCandidate candidate)
    {
        var score = 42 + CareerProfile.BoardTrust / 4 + CareerProfile.StaffTrust / 5 + Math.Max(0, candidate.Quality - GetStaffQuality(candidate.Role));
        if (candidate.Wage > 22000)
        {
            score -= 8;
        }

        if (candidate.Wage > 35000)
        {
            score -= 45;
        }

        if (CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard)
        {
            score -= candidate.Wage > 18000 ? 12 : 4;
        }

        if (CareerProfile.Role == ManagerRole.Manager)
        {
            score += 8;
        }

        return Math.Clamp(score, 0, 100);
    }

    private void HireStaffCandidate(StaffMarketCandidate candidate)
    {
        if (CurrentClub == null)
        {
            return;
        }

        var replacement = new StaffMember
        {
            Name = candidate.Name,
            Role = candidate.Role,
            Quality = candidate.Quality,
            InfluenceSummary = $"Hired staff upgrade: {candidate.PreferredStyle}; {candidate.OutcomeSummary}",
            ContractExpiryYear = candidate.ContractExpiryYear,
            Wage = candidate.Wage,
            Reputation = candidate.Reputation,
            Loyalty = candidate.Loyalty,
            Ambition = candidate.Ambition,
            PreferredStyle = candidate.PreferredStyle,
            Relationship = candidate.Relationship
        };
        var replaceIndex = -1;
        for (var index = 0; index < CurrentClub.Staff.Length; index++)
        {
            if (CurrentClub.Staff[index].Role == candidate.Role)
            {
                replaceIndex = index;
                break;
            }
        }

        if (replaceIndex < 0)
        {
            replaceIndex = 0;
            for (var index = 1; index < CurrentClub.Staff.Length; index++)
            {
                if (CurrentClub.Staff[index].Quality < CurrentClub.Staff[replaceIndex].Quality)
                {
                    replaceIndex = index;
                }
            }
        }

        CurrentClub.Staff[replaceIndex] = replacement;
    }

    private static SaveSlotStaffMarketCandidateData BuildStaffMarketCandidateSaveData(StaffMarketCandidate candidate)
    {
        return new SaveSlotStaffMarketCandidateData
        {
            Name = candidate.Name,
            RoleName = CareerFoundation.GetDisplayName(candidate.Role),
            Quality = candidate.Quality,
            Wage = candidate.Wage,
            ContractExpiryYear = candidate.ContractExpiryYear,
            Reputation = candidate.Reputation,
            Loyalty = candidate.Loyalty,
            Ambition = candidate.Ambition,
            PreferredStyle = candidate.PreferredStyle,
            Relationship = candidate.Relationship,
            InterestSummary = candidate.InterestSummary,
            BoardApproval = candidate.BoardApproval,
            Status = candidate.Status,
            OutcomeSummary = candidate.OutcomeSummary
        };
    }

    private static StaffMarketCandidate RestoreStaffMarketCandidate(SaveSlotStaffMarketCandidateData data)
    {
        return new StaffMarketCandidate
        {
            Name = string.IsNullOrWhiteSpace(data.Name) ? "Unknown staff candidate" : data.Name,
            Role = CareerFoundation.ParseStaffRole(data.RoleName),
            Quality = Math.Clamp(data.Quality <= 0 ? 58 : data.Quality, 0, 100),
            Wage = data.Wage <= 0 ? 12000 : data.Wage,
            ContractExpiryYear = data.ContractExpiryYear <= 0 ? 2028 : data.ContractExpiryYear,
            Reputation = Math.Clamp(data.Reputation <= 0 ? 55 : data.Reputation, 0, 100),
            Loyalty = Math.Clamp(data.Loyalty <= 0 ? 55 : data.Loyalty, 0, 100),
            Ambition = Math.Clamp(data.Ambition <= 0 ? 45 : data.Ambition, 0, 100),
            PreferredStyle = string.IsNullOrWhiteSpace(data.PreferredStyle) ? "Balanced" : data.PreferredStyle,
            Relationship = string.IsNullOrWhiteSpace(data.Relationship) ? "Professional" : data.Relationship,
            InterestSummary = string.IsNullOrWhiteSpace(data.InterestSummary) ? "Interest restored from saved staff market." : data.InterestSummary,
            BoardApproval = string.IsNullOrWhiteSpace(data.BoardApproval) ? "Board approval restored from saved staff market." : data.BoardApproval,
            Status = string.IsNullOrWhiteSpace(data.Status) ? "Available" : data.Status,
            OutcomeSummary = string.IsNullOrWhiteSpace(data.OutcomeSummary) ? "Staff market candidate restored." : data.OutcomeSummary
        };
    }

    private static StaffMarketCandidate CloneStaffMarketCandidate(StaffMarketCandidate candidate, string status, string outcomeSummary)
    {
        return new StaffMarketCandidate
        {
            Name = candidate.Name,
            Role = candidate.Role,
            Quality = candidate.Quality,
            Wage = candidate.Wage,
            ContractExpiryYear = candidate.ContractExpiryYear,
            Reputation = candidate.Reputation,
            Loyalty = candidate.Loyalty,
            Ambition = candidate.Ambition,
            PreferredStyle = candidate.PreferredStyle,
            Relationship = candidate.Relationship,
            InterestSummary = candidate.InterestSummary,
            BoardApproval = candidate.BoardApproval,
            Status = status,
            OutcomeSummary = outcomeSummary
        };
    }

    private static StaffMarketCandidate CloneStaffMarketCandidateWithWage(StaffMarketCandidate candidate, int wage)
    {
        return new StaffMarketCandidate
        {
            Name = candidate.Name,
            Role = candidate.Role,
            Quality = candidate.Quality,
            Wage = wage,
            ContractExpiryYear = candidate.ContractExpiryYear,
            Reputation = candidate.Reputation,
            Loyalty = candidate.Loyalty,
            Ambition = candidate.Ambition,
            PreferredStyle = candidate.PreferredStyle,
            Relationship = candidate.Relationship,
            InterestSummary = candidate.InterestSummary,
            BoardApproval = $"High wage stress case: {wage}/w.",
            Status = candidate.Status,
            OutcomeSummary = candidate.OutcomeSummary
        };
    }

    private void RecordStaffHistory(string detail)
    {
        _staffHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_staffHistory.Count > 12)
        {
            _staffHistory.RemoveAt(_staffHistory.Count - 1);
        }
    }

    private void EnsureYouthAcademyState()
    {
        if (YouthAcademyQuality <= 0)
        {
            YouthAcademyQuality = BuildYouthAcademyQuality();
        }

        if (YouthRecruitmentReach <= 0)
        {
            YouthRecruitmentReach = BuildYouthRecruitmentReach();
        }

        if (YouthCoachingQuality <= 0)
        {
            YouthCoachingQuality = Math.Clamp(GetStaffQuality(StaffRole.YouthCoach), 0, 100);
        }

        if (string.IsNullOrWhiteSpace(YouthFacilitiesSummary) || YouthFacilitiesSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            YouthFacilitiesSummary = BuildYouthFacilitiesSummary();
        }

        if (string.IsNullOrWhiteSpace(YouthIntakeDateSummary) || YouthIntakeDateSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            YouthIntakeDateSummary = BuildYouthIntakeDateSummary();
        }

        if (string.IsNullOrWhiteSpace(YouthBoardExpectation) || YouthBoardExpectation.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            YouthBoardExpectation = BuildYouthBoardExpectation();
        }

        if (string.IsNullOrWhiteSpace(YouthFanExpectation) || YouthFanExpectation.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            YouthFanExpectation = BuildYouthFanExpectation();
        }
    }

    private string BuildYouthAcademySummary()
    {
        EnsureYouthAcademyState();
        var prospects = _youthProspects.Count == 0
            ? "No youth intake generated yet."
            : string.Join("\n", _youthProspects.ConvertAll(BuildYouthProspectLine));
        var history = _youthHistory.Count == 0
            ? "Youth history starts after intake, review, promotion, or loan planning."
            : string.Join("\n", _youthHistory);
        return $"Academy quality {YouthAcademyQuality} | recruitment reach {YouthRecruitmentReach} | coaching {YouthCoachingQuality}\nFacilities: {YouthFacilitiesSummary}\nIntake: {YouthIntakeDateSummary}\nBoard: {YouthBoardExpectation}\nFans: {YouthFanExpectation}\nProspects\n{prospects}\nYouth history\n{history}";
    }

    private string BuildYouthProspectLine(YouthProspect prospect)
    {
        return $"{prospect.Name} ({prospect.Age}, {prospect.Position}, {prospect.Region}) | {prospect.Status} | {prospect.VisibleInfo} | hidden potential {prospect.HiddenPotentialBand} ({prospect.PotentialCertainty}% certainty) | {prospect.PlayingStyle} | {prospect.Personality} | {prospect.DevelopmentCurve} | loan: {prospect.LoanSuitability}";
    }

    private int BuildYouthAcademyQuality()
    {
        var archetypeBonus = CurrentClub?.Archetype == ClubArchetype.YouthAcademyClub ? 18 : 0;
        var boardBonus = CurrentClub?.BoardPhilosophy == BoardPhilosophy.YouthDevelopmentBoard ? 8 : 0;
        return Math.Clamp(42 + GetStaffQuality(StaffRole.YouthCoach) / 3 + archetypeBonus + boardBonus, 0, 100);
    }

    private int BuildYouthRecruitmentReach()
    {
        var directorBonus = CurrentClub?.DirectorOfFootballStyle == DirectorOfFootballStyle.AcademyBuilder ? 12 : 0;
        return Math.Clamp(40 + GetStaffQuality(StaffRole.Scout) / 4 + GetStaffQuality(StaffRole.HeadOfRecruitment) / 5 + directorBonus, 0, 100);
    }

    private string BuildYouthFacilitiesSummary()
    {
        return YouthAcademyQuality >= 75
            ? "Strong academy setup with meaningful first-team pathway."
            : YouthAcademyQuality >= 58
                ? "Functional academy setup; standout prospects remain rare."
                : "Limited academy setup; pathway needs staff and facility support.";
    }

    private string BuildYouthIntakeDateSummary()
    {
        return $"Next intake review around {CurrentDate.AddDays(28):yyyy-MM-dd}; early review can generate a small foundation intake.";
    }

    private string BuildYouthBoardExpectation()
    {
        return CurrentClub?.BoardPhilosophy == BoardPhilosophy.YouthDevelopmentBoard
            ? "Board expects visible academy pathway without sacrificing results."
            : "Board treats academy use as positive if first-team standards hold.";
    }

    private string BuildYouthFanExpectation()
    {
        return CurrentClub?.FanCulture == FanCulture.AcademyLoyalists
            ? "Fans respond strongly to credible academy minutes."
            : "Fans welcome academy stories if performances stay credible.";
    }

    private void GenerateYouthIntake()
    {
        EnsureYouthAcademyState();
        _youthProspects.Clear();
        for (var index = 0; index < 3; index++)
        {
            _youthProspects.Add(BuildYouthProspect(index));
        }

        YouthIntakeDateSummary = $"{CurrentDateLabel}: foundation youth intake generated; hidden potential remains partially unknown.";
        YouthReputation = Math.Clamp(YouthReputation + 2, 0, 100);
        RecordYouthHistory($"Youth intake generated with {_youthProspects.Count} prospects; academy quality {YouthAcademyQuality}, reach {YouthRecruitmentReach}, coaching {YouthCoachingQuality}.");
        AddNews(
            "Youth intake reviewed",
            NewsCategory.Club,
            "Academy report",
            $"{SelectedClubName} reviewed {_youthProspects.Count} academy prospects with hidden potential still uncertain.",
            4,
            sourceType: "Academy staff",
            relatedEntity: SelectedClubName ?? "academy",
            effectSummary: $"Youth reputation {YouthReputation}; board/fan expectations now visible.",
            cooldownKey: "youth-intake");
    }

    private YouthProspect BuildYouthProspect(int index)
    {
        var positions = new[] { "CM", "CB", "LW", "ST", "RB", "AM" };
        var firstNames = new[] { "Milo", "Ivo", "Tariq", "Noel", "Sami", "Ren" };
        var lastNames = new[] { "Vale", "Koric", "Amani", "Ilic", "Soren", "Dalo" };
        var styles = new[] { "Tempo-setting prospect", "Front-foot defender", "Direct runner", "Pressing forward", "Inverted fullback", "Between-lines creator" };
        var personalities = new[] { "Grounded", "Driven", "Quiet learner", "Confident", "Resilient", "Raw but receptive" };
        var seed = BuildStableTextHash($"{WorldSeed}|{SelectedClubName}|youth|{index}");
        var position = positions[Math.Abs(seed + index) % positions.Length];
        var name = $"{firstNames[Math.Abs(seed) % firstNames.Length]} {lastNames[Math.Abs(seed / 7 + index) % lastNames.Length]}";
        var certainty = Math.Clamp(35 + YouthCoachingQuality / 4 + GetStaffQuality(StaffRole.DataAnalyst) / 5 - index * 5, 20, 85);
        var potentialTop = Math.Clamp(62 + YouthAcademyQuality / 4 + YouthRecruitmentReach / 6 - index * 3, 58, 88);
        var potentialLow = Math.Max(45, potentialTop - (certainty >= 65 ? 8 : 16));
        return new YouthProspect
        {
            ProspectId = $"youth-{BuildStableTextHash(name):x8}-{index}",
            Name = name,
            Age = 16 + Math.Abs(seed + index) % 3,
            Position = position,
            Region = SelectedClubName ?? "Local academy",
            PlayingStyle = styles[Math.Abs(seed / 11 + index) % styles.Length],
            Personality = personalities[Math.Abs(seed / 13 + index) % personalities.Length],
            VisibleInfo = $"Known: role {position}, age, broad style. Estimated: current ability {46 + index * 2}-{58 + index * 2}. Unknown: exact potential ?, pressure response ?.",
            HiddenPotentialBand = $"{potentialLow}-{potentialTop}",
            PotentialCertainty = certainty,
            DevelopmentCurve = potentialTop >= 78 ? "High-upside but needs careful minutes." : "Steady development path if training and loan fit are credible.",
            LoanSuitability = potentialTop >= 72 ? "Future loan could help after senior promotion if minutes are guaranteed." : "Keep in academy/senior training before loan review.",
            IsPromoted = false,
            Status = "Academy prospect"
        };
    }

    private string PromoteYouthProspect()
    {
        for (var index = 0; index < _youthProspects.Count; index++)
        {
            var prospect = _youthProspects[index];
            if (prospect.IsPromoted)
            {
                continue;
            }

            if (CareerProfile.Role == ManagerRole.AssistantManager)
            {
                _youthProspects[index] = CloneYouthProspect(prospect, false, "Promotion recommended by Assistant Manager");
                RecordYouthHistory($"{prospect.Name}: promotion recommended; Assistant Manager authority cannot finalize senior registration.");
                AddNews(
                    "Youth promotion recommended",
                    NewsCategory.Club,
                    "Academy report",
                    $"{ManagerName} recommended {prospect.Name} for senior review.",
                    2);
                return "Assistant Manager youth recommendation logged; senior promotion not finalized.";
            }

            PromoteProspectToSeniorSquad(prospect);
            _youthProspects[index] = CloneYouthProspect(prospect, true, "Promoted to senior squad");
            var boardDelta = CurrentClub?.BoardPhilosophy == BoardPhilosophy.YouthDevelopmentBoard ? 2 : 1;
            var fanDelta = CurrentClub?.FanCulture == FanCulture.AcademyLoyalists ? 3 : 1;
            BoardConfidence = Math.Clamp(BoardConfidence + boardDelta, 0, 100);
            FanSentiment = Math.Clamp(FanSentiment + fanDelta, 0, 100);
            YouthReputation = Math.Clamp(YouthReputation + 3, 0, 100);
            SyncCurrentClubMoraleFromRuntime();
            RecordYouthHistory($"{prospect.Name}: promoted to senior squad; board +{boardDelta}, fans +{fanDelta}, youth reputation {YouthReputation}.");
            AddNews(
                "Academy prospect promoted",
                NewsCategory.Club,
                "Academy report",
                $"{prospect.Name} joined the senior squad from the academy pathway.",
                4,
                sourceType: "Academy staff",
                relatedEntity: prospect.Name,
                effectSummary: $"Board morale {BoardMorale}; fan morale {FanMorale}; youth reputation {YouthReputation}.",
                cooldownKey: "youth-promotion");
            SquadStatusSummary = BuildSquadStatusSummary();
            return $"{prospect.Name} promoted to the senior squad with loan suitability noted: {prospect.LoanSuitability}";
        }

        return "No unpromoted youth prospects available.";
    }

    private void PromoteProspectToSeniorSquad(YouthProspect prospect)
    {
        var abilityTop = ExtractPotentialTop(prospect.HiddenPotentialBand);
        var currentAbility = Math.Clamp(48 + YouthAcademyQuality / 10 + prospect.PotentialCertainty / 12, 42, Math.Max(55, abilityTop - 8));
        var seniorPlayer = new SquadPlayer
        {
            PlayerId = prospect.ProspectId,
            Name = prospect.Name,
            Position = prospect.Position,
            Age = prospect.Age,
            Nationality = "Novaran",
            TrueAbility = currentAbility,
            TechnicalAttribute = Math.Clamp(currentAbility + 1, 0, 100),
            TacticalAttribute = Math.Clamp(currentAbility + YouthCoachingQuality / 20, 0, 100),
            PhysicalAttribute = Math.Clamp(currentAbility - 1, 0, 100),
            MentalAttribute = Math.Clamp(currentAbility + prospect.PotentialCertainty / 20, 0, 100),
            KnownAttributesSummary = $"Known: academy role {prospect.Position}, fitness baseline, training response.",
            EstimatedAttributesSummary = $"Estimated: current ability {Math.Max(40, currentAbility - 4)}-{currentAbility + 5}, potential {prospect.HiddenPotentialBand}.",
            UnknownAttributesSummary = "Unknown: exact hidden potential ?, senior pressure response ?, agent loyalty ?.",
            PlayingStyle = prospect.PlayingStyle,
            Tendencies = "Learning senior habits.",
            Traits = "academy graduate, coachable",
            Personality = prospect.Personality,
            TacticalFit = $"Academy pathway fit for {TeamStyleName}; senior role still estimated.",
            DevelopmentCurve = prospect.DevelopmentCurve,
            Form = 58,
            Morale = 68,
            Fitness = 82,
            Fatigue = 10,
            InjuryRisk = 18,
            Wage = 3500 + currentAbility * 80,
            ContractExpiryYear = CurrentDate.Year + 2,
            ContractRole = "Youth Prospect",
            Relationship = "Academy pathway",
            PromiseSummary = "Promotion pathway promise active; senior minutes not guaranteed.",
            TransferInterest = "Loan development suitability recorded.",
            TacticalFitScore = Math.Clamp(55 + YouthCoachingQuality / 4, 0, 100),
            PlayerFamiliarity = 70,
            ScoutingConfidence = prospect.PotentialCertainty,
            KnownAttributeGroups = "role,style,fitness",
            EstimatedAttributeGroups = "technical,tactical,physical,mental,potential",
            UnknownAttributeGroups = "hidden potential,pressure response,agent loyalty",
            IsStarting = false
        };
        var updated = new SquadPlayer[SquadPlayers.Length + 1];
        Array.Copy(SquadPlayers, updated, SquadPlayers.Length);
        updated[^1] = seniorPlayer;
        SquadPlayers = updated;
    }

    private static int ExtractPotentialTop(string potentialBand)
    {
        var parts = potentialBand.Split('-', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length == 2 && int.TryParse(parts[1], out var top) ? top : 70;
    }

    private static YouthProspect CloneYouthProspect(YouthProspect prospect, bool isPromoted, string status)
    {
        return new YouthProspect
        {
            ProspectId = prospect.ProspectId,
            Name = prospect.Name,
            Age = prospect.Age,
            Position = prospect.Position,
            Region = prospect.Region,
            PlayingStyle = prospect.PlayingStyle,
            Personality = prospect.Personality,
            VisibleInfo = prospect.VisibleInfo,
            HiddenPotentialBand = prospect.HiddenPotentialBand,
            PotentialCertainty = prospect.PotentialCertainty,
            DevelopmentCurve = prospect.DevelopmentCurve,
            LoanSuitability = prospect.LoanSuitability,
            IsPromoted = isPromoted,
            Status = status
        };
    }

    private static SaveSlotYouthProspectData BuildYouthProspectSaveData(YouthProspect prospect)
    {
        return new SaveSlotYouthProspectData
        {
            ProspectId = prospect.ProspectId,
            Name = prospect.Name,
            Age = prospect.Age,
            Position = prospect.Position,
            Region = prospect.Region,
            PlayingStyle = prospect.PlayingStyle,
            Personality = prospect.Personality,
            VisibleInfo = prospect.VisibleInfo,
            HiddenPotentialBand = prospect.HiddenPotentialBand,
            PotentialCertainty = prospect.PotentialCertainty,
            DevelopmentCurve = prospect.DevelopmentCurve,
            LoanSuitability = prospect.LoanSuitability,
            IsPromoted = prospect.IsPromoted,
            Status = prospect.Status
        };
    }

    private static YouthProspect RestoreYouthProspect(SaveSlotYouthProspectData data)
    {
        return new YouthProspect
        {
            ProspectId = string.IsNullOrWhiteSpace(data.ProspectId) ? $"youth-{BuildStableTextHash(data.Name):x8}" : data.ProspectId,
            Name = string.IsNullOrWhiteSpace(data.Name) ? "Unknown academy prospect" : data.Name,
            Age = data.Age <= 0 ? 17 : data.Age,
            Position = string.IsNullOrWhiteSpace(data.Position) ? "CM" : data.Position,
            Region = string.IsNullOrWhiteSpace(data.Region) ? "Local academy" : data.Region,
            PlayingStyle = string.IsNullOrWhiteSpace(data.PlayingStyle) ? "Balanced academy prospect" : data.PlayingStyle,
            Personality = string.IsNullOrWhiteSpace(data.Personality) ? "Grounded" : data.Personality,
            VisibleInfo = string.IsNullOrWhiteSpace(data.VisibleInfo) ? "Known: academy role. Estimated: current ability range. Unknown: potential ?." : data.VisibleInfo,
            HiddenPotentialBand = string.IsNullOrWhiteSpace(data.HiddenPotentialBand) ? "55-70" : data.HiddenPotentialBand,
            PotentialCertainty = Math.Clamp(data.PotentialCertainty <= 0 ? 45 : data.PotentialCertainty, 0, 100),
            DevelopmentCurve = string.IsNullOrWhiteSpace(data.DevelopmentCurve) ? "Development path pending." : data.DevelopmentCurve,
            LoanSuitability = string.IsNullOrWhiteSpace(data.LoanSuitability) ? "Loan suitability pending." : data.LoanSuitability,
            IsPromoted = data.IsPromoted,
            Status = string.IsNullOrWhiteSpace(data.Status) ? "Academy prospect" : data.Status
        };
    }

    private void RecordYouthHistory(string detail)
    {
        _youthHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_youthHistory.Count > 12)
        {
            _youthHistory.RemoveAt(_youthHistory.Count - 1);
        }
    }

    private void EnsurePlayerDevelopmentState()
    {
        if (string.IsNullOrWhiteSpace(PlayerDevelopmentSummary) || PlayerDevelopmentSummary.Contains("pending", StringComparison.OrdinalIgnoreCase))
        {
            PlayerDevelopmentSummary = BuildInitialPlayerDevelopmentSummary();
        }
    }

    private void ApplyPlayerDevelopmentProgress()
    {
        EnsurePlayerDevelopmentState();
        if (SquadPlayers.Length == 0)
        {
            PlayerDevelopmentSummary = "Development cadence unavailable: no squad players.";
            return;
        }

        var staffScore = BuildDevelopmentStaffScore();
        var update = DevelopmentSystem.ApplyWeeklyDevelopment(
            SquadPlayers,
            SelectedClubName ?? string.Empty,
            WorldSeed,
            CurrentDate,
            TrainingFocusName,
            TrainingIntensityName,
            staffScore,
            YouthAcademyQuality,
            YouthCoachingQuality);
        SquadPlayers = update.SquadPlayers;
        PlayerDevelopmentSummary = update.Summary;
        SquadStatusSummary = BuildSquadStatusSummary();
        foreach (var entry in update.HistoryEntries)
        {
            RecordPlayerDevelopmentHistory($"Weekly development: {entry}");
        }

        AddNews(
            "Player development update",
            NewsCategory.Training,
            "Staff report",
            update.Summary,
            3,
            sourceType: "Coaching staff",
            relatedEntity: SelectedClubName ?? "squad",
            effectSummary: "Training, minutes, age, staff quality, morale, fatigue, injury risk, and loan pathway cues applied.",
            cooldownKey: "player-development-weekly");
    }

    private void RecordSeasonDevelopmentSnapshot()
    {
        EnsurePlayerDevelopmentState();
        var youngCount = 0;
        var seniorCount = 0;
        var riskCount = 0;
        foreach (var player in SquadPlayers)
        {
            if (player.Age <= 22)
            {
                youngCount++;
            }

            if (player.Age >= 30)
            {
                seniorCount++;
            }

            if (player.InjuryRisk >= 30 || player.Fatigue >= 35)
            {
                riskCount++;
            }
        }

        PlayerDevelopmentSummary = $"Season development review | young players {youngCount}, senior decline watch {seniorCount}, condition risks {riskCount}. Ages, ability movement, condition, and development notes updated during rollover.";
        RecordPlayerDevelopmentHistory(PlayerDevelopmentSummary);
        AddNews(
            "Season development review",
            NewsCategory.Training,
            "Staff report",
            PlayerDevelopmentSummary,
            4,
            sourceType: "Coaching staff",
            relatedEntity: SelectedClubName ?? "squad",
            effectSummary: "Age, development curve, condition, and senior decline watch updated.",
            cooldownKey: "player-development-season");
    }

    private string BuildInitialPlayerDevelopmentSummary()
    {
        var youngCount = 0;
        var seniorCount = 0;
        foreach (var player in SquadPlayers)
        {
            if (player.Age <= 21)
            {
                youngCount++;
            }

            if (player.Age >= 30)
            {
                seniorCount++;
            }
        }

        return $"Development cadence | squad {SquadPlayers.Length}, young pathway players {youngCount}, senior decline watch {seniorCount}, staff score {BuildDevelopmentStaffScore()}. Weekly updates use training focus, minutes, morale, fatigue, injury risk, loan cues, and staff quality.";
    }

    private int BuildDevelopmentStaffScore()
    {
        return Math.Clamp(
            (GetStaffQuality(StaffRole.FirstTeamCoach) +
            GetStaffQuality(StaffRole.YouthCoach) +
            GetStaffQuality(StaffRole.FitnessCoach) +
            GetStaffQuality(StaffRole.Physio)) / 4,
            0,
            100);
    }

    private void RecordPlayerDevelopmentHistory(string detail)
    {
        _playerDevelopmentHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_playerDevelopmentHistory.Count > 16)
        {
            _playerDevelopmentHistory.RemoveAt(_playerDevelopmentHistory.Count - 1);
        }
    }

    private void EnsureFinanceState()
    {
        FinanceWageBudget = FinanceWageBudget <= 0 ? CurrentClub?.WageBudget ?? 1 : FinanceWageBudget;
        FinanceTransferBudgetRemaining = FinanceTransferBudgetRemaining <= 0 ? CurrentClub?.TransferBudget ?? 0 : FinanceTransferBudgetRemaining;
        FinanceDebt = FinanceDebt <= 0 ? BuildStartingDebt() : FinanceDebt;
        FinanceCurrentWageBill = CalculateCurrentWageBill();
        ProfitExpectationSummary = string.IsNullOrWhiteSpace(ProfitExpectationSummary) || ProfitExpectationSummary.Contains("pending", StringComparison.OrdinalIgnoreCase)
            ? BuildProfitExpectationSummary()
            : ProfitExpectationSummary;
        BoardFinanceActionSummary = string.IsNullOrWhiteSpace(BoardFinanceActionSummary) || BoardFinanceActionSummary.Contains("pending", StringComparison.OrdinalIgnoreCase)
            ? "Board finance action: no intervention; budget discipline monitored."
            : BoardFinanceActionSummary;
        RefreshFinanceProjection();
    }

    private void ApplyWeeklyFinanceProgress()
    {
        EnsureFinanceState();
        var ticketIncome = 60000 + FanMorale * 1200 + Math.Max(0, 10 - GetClubTablePosition(SelectedClubName ?? string.Empty)) * 2500;
        var commercialIncome = 35000 + ClubReputation * 1100 + (CurrentClub?.BoardPhilosophy == BoardPhilosophy.CommercialGrowthBoard ? 20000 : 0);
        FinanceTicketIncome += ticketIncome;
        FinanceCommercialIncome += commercialIncome;
        FinanceRevenue += ticketIncome + commercialIncome;
        FinanceExpenses += FinanceCurrentWageBill;
        RefreshFinanceProjection();

        if (FinanceProjectedBalance < 0)
        {
            var cut = Math.Min(Math.Abs(FinanceProjectedBalance) / 5, Math.Max(0, FinanceTransferBudgetRemaining / 5));
            FinanceBudgetCut += cut;
            FinanceTransferBudgetRemaining = Math.Max(0, FinanceTransferBudgetRemaining - cut);
            FinancialPressure = Math.Clamp(FinancialPressure + 3, 0, 100);
            BoardFinanceActionSummary = $"Board finance action: budget cut risk triggered; projected balance {FormatFinanceMoney(FinanceProjectedBalance)}, cut watch {FormatFinanceMoney(FinanceBudgetCut)}.";
        }
        else if (CareerProfile.BoardTrust >= 70 && FinanceProjectedBalance > 2500000 && CurrentClub?.BoardPhilosophy != BoardPhilosophy.FinanciallyStrictBoard)
        {
            var injection = Math.Min(250000, FinanceProjectedBalance / 20);
            FinanceBoardInjection += injection;
            FinanceTransferBudgetRemaining += injection;
            BoardFinanceActionSummary = $"Board finance action: modest injection {FormatFinanceMoney(injection)} after trust and projected balance review.";
        }

        RefreshFinanceProjection();
        RecordFinanceHistory($"Weekly finance: ticket {FormatFinanceMoney(ticketIncome)}, commercial {FormatFinanceMoney(commercialIncome)}, wage cost {FormatFinanceMoney(FinanceCurrentWageBill)}, projected {FormatFinanceMoney(FinanceProjectedBalance)}.");
    }

    private void ApplyRecruitmentFinanceImpact(RecruitmentTarget target)
    {
        EnsureFinanceState();
        if (target.IsLoanCandidate)
        {
            RecordFinanceHistory($"{target.PlayerName}: loan path reviewed; fee/wage impact remains conditional.");
            return;
        }

        var fee = EstimateMoneyFromRange(target.EstimatedFeeRange, 0);
        var wage = EstimateMoneyFromRange(target.EstimatedWageRange, 0);
        FinanceTransferBudgetRemaining = Math.Max(0, FinanceTransferBudgetRemaining - fee);
        FinanceTransferCommitments += fee;
        FinanceExpenses += fee;
        FinanceCurrentWageBill += wage;
        RefreshFinanceProjection();
        RecordFinanceHistory($"{target.PlayerName}: transfer approach reserved {FormatFinanceMoney(fee)} fee and {FormatFinanceMoney(wage)}/w wage; remaining transfer budget {FormatFinanceMoney(FinanceTransferBudgetRemaining)}.");
    }

    private bool CanFinanceRecruitmentTarget(RecruitmentTarget target)
    {
        EnsureFinanceState();
        if (target.IsLoanCandidate && target.LoanDirection == "Outgoing loan")
        {
            return true;
        }

        var fee = EstimateMoneyFromRange(target.EstimatedFeeRange, 0);
        var wage = EstimateMoneyFromRange(target.EstimatedWageRange, 0);
        return fee <= FinanceTransferBudgetRemaining &&
            FinanceCurrentWageBill + wage <= FinanceWageBudget * 11 / 10;
    }

    private void ApplyContractFinanceImpact(ContractOffer offer)
    {
        if (!offer.IsAccepted)
        {
            return;
        }

        EnsureFinanceState();
        var wageImpact = offer.IsRenewal
            ? Math.Max(0, offer.ProposedWage - GetHighestSquadWage() / 2)
            : offer.ProposedWage;
        FinanceCurrentWageBill += wageImpact;
        FinanceExpenses += wageImpact * 4;
        RefreshFinanceProjection();
        RecordFinanceHistory($"{offer.PlayerName}: accepted {offer.SourceType.ToLowerInvariant()} added {FormatFinanceMoney(wageImpact)}/w wage pressure; wage structure pressure {WageStructurePressure}/100.");
    }

    private void ApplyStaffFinanceImpact(StaffMarketCandidate candidate)
    {
        EnsureFinanceState();
        FinanceCurrentWageBill += candidate.Wage;
        FinanceExpenses += candidate.Wage * 4;
        RefreshFinanceProjection();
        RecordFinanceHistory($"{candidate.Name}: staff hire added {FormatFinanceMoney(candidate.Wage)}/w; projected balance {FormatFinanceMoney(FinanceProjectedBalance)}.");
    }

    private int BuildFinanceContractPenalty(ContractOffer offer)
    {
        EnsureFinanceState();
        var penalty = 0;
        if (FinanceCurrentWageBill + offer.ProposedWage > FinanceWageBudget)
        {
            penalty += 12;
        }

        if (offer.ProposedWage > GetHighestSquadWage() * 13 / 10)
        {
            penalty += 8;
        }

        if (CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard && WageStructurePressure >= 55)
        {
            penalty += 8;
        }

        return penalty;
    }

    private int BuildStartingDebt()
    {
        return CurrentClub?.Archetype switch
        {
            ClubArchetype.FinanciallyRestrictedClub => 1800000,
            ClubArchetype.FallenGiant => 900000,
            _ => CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard ? 600000 : 0
        };
    }

    private string BuildProfitExpectationSummary()
    {
        return CurrentClub?.BoardPhilosophy switch
        {
            BoardPhilosophy.FinanciallyStrictBoard => "Profit expectation: protect wage structure, avoid deficit, and justify every transfer commitment.",
            BoardPhilosophy.CommercialGrowthBoard => "Profit expectation: grow commercial income while keeping wage pressure visible.",
            BoardPhilosophy.YouthDevelopmentBoard => "Profit expectation: academy pathway and wage discipline should reduce transfer dependence.",
            _ => "Profit expectation: stay within transfer and wage budget while results support revenue."
        };
    }

    private void RefreshFinanceProjection()
    {
        FinanceCurrentWageBill = Math.Max(FinanceCurrentWageBill, CalculateCurrentWageBill());
        WageStructurePressure = Math.Clamp((FinanceCurrentWageBill * 100 / Math.Max(1, FinanceWageBudget)) - 80 + Math.Max(0, FinancialPressure - 40) / 2, 0, 100);
        FinanceProjectedBalance = FinanceTransferBudgetRemaining + FinanceRevenue + FinanceBoardInjection + FinancePrizeMoney - FinanceExpenses - FinanceTransferCommitments - FinanceDebt / 10 - FinanceBudgetCut;
        FinanceSummary = $"Transfer budget remaining {FormatFinanceMoney(FinanceTransferBudgetRemaining)} | wage bill {FormatFinanceMoney(FinanceCurrentWageBill)}/w of {FormatFinanceMoney(FinanceWageBudget)}/w | debt {FormatFinanceMoney(FinanceDebt)} | revenue {FormatFinanceMoney(FinanceRevenue)} | expenses {FormatFinanceMoney(FinanceExpenses)} | ticket income {FormatFinanceMoney(FinanceTicketIncome)} | commercial growth {FormatFinanceMoney(FinanceCommercialIncome)} | prize money {FormatFinanceMoney(FinancePrizeMoney)} | projected balance {FormatFinanceMoney(FinanceProjectedBalance)} | wage structure pressure {WageStructurePressure}/100 | {ProfitExpectationSummary} | {BoardFinanceActionSummary}";
    }

    private int CalculateCurrentWageBill()
    {
        var total = 0;
        foreach (var player in SquadPlayers)
        {
            total += player.Wage;
        }

        foreach (var staff in CurrentClub?.Staff ?? Array.Empty<StaffMember>())
        {
            total += staff.Wage;
        }

        return total;
    }

    private static int EstimateMoneyFromRange(string summary, int fallback)
    {
        var values = new List<int>();
        for (var index = 0; index < summary.Length; index++)
        {
            if (summary[index] != '$')
            {
                continue;
            }

            var end = index + 1;
            while (end < summary.Length && (char.IsDigit(summary[end]) || summary[end] == '.'))
            {
                end++;
            }

            if (end == index + 1 || !decimal.TryParse(summary[(index + 1)..end], out var number))
            {
                continue;
            }

            var multiplier = end < summary.Length && summary[end] == 'm' ? 1000000 : 1000;
            values.Add((int)(number * multiplier));
        }

        if (values.Count == 0)
        {
            return fallback;
        }

        var total = 0;
        foreach (var value in values)
        {
            total += value;
        }

        return total / values.Count;
    }

    private static string FormatFinanceMoney(int amount)
    {
        var sign = amount < 0 ? "-" : string.Empty;
        var value = Math.Abs(amount);
        return value >= 1000000
            ? $"{sign}${value / 1000000.0:0.0}m"
            : $"{sign}${value / 1000}k";
    }

    private void RecordFinanceHistory(string detail)
    {
        _financeHistory.Insert(0, $"{CurrentDateLabel}: {detail}");
        if (_financeHistory.Count > 16)
        {
            _financeHistory.RemoveAt(_financeHistory.Count - 1);
        }
    }

    private void GenerateContextDecisionEvent(string trigger)
    {
        var eventType = ResolveContextDecisionEventType();
        if (TryCreateDecisionEvent(eventType, trigger, out var decisionEvent))
        {
            _activeDecisionEvents.Add(decisionEvent);
            AddNews(
                decisionEvent.Title,
                NewsCategory.Pressure,
                decisionEvent.Reliability,
                decisionEvent.Prompt,
                decisionEvent.Importance,
                decisionEvent.SourceType,
                decisionEvent.RelatedEntity,
                $"Decision pending: {decisionEvent.PrimaryEffectSummary} / {decisionEvent.SecondaryEffectSummary}",
                decisionEvent.CooldownKey);
        }
    }

    private DecisionEventType ResolveContextDecisionEventType()
    {
        if (DressingRoomPressure >= 64)
        {
            return DecisionEventType.PlayerMeeting;
        }

        if (BoardPressure >= 64)
        {
            return DecisionEventType.BoardMeeting;
        }

        if (CareerProfile.MediaPressure >= 58)
        {
            return DecisionEventType.MediaQuestion;
        }

        if (TransferPressure >= 60)
        {
            return DecisionEventType.AgentCall;
        }

        if (CurrentTrainingIntensity == TrainingIntensity.Demanding)
        {
            return DecisionEventType.TrainingIssue;
        }

        if (CurrentClub?.DirectorRelationshipState is DirectorRelationshipState.Tense or DirectorRelationshipState.Hostile)
        {
            return DecisionEventType.DirectorConflict;
        }

        if (FanPressure >= 60)
        {
            return DecisionEventType.FanPressureMoment;
        }

        return DecisionEventType.StaffDisagreement;
    }

    private bool TryCreateDecisionEvent(DecisionEventType eventType, string trigger, out DecisionEvent decisionEvent)
    {
        decisionEvent = BuildDecisionEvent(eventType, trigger);
        if (!CanGenerateDecisionEvent(decisionEvent.CooldownKey))
        {
            decisionEvent = null!;
            return false;
        }

        return true;
    }

    private bool CanGenerateDecisionEvent(string cooldownKey)
    {
        foreach (var activeEvent in _activeDecisionEvents)
        {
            if (activeEvent.CooldownKey == cooldownKey)
            {
                return false;
            }
        }

        foreach (var resolvedEvent in _resolvedDecisionEvents)
        {
            if (resolvedEvent.CooldownKey == cooldownKey && resolvedEvent.DaysUntilRepeat > 0)
            {
                return false;
            }
        }

        return true;
    }

    private DecisionEvent BuildDecisionEvent(DecisionEventType eventType, string trigger)
    {
        var relatedPlayer = SquadPlayers.Length == 0 ? "senior squad" : SquadPlayers[Math.Abs(WorldSeed + (int)eventType) % SquadPlayers.Length].Name;
        var id = $"{CurrentDate:yyyyMMdd}-{eventType}-{_activeDecisionEvents.Count + _resolvedDecisionEvents.Count + 1}";
        return eventType switch
        {
            DecisionEventType.PlayerMeeting => BuildDecisionEventCore(eventType, id, "Player wants clarity", "Player meeting", "Private", relatedPlayer, 4, $"{relatedPlayer} asks how the current plan affects minutes and role security.", "Listen and explain the pathway", "Set selection standards", "Player trust +1, dressing-room pressure -2", "Board trust +1, player trust -1", trigger),
            DecisionEventType.BoardMeeting => BuildDecisionEventCore(eventType, id, "Board requests a pressure briefing", "Board room", "Confirmed", BoardPhilosophyName, 5, $"The board wants a response to job pressure {JobPressure} and board pressure {BoardPressure}.", "Show evidence and ask for patience", "Promise immediate results", "Board trust +1, board pressure -2", "Board trust -1, media pressure +1", trigger),
            DecisionEventType.MediaQuestion => BuildDecisionEventCore(eventType, id, "Media questions the story", "Media", "Press room", SelectedClubName ?? "club", 4, $"Reporters ask about {PressureCategorySummary}.", "Stay calm and factual", "Push back publicly", "Media trust +1, media pressure -2", "Fan trust +1, media pressure +2", trigger),
            DecisionEventType.AgentCall => BuildDecisionEventCore(eventType, id, "Agent asks for assurances", "Agent call", "Agent briefing", relatedPlayer, 4, $"{relatedPlayer}'s camp wants clarity while transfer pressure sits at {TransferPressure}.", "Offer a private pathway", "Refuse guarantees", "Player trust +1, transfer pressure -2", "Board trust +1, transfer pressure +2", trigger),
            DecisionEventType.StaffDisagreement => BuildDecisionEventCore(eventType, id, "Staff disagree on the next plan", "Staff room", "Internal", "first-team staff", 3, "Staff are split between tactical caution and pressing work.", "Back the staff recommendation", "Hold the current line", "Staff trust +1, tactical reputation +1", "Staff trust -1, tactical familiarity +1", trigger),
            DecisionEventType.TrainingIssue => BuildDecisionEventCore(eventType, id, "Training load draws concern", "Fitness staff", "Internal", TrainingFocusName, 3, $"The {TrainingFocusName.ToLowerInvariant()} block at {TrainingIntensityName.ToLowerInvariant()} intensity is raising workload questions.", "Reduce load and recover", "Maintain intensity", "Dressing-room pressure -2, tactical familiarity -1", "Tactical familiarity +1, injury risk concern rises", trigger),
            DecisionEventType.FanPressureMoment => BuildDecisionEventCore(eventType, id, "Supporters demand a response", "Supporter groups", "Club sources", FanCultureName, 4, $"Fan pressure {FanPressure} brings the club identity debate forward.", "Acknowledge supporter standards", "Focus only on the dressing room", "Fan trust +1, fan pressure -2", "Board trust +1, fan trust -1", trigger),
            DecisionEventType.DirectorConflict => BuildDecisionEventCore(eventType, id, "Director of Football challenges the approach", "Director of Football", "Internal", DirectorOfFootballStyleName, 5, $"The Director questions recruitment and squad planning while Director trust is {CareerProfile.DirectorTrust}.", "Share evidence and compromise", "Assert manager authority", "Director trust +1, transfer pressure -2", "Director trust -2, board trust +1", trigger),
            DecisionEventType.CrisisEvent => BuildDecisionEventCore(eventType, id, "Crisis meeting called", "Club leadership", "Confirmed", SelectedClubName ?? "club", 6, $"A crisis check is triggered by job pressure {JobPressure}, media pressure {CareerProfile.MediaPressure}, and dressing-room pressure {DressingRoomPressure}.", "Stabilize privately", "Make a public reset", "Board trust +1, dressing-room pressure -2", "Media reputation +1, media pressure +2", trigger),
            _ => BuildDecisionEventCore(eventType, id, "Media asks for clarity", "Media", "Press room", SelectedClubName ?? "club", 3, "The football story needs a clear response.", "Stay factual", "Create a headline", "Media trust +1", "Media pressure +1", trigger)
        };
    }

    private static DecisionEvent BuildDecisionEventCore(
        DecisionEventType eventType,
        string id,
        string title,
        string sourceType,
        string reliability,
        string relatedEntity,
        int importance,
        string prompt,
        string primaryOption,
        string secondaryOption,
        string primaryEffect,
        string secondaryEffect,
        string trigger)
    {
        return new DecisionEvent
        {
            EventId = id,
            EventType = eventType,
            Title = title,
            SourceType = sourceType,
            Reliability = reliability,
            RelatedEntity = relatedEntity,
            Importance = importance,
            Prompt = $"{trigger}: {prompt}",
            PrimaryOption = primaryOption,
            SecondaryOption = secondaryOption,
            PrimaryEffectSummary = primaryEffect,
            SecondaryEffectSummary = secondaryEffect,
            CooldownKey = StageFoundationText.GetDisplayName(eventType),
            DaysUntilRepeat = 14,
            IsResolved = false,
            OutcomeSummary = "Pending."
        };
    }

    public string ResolveActiveDecisionEvent(int optionIndex = 0)
    {
        if (_activeDecisionEvents.Count == 0)
        {
            return "No active decision event to resolve.";
        }

        var decisionEvent = _activeDecisionEvents[0];
        _activeDecisionEvents.RemoveAt(0);
        var outcome = ApplyDecisionEventChoice(decisionEvent, optionIndex <= 0 ? 0 : 1);
        var resolved = CloneResolvedDecisionEvent(decisionEvent, outcome);
        _resolvedDecisionEvents.Insert(0, resolved);
        if (_resolvedDecisionEvents.Count > 12)
        {
            _resolvedDecisionEvents.RemoveAt(_resolvedDecisionEvents.Count - 1);
        }

        RecordPerceptionHistory("Decision event", $"{decisionEvent.Title}; {outcome}");
        AddNews(
            $"Decision resolved: {decisionEvent.Title}",
            NewsCategory.Pressure,
            decisionEvent.Reliability,
            outcome,
            decisionEvent.Importance,
            decisionEvent.SourceType,
            decisionEvent.RelatedEntity,
            outcome,
            decisionEvent.CooldownKey);
        return outcome;
    }

    private string ApplyDecisionEventChoice(DecisionEvent decisionEvent, int optionIndex)
    {
        var primary = optionIndex == 0;
        switch (decisionEvent.EventType)
        {
            case DecisionEventType.PlayerMeeting:
                CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + (primary ? 1 : -1), 0, 100);
                DressingRoomPressure = Math.Clamp(DressingRoomPressure + (primary ? -2 : 1), 0, 100);
                break;
            case DecisionEventType.BoardMeeting:
                CareerProfile.BoardTrust = Math.Clamp(CareerProfile.BoardTrust + (primary ? 1 : -1), 0, 100);
                BoardPressure = Math.Clamp(BoardPressure + (primary ? -2 : 2), 0, 100);
                break;
            case DecisionEventType.MediaQuestion:
                MediaTrust = Math.Clamp(MediaTrust + (primary ? 1 : -1), 0, 100);
                CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + (primary ? -2 : 2), 0, 100);
                MediaReputation = Math.Clamp(MediaReputation + (primary ? 0 : 1), 0, 100);
                break;
            case DecisionEventType.AgentCall:
                CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + (primary ? 1 : -1), 0, 100);
                TransferPressure = Math.Clamp(TransferPressure + (primary ? -2 : 2), 0, 100);
                break;
            case DecisionEventType.StaffDisagreement:
                CareerProfile.StaffTrust = Math.Clamp(CareerProfile.StaffTrust + (primary ? 1 : -1), 0, 100);
                TacticalReputation = Math.Clamp(TacticalReputation + (primary ? 1 : 0), 0, 100);
                TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + (primary ? 0 : 1), 0, 100);
                break;
            case DecisionEventType.TrainingIssue:
                DressingRoomPressure = Math.Clamp(DressingRoomPressure + (primary ? -2 : 1), 0, 100);
                TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + (primary ? -1 : 1), 0, 100);
                break;
            case DecisionEventType.FanPressureMoment:
                FanTrust = Math.Clamp(FanTrust + (primary ? 1 : -1), 0, 100);
                FanPressure = Math.Clamp(FanPressure + (primary ? -2 : 1), 0, 100);
                break;
            case DecisionEventType.DirectorConflict:
                CareerProfile.DirectorTrust = Math.Clamp(CareerProfile.DirectorTrust + (primary ? 1 : -2), 0, 100);
                TransferPressure = Math.Clamp(TransferPressure + (primary ? -2 : 2), 0, 100);
                break;
            case DecisionEventType.CrisisEvent:
                CareerProfile.BoardTrust = Math.Clamp(CareerProfile.BoardTrust + (primary ? 1 : -1), 0, 100);
                DressingRoomPressure = Math.Clamp(DressingRoomPressure + (primary ? -2 : 1), 0, 100);
                CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + (primary ? -1 : 2), 0, 100);
                break;
        }

        RefreshPressureCategories();
        EvaluateCareerFoundationState();
        return primary
            ? $"{decisionEvent.PrimaryOption}: {decisionEvent.PrimaryEffectSummary}"
            : $"{decisionEvent.SecondaryOption}: {decisionEvent.SecondaryEffectSummary}";
    }

    private static DecisionEvent CloneResolvedDecisionEvent(DecisionEvent decisionEvent, string outcome)
    {
        return new DecisionEvent
        {
            EventId = decisionEvent.EventId,
            EventType = decisionEvent.EventType,
            Title = decisionEvent.Title,
            SourceType = decisionEvent.SourceType,
            Reliability = decisionEvent.Reliability,
            RelatedEntity = decisionEvent.RelatedEntity,
            Importance = decisionEvent.Importance,
            Prompt = decisionEvent.Prompt,
            PrimaryOption = decisionEvent.PrimaryOption,
            SecondaryOption = decisionEvent.SecondaryOption,
            PrimaryEffectSummary = decisionEvent.PrimaryEffectSummary,
            SecondaryEffectSummary = decisionEvent.SecondaryEffectSummary,
            CooldownKey = decisionEvent.CooldownKey,
            DaysUntilRepeat = decisionEvent.DaysUntilRepeat,
            IsResolved = true,
            OutcomeSummary = outcome
        };
    }

    private void TickDecisionEventCooldowns(int days)
    {
        for (var index = 0; index < _resolvedDecisionEvents.Count; index++)
        {
            var decisionEvent = _resolvedDecisionEvents[index];
            if (decisionEvent.DaysUntilRepeat <= 0)
            {
                continue;
            }

            _resolvedDecisionEvents[index] = new DecisionEvent
            {
                EventId = decisionEvent.EventId,
                EventType = decisionEvent.EventType,
                Title = decisionEvent.Title,
                SourceType = decisionEvent.SourceType,
                Reliability = decisionEvent.Reliability,
                RelatedEntity = decisionEvent.RelatedEntity,
                Importance = decisionEvent.Importance,
                Prompt = decisionEvent.Prompt,
                PrimaryOption = decisionEvent.PrimaryOption,
                SecondaryOption = decisionEvent.SecondaryOption,
                PrimaryEffectSummary = decisionEvent.PrimaryEffectSummary,
                SecondaryEffectSummary = decisionEvent.SecondaryEffectSummary,
                CooldownKey = decisionEvent.CooldownKey,
                DaysUntilRepeat = Math.Max(0, decisionEvent.DaysUntilRepeat - days),
                IsResolved = true,
                OutcomeSummary = decisionEvent.OutcomeSummary
            };
        }
    }

    private void AddNews(
        string title,
        NewsCategory category,
        string reliability,
        string text,
        int importance,
        string sourceType = "Club source",
        string relatedEntity = "",
        string effectSummary = "",
        string cooldownKey = "")
    {
        var newsEvent = new NewsEvent
        {
            Title = title,
            Category = category,
            Reliability = reliability,
            Text = text,
            Importance = importance,
            SourceType = sourceType,
            RelatedEntity = relatedEntity,
            EffectSummary = effectSummary,
            CooldownKey = cooldownKey
        };
        _foundationNewsEvents.Insert(0, newsEvent);
        if (_foundationNewsEvents.Count > 12)
        {
            _foundationNewsEvents.RemoveAt(_foundationNewsEvents.Count - 1);
        }

        if (CurrentClub == null)
        {
            return;
        }

        var relatedText = string.IsNullOrWhiteSpace(relatedEntity) ? string.Empty : $" | Related: {relatedEntity}";
        var effectText = string.IsNullOrWhiteSpace(effectSummary) ? string.Empty : $" | Effect: {effectSummary}";
        var formatted = $"{StageFoundationText.GetDisplayName(category)} | {reliability} | {sourceType}: {title} - {text}{relatedText}{effectText}";
        var feed = new List<string> { formatted };
        feed.AddRange(CurrentClub.NewsFeed);
        if (feed.Count > 8)
        {
            feed.RemoveRange(8, feed.Count - 8);
        }

        CurrentClub.NewsFeed = feed.ToArray();
    }

    private void ReviewPromiseLifecycle(string trigger, int elapsedDays)
    {
        if (_promiseRecords.Count == 0)
        {
            return;
        }

        for (var index = 0; index < _promiseRecords.Count; index++)
        {
            var promise = _promiseRecords[index];
            if (promise.Status is PromiseStatus.Broken or PromiseStatus.Fulfilled)
            {
                continue;
            }

            var reviewed = BuildReviewedPromise(promise, trigger, elapsedDays);
            _promiseRecords[index] = reviewed;
            if (reviewed.Status != promise.Status)
            {
                ApplyPromiseStatusConsequences(reviewed, promise.Status);
            }
        }
    }

    private PromiseRecord BuildReviewedPromise(PromiseRecord promise, string trigger, int elapsedDays)
    {
        var daysRemaining = Math.Max(0, promise.DaysRemaining - elapsedDays);
        var pressureLoad = TransferPressure + DressingRoomPressure / 2 + (promise.IsPublic ? CareerProfile.MediaPressure / 2 : 0);
        var newStatus = promise.Status;
        if (promise.Status == PromiseStatus.Active)
        {
            newStatus = pressureLoad >= 65 ? PromiseStatus.AtRisk : PromiseStatus.OnTrack;
        }
        else if (promise.Status == PromiseStatus.OnTrack && pressureLoad >= 78)
        {
            newStatus = PromiseStatus.AtRisk;
        }
        else if (promise.Status == PromiseStatus.AtRisk && pressureLoad >= 88)
        {
            newStatus = PromiseStatus.Broken;
        }
        else if (promise.Status == PromiseStatus.AtRisk && pressureLoad < 55)
        {
            newStatus = PromiseStatus.Renegotiated;
        }

        if (daysRemaining == 0)
        {
            newStatus = newStatus == PromiseStatus.AtRisk ? PromiseStatus.Broken : PromiseStatus.Fulfilled;
        }

        var evidence = newStatus switch
        {
            PromiseStatus.OnTrack => $"{trigger}: evidence is credible; pressure load {pressureLoad}.",
            PromiseStatus.AtRisk => $"{trigger}: promise is drifting; pressure load {pressureLoad} needs action.",
            PromiseStatus.Broken => $"{trigger}: deadline or pressure broke the promise.",
            PromiseStatus.Fulfilled => $"{trigger}: promise requirements satisfied before the deadline.",
            PromiseStatus.Renegotiated => $"{trigger}: staff/player side accepted a narrower path.",
            _ => $"{trigger}: promise active and awaiting clearer evidence."
        };
        var agentMood = newStatus switch
        {
            PromiseStatus.OnTrack => "Satisfied",
            PromiseStatus.AtRisk => "Concerned",
            PromiseStatus.Broken => "Angry",
            PromiseStatus.Fulfilled => "Pleased",
            PromiseStatus.Renegotiated => "Cautiously settled",
            _ => promise.AgentMood
        };
        var risk = newStatus switch
        {
            PromiseStatus.AtRisk => "Player trust and agent mood may drop if no corrective action follows.",
            PromiseStatus.Broken => "Player trust, squad trust, media pressure, and job pressure can all worsen.",
            PromiseStatus.Fulfilled => "Trust improves because the promise had a visible outcome.",
            PromiseStatus.Renegotiated => "Trust hit is contained, but repeated renegotiation will still create pressure.",
            _ => promise.ConsequenceRisk
        };

        return new PromiseRecord
        {
            PromiseType = promise.PromiseType,
            Recipient = promise.Recipient,
            Source = promise.Source,
            IsPublic = promise.IsPublic,
            ExpectedAction = promise.ExpectedAction,
            DeadlineSummary = promise.DeadlineSummary,
            DaysRemaining = daysRemaining,
            Status = newStatus,
            CurrentEvidence = evidence,
            AgentMood = agentMood,
            ConsequenceRisk = risk
        };
    }

    private void ApplyPromiseStatusConsequences(PromiseRecord promise, PromiseStatus previousStatus)
    {
        var playerTrustDelta = promise.Status switch
        {
            PromiseStatus.OnTrack => 1,
            PromiseStatus.AtRisk => -1,
            PromiseStatus.Broken => -5,
            PromiseStatus.Fulfilled => 3,
            PromiseStatus.Renegotiated => -1,
            _ => 0
        };
        var squadDelta = promise.Status switch
        {
            PromiseStatus.Broken when promise.IsPublic => -3,
            PromiseStatus.Broken => -1,
            PromiseStatus.Fulfilled => 1,
            _ => 0
        };
        var pressureDelta = promise.Status switch
        {
            PromiseStatus.AtRisk => 2,
            PromiseStatus.Broken => promise.IsPublic ? 8 : 5,
            PromiseStatus.Fulfilled => -3,
            PromiseStatus.Renegotiated => 1,
            _ => 0
        };

        CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + playerTrustDelta, 0, 100);
        TeamMorale = Math.Clamp(TeamMorale + squadDelta, 0, 100);
        TransferPressure = Math.Clamp(TransferPressure + pressureDelta, 0, 100);
        if (promise.IsPublic)
        {
            CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + Math.Max(0, pressureDelta / 2), 0, 100);
            MediaTrust = Math.Clamp(MediaTrust + ResolveSlowTrustDelta(-pressureDelta), 0, 100);
        }

        SyncCurrentClubMoraleFromRuntime();
        RefreshPressureCategories();
        RecordPerceptionHistory(
            "Promise review",
            $"{promise.PromiseType} to {promise.Recipient} became {StageFoundationText.GetDisplayName(promise.Status)}; player trust {playerTrustDelta:+0;-0;0}, squad morale {squadDelta:+0;-0;0}, transfer pressure {TransferPressure}");
        AddNews(
            "Promise review",
            promise.IsPublic ? NewsCategory.Pressure : NewsCategory.Contract,
            promise.IsPublic ? "Club sources" : "Internal",
            $"{promise.PromiseType} promise to {promise.Recipient} moved from {StageFoundationText.GetDisplayName(previousStatus)} to {StageFoundationText.GetDisplayName(promise.Status)}. {promise.CurrentEvidence}",
            promise.Status is PromiseStatus.Broken or PromiseStatus.AtRisk ? 5 : 3);
    }

    private string BuildScoutingSummary()
    {
        if (CurrentScoutingAssignment == null)
        {
            return "No active scouting assignment.";
        }

        return $"{CurrentScoutingAssignment.Target} | {CurrentScoutingAssignment.DaysRemaining} days | quality {CurrentScoutingAssignment.ReportQuality} | {CurrentScoutingAssignment.DiscoverySummary}";
    }

    private string BuildJobOfferSummary()
    {
        if (CurrentJobOffer == null)
        {
            return "No current offer; market interest can be generated from reputation, pressure, and license.";
        }

        return $"{StageFoundationText.GetDisplayName(CurrentJobOffer.OfferType)} from {CurrentJobOffer.ClubName} as {CurrentJobOffer.RoleName}. {CurrentJobOffer.InterestSummary} {CurrentJobOffer.Reason}";
    }

    private string BuildPromiseSummary()
    {
        var lines = new List<string>();
        foreach (var promise in _promiseRecords)
        {
            var visibility = promise.IsPublic ? "public" : "private";
            lines.Add($"{promise.PromiseType} to {promise.Recipient}: {StageFoundationText.GetDisplayName(promise.Status)} | {visibility} | {promise.DaysRemaining} days | agent {promise.AgentMood} | {promise.CurrentEvidence} | {promise.ExpectedAction}");
        }

        return string.Join("\n", lines);
    }

    private string BuildObjectiveReviewSummary(int goalDifference)
    {
        var resultLine = goalDifference > 0
            ? "result supports the current objective path"
            : goalDifference == 0
                ? "draw keeps objectives under review"
                : "loss increases objective scrutiny";
        return $"Objective review: {resultLine}; board philosophy {BoardPhilosophyName}, fan culture {FanCultureName}, and role authority {CurrentRoleName} all affect pressure.";
    }

    private string BuildLicenseOpportunitySummary()
    {
        if (CareerProfile.License == ManagerLicense.ProLicense)
        {
            return "License progression: Pro License already held.";
        }

        if (WorldReputation >= 62 && JobPressure < 55)
        {
            return $"License progression: board may sponsor the next course after sustained form from {CurrentRoleName}.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager && FanTrust >= 58)
        {
            return "License progression: Assistant Manager pathway can unlock club recommendation if influence keeps rising.";
        }

        return "License progression: no active course yet; reputation, trust, and pressure need a stronger trend.";
    }

    private string BuildPlayerInterestSummary(ClubSquadPlayer candidate, bool isLoanCandidate, string loanDirection)
    {
        if (isLoanCandidate && loanDirection == "Outgoing loan")
        {
            return candidate.Age <= 21
                ? "Open to a development loan if minutes and role are credible."
                : "Loan interest is cautious; player needs a strong competitive reason.";
        }

        if (isLoanCandidate)
        {
            return "Loan interest depends on role clarity, wage coverage, and parent-club trust.";
        }

        if (candidate.Age <= 23)
        {
            return "Open to a development pathway if minutes are credible.";
        }

        return "Interest depends on role, wage, club trajectory, and agent confidence.";
    }

    private string BuildBoardRecruitmentStance(ClubSquadPlayer candidate, bool isLoanCandidate, string loanDirection)
    {
        if (isLoanCandidate && loanDirection == "Outgoing loan")
        {
            return candidate.Age <= 21 ? "Board supports a minutes-led development loan." : "Board wants senior-depth risk checked before approval.";
        }

        if (isLoanCandidate)
        {
            return "Board will consider a low-risk loan if wage exposure is controlled.";
        }

        if (CurrentClub?.BoardPhilosophy == BoardPhilosophy.FinanciallyStrictBoard && candidate.TrueAbility < 74)
        {
            return "Board skeptical: value and wage discipline need stronger proof.";
        }

        if (CurrentClub?.BoardPhilosophy == BoardPhilosophy.YouthDevelopmentBoard && candidate.Age <= 23)
        {
            return "Board supportive: age profile matches youth pathway.";
        }

        return candidate.TacticalFitScore >= 68
            ? "Board open if total cost, role, and Director support remain aligned."
            : "Board cautious: tactical fit and resale evidence are incomplete.";
    }

    private string BuildDirectorRecruitmentStance(ClubSquadPlayer candidate, bool isLoanCandidate, string loanDirection)
    {
        if (isLoanCandidate && loanDirection == "Outgoing loan")
        {
            return "Director wants loan-club fit and recall review before sign-off.";
        }

        if (isLoanCandidate)
        {
            return "Director sees a squad-depth loan as acceptable if it does not block owned-player minutes.";
        }

        return CurrentClub?.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.AcademyBuilder when candidate.Age > 25 => "Director skeptical: target blocks academy minutes.",
            DirectorOfFootballStyle.BargainHunter => "Director demands value discipline and rival-pressure awareness.",
            DirectorOfFootballStyle.StarChaser when candidate.TrueAbility >= 75 => "Director supportive: profile carries ambition signal.",
            DirectorOfFootballStyle.ControlFreak => "Director guarded: wants the approach routed through his shortlist.",
            _ => candidate.TacticalFitScore >= 68 ? "Director supportive with scouting evidence." : "Director wants more fit evidence before backing the move."
        };
    }

    private string BuildLoanValuation(ClubSquadPlayer candidate, string loanDirection)
    {
        if (loanDirection == "Outgoing loan")
        {
            return $"Development value: minutes required; wage coverage target {Math.Clamp(35 + candidate.TrueAbility / 3, 45, 75)}%.";
        }

        return $"Parent club likely asks wage coverage around {Math.Clamp(45 + candidate.TrueAbility / 4, 50, 85)}%.";
    }

    private string BuildAgentMood(ClubSquadPlayer candidate, bool isLoanCandidate)
    {
        if (isLoanCandidate)
        {
            return candidate.Age <= 22 ? "Agent receptive if minutes are written into the pathway." : "Agent neutral; wants role clarity.";
        }

        if (candidate.TrueAbility >= 76)
        {
            return "Agent ambitious; wage, role, and rival interest matter.";
        }

        return candidate.Age <= 23 ? "Agent curious about pathway and development minutes." : "Agent pragmatic; contract fit and club trajectory matter.";
    }

    private string BuildRivalInterest(ClubSquadPlayer candidate, bool isLoanCandidate)
    {
        if (isLoanCandidate)
        {
            return candidate.Age <= 21 ? "Several clubs could offer minutes; fit matters more than fee." : "Limited rival loan pressure.";
        }

        if (candidate.TrueAbility >= 76)
        {
            return "Rival interest likely; delay could raise cost or agent demands.";
        }

        return candidate.TacticalFitScore >= 70 ? "Niche tactical fit lowers rival pressure." : "Rival pressure unknown until deeper scouting.";
    }

    private string BuildDevelopmentLoanSuitability(ClubSquadPlayer candidate, string loanDirection)
    {
        if (loanDirection == "Outgoing loan")
        {
            return candidate.Age <= 21 && !candidate.IsStarting
                ? "High suitability: young non-starter needs senior minutes."
                : "Moderate suitability: depth risk must be reviewed.";
        }

        return candidate.Age <= 23 ? "Suitable incoming loan if minutes do not block owned prospects." : "Short-term cover only; limited development upside.";
    }

    private string BuildLoanPlayingTimeExpectation(ClubSquadPlayer candidate, string loanDirection)
    {
        if (loanDirection == "Outgoing loan")
        {
            return candidate.TrueAbility >= 68 ? "Expected role: regular starter at loan club." : "Expected role: rotation minutes with development plan.";
        }

        return candidate.TrueAbility >= 72 ? "Expected role: first-team rotation." : "Expected role: squad depth with clear minutes cap.";
    }

    private string BuildLoanClubFit(ClubSquadPlayer candidate, string loanDirection)
    {
        if (loanDirection == "Outgoing loan")
        {
            return candidate.TacticalFitScore >= 68 ? "Loan club fit should match current tactical identity." : "Loan club fit needs careful style screening.";
        }

        return candidate.TacticalFitScore >= 68 ? $"Fits {TeamStyleName} cover needs." : $"Partial fit for {TeamStyleName}; loan risk is manageable only as short cover.";
    }

    private string BuildFeeRange(int ability, int age)
    {
        var baseFee = ability * 65000 + (age <= 23 ? 800000 : 250000);
        return $"${baseFee / 1000000.0:0.0}m-${(baseFee * 14 / 10) / 1000000.0:0.0}m";
    }

    private string BuildWageRange(int ability)
    {
        var low = 18000 + ability * 700;
        var high = low + 18000;
        return $"${low / 1000}k/w-${high / 1000}k/w";
    }

    private string BuildRecruitmentInformationSummary(ClubSquadPlayer candidate)
    {
        var reportQuality = CurrentScoutingAssignment?.ReportQuality ?? 35;
        var report = PlayerInformationVisibility.BuildReport(
            candidate,
            PlayerKnowledgeContext.ScoutedTarget,
            CareerProfile.Role,
            CareerProfile.License,
            GetStaffQuality(StaffRole.Scout),
            GetStaffQuality(StaffRole.DataAnalyst),
            GetStaffQuality(StaffRole.HeadOfRecruitment),
            reportQuality);
        return $"{report.KnowledgeLabel}; {report.KnownAttributesSummary} {report.EstimatedAttributesSummary} {report.UnknownAttributesSummary}";
    }

    private string BuildScoutingDiscoverySummary(int reportQuality, bool ready)
    {
        var confidence = reportQuality >= 75
            ? "strong"
            : reportQuality >= 55
                ? "working"
                : "low";
        var exactLine = reportQuality >= 70
            ? "exact current role and some exact current attributes are visible"
            : "exact role context is visible, but attributes remain mostly estimated";
        var unknownLine = reportQuality >= 70
            ? "unknowns remain around potential ?, agent loyalty ?, and pressure response ?"
            : "unknowns remain around tactical detail ?, personality ?, potential ?, and agent loyalty ?";
        return ready
            ? $"Report ready: {CurrentScoutingAssignment?.Target}; confidence {confidence}; {exactLine}; {unknownLine}; tactical fit reviewed for {TeamStyleName}."
            : $"In progress: {CurrentScoutingAssignment?.Target}; confidence {confidence}; wider attribute ranges and tactical-fit language improving; personality still ?.";
    }

    private string BuildDirectorRecruitmentResponse(ClubSquadPlayer candidate)
    {
        if (CurrentClub == null)
        {
            return "Director response unavailable until club foundation is active.";
        }

        return CurrentClub.DirectorOfFootballStyle switch
        {
            DirectorOfFootballStyle.AcademyBuilder when candidate.Age <= 23 => $"Director supports the age profile and pathway value; Director trust {CareerProfile.DirectorTrust}/100 strengthens cooperation.",
            DirectorOfFootballStyle.BargainHunter => $"Director wants fee discipline before any move; Director trust {CareerProfile.DirectorTrust}/100 affects patience.",
            DirectorOfFootballStyle.StarChaser when candidate.TrueAbility >= 75 => $"Director likes the visible ambition signal; Director trust {CareerProfile.DirectorTrust}/100 supports the case.",
            DirectorOfFootballStyle.ControlFreak => $"Director insists recruitment must run through his shortlist process; Director trust {CareerProfile.DirectorTrust}/100 shapes conflict risk.",
            _ => $"Director requests fit evidence before committing; Director trust {CareerProfile.DirectorTrust}/100 sets the cooperation level."
        };
    }

    private string BuildBoardRecruitmentResponse(ClubSquadPlayer candidate)
    {
        if (CurrentClub == null)
        {
            return "Board response unavailable until club foundation is active.";
        }

        return CurrentClub.BoardPhilosophy switch
        {
            BoardPhilosophy.FinanciallyStrictBoard => $"Board demands wage control and resale logic; board trust {CareerProfile.BoardTrust}/100 affects approval tolerance.",
            BoardPhilosophy.YouthDevelopmentBoard when candidate.Age <= 23 => $"Board is open if the pathway remains credible; board trust {CareerProfile.BoardTrust}/100 affects patience.",
            BoardPhilosophy.WinNowBoard when candidate.TrueAbility >= 74 => $"Board accepts a first-team case if results justify it; board trust {CareerProfile.BoardTrust}/100 affects room for risk.",
            BoardPhilosophy.DataDrivenBoard => $"Board wants tactical fit and value evidence, not fee alone; board trust {CareerProfile.BoardTrust}/100 shapes the burden of proof.",
            _ => $"Board will review cost, role, tactical fit, Director view, and board trust {CareerProfile.BoardTrust}/100 together."
        };
    }

    private RecruitmentTarget CloneRecruitmentTarget(RecruitmentTarget target, string status, string? targetStatus = null, string? outcomeState = null)
    {
        return new RecruitmentTarget
        {
            PlayerName = target.PlayerName,
            Position = target.Position,
            InformationSummary = target.InformationSummary,
            InterestSummary = target.InterestSummary,
            TacticalFitSummary = target.TacticalFitSummary,
            EstimatedFeeRange = target.EstimatedFeeRange,
            EstimatedWageRange = target.EstimatedWageRange,
            DirectorResponse = target.DirectorResponse,
            BoardResponse = target.BoardResponse,
            TargetStatus = targetStatus ?? target.TargetStatus,
            ClubValuation = target.ClubValuation,
            AgentMood = target.AgentMood,
            RivalInterest = target.RivalInterest,
            BoardStance = target.BoardStance,
            DirectorStance = target.DirectorStance,
            OutcomeState = outcomeState ?? target.OutcomeState,
            IsLoanCandidate = target.IsLoanCandidate,
            LoanDirection = target.LoanDirection,
            DevelopmentLoanSuitability = target.DevelopmentLoanSuitability,
            PlayingTimeExpectation = target.PlayingTimeExpectation,
            LoanClubFit = target.LoanClubFit,
            LoanReviewSummary = target.LoanReviewSummary,
            Status = status
        };
    }

    private static RecruitmentTarget CloneRecruitmentTargetWithFinanceStress(RecruitmentTarget target)
    {
        return new RecruitmentTarget
        {
            PlayerName = target.PlayerName,
            Position = target.Position,
            InformationSummary = target.InformationSummary,
            InterestSummary = target.InterestSummary,
            TacticalFitSummary = target.TacticalFitSummary,
            EstimatedFeeRange = "$999.0m-$1000.0m",
            EstimatedWageRange = "$999k-$1000k",
            DirectorResponse = target.DirectorResponse,
            BoardResponse = target.BoardResponse,
            TargetStatus = target.TargetStatus,
            ClubValuation = "Finance stress case: selling club valuation exceeds club budget.",
            AgentMood = "Agent stress case: wage demand breaks wage structure.",
            RivalInterest = target.RivalInterest,
            BoardStance = "Board blocks because finance stress exceeds budget and wage structure.",
            DirectorStance = target.DirectorStance,
            OutcomeState = "Blocked by finance",
            IsLoanCandidate = false,
            LoanDirection = string.Empty,
            DevelopmentLoanSuitability = "Not assessed: finance stress case.",
            PlayingTimeExpectation = target.PlayingTimeExpectation,
            LoanClubFit = "Not a loan pathway.",
            LoanReviewSummary = "No loan review opened.",
            Status = "Finance stress case."
        };
    }

    private int GetStaffQuality(StaffRole role)
    {
        if (CurrentClub == null)
        {
            return 55;
        }

        foreach (var staff in CurrentClub.Staff)
        {
            if (staff.Role == role)
            {
                return staff.Quality;
            }
        }

        return 55;
    }
}
