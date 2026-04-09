using Godot;

public partial class CareerSetup : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string ChooseClubScenePath = "res://scenes/ChooseClub.tscn";

    private LineEdit _managerNameInput = default!;
    private SpinBox _seedInput = default!;
    private Label _statusLabel = default!;

    public override void _Ready()
    {
        _managerNameInput = GetNode<LineEdit>("Center/Panel/ManagerNameInput");
        _seedInput = GetNode<SpinBox>("Center/Panel/SeedInput");
        _statusLabel = GetNode<Label>("Center/Panel/StatusLabel");
    }

    private void OnStartCareerPressed()
    {
        var managerName = _managerNameInput.Text.StripEdges();
        if (managerName.Length == 0)
        {
            managerName = "Manager";
        }

        var seed = (int)_seedInput.Value;

        if (GameState.Instance == null)
        {
            _statusLabel.Text = "GameState singleton is unavailable.";
            return;
        }

        GameState.Instance.StartNewCareer(managerName, seed);
        _statusLabel.Text = $"Career initialized for {managerName} (Seed {seed}).";
        GetTree().ChangeSceneToFile(ChooseClubScenePath);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
