using Godot;
using System;

public partial class GameState : Node
{
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
        TacticalFormation = "4-3-3";
        PressIntensity = 60;
        Tempo = 58;
        Width = 55;
        Risk = 52;
    }

    public void SelectClub(string clubName)
    {
        SelectedClubName = clubName;

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
}
