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

        var participantValidation = ValidateActionParticipants(playback);
        if (participantValidation != PassMessage)
        {
            return participantValidation;
        }

        var statsValidation = ValidateMatchStats(playback);
        if (statsValidation != PassMessage)
        {
            return statsValidation;
        }

        return PassMessage;
    }

    public static string ValidateActionParticipants(MatchPlaybackResult playback)
    {
        foreach (var action in playback.Timeline.Actions)
        {
            var participants = action.Participants;
            switch (action.Kind)
            {
                case MatchActionKind.Pass:
                    if (string.IsNullOrWhiteSpace(participants.PasserPlayerId) ||
                        string.IsNullOrWhiteSpace(participants.ReceiverPlayerId))
                    {
                        return $"Pass action {action.Id} is missing passer or receiver metadata.";
                    }
                    break;
                case MatchActionKind.Shot:
                    if (string.IsNullOrWhiteSpace(participants.ShooterPlayerId))
                    {
                        return $"Shot action {action.Id} is missing shooter metadata.";
                    }
                    break;
                case MatchActionKind.Save:
                    if (string.IsNullOrWhiteSpace(participants.ShooterPlayerId) ||
                        string.IsNullOrWhiteSpace(participants.GoalkeeperPlayerId))
                    {
                        return $"Save action {action.Id} is missing shooter or goalkeeper metadata.";
                    }
                    break;
                case MatchActionKind.Clearance:
                    if (string.IsNullOrWhiteSpace(participants.ClearerPlayerId))
                    {
                        return $"Clearance action {action.Id} is missing clearer metadata.";
                    }
                    break;
                case MatchActionKind.Interception:
                    if (string.IsNullOrWhiteSpace(participants.InterceptorPlayerId))
                    {
                        return $"Interception action {action.Id} is missing interceptor metadata.";
                    }
                    break;
                case MatchActionKind.Goal:
                    if (string.IsNullOrWhiteSpace(participants.ScorerPlayerId))
                    {
                        return $"Goal action {action.Id} is missing scorer metadata.";
                    }
                    break;
            }
        }

        return PassMessage;
    }

    public static string ValidateMatchStats(MatchPlaybackResult playback)
    {
        var expected = MatchStatsService.Build(playback.HomeClubName, playback.AwayClubName, playback.Timeline.Actions);
        var stats = playback.Stats;

        if (stats.HomeGoals != playback.FinalHomeScore || stats.AwayGoals != playback.FinalAwayScore)
        {
            return "Match stats goal totals do not match final score.";
        }

        if (stats.HomeShots != expected.HomeShots || stats.AwayShots != expected.AwayShots)
        {
            return "Match stats shot totals do not match playback actions.";
        }

        if (stats.HomeSaves != expected.HomeSaves || stats.AwaySaves != expected.AwaySaves)
        {
            return "Match stats save totals do not match playback actions.";
        }

        if (stats.HomeClearances != expected.HomeClearances || stats.AwayClearances != expected.AwayClearances)
        {
            return "Match stats clearance totals do not match playback actions.";
        }

        if (stats.HomeInterceptions != expected.HomeInterceptions || stats.AwayInterceptions != expected.AwayInterceptions)
        {
            return "Match stats interception totals do not match playback actions.";
        }

        if (stats.HomeCompletedPasses != expected.HomeCompletedPasses || stats.AwayCompletedPasses != expected.AwayCompletedPasses)
        {
            return "Match stats completed pass totals do not match playback actions.";
        }

        if (stats.HomePossessionPhaseCount <= 0 || stats.AwayPossessionPhaseCount <= 0)
        {
            return "Match stats must include possession phases for both teams.";
        }

        return PassMessage;
    }

    private static bool IsNormalized(Godot.Vector2 value)
    {
        return value.X >= 0.0f && value.X <= 1.0f && value.Y >= 0.0f && value.Y <= 1.0f;
    }
}
