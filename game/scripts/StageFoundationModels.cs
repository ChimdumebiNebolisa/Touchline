using System;
using System.Collections.Generic;

public enum TacticalTeamStyle
{
    Balanced,
    Possession,
    DirectPlay,
    Counterattack,
    HighPress,
    LowBlock,
    WideAttack,
    CentralOverload,
    DefensiveSolidity
}

public enum TacticalFamiliarity
{
    Excellent,
    VeryFamiliar,
    Familiar,
    Neutral,
    Unfamiliar,
    Poor,
    VeryPoor
}

public enum TrainingFocus
{
    AttackingMovement,
    DefensiveShape,
    Pressing,
    Possession,
    Counterattack,
    SetPieces,
    Fitness,
    Recovery,
    TeamCohesion,
    YouthIntegration
}

public enum TrainingIntensity
{
    Controlled,
    Standard,
    Demanding
}

public enum ScoutingReportDepth
{
    QuickLook,
    StandardReport,
    FullReport
}

public enum TacticalSetPieceApproach
{
    BalancedSetPieces,
    AttackNearPost,
    AttackFarPost,
    ShortRoutines,
    CrowdKeeper,
    DefensiveSecurity
}

public enum OpponentPreparationFocus
{
    BalancedBrief,
    PressTriggers,
    RestDefense,
    WideContainment,
    CentralContainment,
    DirectDefense,
    LowBlockPatience
}

public enum NewsCategory
{
    Club,
    Training,
    Scouting,
    Match,
    Transfer,
    Contract,
    Career,
    Pressure
}

public enum DecisionEventType
{
    PlayerMeeting,
    BoardMeeting,
    MediaQuestion,
    AgentCall,
    StaffDisagreement,
    TrainingIssue,
    FanPressureMoment,
    DirectorConflict,
    CrisisEvent
}

public enum PromiseStatus
{
    Active,
    OnTrack,
    AtRisk,
    Broken,
    Fulfilled,
    Renegotiated
}

public enum JobSecurityState
{
    Secure,
    Stable,
    Watched,
    UnderPressure,
    Ultimatum,
    NearSacking,
    Sacked
}

public enum JobOfferType
{
    AssistantManagerOffer,
    HeadCoachOffer,
    ManagerOffer,
    InterimManagerOffer,
    EndOfSeasonApproach,
    EmergencyApproach,
    InterviewInvitation
}

public sealed class NewsEvent
{
    public required string Title { get; init; }
    public required NewsCategory Category { get; init; }
    public required string Reliability { get; init; }
    public required string Text { get; init; }
    public required int Importance { get; init; }
    public string SourceType { get; init; } = "Club source";
    public string RelatedEntity { get; init; } = string.Empty;
    public string EffectSummary { get; init; } = string.Empty;
    public string CooldownKey { get; init; } = string.Empty;
}

public sealed class DecisionEvent
{
    public required string EventId { get; init; }
    public required DecisionEventType EventType { get; init; }
    public required string Title { get; init; }
    public required string SourceType { get; init; }
    public required string Reliability { get; init; }
    public required string RelatedEntity { get; init; }
    public required int Importance { get; init; }
    public required string Prompt { get; init; }
    public required string PrimaryOption { get; init; }
    public required string SecondaryOption { get; init; }
    public required string PrimaryEffectSummary { get; init; }
    public required string SecondaryEffectSummary { get; init; }
    public required string CooldownKey { get; init; }
    public required int DaysUntilRepeat { get; init; }
    public required bool IsResolved { get; init; }
    public required string OutcomeSummary { get; init; }
}

public sealed class ScoutingAssignment
{
    public required string Target { get; init; }
    public required int DaysRemaining { get; init; }
    public required int ReportQuality { get; init; }
    public required string DiscoverySummary { get; init; }
    public required bool ReportReady { get; init; }
}

public sealed class RecruitmentTarget
{
    public required string PlayerName { get; init; }
    public required string Position { get; init; }
    public required string InformationSummary { get; init; }
    public required string InterestSummary { get; init; }
    public required string TacticalFitSummary { get; init; }
    public required string EstimatedFeeRange { get; init; }
    public required string EstimatedWageRange { get; init; }
    public required string DirectorResponse { get; init; }
    public required string BoardResponse { get; init; }
    public required string Status { get; init; }
}

public sealed class PromiseRecord
{
    public required string PromiseType { get; init; }
    public required string Recipient { get; init; }
    public required string Source { get; init; }
    public required bool IsPublic { get; init; }
    public required string ExpectedAction { get; init; }
    public required string DeadlineSummary { get; init; }
    public required int DaysRemaining { get; init; }
    public required PromiseStatus Status { get; init; }
    public required string CurrentEvidence { get; init; }
    public required string AgentMood { get; init; }
    public required string ConsequenceRisk { get; init; }
}

public sealed class JobOfferEvent
{
    public required JobOfferType OfferType { get; init; }
    public required string ClubName { get; init; }
    public required string RoleName { get; init; }
    public required string InterestSummary { get; init; }
    public required string Reason { get; init; }
}

public static class StageFoundationText
{
    public static string GetDisplayName(TacticalTeamStyle value)
    {
        return value switch
        {
            TacticalTeamStyle.Possession => "Possession",
            TacticalTeamStyle.DirectPlay => "Direct Play",
            TacticalTeamStyle.Counterattack => "Counterattack",
            TacticalTeamStyle.HighPress => "High Press",
            TacticalTeamStyle.LowBlock => "Low Block",
            TacticalTeamStyle.WideAttack => "Wide Attack",
            TacticalTeamStyle.CentralOverload => "Central Overload",
            TacticalTeamStyle.DefensiveSolidity => "Defensive Solidity",
            _ => "Balanced"
        };
    }

