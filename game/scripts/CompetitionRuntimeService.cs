using System;

public static class CompetitionRuntimeService
{
    public static (GameState.CompetitionRow[] table, GameState.CompetitionFixture[] fixtures) BuildInitialState(
        string[] availableClubs,
        string? selectedClubName)
    {
        if (availableClubs.Length == 0 || string.IsNullOrWhiteSpace(selectedClubName))
        {
            return (Array.Empty<GameState.CompetitionRow>(), Array.Empty<GameState.CompetitionFixture>());
        }

        var table = Array.ConvertAll(
            availableClubs,
            clubName => new GameState.CompetitionRow
            {
                ClubName = clubName,
                Played = 0,
                Won = 0,
                Drawn = 0,
                Lost = 0,
                GoalsFor = 0,
                GoalsAgainst = 0,
                Points = 0
            });

        return (table, BuildCompetitionFixtures(availableClubs, selectedClubName));
    }

    public static (GameState.CompetitionRow[] table, GameState.CompetitionFixture[] fixtures) ApplyMatchdayResult(
        string[] availableClubs,
        GameState.CompetitionFixture[] fixtures,
        int currentMatchday,
        string? selectedClubName,
        int homeGoals,
        int awayGoals,
        int worldSeed,
        int seasonStartYear)
    {
        if (string.IsNullOrWhiteSpace(selectedClubName))
        {
            return (Array.Empty<GameState.CompetitionRow>(), fixtures);
        }

        var updatedFixtures = Array.ConvertAll(
            fixtures,
            fixture => new GameState.CompetitionFixture
            {
                Matchday = fixture.Matchday,
                HomeClubName = fixture.HomeClubName,
                AwayClubName = fixture.AwayClubName,
                IsComplete = fixture.IsComplete,
                Scoreline = fixture.Scoreline,
                ResultSummary = fixture.ResultSummary
            });

        for (var index = 0; index < updatedFixtures.Length; index++)
        {
            var fixture = updatedFixtures[index];
            if (fixture.Matchday != currentMatchday)
            {
                continue;
            }

            if (fixture.HomeClubName == selectedClubName || fixture.AwayClubName == selectedClubName)
            {
                updatedFixtures[index] = BuildCompletedFixture(fixture, homeGoals, awayGoals);
                continue;
            }

            if (!fixture.IsComplete)
            {
                var shadowResult = BuildShadowFixtureResult(fixture, worldSeed, seasonStartYear);
                updatedFixtures[index] = BuildCompletedFixture(fixture, shadowResult.homeGoals, shadowResult.awayGoals);
            }
        }

        var table = RecalculateCompetitionTable(availableClubs, updatedFixtures);
        return (table, updatedFixtures);
    }

    public static (string currentOpponentName, string nextFixtureSummary) ResolveFixtureContext(
        GameState.CompetitionFixture[] fixtures,
        int currentMatchday,
        string? selectedClubName,
        string currentDateLabel)
    {
        if (string.IsNullOrWhiteSpace(selectedClubName))
        {
            return ("Harbor County", "Fixture context unavailable.");
        }

        var currentFixture = GetCurrentClubFixture(fixtures, currentMatchday, selectedClubName);
        if (currentFixture == null)
        {
            return ("Harbor County", "Fixture context unavailable.");
        }

        var currentOpponentName = currentFixture.HomeClubName == selectedClubName
            ? currentFixture.AwayClubName
            : currentFixture.HomeClubName;

        var nextFixtureSummary = currentFixture.IsComplete
            ? $"{currentDateLabel} | Matchday {currentMatchday} complete | {currentFixture.ResultSummary}"
            : $"{currentDateLabel} | Matchday {currentMatchday} | {currentFixture.HomeClubName} vs {currentFixture.AwayClubName}";

        return (currentOpponentName, nextFixtureSummary);
    }

    public static int GetClubTablePosition(GameState.CompetitionRow[] table, string clubName)
    {
        if (string.IsNullOrWhiteSpace(clubName))
        {
            return -1;
        }

        for (var index = 0; index < table.Length; index++)
        {
            if (table[index].ClubName == clubName)
            {
                return index + 1;
            }
        }

        return -1;
    }

