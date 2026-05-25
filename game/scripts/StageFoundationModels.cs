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
    public required string ExpectedAction { get; init; }
    public required string DeadlineSummary { get; init; }
    public required PromiseStatus Status { get; init; }
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
