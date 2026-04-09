using Godot;
using System;
using System.Text.Json;

public sealed class SaveSlotData
{
    public string ManagerName { get; set; } = "Manager";
    public int CareerSeed { get; set; }
    public bool CareerInitialized { get; set; }
    public int WorldSeed { get; set; }
    public string CountryPackId { get; set; } = "country-pack-alpha";
    public string[]? AvailableClubs { get; set; }
    public string? SelectedClubName { get; set; }
    public string NextFixtureSummary { get; set; } = "Fixture context unavailable.";
    public string SquadStatusSummary { get; set; } = "Squad status unavailable.";
    public SaveSlotPlayerData[]? SquadPlayers { get; set; }
    public string TacticalFormation { get; set; } = "4-3-3";
    public int PressIntensity { get; set; }
    public int Tempo { get; set; }
    public int Width { get; set; }
    public int Risk { get; set; }
    public string CompetitionName { get; set; } = "Novara Premier Division";
    public int CurrentMatchday { get; set; }
    public string CurrentOpponentName { get; set; } = "Harbor County";
    public int TeamMorale { get; set; }
    public int FanSentiment { get; set; }
    public int BoardConfidence { get; set; }
    public string CurrentDateIso { get; set; } = "2026-08-03";
    public int SeasonStartYear { get; set; }
    public string FormSummary { get; set; } = "Form: season about to begin.";
    public string[]? RecentResults { get; set; }
    public SaveSlotMatchReportData? LastMatchReport { get; set; }
    public SaveSlotCompetitionRowData[]? CompetitionTable { get; set; }
    public SaveSlotCompetitionFixtureData[]? CompetitionFixtures { get; set; }
}

public sealed class SaveSlotPlayerData
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Form { get; set; }
    public int Morale { get; set; }
    public int Fitness { get; set; }
    public bool IsStarting { get; set; }
}

public sealed class SaveSlotMatchReportData
{
    public string FixtureLabel { get; set; } = string.Empty;
    public string Scoreline { get; set; } = "0 - 0";
    public string ResultLabel { get; set; } = string.Empty;
    public string ConsequenceSummary { get; set; } = string.Empty;
    public string[]? KeyEvents { get; set; }
    public int MoraleDelta { get; set; }
    public int FanDelta { get; set; }
    public int BoardDelta { get; set; }
}

public sealed class SaveSlotCompetitionRowData
{
    public string ClubName { get; set; } = string.Empty;
    public int Played { get; set; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int Points { get; set; }
}

public sealed class SaveSlotCompetitionFixtureData
{
    public int Matchday { get; set; }
    public string HomeClubName { get; set; } = string.Empty;
    public string AwayClubName { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public string Scoreline { get; set; } = "vs";
    public string ResultSummary { get; set; } = string.Empty;
}

public partial class SaveSystem : Node
{
    private const string SaveSlotPath = "user://slot-1.json";
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static SaveSystem? Instance { get; private set; }

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

    public bool HasSaveFile()
    {
        return FileAccess.FileExists(SaveSlotPath);
    }

    public string GetSlotSummary()
    {
        if (!TryReadSave(out var saveData, out var errorMessage))
        {
            return errorMessage;
        }

        return $"Slot 1 | {saveData.SelectedClubName} | Season {saveData.SeasonStartYear}/{((saveData.SeasonStartYear + 1) % 100):00} | {saveData.CurrentDateIso}";
    }

