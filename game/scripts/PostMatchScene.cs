using Godot;

public partial class PostMatchScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    public override void _Ready()
    {
        var fixtureLabel = GetNode<Label>("Center/Panel/FixtureLabel");
        var scoreLabel = GetNode<Label>("Center/Panel/ScoreLabel");
        var resultLabel = GetNode<Label>("Center/Panel/ResultLabel");
        var deltasLabel = GetNode<Label>("Center/Panel/DeltasLabel");
        var eventsLabel = GetNode<Label>("Center/Panel/EventsLabel");

        if (GameState.Instance?.LastMatchReport == null)
        {
            fixtureLabel.Text = "Post-match context unavailable";
            scoreLabel.Text = "0 - 0";
            resultLabel.Text = "No completed result is ready to review.";
            deltasLabel.Text = "Morale +0 | Fans +0 | Board +0";
            eventsLabel.Text = "No key events recorded.";
            return;
        }

        var report = GameState.Instance.LastMatchReport;
        fixtureLabel.Text = report.FixtureLabel;
        scoreLabel.Text = report.Scoreline;
        resultLabel.Text = report.ResultLabel;
        deltasLabel.Text = report.ConsequenceSummary;
        eventsLabel.Text = string.Join("\n", report.KeyEvents);
    }

    private void OnContinuePressed()
    {
        GameState.Instance?.AdvanceDate();
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
