using Godot;

public partial class SaveLoadScene : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    public override void _Ready()
    {
        var slotSummaryLabel = GetNode<Label>("Center/Panel/Padding/Content/SlotSummaryLabel");
        var statusLabel = GetNode<Label>("Center/Panel/Padding/Content/StatusLabel");
        var loadButton = GetNode<Button>("Center/Panel/Padding/Content/LoadButton");

        if (SaveSystem.Instance == null)
        {
            slotSummaryLabel.Text = "Save system unavailable.";
            statusLabel.Text = "Load is unavailable until the save singleton is active.";
            loadButton.Disabled = true;
            return;
        }

        slotSummaryLabel.Text = SaveSystem.Instance.GetSlotSummary();
        statusLabel.Text = SaveSystem.Instance.HasSaveFile()
            ? "Load the local slot to continue your career."
            : "No local save found yet. Start a career and save from the dashboard.";
        loadButton.Disabled = !SaveSystem.Instance.HasSaveFile();
    }

    private void OnLoadPressed()
    {
        var statusLabel = GetNode<Label>("Center/Panel/Padding/Content/StatusLabel");

        if (SaveSystem.Instance == null)
        {
            statusLabel.Text = "Save system unavailable.";
            return;
        }

        if (SaveSystem.Instance.LoadGame(out var statusMessage))
        {
            GetTree().ChangeSceneToFile(ClubDashboardScenePath);
            return;
        }

        statusLabel.Text = statusMessage;
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