    public bool SaveGame(out string statusMessage)
    {
        if (GameState.Instance == null || !GameState.Instance.CareerInitialized || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            statusMessage = "No active career is ready to save.";
            return false;
        }

        try
        {
            var payload = new SaveSlotData
            {
                ManagerName = GameState.Instance.ManagerName,
                CareerSeed = GameState.Instance.CareerSeed,
                CareerInitialized = GameState.Instance.CareerInitialized,
                WorldSeed = GameState.Instance.WorldSeed,
                CountryPackId = GameState.Instance.CountryPackId,
                AvailableClubs = GameState.Instance.AvailableClubs,
                SelectedClubName = GameState.Instance.SelectedClubName,
                NextFixtureSummary = GameState.Instance.NextFixtureSummary,
                SquadStatusSummary = GameState.Instance.SquadStatusSummary,
                SquadPlayers = Array.ConvertAll(
                    GameState.Instance.SquadPlayers,
                    player => new SaveSlotPlayerData
                    {
                        Name = player.Name,
                        Position = player.Position,
                        Age = player.Age,
                        Form = player.Form,
                        Morale = player.Morale,
                        Fitness = player.Fitness,
                        IsStarting = player.IsStarting
                    }),
                TacticalFormation = GameState.Instance.TacticalFormation,
                PressIntensity = GameState.Instance.PressIntensity,
                Tempo = GameState.Instance.Tempo,
                Width = GameState.Instance.Width,
                Risk = GameState.Instance.Risk,
                CompetitionName = GameState.Instance.CompetitionName,
                CurrentMatchday = GameState.Instance.CurrentMatchday,
                CurrentOpponentName = GameState.Instance.CurrentOpponentName,
                TeamMorale = GameState.Instance.TeamMorale,
                FanSentiment = GameState.Instance.FanSentiment,
                BoardConfidence = GameState.Instance.BoardConfidence,
                CurrentDateIso = GameState.Instance.CurrentDate.ToString("yyyy-MM-dd"),
                SeasonStartYear = GameState.Instance.SeasonStartYear,
                FormSummary = GameState.Instance.FormSummary,
                RecentResults = GameState.Instance.RecentResults,
                LastMatchReport = GameState.Instance.LastMatchReport == null
                    ? null
                    : new SaveSlotMatchReportData
                    {
                        FixtureLabel = GameState.Instance.LastMatchReport.FixtureLabel,
                        Scoreline = GameState.Instance.LastMatchReport.Scoreline,
                        ResultLabel = GameState.Instance.LastMatchReport.ResultLabel,
                        ConsequenceSummary = GameState.Instance.LastMatchReport.ConsequenceSummary,
                        KeyEvents = GameState.Instance.LastMatchReport.KeyEvents,
                        MoraleDelta = GameState.Instance.LastMatchReport.MoraleDelta,
                        FanDelta = GameState.Instance.LastMatchReport.FanDelta,
                        BoardDelta = GameState.Instance.LastMatchReport.BoardDelta
                    },
                CompetitionTable = Array.ConvertAll(
                    GameState.Instance.CompetitionTable,
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
                    }),
                CompetitionFixtures = Array.ConvertAll(
                    GameState.Instance.CompetitionFixtures,
                    fixture => new SaveSlotCompetitionFixtureData
                    {
                        Matchday = fixture.Matchday,
                        HomeClubName = fixture.HomeClubName,
                        AwayClubName = fixture.AwayClubName,
                        IsComplete = fixture.IsComplete,
                        Scoreline = fixture.Scoreline,
                        ResultSummary = fixture.ResultSummary
                    })
            };

            using var saveFile = FileAccess.Open(SaveSlotPath, FileAccess.ModeFlags.Write);
            saveFile.StoreString(JsonSerializer.Serialize(payload, JsonOptions));
            statusMessage = $"Career saved to Slot 1 for {payload.SelectedClubName}.";
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Save failed: {ex.Message}";
            return false;
        }
    }

    public bool LoadGame(out string statusMessage)
    {
        if (GameState.Instance == null)
        {
            statusMessage = "Game state singleton unavailable.";
            return false;
        }

        if (!TryReadSave(out var saveData, out statusMessage))
        {
            return false;
        }

        GameState.Instance.RestoreFromSave(saveData);
        statusMessage = $"Loaded Slot 1 for {saveData.SelectedClubName}.";
        return true;
    }

    private bool TryReadSave(out SaveSlotData saveData, out string statusMessage)
    {
        saveData = new SaveSlotData();

        if (!HasSaveFile())
        {
            statusMessage = "No local save found.";
            return false;
        }

        try
        {
            using var saveFile = FileAccess.Open(SaveSlotPath, FileAccess.ModeFlags.Read);
            var json = saveFile.GetAsText();
            var payload = JsonSerializer.Deserialize<SaveSlotData>(json, JsonOptions);

            if (payload == null || !payload.CareerInitialized || string.IsNullOrWhiteSpace(payload.SelectedClubName))
            {
                statusMessage = "Save file is missing career-critical state.";
                return false;
            }

            if (payload.CompetitionTable == null || payload.CompetitionFixtures == null)
            {
                statusMessage = "Save file is missing competition state required by the current build.";
                return false;
            }

            saveData = payload;
            statusMessage = "Save ready.";
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Load failed: {ex.Message}";
            return false;
        }
    }
}
