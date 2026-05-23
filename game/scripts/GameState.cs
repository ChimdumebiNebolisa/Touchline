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
        public required string CauseSummary { get; init; }
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
    public MatchPlaybackResult? CurrentMatchResult { get; private set; }

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
        TeamMorale = selection.TeamMorale;
        FanSentiment = selection.FanSentiment;
        BoardConfidence = selection.BoardConfidence;
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
        SquadPlayers = advance.SquadPlayers;
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

    public MatchPlaybackResult PrepareCurrentMatchResult(bool forceNew = false)
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

    public string ValidateCurrentMatchPlaybackContract()
    {
        return MatchPlaybackContractValidator.Validate(PrepareCurrentMatchResult(true));
    }

    public string ValidateOpponentSquadSourcing()
    {
        var firstPlayback = PrepareCurrentMatchResult(true);
        var firstAwayNames = ExtractTeamNames(firstPlayback, CurrentOpponentName);
        var expectedSquad = GetClubSquad(CurrentOpponentName);

        if (firstAwayNames.Length != 11)
        {
            return $"Expected 11 away player states, found {firstAwayNames.Length}.";
        }

        if (ContainsOldHardcodedAwayName(firstAwayNames))
        {
            return "Opponent lineup still contains the old hardcoded away XI.";
        }

        if (!ContainsSeededOpponentName(firstAwayNames, expectedSquad))
        {
            return "Opponent lineup did not source names from the resolved club squad.";
        }

        var secondPlayback = PrepareCurrentMatchResult(true);
        var secondAwayNames = ExtractTeamNames(secondPlayback, CurrentOpponentName);
        if (!StringArraysMatch(firstAwayNames, secondAwayNames))
        {
            return "Opponent lineup is not stable for the same seed and opponent.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateMatchVariationContract()
    {
        var originalFormation = TacticalFormation;
        var originalPress = PressIntensity;
        var originalTempo = Tempo;
        var originalWidth = Width;
        var originalRisk = Risk;

        var baseline = PrepareCurrentMatchResult(true);
        var kindCount = CountDistinctActionKinds(baseline);
        if (kindCount < 7)
        {
            return $"Expected at least 7 action kinds, found {kindCount}.";
        }

        if (CountDistinctPassLanes(baseline) < 3)
        {
            return "Expected at least three distinct pass lanes across the match.";
        }

        UpdateTactics(originalFormation, 85, 78, 72, 76);
        var aggressive = PrepareCurrentMatchResult(true);
        UpdateTactics(originalFormation, 35, 42, 38, 35);
        var conservative = PrepareCurrentMatchResult(true);
        UpdateTactics(originalFormation, originalPress, originalTempo, originalWidth, originalRisk);
        CurrentMatchResult = null;

        if (BuildActionSignature(aggressive) == BuildActionSignature(conservative))
        {
            return "Different tactical inputs produced the same action signature.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePostMatchCauseContract()
    {
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);

        if (LastMatchReport == null)
        {
            return "Post-match report was not created.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.CauseSummary))
        {
            return "Post-match report is missing cause summary.";
        }

        if (!LastMatchReport.ConsequenceSummary.Contains("Cause:", StringComparison.Ordinal))
        {
            return "Post-match consequence summary does not include playback cause reasoning.";
        }

        if (LastMatchReport.CauseSummary.Contains("scoreline", StringComparison.OrdinalIgnoreCase) &&
            LastMatchReport.CauseSummary.Split(';', StringSplitOptions.RemoveEmptyEntries).Length < 2)
        {
            return "Post-match cause reasoning is still scoreline-only.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public void ApplyMatchResult(MatchPlaybackResult result)
    {
        CurrentMatchResult = result;
        var goalDifference = result.FinalHomeScore - result.FinalAwayScore;
        var previousPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var consequence = PostMatchConsequenceService.Evaluate(result, this);

        TeamMorale = Math.Clamp(TeamMorale + consequence.MoraleDelta, 0, 100);
        FanSentiment = Math.Clamp(FanSentiment + consequence.FanDelta, 0, 100);
        BoardConfidence = Math.Clamp(BoardConfidence + consequence.BoardDelta, 0, 100);
        SquadStatusSummary = BuildSquadStatusSummary();
        UpdateFormSummary(goalDifference);

        RecordCompetitionResults(result.FinalHomeScore, result.FinalAwayScore);
        RefreshFixtureContext();
        var currentPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var tableImpactSummary = BuildTableImpactSummary(previousPosition, currentPosition);

        LastMatchReport = new MatchReport
        {
            FixtureLabel = $"{result.HomeClubName} vs {result.AwayClubName}",
            Scoreline = $"{result.FinalHomeScore} - {result.FinalAwayScore}",
            ResultLabel = consequence.ResultLabel,
            ConsequenceSummary = consequence.ConsequenceSummary,
            TableImpactSummary = tableImpactSummary,
            TacticalSummary = result.TacticalSummary,
            PressureSummary = consequence.PressureSummary,
            CauseSummary = consequence.CauseSummary,
            KeyEvents = consequence.KeyEvents,
            MoraleDelta = consequence.MoraleDelta,
            FanDelta = consequence.FanDelta,
            BoardDelta = consequence.BoardDelta
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
                CauseSummary = string.IsNullOrWhiteSpace(data.LastMatchReport.CauseSummary)
                    ? "Cause detail unavailable for this saved report."
                    : data.LastMatchReport.CauseSummary,
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
        SelectedPlayerProfileName = data.SelectedPlayerProfileName;
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

    public ClubSquadPlayer[] GetClubSquad(string clubName)
    {
        if (!string.IsNullOrWhiteSpace(clubName) && clubName == SelectedClubName)
        {
            var selectedSquad = new ClubSquadPlayer[SquadPlayers.Length];
            for (var index = 0; index < SquadPlayers.Length; index++)
            {
                var player = SquadPlayers[index];
                selectedSquad[index] = new ClubSquadPlayer
                {
                    PlayerId = ClubSquadFactory.BuildPlayerId(clubName, player.Name, index),
                    ClubName = clubName,
                    Name = player.Name,
                    Position = player.Position,
                    Age = player.Age,
                    Form = player.Form,
                    Morale = player.Morale,
                    Fitness = player.Fitness,
                    IsStarting = player.IsStarting
                };
            }

            return selectedSquad;
        }

        return TouchlineWorldGenerator.Instance == null
            ? ClubSquadFactory.BuildFallbackSquad(clubName, WorldSeed)
            : TouchlineWorldGenerator.Instance.ResolveClubSquad(clubName, WorldSeed);
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

    private static string[] ExtractTeamNames(MatchPlaybackResult playback, string teamName)
    {
        if (playback.Timeline.Frames.Length == 0)
        {
            return Array.Empty<string>();
        }

        var names = new List<string>();
        foreach (var player in playback.Timeline.Frames[0].PlayerStates)
        {
            if (player.Team == teamName)
            {
                names.Add(player.Name);
            }
        }

        return names.ToArray();
    }

    private static bool ContainsOldHardcodedAwayName(string[] names)
    {
        var oldNames = new[]
        {
            "Roman Ivic",
            "Maksym Hale",
            "Victor Salcedo",
            "Pavel Drago",
            "Nico Barros",
            "Ilyas Cherif",
            "Samir Gashi",
            "Tom Bisset",
            "Leandro Pires",
            "Bruno Keita",
            "Yuri Markovic"
        };

        foreach (var name in names)
        {
            foreach (var oldName in oldNames)
            {
                if (name == oldName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool ContainsSeededOpponentName(string[] playbackNames, ClubSquadPlayer[] expectedSquad)
    {
        foreach (var playbackName in playbackNames)
        {
            foreach (var expectedPlayer in expectedSquad)
            {
                if (playbackName == expectedPlayer.Name)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool StringArraysMatch(string[] first, string[] second)
    {
        if (first.Length != second.Length)
        {
            return false;
        }

        for (var index = 0; index < first.Length; index++)
        {
            if (first[index] != second[index])
            {
                return false;
            }
        }

        return true;
    }

    private static int CountDistinctActionKinds(MatchPlaybackResult playback)
    {
        var kinds = new List<MatchActionKind>();
        foreach (var action in playback.Timeline.Actions)
        {
            if (!kinds.Contains(action.Kind))
            {
                kinds.Add(action.Kind);
            }
        }

        return kinds.Count;
    }

    private static int CountDistinctPassLanes(MatchPlaybackResult playback)
    {
        var lanes = new List<int>();
        foreach (var action in playback.Timeline.Actions)
        {
            if (action.Kind != MatchActionKind.Pass)
            {
                continue;
            }

            var lane = (int)MathF.Round(action.ToPosition.Y * 10.0f);
            if (!lanes.Contains(lane))
            {
                lanes.Add(lane);
            }
        }

        return lanes.Count;
    }

    private static string BuildActionSignature(MatchPlaybackResult playback)
    {
        var segments = new List<string>();
        var limit = Math.Min(18, playback.Timeline.Actions.Length);
        for (var index = 0; index < limit; index++)
        {
            var action = playback.Timeline.Actions[index];
            segments.Add($"{action.Kind}:{action.Team}:{action.ToPosition.X:0.00}:{action.ToPosition.Y:0.00}");
        }

        return string.Join("|", segments);
    }

    private static string[] ExtractRecentEvents(MatchPlaybackResult result)
    {
        var count = Math.Min(4, result.EventFeed.Length);
        var recentEvents = new string[count];

        for (var index = 0; index < count; index++)
        {
            recentEvents[index] = result.EventFeed[result.EventFeed.Length - count + index].Summary;
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
