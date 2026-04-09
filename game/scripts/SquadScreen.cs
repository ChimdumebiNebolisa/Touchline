using Godot;
using System;
using System.Collections.Generic;

public partial class SquadScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _clubContextLabel = default!;
    private OptionButton _positionFilter = default!;
    private ItemList _playerList = default!;
    private Label _playerDetailLabel = default!;
    private readonly List<int> _visiblePlayerIndexes = new();

    public override void _Ready()
    {
        _clubContextLabel = GetNode<Label>("Center/Panel/ClubContextLabel");
        _positionFilter = GetNode<OptionButton>("Center/Panel/PositionFilter");
        _playerList = GetNode<ItemList>("Center/Panel/PlayerList");
        _playerDetailLabel = GetNode<Label>("Center/Panel/PlayerDetailLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _clubContextLabel.Text = "Club context unavailable.";
            _playerDetailLabel.Text = "Select a player to inspect details.";
            return;
        }

        _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} squad";

        PopulatePlayerList((int)_positionFilter.GetSelectedId());
    }

    private void OnFilterSelected(long index)
    {
        var selectedId = _positionFilter.GetItemId((int)index);
        PopulatePlayerList(selectedId);
    }

    private void PopulatePlayerList(int filterId)
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _playerList.Clear();
        _visiblePlayerIndexes.Clear();

        for (var i = 0; i < GameState.Instance.SquadPlayers.Length; i++)
        {
            var player = GameState.Instance.SquadPlayers[i];
            if (!MatchesFilter(player.Position, filterId))
            {
                continue;
            }

            _visiblePlayerIndexes.Add(i);
            var marker = player.IsStarting ? "[XI]" : "[B]";
            _playerList.AddItem($"{marker} {player.Name} - {player.Position}");
        }

        if (_playerList.ItemCount > 0)
        {
            _playerList.Select(0);
            RenderPlayerDetailByVisibleIndex(0);
        }
        else
        {
            _playerDetailLabel.Text = "No players match this filter.";
        }
    }

    private void OnPlayerSelected(long index)
    {
        RenderPlayerDetailByVisibleIndex((int)index);
    }

    private void RenderPlayerDetailByVisibleIndex(int visibleIndex)
    {
        if (GameState.Instance == null || visibleIndex < 0 || visibleIndex >= _visiblePlayerIndexes.Count)
        {
            _playerDetailLabel.Text = "Player details unavailable.";
            return;
        }

        var index = _visiblePlayerIndexes[visibleIndex];
        var player = GameState.Instance.SquadPlayers[index];
        var lineupTag = player.IsStarting ? "Starting XI" : "Bench";
        _playerDetailLabel.Text =
            $"{player.Name} | {player.Position} | {lineupTag} | Age {player.Age} | Form {player.Form} | Morale {player.Morale} | Fitness {player.Fitness}";
    }

    private static bool MatchesFilter(string position, int filterId)
    {
        return filterId switch
        {
            0 => true,
            1 => position == "GK",
            2 => position is "RB" or "CB" or "LB",
            3 => position is "CM" or "AM",
            4 => position is "RW" or "LW" or "ST",
            _ => true
        };
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
