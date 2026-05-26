using System;

public enum PlayerKnowledgeContext
{
    OwnSquad,
    ScoutedTarget,
    UnknownTarget
}

public sealed class PlayerInformationReport
{
    public required int KnowledgeScore { get; init; }
    public required string KnowledgeLabel { get; init; }
    public required string KnownAttributesSummary { get; init; }
    public required string EstimatedAttributesSummary { get; init; }
    public required string UnknownAttributesSummary { get; init; }
    public required string PersonalitySummary { get; init; }
    public required string TacticalFitSummary { get; init; }
    public required string DevelopmentSummary { get; init; }
    public required string RiskSummary { get; init; }
    public string FullSummary =>
        $"{KnowledgeLabel}\n{KnownAttributesSummary}\n{EstimatedAttributesSummary}\n{UnknownAttributesSummary}\n{PersonalitySummary}\n{TacticalFitSummary}\n{DevelopmentSummary}\n{RiskSummary}";
}

public static class PlayerInformationVisibility
{
    public static PlayerInformationReport BuildReport(
        GameState.SquadPlayer player,
        PlayerKnowledgeContext context,
        ManagerRole role,
        ManagerLicense license,
        int scoutQuality,
        int dataAnalystQuality,
        int staffQuality,
        int reportQuality,
        int difficultyKnowledgeModifier = 0)
    {
        var knowledgeScore = CalculateKnowledgeScore(
            context,
            role,
            license,
            scoutQuality,
            dataAnalystQuality,
            staffQuality,
            reportQuality,
            player.PlayerFamiliarity,
            player.ScoutingConfidence,
            difficultyKnowledgeModifier);

        return BuildReportFromScore(player, context, knowledgeScore, license, scoutQuality, dataAnalystQuality, staffQuality);
    }

    public static PlayerInformationReport BuildReport(
        ClubSquadPlayer player,
        PlayerKnowledgeContext context,
        ManagerRole role,
        ManagerLicense license,
        int scoutQuality,
        int dataAnalystQuality,
        int staffQuality,
        int reportQuality,
        int difficultyKnowledgeModifier = 0)
    {
        var squadPlayer = new GameState.SquadPlayer
        {
            PlayerId = player.PlayerId,
            Name = player.Name,
            Position = player.Position,
            Age = player.Age,
            Nationality = "Unconfirmed",
            TrueAbility = player.TrueAbility,
            TechnicalAttribute = Math.Clamp(player.TrueAbility + PositionTechnicalModifier(player.Position), 35, 95),
            TacticalAttribute = Math.Clamp(player.TacticalFitScore, 35, 95),
            PhysicalAttribute = Math.Clamp((player.TrueAbility + player.Fitness) / 2, 35, 96),
            MentalAttribute = Math.Clamp((player.TrueAbility + player.Morale) / 2, 35, 96),
            PlayingStyle = player.PlayingStyle,
            Tendencies = "Tendencies require repeated scouting.",
            Traits = "Traits partly observed.",
            Personality = "Personality profile incomplete",
            TacticalFit = player.TacticalFit,
            DevelopmentCurve = player.Age <= 21 ? "Development view: upward but still uncertain." : "Development view: current-level read only.",
            Form = player.Form,
            Morale = player.Morale,
            Fitness = player.Fitness,
            Fatigue = player.Fatigue,
            InjuryRisk = Math.Clamp(14 + player.Fatigue / 3, 5, 70),
            Wage = 18000 + player.TrueAbility * 700,
            ContractExpiryYear = 2028,
            ContractRole = player.IsStarting ? "Important Player" : "Squad Player",
            Relationship = "External player relationship unknown",
            PromiseSummary = "No promise recorded.",
            TransferInterest = "Interest requires recruitment contact.",
            TacticalFitScore = player.TacticalFitScore,
            PlayerFamiliarity = context == PlayerKnowledgeContext.ScoutedTarget ? 18 : 4,
            ScoutingConfidence = Math.Clamp(reportQuality, 0, 100),
            KnownAttributeGroups = "form,fitness",
            EstimatedAttributeGroups = "technical,tactical,physical",
            UnknownAttributeGroups = "mental,personality,potential,agent loyalty",
            IsStarting = player.IsStarting
        };

        return BuildReport(
            squadPlayer,
            context,
            role,
            license,
            scoutQuality,
            dataAnalystQuality,
            staffQuality,
            reportQuality,
            difficultyKnowledgeModifier);
    }

