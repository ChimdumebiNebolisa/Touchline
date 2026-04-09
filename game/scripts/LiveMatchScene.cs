using Godot;
using System;
using System.Collections.Generic;

public partial class LiveMatchScene : Control
{
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";
    private const float SimulatedMinutesPerSecond = 6.0f;
    private const float MarkerSize = 30.0f;

    private readonly List<Button> _markerNodes = new();
    private LiveMatchPlayback? _playback;
    private float _elapsedSeconds;
    private int _currentMinute = 1;
    private int _appliedEventCount;

    private Label? _fixtureLabel;
    private Label? _scoreLabel;
    private Label? _clockLabel;
    private Label? _tacticalLabel;
    private Label? _statusLabel;
    private Label? _eventFeedLabel;
    private Control? _markersLayer;

    public override void _Ready()
    {
        _fixtureLabel = GetNode<Label>("Margin/Root/ScoreStrip/FixtureLabel");
        _scoreLabel = GetNode<Label>("Margin/Root/ScoreStrip/ScoreLabel");
        _clockLabel = GetNode<Label>("Margin/Root/ScoreStrip/ClockLabel");
        _tacticalLabel = GetNode<Label>("Margin/Root/Content/Sidebar/TacticalLabel");
        _statusLabel = GetNode<Label>("Margin/Root/Content/Sidebar/StatusLabel");
        _eventFeedLabel = GetNode<Label>("Margin/Root/Content/Sidebar/EventFeedLabel");
        _markersLayer = GetNode<Control>("Margin/Root/Content/PitchFrame/Pitch/MarkersLayer");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _fixtureLabel.Text = "Fixture unavailable";
            _scoreLabel.Text = "0 - 0";
            _clockLabel.Text = "01'";
            _tacticalLabel.Text = "Tactics unavailable";
            _statusLabel.Text = "Live context unavailable.";
            _eventFeedLabel.Text = "No live events yet.";
            return;
        }

        _playback = LiveMatchPlayback.Create(GameState.Instance);
        _fixtureLabel.Text = $"{_playback.HomeClubName} vs {_playback.AwayClubName}";
        _scoreLabel.Text = "0 - 0";
        _clockLabel.Text = "01'";
        _tacticalLabel.Text = _playback.TacticalSummary;
        _statusLabel.Text =
            $"{_playback.HomeClubName} settle into possession while {_playback.AwayClubName} hold a compact shape.";
        _eventFeedLabel.Text = "1' Kick-off.";

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
            _statusLabel!.Text = "Full time. The post-match consequence layer is the next active step.";
            SetProcess(false);
        }
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
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
        _eventFeedLabel!.Text = BuildEventFeed(_appliedEventCount);
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

    private string BuildStatus(LiveMatchPlayback.MatchEvent latestEvent, int minute)
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

    private static StyleBoxFlat BuildMarkerStyle(bool isHome)
    {
        var style = new StyleBoxFlat
        {
            BgColor = isHome ? new Color(0.086f, 0.204f, 0.502f) : new Color(0.541f, 0.125f, 0.149f),
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
}
