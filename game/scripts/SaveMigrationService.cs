using System;

public static class SaveMigrationService
{
    public static bool TryUpgradeLegacyPayload(SaveSlotData payload, out SaveSlotData migratedPayload, out string errorMessage)
    {
        migratedPayload = payload;

        if (string.IsNullOrWhiteSpace(payload.SelectedClubName))
        {
            errorMessage = "Legacy save migration failed because the selected club is missing.";
            return false;
        }

        if (!TryResolveAvailableClubs(payload, out var availableClubs, out errorMessage))
        {
            return false;
        }

        var initialCompetitionState = CompetitionRuntimeService.BuildInitialState(availableClubs, payload.SelectedClubName);
        var seasonLength = CompetitionRuntimeService.GetSeasonMatchdayCount(initialCompetitionState.fixtures);
        var completedMatchdays = Math.Clamp(payload.CurrentMatchday - 1, 0, seasonLength - 1);
        var resultTokens = payload.RecentResults ?? Array.Empty<string>();

        if (completedMatchdays > resultTokens.Length)
        {
            errorMessage = "Legacy save migration failed because there are not enough stored recent results to rebuild the competition state.";
            return false;
        }

        var competitionState = initialCompetitionState;
        for (var matchday = 1; matchday <= completedMatchdays; matchday++)
        {
            var fixture = CompetitionRuntimeService.GetCurrentClubFixture(competitionState.fixtures, matchday, payload.SelectedClubName);
            if (fixture == null)
            {
                errorMessage = $"Legacy save migration failed because matchday {matchday} could not be resolved for {payload.SelectedClubName}.";
                return false;
            }

            var selectedClubIsHome = fixture.HomeClubName == payload.SelectedClubName;
            var scoreline = TryBuildKnownScoreline(payload, matchday, completedMatchdays, out var exactScoreline)
                ? exactScoreline
                : BuildDeterministicClubScoreline(
                    resultTokens[completedMatchdays - matchday],
                    payload.WorldSeed,
                    payload.SeasonStartYear,
                    matchday,
                    selectedClubIsHome);

            competitionState = CompetitionRuntimeService.ApplyMatchdayResult(
                availableClubs,
                competitionState.fixtures,
                matchday,
                payload.SelectedClubName,
                scoreline.homeGoals,
                scoreline.awayGoals,
                payload.WorldSeed,
                payload.SeasonStartYear);
        }

        migratedPayload = SaveSystem.BuildSaveCopy(payload);
        migratedPayload.SaveVersion = SaveSystem.CurrentSaveVersion;
        migratedPayload.AvailableClubs = availableClubs;
        migratedPayload.CompetitionTable = Array.ConvertAll(
            competitionState.table,
            row => new SaveSlotCompetitionRowData
            {
                ClubName = row.ClubName,
                Played = row.Played,
                Won = row.Won,
                Drawn = row.Drawn,
                Lost = row.Lost,
                GoalsFor = row.GoalsFor,
                GoalsAgainst = row.GoalsAgainst,
                Points = row.Points
            });
        migratedPayload.CompetitionFixtures = Array.ConvertAll(
            competitionState.fixtures,
            fixture => new SaveSlotCompetitionFixtureData
            {
                Matchday = fixture.Matchday,
                HomeClubName = fixture.HomeClubName,
                AwayClubName = fixture.AwayClubName,
                IsComplete = fixture.IsComplete,
                Scoreline = fixture.Scoreline,
                ResultSummary = fixture.ResultSummary
            });

        errorMessage = "Legacy save migrated successfully.";
        return true;
    }

    private static bool TryResolveAvailableClubs(SaveSlotData payload, out string[] availableClubs, out string errorMessage)
    {
        availableClubs = payload.AvailableClubs ?? Array.Empty<string>();
        if (availableClubs.Length >= 4)
        {
            errorMessage = string.Empty;
            return true;
        }

        if (!WorldSeedDataLoader.TryLoad(out var seedData, out errorMessage))
        {
            return false;
        }

        availableClubs = Array.ConvertAll(seedData.Clubs, club => club.Name);
        if (Array.IndexOf(availableClubs, payload.SelectedClubName) < 0)
        {
            errorMessage = "Legacy save migration failed because the selected club does not exist in the current world seed data.";
            return false;
        }

        return true;
    }

    private static bool TryBuildKnownScoreline(
        SaveSlotData payload,
        int matchday,
        int completedMatchdays,
        out (int homeGoals, int awayGoals) scoreline)
    {
        scoreline = default;

        if (payload.LastMatchReport == null || matchday != completedMatchdays)
        {
            return false;
        }

        return TryParseScoreline(payload.LastMatchReport.Scoreline, out scoreline);
    }

    private static (int homeGoals, int awayGoals) BuildDeterministicClubScoreline(
        string resultToken,
        int worldSeed,
        int seasonStartYear,
        int matchday,
        bool selectedClubIsHome)
    {
        var seed = Math.Abs(HashCode.Combine(worldSeed, seasonStartYear, matchday, resultToken, "legacy-migration"));
        var rng = new Random(seed);
        var clubGoals = 0;
        var opponentGoals = 0;

        switch (resultToken)
        {
            case "W":
                clubGoals = 1 + rng.Next(1, 3);
                opponentGoals = rng.Next(0, clubGoals);
                break;
            case "D":
                clubGoals = rng.Next(0, 2);
                opponentGoals = clubGoals;
                break;
            case "L":
                opponentGoals = 1 + rng.Next(1, 3);
                clubGoals = rng.Next(0, opponentGoals);
                break;
            default:
                clubGoals = 1;
                opponentGoals = 1;
                break;
        }

        return selectedClubIsHome
            ? (clubGoals, opponentGoals)
            : (opponentGoals, clubGoals);
    }

    private static bool TryParseScoreline(string scoreline, out (int homeGoals, int awayGoals) goals)
    {
        goals = default;
        var tokens = scoreline.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2 || !int.TryParse(tokens[0], out var homeGoals) || !int.TryParse(tokens[1], out var awayGoals))
        {
            return false;
        }

        goals = (homeGoals, awayGoals);
        return true;
    }
}
