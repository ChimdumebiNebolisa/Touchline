using Godot;

public partial class SaveLoadScene : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
