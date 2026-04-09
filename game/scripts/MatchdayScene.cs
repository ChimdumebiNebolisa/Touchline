using Godot;

public partial class MatchdayScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string LiveMatchScenePath = "res://scenes/LiveMatchScene.tscn";

    public override void _Ready()
    {
        var competitionLabel = GetNode<Label>("Center/Panel/CompetitionLabel");
        var fixtureLabel = GetNode<Label>("Center/Panel/FixtureLabel");
        var lineupLabel = GetNode<Label>("Center/Panel/LineupLabel");
        var tacticsLabel = GetNode<Label>("Center/Panel/TacticsLabel");
        var statusLabel = GetNode<Label>("Center/Panel/StatusLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            competitionLabel.Text = "Competition: unavailable";
            fixtureLabel.Text = "Fixture: unavailable";
            lineupLabel.Text = "Starting lineup ready: unavailable";
            tacticsLabel.Text = "Tactics: unavailable";
            statusLabel.Text = "Set up a career and club before entering matchday.";
            return;
        }

        var startingCount = 0;
        foreach (var player in GameState.Instance.SquadPlayers)
        {
            if (player.IsStarting)
            {
                startingCount++;
            }
        }

        competitionLabel.Text = $"Competition: {GameState.Instance.CompetitionName}";
        fixtureLabel.Text =
            $"Fixture: {GameState.Instance.SelectedClubName} vs {GameState.Instance.CurrentOpponentName} (Matchday {GameState.Instance.CurrentMatchday})";
        lineupLabel.Text = $"Starting lineup ready: {startingCount} players marked in XI";
        tacticsLabel.Text =
            $"Tactics: {GameState.Instance.TacticalFormation} | Press {GameState.Instance.PressIntensity} | Tempo {GameState.Instance.Tempo} | Width {GameState.Instance.Width} | Risk {GameState.Instance.Risk}";
    }

    private void OnStartMatchPressed()
    {
        GetTree().ChangeSceneToFile(LiveMatchScenePath);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