    public static string BuildTableImpactSummary(
        GameState.CompetitionRow[] table,
        string? selectedClubName,
        int previousPosition,
        int currentPosition)
    {
        if (string.IsNullOrWhiteSpace(selectedClubName))
        {
            return "League table update could not resolve because no club is selected.";
        }

        var currentRow = GetCompetitionRow(table, selectedClubName);
        if (currentRow == null)
        {
            return $"{selectedClubName} finished the round, but the updated league row could not be resolved in the current pass.";
        }

        if (previousPosition < 0 || currentPosition < 0)
        {
            return $"{selectedClubName} now sit on {currentRow.Points} points with goal difference {FormatSigned(currentRow.GoalDifference)}.";
        }

        if (currentPosition < previousPosition)
        {
            return $"{selectedClubName} climb from {previousPosition} to {currentPosition} with {currentRow.Points} points.";
        }

        if (currentPosition > previousPosition)
        {
            return $"{selectedClubName} slip from {previousPosition} to {currentPosition} despite reaching {currentRow.Points} points.";
        }

        return $"{selectedClubName} hold position {currentPosition} on {currentRow.Points} points and goal difference {FormatSigned(currentRow.GoalDifference)}.";
    }

    public static GameState.CompetitionFixture? GetCurrentClubFixture(
        GameState.CompetitionFixture[] fixtures,
        int currentMatchday,
        string? selectedClubName)
    {
        if (string.IsNullOrWhiteSpace(selectedClubName))
        {
            return null;
        }

        foreach (var fixture in fixtures)
        {
            if (fixture.Matchday == currentMatchday &&
                (fixture.HomeClubName == selectedClubName || fixture.AwayClubName == selectedClubName))
            {
                return fixture;
            }
        }

        return null;
    }

    public static GameState.CompetitionRow? GetCompetitionRow(GameState.CompetitionRow[] table, string clubName)
    {
        foreach (var row in table)
        {
            if (row.ClubName == clubName)
            {
                return row;
            }
        }

        return null;
    }

    public static int GetSeasonMatchdayCount(GameState.CompetitionFixture[] fixtures)
    {
        var maxMatchday = 0;
        foreach (var fixture in fixtures)
        {
            if (fixture.Matchday > maxMatchday)
            {
                maxMatchday = fixture.Matchday;
            }
        }

        return maxMatchday == 0 ? 6 : maxMatchday;
    }

    private static GameState.CompetitionFixture[] BuildCompetitionFixtures(string[] availableClubs, string selectedClubName)
    {
        var rivals = Array.FindAll(availableClubs, clubName => clubName != selectedClubName);
        if (rivals.Length < 3)
        {
            return Array.Empty<GameState.CompetitionFixture>();
        }

        return new[]
        {
            CreateCompetitionFixture(1, selectedClubName, rivals[0]),
            CreateCompetitionFixture(1, rivals[1], rivals[2]),
            CreateCompetitionFixture(2, selectedClubName, rivals[1]),
            CreateCompetitionFixture(2, rivals[0], rivals[2]),
            CreateCompetitionFixture(3, selectedClubName, rivals[2]),
            CreateCompetitionFixture(3, rivals[0], rivals[1]),
            CreateCompetitionFixture(4, selectedClubName, rivals[0]),
            CreateCompetitionFixture(4, rivals[2], rivals[1]),
            CreateCompetitionFixture(5, selectedClubName, rivals[1]),
            CreateCompetitionFixture(5, rivals[2], rivals[0]),
            CreateCompetitionFixture(6, selectedClubName, rivals[2]),
            CreateCompetitionFixture(6, rivals[1], rivals[0])
        };
    }

    private static GameState.CompetitionFixture CreateCompetitionFixture(int matchday, string homeClubName, string awayClubName)
    {
        return new GameState.CompetitionFixture
        {
            Matchday = matchday,
            HomeClubName = homeClubName,
            AwayClubName = awayClubName,
            IsComplete = false,
            Scoreline = "vs",
            ResultSummary = $"{homeClubName} vs {awayClubName}"
        };
    }

