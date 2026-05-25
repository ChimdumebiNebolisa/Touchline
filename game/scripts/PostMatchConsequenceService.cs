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
    public required string TacticalSection { get; init; }
    public required string PlayerFitSection { get; init; }
    public required string FatigueSection { get; init; }
    public required string MoraleSection { get; init; }
    public required string BoardReactionSection { get; init; }
    public required string FanReactionSection { get; init; }
    public required string MediaStorySection { get; init; }
    public required string StaffAnalysisSection { get; init; }
    public required string DevelopmentNotesSection { get; init; }
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
            $"Club pressure now sits at morale {Math.Clamp(state.TeamMorale + moraleDelta, 0, 100)}, fan trust {Math.Clamp(state.FanSentiment + fanDelta, 0, 100)}, and board confidence {Math.Clamp(state.BoardConfidence + boardDelta, 0, 100)}. Categories before update: {state.PressureCategorySummary}. Cause: {causeSummary}";
        var resultLabel = BuildResultLabel(goalDifference, result.AwayClubName, analysis);

        return new PostMatchConsequenceResult
        {
            MoraleDelta = moraleDelta,
            FanDelta = fanDelta,
            BoardDelta = boardDelta,
            ResultLabel = resultLabel,
            ConsequenceSummary =
                $"Morale {FormatSignedDelta(moraleDelta)} | Fans {FormatSignedDelta(fanDelta)} | Board {FormatSignedDelta(boardDelta)} | Cause: {causeSummary}",
            CauseSummary = causeSummary,
            StatsSummary = statsSummary,
            KeyPlayerMoments = keyPlayerMoments,
            TacticalExplanation = tacticalExplanation,
            PressureSummary = pressureSummary,
            TacticalSection = BuildTacticalSection(result, state, analysis),
            PlayerFitSection = BuildPlayerFitSection(result, state),
            FatigueSection = BuildFatigueSection(result),
            MoraleSection = BuildMoraleSection(state, moraleDelta, fanDelta, boardDelta),
            BoardReactionSection = BuildBoardReactionSection(state, boardDelta, causeSummary),
            FanReactionSection = BuildFanReactionSection(state, fanDelta, result),
            MediaStorySection = BuildMediaStorySection(result, resultLabel),
            StaffAnalysisSection = BuildStaffAnalysisSection(result, state),
            DevelopmentNotesSection = BuildDevelopmentNotesSection(result, state),
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

        if (result.Stats.HomeBigChances > 0 || result.Stats.AwayBigChances > 0)
        {
            causes.Add($"{result.Stats.HomeBigChances}-{result.Stats.AwayBigChances} big-chance count");
        }

        if (result.Stats.HomeYellowCards > 0 || result.Stats.AwayYellowCards > 0)
        {
            causes.Add($"{result.Stats.HomeYellowCards + result.Stats.AwayYellowCards} yellow card(s) changed the discipline picture");
        }

        if (result.Stats.HomeFatigueWarnings > 0 || result.Stats.HomeInjuryConcerns > 0)
        {
            causes.Add("late condition warnings affected execution");
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

        if (state.TacticalRoleFitScore >= 74)
        {
            causes.Add("role fit supported the plan");
        }
        else if (state.TacticalRoleFitScore <= 52)
        {
            causes.Add("role-fit concerns limited execution");
        }

        if (state.CurrentOpponentPreparationFocus != OpponentPreparationFocus.BalancedBrief)
        {
            causes.Add($"{state.OpponentPreparationFocusName.ToLowerInvariant()} opponent prep shaped staff instructions");
        }

        return string.Join("; ", causes);
    }

    private static string BuildStatsSummary(MatchStats stats)
    {
        return $"Shots: {stats.HomeShots}-{stats.AwayShots} | Big chances: {stats.HomeBigChances}-{stats.AwayBigChances} | Saves: {stats.HomeSaves}-{stats.AwaySaves} | Interceptions: {stats.HomeInterceptions}-{stats.AwayInterceptions} | Passes: {stats.HomeCompletedPasses}-{stats.AwayCompletedPasses} | Cards: {stats.HomeYellowCards}-{stats.AwayYellowCards} | Fatigue: {stats.HomeFatigueWarnings}-{stats.AwayFatigueWarnings}";
    }

    private static string BuildTacticalExplanation(MatchPlaybackResult result, GameState state, MatchCauseAnalysis analysis)
    {
        var explanations = new List<string>();
        if (!string.IsNullOrWhiteSpace(result.TacticalExplanation))
        {
            explanations.Add(result.TacticalExplanation);
        }

        if (result.TacticalCauses.Length > 0)
        {
            explanations.Add($"Tactical cause records: {string.Join("; ", Array.ConvertAll(result.TacticalCauses, cause => $"{cause.Category} {cause.HomeImpact:+0;-0;0}"))}.");
        }

        if (!string.IsNullOrWhiteSpace(result.OpponentStyleSummary))
        {
            explanations.Add(result.OpponentStyleSummary);
        }

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
        if (!string.IsNullOrWhiteSpace(result.PlayerRatingsSummary))
        {
            moments.Add(result.PlayerRatingsSummary);
        }

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

            if (moments.Count == 4)
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

    private static string BuildTacticalSection(MatchPlaybackResult result, GameState state, MatchCauseAnalysis analysis)
    {
        var causeText = result.TacticalCauses.Length == 0
            ? "No separate tactical cause record was isolated from the timeline."
            : string.Join("; ", Array.ConvertAll(
                result.TacticalCauses,
                cause => $"{cause.Category}: {cause.Summary} ({cause.HomeImpact:+0;-0;0})"));
        var turnoverRead = analysis.HomeInterceptions >= analysis.AwayInterceptions
            ? "turnover pressure held up"
            : "turnover pressure did not outpace the opponent";
        return $"Tactical section: {state.TacticalFormation} {state.TeamStyleName} with {state.TacticalFamiliarityName.ToLowerInvariant()} familiarity; {turnoverRead}. Causes: {causeText}";
    }

    private static string BuildPlayerFitSection(MatchPlaybackResult result, GameState state)
    {
        var best = FindExtremeRating(result, true);
        var weakest = FindExtremeRating(result, false);
        var bestText = best == null ? "No individual rating leader was recorded." : $"{best.Name} {best.Rating:0.0}: {best.Note}";
        var weakText = weakest == null || best == weakest ? "No separate weak-fit rating stood out." : $"{weakest.Name} {weakest.Rating:0.0}: {weakest.Note}";
        return $"Player fit section: {state.TacticalRoleFitSummary} Familiarity: {state.PlayerFamiliaritySummary}. Top note: {bestText}. Watch note: {weakText}";
    }

    private static string BuildFatigueSection(MatchPlaybackResult result)
    {
        var conditionEvents = result.Stats.HomeFatigueWarnings + result.Stats.AwayFatigueWarnings +
            result.Stats.HomeInjuryConcerns + result.Stats.AwayInjuryConcerns;
        var conditionRead = conditionEvents == 0
            ? "No late fatigue or injury-warning events were recorded."
            : $"{result.Stats.HomeFatigueWarnings}-{result.Stats.AwayFatigueWarnings} fatigue warnings and {result.Stats.HomeInjuryConcerns}-{result.Stats.AwayInjuryConcerns} injury concerns came from the timeline.";
        return $"Fatigue section: {conditionRead} {result.DisciplineSummary}";
    }

    private static string BuildMoraleSection(GameState state, int moraleDelta, int fanDelta, int boardDelta)
    {
        return $"Morale section: squad {state.TeamMorale}->{Math.Clamp(state.TeamMorale + moraleDelta, 0, 100)} ({FormatSignedDelta(moraleDelta)}), fans {state.FanSentiment}->{Math.Clamp(state.FanSentiment + fanDelta, 0, 100)} ({FormatSignedDelta(fanDelta)}), board {state.BoardConfidence}->{Math.Clamp(state.BoardConfidence + boardDelta, 0, 100)} ({FormatSignedDelta(boardDelta)}).";
    }

    private static string BuildBoardReactionSection(GameState state, int boardDelta, string causeSummary)
    {
        var lens = state.CurrentClub?.BoardPhilosophy switch
        {
            BoardPhilosophy.WinNowBoard => "result-first standards",
            BoardPhilosophy.FinanciallyStrictBoard => "risk control and wage discipline",
            BoardPhilosophy.YouthDevelopmentBoard => "development path and patience",
            BoardPhilosophy.DataDrivenBoard => "evidence from chances, fit, and value",
            BoardPhilosophy.TriggerHappyBoard => "short patience under pressure",
            _ => "longer-term context"
        };
        return $"Board reaction: {state.BoardPhilosophyName} applies {lens}; board movement {FormatSignedDelta(boardDelta)} because {causeSummary}.";
    }

    private static string BuildFanReactionSection(GameState state, int fanDelta, MatchPlaybackResult result)
    {
        var lens = state.CurrentClub?.FanCulture switch
        {
            FanCulture.AttackingFootball => "chance creation and ambition",
            FanCulture.DefensiveGrit => "defensive interventions and resilience",
            FanCulture.AcademyLoyalists => "young-player pathway and identity",
            FanCulture.AntiSellingFans => "commitment to the squad",
            FanCulture.DerbyObsessed => "emotion and rivalry stakes",
            FanCulture.ResultsFirst => "the result before the process",
            _ => "club identity and effort"
        };
        return $"Fan reaction: {state.FanCultureName} judges {lens}; fans moved {FormatSignedDelta(fanDelta)} after a {result.FinalHomeScore}-{result.FinalAwayScore} result.";
    }

    private static string BuildMediaStorySection(MatchPlaybackResult result, string resultLabel)
    {
        var headline = result.FinalHomeScore >= result.FinalAwayScore
            ? $"{result.HomeClubName} take a result with {result.Stats.HomeBigChances} big chances"
            : $"{result.AwayClubName} expose {result.HomeClubName} despite {result.Stats.HomeShots} home shots";
        return $"Media story: {headline}. Narrative: {resultLabel} Discipline/fatigue angle: {result.DisciplineSummary}";
    }

    private static string BuildStaffAnalysisSection(MatchPlaybackResult result, GameState state)
    {
        var coachQuality = GetStaffQuality(state, StaffRole.FirstTeamCoach);
        var analystQuality = GetStaffQuality(state, StaffRole.DataAnalyst);
        var detailLevel = (coachQuality + analystQuality) / 2 >= 68 ? "high-confidence" : "limited-confidence";
        var cause = result.TacticalCauses.Length > 0 ? result.TacticalCauses[0].Summary : result.TacticalSummary;
        return $"Staff analysis: {detailLevel} review from coach {coachQuality}/100 and analyst {analystQuality}/100. Lead staff point: {cause}";
    }

    private static string BuildDevelopmentNotesSection(MatchPlaybackResult result, GameState state)
    {
        var youngPlayer = FindYoungDevelopmentPlayer(state);
        var youngText = youngPlayer == null
            ? "No young player development note was isolated."
            : $"{youngPlayer.Name}: {youngPlayer.DevelopmentCurve}";
        return $"Development notes: tactical familiarity review starts from {state.TacticalFamiliarityName}; form and condition changes were applied from the shared timeline. Youth/development note: {youngText}. Rating basis: {result.PlayerRatingsSummary}";
    }

    private static PlayerMatchRating? FindExtremeRating(MatchPlaybackResult result, bool highest)
    {
        PlayerMatchRating? selected = null;
        foreach (var rating in result.PlayerRatings)
        {
            if (rating.Team != result.HomeClubName)
            {
                continue;
            }

            if (selected == null ||
                (highest && rating.Rating > selected.Rating) ||
                (!highest && rating.Rating < selected.Rating))
            {
                selected = rating;
            }
        }

        return selected;
    }

    private static GameState.SquadPlayer? FindYoungDevelopmentPlayer(GameState state)
    {
        GameState.SquadPlayer? selected = null;
        foreach (var player in state.SquadPlayers)
        {
            if (player.Age > 23)
            {
                continue;
            }

            if (selected == null || player.Age < selected.Age || player.TrueAbility > selected.TrueAbility)
            {
                selected = player;
            }
        }

        return selected;
    }

    private static int GetStaffQuality(GameState state, StaffRole role)
    {
        if (state.CurrentClub == null)
        {
            return 55;
        }

        foreach (var staff in state.CurrentClub.Staff)
        {
            if (staff.Role == role)
            {
                return staff.Quality;
            }
        }

        return 55;
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
