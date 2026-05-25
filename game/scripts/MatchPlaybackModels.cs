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
    BigChance,
    Shot,
    Save,
    Clearance,
    Interception,
    Goal,
    Foul,
    YellowCard,
    InjuryConcern,
    FatigueWarning,
    TacticalShift,
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
    public required ActionParticipants Participants { get; init; }
    public required Vector2 FromPosition { get; init; }
    public required Vector2 ToPosition { get; init; }
    public required int HomeScoreAfter { get; init; }
    public required int AwayScoreAfter { get; init; }
}

public sealed class ActionParticipants
{
    public string? PasserPlayerId { get; init; }
    public string? ReceiverPlayerId { get; init; }
    public string? CarrierPlayerId { get; init; }
    public string? ShooterPlayerId { get; init; }
    public string? GoalkeeperPlayerId { get; init; }
    public string? DefenderPlayerId { get; init; }
    public string? InterceptorPlayerId { get; init; }
    public string? ClearerPlayerId { get; init; }
    public string? ScorerPlayerId { get; init; }
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

public sealed class MatchStats
{
    public required int HomeShots { get; init; }
    public required int AwayShots { get; init; }
    public required int HomeGoals { get; init; }
    public required int AwayGoals { get; init; }
    public required int HomeSaves { get; init; }
    public required int AwaySaves { get; init; }
    public required int HomeClearances { get; init; }
    public required int AwayClearances { get; init; }
    public required int HomeInterceptions { get; init; }
    public required int AwayInterceptions { get; init; }
    public required int HomePossessionPhaseCount { get; init; }
    public required int AwayPossessionPhaseCount { get; init; }
    public required int HomeCompletedPasses { get; init; }
    public required int AwayCompletedPasses { get; init; }
    public required int HomeLateGoals { get; init; }
    public required int AwayLateGoals { get; init; }
    public required int HomeBigChances { get; init; }
    public required int AwayBigChances { get; init; }
    public required int HomeFouls { get; init; }
    public required int AwayFouls { get; init; }
    public required int HomeYellowCards { get; init; }
    public required int AwayYellowCards { get; init; }
    public required int HomeInjuryConcerns { get; init; }
    public required int AwayInjuryConcerns { get; init; }
    public required int HomeFatigueWarnings { get; init; }
    public required int AwayFatigueWarnings { get; init; }
    public required int TacticalShiftEvents { get; init; }
    public required int PressureTurnovers { get; init; }
    public required int LongestPossessionChain { get; init; }
}

public sealed class PlayerMatchRating
{
    public required string PlayerId { get; init; }
    public required string Name { get; init; }
    public required string Team { get; init; }
    public required string Role { get; init; }
    public required double Rating { get; init; }
    public required string Note { get; init; }
}

public sealed class TacticalCauseRecord
{
    public required string Category { get; init; }
    public required string Summary { get; init; }
    public required int HomeImpact { get; init; }
    public required int AwayImpact { get; init; }
}

public sealed class MatchPlaybackResult
{
    public required string HomeClubName { get; init; }
    public required string AwayClubName { get; init; }
    public required string TacticalSummary { get; init; }
    public string TacticalExplanation { get; init; } = string.Empty;
    public string PlayerRatingsSummary { get; init; } = string.Empty;
    public string PostMatchNotes { get; init; } = string.Empty;
    public string MomentumSummary { get; init; } = string.Empty;
    public string DisciplineSummary { get; init; } = string.Empty;
    public string OpponentStyleSummary { get; init; } = string.Empty;
    public required int FinalHomeScore { get; init; }
    public required int FinalAwayScore { get; init; }
    public required MatchTimeline Timeline { get; init; }
    public required MatchEvent[] EventFeed { get; init; }
    public required PlayerAgentState[] PlayerStates { get; init; }
    public required BallState BallState { get; init; }
    public required string PossessionTeam { get; init; }
    public required string[] ActionLabels { get; init; }
    public required MatchStats Stats { get; init; }
    public required PlayerMatchRating[] PlayerRatings { get; init; }
    public required TacticalCauseRecord[] TacticalCauses { get; init; }
    public required string FinalResultSummary { get; init; }

    public int HomeGoals => FinalHomeScore;
    public int AwayGoals => FinalAwayScore;
    public MatchFrame[] Frames => Timeline.Frames;
}
