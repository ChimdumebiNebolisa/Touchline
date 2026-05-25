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
    public SaveSlotCareerProfileData? CareerProfile { get; set; }
    public SaveSlotClubFoundationData? CurrentClub { get; set; }
    public SaveSlotStageFoundationData? StageFoundations { get; set; }
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

public sealed class SaveSlotCareerProfileData
{
    public string ManagerName { get; set; } = "Manager";
    public int CareerSeed { get; set; }
    public string RoleName { get; set; } = "Manager";
    public string BackgroundName { get; set; } = "Unknown Upstart";
    public string LicenseName { get; set; } = "National C License";
    public string? CurrentClubName { get; set; }
    public int Reputation { get; set; }
    public int BoardTrust { get; set; }
    public int PlayerTrust { get; set; }
    public int StaffTrust { get; set; }
    public int DirectorTrust { get; set; }
    public int MediaPressure { get; set; }
}

public sealed class SaveSlotClubFoundationData
{
    public string Name { get; set; } = string.Empty;
    public string IdentitySummary { get; set; } = string.Empty;
    public string ExpectationSummary { get; set; } = string.Empty;
    public string ArchetypeName { get; set; } = "Mid-table Stabilizer";
    public string BoardPhilosophyName { get; set; } = "Patient Long-Term Board";
    public string FanCultureName { get; set; } = "Attacking Football";
    public string DirectorOfFootballStyleName { get; set; } = "Data Operator";
    public string DirectorRelationshipName { get; set; } = "Neutral";
    public SaveSlotStaffMemberData[]? Staff { get; set; }
    public SaveSlotObjectiveData[]? Objectives { get; set; }
    public int TransferBudget { get; set; }
    public int WageBudget { get; set; }
    public int BoardMorale { get; set; }
    public int FanMorale { get; set; }
    public int SquadMorale { get; set; }
    public int JobPressure { get; set; }
    public string[]? NewsFeed { get; set; }
}

public sealed class SaveSlotStaffMemberData
{
    public string Name { get; set; } = string.Empty;
    public string RoleName { get; set; } = "First-Team Coach";
    public int Quality { get; set; }
    public string InfluenceSummary { get; set; } = string.Empty;
    public int ContractExpiryYear { get; set; }
    public int Wage { get; set; }
    public int Reputation { get; set; }
    public int Loyalty { get; set; }
    public int Ambition { get; set; }
    public string PreferredStyle { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
}

public sealed class SaveSlotObjectiveData
{
    public string Summary { get; set; } = string.Empty;
    public string PriorityName { get; set; } = "Important";
    public string TypeName { get; set; } = "League objective";
}

public sealed class SaveSlotPlayerData
{
    public string PlayerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Nationality { get; set; } = "Novaran";
    public int TrueAbility { get; set; }
    public int TechnicalAttribute { get; set; }
    public int TacticalAttribute { get; set; }
    public int PhysicalAttribute { get; set; }
    public int MentalAttribute { get; set; }
    public string KnownAttributesSummary { get; set; } = string.Empty;
    public string EstimatedAttributesSummary { get; set; } = string.Empty;
    public string UnknownAttributesSummary { get; set; } = string.Empty;
    public string PlayingStyle { get; set; } = string.Empty;
    public string Tendencies { get; set; } = string.Empty;
    public string Traits { get; set; } = string.Empty;
    public string Personality { get; set; } = string.Empty;
    public string TacticalFit { get; set; } = string.Empty;
    public string DevelopmentCurve { get; set; } = string.Empty;
    public int Form { get; set; }
    public int Morale { get; set; }
    public int Fitness { get; set; }
    public int Fatigue { get; set; }
    public int InjuryRisk { get; set; }
    public int Wage { get; set; }
    public int ContractExpiryYear { get; set; }
    public string ContractRole { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string PromiseSummary { get; set; } = string.Empty;
    public string TransferInterest { get; set; } = string.Empty;
    public int TacticalFitScore { get; set; }
    public int PlayerFamiliarity { get; set; }
    public int ScoutingConfidence { get; set; }
    public string KnownAttributeGroups { get; set; } = string.Empty;
    public string EstimatedAttributeGroups { get; set; } = string.Empty;
    public string UnknownAttributeGroups { get; set; } = string.Empty;
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
    public string CauseSummary { get; set; } = string.Empty;
    public string StatsSummary { get; set; } = string.Empty;
    public string KeyPlayerMoments { get; set; } = string.Empty;
    public string TacticalExplanation { get; set; } = string.Empty;
    public string TacticalSection { get; set; } = string.Empty;
    public string PlayerFitSection { get; set; } = string.Empty;
    public string FatigueSection { get; set; } = string.Empty;
    public string MoraleSection { get; set; } = string.Empty;
    public string BoardReactionSection { get; set; } = string.Empty;
    public string FanReactionSection { get; set; } = string.Empty;
    public string MediaStorySection { get; set; } = string.Empty;
    public string StaffAnalysisSection { get; set; } = string.Empty;
    public string DevelopmentNotesSection { get; set; } = string.Empty;
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
    public string CompetitionType { get; set; } = "League";
    public string CompetitionName { get; set; } = string.Empty;
    public string RoundName { get; set; } = string.Empty;
}

public partial class SaveSystem : Node
{
    private const string SaveSlotPath = "user://slot-1.json";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
    public const int CurrentSaveVersion = 19;

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

