using Godot;
using System;
using System.Collections.Generic;

public partial class LiveMatchScene : Control
{
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private const string PostMatchScenePath = "res://scenes/PostMatchScene.tscn";
    private const float SimulatedMinutesPerSecond = 6.0f;
    private const float MarkerSize = 30.0f;

    private readonly List<Button> _markerNodes = new();
    private MatchSimulationResult? _playback;
    private float _elapsedSeconds;
    private int _currentMinute = 1;
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
            _fixtureLabel.Text = "Fixture unavailable";
            _scoreLabel.Text = "0 - 0";
            _clockLabel.Text = "01'";
            _tacticalLabel.Text = "Tactics unavailable";
            _momentumLabel.Text = "Momentum unavailable";
            _statusLabel.Text = "Live context unavailable.";
            _controlLabel.Text = "Broadcast focus unavailable.";
            _eventFeedLabel.Text = "No live events yet.";
            _homeTagLabel.Text = "HOME";
            _awayTagLabel.Text = "AWAY";
            _pitchNoteLabel.Text = "Pitch presentation unavailable.";
            return;
        }

        _playback = GameState.Instance.PrepareCurrentMatchResult();
        _fixtureLabel.Text = $"{_playback.HomeClubName} vs {_playback.AwayClubName}";
        _scoreLabel.Text = "0 - 0";
        _clockLabel.Text = "01'";
        _tacticalLabel.Text = _playback.TacticalSummary;
        _momentumLabel.Text = "Momentum: balanced opening";
        _statusLabel.Text =
            $"{_playback.HomeClubName} settle into possession while {_playback.AwayClubName} hold a compact shape.";
        _controlLabel.Text = "Broadcast focus: opening exchanges and shape recognition.";
        _eventFeedLabel.Text = "1' Kick-off.";
        _homeTagLabel.Text = _playback.HomeClubName;
        _awayTagLabel.Text = _playback.AwayClubName;
        _pitchNoteLabel.Text = "The pitch view tracks marker movement while the sidebar surfaces decisive moments.";

        CreateMarkers();
        ApplyEventsUpToMinute(_currentMinute);
        SetProcess(true);
    }

    public override void _Process(double delta)
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        _elapsedSeconds += (float)delta;
        var simulatedMinute = Math.Min(90, 1 + (int)MathF.Floor(_elapsedSeconds * SimulatedMinutesPerSecond));
        if (simulatedMinute != _currentMinute)
        {
            _currentMinute = simulatedMinute;
            ApplyEventsUpToMinute(_currentMinute);
        }

        UpdateMarkerPositions(_elapsedSeconds);

        if (_currentMinute >= 90)
        {
            FinalizeMatch();
            SetProcess(false);
        }
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(_matchComplete ? PostMatchScenePath : MatchdayScenePath);
    }

    private void CreateMarkers()
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        foreach (var marker in _playback.Markers)
        {
            var node = new Button
            {
                Text = marker.Initials,
                Disabled = true,
                FocusMode = FocusModeEnum.None,
                MouseFilter = MouseFilterEnum.Ignore,
                CustomMinimumSize = new Vector2(MarkerSize, MarkerSize),
                TooltipText = marker.FullName
            };

            node.AddThemeColorOverride("font_disabled_color", Colors.White);
            var markerStyle = BuildMarkerStyle(marker.IsHome);
            node.AddThemeStyleboxOverride("disabled", markerStyle);
            node.AddThemeStyleboxOverride("normal", markerStyle);
            node.AddThemeStyleboxOverride("hover", markerStyle);
            node.AddThemeStyleboxOverride("pressed", markerStyle);

            _markersLayer.AddChild(node);
            _markerNodes.Add(node);
        }

        UpdateMarkerPositions(0.0f);
    }

    private void UpdateMarkerPositions(float elapsedSeconds)
    {
        if (_playback == null || _markersLayer == null)
        {
            return;
        }

        var size = _markersLayer.Size;
        for (var index = 0; index < _playback.Markers.Length; index++)
        {
            var marker = _playback.Markers[index];
            var node = _markerNodes[index];

            var swayX = MathF.Sin(elapsedSeconds * marker.Speed + marker.Phase) * marker.SwingX;
            var swayY = MathF.Cos(elapsedSeconds * (marker.Speed + 0.18f) + marker.Phase) * marker.SwingY;
            var x = Math.Clamp(marker.Anchor.X + swayX, 0.07f, 0.93f);
            var y = Math.Clamp(marker.Anchor.Y + swayY, 0.08f, 0.92f);

            node.Position = new Vector2(
                size.X * x - MarkerSize * 0.5f,
                size.Y * y - MarkerSize * 0.5f);
        }
    }

    private void ApplyEventsUpToMinute(int minute)
    {
        if (_playback == null)
        {
            return;
        }

        while (_appliedEventCount < _playback.Events.Length && _playback.Events[_appliedEventCount].Minute <= minute)
        {
            _appliedEventCount++;
        }

        var latestEvent = _playback.Events[Math.Max(0, _appliedEventCount - 1)];
        _scoreLabel!.Text = $"{latestEvent.HomeScore} - {latestEvent.AwayScore}";
        _clockLabel!.Text = $"{minute:00}'";
        _statusLabel!.Text = BuildStatus(latestEvent, minute);
        _controlLabel!.Text = BuildControlLabel(latestEvent, minute);
        _momentumLabel!.Text = BuildMomentumLabel(latestEvent, minute);
        _eventFeedLabel!.Text = BuildEventFeed(_appliedEventCount);
        _pitchNoteLabel!.Text = BuildPitchNote(latestEvent, minute);
    }

    private string BuildEventFeed(int appliedEventCount)
    {
        if (_playback == null)
        {
            return "No live events yet.";
        }

        var startIndex = Math.Max(0, appliedEventCount - 5);
        var feedLines = new List<string>();
        for (var index = startIndex; index < appliedEventCount; index++)
        {
            feedLines.Add(_playback.Events[index].Summary);
        }

        if (feedLines.Count == 0)
        {
            feedLines.Add("Play is about to begin.");
        }

        return string.Join("\n", feedLines);
    }

    private string BuildStatus(MatchSimulationResult.MatchEvent latestEvent, int minute)
    {
        if (_playback == null)
        {
            return "Live context unavailable.";
        }

        if (minute >= 90)
        {
            return $"{_playback.HomeClubName} {latestEvent.HomeScore} - {latestEvent.AwayScore} {_playback.AwayClubName}.";
        }

        if (latestEvent.Minute == minute)
        {
            return latestEvent.Summary;
        }

        return $"{_playback.HomeClubName} probe for openings while the clock ticks toward {minute:00}'.";
    }

    private string BuildControlLabel(MatchSimulationResult.MatchEvent latestEvent, int minute)
    {
        if (_playback == null)
        {
            return "Broadcast focus unavailable.";
        }

        var scoreDifference = latestEvent.HomeScore - latestEvent.AwayScore;
        if (minute >= 90)
        {
            return "Broadcast focus: full-time whistle and handoff to consequence review.";
        }

        if (latestEvent.Minute == minute)
        {
            return "Broadcast focus: decisive action just landed in the feed.";
        }

        return scoreDifference switch
        {
            > 0 => $"Broadcast focus: {_playback.AwayClubName} are chasing the match while {_playback.HomeClubName} manage territory.",
            < 0 => $"Broadcast focus: {_playback.HomeClubName} need a response while {_playback.AwayClubName} protect the lead.",
            _ => "Broadcast focus: the match is level and both midfields are fighting for control."
        };
    }

    private string BuildMomentumLabel(MatchSimulationResult.MatchEvent latestEvent, int minute)
    {
        if (_playback == null)
        {
            return "Momentum unavailable";
        }

        var scoreDifference = latestEvent.HomeScore - latestEvent.AwayScore;
        if (minute >= 90)
        {
            return "Momentum: full time";
        }

        if (latestEvent.Minute == minute && latestEvent.Summary.Contains("Goal", StringComparison.Ordinal))
        {
            return scoreDifference >= 0
                ? $"Momentum: {_playback.HomeClubName} surge after the latest goal"
                : $"Momentum: {_playback.AwayClubName} seize the latest swing";
        }

        if (minute < 25)
        {
            return "Momentum: balanced opening";
        }

        if (minute < 60)
        {
            return scoreDifference switch
            {
                > 0 => $"Momentum: {_playback.HomeClubName} control the middle phase",
                < 0 => $"Momentum: {_playback.AwayClubName} are dictating transitions",
                _ => "Momentum: midfield battle tightening"
            };
        }

        return scoreDifference switch
        {
            > 0 => $"Momentum: {_playback.HomeClubName} managing the closing phase",
            < 0 => $"Momentum: {_playback.AwayClubName} protecting the edge",
            _ => "Momentum: match hanging on one moment"
        };
    }

    private string BuildPitchNote(MatchSimulationResult.MatchEvent latestEvent, int minute)
    {
        if (_playback == null)
        {
            return "Pitch presentation unavailable.";
        }

        if (minute >= 90)
        {
            return "The final whistle has gone. Continue to post-match for the consequence breakdown.";
        }

        if (latestEvent.Minute == minute)
        {
            return latestEvent.Summary;
        }

        return $"{minute:00}' on the clock. Marker movement reflects the current phase while the feed tracks the most decisive actions.";
    }

    private static StyleBoxFlat BuildMarkerStyle(bool isHome)
    {
        var style = new StyleBoxFlat
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

        return style;
    }

    private void FinalizeMatch()
    {
        if (_matchComplete || _playback == null)
        {
            return;
        }

        GameState.Instance?.ApplyMatchResult(_playback);
        _statusLabel!.Text = "Full time. Review the result and consequence deltas in post-match.";
        _controlLabel!.Text = "Broadcast focus: match complete.";
        _momentumLabel!.Text = "Momentum: full time";
        _pitchNoteLabel!.Text = "Playback complete. Continue to the post-match screen for the aftermath.";
        _backButton!.Text = "Continue to Post-Match";
        _matchComplete = true;
    }
}