    public static TacticalTeamStyle ParseTeamStyle(string value)
    {
        return value switch
        {
            "Possession" => TacticalTeamStyle.Possession,
            "Direct Play" => TacticalTeamStyle.DirectPlay,
            "Counterattack" => TacticalTeamStyle.Counterattack,
            "High Press" => TacticalTeamStyle.HighPress,
            "Low Block" => TacticalTeamStyle.LowBlock,
            "Wide Attack" => TacticalTeamStyle.WideAttack,
            "Central Overload" => TacticalTeamStyle.CentralOverload,
            "Defensive Solidity" => TacticalTeamStyle.DefensiveSolidity,
            _ => TacticalTeamStyle.Balanced
        };
    }

    public static string GetDisplayName(TacticalFamiliarity value)
    {
        return value switch
        {
            TacticalFamiliarity.Excellent => "Excellent",
            TacticalFamiliarity.VeryFamiliar => "Very Familiar",
            TacticalFamiliarity.Familiar => "Familiar",
            TacticalFamiliarity.Unfamiliar => "Unfamiliar",
            TacticalFamiliarity.Poor => "Poor",
            TacticalFamiliarity.VeryPoor => "Very Poor",
            _ => "Neutral"
        };
    }

    public static TacticalFamiliarity ParseTacticalFamiliarity(string value)
    {
        return value switch
        {
            "Excellent" => TacticalFamiliarity.Excellent,
            "Very Familiar" => TacticalFamiliarity.VeryFamiliar,
            "Familiar" => TacticalFamiliarity.Familiar,
            "Unfamiliar" => TacticalFamiliarity.Unfamiliar,
            "Poor" => TacticalFamiliarity.Poor,
            "Very Poor" => TacticalFamiliarity.VeryPoor,
            _ => TacticalFamiliarity.Neutral
        };
    }

    public static string GetDisplayName(TrainingFocus value)
    {
        return value switch
        {
            TrainingFocus.AttackingMovement => "Attacking movement",
            TrainingFocus.DefensiveShape => "Defensive shape",
            TrainingFocus.Pressing => "Pressing",
            TrainingFocus.Possession => "Possession",
            TrainingFocus.Counterattack => "Counterattack",
            TrainingFocus.SetPieces => "Set pieces",
            TrainingFocus.Fitness => "Fitness",
            TrainingFocus.Recovery => "Recovery",
            TrainingFocus.YouthIntegration => "Youth integration",
            _ => "Team cohesion"
        };
    }

    public static string GetDisplayName(TrainingIntensity value)
    {
        return value switch
        {
            TrainingIntensity.Controlled => "Controlled",
            TrainingIntensity.Demanding => "Demanding",
            _ => "Standard"
        };
    }

    public static TrainingIntensity ParseTrainingIntensity(string value)
    {
        return value switch
        {
            "Controlled" => TrainingIntensity.Controlled,
            "Demanding" => TrainingIntensity.Demanding,
            _ => TrainingIntensity.Standard
        };
    }

    public static string GetDisplayName(ScoutingReportDepth value)
    {
        return value switch
        {
            ScoutingReportDepth.QuickLook => "Quick look",
            ScoutingReportDepth.FullReport => "Full report",
            _ => "Standard report"
        };
    }

    public static ScoutingReportDepth ParseScoutingReportDepth(string value)
    {
        return value switch
        {
            "Quick look" => ScoutingReportDepth.QuickLook,
            "Full report" => ScoutingReportDepth.FullReport,
            _ => ScoutingReportDepth.StandardReport
        };
    }

    public static string GetDisplayName(TacticalSetPieceApproach value)
    {
        return value switch
        {
            TacticalSetPieceApproach.AttackNearPost => "Attack near post",
            TacticalSetPieceApproach.AttackFarPost => "Attack far post",
            TacticalSetPieceApproach.ShortRoutines => "Short routines",
            TacticalSetPieceApproach.CrowdKeeper => "Crowd keeper",
            TacticalSetPieceApproach.DefensiveSecurity => "Defensive security",
            _ => "Balanced set pieces"
        };
    }

    public static TacticalSetPieceApproach ParseSetPieceApproach(string value)
    {
        return value switch
        {
            "Attack near post" => TacticalSetPieceApproach.AttackNearPost,
            "Attack far post" => TacticalSetPieceApproach.AttackFarPost,
            "Short routines" => TacticalSetPieceApproach.ShortRoutines,
            "Crowd keeper" => TacticalSetPieceApproach.CrowdKeeper,
            "Defensive security" => TacticalSetPieceApproach.DefensiveSecurity,
            _ => TacticalSetPieceApproach.BalancedSetPieces
        };
    }

    public static string GetDisplayName(OpponentPreparationFocus value)
    {
        return value switch
        {
            OpponentPreparationFocus.PressTriggers => "Press triggers",
            OpponentPreparationFocus.RestDefense => "Rest defense",
            OpponentPreparationFocus.WideContainment => "Wide containment",
            OpponentPreparationFocus.CentralContainment => "Central containment",
            OpponentPreparationFocus.DirectDefense => "Direct defense",
            OpponentPreparationFocus.LowBlockPatience => "Low-block patience",
            _ => "Balanced brief"
        };
    }

