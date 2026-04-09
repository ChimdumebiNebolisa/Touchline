using Godot;

public partial class PlayerProfile : Control
{
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";

    public override void _Ready()
    {
        var headingLabel = GetNode<Label>("Center/Panel/Padding/Content/Heading");
        var clubContextLabel = GetNode<Label>("Center/Panel/Padding/Content/ClubContextLabel");
        var identityLabel = GetNode<Label>("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel");
        var roleLabel = GetNode<Label>("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/RoleLabel");
        var conditionLabel = GetNode<Label>("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/ConditionLabel");
        var pathwayLabel = GetNode<Label>("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/PathwayLabel");
        var statusLabel = GetNode<Label>("Center/Panel/Padding/Content/StatusLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            headingLabel.Text = "Player Profile";
            clubContextLabel.Text = "Club context unavailable.";
            identityLabel.Text = "Identity unavailable.";
            roleLabel.Text = "Role unavailable.";
            conditionLabel.Text = "Condition unavailable.";
            pathwayLabel.Text = "Trajectory unavailable.";
            statusLabel.Text = "Return to the squad screen and select a player first.";
            return;
        }

        var player = GameState.Instance.GetSelectedPlayerProfile();
        if (player == null)
        {
            headingLabel.Text = "Player Profile";
            clubContextLabel.Text = GameState.Instance.SelectedClubName;
            identityLabel.Text = "Identity unavailable.";
            roleLabel.Text = "Role unavailable.";
            conditionLabel.Text = "Condition unavailable.";
            pathwayLabel.Text = "Trajectory unavailable.";
            statusLabel.Text = "No player is currently selected for inspection.";
            return;
        }

        var lineupStatus = player.IsStarting ? "Starting XI" : "Bench option";
        headingLabel.Text = player.Name;
        clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} | {lineupStatus}";
        identityLabel.Text = $"Identity: {player.Position} | Age {player.Age}";
        roleLabel.Text = $"Current role: {lineupStatus} with form {player.Form} and morale {player.Morale}.";
        conditionLabel.Text = $"Condition: fitness {player.Fitness} | morale {player.Morale} | form {player.Form}";
        pathwayLabel.Text = BuildTrajectorySummary(player);
        statusLabel.Text = "Use this view to inspect the player before returning to squad decisions.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(SquadScreenScenePath);
    }

    private static string BuildTrajectorySummary(GameState.SquadPlayer player)
    {
        if (player.Age <= 21)
        {
            return "Trajectory: early-career player with room to grow if minutes and confidence stay high.";
        }

        if (player.Age >= 29)
        {
            return "Trajectory: experienced player whose short-term reliability matters as much as long-term planning.";
        }

        return "Trajectory: core squad player in the prime development window for current-season impact.";
    }
}
