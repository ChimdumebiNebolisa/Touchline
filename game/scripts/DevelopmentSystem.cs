using System;
using System.Collections.Generic;

public static class DevelopmentSystem
{
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

            updated[index] = new GameState.SquadPlayer
            {
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Form = Math.Clamp(player.Form + formDelta, 45, 95),
                Morale = Math.Clamp(player.Morale + moraleDelta, 35, 95),
                Fitness = Math.Clamp(player.Fitness + fitnessDelta, 40, 99),
                IsStarting = player.IsStarting
            };
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
                var rng = new Random(Math.Abs(HashCode.Combine(worldSeed, seasonStartYear, player.Name, player.Position)));
                var formDelta = CalculateFormDelta(nextAge, rng);
                var fitnessDelta = CalculateFitnessDelta(nextAge, rng);
                var moraleDelta = formDelta > 0 ? 2 : formDelta < 0 ? -2 : 0;

                return new GameState.SquadPlayer
                {
                    Name = player.Name,
                    Position = player.Position,
                    Age = nextAge,
                    Form = Math.Clamp(player.Form + formDelta, 55, 90),
                    Morale = Math.Clamp(player.Morale + moraleDelta, 50, 90),
                    Fitness = Math.Clamp(player.Fitness + fitnessDelta, 72, 95),
                    IsStarting = player.IsStarting
                };
            });
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
}
