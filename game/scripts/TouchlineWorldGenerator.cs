using Godot;
using System;

public partial class TouchlineWorldGenerator : Node
{
    private const string DefaultCountryPackId = "country-pack-alpha";
    private const string DefaultCompetitionName = "Novara Premier Division";
    private static readonly DateTime DefaultStartDate = new(2026, 8, 3);

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

        var bootstrap = new CareerBootstrapState
        {
            ManagerName = managerName,
            CareerSeed = seed,
            WorldSeed = seed,
            CountryPackId = DefaultCountryPackId,
            AvailableClubs = BuildAvailableClubs(),
            CompetitionName = DefaultCompetitionName,
            CurrentDate = DefaultStartDate,
            SeasonStartYear = DefaultStartDate.Year,
            TeamMorale = 72,
            FanSentiment = 63,
            BoardConfidence = 61,
            TacticalFormation = "4-3-3",
            PressIntensity = 60,
            Tempo = 58,
            Width = 55,
            Risk = 52,
            FormSummary = "Form: season about to begin."
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

        if (Array.IndexOf(GameState.Instance.AvailableClubs, clubName) < 0)
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
                ClubName = clubName,
                CompetitionName = DefaultCompetitionName,
                CurrentMatchday = 1,
                SquadPlayers = BuildSquadPlayers(clubName),
                CompetitionTable = competitionState.table,
                CompetitionFixtures = competitionState.fixtures,
                CurrentOpponentName = fixtureContext.currentOpponentName,
                NextFixtureSummary = fixtureContext.nextFixtureSummary
            });

        LastStatusMessage = $"Selected club: {clubName}";
        return true;
    }

    public GameState.ClubPreview GetClubPreview(string clubName)
    {
        var openingOpponent = GetOpeningOpponent(clubName);
        return clubName switch
        {
            "Riverton Athletic" => new GameState.ClubPreview
            {
                ClubName = clubName,
                IdentitySummary = "A stable dressing room with supporters who expect assertive front-foot football.",
                ExpectationSummary = "Board line: stay in the upper half and keep home form dependable.",
                OpeningFixtureSummary = $"Opening fixture: {clubName} vs {openingOpponent}"
            },
            "Northbridge City" => new GameState.ClubPreview
            {
                ClubName = clubName,
                IdentitySummary = "A possession-leaning side with little patience for passive matchdays.",
                ExpectationSummary = "Board line: push for the top positions and set the tempo early in the season.",
                OpeningFixtureSummary = $"Opening fixture: {clubName} vs {openingOpponent}"
            },
            "Harbor County" => new GameState.ClubPreview
            {
                ClubName = clubName,
                IdentitySummary = "A resilient squad expected to scrap for points and stay emotionally together under pressure.",
                ExpectationSummary = "Board line: keep the club clear of trouble and make the ground difficult to visit.",
                OpeningFixtureSummary = $"Opening fixture: {clubName} vs {openingOpponent}"
            },
            "Eastvale Rovers" => new GameState.ClubPreview
            {
                ClubName = clubName,
                IdentitySummary = "A younger side with energy, pace, and supporters hungry for visible progress.",
                ExpectationSummary = "Board line: outperform pre-season caution and build belief quickly.",
                OpeningFixtureSummary = $"Opening fixture: {clubName} vs {openingOpponent}"
            },
            _ => new GameState.ClubPreview
            {
                ClubName = clubName,
                IdentitySummary = "Club identity context is still being assembled.",
                ExpectationSummary = "Board line: establish a stable start and manage the opening weeks cleanly.",
                OpeningFixtureSummary = $"Opening fixture: {clubName} vs {openingOpponent}"
            }
        };
    }

    private static string[] BuildAvailableClubs()
    {
        return new[]
        {
            "Riverton Athletic",
            "Northbridge City",
            "Harbor County",
            "Eastvale Rovers"
        };
    }

    private static GameState.SquadPlayer[] BuildSquadPlayers(string clubName)
    {
        return clubName switch
        {
            "Northbridge City" => new[]
            {
                CreateSquadPlayer("Mateo Silva", "GK", 27, 72, 73, 91, true),
                CreateSquadPlayer("Liam Ofori", "RB", 24, 69, 70, 88, true),
                CreateSquadPlayer("Ethan Novak", "CB", 29, 74, 76, 86, true),
                CreateSquadPlayer("Jonas Petrov", "CB", 22, 72, 72, 90, true),
                CreateSquadPlayer("Kai Mendes", "LB", 25, 68, 69, 87, true),
                CreateSquadPlayer("Noah Ibe", "CM", 23, 76, 75, 89, true),
                CreateSquadPlayer("Adrian Voss", "CM", 30, 70, 73, 82, true),
                CreateSquadPlayer("Rafael Costa", "AM", 21, 81, 80, 92, true),
                CreateSquadPlayer("Tariq Balde", "RW", 26, 77, 78, 90, true),
                CreateSquadPlayer("Milo Renard", "ST", 28, 74, 76, 85, true),
                CreateSquadPlayer("Kenji Sato", "LW", 24, 71, 71, 88, true),
                CreateSquadPlayer("Dario Klein", "GK", 20, 64, 66, 84, false),
                CreateSquadPlayer("Felix Mensah", "CB", 25, 66, 68, 83, false),
                CreateSquadPlayer("Omar Nadir", "CM", 19, 70, 72, 86, false),
                CreateSquadPlayer("Lucas Marin", "ST", 23, 69, 70, 87, false)
            },
            "Harbor County" => new[]
            {
                CreateSquadPlayer("Sergio Vale", "GK", 30, 69, 70, 88, true),
                CreateSquadPlayer("Bastien Kone", "RB", 27, 66, 68, 85, true),
                CreateSquadPlayer("Marek Duda", "CB", 28, 71, 72, 84, true),
                CreateSquadPlayer("Caleb Hwang", "CB", 24, 68, 69, 87, true),
                CreateSquadPlayer("Nuri Demir", "LB", 26, 65, 67, 86, true),
                CreateSquadPlayer("Joel Aina", "CM", 23, 72, 73, 88, true),
                CreateSquadPlayer("Rui Esteves", "CM", 29, 67, 69, 81, true),
                CreateSquadPlayer("Mason Pike", "AM", 22, 74, 75, 89, true),
                CreateSquadPlayer("Jamal Sarr", "RW", 25, 73, 74, 87, true),
                CreateSquadPlayer("Ilian Petrescu", "ST", 27, 72, 73, 84, true),
                CreateSquadPlayer("Timo Larsen", "LW", 23, 69, 70, 86, true),
                CreateSquadPlayer("Arlo Finch", "GK", 19, 62, 64, 82, false),
                CreateSquadPlayer("Matias Gori", "CB", 24, 64, 66, 81, false),
                CreateSquadPlayer("Yonas Bekele", "CM", 20, 68, 69, 85, false),
                CreateSquadPlayer("Danilo Viera", "ST", 22, 67, 68, 85, false)
            },
            "Eastvale Rovers" => new[]
            {
                CreateSquadPlayer("Riku Tanaka", "GK", 25, 68, 69, 89, true),
                CreateSquadPlayer("Theo March", "RB", 21, 70, 71, 90, true),
                CreateSquadPlayer("Nils Bauer", "CB", 24, 69, 70, 88, true),
                CreateSquadPlayer("Pietro Leone", "CB", 22, 68, 69, 89, true),
                CreateSquadPlayer("Ayden Okoro", "LB", 20, 71, 72, 91, true),
                CreateSquadPlayer("Sami Halme", "CM", 22, 73, 74, 90, true),
                CreateSquadPlayer("Diego Armas", "CM", 24, 70, 71, 87, true),
                CreateSquadPlayer("Mikel Duarte", "AM", 19, 77, 78, 92, true),
                CreateSquadPlayer("Cian Murphy", "RW", 21, 74, 75, 91, true),
                CreateSquadPlayer("Tiago Freitas", "ST", 22, 72, 73, 88, true),
                CreateSquadPlayer("Luca Neri", "LW", 20, 75, 76, 90, true),
                CreateSquadPlayer("Pavel Cech", "GK", 18, 61, 64, 83, false),
                CreateSquadPlayer("Eli Grant", "CB", 19, 65, 67, 84, false),
                CreateSquadPlayer("Moussa Faye", "CM", 18, 69, 71, 87, false),
                CreateSquadPlayer("Nico Serra", "ST", 19, 68, 69, 86, false)
            },
            _ => new[]
            {
                CreateSquadPlayer("Mateo Silva", "GK", 27, 72, 73, 91, true),
                CreateSquadPlayer("Liam Ofori", "RB", 24, 68, 69, 88, true),
                CreateSquadPlayer("Ethan Novak", "CB", 29, 74, 76, 86, true),
                CreateSquadPlayer("Jonas Petrov", "CB", 22, 71, 71, 90, true),
                CreateSquadPlayer("Kai Mendes", "LB", 25, 67, 68, 87, true),
                CreateSquadPlayer("Noah Ibe", "CM", 23, 75, 74, 89, true),
                CreateSquadPlayer("Adrian Voss", "CM", 30, 69, 72, 82, true),
                CreateSquadPlayer("Rafael Costa", "AM", 21, 80, 79, 92, true),
                CreateSquadPlayer("Tariq Balde", "RW", 26, 76, 77, 90, true),
                CreateSquadPlayer("Milo Renard", "ST", 28, 73, 75, 85, true),
                CreateSquadPlayer("Kenji Sato", "LW", 24, 70, 70, 88, true),
                CreateSquadPlayer("Dario Klein", "GK", 20, 64, 66, 84, false),
                CreateSquadPlayer("Felix Mensah", "CB", 25, 65, 67, 83, false),
                CreateSquadPlayer("Omar Nadir", "CM", 19, 70, 72, 86, false),
                CreateSquadPlayer("Lucas Marin", "ST", 23, 68, 69, 87, false)
            }
        };
    }

    private static GameState.SquadPlayer CreateSquadPlayer(
        string name,
        string position,
        int age,
        int form,
        int morale,
        int fitness,
        bool isStarting)
    {
        return new GameState.SquadPlayer
        {
            Name = name,
            Position = position,
            Age = age,
            Form = form,
            Morale = morale,
            Fitness = fitness,
            IsStarting = isStarting
        };
    }

    private string GetOpeningOpponent(string clubName)
    {
        var clubs = GameState.Instance?.AvailableClubs ?? BuildAvailableClubs();
        foreach (var candidate in clubs)
        {
            if (candidate != clubName)
            {
                return candidate;
            }
        }

        return "Harbor County";
    }
}