    private static PlayerInformationReport BuildReportFromScore(
        GameState.SquadPlayer player,
        PlayerKnowledgeContext context,
        int knowledgeScore,
        ManagerLicense license,
        int scoutQuality,
        int dataAnalystQuality,
        int staffQuality)
    {
        var label = $"Knowledge: {DescribeKnowledge(knowledgeScore)} ({knowledgeScore}/100) | {CareerFoundation.GetDisplayName(license)} | scout {scoutQuality} | analyst {dataAnalystQuality} | staff {staffQuality}";
        if (knowledgeScore >= 78)
        {
            return new PlayerInformationReport
            {
                KnowledgeScore = knowledgeScore,
                KnowledgeLabel = label,
                KnownAttributesSummary = $"Known: Technical {player.TechnicalAttribute}, Tactical {player.TacticalAttribute}, Physical {player.PhysicalAttribute}, Mental {player.MentalAttribute}, form {player.Form}, fitness {player.Fitness}.",
                EstimatedAttributesSummary = $"Estimated: current ability band {BuildRange(player.TrueAbility, 2)}, potential/development {BuildDevelopmentBand(player)}.",
                UnknownAttributesSummary = "Unknown: exact future ceiling ?, exact agent loyalty ?, exact pressure response ?.",
                PersonalitySummary = $"Personality read: {player.Personality}. Traits: {player.Traits}. Tendencies: {player.Tendencies}",
                TacticalFitSummary = $"Tactical fit: {player.TacticalFitScore}/100. {player.TacticalFit}",
                DevelopmentSummary = player.DevelopmentCurve,
                RiskSummary = $"Risk: fatigue {player.Fatigue}, injury risk {player.InjuryRisk}; confidence is high but not absolute."
            };
        }

        if (knowledgeScore >= 60)
        {
            return new PlayerInformationReport
            {
                KnowledgeScore = knowledgeScore,
                KnowledgeLabel = label,
                KnownAttributesSummary = $"Known: Technical {player.TechnicalAttribute}, Physical {player.PhysicalAttribute}, form {player.Form}, fitness {player.Fitness}.",
                EstimatedAttributesSummary = $"Estimated: Tactical {BuildRange(player.TacticalAttribute, 4)}, Mental {BuildRange(player.MentalAttribute, 5)}, ability band {BuildRange(player.TrueAbility, 5)}.",
                UnknownAttributesSummary = "Unknown: exact potential ?, agent loyalty ?, pressure response ?.",
                PersonalitySummary = $"Personality clue: {player.Personality}; traits look like {player.Traits}.",
                TacticalFitSummary = $"Tactical fit language: {player.TacticalFit} Estimated fit {BuildRange(player.TacticalFitScore, 5)}.",
                DevelopmentSummary = BuildDevelopmentLanguage(player, true),
                RiskSummary = $"Risk: fatigue {player.Fatigue}; injury risk estimate {BuildRange(player.InjuryRisk, 4)}."
            };
        }

        if (knowledgeScore >= 40)
        {
            return new PlayerInformationReport
            {
                KnowledgeScore = knowledgeScore,
                KnowledgeLabel = label,
                KnownAttributesSummary = $"Known: form {player.Form}, fitness {player.Fitness}, role context {player.Position}.",
                EstimatedAttributesSummary = $"Estimated: Technical {BuildRange(player.TechnicalAttribute, 7)}, Physical {BuildRange(player.PhysicalAttribute, 7)}, Tactical {BuildRange(player.TacticalAttribute, 9)}.",
                UnknownAttributesSummary = "Unknown: Mental ?, potential ?, deeper personality ?, agent loyalty ?.",
                PersonalitySummary = "Personality clue: limited; staff need more contact before calling this reliable.",
                TacticalFitSummary = $"Tactical fit language: {BuildFitLanguage(player.TacticalFitScore, context)}",
                DevelopmentSummary = BuildDevelopmentLanguage(player, false),
                RiskSummary = $"Risk: fatigue visible at {player.Fatigue}; injury risk remains an estimate {BuildRange(player.InjuryRisk, 8)}."
            };
        }

        return new PlayerInformationReport
        {
            KnowledgeScore = knowledgeScore,
            KnowledgeLabel = label,
            KnownAttributesSummary = context == PlayerKnowledgeContext.OwnSquad
                ? $"Known: form {player.Form}, fitness {player.Fitness}."
                : "Known: current role only; no exact attributes trusted yet.",
            EstimatedAttributesSummary = $"Estimated: Technical {BuildRange(player.TechnicalAttribute, 12)}, Physical {BuildRange(player.PhysicalAttribute, 12)}.",
            UnknownAttributesSummary = "Unknown: Tactical ?, Mental ?, potential ?, personality ?, tactical fit ?, pressure response ?.",
            PersonalitySummary = "Personality clue: ?",
            TacticalFitSummary = "Tactical fit: ? until better scouting, staff contact, or match familiarity.",
            DevelopmentSummary = "Development view: ?; evidence too thin for a reliable pathway call.",
            RiskSummary = "Risk: workload and injury picture incomplete."
        };
    }

