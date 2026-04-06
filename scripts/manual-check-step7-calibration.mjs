import {
  generateAcademyIntake,
  runInstantMatch
} from "../packages/sim-core/dist/src/index.js";

function clamp(value, min, max) {
  return Math.max(min, Math.min(max, value));
}

function createPlayer(teamKey, index, strength) {
  const technicalBase = strength + ((index * 5 + teamKey.length) % 9) - 4;
  const disciplineBase = 52 + ((index * 7) % 16) - 6;
  const durabilityBase = 64 + ((index * 3) % 18) - 5;

  return {
    id: `${teamKey}-p${index + 1}`,
    name: `${teamKey.toUpperCase()} Player ${index + 1}`,
    finishing: Math.round(clamp(technicalBase, 42, 94)),
    creativity: Math.round(clamp(technicalBase - 2, 40, 92)),
    defending: Math.round(clamp(technicalBase - 4, 38, 90)),
    discipline: Math.round(clamp(disciplineBase, 35, 88)),
    durability: Math.round(clamp(durabilityBase, 45, 92)),
    condition: Math.round(clamp(90 - (index % 5), 70, 95))
  };
}

function createTeam(teamId, name, strength, morale) {
  const players = Array.from({ length: 18 }, (_, index) => createPlayer(teamId, index, strength));
  const lineup = players.slice(0, 11);
  const bench = players.slice(11);

  return {
    teamId,
    name,
    morale,
    lineup,
    bench,
    tactics: {
      blockHeight: 0.5 + ((strength % 7) - 3) * 0.015,
      pressingIntensity: 0.5 + ((strength % 5) - 2) * 0.02,
      width: 0.52,
      tempo: 0.53,
      risk: 0.5 + ((strength % 9) - 4) * 0.015
    },
    substitutions: [
      {
        minute: 65,
        playerOutId: lineup[10].id,
        playerIn: bench[0]
      }
    ]
  };
}

function resolvePoints(goalsFor, goalsAgainst) {
  if (goalsFor > goalsAgainst) {
    return 3;
  }

  if (goalsFor === goalsAgainst) {
    return 1;
  }

  return 0;
}

function runMatchCalibration(matchCount) {
  let totalGoals = 0;
  let decisiveMatches = 0;
  let upsetWins = 0;
  let totalGoalEvents = 0;
  let lateGoalEvents = 0;

  const redCard = {
    samples: 0,
    points: 0
  };
  const noRedCard = {
    samples: 0,
    points: 0
  };

  for (let index = 0; index < matchCount; index += 1) {
    let homeStrength = 60 + ((index * 13) % 25);
    let awayStrength = 60 + ((index * 17 + 7) % 25);
    if (homeStrength === awayStrength) {
      awayStrength = clamp(awayStrength + 1, 60, 85);
    }

    const homeTeam = createTeam("home", "Home XI", homeStrength, 0.58);
    const awayTeam = createTeam("away", "Away XI", awayStrength, 0.56);

    const outcome = runInstantMatch({
      seed: 91000 + index,
      home: homeTeam,
      away: awayTeam
    });

    const homeGoals = outcome.result.homeGoals;
    const awayGoals = outcome.result.awayGoals;

    totalGoals += homeGoals + awayGoals;

    if (homeGoals !== awayGoals) {
      decisiveMatches += 1;
      const strongerWinner =
        (homeStrength > awayStrength && homeGoals > awayGoals) ||
        (awayStrength > homeStrength && awayGoals > homeGoals);

      if (!strongerWinner) {
        upsetWins += 1;
      }
    }

    for (const event of outcome.eventLog) {
      if (event.type === "goal") {
        totalGoalEvents += 1;
        if (event.minute >= 75) {
          lateGoalEvents += 1;
        }
      }
    }

    const homeHadRed = outcome.stats.home.redCards > 0;
    const awayHadRed = outcome.stats.away.redCards > 0;

    const homePoints = resolvePoints(homeGoals, awayGoals);
    const awayPoints = resolvePoints(awayGoals, homeGoals);

    if (homeHadRed) {
      redCard.samples += 1;
      redCard.points += homePoints;
    } else {
      noRedCard.samples += 1;
      noRedCard.points += homePoints;
    }

    if (awayHadRed) {
      redCard.samples += 1;
      redCard.points += awayPoints;
    } else {
      noRedCard.samples += 1;
      noRedCard.points += awayPoints;
    }
  }

  const averageGoalsPerMatch = totalGoals / matchCount;
  const upsetRate = decisiveMatches ? upsetWins / decisiveMatches : 0;
  const lateGoalShare = totalGoalEvents ? lateGoalEvents / totalGoalEvents : 0;
  const redCardAveragePoints = redCard.samples ? redCard.points / redCard.samples : 0;
  const noRedCardAveragePoints = noRedCard.samples ? noRedCard.points / noRedCard.samples : 0;

  return {
    matchCount,
    averageGoalsPerMatch,
    upsetRate,
    lateGoalShare,
    redCardSamples: redCard.samples,
    redCardAveragePoints,
    noRedCardAveragePoints,
    redCardPenalty: noRedCardAveragePoints - redCardAveragePoints
  };
}

