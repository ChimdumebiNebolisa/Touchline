using Godot;
using System;

public partial class TouchlineCalendarSystem : Node
{
    private static readonly DateTime DefaultSeasonStartDate = new(2026, 8, 3);

    public static TouchlineCalendarSystem? Instance { get; private set; }

    public string LastStatusMessage { get; private set; } = "Calendar idle.";

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

    public bool AdvanceCareerDate()
    {
        if (GameState.Instance == null)
        {
            LastStatusMessage = "GameState singleton is unavailable.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            LastStatusMessage = "No club is selected for calendar progression.";
            return false;
        }

        var nextDate = GameState.Instance.CurrentDate.AddDays(7);
        var nextMatchday = GameState.Instance.CurrentMatchday + 1;
        var nextSeasonStartYear = GameState.Instance.SeasonStartYear;
        var formSummary = GameState.Instance.FormSummary;
        var resetRecentResults = false;
        var competitionTable = GameState.Instance.CompetitionTable;
        var competitionFixtures = GameState.Instance.CompetitionFixtures;
        var seasonLength = CompetitionRuntimeService.GetSeasonMatchdayCount(GameState.Instance.CompetitionFixtures);

        if (nextMatchday > seasonLength)
        {
            nextSeasonStartYear++;
            nextDate = new DateTime(nextSeasonStartYear, DefaultSeasonStartDate.Month, DefaultSeasonStartDate.Day);
            nextMatchday = 1;
            formSummary = "Form: new season reset.";
            resetRecentResults = true;

            var newCompetitionState = CompetitionRuntimeService.BuildInitialState(
                GameState.Instance.AvailableClubs,
                GameState.Instance.SelectedClubName);
            competitionTable = newCompetitionState.table;
            competitionFixtures = newCompetitionState.fixtures;
        }

        var fixtureContext = CompetitionRuntimeService.ResolveFixtureContext(
            competitionFixtures,
            nextMatchday,
            GameState.Instance.SelectedClubName,
            nextDate.ToString("ddd d MMM yyyy"));

        GameState.Instance.ApplyCalendarAdvance(
            new CalendarAdvanceState
            {
                CurrentDate = nextDate,
                SeasonStartYear = nextSeasonStartYear,
                CurrentMatchday = nextMatchday,
                FormSummary = formSummary,
                ResetRecentResults = resetRecentResults,
                CompetitionTable = competitionTable,
                CompetitionFixtures = competitionFixtures,
                CurrentOpponentName = fixtureContext.currentOpponentName,
                NextFixtureSummary = fixtureContext.nextFixtureSummary
            });

        LastStatusMessage = resetRecentResults
            ? $"Season rolled into {GameState.Instance.SeasonLabel}."
            : $"Calendar advanced to {GameState.Instance.CurrentDateLabel}.";
        return true;
    }
}