    private static int CalculateKnowledgeScore(
        PlayerKnowledgeContext context,
        ManagerRole role,
        ManagerLicense license,
        int scoutQuality,
        int dataAnalystQuality,
        int staffQuality,
        int reportQuality,
        int playerFamiliarity,
        int scoutingConfidence,
        int difficultyKnowledgeModifier)
    {
        var score = context switch
        {
            PlayerKnowledgeContext.OwnSquad => 28,
            PlayerKnowledgeContext.ScoutedTarget => 14,
            _ => 3
        };
        score += LicenseKnowledge(license);
        score += RoleKnowledge(role, context);
        score += context == PlayerKnowledgeContext.OwnSquad
            ? (staffQuality + dataAnalystQuality) / 6
            : (scoutQuality * 2 + dataAnalystQuality) / 8;
        score += reportQuality / (context == PlayerKnowledgeContext.OwnSquad ? 8 : 4);
        score += playerFamiliarity / (context == PlayerKnowledgeContext.OwnSquad ? 3 : 5);
        score += scoutingConfidence / (context == PlayerKnowledgeContext.OwnSquad ? 5 : 3);
        score += difficultyKnowledgeModifier;
        return Math.Clamp(score, 5, 96);
    }

    private static int LicenseKnowledge(ManagerLicense license)
    {
        return license switch
        {
            ManagerLicense.GrassrootsLicense => 2,
            ManagerLicense.NationalBLicense => 10,
            ManagerLicense.NationalALicense => 14,
            ManagerLicense.ProLicense => 18,
            _ => 6
        };
    }

    private static int RoleKnowledge(ManagerRole role, PlayerKnowledgeContext context)
    {
        return role switch
        {
            ManagerRole.AssistantManager => context == PlayerKnowledgeContext.OwnSquad ? 7 : 1,
            ManagerRole.HeadCoach => context == PlayerKnowledgeContext.OwnSquad ? 8 : 3,
            _ => context == PlayerKnowledgeContext.OwnSquad ? 9 : 5
        };
    }

    private static string DescribeKnowledge(int score)
    {
        return score switch
        {
            >= 78 => "high confidence",
            >= 60 => "strong working read",
            >= 40 => "partial read",
            _ => "low visibility"
        };
    }

    private static string BuildRange(int value, int width)
    {
        return $"{Math.Clamp(value - width, 1, 99)}-{Math.Clamp(value + width, 1, 99)}";
    }

    private static string BuildDevelopmentBand(GameState.SquadPlayer player)
    {
        var ceiling = player.Age <= 21 ? player.TrueAbility + 13 : player.Age >= 30 ? player.TrueAbility + 2 : player.TrueAbility + 6;
        return BuildRange(Math.Clamp(ceiling, 1, 99), player.Age <= 21 ? 6 : 4);
    }

    private static string BuildDevelopmentLanguage(GameState.SquadPlayer player, bool strongerRead)
    {
        if (player.Age <= 21)
        {
            return strongerRead
                ? $"Development view: upward pathway, likely first-team band {BuildRange(player.TrueAbility + 8, 6)}."
                : "Development view: upward signs, but the potential band is still wide.";
        }

        if (player.Age >= 30)
        {
            return strongerRead
                ? "Development view: senior maintenance; minutes and recovery matter more than growth."
                : "Development view: senior player; decline/risk needs staff monitoring.";
        }

        return strongerRead
            ? $"Development view: prime-cycle performer, ability stability around {BuildRange(player.TrueAbility, 4)}."
            : "Development view: prime-cycle player; current role evidence is more reliable than long-term projection.";
    }

    private static string BuildFitLanguage(int tacticalFitScore, PlayerKnowledgeContext context)
    {
        var fit = tacticalFitScore >= 72
            ? "looks compatible"
            : tacticalFitScore >= 58
                ? "looks workable"
                : "needs caution";
        return context == PlayerKnowledgeContext.OwnSquad
            ? $"{fit}; current squad familiarity gives some evidence."
            : $"{fit}; scouting confidence is not enough for a final call.";
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
}
