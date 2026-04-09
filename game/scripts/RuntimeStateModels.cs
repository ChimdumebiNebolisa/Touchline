using System;

public sealed class CareerBootstrapState
{
    public required string ManagerName { get; init; }
    public required int CareerSeed { get; init; }
    public required int WorldSeed { get; init; }
    public required string CountryPackId { get; init; }
    public required string[] AvailableClubs { get; init; }
    public required string CompetitionName { get; init; }
    public required DateTime CurrentDate { get; init; }
    public required int SeasonStartYear { get; init; }
    public required int TeamMorale { get; init; }
    public required int FanSentiment { get; init; }
    public required int BoardConfidence { get; init; }
    public required string TacticalFormation { get; init; }
    public required int PressIntensity { get; init; }
    public required int Tempo { get; init; }
    public required int Width { get; init; }
    public required int Risk { get; init; }
    public required string FormSummary { get; init; }
}

public sealed class ClubSelectionState
{
    public required string ClubName { get; init; }
    public required string CompetitionName { get; init; }
    public required int CurrentMatchday { get; init; }
    public required int TeamMorale { get; init; }
    public required int FanSentiment { get; init; }
    public required int BoardConfidence { get; init; }
    public required GameState.SquadPlayer[] SquadPlayers { get; init; }
    public required GameState.CompetitionRow[] CompetitionTable { get; init; }
    public required GameState.CompetitionFixture[] CompetitionFixtures { get; init; }
    public required string CurrentOpponentName { get; init; }
    public required string NextFixtureSummary { get; init; }
}

public sealed class CalendarAdvanceState
{
    public required DateTime CurrentDate { get; init; }
    public required int SeasonStartYear { get; init; }
    public required int CurrentMatchday { get; init; }
    public required string FormSummary { get; init; }
    public required bool ResetRecentResults { get; init; }
    public required GameState.CompetitionRow[] CompetitionTable { get; init; }
    public required GameState.CompetitionFixture[] CompetitionFixtures { get; init; }
    public required string CurrentOpponentName { get; init; }
    public required string NextFixtureSummary { get; init; }
}
