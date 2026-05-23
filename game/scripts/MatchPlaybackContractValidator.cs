using System;

public static class MatchPlaybackContractValidator
{
    public const string PassMessage = "OK";

    public static string Validate(MatchPlaybackResult playback)
    {
        if (playback.Timeline.Frames.Length == 0)
        {
            return "Playback contains no frames.";
        }

        if (playback.EventFeed.Length == 0)
        {
            return "Playback contains no events.";
        }

        var hasPassMovement = false;
        var hasShotMovement = false;
        var hasGoalAction = false;
        foreach (var action in playback.Timeline.Actions)
        {
            if (action.Kind == MatchActionKind.Pass && action.FromPosition.DistanceTo(action.ToPosition) > 0.01f)
            {
                hasPassMovement = true;
            }

            if (action.Kind == MatchActionKind.Shot && action.FromPosition.DistanceTo(action.ToPosition) > 0.01f)
            {
                hasShotMovement = true;
            }

            if (action.Kind == MatchActionKind.Goal)
            {
                hasGoalAction = true;
            }
        }

        if (!hasPassMovement)
        {
            return "No pass action with visible ball movement found.";
        }

        if (!hasShotMovement)
        {
            return "No shot action with visible ball movement found.";
        }

        var finalFrame = playback.Timeline.Frames[^1];
        if (finalFrame.PlayerStates.Length != 22)
        {
            return "Final frame does not contain all 22 player states.";
        }

        if (finalFrame.HomeScore != playback.FinalHomeScore || finalFrame.AwayScore != playback.FinalAwayScore)
        {
            return "Final frame score does not match playback final score.";
        }

        foreach (var frame in playback.Timeline.Frames)
        {
            if (frame.PlayerStates.Length != 22)
            {
                return $"Frame at {frame.MatchSecond}s does not contain all 22 player states.";
            }

            if (!IsNormalized(frame.Ball.Position) || !IsNormalized(frame.Ball.TargetPosition))
            {
                return $"Frame at {frame.MatchSecond}s has ball coordinates outside normalized pitch bounds.";
            }

            foreach (var player in frame.PlayerStates)
            {
                if (!IsNormalized(player.Position) || !IsNormalized(player.TargetPosition))
                {
                    return $"Frame at {frame.MatchSecond}s has player coordinates outside normalized pitch bounds.";
                }
            }
        }

        foreach (var matchEvent in playback.EventFeed)
        {
            if (string.IsNullOrWhiteSpace(matchEvent.ActionId))
            {
                return "Event missing action id.";
            }

            if (matchEvent.StartFrameIndex < 0 ||
                matchEvent.EndFrameIndex < matchEvent.StartFrameIndex ||
                matchEvent.EndFrameIndex >= playback.Timeline.Frames.Length)
            {
                return "Event frame range invalid.";
            }
        }

        if (hasGoalAction && playback.FinalHomeScore + playback.FinalAwayScore <= 0)
        {
            return "Goal action exists but final score did not update.";
        }

        return PassMessage;
    }

    private static bool IsNormalized(Godot.Vector2 value)
    {
        return value.X >= 0.0f && value.X <= 1.0f && value.Y >= 0.0f && value.Y <= 1.0f;
    }
}
