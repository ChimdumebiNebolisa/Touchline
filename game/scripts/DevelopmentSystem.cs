using System;
using System.Collections.Generic;

public static class DevelopmentSystem
{
    public sealed class DevelopmentUpdate
    {
        public required GameState.SquadPlayer[] SquadPlayers { get; init; }
        public required string Summary { get; init; }
        public required string[] HistoryEntries { get; init; }
    }

    public static GameState.SquadPlayer[] ApplyPostMatchChanges(
        GameState.SquadPlayer[] squadPlayers,
        string selectedClubName,
        MatchPlaybackResult result)
    {
        var scorerIds = new HashSet<string>();
        var keeperSaveIds = new HashSet<string>();
        var interventionIds = new HashSet<string>();
        foreach (var action in result.Timeline.Actions)
        {
            if (action.Team != selectedClubName)
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(action.Participants.ScorerPlayerId))
            {
                scorerIds.Add(action.Participants.ScorerPlayerId);
            }

            if (!string.IsNullOrWhiteSpace(action.Participants.GoalkeeperPlayerId))
            {
                keeperSaveIds.Add(action.Participants.GoalkeeperPlayerId);
            }

            if (!string.IsNullOrWhiteSpace(action.Participants.ClearerPlayerId))
            {
                interventionIds.Add(action.Participants.ClearerPlayerId);
            }

            if (!string.IsNullOrWhiteSpace(action.Participants.InterceptorPlayerId))
            {
                interventionIds.Add(action.Participants.InterceptorPlayerId);
            }
        }

        var goalDifference = result.FinalHomeScore - result.FinalAwayScore;
        var squadMoraleDelta = goalDifference > 0 ? 2 : goalDifference == 0 ? 0 : -2;
        var heavyLossPenalty = goalDifference <= -2 ? -1 : 0;
        var updated = new GameState.SquadPlayer[squadPlayers.Length];

        for (var index = 0; index < squadPlayers.Length; index++)
        {
            var player = squadPlayers[index];
            var playerId = ClubSquadFactory.BuildPlayerId(selectedClubName, player.Name, index);
            var fitnessDelta = player.IsStarting
                ? player.Position == "GK" ? -3 : -7
                : 1;
            var formDelta = heavyLossPenalty;
            var moraleDelta = squadMoraleDelta + heavyLossPenalty;

            if (scorerIds.Contains(playerId))
            {
                formDelta += 2;
                moraleDelta += 2;
            }

            if (keeperSaveIds.Contains(playerId))
            {
                formDelta += 1;
                moraleDelta += 1;
            }

            if (interventionIds.Contains(playerId))
            {
                formDelta += 1;
            }

            var nextFitness = Math.Clamp(player.Fitness + fitnessDelta, 40, 99);
            updated[index] = player.With(
                form: Math.Clamp(player.Form + formDelta, 45, 95),
                morale: Math.Clamp(player.Morale + moraleDelta, 35, 95),
                fitness: nextFitness,
                fatigue: Math.Clamp(player.Fatigue + (player.IsStarting ? 9 : -4), 0, 100),
                injuryRisk: Math.Clamp(player.InjuryRisk + (player.IsStarting && nextFitness < 70 ? 3 : -1), 0, 100));
        }

