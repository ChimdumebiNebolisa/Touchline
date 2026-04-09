public sealed class WorldSeedData
{
    public string CountryPackId { get; set; } = "country-pack-alpha";
    public string CompetitionName { get; set; } = "Novara Premier Division";
    public string StartDateIso { get; set; } = "2026-08-03";
    public WorldSeedDefaultsData Defaults { get; set; } = new();
    public WorldSeedClubData[] Clubs { get; set; } = [];
}

public sealed class WorldSeedDefaultsData
{
    public int TeamMorale { get; set; } = 72;
    public int FanSentiment { get; set; } = 63;
    public int BoardConfidence { get; set; } = 61;
    public string TacticalFormation { get; set; } = "4-3-3";
    public int PressIntensity { get; set; } = 60;
    public int Tempo { get; set; } = 58;
    public int Width { get; set; } = 55;
    public int Risk { get; set; } = 52;
    public string FormSummary { get; set; } = "Form: season about to begin.";
}

public sealed class WorldSeedClubData
{
    public string Name { get; set; } = string.Empty;
    public string IdentitySummary { get; set; } = string.Empty;
    public string ExpectationSummary { get; set; } = string.Empty;
    public int? TeamMorale { get; set; }
    public int? FanSentiment { get; set; }
    public int? BoardConfidence { get; set; }
    public WorldSeedPlayerData[] Players { get; set; } = [];
}

public sealed class WorldSeedPlayerData
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Form { get; set; }
    public int Morale { get; set; }
    public int Fitness { get; set; }
    public bool IsStarting { get; set; }
}
