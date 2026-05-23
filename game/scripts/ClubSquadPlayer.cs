using System;
using System.Collections.Generic;
using System.Text;

public sealed class ClubSquadPlayer
{
    public required string PlayerId { get; init; }
    public required string ClubName { get; init; }
    public required string Name { get; init; }
    public required string Position { get; init; }
    public required int Age { get; init; }
    public required int Form { get; init; }
    public required int Morale { get; init; }
    public required int Fitness { get; init; }
    public required bool IsStarting { get; init; }
}

public static class ClubSquadFactory
{
    private static readonly string[] StartingPositions =
    {
        "GK", "RB", "CB", "CB", "LB", "CM", "CM", "AM", "RW", "ST", "LW"
    };

    private static readonly string[] FallbackFirstNames =
    {
        "Aron", "Bence", "Ciro", "Dusan", "Emil", "Faris", "Goran", "Hugo",
        "Ivan", "Jules", "Kamil", "Loren", "Marek", "Niko", "Oskar", "Pavel"
    };

    private static readonly string[] FallbackLastNames =
    {
        "Adler", "Bari", "Cortes", "Doyle", "Eriksen", "Faye", "Grava", "Hale",
        "Ilic", "Jovan", "Kone", "Larsen", "Matic", "Novak", "Orban", "Petrov"
    };

    public static ClubSquadPlayer[] FromSeedClub(WorldSeedClubData clubData, int worldSeed)
    {
        var squad = new ClubSquadPlayer[clubData.Players.Length];
        for (var index = 0; index < clubData.Players.Length; index++)
        {
            var player = clubData.Players[index];
            squad[index] = new ClubSquadPlayer
            {
                PlayerId = BuildPlayerId(clubData.Name, player.Name, index),
                ClubName = clubData.Name,
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Form = player.Form,
                Morale = player.Morale,
                Fitness = player.Fitness,
                IsStarting = player.IsStarting
            };
        }

        return EnsurePlayableSquad(clubData.Name, worldSeed, squad);
    }

    public static ClubSquadPlayer[] BuildFallbackSquad(string clubName, int worldSeed)
    {
        var seed = BuildStableSeed(clubName, worldSeed);
        var rng = new Random(seed);
        var squad = new ClubSquadPlayer[15];

        for (var index = 0; index < squad.Length; index++)
        {
            var position = index < StartingPositions.Length
                ? StartingPositions[index]
                : StartingPositions[(index * 3 + 2) % StartingPositions.Length];
            var firstName = FallbackFirstNames[(index + rng.Next(0, FallbackFirstNames.Length)) % FallbackFirstNames.Length];
            var lastName = FallbackLastNames[(index * 5 + rng.Next(0, FallbackLastNames.Length)) % FallbackLastNames.Length];
            var name = $"{firstName} {lastName}";

            squad[index] = new ClubSquadPlayer
            {
                PlayerId = BuildPlayerId(clubName, name, index),
                ClubName = clubName,
                Name = name,
                Position = position,
                Age = 19 + ((seed + index * 7) % 13),
                Form = 62 + ((seed + index * 5) % 18),
                Morale = 62 + ((seed + index * 3) % 18),
                Fitness = 80 + ((seed + index * 4) % 13),
                IsStarting = index < 11
            };
        }

        return squad;
    }

    public static string BuildPlayerId(string clubName, string playerName, int squadIndex)
    {
        var key = $"{clubName}|{playerName}|{squadIndex}";
        return $"{BuildSlug(clubName)}-{BuildSlug(playerName)}-{squadIndex:00}-{StableHash(key):x8}";
    }

    private static ClubSquadPlayer[] EnsurePlayableSquad(string clubName, int worldSeed, ClubSquadPlayer[] squad)
    {
        if (squad.Length >= 11)
        {
            return squad;
        }

        var fallback = BuildFallbackSquad(clubName, worldSeed);
        var completedSquad = new List<ClubSquadPlayer>(11);
        completedSquad.AddRange(squad);
        for (var index = 0; completedSquad.Count < 11 && index < fallback.Length; index++)
        {
            completedSquad.Add(fallback[index]);
        }

        return completedSquad.ToArray();
    }

    private static int BuildStableSeed(string clubName, int worldSeed)
    {
        var hash = StableHash($"{worldSeed}|{clubName}|fallback-squad");
        return (int)(hash & 0x7fffffff);
    }

    private static string BuildSlug(string value)
    {
        var builder = new StringBuilder();
        foreach (var character in value.ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(character))
            {
                builder.Append(character);
            }
            else if (builder.Length > 0 && builder[^1] != '-')
            {
                builder.Append('-');
            }
        }

        return builder.ToString().Trim('-');
    }

    private static uint StableHash(string value)
    {
        const uint offset = 2166136261;
        const uint prime = 16777619;
        var hash = offset;

        foreach (var character in value)
        {
            hash ^= character;
            hash *= prime;
        }

        return hash;
    }
}