        return updated;
    }

    public static GameState.SquadPlayer[] ApplySeasonRollover(
        GameState.SquadPlayer[] squadPlayers,
        int worldSeed,
        int seasonStartYear)
    {
        return Array.ConvertAll(
            squadPlayers,
            player =>
            {
                var nextAge = player.Age + 1;
                var rng = new Random(BuildStableSeed(worldSeed, seasonStartYear, player.Name, player.Position));
                var formDelta = CalculateFormDelta(nextAge, rng);
                var fitnessDelta = CalculateFitnessDelta(nextAge, rng);
                var moraleDelta = formDelta > 0 ? 2 : formDelta < 0 ? -2 : 0;
                var abilityDelta = CalculateSeasonAbilityDelta(player, nextAge, rng);
                var nextAbility = Math.Clamp(player.TrueAbility + abilityDelta, 35, 95);

                return player.With(
                    trueAbility: nextAbility,
                    technicalAttribute: Math.Clamp(player.TechnicalAttribute + Math.Clamp(abilityDelta, -1, 1), 1, 99),
                    tacticalAttribute: Math.Clamp(player.TacticalAttribute + Math.Clamp(abilityDelta, -1, 1), 1, 99),
                    physicalAttribute: Math.Clamp(player.PhysicalAttribute + (nextAge >= 30 ? Math.Min(0, abilityDelta) : Math.Max(0, abilityDelta)), 1, 99),
                    mentalAttribute: Math.Clamp(player.MentalAttribute + (nextAge >= 28 ? Math.Max(0, -abilityDelta) : Math.Clamp(abilityDelta, -1, 1)), 1, 99),
                    age: nextAge,
                    form: Math.Clamp(player.Form + formDelta, 55, 90),
                    morale: Math.Clamp(player.Morale + moraleDelta, 50, 90),
                    fitness: Math.Clamp(player.Fitness + fitnessDelta, 72, 95),
                    fatigue: Math.Clamp(player.Fatigue - 8, 0, 100),
                    injuryRisk: Math.Clamp(player.InjuryRisk + (nextAge >= 30 ? 2 : -1), 0, 100),
                    developmentCurve: BuildSeasonDevelopmentCurve(player, nextAge, abilityDelta));
            });
    }

    public static DevelopmentUpdate ApplyWeeklyDevelopment(
        GameState.SquadPlayer[] squadPlayers,
        string selectedClubName,
        int worldSeed,
        DateTime currentDate,
        string trainingFocusName,
        string trainingIntensityName,
        int developmentStaffScore,
        int youthAcademyQuality,
        int youthCoachingQuality)
    {
        var updated = new GameState.SquadPlayer[squadPlayers.Length];
        var notes = new List<string>();
        var abilityChanges = 0;
        var conditionChanges = 0;
        var youthChanges = 0;
        var seniorDeclines = 0;
        var intensityFatigue = trainingIntensityName switch
        {
            "Demanding" => 4,
            "Controlled" => -2,
            _ => 1
        };
        var staffModifier = Math.Clamp((developmentStaffScore - 55) / 12, -2, 3);

        for (var index = 0; index < squadPlayers.Length; index++)
        {
            var player = squadPlayers[index];
            var seed = BuildStableSeed(worldSeed, currentDate.DayOfYear, selectedClubName, $"{player.Name}-{index}");
            var trainingDelta = BuildTrainingDevelopmentDelta(player, trainingFocusName, staffModifier, youthAcademyQuality, youthCoachingQuality, seed);
            var minutesDelta = player.IsStarting ? player.Age <= 23 ? 1 : 0 : player.Age <= 21 ? -1 : 0;
            var moraleDelta = trainingFocusName == "Team cohesion" ? 1 : 0;
            var recoveryDelta = trainingFocusName == "Recovery" ? -5 : 0;
            var fitnessDelta = trainingFocusName == "Fitness"
                ? Math.Clamp(2 + staffModifier, 0, 4)
                : trainingFocusName == "Recovery" ? 2 : 0;
            var fatigueDelta = Math.Clamp(intensityFatigue + (player.IsStarting ? 1 : -1) - (trainingFocusName == "Recovery" ? 5 : 0), -7, 8);
            var injuryDelta = Math.Clamp((player.Fatigue + fatigueDelta) / 22 - staffModifier - (trainingFocusName == "Recovery" ? 2 : 0), -4, 5);
            var seniorDecline = player.Age >= 31 && (player.Fatigue > 35 || player.InjuryRisk > 28) ? -1 : 0;
            var abilityDelta = Math.Clamp(trainingDelta + minutesDelta + seniorDecline, -1, 1);
            if (player.TrueAbility >= ResolveDevelopmentCeiling(player))
            {
                abilityDelta = Math.Min(0, abilityDelta);
            }

            var nextAbility = Math.Clamp(player.TrueAbility + abilityDelta, 35, 95);
            var nextForm = Math.Clamp(player.Form + Math.Sign(abilityDelta) + (player.IsStarting ? 1 : 0), 40, 96);
            var nextMorale = Math.Clamp(player.Morale + moraleDelta + minutesDelta, 30, 96);
            var nextFitness = Math.Clamp(player.Fitness + fitnessDelta - Math.Max(0, fatigueDelta / 3), 35, 99);
            var nextFatigue = Math.Clamp(player.Fatigue + fatigueDelta, 0, 100);
            var nextInjuryRisk = Math.Clamp(player.InjuryRisk + injuryDelta + (player.Age >= 30 ? 1 : 0), 0, 100);
            var transferInterest = player.TransferInterest;
            if (!player.IsStarting && player.Age <= 21 && transferInterest.Contains("Loan", StringComparison.OrdinalIgnoreCase))
            {
                transferInterest = "Development loan review: minutes, loan club fit, and recall review should be checked before next pathway decision.";
            }

            var developmentCurve = BuildWeeklyDevelopmentCurve(player, abilityDelta, trainingFocusName, trainingIntensityName, staffModifier, minutesDelta);
            updated[index] = player.With(
                trueAbility: nextAbility,
                technicalAttribute: Math.Clamp(player.TechnicalAttribute + ResolveAttributeDelta(abilityDelta, trainingFocusName, "technical"), 1, 99),
                tacticalAttribute: Math.Clamp(player.TacticalAttribute + ResolveAttributeDelta(abilityDelta, trainingFocusName, "tactical"), 1, 99),
                physicalAttribute: Math.Clamp(player.PhysicalAttribute + ResolveAttributeDelta(abilityDelta, trainingFocusName, "physical"), 1, 99),
                mentalAttribute: Math.Clamp(player.MentalAttribute + ResolveAttributeDelta(abilityDelta, trainingFocusName, "mental"), 1, 99),
                form: nextForm,
                morale: nextMorale,
                fitness: nextFitness,
                fatigue: nextFatigue,
                injuryRisk: nextInjuryRisk,
                developmentCurve: developmentCurve,
                transferInterest: transferInterest);

            if (abilityDelta != 0)
            {
                abilityChanges++;
            }

            if (nextFitness != player.Fitness || nextFatigue != player.Fatigue || nextInjuryRisk != player.InjuryRisk)
            {
                conditionChanges++;
            }

            if (player.Age <= 21 && (abilityDelta > 0 || minutesDelta != 0))
            {
                youthChanges++;
            }

            if (seniorDecline < 0)
            {
                seniorDeclines++;
            }

            if (notes.Count < 5 && (abilityDelta != 0 || player.Age <= 21 || seniorDecline < 0))
            {
                notes.Add($"{player.Name}: ability {FormatSigned(abilityDelta)}, minutes {DescribeMinutes(player)}, fatigue {player.Fatigue}->{nextFatigue}, injury risk {player.InjuryRisk}->{nextInjuryRisk}.");
            }
        }

        if (notes.Count == 0 && squadPlayers.Length > 0)
        {
            var player = updated[0];
            notes.Add($"{player.Name}: stable week; development held by current role, condition, and training load.");
        }

        var summary = $"Development cadence | focus {trainingFocusName}, intensity {trainingIntensityName}, staff score {developmentStaffScore}, academy {youthAcademyQuality}, coaching {youthCoachingQuality}. Ability changes {abilityChanges}, condition changes {conditionChanges}, youth pathway notes {youthChanges}, senior decline notes {seniorDeclines}.";
        return new DevelopmentUpdate
        {
            SquadPlayers = updated,
            Summary = summary,
            HistoryEntries = notes.ToArray()
        };
    }

    private static int CalculateFormDelta(int age, Random rng)
    {
        if (age <= 22)
        {
            return 1 + rng.Next(0, 3);
        }

        if (age <= 28)
        {
            return rng.Next(-1, 2);
        }

        return -1 - rng.Next(0, 3);
    }

    private static int CalculateFitnessDelta(int age, Random rng)
    {
        if (age <= 22)
        {
            return rng.Next(0, 2);
        }

        if (age <= 28)
        {
            return rng.Next(-1, 2);
        }

        return -1 - rng.Next(0, 2);
    }

    private static int CalculateSeasonAbilityDelta(GameState.SquadPlayer player, int nextAge, Random rng)
    {
        if (nextAge <= 22)
        {
            return player.TrueAbility < ResolveDevelopmentCeiling(player) ? rng.Next(0, 2) : 0;
        }

        if (nextAge <= 29)
        {
            return player.Form >= 74 && rng.Next(0, 3) == 0 ? 1 : 0;
        }

        return player.Fitness <= 78 || player.InjuryRisk >= 28 ? -1 : 0;
    }

    private static int BuildTrainingDevelopmentDelta(
        GameState.SquadPlayer player,
        string trainingFocusName,
        int staffModifier,
        int youthAcademyQuality,
        int youthCoachingQuality,
        int seed)
    {
        var youthBonus = player.Age <= 21 && trainingFocusName == "Youth integration" ? 2 : 0;
        var technicalBonus = trainingFocusName is "Possession" or "Attacking movement" ? 1 : 0;
        var tacticalBonus = trainingFocusName is "Defensive shape" or "Pressing" or "Counterattack" ? 1 : 0;
        var academyBonus = player.Age <= 21 ? (youthAcademyQuality + youthCoachingQuality) / 45 : 0;
        var score = player.Form / 18 + player.Morale / 22 + staffModifier + youthBonus + technicalBonus + tacticalBonus + academyBonus - player.Fatigue / 25 - player.InjuryRisk / 30;
        var variance = seed % 5 == 0 ? 1 : 0;
        if (score + variance >= 8)
        {
            return 1;
        }

        if (player.Age >= 31 && score <= 3)
        {
            return -1;
        }

        return 0;
    }

    private static int ResolveAttributeDelta(int abilityDelta, string trainingFocusName, string attributeGroup)
    {
        if (abilityDelta == 0)
        {
            return 0;
        }

        return attributeGroup switch
        {
            "technical" when trainingFocusName is "Possession" or "Attacking movement" or "Set pieces" => abilityDelta,
            "tactical" when trainingFocusName is "Defensive shape" or "Pressing" or "Counterattack" or "Team cohesion" => abilityDelta,
            "physical" when trainingFocusName is "Fitness" or "Pressing" => abilityDelta,
            "mental" when trainingFocusName is "Team cohesion" or "Recovery" or "Youth integration" => abilityDelta,
            _ => abilityDelta > 0 ? 0 : abilityDelta
        };
    }

    private static int ResolveDevelopmentCeiling(GameState.SquadPlayer player)
    {
        if (TryReadPotentialTop(player.EstimatedAttributesSummary, out var potentialTop))
        {
            return potentialTop;
        }

        if (player.Age <= 21)
        {
            return Math.Clamp(player.TrueAbility + 10, 60, 88);
        }

        if (player.Age <= 27)
        {
            return Math.Clamp(player.TrueAbility + 4, 55, 86);
        }

        return Math.Clamp(player.TrueAbility + 1, 50, 84);
    }

    private static bool TryReadPotentialTop(string summary, out int potentialTop)
    {
        potentialTop = 0;
        const string marker = "potential ";
        var markerIndex = summary.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (markerIndex < 0)
        {
            return false;
        }

        var start = markerIndex + marker.Length;
        var dash = summary.IndexOf('-', start);
        if (dash < 0)
        {
            return false;
        }

        var end = dash + 1;
        while (end < summary.Length && char.IsDigit(summary[end]))
        {
            end++;
        }

        return int.TryParse(summary[(dash + 1)..end], out potentialTop);
    }

    private static string BuildWeeklyDevelopmentCurve(
        GameState.SquadPlayer player,
        int abilityDelta,
        string trainingFocusName,
        string trainingIntensityName,
        int staffModifier,
        int minutesDelta)
    {
        var movement = abilityDelta > 0 ? "growth" : abilityDelta < 0 ? "decline risk" : "stable";
        return $"Weekly development: {movement}; focus {trainingFocusName}, intensity {trainingIntensityName}, minutes {DescribeMinutes(player)}, staff modifier {staffModifier}, minutes effect {FormatSigned(minutesDelta)}.";
    }

    private static string BuildSeasonDevelopmentCurve(GameState.SquadPlayer player, int nextAge, int abilityDelta)
    {
        if (nextAge <= 22)
        {
            return $"Season development: aged to {nextAge}; youth pathway produced ability movement {FormatSigned(abilityDelta)}.";
        }

        if (nextAge >= 30)
        {
            return $"Season development: aged to {nextAge}; senior condition produced ability movement {FormatSigned(abilityDelta)}.";
        }

        return $"Season development: aged to {nextAge}; prime-cycle ability movement {FormatSigned(abilityDelta)}.";
    }

    private static string DescribeMinutes(GameState.SquadPlayer player)
    {
        return player.IsStarting ? "starter minutes" : "limited minutes";
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }

    private static int BuildStableSeed(int worldSeed, int seasonStartYear, string playerName, string playerPosition)
    {
        unchecked
        {
            var hash = 17;
            hash = (hash * 31) + worldSeed;
            hash = (hash * 31) + seasonStartYear;
            hash = AddStableStringHash(hash, playerName);
            hash = AddStableStringHash(hash, playerPosition);
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
