import {
  advanceSeasonState,
  createSeasonState,
  deriveSackRiskPressureState,
  deriveSeasonBoardContextFromSeasonState,
  evaluateSeasonBoardDecisions,
  evaluateSeasonBoardContext,
  getFixturesForMatchday,
  isSeasonComplete,
  summarizeCompletedSeasonBoardOutcomes
} from "../packages/sim-core/dist/src/index.js";

function buildSeasonStateAndTimeline() {
  const clubs = [
    { id: "club-a", name: "Club A", strength: 74 },
    { id: "club-b", name: "Club B", strength: 71 },
    { id: "club-c", name: "Club C", strength: 69 },
    { id: "club-d", name: "Club D", strength: 67 }
  ];

  let state = createSeasonState(clubs);
  let rolling = 17;

  const staticContext = {
    "club-a": {
      preseasonObjectivePosition: 3,
      clubStature: 0.85,
      financialPressure: 0.55,
      styleAlignment: 0.58,
      derbyResult: "none"
    },
    "club-b": {
      preseasonObjectivePosition: 6,
      clubStature: 0.45,
      financialPressure: 0.35,
      styleAlignment: 0.63,
      derbyResult: "win"
    },
    "club-c": {
      preseasonObjectivePosition: 8,
      clubStature: 0.3,
      financialPressure: 0.22,
      styleAlignment: 0.54,
      derbyResult: "draw"
    },
    "club-d": {
      preseasonObjectivePosition: 10,
      clubStature: 0.2,
      financialPressure: 0.2,
      styleAlignment: 0.49,
      derbyResult: "loss"
    }
  };

  const timelineRows = [];
  const timelinesByClubId = Object.fromEntries(clubs.map((club) => [club.id, []]));
  const previousRiskByClubId = {};

  while (!isSeasonComplete(state)) {
    const fixtures = getFixturesForMatchday(state, state.currentMatchday);
    const resultsByFixtureId = Object.fromEntries(
      fixtures.map((fixture) => {
        rolling += 1;
        return [
          fixture.id,
          {
            homeGoals: rolling % 4,
            awayGoals: (rolling + 2) % 4
          }
        ];
      })
    );

    state = advanceSeasonState(state, resultsByFixtureId);

    const boardContext = deriveSeasonBoardContextFromSeasonState(state, staticContext);
    const evaluations = evaluateSeasonBoardContext(state, boardContext);

    for (const club of clubs) {
      const risk = evaluations[club.id].sackRisk;
      timelinesByClubId[club.id].push(risk);

      if (club.id === "club-a") {
        const pressure = deriveSackRiskPressureState(risk, previousRiskByClubId[club.id]);
        timelineRows.push({
          matchdayComplete: state.currentMatchday - 1,
          clubId: "club-a",
          recentPPM: boardContext["club-a"].recentPointsPerMatch,
          sackRisk: risk,
          level: pressure.level,
          trend: pressure.trend
        });
      }

      previousRiskByClubId[club.id] = risk;
    }
  }

  return {
    seasonState: state,
    staticContext,
    timelineRows,
    timelinesByClubId
  };
}

function runStep2BoardContextManualCheck() {
  const { seasonState, staticContext, timelineRows, timelinesByClubId } = buildSeasonStateAndTimeline();

  const boardContext = deriveSeasonBoardContextFromSeasonState(seasonState, staticContext);
  const evaluations = evaluateSeasonBoardContext(seasonState, boardContext);
  const decisionSnapshots = evaluateSeasonBoardDecisions(seasonState, boardContext, timelinesByClubId);

  const rows = seasonState.standings.map((row, index) => {
    const board = evaluations[row.clubId];
    const snapshot = decisionSnapshots[row.clubId];
    return {
      clubId: row.clubId,
      leaguePosition: index + 1,
      points: row.points,
      recentPPM: boardContext[row.clubId].recentPointsPerMatch,
      boardDelta: board.boardDelta,
      sackRisk: board.sackRisk,
      sackDecision: snapshot.sackDecision.decision,
      reasons: board.reasonSummary.join(" | ")
    };
  });

  console.log("Step 2 Manual Check");
  console.log("- Season standings and board reasoning sample");
  console.table(rows);
  console.log("- Matchday sack-risk progression sample (club-a)");
  console.table(timelineRows);

  const clubASnapshot = decisionSnapshots["club-a"];
  const pressureSummary = clubASnapshot.pressureSummary;
  const sackDecision = clubASnapshot.sackDecision;
  console.log("- Season pressure streak summary (club-a)");
  console.table([pressureSummary]);
  console.log("- Season sack decision sample (club-a)");
  console.table([
    {
      decision: sackDecision.decision,
      reasons: sackDecision.reasonSummary.join(" | ")
    }
  ]);

  const completedBoardSummary = summarizeCompletedSeasonBoardOutcomes(
    seasonState,
    boardContext,
    timelinesByClubId,
    1,
    1
  );
  const promotionOutcome = completedBoardSummary.completedSeason.promotionRelegation;
  const completedSummary = completedBoardSummary.completedSeason;
  console.log("- Promotion / relegation resolution sample");
  console.table([
    {
      promotedClubIds: promotionOutcome.promotedClubIds.join(", "),
      relegatedClubIds: promotionOutcome.relegatedClubIds.join(", ")
    }
  ]);
  console.log("- Completed season board artifact sample");
  console.table(
    completedSummary.finalStandings.map((row, index) => ({
      clubId: row.clubId,
      finalPosition: index + 1,
      decision: completedBoardSummary.decisionSnapshotsByClubId[row.clubId].sackDecision.decision,
      decisionReasons: completedBoardSummary.decisionSnapshotsByClubId[row.clubId].sackDecision.reasonSummary.join(" | ")
    }))
  );

  const validReasonCoverage = rows.every((entry) => entry.reasons.length > 0);
  const validSackRiskRange = rows.every((entry) => entry.sackRisk >= 0 && entry.sackRisk <= 1);
  const validTimelineRange = timelineRows.every((entry) => entry.sackRisk >= 0 && entry.sackRisk <= 1);
  const validTimelineFlags = timelineRows.every((entry) => entry.level.length > 0 && entry.trend.length > 0);
  const validStreakSummary = pressureSummary.samplesProcessed === timelineRows.length;
  const validSnapshotCoverage = Object.keys(decisionSnapshots).length === seasonState.standings.length;
  const validSackDecision =
    sackDecision.reasonSummary.length > 0 &&
    seasonState.standings.every((row) => decisionSnapshots[row.clubId].sackDecision.reasonSummary.length > 0);
  const validCompletedBoardSnapshots =
    Object.keys(completedBoardSummary.decisionSnapshotsByClubId).length === seasonState.standings.length;
  const validPromotionResolution =
    promotionOutcome.promotedClubIds.length === 1 &&
    promotionOutcome.relegatedClubIds.length === 1 &&
    promotionOutcome.promotedClubIds[0] !== promotionOutcome.relegatedClubIds[0];
  const validCompletedSummary = completedSummary.finalStandings.length === seasonState.standings.length;

  if (
    !validReasonCoverage ||
    !validSackRiskRange ||
    !validTimelineRange ||
    !validTimelineFlags ||
    !validStreakSummary ||
    !validSnapshotCoverage ||
    !validSackDecision ||
    !validCompletedBoardSnapshots ||
    !validPromotionResolution ||
    !validCompletedSummary
  ) {
    console.error("Step 2 manual check failed expected board-context thresholds.");
    process.exit(1);
  }
}

runStep2BoardContextManualCheck();
