using Godot;
using System;

public partial class GameState : Node
{
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
    }

    public void SelectClub(string clubName)
    {
        SelectedClubName = clubName;
    }
}
