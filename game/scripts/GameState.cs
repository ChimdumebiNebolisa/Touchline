using Godot;
using System;

public partial class GameState : Node
{
    public sealed class SquadPlayer
    {
        public required string Name { get; init; }
        public required string Position { get; init; }
        public required int Age { get; init; }
        public required int Morale { get; init; }
        public required int Fitness { get; init; }
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
        AvailableClubs = new[]
        {
            "Riverton Athletic",
            "Northbridge City",
            "Harbor County",
            "Eastvale Rovers"
        };
        SelectedClubName = null;
        NextFixtureSummary = "Matchday 1: Riverton Athletic vs Harbor County";
        SquadStatusSummary = "23 registered players | 20 fit | morale steady";
        SquadPlayers = Array.Empty<SquadPlayer>();
    }

    public void SelectClub(string clubName)
    {
        SelectedClubName = clubName;

        SquadPlayers = new[]
        {
            new SquadPlayer { Name = "Mateo Silva", Position = "GK", Age = 27, Morale = 73, Fitness = 91 },
            new SquadPlayer { Name = "Liam Ofori", Position = "RB", Age = 24, Morale = 69, Fitness = 88 },
            new SquadPlayer { Name = "Ethan Novak", Position = "CB", Age = 29, Morale = 76, Fitness = 86 },
            new SquadPlayer { Name = "Jonas Petrov", Position = "CB", Age = 22, Morale = 71, Fitness = 90 },
            new SquadPlayer { Name = "Kai Mendes", Position = "LB", Age = 25, Morale = 68, Fitness = 87 },
            new SquadPlayer { Name = "Noah Ibe", Position = "CM", Age = 23, Morale = 74, Fitness = 89 },
            new SquadPlayer { Name = "Adrian Voss", Position = "CM", Age = 30, Morale = 72, Fitness = 82 },
            new SquadPlayer { Name = "Rafael Costa", Position = "AM", Age = 21, Morale = 79, Fitness = 92 },
            new SquadPlayer { Name = "Tariq Balde", Position = "RW", Age = 26, Morale = 77, Fitness = 90 },
            new SquadPlayer { Name = "Milo Renard", Position = "ST", Age = 28, Morale = 75, Fitness = 85 },
            new SquadPlayer { Name = "Kenji Sato", Position = "LW", Age = 24, Morale = 70, Fitness = 88 }
        };
    }
}
