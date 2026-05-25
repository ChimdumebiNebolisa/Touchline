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
        return BeginNewCareer(
            managerName,
            seed,
            CareerFoundation.GetDisplayName(ManagerRole.Manager),
            CareerFoundation.GetDisplayName(ManagerBackground.UnknownUpstart),
            CareerFoundation.GetDisplayName(ManagerLicense.NationalCLicense));
    }

    public bool BeginNewCareer(string managerName, int seed, string roleName, string backgroundName, string licenseName)
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
            Role = CareerFoundation.ParseRole(roleName),
            Background = CareerFoundation.ParseBackground(backgroundName),
            License = CareerFoundation.ParseLicense(licenseName),
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

        var selectedClub = CareerFoundation.BuildClubFoundation(
            clubData.Name,
            clubData.IdentitySummary,
            clubData.ExpectationSummary,
            clubData.TeamMorale ?? seedData.Defaults.TeamMorale,
            clubData.FanSentiment ?? seedData.Defaults.FanSentiment,
            clubData.BoardConfidence ?? seedData.Defaults.BoardConfidence,
            GameState.Instance.CareerProfile,
            GameState.Instance.WorldSeed);
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
                Club = selectedClub,
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

        var previewProfile = GameState.Instance?.CareerProfile ??
            CareerFoundation.CreateCareerProfile(
                "Manager",
                0,
                ManagerRole.Manager,
                ManagerBackground.UnknownUpstart,
                ManagerLicense.NationalCLicense);
        var previewClub = CareerFoundation.BuildClubFoundation(
            clubData.Name,
            clubData.IdentitySummary,
            clubData.ExpectationSummary,
            clubData.TeamMorale ?? seedData.Defaults.TeamMorale,
            clubData.FanSentiment ?? seedData.Defaults.FanSentiment,
            clubData.BoardConfidence ?? seedData.Defaults.BoardConfidence,
            previewProfile,
            GameState.Instance?.WorldSeed ?? 0);

        return new GameState.ClubPreview
        {
            ClubName = clubData.Name,
            IdentitySummary = clubData.IdentitySummary,
            ExpectationSummary = clubData.ExpectationSummary,
            ArchetypeName = CareerFoundation.GetDisplayName(previewClub.Archetype),
            BoardPhilosophyName = CareerFoundation.GetDisplayName(previewClub.BoardPhilosophy),
            FanCultureName = CareerFoundation.GetDisplayName(previewClub.FanCulture),
            DirectorOfFootballStyleName = CareerFoundation.GetDisplayName(previewClub.DirectorOfFootballStyle),
            DirectorRelationshipName = CareerFoundation.GetDisplayName(previewClub.DirectorRelationshipState),
            BudgetSummary = CareerFoundation.BuildBudgetSummary(previewClub),
            ObjectivesSummary = CareerFoundation.BuildObjectivesSummary(previewClub),
            OpeningFixtureSummary = $"Opening fixture: {clubData.Name} vs {GetOpeningOpponent(seedData, clubData.Name)}"
        };
    }

    public ClubSquadPlayer[] ResolveClubSquad(string clubName, int worldSeed)
    {
        if (TryGetSeedData(out var seedData))
        {
            var clubData = FindClubData(seedData, clubName);
            if (clubData != null && clubData.Players.Length >= 11)
            {
                return ClubSquadFactory.FromSeedClub(clubData, worldSeed);
            }

            LastStatusMessage = $"Using deterministic fallback squad for {clubName} because seeded club data is incomplete.";
            return ClubSquadFactory.BuildFallbackSquad(clubName, worldSeed);
        }

        LastStatusMessage = $"Using deterministic fallback squad for {clubName} because world seed data could not be loaded.";
        return ClubSquadFactory.BuildFallbackSquad(clubName, worldSeed);
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
        var players = new GameState.SquadPlayer[clubData.Players.Length];
        for (var index = 0; index < clubData.Players.Length; index++)
        {
            players[index] = PlayerIdentityFoundation.BuildSquadPlayer(clubData.Players[index], clubData.Name, GameState.Instance?.WorldSeed ?? 0, index);
        }

        return players;
    }

    private static GameState.ClubPreview BuildFallbackPreview(string clubName)
    {
        var fallbackProfile = CareerFoundation.CreateCareerProfile(
            "Manager",
            0,
            ManagerRole.Manager,
            ManagerBackground.UnknownUpstart,
            ManagerLicense.NationalCLicense);
        var fallbackClub = CareerFoundation.BuildFallbackClubFoundation(clubName, 60, 60, 60, fallbackProfile, 0);
        return new GameState.ClubPreview
        {
            ClubName = clubName,
            IdentitySummary = "Club identity context unavailable because world seed data failed to load.",
            ExpectationSummary = "Board expectation context unavailable because world seed data failed to load.",
            ArchetypeName = CareerFoundation.GetDisplayName(fallbackClub.Archetype),
            BoardPhilosophyName = CareerFoundation.GetDisplayName(fallbackClub.BoardPhilosophy),
            FanCultureName = CareerFoundation.GetDisplayName(fallbackClub.FanCulture),
            DirectorOfFootballStyleName = CareerFoundation.GetDisplayName(fallbackClub.DirectorOfFootballStyle),
            DirectorRelationshipName = CareerFoundation.GetDisplayName(fallbackClub.DirectorRelationshipState),
            BudgetSummary = CareerFoundation.BuildBudgetSummary(fallbackClub),
            ObjectivesSummary = CareerFoundation.BuildObjectivesSummary(fallbackClub),
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
