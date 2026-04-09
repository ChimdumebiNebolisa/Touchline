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

        if (GameState.Instance.WorldSeed <= 0)
        {
            summaryLabel.Text = "Career world seed context is missing.";
            return;
        }

        summaryLabel.Text =
            $"Manager: {GameState.Instance.ManagerName} | Seed: {GameState.Instance.WorldSeed} | Pack: {GameState.Instance.CountryPackId}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }
}
