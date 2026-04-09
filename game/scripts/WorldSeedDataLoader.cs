using Godot;
using System;
using System.Globalization;
using System.Text.Json;

public static class WorldSeedDataLoader
{
    private const string SeedDataPath = "res://data/world-seed.json";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public static bool TryLoad(out WorldSeedData seedData, out string errorMessage)
    {
        seedData = new WorldSeedData();

        if (!FileAccess.FileExists(SeedDataPath))
        {
            errorMessage = $"World seed data is missing at {SeedDataPath}.";
            return false;
        }

        try
        {
            using var file = FileAccess.Open(SeedDataPath, FileAccess.ModeFlags.Read);
            var json = file.GetAsText();
            var payload = JsonSerializer.Deserialize<WorldSeedData>(json, JsonOptions);

            if (payload == null)
            {
                errorMessage = "World seed data could not be deserialized.";
                return false;
            }

            if (!TryValidate(payload, out errorMessage))
            {
                return false;
            }

            seedData = payload;
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = $"World seed load failed: {ex.Message}";
            return false;
        }
    }

    public static bool TryParseStartDate(string startDateIso, out DateTime startDate)
    {
        return DateTime.TryParseExact(
            startDateIso,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out startDate);
    }

    private static bool TryValidate(WorldSeedData payload, out string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(payload.CountryPackId))
        {
            errorMessage = "World seed data is missing CountryPackId.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(payload.CompetitionName))
        {
            errorMessage = "World seed data is missing CompetitionName.";
            return false;
        }

        if (!TryParseStartDate(payload.StartDateIso, out _))
        {
            errorMessage = "World seed data has an invalid StartDateIso. Expected yyyy-MM-dd.";
            return false;
        }

        if (payload.Clubs.Length < 4)
        {
            errorMessage = "World seed data must provide at least four clubs for the shipped competition.";
            return false;
        }

        foreach (var club in payload.Clubs)
        {
            if (string.IsNullOrWhiteSpace(club.Name))
            {
                errorMessage = "World seed data contains a club without a name.";
                return false;
            }

            if (club.Players.Length < 11)
            {
                errorMessage = $"Club {club.Name} does not have enough named players in the seed data.";
                return false;
            }

            foreach (var player in club.Players)
            {
                if (string.IsNullOrWhiteSpace(player.Name) || string.IsNullOrWhiteSpace(player.Position))
                {
                    errorMessage = $"Club {club.Name} contains an incomplete player record.";
                    return false;
                }

                if (player.Name.StartsWith("Player ", StringComparison.OrdinalIgnoreCase))
                {
                    errorMessage = $"Club {club.Name} still contains placeholder player naming.";
                    return false;
                }
            }
        }

        errorMessage = string.Empty;
        return true;
    }
}
