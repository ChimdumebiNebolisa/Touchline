using Godot;

public partial class SquadScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _clubContextLabel = default!;
    private ItemList _playerList = default!;
    private Label _playerDetailLabel = default!;

    public override void _Ready()
    {
        _clubContextLabel = GetNode<Label>("Center/Panel/ClubContextLabel");
        _playerList = GetNode<ItemList>("Center/Panel/PlayerList");
        _playerDetailLabel = GetNode<Label>("Center/Panel/PlayerDetailLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _clubContextLabel.Text = "Club context unavailable.";
            _playerDetailLabel.Text = "Select a player to inspect details.";
            return;
        }

        _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} squad";

        _playerList.Clear();
        for (var i = 0; i < GameState.Instance.SquadPlayers.Length; i++)
        {
            var player = GameState.Instance.SquadPlayers[i];
            _playerList.AddItem($"{player.Name} - {player.Position}");
        }

        if (_playerList.ItemCount > 0)
        {
            _playerList.Select(0);
            RenderPlayerDetail(0);
        }
        else
        {
            _playerDetailLabel.Text = "No players are currently available.";
        }
    }

    private void OnPlayerSelected(long index)
    {
        RenderPlayerDetail((int)index);
    }

    private void RenderPlayerDetail(int index)
    {
        if (GameState.Instance == null || index < 0 || index >= GameState.Instance.SquadPlayers.Length)
        {
            _playerDetailLabel.Text = "Player details unavailable.";
            return;
        }

        var player = GameState.Instance.SquadPlayers[index];
        _playerDetailLabel.Text =
            $"{player.Name} | {player.Position} | Age {player.Age} | Morale {player.Morale} | Fitness {player.Fitness}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
