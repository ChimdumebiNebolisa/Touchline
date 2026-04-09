using Godot;

public partial class CareerSetup : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
