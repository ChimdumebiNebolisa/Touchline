using Godot;
using System;
using System.Text.Json;

public sealed class SaveSlotData
{
    public int SaveVersion { get; set; }
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
    public string? SelectedPlayerProfileName { get; set; }
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
    public string TableImpactSummary { get; set; } = string.Empty;
    public string TacticalSummary { get; set; } = string.Empty;
    public string PressureSummary { get; set; } = string.Empty;
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
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
    public const int CurrentSaveVersion = 2;

    public static SaveSystem? Instance { get; private set; }
    public string LastStatusMessage { get; private set; } = "Save system idle.";

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
        if (!TryReadSave(out var saveData, out var errorMessage, out _))
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
            LastStatusMessage = statusMessage;
            return false;
        }

        try
        {
            var payload = BuildSavePayload(GameState.Instance);
            WriteSavePayload(payload);
            statusMessage = $"Career saved to Slot 1 for {payload.SelectedClubName}.";
            LastStatusMessage = statusMessage;
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Save failed: {ex.Message}";
            LastStatusMessage = statusMessage;
            return false;
        }
    }

    public bool TrySaveGame()
    {
        return SaveGame(out _);
    }

    public bool LoadGame(out string statusMessage)
    {
        if (GameState.Instance == null)
        {
            statusMessage = "Game state singleton unavailable.";
            LastStatusMessage = statusMessage;
            return false;
        }

        if (!TryReadSave(out var saveData, out statusMessage, out var migratedLegacySave))
        {
            LastStatusMessage = statusMessage;
            return false;
        }

        GameState.Instance.RestoreFromSave(saveData);
        if (migratedLegacySave)
        {
            WriteSavePayload(saveData);
        }

        statusMessage = $"Loaded Slot 1 for {saveData.SelectedClubName}.";
        LastStatusMessage = statusMessage;
        return true;
    }

    public bool TryLoadGame()
    {
        return LoadGame(out _);
    }

    private bool TryReadSave(out SaveSlotData saveData, out string statusMessage, out bool migratedLegacySave)
    {
        saveData = new SaveSlotData();
        migratedLegacySave = false;

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

            if (payload == null)
            {
                statusMessage = "Save file could not be deserialized into a career payload.";
                return false;
            }

            if (!payload.CareerInitialized)
            {
                statusMessage = "Save file is missing the career initialization flag.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(payload.SelectedClubName))
            {
                statusMessage = "Save file is missing the selected club.";
                return false;
            }

            if (payload.SaveVersion > CurrentSaveVersion)
            {
                statusMessage = $"Save file requires a newer build. Save version {payload.SaveVersion} is not supported by this client.";
                return false;
            }

            if (!TryNormalizeSavePayload(payload, out saveData, out statusMessage, out migratedLegacySave))
            {
                return false;
            }

            statusMessage = "Save ready.";
            return true;
        }
        catch (Exception ex)
        {
            statusMessage = $"Load failed: {ex.Message}";
            return false;
        }
    }

    private static bool TryNormalizeSavePayload(
        SaveSlotData payload,
        out SaveSlotData normalizedPayload,
        out string statusMessage,
        out bool migratedLegacySave)
    {
        normalizedPayload = payload;
        migratedLegacySave = false;

        if (payload.CompetitionTable != null && payload.CompetitionFixtures != null)
        {
            normalizedPayload = BuildSaveCopy(payload);
            normalizedPayload.SaveVersion = CurrentSaveVersion;
            migratedLegacySave = payload.SaveVersion < CurrentSaveVersion;
            statusMessage = "Save ready.";
            return true;
        }

        if (!SaveMigrationService.TryUpgradeLegacyPayload(payload, out normalizedPayload, out statusMessage))
        {
            return false;
        }

        migratedLegacySave = true;
        return true;
    }

    private static SaveSlotData BuildSavePayload(GameState state)
    {
        return new SaveSlotData
        {
            SaveVersion = CurrentSaveVersion,
            ManagerName = state.ManagerName,
            CareerSeed = state.CareerSeed,
            CareerInitialized = state.CareerInitialized,
            WorldSeed = state.WorldSeed,
            CountryPackId = state.CountryPackId,
            AvailableClubs = state.AvailableClubs,
            SelectedClubName = state.SelectedClubName,
            NextFixtureSummary = state.NextFixtureSummary,
            SquadStatusSummary = state.SquadStatusSummary,
            SquadPlayers = Array.ConvertAll(
                state.SquadPlayers,
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
            TacticalFormation = state.TacticalFormation,
            PressIntensity = state.PressIntensity,
            Tempo = state.Tempo,
            Width = state.Width,
            Risk = state.Risk,
            CompetitionName = state.CompetitionName,
            CurrentMatchday = state.CurrentMatchday,
            CurrentOpponentName = state.CurrentOpponentName,
            TeamMorale = state.TeamMorale,
            FanSentiment = state.FanSentiment,
            BoardConfidence = state.BoardConfidence,
            CurrentDateIso = state.CurrentDate.ToString("yyyy-MM-dd"),
            SeasonStartYear = state.SeasonStartYear,
            FormSummary = state.FormSummary,
            RecentResults = state.RecentResults,
            LastMatchReport = state.LastMatchReport == null
                ? null
                : new SaveSlotMatchReportData
                {
                    FixtureLabel = state.LastMatchReport.FixtureLabel,
                    Scoreline = state.LastMatchReport.Scoreline,
                    ResultLabel = state.LastMatchReport.ResultLabel,
                    ConsequenceSummary = state.LastMatchReport.ConsequenceSummary,
                    TableImpactSummary = state.LastMatchReport.TableImpactSummary,
                    TacticalSummary = state.LastMatchReport.TacticalSummary,
                    PressureSummary = state.LastMatchReport.PressureSummary,
                    KeyEvents = state.LastMatchReport.KeyEvents,
                    MoraleDelta = state.LastMatchReport.MoraleDelta,
                    FanDelta = state.LastMatchReport.FanDelta,
                    BoardDelta = state.LastMatchReport.BoardDelta
                },
            SelectedPlayerProfileName = state.SelectedPlayerProfileName,
            CompetitionTable = Array.ConvertAll(
                state.CompetitionTable,
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
                state.CompetitionFixtures,
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
    }

    internal static SaveSlotData BuildSaveCopy(SaveSlotData source)
    {
        return new SaveSlotData
        {
            SaveVersion = source.SaveVersion,
            ManagerName = source.ManagerName,
            CareerSeed = source.CareerSeed,
            CareerInitialized = source.CareerInitialized,
            WorldSeed = source.WorldSeed,
            CountryPackId = source.CountryPackId,
            AvailableClubs = source.AvailableClubs == null ? null : (string[])source.AvailableClubs.Clone(),
            SelectedClubName = source.SelectedClubName,
            NextFixtureSummary = source.NextFixtureSummary,
            SquadStatusSummary = source.SquadStatusSummary,
            SquadPlayers = source.SquadPlayers == null
                ? null
                : Array.ConvertAll(
                    source.SquadPlayers,
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
            TacticalFormation = source.TacticalFormation,
            PressIntensity = source.PressIntensity,
            Tempo = source.Tempo,
            Width = source.Width,
            Risk = source.Risk,
            CompetitionName = source.CompetitionName,
            CurrentMatchday = source.CurrentMatchday,
            CurrentOpponentName = source.CurrentOpponentName,
            TeamMorale = source.TeamMorale,
            FanSentiment = source.FanSentiment,
            BoardConfidence = source.BoardConfidence,
            CurrentDateIso = source.CurrentDateIso,
            SeasonStartYear = source.SeasonStartYear,
            FormSummary = source.FormSummary,
            RecentResults = source.RecentResults == null ? null : (string[])source.RecentResults.Clone(),
            LastMatchReport = source.LastMatchReport == null
                ? null
                : new SaveSlotMatchReportData
                {
                    FixtureLabel = source.LastMatchReport.FixtureLabel,
                    Scoreline = source.LastMatchReport.Scoreline,
                    ResultLabel = source.LastMatchReport.ResultLabel,
                    ConsequenceSummary = source.LastMatchReport.ConsequenceSummary,
                    TableImpactSummary = source.LastMatchReport.TableImpactSummary,
                    TacticalSummary = source.LastMatchReport.TacticalSummary,
                    PressureSummary = source.LastMatchReport.PressureSummary,
                    KeyEvents = source.LastMatchReport.KeyEvents == null ? null : (string[])source.LastMatchReport.KeyEvents.Clone(),
                    MoraleDelta = source.LastMatchReport.MoraleDelta,
                    FanDelta = source.LastMatchReport.FanDelta,
                    BoardDelta = source.LastMatchReport.BoardDelta
                },
            SelectedPlayerProfileName = source.SelectedPlayerProfileName,
            CompetitionTable = source.CompetitionTable == null
                ? null
                : Array.ConvertAll(
                    source.CompetitionTable,
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
            CompetitionFixtures = source.CompetitionFixtures == null
                ? null
                : Array.ConvertAll(
                    source.CompetitionFixtures,
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
    }

    private static void WriteSavePayload(SaveSlotData payload)
    {
        using var saveFile = FileAccess.Open(SaveSlotPath, FileAccess.ModeFlags.Write);
        saveFile.StoreString(JsonSerializer.Serialize(payload, JsonOptions));
    }
}
