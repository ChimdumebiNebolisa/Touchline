using System;
using System.Collections.Generic;

public sealed class PostMatchConsequenceResult
{
    public required int MoraleDelta { get; init; }
    public required int FanDelta { get; init; }
    public required int BoardDelta { get; init; }
    public required string ResultLabel { get; init; }
    public required string ConsequenceSummary { get; init; }
    public required string CauseSummary { get; init; }
    public required string StatsSummary { get; init; }
    public required string KeyPlayerMoments { get; init; }
    public required string TacticalExplanation { get; init; }
    public required string PressureSummary { get; init; }
    public required string[] KeyEvents { get; init; }
}

public static class PostMatchConsequenceService
{
    public static PostMatchConsequenceResult Evaluate(MatchPlaybackResult result, GameState state)
    {
        var goalDifference = result.FinalHomeScore - result.FinalAwayScore;
        var analysis = Analyze(result);
        var moraleDelta = goalDifference > 0 ? 4 : goalDifference == 0 ? 1 : -4;
        var fanDelta = goalDifference > 0 ? 5 : goalDifference == 0 ? 0 : -5;
        var boardDelta = goalDifference > 0 ? 3 : goalDifference == 0 ? -1 : -4;

        if (analysis.HomeShots > analysis.AwayShots)
        {
            moraleDelta += 1;
        }

        if (analysis.HomeSaves >= 3 && goalDifference >= 0)
        {
            moraleDelta += 1;
            boardDelta += 1;
        }

        if (analysis.HomeInterceptions + analysis.HomeClearances >= 5 && state.PressIntensity >= 65)
        {
            boardDelta += 1;
        }

        if (analysis.HomeLateGoal)
        {
            fanDelta += goalDifference >= 0 ? 1 : 0;
        }

        if (analysis.Comeback)
        {
            moraleDelta += 2;
            fanDelta += 2;
            boardDelta += 1;
        }

        if (analysis.Collapse)
        {
            moraleDelta -= 2;
            fanDelta -= 2;
            boardDelta -= 1;
        }

        if (goalDifference < 0 && state.Risk >= 70)
        {
            fanDelta -= 1;
            boardDelta -= 1;
        }

        if (goalDifference <= 0 && analysis.HomeShots == 0)
        {
            moraleDelta -= 1;
            fanDelta -= 1;
        }

        moraleDelta = Math.Clamp(moraleDelta, -8, 8);
        fanDelta = Math.Clamp(fanDelta, -8, 8);
        boardDelta = Math.Clamp(boardDelta, -8, 8);
        var causeSummary = BuildCauseSummary(result, state, analysis);
        var statsSummary = BuildStatsSummary(result.Stats);
        var tacticalExplanation = BuildTacticalExplanation(result, state, analysis);
        var keyPlayerMoments = BuildKeyPlayerMoments(result);
        var pressureSummary =
            $"Club pressure now sits at morale {Math.Clamp(state.TeamMorale + moraleDelta, 0, 100)}, fan trust {Math.Clamp(state.FanSentiment + fanDelta, 0, 100)}, and board confidence {Math.Clamp(state.BoardConfidence + boardDelta, 0, 100)}. Cause: {causeSummary}";

        return new PostMatchConsequenceResult
        {
            MoraleDelta = moraleDelta,
            FanDelta = fanDelta,
            BoardDelta = boardDelta,
            ResultLabel = BuildResultLabel(goalDifference, result.AwayClubName, analysis),
            ConsequenceSummary =
                $"Morale {FormatSignedDelta(moraleDelta)} | Fans {FormatSignedDelta(fanDelta)} | Board {FormatSignedDelta(boardDelta)} | Cause: {causeSummary}",
            CauseSummary = causeSummary,
            StatsSummary = statsSummary,
            KeyPlayerMoments = keyPlayerMoments,
            TacticalExplanation = tacticalExplanation,
            PressureSummary = pressureSummary,
            KeyEvents = BuildKeyEvents(result, causeSummary)
        };
    }

    private static MatchCauseAnalysis Analyze(MatchPlaybackResult result)
    {
        var analysis = new MatchCauseAnalysis();
        var previousHomeScore = 0;
        var previousAwayScore = 0;

        analysis.HomeShots = result.Stats.HomeShots;
        analysis.AwayShots = result.Stats.AwayShots;
        analysis.HomeSaves = result.Stats.HomeSaves;
        analysis.AwaySaves = result.Stats.AwaySaves;
        analysis.HomeClearances = result.Stats.HomeClearances;
        analysis.AwayClearances = result.Stats.AwayClearances;
        analysis.HomeInterceptions = result.Stats.HomeInterceptions;
        analysis.AwayInterceptions = result.Stats.AwayInterceptions;
        analysis.HomeLateGoal = result.Stats.HomeLateGoals > 0;

        foreach (var action in result.Timeline.Actions)
        {
            switch (action.Kind)
            {
                case MatchActionKind.Goal:
                    if (previousHomeScore < previousAwayScore && action.HomeScoreAfter >= action.AwayScoreAfter)
                    {
                        analysis.Comeback = true;
                    }

                    if (previousHomeScore > previousAwayScore && action.HomeScoreAfter <= action.AwayScoreAfter)
                    {
                        analysis.Collapse = true;
                    }

                    previousHomeScore = action.HomeScoreAfter;
                    previousAwayScore = action.AwayScoreAfter;
                    break;
            }
        }

        return analysis;
    }

