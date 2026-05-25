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
        public required int Ability { get; init; }
        public required int Morale { get; init; }
        public required int Form { get; init; }
        public required int Fitness { get; init; }
        public required int Fatigue { get; init; }
        public required int TacticalFitScore { get; init; }
    }

    public static MatchPlaybackResult Simulate(GameState state)
    {
        var rng = new Random(state.WorldSeed * 31 + state.CurrentMatchday * 17 + state.PressIntensity + state.Risk);
        var homeClubName = state.SelectedClubName ?? "Home";
        var awayClubName = state.CurrentOpponentName;
        var tacticalShape = BuildTacticalShape(state.TacticalFormation, state.Width);
        var homeLineup = BuildHomeLineup(state, homeClubName);
        var awayLineup = BuildAwayLineup(state, awayClubName);
        var players = new List<RuntimePlayer>(22);
        players.AddRange(homeLineup);
        players.AddRange(awayLineup);

        var homeAttackQuality = AverageRoleQuality(homeLineup, state.TacticalFamiliarityScore, "CM", "AM", "RW", "LW", "ST");
        var homeDefensiveQuality = AverageRoleQuality(homeLineup, state.TacticalFamiliarityScore, "GK", "RB", "CB", "LB");
        var awayAttackQuality = AverageRoleQuality(awayLineup, 58, "CM", "AM", "RW", "LW", "ST");
        var awayDefensiveQuality = AverageRoleQuality(awayLineup, 58, "GK", "RB", "CB", "LB");
        var staffPreparation = Math.Clamp((state.CareerProfile.StaffTrust + state.CareerProfile.DirectorTrust) / 12, 0, 14);
        var moraleEffect = Math.Clamp((state.SquadMorale - 55) / 5, -8, 8);
        var familiarityEffect = Math.Clamp((state.TacticalFamiliarityScore - 55) / 4, -8, 10);
        var roleFitEffect = Math.Clamp((state.TacticalRoleFitScore - 60) / 5, -7, 8);
        var setPieceEffect = ResolveSetPieceEffect(state);
        var opponentPrepEffect = ResolveOpponentPreparationEffect(state);
        var plannedHomeGoals = Math.Clamp(
            (homeAttackQuality - awayDefensiveQuality + state.PressIntensity / 3 + state.Tempo / 2 + state.Risk / 2 + staffPreparation + moraleEffect + familiarityEffect + roleFitEffect + setPieceEffect + opponentPrepEffect - 78) / 24 + rng.Next(0, 2),
            0,
            4);
        var plannedAwayGoals = Math.Clamp(
            (awayAttackQuality - homeDefensiveQuality + state.Risk / 2 + (100 - state.PressIntensity) / 4 - moraleEffect - familiarityEffect - roleFitEffect / 2 - opponentPrepEffect - (state.SetPieceApproach == TacticalSetPieceApproach.DefensiveSecurity ? 1 : 0) - 45) / 28 + rng.Next(0, 2),
            0,
            3);
        if (plannedHomeGoals == 0 && plannedAwayGoals == 0)
        {
            if (state.Risk >= 62 || state.Tempo >= 62)
            {
                plannedHomeGoals = 1;
            }
            else
            {
                plannedAwayGoals = rng.NextDouble() < 0.35 ? 1 : 0;
                if (plannedAwayGoals == 0)
                {
                    plannedHomeGoals = 1;
                }
            }
        }

        var actions = BuildActions(
            homeClubName,
            awayClubName,
            players,
            tacticalShape,
            plannedHomeGoals,
            plannedAwayGoals,
            state.PressIntensity,
            state.Tempo,
            state.Width,
            state.Risk,
            rng);
        var frames = BuildFrames(homeClubName, awayClubName, players, tacticalShape, actions);
        var events = BuildEvents(homeClubName, awayClubName, actions, frames);
        frames = AttachEventsToFrames(frames, events);
        var finalFrame = frames[^1];
        var stats = MatchStatsService.Build(homeClubName, awayClubName, actions);

        return new MatchPlaybackResult
        {
            HomeClubName = homeClubName,
            AwayClubName = awayClubName,
            TacticalSummary =
                $"Shape {state.TacticalFormation} | Style {state.TeamStyleName} | Familiarity {state.TacticalFamiliarityName} | Role fit {state.TacticalRoleFitScore}/100 | Set pieces {state.SetPieceApproachName} | Opponent prep {state.OpponentPreparationFocusName} | Pressing {state.PressIntensity} | Tempo {state.Tempo} | Width {state.Width} | Mentality {state.Risk}",
            TacticalExplanation = $"The match engine weighted player ability, form, morale, fitness, staff preparation, {state.TeamStyleName.ToLowerInvariant()} style, familiarity {state.TacticalFamiliarityName}, role fit {state.TacticalRoleFitScore}/100, set-piece approach {state.SetPieceApproachName.ToLowerInvariant()}, and opponent preparation {state.OpponentPreparationFocusName.ToLowerInvariant()}. {state.TacticalRoleFitSummary} {state.OpponentPreparationSummary}",
            PlayerRatingsSummary = BuildPlayerRatingsSummary(homeLineup, awayLineup, homeClubName, awayClubName),
            PostMatchNotes = $"Preparation {staffPreparation}/14 | Morale effect {moraleEffect:+0;-0;0} | Familiarity effect {familiarityEffect:+0;-0;0} | Role-fit effect {roleFitEffect:+0;-0;0} | Set-piece effect {setPieceEffect:+0;-0;0} | Opponent-prep effect {opponentPrepEffect:+0;-0;0} | Fit notes: {state.TacticalFitNotes}",
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
            Stats = stats,
            FinalResultSummary = $"{homeClubName} {finalFrame.HomeScore} - {finalFrame.AwayScore} {awayClubName}"
        };
    }

    private static int ResolveSetPieceEffect(GameState state)
    {
        return state.SetPieceApproach switch
        {
            TacticalSetPieceApproach.AttackNearPost or TacticalSetPieceApproach.AttackFarPost => 1,
            TacticalSetPieceApproach.CrowdKeeper when state.Risk >= 60 => 1,
            TacticalSetPieceApproach.ShortRoutines when state.TeamStyle == TacticalTeamStyle.Possession => 1,
            TacticalSetPieceApproach.DefensiveSecurity => -1,
            _ => 0
        };
    }

    private static int ResolveOpponentPreparationEffect(GameState state)
    {
        return state.CurrentOpponentPreparationFocus switch
        {
            OpponentPreparationFocus.PressTriggers when state.PressIntensity >= 65 => 1,
            OpponentPreparationFocus.RestDefense => 2,
            OpponentPreparationFocus.WideContainment when state.Width >= 60 => 1,
            OpponentPreparationFocus.CentralContainment when state.TeamStyle == TacticalTeamStyle.CentralOverload => 1,
            OpponentPreparationFocus.DirectDefense when state.TeamStyle == TacticalTeamStyle.DirectPlay => 1,
            OpponentPreparationFocus.LowBlockPatience when state.Risk <= 50 => 1,
            _ => 0
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
                Id = ClubSquadFactory.BuildPlayerId(teamName, selectedPlayers[index].Name, index),
                Name = selectedPlayers[index].Name,
                Team = teamName,
                Role = selectedPlayers[index].Position,
                IsHome = true,
                ShapeIndex = index,
                Ability = selectedPlayers[index].TrueAbility,
                Morale = selectedPlayers[index].Morale,
                Form = selectedPlayers[index].Form,
                Fitness = selectedPlayers[index].Fitness,
                Fatigue = selectedPlayers[index].Fatigue,
                TacticalFitScore = selectedPlayers[index].TacticalFitScore
            };
        }

        return lineup;
    }

    private static RuntimePlayer[] BuildAwayLineup(GameState state, string teamName)
    {
        var squad = state.GetClubSquad(teamName);
        var selectedPlayers = new List<ClubSquadPlayer>(11);
        foreach (var player in squad)
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

        foreach (var player in squad)
        {
            if (selectedPlayers.Count == 11)
            {
                break;
            }

            if (!selectedPlayers.Exists(candidate => candidate.PlayerId == player.PlayerId))
            {
                selectedPlayers.Add(player);
            }
        }

        if (selectedPlayers.Count == 0)
        {
            selectedPlayers.AddRange(ClubSquadFactory.BuildFallbackSquad(teamName, state.WorldSeed));
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
            var player = selectedPlayers[index];
            lineup[index] = new RuntimePlayer
            {
                Id = player.PlayerId,
                Name = player.Name,
                Team = teamName,
                Role = player.Position,
                IsHome = false,
                ShapeIndex = index,
                Ability = player.TrueAbility,
                Morale = player.Morale,
                Form = player.Form,
                Fitness = player.Fitness,
                Fatigue = player.Fatigue,
                TacticalFitScore = player.TacticalFitScore
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
        int pressIntensity,
        int tempo,
        int width,
        int risk,
        Random rng)
    {
        var actions = new List<MatchAction>();
        var homeGoalsRemaining = plannedHomeGoals;
        var awayGoalsRemaining = plannedAwayGoals;
        var homeScore = 0;
        var awayScore = 0;
        var actionIndex = 1;

        var kickoffPlayer = PickByRole(players, homeClubName, rng, 6, "CM", "AM");
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

        var phaseCount = Math.Clamp(12 + tempo / 18 + risk / 40, 13, 18);
        var phaseGap = (MatchDurationSeconds - 540) / phaseCount;
        var homeGoalSlots = BuildGoalSlots(plannedHomeGoals, phaseCount, 1);
        var awayGoalSlots = BuildGoalSlots(plannedAwayGoals, phaseCount, 3);

        for (var phase = 0; phase < phaseCount; phase++)
        {
            var forceHomeGoal = homeGoalsRemaining > 0 && homeGoalSlots.Contains(phase);
            var forceAwayGoal = awayGoalsRemaining > 0 && !forceHomeGoal && awayGoalSlots.Contains(phase);
            var homePossessionBias = Math.Clamp(0.52f + (pressIntensity - 50) * 0.003f + (tempo - 50) * 0.001f - Math.Max(0, risk - 70) * 0.0015f, 0.38f, 0.68f);
            var possessionTeam = forceHomeGoal
                ? homeClubName
                : forceAwayGoal
                    ? awayClubName
                    : rng.NextDouble() < homePossessionBias ? homeClubName : awayClubName;
            var possessionIsHome = possessionTeam == homeClubName;
            var goalInPhase = (possessionIsHome && homeGoalsRemaining > 0 && forceHomeGoal) ||
                (!possessionIsHome && awayGoalsRemaining > 0 && forceAwayGoal);
            var sequenceStart = 180 + phase * phaseGap + rng.Next(-22, 23);
            sequenceStart = Math.Clamp(sequenceStart, 60, MatchDurationSeconds - 330);
            var patternRoll = rng.NextDouble();
            var directBreak = (risk >= 65 && patternRoll < 0.35) || (tempo >= 72 && patternRoll < 0.25);
            var wideAttack = !directBreak && width >= 58 && patternRoll < 0.78;
            var laneY = ResolveLaneY(width, phase, rng, wideAttack);
            var finalLaneY = Math.Clamp(laneY + ((float)rng.NextDouble() - 0.5f) * 0.10f, 0.14f, 0.86f);

            var firstPasser = PickByRole(players, possessionTeam, rng, 5 + phase % 2, "CM", "AM", "RB", "LB");
            var firstReceiver = wideAttack
                ? PickByRole(players, possessionTeam, rng, 8 + phase % 3, "RW", "LW", "RB", "LB")
                : PickByRole(players, possessionTeam, rng, 7, "CM", "AM");
            var carrier = firstReceiver;
            var finalReceiver = directBreak
                ? PickByRole(players, possessionTeam, rng, 9, "ST", "RW", "LW")
                : PickByRole(players, possessionTeam, rng, 9 + phase % 2, "ST", "AM", "RW", "LW");
            var defendingTeam = possessionIsHome ? awayClubName : homeClubName;
            var defender = PickByRole(players, defendingTeam, rng, 2 + phase % 3, "CB", "RB", "LB", "CM");
            var keeper = PickByRole(players, defendingTeam, rng, 0, "GK");
            var passStart = GetShapePosition(firstPasser, shape, true);
            var passEnd = GetAttackingLanePosition(firstReceiver, shape, possessionIsHome, directBreak ? 0.44f : 0.36f + (phase % 3) * 0.04f, laneY);
            var carryEnd = GetAttackingLanePosition(carrier, shape, possessionIsHome, directBreak ? 0.66f : 0.54f + (phase % 2) * 0.07f, finalLaneY);
            var finalPassEnd = GetAttackingLanePosition(finalReceiver, shape, possessionIsHome, directBreak ? 0.78f : 0.74f, finalLaneY);
            var goalTarget = possessionIsHome
                ? new Vector2(0.98f, 0.44f + (float)rng.NextDouble() * 0.12f)
                : new Vector2(0.02f, 0.44f + (float)rng.NextDouble() * 0.12f);
            var clearanceTarget = possessionIsHome
                ? new Vector2(0.36f, 0.30f + (float)rng.NextDouble() * 0.40f)
                : new Vector2(0.64f, 0.30f + (float)rng.NextDouble() * 0.40f);

            var passDuration = tempo >= 70 ? 8 : 12;
            var carryDuration = directBreak ? 12 : tempo >= 70 ? 15 : 20;
            var finalPassDuration = directBreak ? 8 : 12;
            var currentSecond = sequenceStart;
            var pressureTurnoverChance = Math.Clamp(
                0.12f +
                (defendingTeam == homeClubName ? pressIntensity * 0.003f : risk * 0.002f) +
                (directBreak ? 0.04f : 0.0f) +
                (wideAttack ? 0.02f : 0.0f),
                0.12f,
                0.52f);

            AddAction(actions, ref actionIndex, MatchActionKind.Pass, currentSecond, currentSecond + passDuration, possessionTeam, BuildPassLabel(firstPasser, firstReceiver, wideAttack, directBreak), firstPasser, firstReceiver, passStart, passEnd, homeScore, awayScore);
            currentSecond += passDuration;
            AddAction(actions, ref actionIndex, MatchActionKind.Carry, currentSecond, currentSecond + carryDuration, possessionTeam, BuildCarryLabel(carrier, directBreak, wideAttack), carrier, carrier, passEnd, carryEnd, homeScore, awayScore);
            currentSecond += carryDuration;

            if (!goalInPhase && rng.NextDouble() < pressureTurnoverChance)
            {
                var interceptionPoint = carryEnd.Lerp(finalPassEnd, 0.62f);
                if (!directBreak)
                {
                    AddAction(actions, ref actionIndex, MatchActionKind.Pass, currentSecond, currentSecond + finalPassDuration, possessionTeam, $"Pass: {carrier.Name} risks the lane toward {finalReceiver.Name}", carrier, finalReceiver, carryEnd, finalPassEnd, homeScore, awayScore);
                    currentSecond += finalPassDuration;
                }

                AddAction(actions, ref actionIndex, MatchActionKind.Interception, currentSecond, currentSecond + 12, defendingTeam, $"Interception: {defender.Name} reads the {DescribeLane(laneY)} lane", finalReceiver, defender, interceptionPoint, GetShapePosition(defender, shape, false), homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Reset, currentSecond + 12, currentSecond + 30, defendingTeam, "Reset: possession settles after the turnover", defender, defender, GetShapePosition(defender, shape, true), GetShapePosition(defender, shape, true), homeScore, awayScore);
                continue;
            }

            if (!directBreak)
            {
                AddAction(actions, ref actionIndex, MatchActionKind.Pass, currentSecond, currentSecond + finalPassDuration, possessionTeam, BuildFinalPassLabel(carrier, finalReceiver, wideAttack), carrier, finalReceiver, carryEnd, finalPassEnd, homeScore, awayScore);
                currentSecond += finalPassDuration;
            }

            AddAction(actions, ref actionIndex, MatchActionKind.Shot, currentSecond, currentSecond + 12, possessionTeam, BuildShotLabel(finalReceiver, risk, directBreak, wideAttack), finalReceiver, null, finalPassEnd, goalTarget, homeScore, awayScore);
            currentSecond += 12;

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

                AddAction(actions, ref actionIndex, MatchActionKind.Goal, currentSecond, currentSecond + 12, possessionTeam, $"Goal: {finalReceiver.Name} finishes the {DescribeLane(laneY)} attack for {possessionTeam}", finalReceiver, null, goalTarget, goalTarget, homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Reset, currentSecond + 12, currentSecond + 32, defendingTeam, "Reset: restart after the goal", keeper, keeper, new Vector2(0.50f, 0.50f), new Vector2(0.50f, 0.50f), homeScore, awayScore);
                continue;
            }

            var saveChance = Math.Clamp(0.48f + keeper.Form * 0.002f - risk * 0.002f - (directBreak ? 0.05f : 0.0f), 0.28f, 0.72f);
            if (rng.NextDouble() < saveChance)
            {
                AddAction(actions, ref actionIndex, MatchActionKind.Save, currentSecond, currentSecond + 14, defendingTeam, $"Save: {keeper.Name} gets behind the {DescribeLane(laneY)} shot", finalReceiver, keeper, goalTarget, GetShapePosition(keeper, shape, true), homeScore, awayScore);
                AddAction(actions, ref actionIndex, MatchActionKind.Clearance, currentSecond + 14, currentSecond + 30, defendingTeam, $"Clearance: {keeper.Name} sends play away", keeper, null, GetShapePosition(keeper, shape, true), clearanceTarget, homeScore, awayScore);
            }
            else
            {
                AddAction(actions, ref actionIndex, MatchActionKind.Clearance, currentSecond, currentSecond + 18, defendingTeam, $"Clearance: {defender.Name} clears the danger", defender, null, goalTarget, clearanceTarget, homeScore, awayScore);
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
            Participants = BuildParticipants(kind, fromPlayer, toPlayer),
            FromPosition = ClampPitch(fromPosition),
            ToPosition = ClampPitch(toPosition),
            HomeScoreAfter = homeScoreAfter,
            AwayScoreAfter = awayScoreAfter
        });
        actionIndex++;
    }

    private static ActionParticipants BuildParticipants(MatchActionKind kind, RuntimePlayer? fromPlayer, RuntimePlayer? toPlayer)
    {
        return kind switch
        {
            MatchActionKind.Kickoff => new ActionParticipants
            {
                CarrierPlayerId = fromPlayer?.Id ?? toPlayer?.Id
            },
            MatchActionKind.Pass => new ActionParticipants
            {
                PasserPlayerId = fromPlayer?.Id,
                ReceiverPlayerId = toPlayer?.Id
            },
            MatchActionKind.Carry => new ActionParticipants
            {
                CarrierPlayerId = fromPlayer?.Id ?? toPlayer?.Id
            },
            MatchActionKind.Shot => new ActionParticipants
            {
                ShooterPlayerId = fromPlayer?.Id
            },
            MatchActionKind.Save => new ActionParticipants
            {
                ShooterPlayerId = fromPlayer?.Id,
                GoalkeeperPlayerId = toPlayer?.Id
            },
            MatchActionKind.Clearance => new ActionParticipants
            {
                ClearerPlayerId = fromPlayer?.Id,
                GoalkeeperPlayerId = fromPlayer?.Role == "GK" ? fromPlayer.Id : null
            },
            MatchActionKind.Interception => new ActionParticipants
            {
                DefenderPlayerId = toPlayer?.Id,
                InterceptorPlayerId = toPlayer?.Id,
                ReceiverPlayerId = fromPlayer?.Id
            },
            MatchActionKind.Goal => new ActionParticipants
            {
                ShooterPlayerId = fromPlayer?.Id,
                ScorerPlayerId = fromPlayer?.Id
            },
            MatchActionKind.Reset => new ActionParticipants
            {
                CarrierPlayerId = fromPlayer?.Id ?? toPlayer?.Id
            },
            _ => new ActionParticipants()
        };
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

    private static HashSet<int> BuildGoalSlots(int goalCount, int phaseCount, int offset)
    {
        var slots = new HashSet<int>();
        for (var index = 0; index < goalCount; index++)
        {
            var slot = Math.Clamp(offset + ((index + 1) * phaseCount) / (goalCount + 1), 1, Math.Max(1, phaseCount - 2));
            while (slots.Contains(slot) && slot < phaseCount - 1)
            {
                slot++;
            }

            slots.Add(slot);
        }

        return slots;
    }

    private static int AverageRoleQuality(IReadOnlyList<RuntimePlayer> players, int tacticalFamiliarity, params string[] roles)
    {
        var total = 0;
        var count = 0;
        foreach (var player in players)
        {
            if (!HasRole(player, roles))
            {
                continue;
            }

            total += CalculatePlayerMatchQuality(player, tacticalFamiliarity);
            count++;
        }

        return count == 0 ? 68 : total / count;
    }

    private static int CalculatePlayerMatchQuality(RuntimePlayer player, int tacticalFamiliarity)
    {
        var quality = player.Ability * 5 +
            player.Form * 3 +
            player.Morale +
            player.Fitness +
            player.TacticalFitScore * 2 +
            tacticalFamiliarity * 2 -
            player.Fatigue * 2;
        return Math.Clamp(quality / 14, 35, 96);
    }

    private static string BuildPlayerRatingsSummary(
        IReadOnlyList<RuntimePlayer> homeLineup,
        IReadOnlyList<RuntimePlayer> awayLineup,
        string homeClubName,
        string awayClubName)
    {
        var homeBest = PickBestMatchQuality(homeLineup, 58);
        var awayBest = PickBestMatchQuality(awayLineup, 58);
        return $"{homeClubName}: {homeBest.Name} {CalculatePlayerMatchQuality(homeBest, 58) / 10.0:0.0}; {awayClubName}: {awayBest.Name} {CalculatePlayerMatchQuality(awayBest, 58) / 10.0:0.0}. Ratings reflect ability, role fit, form, morale, fitness, and fatigue.";
    }

    private static RuntimePlayer PickBestMatchQuality(IReadOnlyList<RuntimePlayer> lineup, int tacticalFamiliarity)
    {
        var best = lineup[0];
        var bestQuality = CalculatePlayerMatchQuality(best, tacticalFamiliarity);
        foreach (var player in lineup)
        {
            var quality = CalculatePlayerMatchQuality(player, tacticalFamiliarity);
            if (quality > bestQuality)
            {
                best = player;
                bestQuality = quality;
            }
        }

        return best;
    }

    private static RuntimePlayer PickByRole(
        IReadOnlyList<RuntimePlayer> players,
        string team,
        Random rng,
        int fallbackShapeIndex,
        params string[] roles)
    {
        var candidates = new List<RuntimePlayer>();
        foreach (var player in players)
        {
            if (player.Team == team && HasRole(player, roles))
            {
                candidates.Add(player);
            }
        }

        if (candidates.Count > 0)
        {
            return candidates[rng.Next(0, candidates.Count)];
        }

        return Pick(players, team, fallbackShapeIndex);
    }

    private static bool HasRole(RuntimePlayer player, params string[] roles)
    {
        foreach (var role in roles)
        {
            if (player.Role == role)
            {
                return true;
            }
        }

        return false;
    }

    private static float ResolveLaneY(int width, int phase, Random rng, bool wideAttack)
    {
        if (wideAttack || width >= 65)
        {
            var wideBase = phase % 2 == 0 ? 0.22f : 0.78f;
            return Math.Clamp(wideBase + ((float)rng.NextDouble() - 0.5f) * 0.10f, 0.12f, 0.88f);
        }

        if (width <= 42)
        {
            return 0.42f + (float)rng.NextDouble() * 0.16f;
        }

        var lane = phase % 3 switch
        {
            0 => 0.34f,
            1 => 0.50f,
            _ => 0.66f
        };
        return Math.Clamp(lane + ((float)rng.NextDouble() - 0.5f) * 0.08f, 0.16f, 0.84f);
    }

    private static string BuildPassLabel(RuntimePlayer passer, RuntimePlayer receiver, bool wideAttack, bool directBreak)
    {
        if (directBreak)
        {
            return $"Pass: {passer.Name} breaks forward into {receiver.Name}";
        }

        return wideAttack
            ? $"Pass: {passer.Name} switches wide to {receiver.Name}"
            : $"Pass: {passer.Name} links play through {receiver.Name}";
    }

    private static string BuildCarryLabel(RuntimePlayer carrier, bool directBreak, bool wideAttack)
    {
        if (directBreak)
        {
            return $"Carry: {carrier.Name} drives vertically";
        }

        return wideAttack
            ? $"Carry: {carrier.Name} attacks the channel"
            : $"Carry: {carrier.Name} advances the ball";
    }

    private static string BuildFinalPassLabel(RuntimePlayer passer, RuntimePlayer receiver, bool wideAttack)
    {
        return wideAttack
            ? $"Pass: {passer.Name} cuts the ball back to {receiver.Name}"
            : $"Pass: {passer.Name} releases {receiver.Name}";
    }

    private static string BuildShotLabel(RuntimePlayer shooter, int risk, bool directBreak, bool wideAttack)
    {
        if (directBreak || risk >= 70)
        {
            return $"Shot: {shooter.Name} takes on the early chance";
        }

        return wideAttack
            ? $"Shot: {shooter.Name} meets the wide attack"
            : $"Shot: {shooter.Name} attacks the goal";
    }

    private static string DescribeLane(float y)
    {
        return y switch
        {
            < 0.36f => "left",
            > 0.64f => "right",
            _ => "central"
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

    private static Vector2 GetAttackingLanePosition(RuntimePlayer player, TacticalShape shape, bool homePossession, float x, float laneY)
    {
        var attackX = homePossession ? x : 1.0f - x;
        return ClampPitch(new Vector2(attackX, laneY));
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
