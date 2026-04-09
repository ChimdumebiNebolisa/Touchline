using Godot;
using System;
using System.Collections.Generic;

public sealed class MatchSimulationResult
{
    public sealed class MarkerState
    {
        public required string FullName { get; init; }
        public required string Initials { get; init; }
        public required bool IsHome { get; init; }
        public required Vector2 Anchor { get; init; }
        public required float SwingX { get; init; }
        public required float SwingY { get; init; }
        public required float Speed { get; init; }
        public required float Phase { get; init; }
    }

    public sealed class MatchEvent
    {
        public required int Minute { get; init; }
        public required string Summary { get; init; }
        public required int HomeScore { get; init; }
        public required int AwayScore { get; init; }
    }

    public required string HomeClubName { get; init; }
    public required string AwayClubName { get; init; }
    public required string TacticalSummary { get; init; }
    public required MarkerState[] Markers { get; init; }
    public required MatchEvent[] Events { get; init; }
    public int HomeGoals => Events.Length == 0 ? 0 : Events[^1].HomeScore;
    public int AwayGoals => Events.Length == 0 ? 0 : Events[^1].AwayScore;
}

public static class MatchSimulator
{
    public static MatchSimulationResult Simulate(GameState state)
    {
        var rng = new Random(state.WorldSeed * 31 + state.CurrentMatchday * 17 + state.PressIntensity + state.Risk);

        var homeLineup = BuildHomeLineup(state);
        var awayLineup = BuildAwayLineup();
        var homeAnchors = BuildFormationAnchors(false);
        var awayAnchors = BuildFormationAnchors(true);
        var markers = new List<MatchSimulationResult.MarkerState>(22);

        for (var index = 0; index < homeLineup.Count; index++)
        {
            markers.Add(CreateMarker(homeLineup[index], true, homeAnchors[index], rng, index));
        }

        for (var index = 0; index < awayLineup.Count; index++)
        {
            markers.Add(CreateMarker(awayLineup[index], false, awayAnchors[index], rng, index + 11));
        }

        var homeGoals = Math.Clamp((state.PressIntensity + state.Tempo + state.Risk - 150) / 35 + rng.Next(0, 2), 0, 3);
        var awayGoals = Math.Clamp((150 - state.Width) / 45 + rng.Next(0, 2), 0, 2);

        if (homeGoals == 0 && awayGoals == 0)
        {
            homeGoals = 1;
        }

        var events = BuildEvents(state, homeLineup, awayLineup, homeGoals, awayGoals, rng);

        return new MatchSimulationResult
        {
            HomeClubName = state.SelectedClubName ?? "Home",
            AwayClubName = state.CurrentOpponentName,
            TacticalSummary =
                $"Shape {state.TacticalFormation} | Press {state.PressIntensity} | Tempo {state.Tempo} | Width {state.Width} | Risk {state.Risk}",
            Markers = markers.ToArray(),
            Events = events.ToArray()
        };
    }

    private static List<string> BuildHomeLineup(GameState state)
    {
        var names = new List<string>(11);

        foreach (var player in state.SquadPlayers)
        {
            if (player.IsStarting)
            {
                names.Add(player.Name);
            }

            if (names.Count == 11)
            {
                break;
            }
        }

        foreach (var player in state.SquadPlayers)
        {
            if (names.Count == 11)
            {
                break;
            }

            if (!names.Contains(player.Name))
            {
                names.Add(player.Name);
            }
        }

        return names;
    }

    private static List<string> BuildAwayLineup()
    {
        return new List<string>
        {
            "Roman Ivic",
            "Maksym Hale",
            "Victor Salcedo",
            "Pavel Drago",
            "Nico Barros",
            "Ilyas Cherif",
            "Samir Gashi",
            "Tom Bisset",
            "Leandro Pires",
            "Bruno Keita",
            "Yuri Markovic"
        };
    }

    private static Vector2[] BuildFormationAnchors(bool mirror)
    {
        var anchors = new[]
        {
            new Vector2(0.10f, 0.50f),
            new Vector2(0.24f, 0.20f),
            new Vector2(0.20f, 0.38f),
            new Vector2(0.20f, 0.62f),
            new Vector2(0.24f, 0.80f),
            new Vector2(0.40f, 0.30f),
            new Vector2(0.36f, 0.50f),
            new Vector2(0.40f, 0.70f),
            new Vector2(0.60f, 0.22f),
            new Vector2(0.68f, 0.50f),
            new Vector2(0.60f, 0.78f)
        };

        if (!mirror)
        {
            return anchors;
        }

        var mirrored = new Vector2[anchors.Length];
        for (var index = 0; index < anchors.Length; index++)
        {
            mirrored[index] = new Vector2(1.0f - anchors[index].X, anchors[index].Y);
        }

        return mirrored;
    }

