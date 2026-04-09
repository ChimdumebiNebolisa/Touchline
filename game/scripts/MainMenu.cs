using Godot;

public partial class MainMenu : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string SaveLoadScenePath = "res://scenes/SaveLoadScene.tscn";

    public override void _Ready()
    {
        var resumeSummaryLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeSummaryLabel");
        var resumeStatusLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeStatusLabel");
        var loadButton = GetNode<Button>("Center/MenuCard/Padding/Menu/LoadGameButton");

        if (SaveSystem.Instance == null)
        {
            resumeSummaryLabel.Text = "Save system unavailable.";
            resumeStatusLabel.Text = "Resume flow is offline until the save singleton is active.";
            loadButton.Disabled = true;
            return;
        }

        if (!SaveSystem.Instance.HasSaveFile())
        {
            resumeSummaryLabel.Text = "No saved career found.";
            resumeStatusLabel.Text = "Start a new touchline career to create your first persistent club journey.";
            loadButton.Text = "Load Game";
            loadButton.Disabled = true;
            return;
        }

        resumeSummaryLabel.Text = SaveSystem.Instance.GetSlotSummary();
        resumeStatusLabel.Text = "A local career is ready to continue from the last saved checkpoint.";
        loadButton.Text = "Continue Career";
        loadButton.Disabled = false;
    }

    private void OnNewCareerPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }

    private void OnLoadGamePressed()
    {
        GetTree().ChangeSceneToFile(SaveLoadScenePath);
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}