    public static OpponentPreparationFocus ParseOpponentPreparationFocus(string value)
    {
        return value switch
        {
            "Press triggers" => OpponentPreparationFocus.PressTriggers,
            "Rest defense" => OpponentPreparationFocus.RestDefense,
            "Wide containment" => OpponentPreparationFocus.WideContainment,
            "Central containment" => OpponentPreparationFocus.CentralContainment,
            "Direct defense" => OpponentPreparationFocus.DirectDefense,
            "Low-block patience" => OpponentPreparationFocus.LowBlockPatience,
            _ => OpponentPreparationFocus.BalancedBrief
        };
    }

    public static TrainingFocus ParseTrainingFocus(string value)
    {
        return value switch
        {
            "Attacking movement" => TrainingFocus.AttackingMovement,
            "Defensive shape" => TrainingFocus.DefensiveShape,
            "Pressing" => TrainingFocus.Pressing,
            "Possession" => TrainingFocus.Possession,
            "Counterattack" => TrainingFocus.Counterattack,
            "Set pieces" => TrainingFocus.SetPieces,
            "Fitness" => TrainingFocus.Fitness,
            "Recovery" => TrainingFocus.Recovery,
            "Youth integration" => TrainingFocus.YouthIntegration,
            _ => TrainingFocus.TeamCohesion
        };
    }

    public static string GetDisplayName(NewsCategory value)
    {
        return value switch
        {
            NewsCategory.Training => "Training",
            NewsCategory.Scouting => "Scouting",
            NewsCategory.Match => "Match",
            NewsCategory.Transfer => "Transfer",
            NewsCategory.Contract => "Contract",
            NewsCategory.Career => "Career",
            NewsCategory.Pressure => "Pressure",
            _ => "Club"
        };
    }

    public static NewsCategory ParseNewsCategory(string value)
    {
        return value switch
        {
            "Training" => NewsCategory.Training,
            "Scouting" => NewsCategory.Scouting,
            "Match" => NewsCategory.Match,
            "Transfer" => NewsCategory.Transfer,
            "Contract" => NewsCategory.Contract,
            "Career" => NewsCategory.Career,
            "Pressure" => NewsCategory.Pressure,
            _ => NewsCategory.Club
        };
    }

    public static string GetDisplayName(DecisionEventType value)
    {
        return value switch
        {
            DecisionEventType.PlayerMeeting => "Player meeting",
            DecisionEventType.BoardMeeting => "Board meeting",
            DecisionEventType.MediaQuestion => "Media question",
            DecisionEventType.AgentCall => "Agent call",
            DecisionEventType.StaffDisagreement => "Staff disagreement",
            DecisionEventType.TrainingIssue => "Training issue",
            DecisionEventType.FanPressureMoment => "Fan pressure moment",
            DecisionEventType.DirectorConflict => "Director of Football conflict",
            DecisionEventType.CrisisEvent => "Crisis event",
            _ => "Media question"
        };
    }

    public static DecisionEventType ParseDecisionEventType(string value)
    {
        return value switch
        {
            "Player meeting" => DecisionEventType.PlayerMeeting,
            "Board meeting" => DecisionEventType.BoardMeeting,
            "Media question" => DecisionEventType.MediaQuestion,
            "Agent call" => DecisionEventType.AgentCall,
            "Staff disagreement" => DecisionEventType.StaffDisagreement,
            "Training issue" => DecisionEventType.TrainingIssue,
            "Fan pressure moment" => DecisionEventType.FanPressureMoment,
            "Director of Football conflict" => DecisionEventType.DirectorConflict,
            "Crisis event" => DecisionEventType.CrisisEvent,
            _ => DecisionEventType.MediaQuestion
        };
    }

    public static string GetDisplayName(PromiseStatus value)
    {
        return value switch
        {
            PromiseStatus.OnTrack => "On Track",
            PromiseStatus.AtRisk => "At Risk",
            PromiseStatus.Broken => "Broken",
            PromiseStatus.Fulfilled => "Fulfilled",
            PromiseStatus.Renegotiated => "Renegotiated",
            _ => "Active"
        };
    }

    public static PromiseStatus ParsePromiseStatus(string value)
    {
        return value switch
        {
            "On Track" => PromiseStatus.OnTrack,
            "At Risk" => PromiseStatus.AtRisk,
            "Broken" => PromiseStatus.Broken,
            "Fulfilled" => PromiseStatus.Fulfilled,
            "Renegotiated" => PromiseStatus.Renegotiated,
            _ => PromiseStatus.Active
        };
    }

    public static string GetDisplayName(JobSecurityState value)
    {
        return value switch
        {
            JobSecurityState.Secure => "Secure",
            JobSecurityState.Watched => "Watched",
            JobSecurityState.UnderPressure => "Under Pressure",
            JobSecurityState.Ultimatum => "Ultimatum",
            JobSecurityState.NearSacking => "Near Sacking",
            JobSecurityState.Sacked => "Sacked",
            _ => "Stable"
        };
    }

    public static JobSecurityState ParseJobSecurity(string value)
    {
        return value switch
        {
            "Secure" => JobSecurityState.Secure,
            "Watched" => JobSecurityState.Watched,
            "Under Pressure" => JobSecurityState.UnderPressure,
            "Ultimatum" => JobSecurityState.Ultimatum,
            "Near Sacking" => JobSecurityState.NearSacking,
            "Sacked" => JobSecurityState.Sacked,
            _ => JobSecurityState.Stable
        };
    }

    public static string GetDisplayName(JobOfferType value)
    {
        return value switch
        {
            JobOfferType.AssistantManagerOffer => "Assistant Manager offer",
            JobOfferType.HeadCoachOffer => "Head Coach offer",
            JobOfferType.ManagerOffer => "Manager offer",
            JobOfferType.InterimManagerOffer => "Interim Manager offer",
            JobOfferType.EndOfSeasonApproach => "End-of-season approach",
            JobOfferType.EmergencyApproach => "Emergency approach",
            _ => "Interview invitation"
        };
    }

