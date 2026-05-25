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
    public SaveSlotRecruitmentTargetData? RecruitmentTarget { get; set; }
    public SaveSlotPromiseRecordData[]? PromiseRecords { get; set; }
    public string JobSecurityName { get; set; } = "Stable";
    public SaveSlotJobOfferEventData? JobOffer { get; set; }
    public string[]? CareerHistory { get; set; }
    public string LicenseOpportunitySummary { get; set; } = string.Empty;
    public string ObjectiveReviewSummary { get; set; } = string.Empty;
    public int FanTrust { get; set; } = 55;
    public int WorldReputation { get; set; } = 45;
    public int DressingRoomPressure { get; set; } = 35;
    public int TransferPressure { get; set; } = 25;
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
    public string Status { get; set; } = string.Empty;
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
    private readonly List<PromiseRecord> _promiseRecords = new();
    private readonly List<string> _careerHistory = new();

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
    public JobSecurityState JobSecurity { get; private set; } = JobSecurityState.Stable;
    public JobOfferEvent? CurrentJobOffer { get; private set; }
    public string LicenseOpportunitySummary { get; private set; } = "License progression will be reviewed after sustained progress.";
    public string ObjectiveReviewSummary { get; private set; } = "Objective review pending first run of matches.";
    public int FanTrust { get; private set; } = 55;
    public int WorldReputation { get; private set; } = 45;
    public int DressingRoomPressure { get; private set; } = 35;
    public int TransferPressure { get; private set; } = 25;

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
    public string RecruitmentFoundationSummary => CurrentRecruitmentTarget == null
        ? "Recruitment foundation pending scouting target."
        : $"{CurrentRecruitmentTarget.PlayerName} ({CurrentRecruitmentTarget.Position}) | {CurrentRecruitmentTarget.InformationSummary} | {CurrentRecruitmentTarget.InterestSummary} | {CurrentRecruitmentTarget.TacticalFitSummary} | Fee {CurrentRecruitmentTarget.EstimatedFeeRange} | Wage {CurrentRecruitmentTarget.EstimatedWageRange} | {CurrentRecruitmentTarget.Status}";
    public string TrainingScoutingSummary => $"{TrainingFocusName} ({TrainingIntensityName}): {TrainingStatusSummary}\nScouting depth: {ScoutingReportDepthName}\nScouting: {BuildScoutingSummary()}";
    public string CareerMarketSummary => $"Job security: {JobSecurityName} | Reputation {WorldReputation} | Fan trust {FanTrust} | Dressing-room pressure {DressingRoomPressure} | Transfer pressure {TransferPressure}\nLicense: {LicenseOpportunitySummary}\nJob market: {BuildJobOfferSummary()}";
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
        ApplyTrainingEffects();
        ApplyScoutingProgress(7);
        ReviewPromiseLifecycle("Weekly review", 7);
        EvaluateCareerFoundationState();
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
        EnsureRecruitmentTarget();
        if (CurrentRecruitmentTarget == null)
        {
            return "Recruitment target unavailable.";
        }

        var target = CurrentRecruitmentTarget;
        var role = CareerProfile.Role;
        if (role == ManagerRole.AssistantManager)
        {
            CurrentRecruitmentTarget = CloneRecruitmentTarget(target, "Recommended by Assistant Manager; final authority sits with senior staff.");
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
            CurrentRecruitmentTarget = CloneRecruitmentTarget(target, "Requested by Head Coach; Director of Football and board review required.");
            AddNews(
                "Head Coach submits recruitment request",
                NewsCategory.Transfer,
                "Internal",
                $"{ManagerName} requested {target.PlayerName}; recruitment control remains shared with the Director of Football.",
                3);
            return CurrentRecruitmentTarget.Status;
        }

        var approved = target.TacticalFitSummary.Contains("Strong", StringComparison.Ordinal) ||
            CurrentClub?.DirectorRelationshipState is DirectorRelationshipState.Ally or DirectorRelationshipState.Supportive;
        var status = approved
            ? "Board approval granted for a basic approach; promise logged around squad role."
            : "Board rejects the basic approach: fit, wage, and Director confidence do not align.";
        CurrentRecruitmentTarget = CloneRecruitmentTarget(target, status);
        if (approved)
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
        AddNews(
            approved ? "Transfer approach approved" : "Transfer approach blocked",
            NewsCategory.Transfer,
            "Club sources",
            $"{target.PlayerName}: {status}",
            approved ? 4 : 5);
        return status;
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
                    Importance = newsEvent.Importance
                }),
            RecruitmentTarget = CurrentRecruitmentTarget == null
                ? null
                : new SaveSlotRecruitmentTargetData
                {
                    PlayerName = CurrentRecruitmentTarget.PlayerName,
                    Position = CurrentRecruitmentTarget.Position,
                    InformationSummary = CurrentRecruitmentTarget.InformationSummary,
                    InterestSummary = CurrentRecruitmentTarget.InterestSummary,
                    TacticalFitSummary = CurrentRecruitmentTarget.TacticalFitSummary,
                    EstimatedFeeRange = CurrentRecruitmentTarget.EstimatedFeeRange,
                    EstimatedWageRange = CurrentRecruitmentTarget.EstimatedWageRange,
                    DirectorResponse = CurrentRecruitmentTarget.DirectorResponse,
                    BoardResponse = CurrentRecruitmentTarget.BoardResponse,
                    Status = CurrentRecruitmentTarget.Status
                },
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
            WorldReputation = WorldReputation,
            DressingRoomPressure = DressingRoomPressure,
            TransferPressure = TransferPressure
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
                    Importance = newsEvent.Importance
                });
            }
        }

        CurrentRecruitmentTarget = data.RecruitmentTarget == null
            ? null
            : new RecruitmentTarget
            {
                PlayerName = data.RecruitmentTarget.PlayerName,
                Position = data.RecruitmentTarget.Position,
                InformationSummary = string.IsNullOrWhiteSpace(data.RecruitmentTarget.InformationSummary)
                    ? "Knowledge: saved target visibility unavailable; scout confidence should be rebuilt by a new report."
                    : data.RecruitmentTarget.InformationSummary,
                InterestSummary = data.RecruitmentTarget.InterestSummary,
                TacticalFitSummary = data.RecruitmentTarget.TacticalFitSummary,
                EstimatedFeeRange = data.RecruitmentTarget.EstimatedFeeRange,
                EstimatedWageRange = data.RecruitmentTarget.EstimatedWageRange,
                DirectorResponse = data.RecruitmentTarget.DirectorResponse,
                BoardResponse = data.RecruitmentTarget.BoardResponse,
                Status = data.RecruitmentTarget.Status
            };

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
        WorldReputation = Math.Clamp(data.WorldReputation <= 0 ? CareerProfile.Reputation : data.WorldReputation, 0, 100);
        DressingRoomPressure = Math.Clamp(data.DressingRoomPressure, 0, 100);
        TransferPressure = Math.Clamp(data.TransferPressure, 0, 100);
        EnsureRecruitmentTarget();
        EnsureJobMarketFoundation();
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
        JobSecurity = JobSecurityState.Stable;
        CurrentJobOffer = null;
        LicenseOpportunitySummary = "License progression will be reviewed after sustained progress.";
        ObjectiveReviewSummary = "Objective review pending first run of matches.";
        FanTrust = 55;
        WorldReputation = CareerProfile.Reputation;
        DressingRoomPressure = 35;
        TransferPressure = 25;
        _foundationNewsEvents.Clear();
        _promiseRecords.Clear();
        _careerHistory.Clear();
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
        if (CurrentScoutingAssignment == null)
        {
            StartBasicScoutingAssignment("Position need: versatile midfielder");
        }

        EnsureRecruitmentTarget();
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
        CareerProfile.BoardTrust = Math.Clamp(CareerProfile.BoardTrust + Math.Sign(consequence.BoardDelta), 0, 100);
        CareerProfile.PlayerTrust = Math.Clamp(CareerProfile.PlayerTrust + Math.Sign(consequence.MoraleDelta), 0, 100);
        CareerProfile.DirectorTrust = Math.Clamp(CareerProfile.DirectorTrust + (goalDifference >= 0 ? 1 : -1), 0, 100);
        FanTrust = Math.Clamp(FanTrust + Math.Sign(consequence.FanDelta), 0, 100);
        WorldReputation = Math.Clamp(WorldReputation + (goalDifference > 0 ? 2 : goalDifference == 0 ? 0 : -1), 0, 100);
        CareerProfile.Reputation = WorldReputation;
        CareerProfile.MediaPressure = Math.Clamp(CareerProfile.MediaPressure + (goalDifference < 0 ? 3 : -1), 0, 100);
        TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + (goalDifference >= 0 ? 2 : -1), 0, 100);
        DressingRoomPressure = Math.Clamp(100 - SquadMorale + Math.Max(0, JobPressure - 55) / 2, 0, 100);
        TransferPressure = Math.Clamp(TransferPressure + (goalDifference < 0 ? 3 : -1), 0, 100);
        EvaluateCareerFoundationState();
        RefreshTacticFoundation(TacticalFormation, TeamStyle);
        ObjectiveReviewSummary = BuildObjectiveReviewSummary(goalDifference);
        LicenseOpportunitySummary = BuildLicenseOpportunitySummary();
        AddNews(
            "Post-match consequences logged",
            NewsCategory.Pressure,
            "Confirmed",
            $"{result.FinalResultSummary}: board, fan, squad, trust, reputation, and job security states were updated.",
            5);
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
        var familiarityDelta = Math.Max(0, baseFamiliarityDelta + intensityFamiliarityDelta);
        TacticalFamiliarityScore = Math.Clamp(TacticalFamiliarityScore + familiarityDelta, 0, 100);

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
            var fitnessDelta = baseFitnessDelta + (CurrentTrainingIntensity == TrainingIntensity.Controlled ? 2 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? -2 : 0);
            var fatigueDelta = baseFatigueDelta + (CurrentTrainingIntensity == TrainingIntensity.Controlled ? -4 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? 6 : 0);
            var moraleDelta = CurrentTrainingFocus == TrainingFocus.TeamCohesion ? 2 : CurrentTrainingIntensity == TrainingIntensity.Demanding ? -1 : 0;
            var injuryDelta = fatigueDelta > 0 ? 1 + (CurrentTrainingIntensity == TrainingIntensity.Demanding ? 2 : 0) : -2;
            SquadPlayers[index] = player.With(
                fitness: Math.Clamp(player.Fitness + fitnessDelta, 35, 99),
                morale: Math.Clamp(player.Morale + moraleDelta, 0, 100),
                fatigue: Math.Clamp(player.Fatigue + fatigueDelta, 0, 100),
                injuryRisk: Math.Clamp(player.InjuryRisk + injuryDelta, 0, 100),
                tacticalFitScore: Math.Clamp(player.TacticalFitScore + familiarityDelta / 2, 0, 100),
                playerFamiliarity: Math.Clamp(player.PlayerFamiliarity + 2 + familiarityDelta / 2, 0, 100));
        }

        RefreshTacticFoundation(TacticalFormation, TeamStyle);
        TrainingStatusSummary = $"{TrainingFocusName} at {TrainingIntensityName.ToLowerInvariant()} intensity changed tactical familiarity to {TacticalFamiliarityName}, updated condition, and raised staff familiarity with the squad.";
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
            return;
        }

        var sourceClub = string.IsNullOrWhiteSpace(CurrentOpponentName) ? ResolveDifferentClub(SelectedClubName) : CurrentOpponentName;
        var sourceSquad = GetClubSquad(sourceClub);
        var candidate = sourceSquad.Length == 0
            ? ClubSquadFactory.BuildFallbackSquad(sourceClub, WorldSeed)[0]
            : sourceSquad[Math.Min(8, sourceSquad.Length - 1)];
        var strongFit = candidate.TacticalFitScore >= 68;
        CurrentRecruitmentTarget = new RecruitmentTarget
        {
            PlayerName = candidate.Name,
            Position = candidate.Position,
            InformationSummary = BuildRecruitmentInformationSummary(candidate),
            InterestSummary = candidate.Age <= 23 ? "Open to a development pathway if minutes are credible." : "Interest depends on role, wage, and club trajectory.",
            TacticalFitSummary = strongFit ? $"Strong fit for {TeamStyleName}." : $"Partial fit for {TeamStyleName}; scouting recommends caution.",
            EstimatedFeeRange = BuildFeeRange(candidate.TrueAbility, candidate.Age),
            EstimatedWageRange = BuildWageRange(candidate.TrueAbility),
            DirectorResponse = BuildDirectorRecruitmentResponse(candidate),
            BoardResponse = BuildBoardRecruitmentResponse(candidate),
            Status = "Shortlisted foundation target; no negotiation started."
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
        JobSecurity = EvaluateJobSecurity();
        LicenseOpportunitySummary = BuildLicenseOpportunitySummary();
        DressingRoomPressure = Math.Clamp(100 - SquadMorale + Math.Max(0, JobPressure - 55) / 2, 0, 100);
    }

    private JobSecurityState EvaluateJobSecurity()
    {
        var pressure = JobPressure + Math.Max(0, 55 - BoardMorale) / 2 + Math.Max(0, 55 - FanMorale) / 3 + GetRolePressureWeight();
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

    private void AddNews(string title, NewsCategory category, string reliability, string text, int importance)
    {
        var newsEvent = new NewsEvent
        {
            Title = title,
            Category = category,
            Reliability = reliability,
            Text = text,
            Importance = importance
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

        var formatted = $"{StageFoundationText.GetDisplayName(category)} | {reliability}: {title} - {text}";
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
        }

        SyncCurrentClubMoraleFromRuntime();
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
            DirectorOfFootballStyle.AcademyBuilder when candidate.Age <= 23 => "Director supports the age profile and pathway value.",
            DirectorOfFootballStyle.BargainHunter => "Director wants fee discipline before any move.",
            DirectorOfFootballStyle.StarChaser when candidate.TrueAbility >= 75 => "Director likes the visible ambition signal.",
            DirectorOfFootballStyle.ControlFreak => "Director insists recruitment must run through his shortlist process.",
            _ => "Director requests fit evidence before committing."
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
            BoardPhilosophy.FinanciallyStrictBoard => "Board demands wage control and resale logic.",
            BoardPhilosophy.YouthDevelopmentBoard when candidate.Age <= 23 => "Board is open if the pathway remains credible.",
            BoardPhilosophy.WinNowBoard when candidate.TrueAbility >= 74 => "Board accepts a first-team case if results justify it.",
            BoardPhilosophy.DataDrivenBoard => "Board wants tactical fit and value evidence, not fee alone.",
            _ => "Board will review cost, role, tactical fit, and Director view together."
        };
    }

    private RecruitmentTarget CloneRecruitmentTarget(RecruitmentTarget target, string status)
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
            Status = status
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
