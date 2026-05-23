using Godot;
using System;
using System.Collections.Generic;

public partial class LiveMatchScene : Control
{
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private const string PostMatchScenePath = "res://scenes/PostMatchScene.tscn";
    private const float SimulatedMinutesPerSecond = 6.0f;
    private const float MarkerSize = 30.0f;
    private const float BallSize = 14.0f;

    private readonly List<Button> _markerNodes = new();
    private readonly List<string> _markerPlayerIds = new();
    private MatchPlaybackResult? _playback;
    private PanelContainer? _ballNode;
    private float _elapsedSeconds;
    private int _appliedEventCount;
    private bool _matchComplete;

    private Label? _fixtureLabel;
    private Label? _scoreLabel;
    private Label? _clockLabel;
    private Label? _tacticalLabel;
    private Label? _momentumLabel;
    private Label? _statusLabel;
    private Label? _controlLabel;
    private Label? _eventFeedLabel;
    private Label? _homeTagLabel;
    private Label? _awayTagLabel;
    private Label? _pitchNoteLabel;
    private Control? _markersLayer;
    private Button? _backButton;

    public override void _Ready()
    {
        _fixtureLabel = GetNode<Label>("Margin/Root/BroadcastBar/BarPadding/BarContent/FixtureBlock/FixtureLabel");
        _scoreLabel = GetNode<Label>("Margin/Root/BroadcastBar/BarPadding/BarContent/ScoreBlock/ScoreLabel");
        _clockLabel = GetNode<Label>("Margin/Root/BroadcastBar/BarPadding/BarContent/ScoreBlock/ClockLabel");
        _tacticalLabel = GetNode<Label>("Margin/Root/BroadcastBar/BarPadding/BarContent/FixtureBlock/TacticalLabel");
        _momentumLabel = GetNode<Label>("Margin/Root/BroadcastBar/BarPadding/BarContent/MomentumLabel");
        _statusLabel = GetNode<Label>("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/StatusLabel");
        _controlLabel = GetNode<Label>("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/ControlLabel");
        _eventFeedLabel = GetNode<Label>("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/EventFeedLabel");
        _homeTagLabel = GetNode<Label>("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/PitchHeader/HomeTagLabel");
        _awayTagLabel = GetNode<Label>("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/PitchHeader/AwayTagLabel");
        _pitchNoteLabel = GetNode<Label>("Margin/Root/ContentRow/PitchColumn/PitchNoteLabel");
        _markersLayer = GetNode<Control>("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/MarkersLayer");
        _backButton = GetNode<Button>("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/BackButton");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState();
            return;
        }

        _playback = GameState.Instance.PrepareCurrentMatchResult();
        _fixtureLabel.Text = $"{_playback.HomeClubName} vs {_playback.AwayClubName}";
        _scoreLabel.Text = "0 - 0";
        _clockLabel.Text = "01'";
        _tacticalLabel.Text = _playback.TacticalSummary;
        _momentumLabel.Text = "Possession: opening restart";
        _statusLabel.Text = "Playback model loaded. Rendering engine frames.";
        _controlLabel.Text = "Action: kickoff";
        _eventFeedLabel.Text = "1' Kick-off.";
        _homeTagLabel.Text = _playback.HomeClubName;
        _awayTagLabel.Text = _playback.AwayClubName;
        _pitchNoteLabel.Text = "The pitch now renders frame-based player and ball state from the match engine.";

