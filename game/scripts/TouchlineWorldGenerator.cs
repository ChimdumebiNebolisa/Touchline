using Godot;
using System;

public partial class TouchlineWorldGenerator : Node
{
    private WorldSeedData? _cachedSeedData;

    public static TouchlineWorldGenerator? Instance { get; private set; }

    public string LastStatusMessage { get; private set; } = "World generation idle.";

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

    public bool BeginNewCareer(string managerName, int seed)
    {
        if (GameState.Instance == null)
        {
            LastStatusMessage = "GameState singleton is unavailable.";
            return false;
        }

        if (!TryGetSeedData(out var seedData))
        {
            return false;
        }

        if (!WorldSeedDataLoader.TryParseStartDate(seedData.StartDateIso, out var startDate))
        {
            LastStatusMessage = "World seed data has an invalid StartDateIso. Expected yyyy-MM-dd.";
            return false;
        }

        var bootstrap = new CareerBootstrapState
        {
            ManagerName = managerName,
            CareerSeed = seed,
            WorldSeed = seed,
            CountryPackId = seedData.CountryPackId,
            AvailableClubs = BuildAvailableClubs(seedData),
            CompetitionName = seedData.CompetitionName,
            CurrentDate = startDate,
            SeasonStartYear = startDate.Year,
            TeamMorale = seedData.Defaults.TeamMorale,
            FanSentiment = seedData.Defaults.FanSentiment,
            BoardConfidence = seedData.Defaults.BoardConfidence,
            TacticalFormation = seedData.Defaults.TacticalFormation,
            PressIntensity = seedData.Defaults.PressIntensity,
            Tempo = seedData.Defaults.Tempo,
            Width = seedData.Defaults.Width,
            Risk = seedData.Defaults.Risk,
            FormSummary = seedData.Defaults.FormSummary
        };

        GameState.Instance.ApplyCareerBootstrap(bootstrap);
        LastStatusMessage = $"Career initialized for {managerName} (Seed {seed}).";
        return true;
    }

    public bool SelectClub(string clubName)
    {
        if (GameState.Instance == null)
        {
            LastStatusMessage = "GameState singleton is unavailable.";
            return false;
        }

        if (!GameState.Instance.CareerInitialized)
        {
            LastStatusMessage = "Career setup is incomplete.";
            return false;
        }

        if (!TryGetSeedData(out var seedData))
        {
            return false;
        }

        var clubData = FindClubData(seedData, clubName);
        if (clubData == null)
        {
            LastStatusMessage = "Selected club is unavailable from seeded data.";
            return false;
        }

        var competitionState = CompetitionRuntimeService.BuildInitialState(GameState.Instance.AvailableClubs, clubName);
        var fixtureContext = CompetitionRuntimeService.ResolveFixtureContext(
            competitionState.fixtures,
            1,
            clubName,
            GameState.Instance.CurrentDateLabel);

        GameState.Instance.ApplyClubSelection(
            new ClubSelectionState
            {
                ClubName = clubData.Name,
                CompetitionName = seedData.CompetitionName,
                CurrentMatchday = 1,
                TeamMorale = clubData.TeamMorale ?? seedData.Defaults.TeamMorale,
                FanSentiment = clubData.FanSentiment ?? seedData.Defaults.FanSentiment,
                BoardConfidence = clubData.BoardConfidence ?? seedData.Defaults.BoardConfidence,
                SquadPlayers = BuildSquadPlayers(clubData),
                CompetitionTable = competitionState.table,
                CompetitionFixtures = competitionState.fixtures,
                CurrentOpponentName = fixtureContext.currentOpponentName,
                NextFixtureSummary = fixtureContext.nextFixtureSummary
            });

        LastStatusMessage = $"Selected club: {clubData.Name}";
        return true;
    }

    public GameState.ClubPreview GetClubPreview(string clubName)
    {
        if (!TryGetSeedData(out var seedData))
        {
            return BuildFallbackPreview(clubName);
        }

        var clubData = FindClubData(seedData, clubName);
        if (clubData == null)
        {
            return BuildFallbackPreview(clubName);
        }

        return new GameState.ClubPreview
        {
            ClubName = clubData.Name,
            IdentitySummary = clubData.IdentitySummary,
            ExpectationSummary = clubData.ExpectationSummary,
            OpeningFixtureSummary = $"Opening fixture: {clubData.Name} vs {GetOpeningOpponent(seedData, clubData.Name)}"
        };
    }

    private bool TryGetSeedData(out WorldSeedData seedData)
    {
        if (_cachedSeedData != null)
        {
            seedData = _cachedSeedData;
            return true;
        }

        if (!WorldSeedDataLoader.TryLoad(out seedData, out var errorMessage))
        {
            LastStatusMessage = errorMessage;
            return false;
        }

        _cachedSeedData = seedData;
        return true;
    }

    private static string[] BuildAvailableClubs(WorldSeedData seedData)
    {
        return Array.ConvertAll(seedData.Clubs, club => club.Name);
    }

    private static WorldSeedClubData? FindClubData(WorldSeedData seedData, string clubName)
    {
        foreach (var club in seedData.Clubs)
        {
            if (club.Name == clubName)
            {
                return club;
            }
        }

        return null;
    }

    private static GameState.SquadPlayer[] BuildSquadPlayers(WorldSeedClubData clubData)
    {
        return Array.ConvertAll(
            clubData.Players,
            player => new GameState.SquadPlayer
            {
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Form = player.Form,
                Morale = player.Morale,
                Fitness = player.Fitness,
                IsStarting = player.IsStarting
            });
    }

    private static GameState.ClubPreview BuildFallbackPreview(string clubName)
    {
        return new GameState.ClubPreview
        {
            ClubName = clubName,
            IdentitySummary = "Club identity context unavailable because world seed data failed to load.",
            ExpectationSummary = "Board expectation context unavailable because world seed data failed to load.",
            OpeningFixtureSummary = "Opening fixture unavailable."
        };
    }

    private static string GetOpeningOpponent(WorldSeedData seedData, string clubName)
    {
        foreach (var club in seedData.Clubs)
        {
            if (club.Name != clubName)
            {
                return club.Name;
            }
        }

        return "Opponent unavailable";
    }
}
