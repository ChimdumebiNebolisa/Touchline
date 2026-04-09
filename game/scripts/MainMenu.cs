using Godot;

public partial class MainMenu : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string SaveLoadScenePath = "res://scenes/SaveLoadScene.tscn";

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