function runYouthCalibration(intakeRuns) {
  let totalProspects = 0;
  let eliteProspects = 0;
  let highPotentialProspects = 0;

  for (let index = 0; index < intakeRuns; index += 1) {
    const academyQuality = 0.25 + ((index * 19) % 56) / 100;
    const pathwayBias = 0.2 + ((index * 23) % 61) / 100;
    const squadCongestion = ((index * 29) % 100) / 100;

    const intake = generateAcademyIntake({
      clubId: `club-${index % 50}`,
      seasonYear: 2030 + Math.floor(index / 50),
      seed: 120000 + index,
      academyQuality,
      pathwayBias,
      squadCongestion,
      intakeSize: 12
    });

    totalProspects += intake.prospects.length;
    eliteProspects += intake.eliteCount;
    highPotentialProspects += intake.highPotentialCount;
  }

  return {
    intakeRuns,
    totalProspects,
    eliteRate: eliteProspects / totalProspects,
    highPotentialRate: highPotentialProspects / totalProspects
  };
}

function main() {
  const matchMetrics = runMatchCalibration(500);
  const youthMetrics = runYouthCalibration(1200);

  console.log("Step 7 Calibration Check");
  console.log("- Match metric summary");
  console.table([
    {
      matchCount: matchMetrics.matchCount,
      averageGoalsPerMatch: Number(matchMetrics.averageGoalsPerMatch.toFixed(3)),
      upsetRate: Number(matchMetrics.upsetRate.toFixed(3)),
      lateGoalShare: Number(matchMetrics.lateGoalShare.toFixed(3)),
      redCardSamples: matchMetrics.redCardSamples,
      redCardAveragePoints: Number(matchMetrics.redCardAveragePoints.toFixed(3)),
      noRedCardAveragePoints: Number(matchMetrics.noRedCardAveragePoints.toFixed(3)),
      redCardPenalty: Number(matchMetrics.redCardPenalty.toFixed(3))
    }
  ]);

  console.log("- Youth rarity summary");
  console.table([
    {
      intakeRuns: youthMetrics.intakeRuns,
      totalProspects: youthMetrics.totalProspects,
      eliteRate: Number(youthMetrics.eliteRate.toFixed(4)),
      highPotentialRate: Number(youthMetrics.highPotentialRate.toFixed(4))
    }
  ]);

  const checks = {
    goalsInPlausibleRange:
      matchMetrics.averageGoalsPerMatch >= 1.8 && matchMetrics.averageGoalsPerMatch <= 4.2,
    upsetRateInPlausibleRange: matchMetrics.upsetRate >= 0.08 && matchMetrics.upsetRate <= 0.45,
    lateGoalShareInPlausibleRange:
      matchMetrics.lateGoalShare >= 0.08 && matchMetrics.lateGoalShare <= 0.5,
    redCardSampleSufficient: matchMetrics.redCardSamples >= 20,
    redCardsReduceAveragePoints: matchMetrics.redCardPenalty > 0,
    eliteYouthRemainsRare: youthMetrics.eliteRate <= 0.06,
    highPotentialRatePlausible: youthMetrics.highPotentialRate <= 0.35
  };

  console.log("- Threshold checks");
  console.table([checks]);

  if (Object.values(checks).some((result) => !result)) {
    console.error("Step 7 calibration check failed expected thresholds.");
    process.exit(1);
  }
}

main();
