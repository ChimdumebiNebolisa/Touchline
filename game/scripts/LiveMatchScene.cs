using Godot;
using System;
using System.Collections.Generic;

public partial class LiveMatchScene : Control
{
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private const string PostMatchScenePath = "res://scenes/PostMatchScene.tscn";
    private const float SimulatedMinutesPerSecond = 6.0f;
    private const float MarkerSize = 30.0f;
    private const float BallSize = 18.0f;
    private const float BallHaloSize = 34.0f;
    private const float ActionLineThickness = 5.0f;

    private readonly List<Button> _markerNodes = new();
    private readonly List<string> _markerPlayerIds = new();
    private MatchPlaybackResult? _playback;
    private ColorRect? _actionLineNode;
    private PanelContainer? _ballHaloNode;
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
    private Label? _pitchStateLabel;
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
        _pitchStateLabel = GetNode<Label>("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/PitchHeader/PitchStateLabel");
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
        _controlLabel.Text = "Action | kickoff\nPossession | opening restart\nBall | carried | Carrier | loading";
        _eventFeedLabel.Text = "1' Kick-off.";
        _homeTagLabel.Text = _playback.HomeClubName;
        _pitchStateLabel.Text = "Kickoff";
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
        _pitchStateLabel!.Text = "Unavailable";
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
        _actionLineNode = new ColorRect
        {
            Name = "PlaybackActionLine",
            MouseFilter = MouseFilterEnum.Ignore,
            Visible = false,
            Color = new Color(1.0f, 0.96f, 0.58f, 0.72f)
        };
        _markersLayer.AddChild(_actionLineNode);

        foreach (var player in initialFrame.PlayerStates)
        {
            var node = new Button
            {
                Name = $"Marker_{player.PlayerId.Replace("-", "_")}",
                Text = BuildInitials(player.Name),
                Disabled = true,
                FocusMode = FocusModeEnum.None,
                MouseFilter = MouseFilterEnum.Pass,
                CustomMinimumSize = new Vector2(MarkerSize, MarkerSize),
                TooltipText = BuildPlayerTooltip(player, initialFrame.Ball.CarrierPlayerId == player.PlayerId)
            };

            node.AddThemeColorOverride("font_disabled_color", Colors.White);
            node.AddThemeFontSizeOverride("font_size", 13);
            ApplyMarkerStyle(node, BuildMarkerStyle(player.Team == _playback.HomeClubName, player.CurrentIntent, player.HasBall));

            _markersLayer.AddChild(node);
            _markerNodes.Add(node);
            _markerPlayerIds.Add(player.PlayerId);
        }

        _ballHaloNode = new PanelContainer
        {
            Name = "PlaybackBallHalo",
            CustomMinimumSize = new Vector2(BallHaloSize, BallHaloSize),
            MouseFilter = MouseFilterEnum.Ignore
        };
        _ballHaloNode.AddThemeStyleboxOverride("panel", BuildBallHaloStyle(initialFrame.Ball.MovementState));
        _markersLayer.AddChild(_ballHaloNode);