    public static JobOfferType ParseJobOfferType(string value)
    {
        return value switch
        {
            "Assistant Manager offer" => JobOfferType.AssistantManagerOffer,
            "Head Coach offer" => JobOfferType.HeadCoachOffer,
            "Manager offer" => JobOfferType.ManagerOffer,
            "Interim Manager offer" => JobOfferType.InterimManagerOffer,
            "End-of-season approach" => JobOfferType.EndOfSeasonApproach,
            "Emergency approach" => JobOfferType.EmergencyApproach,
            _ => JobOfferType.InterviewInvitation
        };
    }
}

public static class PlayerIdentityFoundation
{
    private static readonly string[] Regions =
    {
        "Novaran", "Asterian", "Caled", "Luskan", "Maritian", "Veyran"
    };

    public static GameState.SquadPlayer BuildSquadPlayer(WorldSeedPlayerData player, string clubName, int worldSeed, int index)
    {
        var seed = BuildStableSeed(worldSeed, clubName, player.Name, player.Position, index);
        var trueAbility = Math.Clamp((player.Form + player.Morale + player.Fitness) / 3 - 2 + seed % 7, 45, 92);
        var technical = Math.Clamp(trueAbility + PositionTechnicalModifier(player.Position) + (seed % 5) - 2, 35, 95);
        var tactical = Math.Clamp(trueAbility + PositionTacticalModifier(player.Position) + ((seed / 7) % 5) - 2, 35, 95);
        var physical = Math.Clamp((player.Fitness + trueAbility) / 2 + ((seed / 11) % 5) - 2, 35, 96);
        var mental = Math.Clamp((player.Morale + trueAbility) / 2 + ((seed / 13) % 5) - 2, 35, 96);
        var style = ResolvePlayingStyle(player.Position, seed);
        var traits = ResolveTraits(player.Position, seed);
        var personality = ResolvePersonality(seed);
        var contractRole = player.IsStarting ? "Important Player" : player.Age <= 21 ? "Development Player" : "Squad Player";

        return new GameState.SquadPlayer
        {
            PlayerId = ClubSquadFactory.BuildPlayerId(clubName, player.Name, index),
            Name = player.Name,
            Position = player.Position,
            Age = player.Age,
            Nationality = Regions[Math.Abs(seed) % Regions.Length],
            TrueAbility = trueAbility,
            TechnicalAttribute = technical,
            TacticalAttribute = tactical,
            PhysicalAttribute = physical,
            MentalAttribute = mental,
            KnownAttributesSummary = $"Known: Technical {technical}, Physical {physical}, Form {player.Form}",
            EstimatedAttributesSummary = $"Estimated: Tactical {ClampRange(tactical - 4)}-{ClampRange(tactical + 5)}, Mental {ClampRange(mental - 5)}-{ClampRange(mental + 4)}",
            UnknownAttributesSummary = "Unknown: Potential ?, pressure response ?, agent loyalty ?",
            PlayingStyle = style,
            Tendencies = ResolveTendencies(player.Position, seed),
            Traits = traits,
            Personality = personality,
            TacticalFit = BuildTacticalFit(player.Position, style, player.IsStarting),
            DevelopmentCurve = BuildDevelopmentCurve(player.Age, trueAbility),
            Form = player.Form,
            Morale = player.Morale,
            Fitness = player.Fitness,
            Fatigue = Math.Clamp(100 - player.Fitness + (player.IsStarting ? 8 : 2), 0, 65),
            InjuryRisk = Math.Clamp(14 + (100 - player.Fitness) / 3 + (player.Age >= 30 ? 5 : 0), 5, 60),
            Wage = EstimateWage(trueAbility, player.Age, player.IsStarting),
            ContractExpiryYear = 2027 + Math.Abs(seed % 4),
            ContractRole = contractRole,
            Relationship = player.Morale >= 75 ? "Aligned" : player.Morale >= 60 ? "Professional" : "Needs attention",
            PromiseSummary = player.IsStarting ? "Implicit playing-time expectation." : "No active promise.",
            TransferInterest = BuildTransferInterest(trueAbility, player.Age),
            TacticalFitScore = Math.Clamp((tactical + mental + (player.IsStarting ? 6 : 0)) / 2, 35, 95),
            PlayerFamiliarity = Math.Clamp(player.IsStarting ? 68 + seed % 12 : 46 + seed % 16, 0, 100),
            ScoutingConfidence = Math.Clamp(player.IsStarting ? 58 + seed % 12 : 42 + seed % 14, 0, 100),
            KnownAttributeGroups = player.IsStarting
                ? "form,fitness,technical,physical,current role"
                : "form,fitness,current role",
            EstimatedAttributeGroups = "technical,tactical,physical,mental,potential",
            UnknownAttributeGroups = "pressure response,agent loyalty,future behavior,exact potential",
            IsStarting = player.IsStarting
        };
    }

    public static ClubSquadPlayer BuildClubSquadPlayer(WorldSeedPlayerData player, string clubName, int worldSeed, int index)
    {
        var enriched = BuildSquadPlayer(player, clubName, worldSeed, index);
        return ToClubSquadPlayer(enriched, clubName, index);
    }

