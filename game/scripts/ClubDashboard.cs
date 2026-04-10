using Godot;

public partial class ClubDashboard : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private Label? _statusLabel;
    private Label? _saveHintLabel;

    public override void _Ready()
    {
        var contextLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ClubContextLabel");
        var dateLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/DateLabel");
        var fixturePreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/FixturePreviewLabel");
        var squadStatusLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/SquadStatusLabel");
        var formLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/FormLabel");
        var pressureLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/PressureLabel");
        var pressureReasonsLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/PressureReasonsLabel");
        var tacticsSummaryLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/TacticsSummaryLabel");
        var priorityLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/PriorityLabel");
        _statusLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/StatusLabel");
        _saveHintLabel = GetNode<Label>("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/SaveHintLabel");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            contextLabel.Text = "Career context is unavailable.";
            dateLabel.Text = "Season and date unavailable.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            formLabel.Text = "Form unavailable.";
            pressureLabel.Text = "Pressure unavailable.";
            pressureReasonsLabel.Text = "Pressure reasons unavailable.";
            tacticsSummaryLabel.Text = "Tactical setup unavailable.";
            priorityLabel.Text = "Recommended next move unavailable.";
            _statusLabel.Text = "Dashboard context is unavailable.";
            _saveHintLabel.Text = "Save is unavailable until a career is active.";
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            contextLabel.Text = "Club selection is missing.";
            dateLabel.Text = "Season and date unavailable.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            formLabel.Text = "Form unavailable.";
            pressureLabel.Text = "Pressure unavailable.";
            pressureReasonsLabel.Text = "Pressure reasons unavailable.";
            tacticsSummaryLabel.Text = "Tactical setup unavailable.";
            priorityLabel.Text = "Recommended next move unavailable.";
            _statusLabel.Text = "Choose a club before using the dashboard.";
            _saveHintLabel.Text = "Save is unavailable until a club is selected.";
            return;
        }

        contextLabel.Text =
            $"Manager {GameState.Instance.ManagerName} is now leading {GameState.Instance.SelectedClubName}.";
        dateLabel.Text = $"Season {GameState.Instance.SeasonLabel} | {GameState.Instance.CurrentDateLabel}";
        fixturePreviewLabel.Text = $"Next fixture: {GameState.Instance.NextFixtureSummary}";
        squadStatusLabel.Text = $"Squad: {GameState.Instance.SquadStatusSummary}";
        formLabel.Text = GameState.Instance.FormSummary;
        pressureLabel.Text =
            $"Club pressure: morale {GameState.Instance.TeamMorale} | fans {GameState.Instance.FanSentiment} | board {GameState.Instance.BoardConfidence}";
        pressureReasonsLabel.Text = PerceptionSystem.BuildPressureReasonSummary(GameState.Instance);
        tacticsSummaryLabel.Text =
            $"Tactical setup: {GameState.Instance.TacticalFormation} | Press {GameState.Instance.PressIntensity} | Tempo {GameState.Instance.Tempo} | Width {GameState.Instance.Width} | Risk {GameState.Instance.Risk}";
        priorityLabel.Text = BuildPrioritySummary(GameState.Instance);
        _statusLabel.Text = GameState.Instance.LastMatchReport == null
            ? "No completed result yet. Use the hub to prepare for the opening run of fixtures."
            : $"Last result: {GameState.Instance.LastMatchReport.Scoreline}. {GameState.Instance.LastMatchReport.ConsequenceSummary}";
        _saveHintLabel.Text = SaveSystem.Instance == null
            ? "Save system unavailable."
            : "Save the current club state before leaving the session.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }

    private void OnSquadPressed()
    {
        GetTree().ChangeSceneToFile(SquadScreenScenePath);
    }

    private void OnTacticsPressed()
    {
        GetTree().ChangeSceneToFile(TacticsScreenScenePath);
    }

    private void OnFixturesPressed()
    {
        GetTree().ChangeSceneToFile(FixturesScreenScenePath);
    }

    private void OnStandingsPressed()
    {
        GetTree().ChangeSceneToFile(StandingsScreenScenePath);
    }

    private void OnMatchdayPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
    }

    private void OnSavePressed()
    {
        if (_statusLabel == null || _saveHintLabel == null)
        {
            return;
        }

        if (SaveSystem.Instance == null)
        {
            _statusLabel.Text = "Save system unavailable.";
            _saveHintLabel.Text = "Save system unavailable.";
            return;
        }

        SaveSystem.Instance.SaveGame(out var statusMessage);
        _statusLabel.Text = statusMessage;
        _saveHintLabel.Text = statusMessage;
    }

    private static string BuildPrioritySummary(GameState state)
    {
        if (state.BoardConfidence < 50)
        {
            return "Recommended next move: steady the board by tightening tactics and heading into the next fixture prepared.";
        }

        if (state.TeamMorale < 60)
        {
            return "Recommended next move: check the squad pulse and protect confidence before matchday.";
        }

        if (state.LastMatchReport == null)
        {
            return "Recommended next move: review squad and tactics, then launch into the opening fixture.";
        }

        return "Recommended next move: use fixtures, standings, and matchday to keep the season rhythm under control.";
    }
}