        _ballNode = new PanelContainer
        {
            Name = "PlaybackBall",
            CustomMinimumSize = new Vector2(BallSize, BallSize),
            MouseFilter = MouseFilterEnum.Ignore
        };
        _ballNode.AddThemeStyleboxOverride("panel", BuildBallStyle(initialFrame.Ball.MovementState));
        _markersLayer.AddChild(_ballNode);
    }

    private void RenderPlaybackSecond(int simulatedSecond)
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        var (currentFrame, nextFrame, progress) = ResolveFramePair(simulatedSecond);
        var carrierLabel = ResolveCarrierLabel(currentFrame);
        var involvedLabel = ResolveInvolvedPlayerLabel(currentFrame);
        var hasEvent = !string.IsNullOrWhiteSpace(currentFrame.EventSummary);

        _scoreLabel!.Text = $"{currentFrame.HomeScore} - {currentFrame.AwayScore}";
        _clockLabel!.Text = $"{currentFrame.Minute:00}'";
        _statusLabel!.Text = currentFrame.EventSummary ?? currentFrame.CurrentActionLabel;
        _statusLabel.AddThemeColorOverride("font_color", hasEvent ? new Color(1.0f, 0.84f, 0.32f) : TouchlineTheme.TextPrimary);
        _controlLabel!.Text =
            $"Action | {currentFrame.CurrentActionLabel}\nPossession | {currentFrame.PossessionTeam}\nBall | {FormatBallMovement(currentFrame.Ball.MovementState)} | Carrier | {carrierLabel}\nFocus | {involvedLabel}";
        _momentumLabel!.Text = $"Possession\n{currentFrame.PossessionTeam}\nBall: {FormatBallMovement(currentFrame.Ball.MovementState)}";
        _pitchStateLabel!.Text = hasEvent ? "Key Moment" : FormatBallMovement(currentFrame.Ball.MovementState);
        _pitchStateLabel.AddThemeColorOverride("font_color", hasEvent ? new Color(1.0f, 0.84f, 0.32f) : TouchlineTheme.TextMuted);
        _pitchNoteLabel!.Text = hasEvent
            ? currentFrame.EventSummary!
            : $"Carrier: {carrierLabel} | Target: {FormatPitchPoint(currentFrame.Ball.TargetPosition)} | Focus: {involvedLabel}";

        UpdateEventFeed(simulatedSecond, currentFrame.EventId);
        UpdateMarkerPositions(currentFrame, nextFrame, progress);
        UpdateBallPosition(currentFrame, nextFrame, progress);
        UpdateActionLine(currentFrame, nextFrame, progress);
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
                var progress = Math.Clamp((simulatedSecond - current.MatchSecond) / (float)duration, 0.0f, 1.0f);
                return (current, next, SmoothStep(progress));
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
            var isCarrier = currentState.HasBall || currentFrame.Ball.CarrierPlayerId == currentState.PlayerId;
            var markerSize = ResolveMarkerSize(currentState.CurrentIntent, isCarrier);
            _markerNodes[index].CustomMinimumSize = new Vector2(markerSize, markerSize);
            _markerNodes[index].Size = new Vector2(markerSize, markerSize);
            _markerNodes[index].Position = new Vector2(
                size.X * position.X - markerSize * 0.5f,
                size.Y * position.Y - markerSize * 0.5f);
            _markerNodes[index].TooltipText = BuildPlayerTooltip(currentState, isCarrier);
            _markerNodes[index].AddThemeColorOverride("font_disabled_color", isCarrier ? new Color(0.044f, 0.051f, 0.047f) : Colors.White);
            _markerNodes[index].AddThemeFontSizeOverride("font_size", isCarrier ? 15 : 13);
            ApplyMarkerStyle(
                _markerNodes[index],
                BuildMarkerStyle(currentState.Team == _playback!.HomeClubName, currentState.CurrentIntent, isCarrier));
        }
    }

    private void UpdateBallPosition(MatchFrame currentFrame, MatchFrame nextFrame, float progress)
    {
        if (_markersLayer == null || _ballNode == null || _ballHaloNode == null)
        {
            return;
        }

        var size = _markersLayer.Size;
        var ballPosition = currentFrame.Ball.Position.Lerp(nextFrame.Ball.Position, progress);
        var liveBallSize = currentFrame.Ball.MovementState is BallMovementState.Shot or BallMovementState.Goal
            ? BallSize + 4.0f
            : BallSize;
        var liveHaloSize = currentFrame.Ball.MovementState is BallMovementState.Shot or BallMovementState.Goal
            ? BallHaloSize + 8.0f
            : BallHaloSize;

        _ballHaloNode.CustomMinimumSize = new Vector2(liveHaloSize, liveHaloSize);
        _ballHaloNode.Size = new Vector2(liveHaloSize, liveHaloSize);
        _ballHaloNode.Position = new Vector2(
            size.X * ballPosition.X - liveHaloSize * 0.5f,
            size.Y * ballPosition.Y - liveHaloSize * 0.5f);
        _ballHaloNode.AddThemeStyleboxOverride("panel", BuildBallHaloStyle(currentFrame.Ball.MovementState));

        _ballNode.CustomMinimumSize = new Vector2(liveBallSize, liveBallSize);
        _ballNode.Size = new Vector2(liveBallSize, liveBallSize);
        _ballNode.Position = new Vector2(
            size.X * ballPosition.X - liveBallSize * 0.5f,
            size.Y * ballPosition.Y - liveBallSize * 0.5f);
        _ballNode.AddThemeStyleboxOverride("panel", BuildBallStyle(currentFrame.Ball.MovementState));
    }

    private void UpdateActionLine(MatchFrame currentFrame, MatchFrame nextFrame, float progress)
    {
        if (_markersLayer == null || _actionLineNode == null)
        {
            return;
        }

        if (!ShouldShowActionLine(currentFrame.Ball.MovementState))
        {
            _actionLineNode.Visible = false;
            return;
        }

        var layerSize = _markersLayer.Size;
        var currentBallPosition = currentFrame.Ball.Position.Lerp(nextFrame.Ball.Position, progress);
        var start = ToPitchPixel(currentBallPosition, layerSize);
        var target = ToPitchPixel(currentFrame.Ball.TargetPosition, layerSize);
        var delta = target - start;
        var length = delta.Length();
        if (length < 8.0f)
        {
            _actionLineNode.Visible = false;
            return;
        }

        _actionLineNode.Visible = true;
        _actionLineNode.Position = start;
        _actionLineNode.Size = new Vector2(length, ResolveActionLineThickness(currentFrame.Ball.MovementState));
        _actionLineNode.PivotOffset = new Vector2(0.0f, _actionLineNode.Size.Y * 0.5f);
        _actionLineNode.Rotation = delta.Angle();
        _actionLineNode.Color = ResolveActionLineColor(currentFrame.Ball.MovementState, !string.IsNullOrWhiteSpace(currentFrame.EventSummary));
    }

    private void UpdateEventFeed(int simulatedSecond, string? activeEventId)
    {
        if (_playback == null)
        {
            return;
        }

        while (_appliedEventCount < _playback.EventFeed.Length && _playback.EventFeed[_appliedEventCount].MatchSecond <= simulatedSecond)
        {
            _appliedEventCount++;
        }

        var activeEventIndex = FindEventIndex(activeEventId);
        if (activeEventIndex >= _appliedEventCount)
        {
            _appliedEventCount = activeEventIndex + 1;
        }

        var startIndex = Math.Max(0, _appliedEventCount - 5);
        var feedLines = new List<string>();
        for (var index = startIndex; index < _appliedEventCount; index++)
        {
            var matchEvent = _playback.EventFeed[index];
            var prefix = matchEvent.Id == activeEventId ? "> " : "  ";
            feedLines.Add($"{prefix}{matchEvent.Summary}");
        }

        if (feedLines.Count == 0)
        {
            feedLines.Add("Play is about to begin.");
        }

        _eventFeedLabel!.Text = string.Join("\n", feedLines);
    }

    private int FindEventIndex(string? eventId)
    {
        if (_playback == null || string.IsNullOrWhiteSpace(eventId))
        {
            return -1;
        }

        for (var index = 0; index < _playback.EventFeed.Length; index++)
        {
            if (_playback.EventFeed[index].Id == eventId)
            {
                return index;
            }
        }

        return -1;
    }

    private string ResolveCarrierLabel(MatchFrame frame)
    {
        if (string.IsNullOrWhiteSpace(frame.Ball.CarrierPlayerId))
        {
            return frame.Ball.MovementState is BallMovementState.Passed or BallMovementState.Shot or BallMovementState.Cleared or BallMovementState.Loose or BallMovementState.Goal
                ? "ball in motion"
                : "none";
        }

        var carrier = FindPlayerState(frame, frame.Ball.CarrierPlayerId);
        return carrier?.Name ?? frame.Ball.CarrierPlayerId;
    }

    private static string ResolveInvolvedPlayerLabel(MatchFrame frame)
    {
        var player = FindPriorityPlayer(frame, PlayerIntent.Carry)
            ?? FindPriorityPlayer(frame, PlayerIntent.Shoot)
            ?? FindPriorityPlayer(frame, PlayerIntent.Receive)
            ?? FindPriorityPlayer(frame, PlayerIntent.Press)
            ?? FindPriorityPlayer(frame, PlayerIntent.Recover)
            ?? FindPriorityPlayer(frame, PlayerIntent.Support);

        return player == null
            ? frame.CurrentActionLabel
            : $"{player.Name} ({FormatIntent(player.CurrentIntent)})";
    }

    private static PlayerAgentState? FindPriorityPlayer(MatchFrame frame, PlayerIntent intent)
    {
        foreach (var player in frame.PlayerStates)
        {
            if (player.HasBall && intent == PlayerIntent.Carry)
            {
                return player;
            }

            if (player.CurrentIntent == intent)
            {
                return player;
            }
        }

        return null;
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

    private static void ApplyMarkerStyle(Button node, StyleBoxFlat markerStyle)
    {
        node.AddThemeStyleboxOverride("disabled", markerStyle);
        node.AddThemeStyleboxOverride("normal", markerStyle);
        node.AddThemeStyleboxOverride("hover", markerStyle);
        node.AddThemeStyleboxOverride("pressed", markerStyle);
    }

    private static StyleBoxFlat BuildMarkerStyle(bool isHome, PlayerIntent intent, bool hasBall)
    {
        var background = isHome ? new Color(0.129f, 0.424f, 0.690f) : new Color(0.698f, 0.204f, 0.251f);
        var border = new Color(0.95f, 0.95f, 0.95f);
        var borderWidth = 2;

        if (hasBall)
        {
            background = new Color(0.96f, 0.72f, 0.20f);
            border = new Color(1.0f, 0.96f, 0.58f);
            borderWidth = 4;
        }
        else if (intent == PlayerIntent.Receive)
        {
            border = new Color(0.50f, 0.88f, 1.0f);
            borderWidth = 3;
        }
        else if (intent == PlayerIntent.Shoot)
        {
            border = new Color(1.0f, 0.62f, 0.28f);
            borderWidth = 4;
        }
        else if (intent == PlayerIntent.Press)
        {
            border = new Color(0.92f, 0.98f, 0.92f);
            borderWidth = 3;
        }
        else if (intent == PlayerIntent.Recover)
        {
            border = new Color(0.70f, 0.88f, 1.0f);
            borderWidth = 3;
        }

        return new StyleBoxFlat
        {
            BgColor = background,
            CornerRadiusTopLeft = 15,
            CornerRadiusTopRight = 15,
            CornerRadiusBottomRight = 15,
            CornerRadiusBottomLeft = 15,
            BorderWidthLeft = borderWidth,
            BorderWidthTop = borderWidth,
            BorderWidthRight = borderWidth,
            BorderWidthBottom = borderWidth,
            BorderColor = border
        };
    }

    private static StyleBoxFlat BuildBallStyle(BallMovementState movementState)
    {
        var fill = movementState switch
        {
            BallMovementState.Shot or BallMovementState.Goal => new Color(1.0f, 0.92f, 0.18f),
            BallMovementState.Passed => new Color(0.98f, 0.98f, 0.94f),
            BallMovementState.Cleared or BallMovementState.Saved => new Color(0.78f, 0.94f, 1.0f),
            BallMovementState.Loose => new Color(1.0f, 0.72f, 0.58f),
            _ => new Color(0.98f, 0.92f, 0.35f)
        };

        return new StyleBoxFlat
        {
            BgColor = fill,
            CornerRadiusTopLeft = 9,
            CornerRadiusTopRight = 9,
            CornerRadiusBottomRight = 9,
            CornerRadiusBottomLeft = 9,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            BorderColor = new Color(0.08f, 0.08f, 0.08f)
        };
    }

    private static StyleBoxFlat BuildBallHaloStyle(BallMovementState movementState)
    {
        var halo = movementState switch
        {
            BallMovementState.Shot or BallMovementState.Goal => new Color(1.0f, 0.74f, 0.12f, 0.24f),
            BallMovementState.Passed => new Color(1.0f, 1.0f, 0.86f, 0.18f),
            BallMovementState.Cleared or BallMovementState.Saved => new Color(0.62f, 0.88f, 1.0f, 0.20f),
            BallMovementState.Loose => new Color(1.0f, 0.44f, 0.30f, 0.20f),
            _ => new Color(1.0f, 0.92f, 0.32f, 0.18f)
        };

        return new StyleBoxFlat
        {
            BgColor = halo,
            CornerRadiusTopLeft = 17,
            CornerRadiusTopRight = 17,
            CornerRadiusBottomRight = 17,
            CornerRadiusBottomLeft = 17,
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            BorderColor = new Color(1.0f, 0.98f, 0.78f, 0.24f)
        };
    }

    private static bool ShouldShowActionLine(BallMovementState movementState)
    {
        return movementState is BallMovementState.Passed
            or BallMovementState.Shot
            or BallMovementState.Cleared
            or BallMovementState.Saved
            or BallMovementState.Loose
            or BallMovementState.Goal;
    }

    private static float ResolveActionLineThickness(BallMovementState movementState)
    {
        return movementState is BallMovementState.Shot or BallMovementState.Goal
            ? ActionLineThickness + 2.0f
            : ActionLineThickness;
    }

    private static Color ResolveActionLineColor(BallMovementState movementState, bool hasEvent)
    {
        var alpha = hasEvent ? 0.95f : 0.68f;
        return movementState switch
        {
            BallMovementState.Shot or BallMovementState.Goal => new Color(1.0f, 0.74f, 0.18f, alpha),
            BallMovementState.Cleared or BallMovementState.Saved => new Color(0.60f, 0.90f, 1.0f, alpha),
            BallMovementState.Loose => new Color(1.0f, 0.42f, 0.28f, alpha),
            _ => new Color(1.0f, 1.0f, 0.86f, alpha)
        };
    }

    private static float ResolveMarkerSize(PlayerIntent intent, bool hasBall)
    {
        if (hasBall)
        {
            return MarkerSize + 8.0f;
        }

        return intent is PlayerIntent.Receive or PlayerIntent.Shoot or PlayerIntent.Press
            ? MarkerSize + 3.0f
            : MarkerSize;
    }

    private static Vector2 ToPitchPixel(Vector2 normalizedPosition, Vector2 layerSize)
    {
        return new Vector2(layerSize.X * normalizedPosition.X, layerSize.Y * normalizedPosition.Y);
    }

    private static float SmoothStep(float value)
    {
        var clamped = Math.Clamp(value, 0.0f, 1.0f);
        return clamped * clamped * (3.0f - 2.0f * clamped);
    }

    private static string BuildPlayerTooltip(PlayerAgentState player, bool hasBall)
    {
        var ballLabel = hasBall ? " | BALL" : string.Empty;
        return $"{player.Name} | {player.Team} | {player.Role} | Intent: {FormatIntent(player.CurrentIntent)}{ballLabel}";
    }

    private static string FormatBallMovement(BallMovementState movementState)
    {
        return movementState switch
        {
            BallMovementState.Carried => "carried",
            BallMovementState.Passed => "pass in flight",
            BallMovementState.Shot => "shot",
            BallMovementState.Loose => "loose",
            BallMovementState.Cleared => "clearance",
            BallMovementState.Saved => "save",
            BallMovementState.Goal => "goal",
            _ => movementState.ToString()
        };
    }

    private static string FormatIntent(PlayerIntent intent)
    {
        return intent switch
        {
            PlayerIntent.HoldShape => "hold shape",
            PlayerIntent.Support => "support",
            PlayerIntent.Press => "press",
            PlayerIntent.Receive => "receive",
            PlayerIntent.Carry => "carry",
            PlayerIntent.Shoot => "shoot",
            PlayerIntent.Defend => "defend",
            PlayerIntent.Recover => "recover",
            _ => intent.ToString()
        };
    }

    private static string FormatPitchPoint(Vector2 normalizedPosition)
    {
        return $"{normalizedPosition.X:0.00}, {normalizedPosition.Y:0.00}";
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
        _scoreLabel!.Text = $"{_playback.FinalHomeScore} - {_playback.FinalAwayScore}";
        _clockLabel!.Text = "FT";
        _statusLabel!.Text = $"Full time | {_playback.FinalResultSummary}";
        _statusLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.84f, 0.32f));
        _controlLabel!.Text = "Action | full time\nNext | Continue to Post-Match";
        _momentumLabel!.Text = "Full time\nPost-match ready";
        _pitchStateLabel!.Text = "Full Time";
        _pitchStateLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.84f, 0.32f));
        _pitchNoteLabel!.Text = "Playback complete. Continue to the post-match screen for the aftermath.";
        _backButton!.Text = "Continue to Post-Match";
        _matchComplete = true;
    }
}