    public static ClubSquadPlayer ToClubSquadPlayer(GameState.SquadPlayer player, string clubName, int index)
    {
        return new ClubSquadPlayer
        {
            PlayerId = string.IsNullOrWhiteSpace(player.PlayerId)
                ? ClubSquadFactory.BuildPlayerId(clubName, player.Name, index)
                : player.PlayerId,
            ClubName = clubName,
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

    public static GameState.SquadPlayer EnsureIdentity(GameState.SquadPlayer player, string clubName, int worldSeed, int index)
    {
        if (!string.IsNullOrWhiteSpace(player.KnownAttributesSummary) &&
            !string.IsNullOrWhiteSpace(player.PlayingStyle) &&
            !string.IsNullOrWhiteSpace(player.ContractRole))
        {
            return player;
        }

        return BuildSquadPlayer(
            new WorldSeedPlayerData
            {
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Form = player.Form,
                Morale = player.Morale,
                Fitness = player.Fitness,
                IsStarting = player.IsStarting
            },
            clubName,
            worldSeed,
            index);
    }

    public static string BuildProfileSummary(GameState.SquadPlayer player)
    {
        return $"{player.Nationality} {player.Position} | {player.PlayingStyle} | {player.Traits} | {player.Personality}";
    }

    public static string BuildInformationSummary(GameState.SquadPlayer player)
    {
        return $"{player.KnownAttributesSummary}\n{player.EstimatedAttributesSummary}\n{player.UnknownAttributesSummary}";
    }

    public static string BuildContractSummary(GameState.SquadPlayer player)
    {
        return $"Contract: {player.ContractRole}, wage {FormatMoney(player.Wage)}, expires {player.ContractExpiryYear}. {player.PromiseSummary}";
    }

    private static string ResolvePlayingStyle(string position, int seed)
    {
        return position switch
        {
            "GK" => seed % 2 == 0 ? "Sweeper keeper" : "Line goalkeeper",
            "CB" => seed % 2 == 0 ? "Ball-playing defender" : "Stopper",
            "RB" or "LB" => seed % 2 == 0 ? "Overlapping fullback" : "Balanced fullback",
            "CM" => seed % 2 == 0 ? "Tempo setter" : "Ball-winning midfielder",
            "AM" => seed % 2 == 0 ? "Between-lines creator" : "Late box runner",
            "RW" or "LW" => seed % 2 == 0 ? "Inverted winger" : "Direct wide runner",
            _ => seed % 2 == 0 ? "Pressing forward" : "Penalty-box striker"
        };
    }

    private static string ResolveTendencies(string position, int seed)
    {
        return position switch
        {
            "GK" => "Looks early for fullback outlets; holds position under pressure.",
            "CB" => seed % 2 == 0 ? "Steps into midfield when the lane opens." : "Protects the box before chasing duels.",
            "RB" or "LB" => "Times overlaps, but can leave space behind if the press breaks.",
            "CM" => "Checks shoulder often and offers short passing support.",
            "AM" => "Receives between lines and looks for the final pass quickly.",
            "RW" or "LW" => "Attacks the channel and cuts inside when isolated.",
            _ => "Pins centre-backs and presses the first backward pass."
        };
    }

    private static string ResolveTraits(string position, int seed)
    {
        var firstTrait = position switch
        {
            "GK" => "calm handling",
            "CB" => "aerial presence",
            "RB" or "LB" => "overlap timing",
            "CM" => "press resistance",
            "AM" => "creative risk",
            "RW" or "LW" => "1v1 aggression",
            _ => "box movement"
        };
        var secondTrait = seed % 3 == 0 ? "big-match focus" : seed % 3 == 1 ? "training intensity" : "role discipline";
        return $"{firstTrait}, {secondTrait}";
    }

    private static string ResolvePersonality(int seed)
    {
        return (seed % 4) switch
        {
            0 => "Driven but private",
            1 => "Vocal teammate",
            2 => "Low-maintenance professional",
            _ => "Confidence-sensitive"
        };
    }

    private static string BuildTacticalFit(string position, string style, bool isStarting)
    {
        var fit = isStarting ? "Strong fit" : "Partial fit";
        return $"{fit}: {style.ToLowerInvariant()} maps naturally to current {position} usage, with role comfort still affected by tactical familiarity.";
    }

    private static string BuildDevelopmentCurve(int age, int trueAbility)
    {
        if (age <= 21)
        {
            return $"Growth curve: upward, potential estimate {ClampRange(trueAbility + 8)}-{ClampRange(trueAbility + 15)}.";
        }

        if (age >= 30)
        {
            return "Growth curve: senior maintenance, performance depends on form and workload.";
        }

        return $"Growth curve: prime years, potential estimate {ClampRange(trueAbility + 2)}-{ClampRange(trueAbility + 7)}.";
    }

    private static string BuildTransferInterest(int trueAbility, int age)
    {
        if (trueAbility >= 78 && age <= 25)
        {
            return "Interest: monitored by larger clubs, but no active bid.";
        }

        if (age >= 30)
        {
            return "Interest: short-term market only.";
        }

        return "Interest: internal squad value, no live approach.";
    }

    private static int EstimateWage(int trueAbility, int age, bool isStarting)
    {
        var wage = 18000 + trueAbility * 950 + (isStarting ? 18000 : 6000);
        if (age <= 21)
        {
            wage -= 7000;
        }

        return Math.Clamp(wage, 12000, 155000);
    }

    private static int PositionTechnicalModifier(string position)
    {
        return position switch
        {
            "AM" or "CM" or "RW" or "LW" => 4,
            "GK" => -3,
            _ => 0
        };
    }

    private static int PositionTacticalModifier(string position)
    {
        return position switch
        {
            "CB" or "CM" or "GK" => 4,
            "ST" => -1,
            _ => 1
        };
    }

    private static int ClampRange(int value)
    {
        return Math.Clamp(value, 1, 99);
    }

    private static string FormatMoney(int amount)
    {
        return amount >= 1000000
            ? $"${amount / 1000000.0:0.0}m"
            : $"${amount / 1000}k/w";
    }

    private static int BuildStableSeed(int worldSeed, string clubName, string playerName, string position, int index)
    {
        unchecked
        {
            var hash = 17;
            hash = (hash * 31) + worldSeed;
            hash = AddStableStringHash(hash, clubName);
            hash = AddStableStringHash(hash, playerName);
            hash = AddStableStringHash(hash, position);
            hash = (hash * 31) + index;
            return hash == int.MinValue ? int.MaxValue : Math.Abs(hash);
        }
    }

    private static int AddStableStringHash(int hash, string value)
    {
        foreach (var character in value ?? string.Empty)
        {
            hash = (hash * 31) + character;
        }

        return hash;
    }
}

public static class TacticsFoundation
{
    private static readonly string[] DefaultFormationRoles =
    {
        "GK", "RB", "CB", "CB", "LB", "CM", "CM", "AM", "LW", "ST", "RW"
    };

    public static string BuildTeamInstructions(TacticalTeamStyle style, int tempo, int passingDirectness, int press, int line, int width, int risk, int tackling)
    {
        return $"{StageFoundationText.GetDisplayName(style)} | Tempo {tempo} | Passing directness {passingDirectness} | Press {press} | Defensive line {line} | Width {width} | Attacking risk {risk} | Tackling {tackling}";
    }

    public static string BuildPlayerRolesSummary(GameState.SquadPlayer[] squadPlayers, string formation, TacticalTeamStyle style)
    {
        var starters = new List<string>();
        foreach (var player in squadPlayers)
        {
            if (!player.IsStarting)
            {
                continue;
            }

            starters.Add($"{player.Position}: {ResolveRole(player.Position, style)}");
            if (starters.Count == 5)
            {
                break;
            }
        }

        return starters.Count == 0
            ? $"Roles pending for {formation}."
            : $"Roles in {formation}: {string.Join("; ", starters)}.";
    }

    public static string BuildPlayerInstructionsSummary(TacticalTeamStyle style)
    {
        return style switch
        {
            TacticalTeamStyle.HighPress => "Player instructions: front line presses first backward pass; midfield protects second balls.",
            TacticalTeamStyle.LowBlock => "Player instructions: defenders hold the box; wide players recover before countering.",
            TacticalTeamStyle.Possession => "Player instructions: midfield offers short support; fullbacks recycle rather than force crosses.",
            TacticalTeamStyle.DirectPlay => "Player instructions: forwards attack early channels; midfield follows for second balls.",
            TacticalTeamStyle.Counterattack => "Player instructions: first pass forward after regain; far-side winger attacks space.",
            TacticalTeamStyle.WideAttack => "Player instructions: fullbacks overlap; winger isolates before cutback.",
            TacticalTeamStyle.CentralOverload => "Player instructions: midfield narrows to combine; striker pins centre-backs.",
            TacticalTeamStyle.DefensiveSolidity => "Player instructions: back line keeps distances; tackling stays controlled.",
            _ => "Player instructions: hold shape, support the ball, and protect transition cover."
        };
    }

    public static string BuildFitNotes(GameState.SquadPlayer[] squadPlayers, TacticalTeamStyle style, TacticalFamiliarity familiarity)
    {
        var fitTotal = 0;
        var count = 0;
        foreach (var player in squadPlayers)
        {
            if (!player.IsStarting)
            {
                continue;
            }

            fitTotal += player.TacticalFitScore;
            count++;
        }

        var averageFit = count == 0 ? 60 : fitTotal / count;
        return $"Fit notes: XI fit {averageFit}/100 with {StageFoundationText.GetDisplayName(style).ToLowerInvariant()} style; familiarity {StageFoundationText.GetDisplayName(familiarity)}.";
    }

    public static string BuildRiskNotes(TacticalTeamStyle style, int press, int tempo, int risk, TacticalFamiliarity familiarity)
    {
        var riskText = risk >= 70 || press >= 75
            ? "transition exposure is high"
            : risk <= 35 || style == TacticalTeamStyle.LowBlock
                ? "chance volume may be limited"
                : "risk profile is balanced";
        if (familiarity is TacticalFamiliarity.Poor or TacticalFamiliarity.VeryPoor)
        {
            riskText += "; low familiarity raises execution risk";
        }

        return $"Risk notes: {riskText}.";
    }

    public static int CalculateRoleFitScore(GameState.SquadPlayer[] squadPlayers, string formation, TacticalTeamStyle style)
    {
        var starters = BuildStarterList(squadPlayers);
        if (starters.Count == 0)
        {
            return 60;
        }

        var fitTotal = 0;
        var styleMatches = 0;
        foreach (var player in starters)
        {
            fitTotal += player.TacticalFitScore;
            if (StyleMatchesPlayer(player, style))
            {
                styleMatches++;
            }
        }

        var averageFit = fitTotal / starters.Count;
        var shapeFit = CalculateShapeBalance(starters, formation);
        var styleBonus = Math.Clamp((styleMatches * 4) - 8, -8, 12);
        return Math.Clamp((averageFit * 2 + shapeFit) / 3 + styleBonus, 25, 98);
    }

    public static string BuildRoleFitDepthSummary(GameState.SquadPlayer[] squadPlayers, string formation, TacticalTeamStyle style, int score)
    {
        var starters = BuildStarterList(squadPlayers);
        if (starters.Count == 0)
        {
            return $"Role fit: {score}/100. No XI is available for role-fit analysis.";
        }

        var strongest = starters[0];
        var concern = starters[0];
        foreach (var player in starters)
        {
            if (player.TacticalFitScore > strongest.TacticalFitScore)
            {
                strongest = player;
            }

            if (player.TacticalFitScore < concern.TacticalFitScore)
            {
                concern = player;
            }
        }

        var level = score >= 76
            ? "strong"
            : score >= 60
                ? "workable"
                : "fragile";
        return $"Role fit: {score}/100 ({level}) in {formation}. Best fit: {strongest.Name} as {ResolveRole(strongest.Position, style)}. Watch: {concern.Name} needs clearer role support.";
    }

    public static string BuildPlayerFamiliaritySummary(GameState.SquadPlayer[] squadPlayers, TacticalFamiliarity familiarity)
    {
        var starters = BuildStarterList(squadPlayers);
        if (starters.Count == 0)
        {
            return $"Player familiarity: no XI loaded; team familiarity {StageFoundationText.GetDisplayName(familiarity)}.";
        }

        var total = 0;
        var lowCount = 0;
        foreach (var player in starters)
        {
            total += player.PlayerFamiliarity;
            if (player.PlayerFamiliarity < 45)
            {
                lowCount++;
            }
        }

        var average = total / starters.Count;
        var note = lowCount > 0
            ? $"{lowCount} starter(s) still have role/familiarity uncertainty"
            : "the XI understands the current plan well enough for matchday";
        return $"Player familiarity: XI average {average}/100; {note}; team familiarity {StageFoundationText.GetDisplayName(familiarity)}.";
    }

    public static TacticalSetPieceApproach ResolveSetPieceApproach(TacticalTeamStyle style, TrainingFocus focus, int risk)
    {
        if (focus == TrainingFocus.SetPieces)
        {
            return risk >= 62 ? TacticalSetPieceApproach.AttackNearPost : TacticalSetPieceApproach.ShortRoutines;
        }

        return style switch
        {
            TacticalTeamStyle.DirectPlay => TacticalSetPieceApproach.AttackFarPost,
            TacticalTeamStyle.WideAttack => TacticalSetPieceApproach.AttackNearPost,
            TacticalTeamStyle.Possession => TacticalSetPieceApproach.ShortRoutines,
            TacticalTeamStyle.HighPress when risk >= 65 => TacticalSetPieceApproach.CrowdKeeper,
            TacticalTeamStyle.LowBlock or TacticalTeamStyle.DefensiveSolidity => TacticalSetPieceApproach.DefensiveSecurity,
            _ => TacticalSetPieceApproach.BalancedSetPieces
        };
    }

    public static string BuildSetPieceSummary(TacticalSetPieceApproach approach, TrainingFocus focus)
    {
        var focusText = focus == TrainingFocus.SetPieces
            ? "set-piece training is active this week"
            : "set-piece work follows the wider team style";
        return $"Set pieces: {StageFoundationText.GetDisplayName(approach)}; {focusText}.";
    }

    public static OpponentPreparationFocus ResolveOpponentPreparationFocus(TacticalTeamStyle style, int press, int width, int risk)
    {
        if (risk >= 72)
        {
            return OpponentPreparationFocus.RestDefense;
        }

        if (press >= 72)
        {
            return OpponentPreparationFocus.PressTriggers;
        }

        if (width >= 68 || style == TacticalTeamStyle.WideAttack)
        {
            return OpponentPreparationFocus.WideContainment;
        }

        return style switch
        {
            TacticalTeamStyle.CentralOverload => OpponentPreparationFocus.CentralContainment,
            TacticalTeamStyle.DirectPlay => OpponentPreparationFocus.DirectDefense,
            TacticalTeamStyle.LowBlock or TacticalTeamStyle.DefensiveSolidity => OpponentPreparationFocus.LowBlockPatience,
            _ => OpponentPreparationFocus.BalancedBrief
        };
    }

    public static string BuildOpponentPreparationSummary(OpponentPreparationFocus focus, string opponentName)
    {
        var opponent = string.IsNullOrWhiteSpace(opponentName) ? "the next opponent" : opponentName;
        var explanation = focus switch
        {
            OpponentPreparationFocus.PressTriggers => "staff highlight backward passes and loose first touches as press cues",
            OpponentPreparationFocus.RestDefense => "staff stress cover behind attacks before chasing extra runners",
            OpponentPreparationFocus.WideContainment => "staff prepare fullback support and far-post cover",
            OpponentPreparationFocus.CentralContainment => "staff protect the central lane and second balls",
            OpponentPreparationFocus.DirectDefense => "staff prepare for early balls and aerial duels",
            OpponentPreparationFocus.LowBlockPatience => "staff ask for patience against a compressed game",
            _ => "staff keep the brief balanced while scouting detail remains limited"
        };
        return $"Opponent prep: {StageFoundationText.GetDisplayName(focus)} vs {opponent}; {explanation}.";
    }

    public static TacticalFamiliarity FamiliarityFromScore(int score)
    {
        return score switch
        {
            >= 90 => TacticalFamiliarity.Excellent,
            >= 78 => TacticalFamiliarity.VeryFamiliar,
            >= 66 => TacticalFamiliarity.Familiar,
            >= 52 => TacticalFamiliarity.Neutral,
            >= 40 => TacticalFamiliarity.Unfamiliar,
            >= 28 => TacticalFamiliarity.Poor,
            _ => TacticalFamiliarity.VeryPoor
        };
    }

    private static List<GameState.SquadPlayer> BuildStarterList(GameState.SquadPlayer[] squadPlayers)
    {
        var starters = new List<GameState.SquadPlayer>();
        foreach (var player in squadPlayers)
        {
            if (!player.IsStarting)
            {
                continue;
            }

            starters.Add(player);
            if (starters.Count == 11)
            {
                break;
            }
        }

        return starters;
    }

    private static int CalculateShapeBalance(List<GameState.SquadPlayer> starters, string formation)
    {
        var defenders = 0;
        var midfielders = 0;
        var forwards = 0;
        var widePlayers = 0;
        foreach (var player in starters)
        {
            var family = PositionFamily(player.Position);
            if (family == "DEF")
            {
                defenders++;
            }
            else if (family == "MID")
            {
                midfielders++;
            }
            else if (family == "FWD")
            {
                forwards++;
            }

            if (player.Position is "RB" or "LB" or "RW" or "LW")
            {
                widePlayers++;
            }
        }

        var expected = BuildFormationRoles(formation);
        var expectedDefenders = CountFamily(expected, "DEF");
        var expectedMidfielders = CountFamily(expected, "MID");
        var expectedForwards = CountFamily(expected, "FWD");
        var expectedWide = CountWide(expected);
        var penalty =
            Math.Abs(defenders - expectedDefenders) * 5 +
            Math.Abs(midfielders - expectedMidfielders) * 4 +
            Math.Abs(forwards - expectedForwards) * 4 +
            Math.Abs(widePlayers - expectedWide) * 3;
        return Math.Clamp(82 - penalty, 35, 92);
    }

    private static string[] BuildFormationRoles(string formation)
    {
        return formation switch
        {
            "4-2-3-1" => new[] { "GK", "RB", "CB", "CB", "LB", "CM", "CM", "LW", "AM", "RW", "ST" },
            "3-5-2" => new[] { "GK", "CB", "CB", "CB", "LWB", "CM", "CM", "AM", "RWB", "ST", "ST" },
            _ => DefaultFormationRoles
        };
    }

    private static int CountFamily(string[] roles, string family)
    {
        var count = 0;
        foreach (var role in roles)
        {
            if (PositionFamily(role) == family)
            {
                count++;
            }
        }

        return count;
    }

    private static int CountWide(string[] roles)
    {
        var count = 0;
        foreach (var role in roles)
        {
            if (role is "RB" or "LB" or "RWB" or "LWB" or "RW" or "LW")
            {
                count++;
            }
        }

        return count;
    }

    private static string PositionFamily(string position)
    {
        return position switch
        {
            "GK" => "GK",
            "RB" or "LB" or "CB" or "RWB" or "LWB" => "DEF",
            "CM" or "AM" => "MID",
            _ => "FWD"
        };
    }

    private static bool StyleMatchesPlayer(GameState.SquadPlayer player, TacticalTeamStyle style)
    {
        var styleText = $"{player.PlayingStyle} {player.Traits} {player.Tendencies}".ToLowerInvariant();
        return style switch
        {
            TacticalTeamStyle.HighPress => styleText.Contains("press", StringComparison.Ordinal) || styleText.Contains("training intensity", StringComparison.Ordinal),
            TacticalTeamStyle.Possession => styleText.Contains("tempo", StringComparison.Ordinal) || styleText.Contains("ball-playing", StringComparison.Ordinal) || styleText.Contains("sweeper", StringComparison.Ordinal),
            TacticalTeamStyle.DirectPlay => styleText.Contains("direct", StringComparison.Ordinal) || styleText.Contains("box", StringComparison.Ordinal) || styleText.Contains("aerial", StringComparison.Ordinal),
            TacticalTeamStyle.Counterattack => styleText.Contains("channel", StringComparison.Ordinal) || styleText.Contains("runner", StringComparison.Ordinal) || styleText.Contains("forward", StringComparison.Ordinal),
            TacticalTeamStyle.WideAttack => styleText.Contains("overlap", StringComparison.Ordinal) || styleText.Contains("wide", StringComparison.Ordinal) || styleText.Contains("1v1", StringComparison.Ordinal),
            TacticalTeamStyle.CentralOverload => styleText.Contains("creator", StringComparison.Ordinal) || styleText.Contains("tempo", StringComparison.Ordinal) || styleText.Contains("between", StringComparison.Ordinal),
            TacticalTeamStyle.LowBlock or TacticalTeamStyle.DefensiveSolidity => styleText.Contains("discipline", StringComparison.Ordinal) || styleText.Contains("stopper", StringComparison.Ordinal) || styleText.Contains("handling", StringComparison.Ordinal),
            _ => true
        };
    }

    private static string ResolveRole(string position, TacticalTeamStyle style)
    {
        return position switch
        {
            "GK" => style == TacticalTeamStyle.Possession ? "sweeper keeper" : "goalkeeper",
            "RB" or "LB" => style == TacticalTeamStyle.WideAttack ? "attacking fullback" : "balanced fullback",
            "CB" => style == TacticalTeamStyle.LowBlock ? "box defender" : "central defender",
            "CM" => style == TacticalTeamStyle.HighPress ? "pressing midfielder" : "connector",
            "AM" => style == TacticalTeamStyle.CentralOverload ? "central creator" : "advanced midfielder",
            "RW" or "LW" => style == TacticalTeamStyle.WideAttack ? "touchline winger" : "inside forward",
            _ => style == TacticalTeamStyle.DirectPlay ? "target forward" : "pressing forward"
        };
    }
}
