using Godot;

public partial class ClubDashboard : Control
{
    private const string ChooseClubScenePath = "res://scenes/ChooseClub.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    public override void _Ready()
    {
        var contextLabel = GetNode<Label>("Center/Panel/ClubContextLabel");
        var fixturePreviewLabel = GetNode<Label>("Center/Panel/FixturePreviewLabel");
        var squadStatusLabel = GetNode<Label>("Center/Panel/SquadStatusLabel");
        var stubMessageLabel = GetNode<Label>("Center/Panel/StubMessage");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            contextLabel.Text = "Career context is unavailable.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            stubMessageLabel.Text = "Dashboard context is unavailable.";
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            contextLabel.Text = "Club selection is missing.";
            fixturePreviewLabel.Text = "Next fixture: unavailable";
            squadStatusLabel.Text = "Squad status: unavailable";
            stubMessageLabel.Text = "Choose a club before using the dashboard.";
            return;
        }

        contextLabel.Text =
            $"Manager {GameState.Instance.ManagerName} is now leading {GameState.Instance.SelectedClubName}.";
        fixturePreviewLabel.Text = $"Next fixture: {GameState.Instance.NextFixtureSummary}";
        squadStatusLabel.Text = $"Squad: {GameState.Instance.SquadStatusSummary}";
        stubMessageLabel.Text = GameState.Instance.LastMatchReport == null
            ? "The dashboard tracks club rhythm, squad context, and the next matchday."
            : $"Last result: {GameState.Instance.LastMatchReport.Scoreline}. {GameState.Instance.LastMatchReport.ConsequenceSummary}.";
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
}