    private static MatchSimulationResult.MarkerState CreateMarker(string fullName, bool isHome, Vector2 anchor, Random rng, int index)
    {
        return new MatchSimulationResult.MarkerState
        {
            FullName = fullName,
            Initials = BuildInitials(fullName),
            IsHome = isHome,
            Anchor = anchor,
            SwingX = 0.014f + (float)rng.NextDouble() * 0.018f,
            SwingY = 0.010f + (float)rng.NextDouble() * 0.015f,
            Speed = 0.70f + (float)rng.NextDouble() * 0.55f,
            Phase = index * 0.55f + (float)rng.NextDouble()
        };
    }

    private static List<MatchSimulationResult.MatchEvent> BuildEvents(
        GameState state,
        IReadOnlyList<string> homeLineup,
        IReadOnlyList<string> awayLineup,
        int homeGoals,
        int awayGoals,
        Random rng)
    {
        var events = new List<MatchSimulationResult.MatchEvent>();
        var homeScore = 0;
        var awayScore = 0;

        events.Add(new MatchSimulationResult.MatchEvent
        {
            Minute = 1,
            Summary = $"1' Kick-off. {state.SelectedClubName} open in a {state.TacticalFormation} and look to set the tempo.",
            HomeScore = homeScore,
            AwayScore = awayScore
        });

        var openingChanceMinute = 9 + rng.Next(0, 5);
        events.Add(new MatchSimulationResult.MatchEvent
        {
            Minute = openingChanceMinute,
            Summary = $"{openingChanceMinute}' Early warning. {homeLineup[8]} stretches the back line and forces a hurried clearance.",
            HomeScore = homeScore,
            AwayScore = awayScore
        });

        var bookedMinute = 34 + rng.Next(0, 10);
        events.Add(new MatchSimulationResult.MatchEvent
        {
            Minute = bookedMinute,
            Summary = $"{bookedMinute}' Midfield collision. {awayLineup[6]} goes into the book after stopping a transition.",
            HomeScore = homeScore,
            AwayScore = awayScore
        });

        var goalMinutes = new List<int>();
        for (var index = 0; index < homeGoals + awayGoals; index++)
        {
            goalMinutes.Add(18 + rng.Next(index * 12, index * 12 + 12));
        }

        goalMinutes.Sort();

        foreach (var minute in goalMinutes)
        {
            var homeNext = homeGoals > 0 && (awayGoals == 0 || rng.NextDouble() > 0.42);
            if (homeNext)
            {
                homeGoals--;
                homeScore++;
                var scorer = homeLineup[8 + rng.Next(0, 3)];
                events.Add(new MatchSimulationResult.MatchEvent
                {
                    Minute = minute,
                    Summary = $"{minute}' Goal {state.SelectedClubName}. {scorer} finishes a move worked through the right half-space.",
                    HomeScore = homeScore,
                    AwayScore = awayScore
                });
            }
            else
            {
                awayGoals--;
                awayScore++;
                var scorer = awayLineup[8 + rng.Next(0, 3)];
                events.Add(new MatchSimulationResult.MatchEvent
                {
                    Minute = minute,
                    Summary = $"{minute}' Goal {state.CurrentOpponentName}. {scorer} sneaks in at the far post after sustained pressure.",
                    HomeScore = homeScore,
                    AwayScore = awayScore
                });
            }
        }

        var lateMinute = 82 + rng.Next(0, 7);
        events.Add(new MatchSimulationResult.MatchEvent
        {
            Minute = lateMinute,
            Summary = $"{lateMinute}' Late surge. {homeLineup[6]} keeps the ball alive and the crowd sense a final push.",
            HomeScore = homeScore,
            AwayScore = awayScore
        });

        events.Sort((left, right) => left.Minute.CompareTo(right.Minute));

        return events;
    }

    private static string BuildInitials(string fullName)
    {
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return "P";
        }

        if (parts.Length == 1)
        {
            return parts[0][0].ToString().ToUpperInvariant();
        }

        return string.Concat(parts[0][0], parts[^1][0]).ToUpperInvariant();
    }
}
