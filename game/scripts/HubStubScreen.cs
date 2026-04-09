using Godot;

public partial class HubStubScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    [Export]
    public string ScreenTitle { get; set; } = "Stub Screen";

    public override void _Ready()
    {
        var heading = GetNode<Label>("Center/Panel/Heading");
        var message = GetNode<Label>("Center/Panel/Message");

        heading.Text = ScreenTitle;
        message.Text = $"{ScreenTitle} is stubbed for current active-step sequencing.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
