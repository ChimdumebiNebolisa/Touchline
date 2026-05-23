using Godot;
using System;

public enum BallMovementState
{
    Carried,
    Passed,
    Shot,
    Loose,
    Cleared,
    Saved,
    Goal
}

public enum PlayerIntent
{
    HoldShape,
    Support,
    Press,
    Receive,
    Carry,
    Shoot,
    Defend,
    Recover
}

public enum MatchActionKind
{
    Kickoff,
    Pass,
    Carry,
    Shot,
    Save,
    Clearance,
    Interception,
    Goal,
    Reset
}

public sealed class BallState
{
    public required Vector2 Position { get; init; }
    public required Vector2 TargetPosition { get; init; }
    public string? CarrierPlayerId { get; init; }
    public required BallMovementState MovementState { get; init; }
}

public sealed class PlayerAgentState
{
    public required string PlayerId { get; init; }
    public required string Name { get; init; }
    public required string Team { get; init; }
    public required string Role { get; init; }
    public required Vector2 Position { get; init; }
    public required Vector2 TargetPosition { get; init; }
    public required bool HasBall { get; init; }
    public required PlayerIntent CurrentIntent { get; init; }
}

public sealed class MatchFrame
{
    public required int MatchSecond { get; init; }
    public int Minute => Math.Max(1, (MatchSecond / 60) + 1);
    public required int HomeScore { get; init; }
    public required int AwayScore { get; init; }
    public required string PossessionTeam { get; init; }
    public required BallState Ball { get; init; }
    public required PlayerAgentState[] PlayerStates { get; init; }
    public required string CurrentActionLabel { get; init; }
    public string? EventId { get; init; }
    public string? EventSummary { get; init; }
}

public sealed class MatchAction
{
    public required string Id { get; init; }
    public required MatchActionKind Kind { get; init; }
    public required int StartSecond { get; init; }
    public required int EndSecond { get; init; }
    public required string Team { get; init; }
    public required string Label { get; init; }
    public string? FromPlayerId { get; init; }
    public string? ToPlayerId { get; init; }
    public required Vector2 FromPosition { get; init; }
    public required Vector2 ToPosition { get; init; }
    public required int HomeScoreAfter { get; init; }
    public required int AwayScoreAfter { get; init; }
}

public sealed class MatchEvent
{
    public required string Id { get; init; }
    public required int Minute { get; init; }
    public required int MatchSecond { get; init; }
    public required string Summary { get; init; }
    public required int HomeScore { get; init; }
    public required int AwayScore { get; init; }
    public required string ActionId { get; init; }
    public required int StartFrameIndex { get; init; }
    public required int EndFrameIndex { get; init; }
}

public sealed class TacticalShape
{
    public required string Formation { get; init; }
    public required Vector2[] HomeInPossession { get; init; }
    public required Vector2[] HomeOutOfPossession { get; init; }
    public required Vector2[] AwayInPossession { get; init; }
    public required Vector2[] AwayOutOfPossession { get; init; }
}

public sealed class MatchTimeline
{
    public required int DurationSeconds { get; init; }
    public required MatchFrame[] Frames { get; init; }
    public required MatchAction[] Actions { get; init; }
}

public sealed class MatchPlaybackResult
{
    public required string HomeClubName { get; init; }
    public required string AwayClubName { get; init; }
    public required string TacticalSummary { get; init; }
    public required int FinalHomeScore { get; init; }
    public required int FinalAwayScore { get; init; }
    public required MatchTimeline Timeline { get; init; }
    public required MatchEvent[] EventFeed { get; init; }
    public required PlayerAgentState[] PlayerStates { get; init; }
    public required BallState BallState { get; init; }
    public required string PossessionTeam { get; init; }
    public required string[] ActionLabels { get; init; }
    public required string FinalResultSummary { get; init; }

    public int HomeGoals => FinalHomeScore;
    public int AwayGoals => FinalAwayScore;
    public MatchFrame[] Frames => Timeline.Frames;
}