    private static string BuildCauseSummary(MatchPlaybackResult result, GameState state, MatchCauseAnalysis analysis)
    {
        var causes = new List<string>();
        causes.Add($"{analysis.HomeShots}-{analysis.AwayShots} shot count");

        if (analysis.HomeSaves > 0)
        {
            causes.Add($"{analysis.HomeSaves} saves protected the result");
        }

        if (analysis.HomeInterceptions > 0 || analysis.HomeClearances > 0)
        {
            causes.Add($"{analysis.HomeInterceptions + analysis.HomeClearances} defensive interventions");
        }

        if (analysis.HomeLateGoal)
        {
            causes.Add("late home goal shifted the mood");
        }

        if (analysis.Comeback)
        {
            causes.Add("comeback resilience");
        }
        else if (analysis.Collapse)
        {
            causes.Add("lead was not protected");
        }

        if (state.Risk >= 70)
        {
            causes.Add("high-risk plan raised volatility");
        }
        else if (state.PressIntensity >= 70)
        {
            causes.Add("pressing intensity shaped the match");
        }

        return string.Join("; ", causes);
    }

    private static string BuildStatsSummary(MatchStats stats)
    {
        return $"Shots: {stats.HomeShots}-{stats.AwayShots} | Saves: {stats.HomeSaves}-{stats.AwaySaves} | Interceptions: {stats.HomeInterceptions}-{stats.AwayInterceptions} | Passes: {stats.HomeCompletedPasses}-{stats.AwayCompletedPasses}";
    }

    private static string BuildTacticalExplanation(MatchPlaybackResult result, GameState state, MatchCauseAnalysis analysis)
    {
        var explanations = new List<string>();
        if (state.Risk >= 70)
        {
            explanations.Add(analysis.HomeShots >= analysis.AwayShots
                ? "The high-risk setup created shot volume but left transition pressure in the report."
                : "The high-risk setup did not convert into enough shots and increased exposure.");
        }

        if (state.PressIntensity >= 70)
        {
            explanations.Add(analysis.HomeInterceptions >= analysis.AwayInterceptions
                ? "The press generated enough interceptions to shape the match."
                : "The press did not create enough turnovers to control the flow.");
        }

        if (result.Stats.HomeLateGoals + result.Stats.AwayLateGoals > 0)
        {
            explanations.Add("A late goal changed the emotional read of the result.");
        }

        if (explanations.Count == 0)
        {
            explanations.Add("The report follows the action profile: shots, saves, turnovers, and possession chains.");
        }

        return string.Join(" ", explanations);
    }

    private static string BuildKeyPlayerMoments(MatchPlaybackResult result)
    {
        var moments = new List<string>();
        foreach (var action in result.Timeline.Actions)
        {
            if (action.Kind == MatchActionKind.Goal && !string.IsNullOrWhiteSpace(action.Participants.ScorerPlayerId))
            {
                moments.Add($"Goal: {ResolvePlayerName(result, action.Participants.ScorerPlayerId)} at {FormatMinute(action.StartSecond)}.");
            }
            else if (action.Kind == MatchActionKind.Save && !string.IsNullOrWhiteSpace(action.Participants.GoalkeeperPlayerId))
            {
                moments.Add($"Save: {ResolvePlayerName(result, action.Participants.GoalkeeperPlayerId)} stopped a shot at {FormatMinute(action.StartSecond)}.");
            }
            else if (action.Kind == MatchActionKind.Interception && !string.IsNullOrWhiteSpace(action.Participants.InterceptorPlayerId))
            {
                moments.Add($"Turnover: {ResolvePlayerName(result, action.Participants.InterceptorPlayerId)} intercepted at {FormatMinute(action.StartSecond)}.");
            }

            if (moments.Count == 3)
            {
                break;
            }
        }

        if (moments.Count == 0)
        {
            moments.Add("No standout participant moment separated from the action feed.");
        }

        return string.Join("\n", moments);
    }

    private static string BuildResultLabel(int goalDifference, string opponentName, MatchCauseAnalysis analysis)
    {
        if (goalDifference > 0)
        {
            return analysis.Comeback
                ? $"The comeback win over {opponentName} gives the dressing room a real lift."
                : $"Winning over {opponentName} lifts the mood around the club.";
        }

        if (goalDifference == 0)
        {
            return analysis.Collapse
                ? $"The draw with {opponentName} feels costly after surrendering control."
                : $"The draw with {opponentName} leaves the dressing room asking for more control.";
        }

        return analysis.Collapse
            ? $"{opponentName} punish the collapse and the pressure tightens."
            : $"{opponentName} leave with the points and the pressure tightens.";
    }

    private static string[] BuildKeyEvents(MatchPlaybackResult result, string causeSummary)
    {
        var count = Math.Min(4, result.EventFeed.Length);
        var recentEvents = new string[count + 1];

        for (var index = 0; index < count; index++)
        {
            recentEvents[index] = result.EventFeed[result.EventFeed.Length - count + index].Summary;
        }

        recentEvents[^1] = $"Cause: {causeSummary}";
        return recentEvents;
    }

    private static string ResolvePlayerName(MatchPlaybackResult result, string playerId)
    {
        foreach (var player in result.PlayerStates)
        {
            if (player.PlayerId == playerId)
            {
                return player.Name;
            }
        }

        return playerId;
    }

    private static string FormatMinute(int matchSecond)
    {
        return $"{Math.Max(1, (matchSecond / 60) + 1)}'";
    }

    private static string FormatSignedDelta(int delta)
    {
        return delta >= 0 ? $"+{delta}" : delta.ToString();
    }

    private sealed class MatchCauseAnalysis
    {
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }
        public int HomeSaves { get; set; }
        public int AwaySaves { get; set; }
        public int HomeClearances { get; set; }
        public int AwayClearances { get; set; }
        public int HomeInterceptions { get; set; }
        public int AwayInterceptions { get; set; }
        public bool HomeLateGoal { get; set; }
        public bool Comeback { get; set; }
        public bool Collapse { get; set; }
    }
}
