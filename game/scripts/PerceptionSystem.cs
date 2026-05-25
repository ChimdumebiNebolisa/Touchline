using System;

public static class PerceptionSystem
{
    public static string BuildPressureReasonSummary(GameState state)
    {
        var boardReason = BuildBoardReason(state);
        var fanReason = BuildFanReason(state);
        var dressingRoomReason = BuildDressingRoomReason(state);
        return $"Board: {boardReason} Fans: {fanReason} Dressing room: {dressingRoomReason}\n{state.TrustSummary}\n{state.ReputationSummary}\n{state.PressureCategorySummary}\nRecent perception: {state.PerceptionHistorySummary}";
    }

    private static string BuildBoardReason(GameState state)
    {
        var tablePosition = FindTablePosition(state);
        if (state.BoardMorale >= 70)
        {
            return tablePosition > 0
                ? $"morale is strong because the club sits {tablePosition}th and the season plan looks on track."
                : "morale is strong because results have kept the plan on track.";
        }

        if (state.BoardMorale >= 55)
        {
            return tablePosition > 0
                ? $"morale is steady, but the board still expects movement from position {tablePosition}."
                : "morale is steady, but the board still wants cleaner execution.";
        }

        return tablePosition > 0
            ? $"morale is tightening because position {tablePosition} is below the current expectation line."
            : "morale is tightening because the board does not trust the current run.";
    }

    private static string BuildFanReason(GameState state)
    {
        var recentRun = state.RecentResults.Length == 0 ? "no recent reference line yet" : string.Join(" ", state.RecentResults);
        if (state.FanMorale >= 70)
        {
            return $"supporters are leaning in because the recent run ({recentRun}) feels positive.";
        }

        if (state.FanMorale >= 55)
        {
            return $"supporters are still with the team, but the recent run ({recentRun}) has not settled them fully.";
        }

        return $"supporters are restless and the recent run ({recentRun}) is keeping that pressure alive.";
    }

    private static string BuildDressingRoomReason(GameState state)
    {
        if (state.SquadMorale >= 70)
        {
            return "the group believes the current plan is working and the mood is carrying training intensity.";
        }

        if (state.SquadMorale >= 55)
        {
            return "the group is holding together, but the next result still matters for belief.";
        }

        return "belief is fragile and another setback would make selection and matchday decisions heavier.";
    }

    private static int FindTablePosition(GameState state)
    {
        for (var index = 0; index < state.CompetitionTable.Length; index++)
        {
            if (state.CompetitionTable[index].ClubName == state.SelectedClubName)
            {
                return index + 1;
            }
        }

        return -1;
    }
}