    public bool TryGetSlotPreview(out SaveSlotData saveData, out string statusMessage)
    {
        return TryReadSave(out saveData, out statusMessage, out _);
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

            if (!TryValidateLoadablePayload(saveData, out statusMessage))
            {
                return false;
            }

            statusMessage = "Save ready.";
            return true;
        }
        catch (JsonException ex)
        {
            statusMessage = $"Load failed: save JSON is corrupt or unreadable. {ex.Message}";
            return false;
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

        if (payload.SaveVersion >= 2 &&
            (payload.CompetitionTable == null || payload.CompetitionFixtures == null))
        {
            normalizedPayload = new SaveSlotData();
            statusMessage = "Save file is incomplete: versioned saves must include competition table and fixture state.";
            return false;
        }

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

    private static bool TryValidateLoadablePayload(SaveSlotData payload, out string statusMessage)
    {
        if (payload.AvailableClubs == null || payload.AvailableClubs.Length == 0)
        {
            statusMessage = "Save file is incomplete: no available clubs were stored.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(payload.SelectedClubName) || Array.IndexOf(payload.AvailableClubs, payload.SelectedClubName) < 0)
        {
            statusMessage = "Save file is incomplete: the selected club is not part of the stored club list.";
            return false;
        }

        if (payload.SquadPlayers == null || payload.SquadPlayers.Length == 0)
        {
            statusMessage = "Save file is incomplete: squad data is missing.";
            return false;
        }

        if (payload.CompetitionTable == null || payload.CompetitionTable.Length == 0)
        {
            statusMessage = "Save file is incomplete: competition table data is missing.";
            return false;
        }

        if (payload.CompetitionFixtures == null || payload.CompetitionFixtures.Length == 0)
        {
            statusMessage = "Save file is incomplete: fixture data is missing.";
            return false;
        }

        if (payload.CurrentMatchday <= 0)
        {
            statusMessage = "Save file is incomplete: current matchday is invalid.";
            return false;
        }

        if (payload.SeasonStartYear <= 0 || !DateTime.TryParse(payload.CurrentDateIso, out _))
        {
            statusMessage = "Save file is incomplete: season or date data is invalid.";
            return false;
        }

        var selectedClubInTable = false;
        foreach (var row in payload.CompetitionTable)
        {
            if (row.ClubName == payload.SelectedClubName)
            {
                selectedClubInTable = true;
                break;
            }
        }

        if (!selectedClubInTable)
        {
            statusMessage = "Save file is incomplete: the selected club is missing from the table.";
            return false;
        }

        statusMessage = "Save ready.";
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
            CareerProfile = BuildCareerProfileData(state.CareerProfile),
            CurrentClub = BuildClubFoundationData(state.CurrentClub),
            StageFoundations = state.BuildStageFoundationSaveData(),
            NextFixtureSummary = state.NextFixtureSummary,
            SquadStatusSummary = state.SquadStatusSummary,
            SquadPlayers = Array.ConvertAll(
                state.SquadPlayers,
                player => new SaveSlotPlayerData
                {
                    PlayerId = player.PlayerId,
                    Name = player.Name,
                    Position = player.Position,
                    Age = player.Age,
                    Nationality = player.Nationality,
                    TrueAbility = player.TrueAbility,
                    TechnicalAttribute = player.TechnicalAttribute,
                    TacticalAttribute = player.TacticalAttribute,
                    PhysicalAttribute = player.PhysicalAttribute,
                    MentalAttribute = player.MentalAttribute,
                    KnownAttributesSummary = player.KnownAttributesSummary,
                    EstimatedAttributesSummary = player.EstimatedAttributesSummary,
                    UnknownAttributesSummary = player.UnknownAttributesSummary,
                    PlayingStyle = player.PlayingStyle,
                    Tendencies = player.Tendencies,
                    Traits = player.Traits,
                    Personality = player.Personality,
                    TacticalFit = player.TacticalFit,
                    DevelopmentCurve = player.DevelopmentCurve,
                    Form = player.Form,
                    Morale = player.Morale,
                    Fitness = player.Fitness,
                    Fatigue = player.Fatigue,
                    InjuryRisk = player.InjuryRisk,
                    Wage = player.Wage,
                    ContractExpiryYear = player.ContractExpiryYear,
                    ContractRole = player.ContractRole,
                    Relationship = player.Relationship,
                    PromiseSummary = player.PromiseSummary,
                    TransferInterest = player.TransferInterest,
                    TacticalFitScore = player.TacticalFitScore,
                    PlayerFamiliarity = player.PlayerFamiliarity,
                    ScoutingConfidence = player.ScoutingConfidence,
                    KnownAttributeGroups = player.KnownAttributeGroups,
                    EstimatedAttributeGroups = player.EstimatedAttributeGroups,
                    UnknownAttributeGroups = player.UnknownAttributeGroups,
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
                    CauseSummary = state.LastMatchReport.CauseSummary,
                    StatsSummary = state.LastMatchReport.StatsSummary,
                    KeyPlayerMoments = state.LastMatchReport.KeyPlayerMoments,
                    TacticalExplanation = state.LastMatchReport.TacticalExplanation,
                    TacticalSection = state.LastMatchReport.TacticalSection,
                    PlayerFitSection = state.LastMatchReport.PlayerFitSection,
                    FatigueSection = state.LastMatchReport.FatigueSection,
                    MoraleSection = state.LastMatchReport.MoraleSection,
                    BoardReactionSection = state.LastMatchReport.BoardReactionSection,
                    FanReactionSection = state.LastMatchReport.FanReactionSection,
                    MediaStorySection = state.LastMatchReport.MediaStorySection,
                    StaffAnalysisSection = state.LastMatchReport.StaffAnalysisSection,
                    DevelopmentNotesSection = state.LastMatchReport.DevelopmentNotesSection,
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
                    ResultSummary = fixture.ResultSummary,
                    CompetitionType = fixture.CompetitionType,
                    CompetitionName = fixture.CompetitionName,
                    RoundName = fixture.RoundName
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
            CareerProfile = CloneCareerProfileData(source.CareerProfile),
            CurrentClub = CloneClubFoundationData(source.CurrentClub),
            StageFoundations = CloneStageFoundationData(source.StageFoundations),
            NextFixtureSummary = source.NextFixtureSummary,
            SquadStatusSummary = source.SquadStatusSummary,
            SquadPlayers = source.SquadPlayers == null
                ? null
                : Array.ConvertAll(
                    source.SquadPlayers,
                    player => new SaveSlotPlayerData
                    {
                        PlayerId = player.PlayerId,
                        Name = player.Name,
                        Position = player.Position,
                        Age = player.Age,
                        Nationality = player.Nationality,
                        TrueAbility = player.TrueAbility,
                        TechnicalAttribute = player.TechnicalAttribute,
                        TacticalAttribute = player.TacticalAttribute,
                        PhysicalAttribute = player.PhysicalAttribute,
                        MentalAttribute = player.MentalAttribute,
                        KnownAttributesSummary = player.KnownAttributesSummary,
                        EstimatedAttributesSummary = player.EstimatedAttributesSummary,
                        UnknownAttributesSummary = player.UnknownAttributesSummary,
                        PlayingStyle = player.PlayingStyle,
                        Tendencies = player.Tendencies,
                        Traits = player.Traits,
                        Personality = player.Personality,
                        TacticalFit = player.TacticalFit,
                        DevelopmentCurve = player.DevelopmentCurve,
                        Form = player.Form,
                        Morale = player.Morale,
                        Fitness = player.Fitness,
                        Fatigue = player.Fatigue,
                        InjuryRisk = player.InjuryRisk,
                        Wage = player.Wage,
                        ContractExpiryYear = player.ContractExpiryYear,
                        ContractRole = player.ContractRole,
                        Relationship = player.Relationship,
                        PromiseSummary = player.PromiseSummary,
                        TransferInterest = player.TransferInterest,
                        TacticalFitScore = player.TacticalFitScore,
                        PlayerFamiliarity = player.PlayerFamiliarity,
                        ScoutingConfidence = player.ScoutingConfidence,
                        KnownAttributeGroups = player.KnownAttributeGroups,
                        EstimatedAttributeGroups = player.EstimatedAttributeGroups,
                        UnknownAttributeGroups = player.UnknownAttributeGroups,
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
                    CauseSummary = source.LastMatchReport.CauseSummary,
                    StatsSummary = source.LastMatchReport.StatsSummary,
                    KeyPlayerMoments = source.LastMatchReport.KeyPlayerMoments,
                    TacticalExplanation = source.LastMatchReport.TacticalExplanation,
                    TacticalSection = source.LastMatchReport.TacticalSection,
                    PlayerFitSection = source.LastMatchReport.PlayerFitSection,
                    FatigueSection = source.LastMatchReport.FatigueSection,
                    MoraleSection = source.LastMatchReport.MoraleSection,
                    BoardReactionSection = source.LastMatchReport.BoardReactionSection,
                    FanReactionSection = source.LastMatchReport.FanReactionSection,
                    MediaStorySection = source.LastMatchReport.MediaStorySection,
                    StaffAnalysisSection = source.LastMatchReport.StaffAnalysisSection,
                    DevelopmentNotesSection = source.LastMatchReport.DevelopmentNotesSection,
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
                        ResultSummary = fixture.ResultSummary,
                        CompetitionType = fixture.CompetitionType,
                        CompetitionName = fixture.CompetitionName,
                        RoundName = fixture.RoundName
                })
        };
    }

    private static SaveSlotCareerProfileData BuildCareerProfileData(CareerProfile profile)
    {
        return new SaveSlotCareerProfileData
        {
            ManagerName = profile.ManagerName,
            CareerSeed = profile.CareerSeed,
            RoleName = CareerFoundation.GetDisplayName(profile.Role),
            BackgroundName = CareerFoundation.GetDisplayName(profile.Background),
            LicenseName = CareerFoundation.GetDisplayName(profile.License),
            CurrentClubName = profile.CurrentClubName,
            Reputation = profile.Reputation,
            BoardTrust = profile.BoardTrust,
            PlayerTrust = profile.PlayerTrust,
            StaffTrust = profile.StaffTrust,
            DirectorTrust = profile.DirectorTrust,
            MediaPressure = profile.MediaPressure
        };
    }

    private static SaveSlotClubFoundationData? BuildClubFoundationData(Club? club)
    {
        if (club == null)
        {
            return null;
        }

        return new SaveSlotClubFoundationData
        {
            Name = club.Name,
            IdentitySummary = club.IdentitySummary,
            ExpectationSummary = club.ExpectationSummary,
            ArchetypeName = CareerFoundation.GetDisplayName(club.Archetype),
            BoardPhilosophyName = CareerFoundation.GetDisplayName(club.BoardPhilosophy),
            FanCultureName = CareerFoundation.GetDisplayName(club.FanCulture),
            DirectorOfFootballStyleName = CareerFoundation.GetDisplayName(club.DirectorOfFootballStyle),
            DirectorRelationshipName = CareerFoundation.GetDisplayName(club.DirectorRelationshipState),
            Staff = Array.ConvertAll(
                club.Staff,
                staff => new SaveSlotStaffMemberData
                {
                    Name = staff.Name,
                    RoleName = CareerFoundation.GetDisplayName(staff.Role),
                    Quality = staff.Quality,
                    InfluenceSummary = staff.InfluenceSummary,
                    ContractExpiryYear = staff.ContractExpiryYear,
                    Wage = staff.Wage,
                    Reputation = staff.Reputation,
                    Loyalty = staff.Loyalty,
                    Ambition = staff.Ambition,
                    PreferredStyle = staff.PreferredStyle,
                    Relationship = staff.Relationship
                }),
            Objectives = Array.ConvertAll(
                club.Objectives,
                objective => new SaveSlotObjectiveData
                {
                    Summary = objective.Summary,
                    PriorityName = CareerFoundation.GetDisplayName(objective.Priority),
                    TypeName = CareerFoundation.GetDisplayName(objective.Type)
                }),
            TransferBudget = club.TransferBudget,
            WageBudget = club.WageBudget,
            BoardMorale = club.BoardMorale,
            FanMorale = club.FanMorale,
            SquadMorale = club.SquadMorale,
            JobPressure = club.JobPressure,
            NewsFeed = club.NewsFeed
        };
    }

    private static SaveSlotCareerProfileData? CloneCareerProfileData(SaveSlotCareerProfileData? source)
    {
        if (source == null)
        {
            return null;
        }

        return new SaveSlotCareerProfileData
        {
            ManagerName = source.ManagerName,
            CareerSeed = source.CareerSeed,
            RoleName = source.RoleName,
            BackgroundName = source.BackgroundName,
            LicenseName = source.LicenseName,
            CurrentClubName = source.CurrentClubName,
            Reputation = source.Reputation,
            BoardTrust = source.BoardTrust,
            PlayerTrust = source.PlayerTrust,
            StaffTrust = source.StaffTrust,
            DirectorTrust = source.DirectorTrust,
            MediaPressure = source.MediaPressure
        };
    }

    private static SaveSlotClubFoundationData? CloneClubFoundationData(SaveSlotClubFoundationData? source)
    {
        if (source == null)
        {
            return null;
        }

        return new SaveSlotClubFoundationData
        {
            Name = source.Name,
            IdentitySummary = source.IdentitySummary,
            ExpectationSummary = source.ExpectationSummary,
            ArchetypeName = source.ArchetypeName,
            BoardPhilosophyName = source.BoardPhilosophyName,
            FanCultureName = source.FanCultureName,
            DirectorOfFootballStyleName = source.DirectorOfFootballStyleName,
            DirectorRelationshipName = source.DirectorRelationshipName,
            Staff = source.Staff == null
                ? null
                : Array.ConvertAll(
                    source.Staff,
                    staff => new SaveSlotStaffMemberData
                    {
                        Name = staff.Name,
                        RoleName = staff.RoleName,
                        Quality = staff.Quality,
                        InfluenceSummary = staff.InfluenceSummary,
                        ContractExpiryYear = staff.ContractExpiryYear,
                        Wage = staff.Wage,
                        Reputation = staff.Reputation,
                        Loyalty = staff.Loyalty,
                        Ambition = staff.Ambition,
                        PreferredStyle = staff.PreferredStyle,
                        Relationship = staff.Relationship
                    }),
            Objectives = source.Objectives == null
                ? null
                : Array.ConvertAll(
                    source.Objectives,
                    objective => new SaveSlotObjectiveData
                    {
                        Summary = objective.Summary,
                        PriorityName = objective.PriorityName,
                        TypeName = objective.TypeName
                    }),
            TransferBudget = source.TransferBudget,
            WageBudget = source.WageBudget,
            BoardMorale = source.BoardMorale,
            FanMorale = source.FanMorale,
            SquadMorale = source.SquadMorale,
            JobPressure = source.JobPressure,
            NewsFeed = source.NewsFeed == null ? null : (string[])source.NewsFeed.Clone()
        };
    }

    private static SaveSlotStageFoundationData? CloneStageFoundationData(SaveSlotStageFoundationData? source)
    {
        if (source == null)
        {
            return null;
        }

        return new SaveSlotStageFoundationData
        {
            TeamStyleName = source.TeamStyleName,
            PassingDirectness = source.PassingDirectness,
            DefensiveLine = source.DefensiveLine,
            Tackling = source.Tackling,
            TacticalFamiliarityScore = source.TacticalFamiliarityScore,
            TeamInstructionsSummary = source.TeamInstructionsSummary,
            PlayerRolesSummary = source.PlayerRolesSummary,
            PlayerInstructionsSummary = source.PlayerInstructionsSummary,
            TacticalRoleFitScore = source.TacticalRoleFitScore,
            TacticalRoleFitSummary = source.TacticalRoleFitSummary,
            PlayerFamiliaritySummary = source.PlayerFamiliaritySummary,
            SetPieceApproachName = source.SetPieceApproachName,
            SetPieceSummary = source.SetPieceSummary,
            OpponentPreparationFocusName = source.OpponentPreparationFocusName,
            OpponentPreparationSummary = source.OpponentPreparationSummary,
            TacticalFitNotes = source.TacticalFitNotes,
            TacticalRiskNotes = source.TacticalRiskNotes,
            TrainingFocusName = source.TrainingFocusName,
            TrainingIntensityName = source.TrainingIntensityName,
            TrainingStatusSummary = source.TrainingStatusSummary,
            ScoutingReportDepthName = source.ScoutingReportDepthName,
            ScoutingAssignment = source.ScoutingAssignment == null
                ? null
                : new SaveSlotScoutingAssignmentData
                {
                    Target = source.ScoutingAssignment.Target,
                    DaysRemaining = source.ScoutingAssignment.DaysRemaining,
                    ReportQuality = source.ScoutingAssignment.ReportQuality,
                    DiscoverySummary = source.ScoutingAssignment.DiscoverySummary,
                    ReportReady = source.ScoutingAssignment.ReportReady
                },
            NewsEvents = source.NewsEvents == null
                ? null
                : Array.ConvertAll(
                    source.NewsEvents,
                    newsEvent => new SaveSlotNewsEventData
                    {
                        Title = newsEvent.Title,
                        CategoryName = newsEvent.CategoryName,
                        Reliability = newsEvent.Reliability,
                        Text = newsEvent.Text,
                        Importance = newsEvent.Importance,
                        SourceType = newsEvent.SourceType,
                        RelatedEntity = newsEvent.RelatedEntity,
                        EffectSummary = newsEvent.EffectSummary,
                        CooldownKey = newsEvent.CooldownKey
                    }),
            ActiveDecisionEvents = source.ActiveDecisionEvents == null
                ? null
                : Array.ConvertAll(source.ActiveDecisionEvents, CloneDecisionEventData),
            ResolvedDecisionEvents = source.ResolvedDecisionEvents == null
                ? null
                : Array.ConvertAll(source.ResolvedDecisionEvents, CloneDecisionEventData),
            RecruitmentTarget = source.RecruitmentTarget == null
                ? null
                : new SaveSlotRecruitmentTargetData
                {
                    PlayerName = source.RecruitmentTarget.PlayerName,
                    Position = source.RecruitmentTarget.Position,
                    InformationSummary = source.RecruitmentTarget.InformationSummary,
                    InterestSummary = source.RecruitmentTarget.InterestSummary,
                    TacticalFitSummary = source.RecruitmentTarget.TacticalFitSummary,
                    EstimatedFeeRange = source.RecruitmentTarget.EstimatedFeeRange,
                    EstimatedWageRange = source.RecruitmentTarget.EstimatedWageRange,
                    DirectorResponse = source.RecruitmentTarget.DirectorResponse,
                    BoardResponse = source.RecruitmentTarget.BoardResponse,
                    TargetStatus = source.RecruitmentTarget.TargetStatus,
                    ClubValuation = source.RecruitmentTarget.ClubValuation,
                    AgentMood = source.RecruitmentTarget.AgentMood,
                    RivalInterest = source.RecruitmentTarget.RivalInterest,
                    BoardStance = source.RecruitmentTarget.BoardStance,
                    DirectorStance = source.RecruitmentTarget.DirectorStance,
                    OutcomeState = source.RecruitmentTarget.OutcomeState,
                    IsLoanCandidate = source.RecruitmentTarget.IsLoanCandidate,
                    LoanDirection = source.RecruitmentTarget.LoanDirection,
                    DevelopmentLoanSuitability = source.RecruitmentTarget.DevelopmentLoanSuitability,
                    PlayingTimeExpectation = source.RecruitmentTarget.PlayingTimeExpectation,
                    LoanClubFit = source.RecruitmentTarget.LoanClubFit,
                    LoanReviewSummary = source.RecruitmentTarget.LoanReviewSummary,
                    Status = source.RecruitmentTarget.Status
                },
            RecruitmentShortlist = source.RecruitmentShortlist == null
                ? null
                : Array.ConvertAll(
                    source.RecruitmentShortlist,
                    target => new SaveSlotRecruitmentTargetData
                    {
                        PlayerName = target.PlayerName,
                        Position = target.Position,
                        InformationSummary = target.InformationSummary,
                        InterestSummary = target.InterestSummary,
                        TacticalFitSummary = target.TacticalFitSummary,
                        EstimatedFeeRange = target.EstimatedFeeRange,
                        EstimatedWageRange = target.EstimatedWageRange,
                        DirectorResponse = target.DirectorResponse,
                        BoardResponse = target.BoardResponse,
                        TargetStatus = target.TargetStatus,
                        ClubValuation = target.ClubValuation,
                        AgentMood = target.AgentMood,
                        RivalInterest = target.RivalInterest,
                        BoardStance = target.BoardStance,
                        DirectorStance = target.DirectorStance,
                        OutcomeState = target.OutcomeState,
                        IsLoanCandidate = target.IsLoanCandidate,
                        LoanDirection = target.LoanDirection,
                        DevelopmentLoanSuitability = target.DevelopmentLoanSuitability,
                        PlayingTimeExpectation = target.PlayingTimeExpectation,
                        LoanClubFit = target.LoanClubFit,
                        LoanReviewSummary = target.LoanReviewSummary,
                        Status = target.Status
                    }),
            TransferHistory = source.TransferHistory == null ? null : (string[])source.TransferHistory.Clone(),
            TransferContractOffer = source.TransferContractOffer == null ? null : CloneContractOfferData(source.TransferContractOffer),
            RenewalContractOffer = source.RenewalContractOffer == null ? null : CloneContractOfferData(source.RenewalContractOffer),
            ContractHistory = source.ContractHistory == null ? null : (string[])source.ContractHistory.Clone(),
            PromiseRecords = source.PromiseRecords == null
                ? null
                : Array.ConvertAll(
                    source.PromiseRecords,
                    promise => new SaveSlotPromiseRecordData
                    {
                        PromiseType = promise.PromiseType,
                        Recipient = promise.Recipient,
                        Source = promise.Source,
                        IsPublic = promise.IsPublic,
                        ExpectedAction = promise.ExpectedAction,
                        DeadlineSummary = promise.DeadlineSummary,
                        DaysRemaining = promise.DaysRemaining,
                        StatusName = promise.StatusName,
                        CurrentEvidence = promise.CurrentEvidence,
                        AgentMood = promise.AgentMood,
                        ConsequenceRisk = promise.ConsequenceRisk
                    }),
            JobSecurityName = source.JobSecurityName,
            JobOffer = source.JobOffer == null
                ? null
                : new SaveSlotJobOfferEventData
                {
                    OfferTypeName = source.JobOffer.OfferTypeName,
                    ClubName = source.JobOffer.ClubName,
                    RoleName = source.JobOffer.RoleName,
                    InterestSummary = source.JobOffer.InterestSummary,
                    Reason = source.JobOffer.Reason
                },
            CareerHistory = source.CareerHistory == null ? null : (string[])source.CareerHistory.Clone(),
            LicenseOpportunitySummary = source.LicenseOpportunitySummary,
            ObjectiveReviewSummary = source.ObjectiveReviewSummary,
            FanTrust = source.FanTrust,
            MediaTrust = source.MediaTrust,
            WorldReputation = source.WorldReputation,
            ClubReputation = source.ClubReputation,
            MediaReputation = source.MediaReputation,
            TacticalReputation = source.TacticalReputation,
            YouthReputation = source.YouthReputation,
            RecruitmentReputation = source.RecruitmentReputation,
            BoardPressure = source.BoardPressure,
            FanPressure = source.FanPressure,
            DressingRoomPressure = source.DressingRoomPressure,
            TransferPressure = source.TransferPressure,
            FinancialPressure = source.FinancialPressure,
            PerceptionHistory = source.PerceptionHistory == null ? null : (string[])source.PerceptionHistory.Clone(),
            DirectorCooperation = source.DirectorCooperation,
            DirectorConflict = source.DirectorConflict,
            DirectorScoutingPriority = source.DirectorScoutingPriority,
            DirectorTransferPreference = source.DirectorTransferPreference,
            DirectorSalesPressureSummary = source.DirectorSalesPressureSummary,
            DirectorBoardReportSummary = source.DirectorBoardReportSummary,
            DirectorActionHistory = source.DirectorActionHistory == null ? null : (string[])source.DirectorActionHistory.Clone(),
            StaffMarketCandidate = source.StaffMarketCandidate == null ? null : CloneStaffMarketCandidateData(source.StaffMarketCandidate),
            StaffReportSummary = source.StaffReportSummary,
            StaffMarketSummary = source.StaffMarketSummary,
            StaffHistory = source.StaffHistory == null ? null : (string[])source.StaffHistory.Clone(),
            YouthAcademyQuality = source.YouthAcademyQuality,
            YouthRecruitmentReach = source.YouthRecruitmentReach,
            YouthCoachingQuality = source.YouthCoachingQuality,
            YouthFacilitiesSummary = source.YouthFacilitiesSummary,
            YouthIntakeDateSummary = source.YouthIntakeDateSummary,
            YouthBoardExpectation = source.YouthBoardExpectation,
            YouthFanExpectation = source.YouthFanExpectation,
            YouthProspects = source.YouthProspects == null ? null : Array.ConvertAll(source.YouthProspects, CloneYouthProspectData),
            YouthHistory = source.YouthHistory == null ? null : (string[])source.YouthHistory.Clone(),
            PlayerDevelopmentSummary = source.PlayerDevelopmentSummary,
            PlayerDevelopmentHistory = source.PlayerDevelopmentHistory == null ? null : (string[])source.PlayerDevelopmentHistory.Clone(),
            FinanceTransferBudgetRemaining = source.FinanceTransferBudgetRemaining,
            FinanceWageBudget = source.FinanceWageBudget,
            FinanceCurrentWageBill = source.FinanceCurrentWageBill,
            FinanceTransferCommitments = source.FinanceTransferCommitments,
            FinanceDebt = source.FinanceDebt,
            FinanceRevenue = source.FinanceRevenue,
            FinanceExpenses = source.FinanceExpenses,
            FinanceProjectedBalance = source.FinanceProjectedBalance,
            FinanceTicketIncome = source.FinanceTicketIncome,
            FinanceCommercialIncome = source.FinanceCommercialIncome,
            FinancePrizeMoney = source.FinancePrizeMoney,
            FinanceBoardInjection = source.FinanceBoardInjection,
            FinanceBudgetCut = source.FinanceBudgetCut,
            WageStructurePressure = source.WageStructurePressure,
            FinanceSummary = source.FinanceSummary,
            ProfitExpectationSummary = source.ProfitExpectationSummary,
            BoardFinanceActionSummary = source.BoardFinanceActionSummary,
            FinanceHistory = source.FinanceHistory == null ? null : (string[])source.FinanceHistory.Clone(),
            CurrentDivisionTier = source.CurrentDivisionTier,
            CurrentDivisionName = source.CurrentDivisionName,
            CurrentDivisionReputation = source.CurrentDivisionReputation,
            CurrentDivisionSimulationDepth = source.CurrentDivisionSimulationDepth,
            LeaguePyramidSummary = source.LeaguePyramidSummary,
            PromotionRelegationSummary = source.PromotionRelegationSummary,
            ShadowLeagueSummary = source.ShadowLeagueSummary,
            LeagueHistory = source.LeagueHistory == null ? null : (string[])source.LeagueHistory.Clone(),
            CupCompetitionName = source.CupCompetitionName,
            CupStatusSummary = source.CupStatusSummary,
            CupDrawSummary = source.CupDrawSummary,
            CupObjectiveSummary = source.CupObjectiveSummary,
            CupPrizeMoney = source.CupPrizeMoney,
            CupEliminated = source.CupEliminated,
            CupWon = source.CupWon,
            CupHistory = source.CupHistory == null ? null : (string[])source.CupHistory.Clone()
        };
    }

    private static SaveSlotYouthProspectData CloneYouthProspectData(SaveSlotYouthProspectData source)
    {
        return new SaveSlotYouthProspectData
        {
            ProspectId = source.ProspectId,
            Name = source.Name,
            Age = source.Age,
            Position = source.Position,
            Region = source.Region,
            PlayingStyle = source.PlayingStyle,
            Personality = source.Personality,
            VisibleInfo = source.VisibleInfo,
            HiddenPotentialBand = source.HiddenPotentialBand,
            PotentialCertainty = source.PotentialCertainty,
            DevelopmentCurve = source.DevelopmentCurve,
            LoanSuitability = source.LoanSuitability,
            IsPromoted = source.IsPromoted,
            Status = source.Status
        };
    }

    private static SaveSlotStaffMarketCandidateData CloneStaffMarketCandidateData(SaveSlotStaffMarketCandidateData source)
    {
        return new SaveSlotStaffMarketCandidateData
        {
            Name = source.Name,
            RoleName = source.RoleName,
            Quality = source.Quality,
            Wage = source.Wage,
            ContractExpiryYear = source.ContractExpiryYear,
            Reputation = source.Reputation,
            Loyalty = source.Loyalty,
            Ambition = source.Ambition,
            PreferredStyle = source.PreferredStyle,
            Relationship = source.Relationship,
            InterestSummary = source.InterestSummary,
            BoardApproval = source.BoardApproval,
            Status = source.Status,
            OutcomeSummary = source.OutcomeSummary
        };
    }

    private static SaveSlotContractOfferData CloneContractOfferData(SaveSlotContractOfferData source)
    {
        return new SaveSlotContractOfferData
        {
            OfferId = source.OfferId,
            PlayerName = source.PlayerName,
            IsRenewal = source.IsRenewal,
            SourceType = source.SourceType,
            AgentArchetype = source.AgentArchetype,
            WageSummary = source.WageSummary,
            ProposedWage = source.ProposedWage,
            DurationSummary = source.DurationSummary,
            DurationYears = source.DurationYears,
            ExpirySummary = source.ExpirySummary,
            SquadRole = source.SquadRole,
            ClausesSummary = source.ClausesSummary,
            RenewalStatus = source.RenewalStatus,
            AgentMood = source.AgentMood,
            PlayerInterest = source.PlayerInterest,
            BoardApproval = source.BoardApproval,
            PromiseSummary = source.PromiseSummary,
            Status = source.Status,
            OutcomeSummary = source.OutcomeSummary,
            IsAccepted = source.IsAccepted
        };
    }

    private static SaveSlotDecisionEventData CloneDecisionEventData(SaveSlotDecisionEventData source)
    {
        return new SaveSlotDecisionEventData
        {
            EventId = source.EventId,
            EventTypeName = source.EventTypeName,
            Title = source.Title,
            SourceType = source.SourceType,
            Reliability = source.Reliability,
            RelatedEntity = source.RelatedEntity,
            Importance = source.Importance,
            Prompt = source.Prompt,
            PrimaryOption = source.PrimaryOption,
            SecondaryOption = source.SecondaryOption,
            PrimaryEffectSummary = source.PrimaryEffectSummary,
            SecondaryEffectSummary = source.SecondaryEffectSummary,
            CooldownKey = source.CooldownKey,
            DaysUntilRepeat = source.DaysUntilRepeat,
            IsResolved = source.IsResolved,
            OutcomeSummary = source.OutcomeSummary
        };
    }

    private static void WriteSavePayload(SaveSlotData payload)
    {
        using var saveFile = FileAccess.Open(SaveSlotPath, FileAccess.ModeFlags.Write);
        saveFile.StoreString(JsonSerializer.Serialize(payload, JsonOptions));
    }
}
