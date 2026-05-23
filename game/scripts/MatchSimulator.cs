using Godot;
using System;
using System.Collections.Generic;

public static class MatchSimulator
{
    private const int MatchDurationSeconds = 90 * 60;
    private const int FrameStepSeconds = 10;

    private sealed class RuntimePlayer
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public required string Team { get; init; }
        public required string Role { get; init; }
        public required bool IsHome { get; init; }
        public required int ShapeIndex { get; init; }
    }

    public static MatchPlaybackResult Simulate(GameState state)
    {
        var rng = new Random(state.WorldSeed * 31 + state.CurrentMatchday * 17 + state.PressIntensity + state.Risk);
        var homeClubName = state.SelectedClubName ?? "Home";
        var awayClubName = state.CurrentOpponentName;
        var tacticalShape = BuildTacticalShape(state.TacticalFormation, state.Width);
        var homeLineup = BuildHomeLineup(state, homeClubName);
        var awayLineup = BuildAwayLineup(awayClubName);
        var players = new List<RuntimePlayer>(22);
        players.AddRange(homeLineup);
        players.AddRange(awayLineup);

        var plannedHomeGoals = Math.Clamp((state.PressIntensity + state.Tempo + state.Risk - 150) / 35 + rng.Next(0, 2), 0, 3);
        var plannedAwayGoals = Math.Clamp((150 - state.Width) / 45 + rng.Next(0, 2), 0, 2);
        if (plannedHomeGoals == 0 && plannedAwayGoals == 0)
        {
            plannedHomeGoals = 1;
        }

        var actions = BuildActions(
            homeClubName,
            awayClubName,
            players,
            tacticalShape,
            plannedHomeGoals,
            plannedAwayGoals,
            rng);
        var frames = BuildFrames(homeClubName, awayClubName, players, tacticalShape, actions);
        var events = BuildEvents(homeClubName, awayClubName, actions, frames);
        frames = AttachEventsToFrames(frames, events);
        var finalFrame = frames[^1];

        return new MatchPlaybackResult
        {
            HomeClubName = homeClubName,
            AwayClubName = awayClubName,
            TacticalSummary =
                $"Shape {state.TacticalFormation} | Press {state.PressIntensity} | Tempo {state.Tempo} | Width {state.Width} | Risk {state.Risk}",
            FinalHomeScore = finalFrame.HomeScore,
            FinalAwayScore = finalFrame.AwayScore,
            Timeline = new MatchTimeline
            {
                DurationSeconds = MatchDurationSeconds,
                Frames = frames,
                Actions = actions
            },
            EventFeed = events,
            PlayerStates = finalFrame.PlayerStates,
            BallState = finalFrame.Ball,
            PossessionTeam = finalFrame.PossessionTeam,
            ActionLabels = Array.ConvertAll(actions, action => action.Label),
            FinalResultSummary = $"{homeClubName} {finalFrame.HomeScore} - {finalFrame.AwayScore} {awayClubName}"
        };
    }

    private static RuntimePlayer[] BuildHomeLineup(GameState state, string teamName)
    {
        var selectedPlayers = new List<GameState.SquadPlayer>(11);
        foreach (var player in state.SquadPlayers)
        {
            if (player.IsStarting)
            {
                selectedPlayers.Add(player);
            }

            if (selectedPlayers.Count == 11)
            {
                break;
            }
        }

        foreach (var player in state.SquadPlayers)
        {
            if (selectedPlayers.Count == 11)
            {
                break;
            }

            if (!selectedPlayers.Exists(candidate => candidate.Name == player.Name))
            {
                selectedPlayers.Add(player);
            }
        }

        if (selectedPlayers.Count == 0)
        {
            selectedPlayers.Add(new GameState.SquadPlayer
            {
                Name = teamName,
                Position = "CM",
                Age = 24,
                Form = 65,
                Morale = 65,
                Fitness = 80,
                IsStarting = true
            });
        }

        var rotationIndex = 0;
        while (selectedPlayers.Count < 11)
        {
            selectedPlayers.Add(selectedPlayers[rotationIndex % selectedPlayers.Count]);
            rotationIndex++;
        }

        var lineup = new RuntimePlayer[11];
        for (var index = 0; index < lineup.Length; index++)
        {
            lineup[index] = new RuntimePlayer
            {
                Id = $"home-{index:00}",
                Name = selectedPlayers[index].Name,
                Team = teamName,
                Role = selectedPlayers[index].Position,
                IsHome = true,
                ShapeIndex = index
            };
        }

        return lineup;
    }

    private static RuntimePlayer[] BuildAwayLineup(string teamName)
    {
        var names = new[]
        {
            ("Roman Ivic", "GK"),
            ("Maksym Hale", "RB"),
            ("Victor Salcedo", "CB"),
            ("Pavel Drago", "CB"),
            ("Nico Barros", "LB"),
            ("Ilyas Cherif", "CM"),
            ("Samir Gashi", "CM"),
            ("Tom Bisset", "AM"),
            ("Leandro Pires", "RW"),
            ("Bruno Keita", "ST"),
            ("Yuri Markovic", "LW")
        };

        var lineup = new RuntimePlayer[11];
        for (var index = 0; index < lineup.Length; index++)
        {
            lineup[index] = new RuntimePlayer
            {
                Id = $"away-{index:00}",
                Name = names[index].Item1,
                Team = teamName,
                Role = names[index].Item2,
                IsHome = false,
                ShapeIndex = index
            };
        }

        return lineup;
    }

    private static MatchAction[] BuildActions(
        string homeClubName,
        string awayClubName,
        IReadOnlyList<RuntimePlayer> players,
        TacticalShape shape,
        int plannedHomeGoals,
        int plannedAwayGoals,
        Random rng)
    {
        var actions = new List<MatchAction>();
        var homeGoalsRemaining = plannedHomeGoals;
        var awayGoalsRemaining = plannedAwayGoals;
        var homeScore = 0;
        var awayScore = 0;
        var actionIndex = 1;

        var kickoffPlayer = Pick(players, homeClubName, 6);
        AddAction(
            actions,
            ref actionIndex,
            MatchActionKind.Kickoff,
            0,
            12,
            homeClubName,
            "Kickoff",
            kickoffPlayer,
            kickoffPlayer,
            new Vector2(0.50f, 0.50f),
            GetShapePosition(kickoffPlayer, shape, true),
            homeScore,
            awayScore);

        var phaseStart = 270;
        for (var phase = 0; phase < 15; phase++)
        {
            var forceHomeGoal = homeGoalsRemaining > 0 && (phase == 1 || phase == 5 || (phase == 10 && awayGoalsRemaining == 0));
            var forceAwayGoal = awayGoalsRemaining > 0 && !forceHomeGoal && (phase == 3 || phase == 8 || phase == 12);
            var possessionTeam = forceHomeGoal
                ? homeClubName
                : forceAwayGoal
                    ? awayClubName
                    : rng.NextDouble() < 0.55 ? homeClubName : awayClubName;
            var possessionIsHome = possessionTeam == homeClubName;
            var goalInPhase = (possessionIsHome && homeGoalsRemaining > 0 && forceHomeGoal) ||
                (!possessionIsHome && awayGoalsRemaining > 0 && forceAwayGoal);
            var sequenceStart = phaseStart + phase * 330 + rng.Next(-18, 19);
            sequenceStart = Math.Clamp(sequenceStart, 60, MatchDurationSeconds - 330);

            var firstPasser = Pick(players, possessionTeam, 5);
            var firstReceiver = Pick(players, possessionTeam, 7 + (phase % 2));
            var carrier = firstReceiver;
            var finalReceiver = Pick(players, possessionTeam, 9 + (phase % 2));
            var defendingTeam = possessionIsHome ? awayClubName : homeClubName;
            var defender = Pick(players, defendingTeam, 2 + (phase % 3));
            var keeper = Pick(players, defendingTeam, 0);
            var passStart = GetShapePosition(firstPasser, shape, true);
            var passEnd = GetAttackingLanePosition(firstReceiver, shape, possessionIsHome, 0.42f + (phase % 3) * 0.08f);
            var carryEnd = GetAttackingLanePosition(carrier, shape, possessionIsHome, 0.58f + (phase % 2) * 0.08f);
            var finalPassEnd = GetAttackingLanePosition(finalReceiver, shape, possessionIsHome, 0.76f);
            var goalTarget = possessionIsHome
                ? new Vector2(0.98f, 0.44f + (float)rng.NextDouble() * 0.12f)
                : new Vector2(0.02f, 0.44f + (float)rng.NextDouble() * 0.12f);
            var clearanceTarget = possessionIsHome
                ? new Vector2(0.36f, 0.30f + (float)rng.NextDouble() * 0.40f)
                : new Vector2(0.64f, 0.30f + (float)rng.NextDouble() * 0.40f);

            AddAction(actions, ref actionIndex, MatchActionKind.Pass, sequenceStart, sequenceStart + 12, possessionTeam, $"Pass: {firstPasser.Name} to {firstReceiver.Name}", firstPasser, firstReceiver, passStart, passEnd, homeScore, awayScore);
            AddAction(actions, ref actionIndex, MatchActionKind.Carry, sequenceStart + 12, sequenceStart + 30, possessionTeam, $"Carry: {carrier.Name} advances the ball", carrier, carrier, passEnd, carryEnd, homeScore, awayScore);

            if (!goalInPhase && phase % 5 == 2)
            {
                var interceptionPoint = carryEnd.Lerp(finalPassEnd, 0.62f);
                AddAction(actions, ref actionIndex, MatchActionKind.Pass, sequenceStart + 30, sequenceStart + 42, possessionTeam, $"Pass: {carrier.Name} looks for {finalReceiver.Name}", carrier, finalReceiver, carryEnd, finalPassEnd, homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Interception, sequenceStart + 42, sequenceStart + 54, defendingTeam, $"Interception: {defender.Name} steps across the lane", finalReceiver, defender, interceptionPoint, GetShapePosition(defender, shape, false), homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Reset, sequenceStart + 54, sequenceStart + 72, defendingTeam, "Reset: possession settles after the turnover", defender, defender, GetShapePosition(defender, shape, true), GetShapePosition(defender, shape, true), homeScore, awayScore);
                continue;
            }

            AddAction(actions, ref actionIndex, MatchActionKind.Pass, sequenceStart + 30, sequenceStart + 42, possessionTeam, $"Pass: {carrier.Name} releases {finalReceiver.Name}", carrier, finalReceiver, carryEnd, finalPassEnd, homeScore, awayScore);
            AddAction(actions, ref actionIndex, MatchActionKind.Shot, sequenceStart + 42, sequenceStart + 54, possessionTeam, $"Shot: {finalReceiver.Name} attacks the goal", finalReceiver, null, finalPassEnd, goalTarget, homeScore, awayScore);

            if (goalInPhase)
            {
                if (possessionIsHome)
                {
                    homeGoalsRemaining--;
                    homeScore++;
                }
                else
                {
                    awayGoalsRemaining--;
                    awayScore++;
                }

                AddAction(actions, ref actionIndex, MatchActionKind.Goal, sequenceStart + 54, sequenceStart + 66, possessionTeam, $"Goal: {finalReceiver.Name} scores for {possessionTeam}", finalReceiver, null, goalTarget, goalTarget, homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Reset, sequenceStart + 66, sequenceStart + 86, defendingTeam, "Reset: restart after the goal", keeper, keeper, new Vector2(0.50f, 0.50f), new Vector2(0.50f, 0.50f), homeScore, awayScore);
                continue;
            }

            if (phase % 3 == 0)
            {
                AddAction(actions, ref actionIndex, MatchActionKind.Save, sequenceStart + 54, sequenceStart + 68, defendingTeam, $"Save: {keeper.Name} gets behind the shot", finalReceiver, keeper, goalTarget, GetShapePosition(keeper, shape, true), homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Clearance, sequenceStart + 68, sequenceStart + 84, defendingTeam, $"Clearance: {keeper.Name} sends play away", keeper, null, GetShapePosition(keeper, shape, true), clearanceTarget, homeScore, awayScore);
            }
            else
            {
                AddAction(actions, ref actionIndex, MatchActionKind.Clearance, sequenceStart + 54, sequenceStart + 72, defendingTeam, $"Clearance: {defender.Name} clears the danger", defender, null, goalTarget, clearanceTarget, homeScore, awayScore);
            }
        }

        AddAction(
            actions,
            ref actionIndex,
            MatchActionKind.Reset,
            MatchDurationSeconds - 30,
            MatchDurationSeconds,
            homeScore >= awayScore ? homeClubName : awayClubName,
            "Reset: full-time shape",
            null,
            null,
            new Vector2(0.50f, 0.50f),
            new Vector2(0.50f, 0.50f),
            homeScore,
            awayScore);

        actions.Sort((left, right) => left.StartSecond.CompareTo(right.StartSecond));
        return actions.ToArray();
    }

    private static MatchFrame[] BuildFrames(
        string homeClubName,
        string awayClubName,
        IReadOnlyList<RuntimePlayer> players,
        TacticalShape shape,
        IReadOnlyList<MatchAction> actions)
    {
        var frames = new List<MatchFrame>();
        for (var second = 0; second <= MatchDurationSeconds; second += FrameStepSeconds)
        {
            var action = ResolveAction(actions, second);
            var progress = CalculateProgress(action, second);
            var ball = BuildBallState(action, progress);
            var playerStates = BuildPlayerStates(homeClubName, awayClubName, players, shape, action, ball, progress);
            frames.Add(new MatchFrame
            {
                MatchSecond = second,
                HomeScore = ResolveHomeScore(actions, second),
                AwayScore = ResolveAwayScore(actions, second),
                PossessionTeam = action.Team,
                Ball = ball,
                PlayerStates = playerStates,
                CurrentActionLabel = action.Label
            });
        }

        return frames.ToArray();
    }

    private static MatchEvent[] BuildEvents(
        string homeClubName,
        string awayClubName,
        IReadOnlyList<MatchAction> actions,
        IReadOnlyList<MatchFrame> frames)
    {
        var events = new List<MatchEvent>();
        foreach (var action in actions)
        {
            if (!ShouldCreateEvent(action))
            {
                continue;
            }

            var eventSecond = action.Kind == MatchActionKind.Goal ? action.StartSecond : action.EndSecond;
            var frameIndex = FindFrameIndex(frames, eventSecond);
            events.Add(new MatchEvent
            {
                Id = $"event-{events.Count + 1:000}",
                Minute = Math.Max(1, (eventSecond / 60) + 1),
                MatchSecond = eventSecond,
                Summary = BuildEventSummary(homeClubName, awayClubName, action),
                HomeScore = action.HomeScoreAfter,
                AwayScore = action.AwayScoreAfter,
                ActionId = action.Id,
                StartFrameIndex = FindFrameIndex(frames, action.StartSecond),
                EndFrameIndex = frameIndex
            });
        }

        events.Sort((left, right) => left.MatchSecond.CompareTo(right.MatchSecond));
        return events.ToArray();
    }

    private static MatchFrame[] AttachEventsToFrames(MatchFrame[] frames, IReadOnlyList<MatchEvent> events)
    {
        var updatedFrames = new MatchFrame[frames.Length];
        for (var index = 0; index < frames.Length; index++)
        {
            var frame = frames[index];
            var frameEvent = FindFrameEvent(events, index);
            updatedFrames[index] = new MatchFrame
            {
                MatchSecond = frame.MatchSecond,
                HomeScore = frame.HomeScore,
                AwayScore = frame.AwayScore,
                PossessionTeam = frame.PossessionTeam,
                Ball = frame.Ball,
                PlayerStates = frame.PlayerStates,
                CurrentActionLabel = frame.CurrentActionLabel,
                EventId = frameEvent?.Id,
                EventSummary = frameEvent?.Summary
            };
        }

        return updatedFrames;
    }

    private static MatchEvent? FindFrameEvent(IReadOnlyList<MatchEvent> events, int frameIndex)
    {
        foreach (var matchEvent in events)
        {
            if (matchEvent.EndFrameIndex == frameIndex)
            {
                return matchEvent;
            }
        }

        return null;
    }

    private static PlayerAgentState[] BuildPlayerStates(
        string homeClubName,
        string awayClubName,
        IReadOnlyList<RuntimePlayer> players,
        TacticalShape shape,
        MatchAction action,
        BallState ball,
        float progress)
    {
        var states = new PlayerAgentState[players.Count];
        for (var index = 0; index < players.Count; index++)
        {
            var player = players[index];
            var inPossession = player.Team == action.Team;
            var basePosition = GetPhaseShapePosition(player, shape, inPossession);
            var ballShift = new Vector2((ball.Position.X - 0.5f) * 0.10f, (ball.Position.Y - 0.5f) * 0.22f);
            var target = inPossession
                ? ClampPitch(basePosition + new Vector2(player.IsHome ? 0.03f : -0.03f, 0.0f) + ballShift * 0.45f)
                : ClampPitch(CompactDefensivePosition(basePosition, ball.Position, player.Team == homeClubName || player.Team == awayClubName));
            var intent = inPossession ? PlayerIntent.Support : PlayerIntent.Defend;

            if (player.Id == action.FromPlayerId)
            {
                target = action.Kind is MatchActionKind.Carry or MatchActionKind.Shot
                    ? ball.Position
                    : ClampPitch(action.FromPosition.Lerp(action.ToPosition, progress * 0.35f));
                intent = action.Kind switch
                {
                    MatchActionKind.Carry => PlayerIntent.Carry,
                    MatchActionKind.Shot => PlayerIntent.Shoot,
                    MatchActionKind.Clearance => PlayerIntent.Recover,
                    _ => PlayerIntent.Support
                };
            }
            else if (player.Id == action.ToPlayerId)
            {
                target = action.ToPosition;
                intent = action.Kind == MatchActionKind.Interception ? PlayerIntent.Press : PlayerIntent.Receive;
            }
            else if (!inPossession)
            {
                var distanceToBall = player.IsHome
                    ? basePosition.DistanceTo(ball.Position)
                    : basePosition.DistanceTo(ball.Position);
                intent = distanceToBall < 0.22f ? PlayerIntent.Press : PlayerIntent.Defend;
            }

            var hasBall = ball.CarrierPlayerId == player.Id;
            if (hasBall)
            {
                target = ball.Position;
                intent = action.Kind == MatchActionKind.Carry ? PlayerIntent.Carry : PlayerIntent.HoldShape;
            }

            states[index] = new PlayerAgentState
            {
                PlayerId = player.Id,
                Name = player.Name,
                Team = player.Team,
                Role = player.Role,
                Position = ClampPitch(basePosition.Lerp(target, 0.72f)),
                TargetPosition = ClampPitch(target),
                HasBall = hasBall,
                CurrentIntent = intent
            };
        }

        return states;
    }

    private static BallState BuildBallState(MatchAction action, float progress)
    {
        var position = ClampPitch(action.FromPosition.Lerp(action.ToPosition, progress));
        var movementState = action.Kind switch
        {
            MatchActionKind.Pass => BallMovementState.Passed,
            MatchActionKind.Carry => BallMovementState.Carried,
            MatchActionKind.Shot => BallMovementState.Shot,
            MatchActionKind.Save => BallMovementState.Saved,
            MatchActionKind.Clearance => BallMovementState.Cleared,
            MatchActionKind.Goal => BallMovementState.Goal,
            MatchActionKind.Interception => BallMovementState.Loose,
            _ => BallMovementState.Carried
        };
        var carrierId = action.Kind is MatchActionKind.Carry or MatchActionKind.Kickoff or MatchActionKind.Reset
            ? action.FromPlayerId ?? action.ToPlayerId
            : action.Kind == MatchActionKind.Save ? action.ToPlayerId : null;

        return new BallState
        {
            Position = position,
            TargetPosition = action.ToPosition,
            CarrierPlayerId = carrierId,
            MovementState = movementState
        };
    }

    private static TacticalShape BuildTacticalShape(string formation, int width)
    {
        var compactness = Math.Clamp(width / 100.0f, 0.25f, 0.90f);
        var baseHome = formation switch
        {
            "4-2-3-1" => new[]
            {
                new Vector2(0.08f, 0.50f),
                new Vector2(0.22f, 0.18f),
                new Vector2(0.19f, 0.38f),
                new Vector2(0.19f, 0.62f),
                new Vector2(0.22f, 0.82f),
                new Vector2(0.38f, 0.38f),
                new Vector2(0.38f, 0.62f),
                new Vector2(0.55f, 0.50f),
                new Vector2(0.62f, 0.22f),
                new Vector2(0.72f, 0.50f),
                new Vector2(0.62f, 0.78f)
            },
            "3-5-2" => new[]
            {
                new Vector2(0.08f, 0.50f),
                new Vector2(0.20f, 0.32f),
                new Vector2(0.18f, 0.50f),
                new Vector2(0.20f, 0.68f),
                new Vector2(0.42f, 0.18f),
                new Vector2(0.40f, 0.40f),
                new Vector2(0.40f, 0.60f),
                new Vector2(0.42f, 0.82f),
                new Vector2(0.56f, 0.50f),
                new Vector2(0.72f, 0.40f),
                new Vector2(0.72f, 0.60f)
            },
            _ => new[]
            {
                new Vector2(0.08f, 0.50f),
                new Vector2(0.24f, 0.20f),
                new Vector2(0.20f, 0.38f),
                new Vector2(0.20f, 0.62f),
                new Vector2(0.24f, 0.80f),
                new Vector2(0.40f, 0.30f),
                new Vector2(0.36f, 0.50f),
                new Vector2(0.40f, 0.70f),
                new Vector2(0.62f, 0.22f),
                new Vector2(0.70f, 0.50f),
                new Vector2(0.62f, 0.78f)
            }
        };

        var homeInPossession = TransformShape(baseHome, 0.07f, compactness, false);
        var homeOutOfPossession = TransformShape(baseHome, -0.04f, 0.58f, false);
        var awayInPossession = MirrorShape(TransformShape(baseHome, 0.07f, compactness, false));
        var awayOutOfPossession = MirrorShape(TransformShape(baseHome, -0.04f, 0.58f, false));

        return new TacticalShape
        {
            Formation = formation,
            HomeInPossession = homeInPossession,
            HomeOutOfPossession = homeOutOfPossession,
            AwayInPossession = awayInPossession,
            AwayOutOfPossession = awayOutOfPossession
        };
    }

    private static Vector2[] TransformShape(Vector2[] shape, float xShift, float widthFactor, bool mirror)
    {
        var transformed = new Vector2[shape.Length];
        for (var index = 0; index < shape.Length; index++)
        {
            var point = shape[index];
            var widenedY = 0.5f + (point.Y - 0.5f) * widthFactor / 0.55f;
            var transformedPoint = ClampPitch(new Vector2(point.X + xShift, widenedY));
            transformed[index] = mirror ? new Vector2(1.0f - transformedPoint.X, transformedPoint.Y) : transformedPoint;
        }

        return transformed;
    }

    private static Vector2[] MirrorShape(Vector2[] shape)
    {
        var mirrored = new Vector2[shape.Length];
        for (var index = 0; index < shape.Length; index++)
        {
            mirrored[index] = new Vector2(1.0f - shape[index].X, shape[index].Y);
        }

        return mirrored;
    }

    private static void AddAction(
        List<MatchAction> actions,
        ref int actionIndex,
        MatchActionKind kind,
        int startSecond,
        int endSecond,
        string team,
        string label,
        RuntimePlayer? fromPlayer,
        RuntimePlayer? toPlayer,
        Vector2 fromPosition,
        Vector2 toPosition,
        int homeScoreAfter,
        int awayScoreAfter)
    {
        actions.Add(new MatchAction
        {
            Id = $"action-{actionIndex:000}",
            Kind = kind,
            StartSecond = Math.Clamp(startSecond, 0, MatchDurationSeconds),
            EndSecond = Math.Clamp(Math.Max(endSecond, startSecond + 1), 0, MatchDurationSeconds),
            Team = team,
            Label = label,
            FromPlayerId = fromPlayer?.Id,
            ToPlayerId = toPlayer?.Id,
            FromPosition = ClampPitch(fromPosition),
            ToPosition = ClampPitch(toPosition),
            HomeScoreAfter = homeScoreAfter,
            AwayScoreAfter = awayScoreAfter
        });
        actionIndex++;
    }

    private static bool ShouldCreateEvent(MatchAction action)
    {
        return action.Kind is MatchActionKind.Kickoff or MatchActionKind.Pass or MatchActionKind.Shot or MatchActionKind.Save or MatchActionKind.Clearance or MatchActionKind.Interception or MatchActionKind.Goal;
    }

    private static string BuildEventSummary(string homeClubName, string awayClubName, MatchAction action)
    {
        var minute = Math.Max(1, ((action.Kind == MatchActionKind.Goal ? action.StartSecond : action.EndSecond) / 60) + 1);
        return action.Kind switch
        {
            MatchActionKind.Kickoff => $"{minute}' Kick-off. {homeClubName} start the match and the shape opens around the ball.",
            MatchActionKind.Pass => $"{minute}' {action.Label}.",
            MatchActionKind.Carry => $"{minute}' {action.Label}.",
            MatchActionKind.Shot => $"{minute}' {action.Label}.",
            MatchActionKind.Save => $"{minute}' {action.Label}.",
            MatchActionKind.Clearance => $"{minute}' {action.Label}.",
            MatchActionKind.Interception => $"{minute}' {action.Label}.",
            MatchActionKind.Goal => $"{minute}' {action.Label}. {homeClubName} {action.HomeScoreAfter} - {action.AwayScoreAfter} {awayClubName}.",
            _ => $"{minute}' {action.Label}."
        };
    }

    private static RuntimePlayer Pick(IReadOnlyList<RuntimePlayer> players, string team, int preferredShapeIndex)
    {
        foreach (var player in players)
        {
            if (player.Team == team && player.ShapeIndex == preferredShapeIndex)
            {
                return player;
            }
        }

        foreach (var player in players)
        {
            if (player.Team == team)
            {
                return player;
            }
        }

        return players[0];
    }

    private static MatchAction ResolveAction(IReadOnlyList<MatchAction> actions, int second)
    {
        MatchAction? latest = null;
        foreach (var action in actions)
        {
            if (second >= action.StartSecond && second <= action.EndSecond)
            {
                return action;
            }

            if (action.StartSecond <= second)
            {
                latest = action;
            }
        }

        return latest ?? actions[0];
    }

    private static float CalculateProgress(MatchAction action, int second)
    {
        var duration = Math.Max(1, action.EndSecond - action.StartSecond);
        return Math.Clamp((second - action.StartSecond) / (float)duration, 0.0f, 1.0f);
    }

    private static int ResolveHomeScore(IReadOnlyList<MatchAction> actions, int second)
    {
        var score = 0;
        foreach (var action in actions)
        {
            if (action.StartSecond <= second)
            {
                score = action.HomeScoreAfter;
            }
        }

        return score;
    }

    private static int ResolveAwayScore(IReadOnlyList<MatchAction> actions, int second)
    {
        var score = 0;
        foreach (var action in actions)
        {
            if (action.StartSecond <= second)
            {
                score = action.AwayScoreAfter;
            }
        }

        return score;
    }

    private static int FindFrameIndex(IReadOnlyList<MatchFrame> frames, int second)
    {
        var bestIndex = 0;
        var bestDistance = int.MaxValue;
        for (var index = 0; index < frames.Count; index++)
        {
            var distance = Math.Abs(frames[index].MatchSecond - second);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestIndex = index;
            }
        }

        return bestIndex;
    }

    private static Vector2 GetAttackingLanePosition(RuntimePlayer player, TacticalShape shape, bool homePossession, float x)
    {
        var basePosition = GetPhaseShapePosition(player, shape, true);
        var attackX = homePossession ? x : 1.0f - x;
        return ClampPitch(new Vector2(attackX, basePosition.Y));
    }

    private static Vector2 GetShapePosition(RuntimePlayer player, TacticalShape shape, bool inPossession)
    {
        return GetPhaseShapePosition(player, shape, inPossession);
    }

    private static Vector2 GetPhaseShapePosition(RuntimePlayer player, TacticalShape shape, bool inPossession)
    {
        var positions = player.IsHome
            ? inPossession ? shape.HomeInPossession : shape.HomeOutOfPossession
            : inPossession ? shape.AwayInPossession : shape.AwayOutOfPossession;
        return positions[Math.Clamp(player.ShapeIndex, 0, positions.Length - 1)];
    }

    private static Vector2 CompactDefensivePosition(Vector2 basePosition, Vector2 ballPosition, bool _knownTeam)
    {
        var y = 0.5f + (basePosition.Y - 0.5f) * 0.70f + (ballPosition.Y - 0.5f) * 0.18f;
        var x = basePosition.X + (ballPosition.X - basePosition.X) * 0.10f;
        return ClampPitch(new Vector2(x, y));
    }

    private static Vector2 ClampPitch(Vector2 value)
    {
        return new Vector2(
            Math.Clamp(value.X, 0.02f, 0.98f),
            Math.Clamp(value.Y, 0.06f, 0.94f));
    }
}
