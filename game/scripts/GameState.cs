using Godot;
using System;
using System.Collections.Generic;

public partial class GameState : Node
{
    private readonly List<string> _recentResults = new();

    public sealed class SquadPlayer
    {
        public required string Name { get; init; }
        public required string Position { get; init; }
        public required int Age { get; init; }
        public required int Form { get; init; }
        public required int Morale { get; init; }
        public required int Fitness { get; init; }
        public required bool IsStarting { get; init; }
    }

    public sealed class MatchReport
    {
        public required string FixtureLabel { get; init; }
        public required string Scoreline { get; init; }
        public required string ResultLabel { get; init; }
        public required string ConsequenceSummary { get; init; }
        public required string TableImpactSummary { get; init; }
        public required string TacticalSummary { get; init; }
        public required string PressureSummary { get; init; }
        public required string[] KeyEvents { get; init; }
        public required int MoraleDelta { get; init; }
        public required int FanDelta { get; init; }
        public required int BoardDelta { get; init; }
    }

    public sealed class ClubPreview
    {
        public required string ClubName { get; init; }
        public required string IdentitySummary { get; init; }
        public required string ExpectationSummary { get; init; }
        public required string OpeningFixtureSummary { get; init; }
    }

    public sealed class CompetitionRow
    {
        public required string ClubName { get; init; }
        public required int Played { get; init; }
        public required int Won { get; init; }
        public required int Drawn { get; init; }
        public required int Lost { get; init; }
        public required int GoalsFor { get; init; }
        public required int GoalsAgainst { get; init; }
        public int GoalDifference => GoalsFor - GoalsAgainst;
        public required int Points { get; init; }
    }

    public sealed class CompetitionFixture
    {
        public required int Matchday { get; init; }
        public required string HomeClubName { get; init; }
        public required string AwayClubName { get; init; }
        public required bool IsComplete { get; init; }
        public required string Scoreline { get; init; }
        public required string ResultSummary { get; init; }
    }

    public static GameState? Instance { get; private set; }

