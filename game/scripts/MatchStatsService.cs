using System;

public static class MatchStatsService
{
    public static MatchStats Build(string homeClubName, string awayClubName, MatchAction[] actions)
    {
        var homeShots = 0;
        var awayShots = 0;
        var homeGoals = 0;
        var awayGoals = 0;
        var homeSaves = 0;
        var awaySaves = 0;
        var homeClearances = 0;
        var awayClearances = 0;
        var homeInterceptions = 0;
        var awayInterceptions = 0;
        var homePasses = 0;
        var awayPasses = 0;
        var homePhases = 0;
        var awayPhases = 0;
        var homeLateGoals = 0;
        var awayLateGoals = 0;
        var homeBigChances = 0;
        var awayBigChances = 0;
        var homeFouls = 0;
        var awayFouls = 0;
        var homeYellowCards = 0;
        var awayYellowCards = 0;
        var homeInjuryConcerns = 0;
        var awayInjuryConcerns = 0;
        var homeFatigueWarnings = 0;
        var awayFatigueWarnings = 0;
        var tacticalShiftEvents = 0;
        var pressureTurnovers = 0;
        var longestChain = 0;
        var currentChain = 0;
        string? previousPossessionTeam = null;

        foreach (var action in actions)
        {
            if (action.Kind == MatchActionKind.Reset)
            {
                previousPossessionTeam = null;
                currentChain = 0;
                continue;
            }

            if (action.Team != previousPossessionTeam && IsPossessionAction(action.Kind))
            {
                if (action.Team == homeClubName)
                {
                    homePhases++;
                }
                else if (action.Team == awayClubName)
                {
                    awayPhases++;
                }

                previousPossessionTeam = action.Team;
                currentChain = 0;
            }

            if (IsPossessionAction(action.Kind))
            {
                currentChain++;
                longestChain = Math.Max(longestChain, currentChain);
            }

            var isHomeAction = action.Team == homeClubName;
            switch (action.Kind)
            {
                case MatchActionKind.BigChance:
                    if (isHomeAction)
                    {
                        homeBigChances++;
                    }
                    else
                    {
                        awayBigChances++;
                    }
                    break;
                case MatchActionKind.Pass:
                    if (isHomeAction)
                    {
                        homePasses++;
                    }
                    else
                    {
                        awayPasses++;
                    }
                    break;
                case MatchActionKind.Shot:
                    if (isHomeAction)
                    {
                        homeShots++;
                    }
                    else
                    {
                        awayShots++;
                    }
                    break;
                case MatchActionKind.Goal:
                    if (isHomeAction)
                    {
                        homeGoals++;
                        if (action.StartSecond >= 75 * 60)
                        {
                            homeLateGoals++;
                        }
                    }
                    else
                    {
                        awayGoals++;
                        if (action.StartSecond >= 75 * 60)
                        {
                            awayLateGoals++;
                        }
                    }
                    break;
                case MatchActionKind.Save:
                    if (isHomeAction)
                    {
                        homeSaves++;
                    }
                    else
                    {
                        awaySaves++;
                    }
                    break;
                case MatchActionKind.Clearance:
                    if (isHomeAction)
                    {
                        homeClearances++;
                    }
                    else
                    {
                        awayClearances++;
                    }
                    break;
                case MatchActionKind.Interception:
                    pressureTurnovers++;
                    currentChain = 1;
                    previousPossessionTeam = action.Team;
                    if (isHomeAction)
                    {
                        homeInterceptions++;
                    }
                    else
                    {
                        awayInterceptions++;
                    }
                    break;
                case MatchActionKind.Foul:
                    if (isHomeAction)
                    {
                        homeFouls++;
                    }
                    else
                    {
                        awayFouls++;
                    }
                    break;
                case MatchActionKind.YellowCard:
                    if (isHomeAction)
                    {
                        homeYellowCards++;
                    }
                    else
                    {
                        awayYellowCards++;
                    }
                    break;
                case MatchActionKind.InjuryConcern:
                    if (isHomeAction)
                    {
                        homeInjuryConcerns++;
                    }
                    else
                    {
                        awayInjuryConcerns++;
                    }
                    break;
                case MatchActionKind.FatigueWarning:
                    if (isHomeAction)
                    {
                        homeFatigueWarnings++;
                    }
                    else
                    {
                        awayFatigueWarnings++;
                    }
                    break;
                case MatchActionKind.TacticalShift:
                    tacticalShiftEvents++;
                    break;
            }
        }

        return new MatchStats
        {
            HomeShots = homeShots,
            AwayShots = awayShots,
            HomeGoals = homeGoals,
            AwayGoals = awayGoals,
            HomeSaves = homeSaves,
            AwaySaves = awaySaves,
            HomeClearances = homeClearances,
            AwayClearances = awayClearances,
            HomeInterceptions = homeInterceptions,
            AwayInterceptions = awayInterceptions,
            HomePossessionPhaseCount = homePhases,
            AwayPossessionPhaseCount = awayPhases,
            HomeCompletedPasses = homePasses,
            AwayCompletedPasses = awayPasses,
            HomeLateGoals = homeLateGoals,
            AwayLateGoals = awayLateGoals,
            HomeBigChances = homeBigChances,
            AwayBigChances = awayBigChances,
            HomeFouls = homeFouls,
            AwayFouls = awayFouls,
            HomeYellowCards = homeYellowCards,
            AwayYellowCards = awayYellowCards,
            HomeInjuryConcerns = homeInjuryConcerns,
            AwayInjuryConcerns = awayInjuryConcerns,
            HomeFatigueWarnings = homeFatigueWarnings,
            AwayFatigueWarnings = awayFatigueWarnings,
            TacticalShiftEvents = tacticalShiftEvents,
            PressureTurnovers = pressureTurnovers,
            LongestPossessionChain = longestChain
        };
    }

    private static bool IsPossessionAction(MatchActionKind kind)
    {
        return kind is MatchActionKind.Kickoff
            or MatchActionKind.Pass
            or MatchActionKind.Carry
            or MatchActionKind.BigChance
            or MatchActionKind.Shot
            or MatchActionKind.Goal
            or MatchActionKind.Interception;
    }
}
