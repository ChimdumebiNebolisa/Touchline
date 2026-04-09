using Godot;

public partial class ClubDashboard : Control
{
    private const string ChooseClubScenePath = "res://scenes/ChooseClub.tscn";

    public override void _Ready()
    {
        var contextLabel = GetNode<Label>("Center/Panel/ClubContextLabel");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            contextLabel.Text = "Career context is unavailable.";
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            contextLabel.Text = "Club selection is missing.";
            return;
        }

        contextLabel.Text =
            $"Manager {GameState.Instance.ManagerName} is now leading {GameState.Instance.SelectedClubName}.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ChooseClubScenePath);
    }
}