    public string ManagerName { get; private set; } = "Manager";
    public int CareerSeed { get; private set; }
    public bool CareerInitialized { get; private set; }
    public int WorldSeed { get; private set; }
    public string CountryPackId { get; private set; } = "country-pack-alpha";
    public string[] AvailableClubs { get; private set; } = Array.Empty<string>();
    public string? SelectedClubName { get; private set; }
    public string NextFixtureSummary { get; private set; } = "Fixture context unavailable.";
    public string SquadStatusSummary { get; private set; } = "Squad status unavailable.";
    public SquadPlayer[] SquadPlayers { get; private set; } = Array.Empty<SquadPlayer>();
    public string TacticalFormation { get; private set; } = "4-3-3";
    public int PressIntensity { get; private set; } = 60;
    public int Tempo { get; private set; } = 58;
    public int Width { get; private set; } = 55;
    public int Risk { get; private set; } = 52;
    public string CompetitionName { get; private set; } = "Novara Premier Division";
    public int CurrentMatchday { get; private set; } = 1;
    public string CurrentOpponentName { get; private set; } = "Harbor County";
    public int TeamMorale { get; private set; } = 72;
    public int FanSentiment { get; private set; } = 63;
    public int BoardConfidence { get; private set; } = 61;
    public DateTime CurrentDate { get; private set; } = new(2026, 8, 3);
    public int SeasonStartYear { get; private set; } = 2026;
    public string FormSummary { get; private set; } = "Form: season about to begin.";
    public MatchReport? LastMatchReport { get; private set; }
    public string[] RecentResults => _recentResults.ToArray();
    public string SeasonLabel => $"{SeasonStartYear}/{((SeasonStartYear + 1) % 100):00}";
    public string CurrentDateLabel => CurrentDate.ToString("ddd d MMM yyyy");
    public string? SelectedPlayerProfileName { get; private set; }
    public CompetitionRow[] CompetitionTable { get; private set; } = Array.Empty<CompetitionRow>();
    public CompetitionFixture[] CompetitionFixtures { get; private set; } = Array.Empty<CompetitionFixture>();
    public MatchSimulationResult? CurrentMatchResult { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _ExitTree()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void StartNewCareer(string managerName, int seed)
    {
        TouchlineWorldGenerator.Instance?.BeginNewCareer(managerName, seed);
    }

    public void ApplyCareerBootstrap(CareerBootstrapState bootstrap)
    {
        ManagerName = bootstrap.ManagerName;
        CareerSeed = bootstrap.CareerSeed;
        CareerInitialized = true;
        WorldSeed = bootstrap.WorldSeed;
        CountryPackId = bootstrap.CountryPackId;
        AvailableClubs = bootstrap.AvailableClubs;
        SelectedClubName = null;
        NextFixtureSummary = "Select a club to view the opening fixture.";
        SquadPlayers = Array.Empty<SquadPlayer>();
        TacticalFormation = bootstrap.TacticalFormation;
        PressIntensity = bootstrap.PressIntensity;
        Tempo = bootstrap.Tempo;
        Width = bootstrap.Width;
        Risk = bootstrap.Risk;
        CompetitionName = bootstrap.CompetitionName;
        CurrentMatchday = 1;
        CurrentOpponentName = "Opponent unavailable";
        TeamMorale = bootstrap.TeamMorale;
        FanSentiment = bootstrap.FanSentiment;
        BoardConfidence = bootstrap.BoardConfidence;
        CurrentDate = bootstrap.CurrentDate;
        SeasonStartYear = bootstrap.SeasonStartYear;
        FormSummary = bootstrap.FormSummary;
        _recentResults.Clear();
        LastMatchReport = null;
        SelectedPlayerProfileName = null;
        CompetitionTable = Array.Empty<CompetitionRow>();
        CompetitionFixtures = Array.Empty<CompetitionFixture>();
        CurrentMatchResult = null;
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    public void SelectClub(string clubName)
    {
        TouchlineWorldGenerator.Instance?.SelectClub(clubName);
    }

    public void ApplyClubSelection(ClubSelectionState selection)
    {
        SelectedClubName = selection.ClubName;
        CompetitionName = selection.CompetitionName;
        CurrentMatchday = selection.CurrentMatchday;
        LastMatchReport = null;
        SelectedPlayerProfileName = null;
        CurrentMatchResult = null;
        SquadPlayers = selection.SquadPlayers;
        CompetitionTable = selection.CompetitionTable;
        CompetitionFixtures = selection.CompetitionFixtures;
        CurrentOpponentName = selection.CurrentOpponentName;
        NextFixtureSummary = selection.NextFixtureSummary;
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    public void UpdateTactics(string formation, int pressIntensity, int tempo, int width, int risk)
    {
        TacticalFormation = formation;
        PressIntensity = pressIntensity;
        Tempo = tempo;
        Width = width;
        Risk = risk;
    }

    public void AdvanceDate()
    {
        TouchlineCalendarSystem.Instance?.AdvanceCareerDate();
    }

    public void ApplyCalendarAdvance(CalendarAdvanceState advance)
    {
        CurrentDate = advance.CurrentDate;
        SeasonStartYear = advance.SeasonStartYear;
        CurrentMatchday = advance.CurrentMatchday;
        FormSummary = advance.FormSummary;
        CompetitionTable = advance.CompetitionTable;
        CompetitionFixtures = advance.CompetitionFixtures;
        CurrentOpponentName = advance.CurrentOpponentName;
        NextFixtureSummary = advance.NextFixtureSummary;

        if (advance.ResetRecentResults)
        {
            _recentResults.Clear();
        }

        LastMatchReport = null;
        CurrentMatchResult = null;
    }

    public MatchSimulationResult PrepareCurrentMatchResult(bool forceNew = false)
    {
        if (!forceNew && CurrentMatchResult != null)
        {
            return CurrentMatchResult;
        }

        CurrentMatchResult = MatchSimulator.Simulate(this);
        return CurrentMatchResult;
    }

    public void ResolveCurrentMatchInstantly()
    {
        var result = PrepareCurrentMatchResult();
        ApplyMatchResult(result);
    }

    public void ApplyMatchResult(MatchSimulationResult result)
    {
        CurrentMatchResult = result;
        var finalEvent = result.Events[^1];
        var goalDifference = finalEvent.HomeScore - finalEvent.AwayScore;
        var previousPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var moraleDelta = goalDifference > 0 ? 4 : goalDifference == 0 ? 1 : -4;
        var fanDelta = goalDifference > 0 ? 5 : goalDifference == 0 ? 0 : -5;
        var boardDelta = goalDifference > 0 ? 3 : goalDifference == 0 ? -1 : -4;

        TeamMorale = Math.Clamp(TeamMorale + moraleDelta, 0, 100);
        FanSentiment = Math.Clamp(FanSentiment + fanDelta, 0, 100);
        BoardConfidence = Math.Clamp(BoardConfidence + boardDelta, 0, 100);
        SquadStatusSummary = BuildSquadStatusSummary();
        UpdateFormSummary(goalDifference);

        RecordCompetitionResults(finalEvent.HomeScore, finalEvent.AwayScore);
        RefreshFixtureContext();
        var currentPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var tableImpactSummary = BuildTableImpactSummary(previousPosition, currentPosition);
        var pressureSummary =
            $"Club pressure now sits at morale {TeamMorale}, fan trust {FanSentiment}, and board confidence {BoardConfidence}.";

        LastMatchReport = new MatchReport
        {
            FixtureLabel = $"{result.HomeClubName} vs {result.AwayClubName}",
            Scoreline = $"{finalEvent.HomeScore} - {finalEvent.AwayScore}",
            ResultLabel = BuildResultLabel(goalDifference, result.AwayClubName),
            ConsequenceSummary =
                $"Morale {FormatSignedDelta(moraleDelta)} | Fans {FormatSignedDelta(fanDelta)} | Board {FormatSignedDelta(boardDelta)}",
            TableImpactSummary = tableImpactSummary,
            TacticalSummary = result.TacticalSummary,
            PressureSummary = pressureSummary,
            KeyEvents = ExtractRecentEvents(result),
            MoraleDelta = moraleDelta,
            FanDelta = fanDelta,
            BoardDelta = boardDelta
        };
        CurrentMatchResult = null;
    }

    public void RestoreFromSave(SaveSlotData data)
    {
        ManagerName = data.ManagerName;
        CareerSeed = data.CareerSeed;
        CareerInitialized = data.CareerInitialized;
        WorldSeed = data.WorldSeed;
        CountryPackId = data.CountryPackId;
        AvailableClubs = data.AvailableClubs ?? Array.Empty<string>();
        SelectedClubName = data.SelectedClubName;
        NextFixtureSummary = data.NextFixtureSummary;
        SquadStatusSummary = data.SquadStatusSummary;
        TacticalFormation = data.TacticalFormation;
        PressIntensity = data.PressIntensity;
        Tempo = data.Tempo;
        Width = data.Width;
        Risk = data.Risk;
        CompetitionName = data.CompetitionName;
        CurrentMatchday = data.CurrentMatchday;
        CurrentOpponentName = data.CurrentOpponentName;
        TeamMorale = data.TeamMorale;
        FanSentiment = data.FanSentiment;
        BoardConfidence = data.BoardConfidence;
        CurrentDate = DateTime.Parse(data.CurrentDateIso);
        SeasonStartYear = data.SeasonStartYear;
        FormSummary = data.FormSummary;

        _recentResults.Clear();
        if (data.RecentResults != null)
        {
            _recentResults.AddRange(data.RecentResults);
        }

        SquadPlayers = Array.ConvertAll(
            data.SquadPlayers ?? Array.Empty<SaveSlotPlayerData>(),
            player => new SquadPlayer
            {
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Form = player.Form,
                Morale = player.Morale,
                Fitness = player.Fitness,
                IsStarting = player.IsStarting
            });

        LastMatchReport = data.LastMatchReport == null
            ? null
            : new MatchReport
            {
                FixtureLabel = data.LastMatchReport.FixtureLabel,
                Scoreline = data.LastMatchReport.Scoreline,
                ResultLabel = data.LastMatchReport.ResultLabel,
                ConsequenceSummary = data.LastMatchReport.ConsequenceSummary,
                TableImpactSummary = data.LastMatchReport.TableImpactSummary,
                TacticalSummary = data.LastMatchReport.TacticalSummary,
                PressureSummary = data.LastMatchReport.PressureSummary,
                KeyEvents = data.LastMatchReport.KeyEvents ?? Array.Empty<string>(),
                MoraleDelta = data.LastMatchReport.MoraleDelta,
                FanDelta = data.LastMatchReport.FanDelta,
                BoardDelta = data.LastMatchReport.BoardDelta
            };
        CompetitionTable = Array.ConvertAll(
            data.CompetitionTable ?? Array.Empty<SaveSlotCompetitionRowData>(),
            row => new CompetitionRow
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
        CompetitionFixtures = Array.ConvertAll(
            data.CompetitionFixtures ?? Array.Empty<SaveSlotCompetitionFixtureData>(),
            fixture => new CompetitionFixture
            {
                Matchday = fixture.Matchday,
                HomeClubName = fixture.HomeClubName,
                AwayClubName = fixture.AwayClubName,
                IsComplete = fixture.IsComplete,
                Scoreline = fixture.Scoreline,
                ResultSummary = fixture.ResultSummary
            });
        SelectedPlayerProfileName = null;
        CurrentMatchResult = null;
        RefreshFixtureContext();
    }

    public void SelectPlayerProfile(string playerName)
    {
        SelectedPlayerProfileName = playerName;
    }

    public SquadPlayer? GetSelectedPlayerProfile()
    {
        if (string.IsNullOrWhiteSpace(SelectedPlayerProfileName))
        {
            return null;
        }

        foreach (var player in SquadPlayers)
        {
            if (player.Name == SelectedPlayerProfileName)
            {
                return player;
            }
        }

        return null;
    }

    public string TogglePlayerLineupStatus(string playerName)
    {
        var targetIndex = Array.FindIndex(SquadPlayers, player => player.Name == playerName);
        if (targetIndex < 0)
        {
            return "Selected player is unavailable for lineup changes.";
        }

        var targetPlayer = SquadPlayers[targetIndex];
        var startingCount = CountStartingPlayers();

        if (targetPlayer.IsStarting)
        {
            var replacementIndex = FindReplacementBenchIndex(targetPlayer.Position);
            if (replacementIndex < 0)
            {
                return "No bench player is available to keep the XI balanced.";
            }

            var replacementPlayer = SquadPlayers[replacementIndex];
            SetPlayerStartingStatus(targetIndex, false);
            SetPlayerStartingStatus(replacementIndex, true);
            return $"{targetPlayer.Name} moves to the bench. {replacementPlayer.Name} steps into the XI.";
        }

        if (startingCount < 11)
        {
            SetPlayerStartingStatus(targetIndex, true);
            return $"{targetPlayer.Name} is promoted into the XI.";
        }

        var playerToBenchIndex = FindStarterToBenchIndex(targetPlayer.Position, targetIndex);
        if (playerToBenchIndex < 0)
        {
            return "A balanced swap could not be found for this lineup move.";
        }

        var benchPlayer = SquadPlayers[playerToBenchIndex];
        SetPlayerStartingStatus(playerToBenchIndex, false);
        SetPlayerStartingStatus(targetIndex, true);
        return $"{targetPlayer.Name} enters the XI for {benchPlayer.Name}.";
    }

    private string BuildSquadStatusSummary()
    {
        return $"23 registered players | morale {DescribeLevel(TeamMorale)} | fans {DescribeLevel(FanSentiment)} | board {DescribeLevel(BoardConfidence)}";
    }

    private void RecordCompetitionResults(int homeGoals, int awayGoals)
    {
        var competitionState = CompetitionRuntimeService.ApplyMatchdayResult(
            AvailableClubs,
            CompetitionFixtures,
            CurrentMatchday,
            SelectedClubName,
            homeGoals,
            awayGoals,
            WorldSeed,
            SeasonStartYear);
        CompetitionTable = competitionState.table;
        CompetitionFixtures = competitionState.fixtures;
    }

    private int CountStartingPlayers()
    {
        var count = 0;
        foreach (var player in SquadPlayers)
        {
            if (player.IsStarting)
            {
                count++;
            }
        }

        return count;
    }

    private void SetPlayerStartingStatus(int index, bool isStarting)
    {
        var player = SquadPlayers[index];
        SquadPlayers[index] = new SquadPlayer
        {
            Name = player.Name,
            Position = player.Position,
            Age = player.Age,
            Form = player.Form,
            Morale = player.Morale,
            Fitness = player.Fitness,
            IsStarting = isStarting
        };
    }

    private int FindReplacementBenchIndex(string position)
    {
        var preferredFamily = GetPositionFamily(position);
        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (!SquadPlayers[index].IsStarting && GetPositionFamily(SquadPlayers[index].Position) == preferredFamily)
            {
                return index;
            }
        }

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (!SquadPlayers[index].IsStarting)
            {
                return index;
            }
        }

        return -1;
    }

    private int FindStarterToBenchIndex(string position, int excludedIndex)
    {
        var preferredFamily = GetPositionFamily(position);
        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (index != excludedIndex && SquadPlayers[index].IsStarting && GetPositionFamily(SquadPlayers[index].Position) == preferredFamily)
            {
                return index;
            }
        }

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (index != excludedIndex && SquadPlayers[index].IsStarting)
            {
                return index;
            }
        }

        return -1;
    }