    private static GameState.CompetitionFixture BuildCompletedFixture(GameState.CompetitionFixture fixture, int homeGoals, int awayGoals)
    {
        return new GameState.CompetitionFixture
        {
            Matchday = fixture.Matchday,
            HomeClubName = fixture.HomeClubName,
            AwayClubName = fixture.AwayClubName,
            IsComplete = true,
            Scoreline = $"{homeGoals} - {awayGoals}",
            ResultSummary = $"{fixture.HomeClubName} {homeGoals} - {awayGoals} {fixture.AwayClubName}"
        };
    }

    private static (int homeGoals, int awayGoals) BuildShadowFixtureResult(
        GameState.CompetitionFixture fixture,
        int worldSeed,
        int seasonStartYear)
    {
        var seed = Math.Abs(HashCode.Combine(worldSeed, seasonStartYear, fixture.Matchday, fixture.HomeClubName, fixture.AwayClubName));
        var rng = new Random(seed);
        var homeGoals = rng.Next(0, 3);
        var awayGoals = rng.Next(0, 3);

        if (homeGoals == 0 && awayGoals == 0 && fixture.Matchday % 2 == 0)
        {
            homeGoals = 1;
        }

        return (homeGoals, awayGoals);
    }

    private static GameState.CompetitionRow[] RecalculateCompetitionTable(
        string[] availableClubs,
        GameState.CompetitionFixture[] fixtures)
    {
        var rows = Array.ConvertAll(
            availableClubs,
            clubName => new GameState.CompetitionRow
            {
                ClubName = clubName,
                Played = 0,
                Won = 0,
                Drawn = 0,
                Lost = 0,
                GoalsFor = 0,
                GoalsAgainst = 0,
                Points = 0
            });

        foreach (var fixture in fixtures)
        {
            if (!fixture.IsComplete)
            {
                continue;
            }

            var goals = ParseScoreline(fixture.Scoreline);
            var homeIndex = FindCompetitionRowIndex(rows, fixture.HomeClubName);
            var awayIndex = FindCompetitionRowIndex(rows, fixture.AwayClubName);
            if (homeIndex < 0 || awayIndex < 0)
            {
                continue;
            }

            rows[homeIndex] = UpdateCompetitionRow(rows[homeIndex], goals.homeGoals, goals.awayGoals);
            rows[awayIndex] = UpdateCompetitionRow(rows[awayIndex], goals.awayGoals, goals.homeGoals);
        }

        Array.Sort(
            rows,
            (left, right) =>
            {
                var pointComparison = right.Points.CompareTo(left.Points);
                if (pointComparison != 0)
                {
                    return pointComparison;
                }

                var goalDifferenceComparison = right.GoalDifference.CompareTo(left.GoalDifference);
                if (goalDifferenceComparison != 0)
                {
                    return goalDifferenceComparison;
                }

                var goalsForComparison = right.GoalsFor.CompareTo(left.GoalsFor);
                if (goalsForComparison != 0)
                {
                    return goalsForComparison;
                }

                return string.Compare(left.ClubName, right.ClubName, StringComparison.Ordinal);
            });

        return rows;
    }

    private static (int homeGoals, int awayGoals) ParseScoreline(string scoreline)
    {
        var tokens = scoreline.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2 || !int.TryParse(tokens[0], out var homeGoals) || !int.TryParse(tokens[1], out var awayGoals))
        {
            return (0, 0);
        }

        return (homeGoals, awayGoals);
    }

    private static int FindCompetitionRowIndex(GameState.CompetitionRow[] rows, string clubName)
    {
        for (var index = 0; index < rows.Length; index++)
        {
            if (rows[index].ClubName == clubName)
            {
                return index;
            }
        }

        return -1;
    }

    private static GameState.CompetitionRow UpdateCompetitionRow(GameState.CompetitionRow row, int goalsFor, int goalsAgainst)
    {
        var win = goalsFor > goalsAgainst ? 1 : 0;
        var draw = goalsFor == goalsAgainst ? 1 : 0;
        var loss = goalsFor < goalsAgainst ? 1 : 0;

        return new GameState.CompetitionRow
        {
            ClubName = row.ClubName,
            Played = row.Played + 1,
            Won = row.Won + win,
            Drawn = row.Drawn + draw,
            Lost = row.Lost + loss,
            GoalsFor = row.GoalsFor + goalsFor,
            GoalsAgainst = row.GoalsAgainst + goalsAgainst,
            Points = row.Points + (win * 3) + draw
        };
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }
}
