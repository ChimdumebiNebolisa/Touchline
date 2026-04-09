using Godot;
using System;
using System.Collections.Generic;

public partial class GameState : Node
{
    private const int MatchesPerSeason = 6;
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
        public required string[] KeyEvents { get; init; }
        public required int MoraleDelta { get; init; }
        public required int FanDelta { get; init; }
        public required int BoardDelta { get; init; }
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
        ManagerName = managerName;
        CareerSeed = seed;
        CareerInitialized = true;
        WorldSeed = seed;
        CurrentDate = new DateTime(2026, 8, 3);
        SeasonStartYear = 2026;
        AvailableClubs = new[]
        {
            "Riverton Athletic",
            "Northbridge City",
            "Harbor County",
            "Eastvale Rovers"
        };
        SelectedClubName = null;
        NextFixtureSummary = "Matchday 1: Riverton Athletic vs Harbor County";
        SquadStatusSummary = BuildSquadStatusSummary();
        SquadPlayers = Array.Empty<SquadPlayer>();
        TacticalFormation = "4-3-3";
        PressIntensity = 60;
        Tempo = 58;
        Width = 55;
        Risk = 52;
        CompetitionName = "Novara Premier Division";
        CurrentMatchday = 1;
        CurrentOpponentName = "Harbor County";
        TeamMorale = 72;
        FanSentiment = 63;
        BoardConfidence = 61;
        FormSummary = "Form: season about to begin.";
        _recentResults.Clear();
        LastMatchReport = null;
    }

    public void SelectClub(string clubName)
    {
        SelectedClubName = clubName;
        CompetitionName = "Novara Premier Division";
        CurrentMatchday = 1;
        LastMatchReport = null;
        RefreshFixtureContext();
        SquadStatusSummary = BuildSquadStatusSummary();

        SquadPlayers = new[]
        {
            new SquadPlayer { Name = "Mateo Silva", Position = "GK", Age = 27, Form = 72, Morale = 73, Fitness = 91, IsStarting = true },
            new SquadPlayer { Name = "Liam Ofori", Position = "RB", Age = 24, Form = 68, Morale = 69, Fitness = 88, IsStarting = true },
            new SquadPlayer { Name = "Ethan Novak", Position = "CB", Age = 29, Form = 74, Morale = 76, Fitness = 86, IsStarting = true },
            new SquadPlayer { Name = "Jonas Petrov", Position = "CB", Age = 22, Form = 71, Morale = 71, Fitness = 90, IsStarting = true },
            new SquadPlayer { Name = "Kai Mendes", Position = "LB", Age = 25, Form = 67, Morale = 68, Fitness = 87, IsStarting = true },
            new SquadPlayer { Name = "Noah Ibe", Position = "CM", Age = 23, Form = 75, Morale = 74, Fitness = 89, IsStarting = true },
            new SquadPlayer { Name = "Adrian Voss", Position = "CM", Age = 30, Form = 69, Morale = 72, Fitness = 82, IsStarting = true },
            new SquadPlayer { Name = "Rafael Costa", Position = "AM", Age = 21, Form = 80, Morale = 79, Fitness = 92, IsStarting = true },
            new SquadPlayer { Name = "Tariq Balde", Position = "RW", Age = 26, Form = 76, Morale = 77, Fitness = 90, IsStarting = true },
            new SquadPlayer { Name = "Milo Renard", Position = "ST", Age = 28, Form = 73, Morale = 75, Fitness = 85, IsStarting = true },
            new SquadPlayer { Name = "Kenji Sato", Position = "LW", Age = 24, Form = 70, Morale = 70, Fitness = 88, IsStarting = true },
            new SquadPlayer { Name = "Dario Klein", Position = "GK", Age = 20, Form = 64, Morale = 66, Fitness = 84, IsStarting = false },
            new SquadPlayer { Name = "Felix Mensah", Position = "CB", Age = 25, Form = 65, Morale = 67, Fitness = 83, IsStarting = false },
            new SquadPlayer { Name = "Omar Nadir", Position = "CM", Age = 19, Form = 70, Morale = 72, Fitness = 86, IsStarting = false },
            new SquadPlayer { Name = "Lucas Marin", Position = "ST", Age = 23, Form = 68, Morale = 69, Fitness = 87, IsStarting = false }
        };
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
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return;
        }

        CurrentDate = CurrentDate.AddDays(7);
        CurrentMatchday++;

        if (CurrentMatchday > MatchesPerSeason)
        {
            SeasonStartYear++;
            CurrentDate = new DateTime(SeasonStartYear, 8, 3);
            CurrentMatchday = 1;
            _recentResults.Clear();
            FormSummary = "Form: new season reset.";
        }

        LastMatchReport = null;
        RefreshFixtureContext();
    }

    public void CompleteLiveMatch(LiveMatchPlayback playback)
    {
        var finalEvent = playback.Events[^1];
        var goalDifference = finalEvent.HomeScore - finalEvent.AwayScore;
        var moraleDelta = goalDifference > 0 ? 4 : goalDifference == 0 ? 1 : -4;
        var fanDelta = goalDifference > 0 ? 5 : goalDifference == 0 ? 0 : -5;
        var boardDelta = goalDifference > 0 ? 3 : goalDifference == 0 ? -1 : -4;

        TeamMorale = Math.Clamp(TeamMorale + moraleDelta, 0, 100);
        FanSentiment = Math.Clamp(FanSentiment + fanDelta, 0, 100);
        BoardConfidence = Math.Clamp(BoardConfidence + boardDelta, 0, 100);
        SquadStatusSummary = BuildSquadStatusSummary();
        UpdateFormSummary(goalDifference);

        LastMatchReport = new MatchReport
        {
            FixtureLabel = $"{playback.HomeClubName} vs {playback.AwayClubName}",
            Scoreline = $"{finalEvent.HomeScore} - {finalEvent.AwayScore}",
            ResultLabel = BuildResultLabel(goalDifference, playback.AwayClubName),
            ConsequenceSummary =
                $"Morale {FormatSignedDelta(moraleDelta)} | Fans {FormatSignedDelta(fanDelta)} | Board {FormatSignedDelta(boardDelta)}",
            KeyEvents = ExtractRecentEvents(playback),
            MoraleDelta = moraleDelta,
            FanDelta = fanDelta,
            BoardDelta = boardDelta
        };
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
                KeyEvents = data.LastMatchReport.KeyEvents ?? Array.Empty<string>(),
                MoraleDelta = data.LastMatchReport.MoraleDelta,
                FanDelta = data.LastMatchReport.FanDelta,
                BoardDelta = data.LastMatchReport.BoardDelta
            };
    }

    private string BuildSquadStatusSummary()
    {
        return $"23 registered players | morale {DescribeLevel(TeamMorale)} | fans {DescribeLevel(FanSentiment)} | board {DescribeLevel(BoardConfidence)}";
    }

    private void RefreshFixtureContext()
    {
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            CurrentOpponentName = "Harbor County";
            NextFixtureSummary = "Fixture context unavailable.";
            return;
        }

        var opponents = Array.FindAll(AvailableClubs, candidate => candidate != SelectedClubName);
        CurrentOpponentName = opponents.Length == 0
            ? "Harbor County"
            : opponents[(CurrentMatchday - 1) % opponents.Length];

        NextFixtureSummary =
            $"{CurrentDateLabel} | Matchday {CurrentMatchday} | {SelectedClubName} vs {CurrentOpponentName}";
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

    private static string[] ExtractRecentEvents(LiveMatchPlayback playback)
    {
        var count = Math.Min(4, playback.Events.Length);
        var recentEvents = new string[count];

        for (var index = 0; index < count; index++)
        {
            recentEvents[index] = playback.Events[playback.Events.Length - count + index].Summary;
        }

        return recentEvents;
    }
}
