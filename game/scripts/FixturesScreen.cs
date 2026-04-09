using Godot;

public partial class FixturesScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    public override void _Ready()
    {
        var seasonLabel = GetNode<Label>("Center/Panel/SeasonLabel");
        var dateLabel = GetNode<Label>("Center/Panel/DateLabel");
        var nextFixtureLabel = GetNode<Label>("Center/Panel/NextFixtureLabel");
        var formLabel = GetNode<Label>("Center/Panel/FormLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            seasonLabel.Text = "Season unavailable";
            dateLabel.Text = "Date unavailable";
            nextFixtureLabel.Text = "Next fixture unavailable";
            formLabel.Text = "Form unavailable";
            return;
        }

        seasonLabel.Text = $"Season {GameState.Instance.SeasonLabel}";
        dateLabel.Text = GameState.Instance.CurrentDateLabel;
        nextFixtureLabel.Text = GameState.Instance.NextFixtureSummary;
        formLabel.Text = GameState.Instance.FormSummary;
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
