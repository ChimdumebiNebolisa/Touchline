using Godot;

public partial class ClubDashboard : Control
{
    private const string ChooseClubScenePath = "res://scenes/ChooseClub.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private Label? _stubMessageLabel;

    public override void _Ready()
    {
        var contextLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ClubContextLabel");
        var dateLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/DateLabel");
        var fixturePreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/SnapshotCard/SnapshotPadding/SnapshotContent/FixturePreviewLabel");
        var squadStatusLabel = GetNode<Label>("Center/Shell/Padding/Content/SnapshotCard/SnapshotPadding/SnapshotContent/SquadStatusLabel");
        _stubMessageLabel = GetNode<Label>("Center/Shell/Padding/Content/SnapshotCard/SnapshotPadding/SnapshotContent/StubMessage");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            contextLabel.Text = "Career context is unavailable.";
            dateLabel.Text = "Season and date unavailable.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            _stubMessageLabel.Text = "Dashboard context is unavailable.";
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            contextLabel.Text = "Club selection is missing.";
            dateLabel.Text = "Season and date unavailable.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            _stubMessageLabel.Text = "Choose a club before using the dashboard.";
            return;
        }

        contextLabel.Text =
            $"Manager {GameState.Instance.ManagerName} is now leading {GameState.Instance.SelectedClubName}.";
        dateLabel.Text = $"Season {GameState.Instance.SeasonLabel} | {GameState.Instance.CurrentDateLabel}";
        fixturePreviewLabel.Text = $"Next fixture: {GameState.Instance.NextFixtureSummary}";
        squadStatusLabel.Text = $"Squad: {GameState.Instance.SquadStatusSummary}";
        _stubMessageLabel.Text = GameState.Instance.LastMatchReport == null
            ? $"{GameState.Instance.FormSummary} The dashboard tracks club rhythm and the next matchday."
            : $"Last result: {GameState.Instance.LastMatchReport.Scoreline}. {GameState.Instance.LastMatchReport.ConsequenceSummary}. {GameState.Instance.FormSummary}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ChooseClubScenePath);
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
        if (_stubMessageLabel == null)
        {
            return;
        }

        if (SaveSystem.Instance == null)
        {
            _stubMessageLabel.Text = "Save system unavailable.";
            return;
        }

        SaveSystem.Instance.SaveGame(out var statusMessage);
        _stubMessageLabel.Text = statusMessage;
    }
}
