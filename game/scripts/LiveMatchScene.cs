using Godot;

public partial class LiveMatchScene : Control
{
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    public override void _Ready()
    {
        var contextLabel = GetNode<Label>("Center/Panel/ContextLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            contextLabel.Text = "Live context unavailable.";
            return;
        }

        contextLabel.Text =
            $"Live feed: {GameState.Instance.SelectedClubName} vs {GameState.Instance.CurrentOpponentName}.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
    }
}
