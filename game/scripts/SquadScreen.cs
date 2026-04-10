using Godot;
using System;
using System.Collections.Generic;

public partial class SquadScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string PlayerProfileScenePath = "res://scenes/PlayerProfile.tscn";

    private Label _clubContextLabel = default!;
    private OptionButton _positionFilter = default!;
    private Label _lineupSummaryLabel = default!;
    private ItemList _playerList = default!;
    private Label _playerDetailLabel = default!;
    private Button _lineupActionButton = default!;
    private Label _lineupStatusLabel = default!;
    private Button _openProfileButton = default!;
    private readonly List<int> _visiblePlayerIndexes = new();
    private int _currentVisibleSelectionIndex = -1;

    public override void _Ready()
    {
        _clubContextLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ClubContextLabel");
        _positionFilter = GetNode<OptionButton>("Center/Shell/Padding/Content/BodyRow/SelectionCard/SelectionPadding/SelectionContent/PositionFilter");
        _lineupSummaryLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/LineupSummaryLabel");
        _playerList = GetNode<ItemList>("Center/Shell/Padding/Content/BodyRow/SelectionCard/SelectionPadding/SelectionContent/PlayerList");
        _playerDetailLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/DetailCard/DetailPadding/DetailContent/PlayerDetailLabel");
        _lineupActionButton = GetNode<Button>("Center/Shell/Padding/Content/BodyRow/DetailCard/DetailPadding/DetailContent/ActionsRow/LineupActionButton");
        _lineupStatusLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/DetailCard/DetailPadding/DetailContent/LineupStatusLabel");
        _openProfileButton = GetNode<Button>("Center/Shell/Padding/Content/BodyRow/DetailCard/DetailPadding/DetailContent/ActionsRow/OpenProfileButton");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _clubContextLabel.Text = "Club context unavailable.";
            _lineupSummaryLabel.Text = "Lineup summary unavailable.";
            _playerDetailLabel.Text = "Select a player to inspect the current role, condition, and pathway notes.";
            _lineupActionButton.Disabled = true;
            _lineupStatusLabel.Text = "Lineup changes unavailable.";
            _openProfileButton.Disabled = true;
            return;
        }

        _clubContextLabel.Text =
            $"{GameState.Instance.SelectedClubName} squad workspace | Next assignment: {GameState.Instance.CurrentOpponentName}";

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

        RenderLineupSummary();

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
            _playerDetailLabel.Text = "No players match this filter in the current squad view.";
            _lineupActionButton.Disabled = true;
            _openProfileButton.Disabled = true;
            _lineupStatusLabel.Text = "Adjust the filter to return to an actionable squad group.";
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
            _lineupActionButton.Disabled = true;
            _openProfileButton.Disabled = true;
            _currentVisibleSelectionIndex = -1;
            return;
        }

        _currentVisibleSelectionIndex = visibleIndex;
        var index = _visiblePlayerIndexes[visibleIndex];
        var player = GameState.Instance.SquadPlayers[index];
        var lineupTag = player.IsStarting ? "Starting XI" : "Bench";
        _playerDetailLabel.Text =
            $"{player.Name}\n{player.Position} | {lineupTag} | Age {player.Age}\nForm {player.Form} | Morale {player.Morale} | Fitness {player.Fitness}";
        _lineupActionButton.Disabled = false;
        _lineupActionButton.Text = player.IsStarting ? $"Move {player.Name} to Bench" : $"Promote {player.Name} to XI";
        _openProfileButton.Disabled = false;
        _openProfileButton.Text = $"Open {player.Name} Profile";
        _lineupStatusLabel.Text = player.IsStarting
            ? $"{player.Name} currently anchors the XI. Use the action row to rotate or inspect the full profile."
            : $"{player.Name} is currently a bench option. Use the action row to promote or inspect the full profile.";
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

    private void OnOpenProfilePressed()
    {
        if (GameState.Instance == null || _currentVisibleSelectionIndex < 0 || _currentVisibleSelectionIndex >= _visiblePlayerIndexes.Count)
        {
            return;
        }

        var index = _visiblePlayerIndexes[_currentVisibleSelectionIndex];
        GameState.Instance.SelectPlayerProfile(GameState.Instance.SquadPlayers[index].Name);
        GetTree().ChangeSceneToFile(PlayerProfileScenePath);
    }

    private void OnLineupActionPressed()
    {
        if (GameState.Instance == null || _currentVisibleSelectionIndex < 0 || _currentVisibleSelectionIndex >= _visiblePlayerIndexes.Count)
        {
            return;
        }

        var selectedIndex = _visiblePlayerIndexes[_currentVisibleSelectionIndex];
        var selectedPlayerName = GameState.Instance.SquadPlayers[selectedIndex].Name;
        _lineupStatusLabel.Text = GameState.Instance.TogglePlayerLineupStatus(selectedPlayerName);
        PopulatePlayerList((int)_positionFilter.GetSelectedId());

        for (var visibleIndex = 0; visibleIndex < _visiblePlayerIndexes.Count; visibleIndex++)
        {
            var playerIndex = _visiblePlayerIndexes[visibleIndex];
            if (GameState.Instance.SquadPlayers[playerIndex].Name == selectedPlayerName)
            {
                _playerList.Select(visibleIndex);
                RenderPlayerDetailByVisibleIndex(visibleIndex);
                return;
            }
        }
    }

    private void RenderLineupSummary()
    {
        if (GameState.Instance == null)
        {
            _lineupSummaryLabel.Text = "Lineup summary unavailable.";
            return;
        }

        var starters = 0;
        foreach (var player in GameState.Instance.SquadPlayers)
        {
            if (player.IsStarting)
            {
                starters++;
            }
        }

        var bench = GameState.Instance.SquadPlayers.Length - starters;
        _lineupSummaryLabel.Text =
            $"XI ready: {starters} starters | {bench} bench options. Every change here feeds straight into the next match preparation flow.";
    }
}
