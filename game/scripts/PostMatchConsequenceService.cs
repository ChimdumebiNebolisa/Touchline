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
            PressureSummary = pressureSummary,
            KeyEvents = BuildKeyEvents(result, causeSummary)
        };
    }

    private static MatchCauseAnalysis Analyze(MatchPlaybackResult result)
    {
        var analysis = new MatchCauseAnalysis();
        var previousHomeScore = 0;
        var previousAwayScore = 0;

        foreach (var action in result.Timeline.Actions)
        {
            var isHomeAction = action.Team == result.HomeClubName;
            switch (action.Kind)
            {
                case MatchActionKind.Shot:
                    if (isHomeAction)
                    {
                        analysis.HomeShots++;
                    }
                    else
                    {
                        analysis.AwayShots++;
                    }
                    break;
                case MatchActionKind.Save:
                    if (isHomeAction)
                    {
                        analysis.HomeSaves++;
                    }
                    else
                    {
                        analysis.AwaySaves++;
                    }
                    break;
                case MatchActionKind.Clearance:
                    if (isHomeAction)
                    {
                        analysis.HomeClearances++;
                    }
                    else
                    {
                        analysis.AwayClearances++;
                    }
                    break;
                case MatchActionKind.Interception:
                    if (isHomeAction)
                    {
                        analysis.HomeInterceptions++;
                    }
                    else
                    {
                        analysis.AwayInterceptions++;
                    }
                    break;
                case MatchActionKind.Goal:
                    if (action.StartSecond >= 75 * 60 && isHomeAction)
                    {
                        analysis.HomeLateGoal = true;
                    }

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