        CreateMarkers();
        RenderPlaybackSecond(0);
        SetProcess(true);
    }

    public override void _Process(double delta)
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        _elapsedSeconds += (float)delta;
        var simulatedSecond = Math.Min(
            _playback.Timeline.DurationSeconds,
            (int)MathF.Floor(_elapsedSeconds * SimulatedMinutesPerSecond * 60.0f));
        RenderPlaybackSecond(simulatedSecond);

        if (simulatedSecond >= _playback.Timeline.DurationSeconds)
        {
            FinalizeMatch();
            SetProcess(false);
        }
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(_matchComplete ? PostMatchScenePath : MatchdayScenePath);
    }

    private void RenderUnavailableState()
    {
        _fixtureLabel!.Text = "Fixture unavailable";
        _scoreLabel!.Text = "0 - 0";
        _clockLabel!.Text = "01'";
        _tacticalLabel!.Text = "Tactics unavailable";
        _momentumLabel!.Text = "Possession unavailable";
        _statusLabel!.Text = "Live context unavailable.";
        _controlLabel!.Text = "Action unavailable.";
        _eventFeedLabel!.Text = "No live events yet.";
        _homeTagLabel!.Text = "HOME";
        _awayTagLabel!.Text = "AWAY";
        _pitchNoteLabel!.Text = "Pitch presentation unavailable.";
    }

    private void CreateMarkers()
    {
        if (_playback == null || _markersLayer == null || _playback.Timeline.Frames.Length == 0)
        {
            return;
        }

        var initialFrame = _playback.Timeline.Frames[0];
        foreach (var player in initialFrame.PlayerStates)
        {
            var node = new Button
            {
                Text = BuildInitials(player.Name),
                Disabled = true,
                FocusMode = FocusModeEnum.None,
                MouseFilter = MouseFilterEnum.Ignore,
                CustomMinimumSize = new Vector2(MarkerSize, MarkerSize),
                TooltipText = $"{player.Name} | {player.CurrentIntent}"
            };

            node.AddThemeColorOverride("font_disabled_color", Colors.White);
            var markerStyle = BuildMarkerStyle(player.Team == _playback.HomeClubName);
            node.AddThemeStyleboxOverride("disabled", markerStyle);
            node.AddThemeStyleboxOverride("normal", markerStyle);
            node.AddThemeStyleboxOverride("hover", markerStyle);
            node.AddThemeStyleboxOverride("pressed", markerStyle);

            _markersLayer.AddChild(node);
            _markerNodes.Add(node);
            _markerPlayerIds.Add(player.PlayerId);
        }

        _ballNode = new PanelContainer
        {
            CustomMinimumSize = new Vector2(BallSize, BallSize),
            MouseFilter = MouseFilterEnum.Ignore
        };
        _ballNode.AddThemeStyleboxOverride("panel", BuildBallStyle());
        _markersLayer.AddChild(_ballNode);
    }

    private void RenderPlaybackSecond(int simulatedSecond)
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        var (currentFrame, nextFrame, progress) = ResolveFramePair(simulatedSecond);
        _scoreLabel!.Text = $"{currentFrame.HomeScore} - {currentFrame.AwayScore}";
        _clockLabel!.Text = $"{currentFrame.Minute:00}'";
        _statusLabel!.Text = currentFrame.EventSummary ?? currentFrame.CurrentActionLabel;
        _controlLabel!.Text = $"Action: {currentFrame.CurrentActionLabel}";
        _momentumLabel!.Text = $"Possession: {currentFrame.PossessionTeam}";
        _pitchNoteLabel!.Text =
            $"Ball: {currentFrame.Ball.MovementState} | Carrier: {ResolveCarrierLabel(currentFrame)}";
        UpdateEventFeed(simulatedSecond);
        UpdateMarkerPositions(currentFrame, nextFrame, progress);
        UpdateBallPosition(currentFrame, nextFrame, progress);
    }

    private (MatchFrame currentFrame, MatchFrame nextFrame, float progress) ResolveFramePair(int simulatedSecond)
    {
        if (_playback == null || _playback.Timeline.Frames.Length == 0)
        {
            throw new InvalidOperationException("Playback frames are unavailable.");
        }

        var frames = _playback.Timeline.Frames;
        for (var index = 0; index < frames.Length - 1; index++)
        {
            var current = frames[index];
            var next = frames[index + 1];
            if (simulatedSecond >= current.MatchSecond && simulatedSecond <= next.MatchSecond)
            {
                var duration = Math.Max(1, next.MatchSecond - current.MatchSecond);
                return (current, next, Math.Clamp((simulatedSecond - current.MatchSecond) / (float)duration, 0.0f, 1.0f));
            }
        }

        return (frames[^1], frames[^1], 1.0f);
    }

    private void UpdateMarkerPositions(MatchFrame currentFrame, MatchFrame nextFrame, float progress)
    {
        if (_markersLayer == null)
        {
            return;
        }

        var size = _markersLayer.Size;
        for (var index = 0; index < _markerNodes.Count; index++)
        {
            var playerId = _markerPlayerIds[index];
            var currentState = FindPlayerState(currentFrame, playerId);
            var nextState = FindPlayerState(nextFrame, playerId) ?? currentState;
            if (currentState == null)
            {
                continue;
            }

            var position = nextState == null
                ? currentState.Position
                : currentState.Position.Lerp(nextState.Position, progress);
            _markerNodes[index].Position = new Vector2(
                size.X * position.X - MarkerSize * 0.5f,
                size.Y * position.Y - MarkerSize * 0.5f);
            _markerNodes[index].TooltipText = $"{currentState.Name} | {currentState.CurrentIntent}";
        }
    }

    private void UpdateBallPosition(MatchFrame currentFrame, MatchFrame nextFrame, float progress)
    {
        if (_markersLayer == null || _ballNode == null)
        {
            return;
        }

        var size = _markersLayer.Size;
        var ballPosition = currentFrame.Ball.Position.Lerp(nextFrame.Ball.Position, progress);
        _ballNode.Position = new Vector2(
            size.X * ballPosition.X - BallSize * 0.5f,
            size.Y * ballPosition.Y - BallSize * 0.5f);
    }

    private void UpdateEventFeed(int simulatedSecond)
    {
        if (_playback == null)
        {
            return;
        }

        while (_appliedEventCount < _playback.EventFeed.Length && _playback.EventFeed[_appliedEventCount].MatchSecond <= simulatedSecond)
        {
            _appliedEventCount++;
        }

        var startIndex = Math.Max(0, _appliedEventCount - 5);
        var feedLines = new List<string>();
        for (var index = startIndex; index < _appliedEventCount; index++)
        {
            feedLines.Add(_playback.EventFeed[index].Summary);
        }

        if (feedLines.Count == 0)
        {
            feedLines.Add("Play is about to begin.");
        }

        _eventFeedLabel!.Text = string.Join("\n", feedLines);
    }

    private string ResolveCarrierLabel(MatchFrame frame)
    {
        if (string.IsNullOrWhiteSpace(frame.Ball.CarrierPlayerId))
        {
            return "none";
        }

        var carrier = FindPlayerState(frame, frame.Ball.CarrierPlayerId);
        return carrier?.Name ?? frame.Ball.CarrierPlayerId;
    }

    private static PlayerAgentState? FindPlayerState(MatchFrame frame, string? playerId)
    {
        if (string.IsNullOrWhiteSpace(playerId))
        {
            return null;
        }

        foreach (var player in frame.PlayerStates)
        {
            if (player.PlayerId == playerId)
            {
                return player;
            }
        }

        return null;
    }

    private static StyleBoxFlat BuildMarkerStyle(bool isHome)
    {
        return new StyleBoxFlat
        {
            BgColor = isHome ? new Color(0.129f, 0.424f, 0.690f) : new Color(0.698f, 0.204f, 0.251f),
            CornerRadiusTopLeft = 15,
            CornerRadiusTopRight = 15,
            CornerRadiusBottomRight = 15,
            CornerRadiusBottomLeft = 15,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            BorderColor = new Color(0.95f, 0.95f, 0.95f)
        };
    }

    private static StyleBoxFlat BuildBallStyle()
    {
        return new StyleBoxFlat
        {
            BgColor = new Color(0.98f, 0.92f, 0.35f),
            CornerRadiusTopLeft = 7,
            CornerRadiusTopRight = 7,
            CornerRadiusBottomRight = 7,
            CornerRadiusBottomLeft = 7,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            BorderColor = new Color(0.08f, 0.08f, 0.08f)
        };
    }

    private static string BuildInitials(string fullName)
    {
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return "P";
        }

        if (parts.Length == 1)
        {
            return parts[0][0].ToString().ToUpperInvariant();
        }

        return string.Concat(parts[0][0], parts[^1][0]).ToUpperInvariant();
    }

    private void FinalizeMatch()
    {
        if (_matchComplete || _playback == null)
        {
            return;
        }

        GameState.Instance?.ApplyMatchResult(_playback);
        _statusLabel!.Text = "Full time. Review the result and consequence deltas in post-match.";
        _controlLabel!.Text = "Action: full time";
        _momentumLabel!.Text = "Possession: full time";
        _pitchNoteLabel!.Text = "Playback complete. Continue to the post-match screen for the aftermath.";
        _backButton!.Text = "Continue to Post-Match";
        _matchComplete = true;
    }
}
