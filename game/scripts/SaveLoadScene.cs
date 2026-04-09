using Godot;

public partial class SaveLoadScene : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    public override void _Ready()
    {
        var slotSummaryLabel = GetNode<Label>("Center/Panel/Padding/Content/SlotCard/SlotPadding/SlotContent/SlotSummaryLabel");
        var statusLabel = GetNode<Label>("Center/Panel/Padding/Content/SlotCard/SlotPadding/SlotContent/StatusLabel");
        var loadButton = GetNode<Button>("Center/Panel/Padding/Content/LoadButton");

        if (SaveSystem.Instance == null)
        {
            slotSummaryLabel.Text = "Save system unavailable.";
            statusLabel.Text = "Load is unavailable until the save singleton is active.";
            loadButton.Text = "Continue Career";
            loadButton.Disabled = true;
            return;
        }

        slotSummaryLabel.Text = SaveSystem.Instance.GetSlotSummary();
        statusLabel.Text = SaveSystem.Instance.HasSaveFile()
            ? "This career is ready to continue from the latest local save."
            : "No local save found yet. Start a career and save from the dashboard.";
        loadButton.Text = "Continue Career";
        loadButton.Disabled = !SaveSystem.Instance.HasSaveFile();
    }

    private void OnLoadPressed()
    {
        var statusLabel = GetNode<Label>("Center/Panel/Padding/Content/SlotCard/SlotPadding/SlotContent/StatusLabel");

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
