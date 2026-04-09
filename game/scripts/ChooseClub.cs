using Godot;

public partial class ChooseClub : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";

    public override void _Ready()
    {
        var summaryLabel = GetNode<Label>("Center/Panel/CareerSummaryLabel");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            summaryLabel.Text = "Career setup is incomplete.";
            return;
        }

        summaryLabel.Text =
            $"Manager: {GameState.Instance.ManagerName} | Seed: {GameState.Instance.CareerSeed}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }
}