    private static string GetPositionFamily(string position)
    {
        return position switch
        {
            "GK" => "GK",
            "RB" or "CB" or "LB" => "DEF",
            "CM" or "AM" => "MID",
            _ => "ATT"
        };
    }

    private void RefreshFixtureContext()
    {
        var fixtureContext = CompetitionRuntimeService.ResolveFixtureContext(
            CompetitionFixtures,
            CurrentMatchday,
            SelectedClubName,
            CurrentDateLabel);
        CurrentOpponentName = fixtureContext.currentOpponentName;
        NextFixtureSummary = fixtureContext.nextFixtureSummary;
    }

    private void UpdateFormSummary(int goalDifference)
    {
        var resultToken = goalDifference switch
        {
            > 0 => "W",
            0 => "D",
            _ => "L"
        };

        _recentResults.Insert(0, resultToken);
        if (_recentResults.Count > 5)
        {
            _recentResults.RemoveAt(5);
        }

        FormSummary = $"Form: {string.Join(" ", _recentResults)}";
    }

    private static string DescribeLevel(int value)
    {
        return value switch
        {
            >= 75 => "surging",
            >= 60 => "steady",
            >= 45 => "uneasy",
            _ => "under pressure"
        };
    }

    private static string BuildResultLabel(int goalDifference, string opponentName)
    {
        return goalDifference switch
        {
            > 0 => $"Winning over {opponentName} lifts the mood around the club.",
            0 => $"The draw with {opponentName} leaves the dressing room asking for more control.",
            _ => $"{opponentName} leave with the points and the pressure tightens."
        };
    }

    private static string FormatSignedDelta(int delta)
    {
        return delta >= 0 ? $"+{delta}" : delta.ToString();
    }

    private static string[] ExtractRecentEvents(MatchSimulationResult result)
    {
        var count = Math.Min(4, result.Events.Length);
        var recentEvents = new string[count];

        for (var index = 0; index < count; index++)
        {
            recentEvents[index] = result.Events[result.Events.Length - count + index].Summary;
        }

        return recentEvents;
    }

    private int GetClubTablePosition(string clubName)
    {
        return CompetitionRuntimeService.GetClubTablePosition(CompetitionTable, clubName);
    }

    private string BuildTableImpactSummary(int previousPosition, int currentPosition)
    {
        return CompetitionRuntimeService.BuildTableImpactSummary(
            CompetitionTable,
            SelectedClubName,
            previousPosition,
            currentPosition);
    }

    private CompetitionFixture? GetCurrentClubFixture()
    {
        return CompetitionRuntimeService.GetCurrentClubFixture(CompetitionFixtures, CurrentMatchday, SelectedClubName);
    }

    private CompetitionRow? GetCompetitionRow(string clubName)
    {
        return CompetitionRuntimeService.GetCompetitionRow(CompetitionTable, clubName);
    }
}
