import {
  evaluateBoardExpectationContext,
  evaluateTransferDecision,
  generateAcademyIntake
} from "../packages/sim-core/dist/src/index.js";

function clamp(value, min, max) {
  return Math.max(min, Math.min(max, value));
}

function countRecommendations(prospects, recommendation) {
  return prospects.filter((prospect) => prospect.pathwayRecommendation === recommendation).length;
}

function evaluateWindow(options) {
  const target = {
    id: "target-aurel-voss",
    name: "Aurel Voss",
    roleFit: 0.76,
    projectFit: 0.74,
    wageDemand: 185,
    pathwayPreference: 0.7,
    competitionTolerance: 0.53,
    reputationSensitivity: 0.82
  };

  let totalProspects = 0;
  let totalEliteCount = 0;
  let totalHighPotentialCount = 0;
  let totalAveragePotential = 0;
  let totalBlockageScore = 0;
  let totalLoanRecommendations = 0;
  let totalFirstTeamRecommendations = 0;
  let totalTransferScore = 0;
  let totalTransferAccepted = 0;
  let totalBoardSackRisk = 0;
  let lastTransferReasons = [];

  for (let seasonOffset = 0; seasonOffset < options.seasons; seasonOffset += 1) {
    const intake = generateAcademyIntake({
      clubId: options.clubId,
      seasonYear: options.startSeasonYear + seasonOffset,
      seed: options.seed,
      academyQuality: options.academyQuality,
      pathwayBias: options.pathwayBias,
      intakeSize: options.intakeSize
    });

    const loanRecommendations = countRecommendations(intake.prospects, "loan-pathway");
    const firstTeamRecommendations = countRecommendations(intake.prospects, "first-team-minutes");

    const transferDecision = evaluateTransferDecision(target, {
      clubWageBudget: 230,
      clubStature: 0.56,
      managerReputation: 62,
      squadCompetition: clamp(0.35 + intake.pathwayPressure.blockageScore * 0.55, 0.05, 0.98),
      pathwayClarity: clamp(1 - intake.pathwayPressure.blockageScore * 0.85, 0.05, 0.95),
      recentPromiseBreak: intake.pathwayPressure.blockageRisk === "high",
      boardWageDiscipline: 0.58
    });

    const boardEvaluation = evaluateBoardExpectationContext({
      leaguePosition: 9,
      totalTeams: 20,
      preseasonObjectivePosition: 8,
      clubStature: 0.46,
      financialPressure: clamp(
        0.35 +
          intake.pathwayPressure.blockageScore * 0.2 +
          (loanRecommendations / Math.max(1, intake.prospects.length)) * 0.1,
        0,
        1
      ),
      recentPointsPerMatch: 1.32,
      styleAlignment: 0.59,
      derbyResult: "draw"
    });

    totalProspects += intake.prospects.length;
    totalEliteCount += intake.eliteCount;
    totalHighPotentialCount += intake.highPotentialCount;
    totalAveragePotential += intake.averagePotential;
    totalBlockageScore += intake.pathwayPressure.blockageScore;
    totalLoanRecommendations += loanRecommendations;
    totalFirstTeamRecommendations += firstTeamRecommendations;
    totalTransferScore += transferDecision.score;
    totalTransferAccepted += transferDecision.accepted ? 1 : 0;
    totalBoardSackRisk += boardEvaluation.sackRisk;
    lastTransferReasons = transferDecision.reasonSummary;
  }

  const usableYouthRecommendations = totalLoanRecommendations + totalFirstTeamRecommendations;

  return {
    label: options.label,
    academyQuality: options.academyQuality,
    pathwayBias: options.pathwayBias,
    seasons: options.seasons,
    intakeSize: options.intakeSize,
    averagePotential: totalAveragePotential / options.seasons,
    highPotentialRate: totalHighPotentialCount / totalProspects,
    eliteRate: totalEliteCount / totalProspects,
    averageBlockageScore: totalBlockageScore / options.seasons,
    totalLoanRecommendations,
    totalFirstTeamRecommendations,
    usableYouthRecommendationRate: usableYouthRecommendations / totalProspects,
    transferAcceptedRate: totalTransferAccepted / options.seasons,
    averageTransferScore: totalTransferScore / options.seasons,
    averageBoardSackRisk: totalBoardSackRisk / options.seasons,
    sampleTransferReasons: lastTransferReasons
  };
}

function formatPercent(value) {
  return `${(value * 100).toFixed(2)}%`;
}

function printWindowSummary(result) {
  console.table([
    {
      label: result.label,
      academyQuality: result.academyQuality.toFixed(2),
      pathwayBias: result.pathwayBias.toFixed(2),
      averagePotential: result.averagePotential.toFixed(2),
      highPotentialRate: formatPercent(result.highPotentialRate),
      eliteRate: formatPercent(result.eliteRate),
      averageBlockageScore: result.averageBlockageScore.toFixed(2),
      loanRecommendations: result.totalLoanRecommendations,
      firstTeamRecommendations: result.totalFirstTeamRecommendations,
      usableYouthRecommendationRate: formatPercent(result.usableYouthRecommendationRate),
      transferAcceptedRate: formatPercent(result.transferAcceptedRate),
      averageTransferScore: result.averageTransferScore.toFixed(2),
      averageBoardSackRisk: formatPercent(result.averageBoardSackRisk)
    }
  ]);
}

const lowQualityWindow = evaluateWindow({
  label: "low-quality",
  clubId: "club-step4-quality",
  seed: 713,
  startSeasonYear: 2090,
  seasons: 120,
  academyQuality: 0.25,
  pathwayBias: 0.55,
  intakeSize: 10
});

const highQualityWindow = evaluateWindow({
  label: "high-quality",
  clubId: "club-step4-quality",
  seed: 713,
  startSeasonYear: 2090,
  seasons: 120,
  academyQuality: 0.85,
  pathwayBias: 0.55,
  intakeSize: 10
});

const lowPathwayBiasWindow = evaluateWindow({
  label: "low-pathway-bias",
  clubId: "club-step4-pathway",
  seed: 844,
  startSeasonYear: 2096,
  seasons: 20,
  academyQuality: 0.69,
  pathwayBias: 0.35,
  intakeSize: 10
});

const highPathwayBiasWindow = evaluateWindow({
  label: "high-pathway-bias",
  clubId: "club-step4-pathway",
  seed: 844,
  startSeasonYear: 2096,
  seasons: 20,
  academyQuality: 0.69,
  pathwayBias: 0.75,
  intakeSize: 10
});

const blockedPathwayWindow =
  highPathwayBiasWindow.averageBlockageScore >= lowPathwayBiasWindow.averageBlockageScore
    ? highPathwayBiasWindow
    : lowPathwayBiasWindow;
const clearPathwayWindow =
  blockedPathwayWindow.label === highPathwayBiasWindow.label
    ? lowPathwayBiasWindow
    : highPathwayBiasWindow;

const checks = {
  qualityRaisesPotential: highQualityWindow.averagePotential > lowQualityWindow.averagePotential,
  qualityRaisesHighPotentialRate: highQualityWindow.highPotentialRate > lowQualityWindow.highPotentialRate,
  eliteOutcomesRemainRare:
    lowQualityWindow.eliteRate > 0 &&
    highQualityWindow.eliteRate > lowQualityWindow.eliteRate &&
    highQualityWindow.eliteRate < 0.07,
  strongerPathwayBiasRaisesUsableYouth:
    highPathwayBiasWindow.usableYouthRecommendationRate > lowPathwayBiasWindow.usableYouthRecommendationRate,
  blockedPathwaysRaisePressureOrLoanUsage:
    blockedPathwayWindow.totalLoanRecommendations > clearPathwayWindow.totalLoanRecommendations ||
    blockedPathwayWindow.averageTransferScore < clearPathwayWindow.averageTransferScore ||
    blockedPathwayWindow.transferAcceptedRate < clearPathwayWindow.transferAcceptedRate,
  blockedPathwaysIncreaseBoardRisk:
    blockedPathwayWindow.averageBoardSackRisk > clearPathwayWindow.averageBoardSackRisk
};

console.log("Step 4 Manual Check");
console.log("- Academy quality and rarity calibration window");
printWindowSummary(lowQualityWindow);
printWindowSummary(highQualityWindow);

console.log("- Pathway pressure and downstream transfer/board context window");
printWindowSummary(lowPathwayBiasWindow);
printWindowSummary(highPathwayBiasWindow);

console.log("- Blocked-vs-clear pathway mapping");
console.table([
  {
    blockedPathwayLabel: blockedPathwayWindow.label,
    clearPathwayLabel: clearPathwayWindow.label,
    blockedAverageBlockage: blockedPathwayWindow.averageBlockageScore.toFixed(2),
    clearAverageBlockage: clearPathwayWindow.averageBlockageScore.toFixed(2)
  }
]);

console.log("- Sample transfer reasons from blocked-pathway window");
console.log(blockedPathwayWindow.sampleTransferReasons.join(" | "));

console.log("- Threshold checks");
console.table([
  {
    qualityRaisesPotential: checks.qualityRaisesPotential,
    qualityRaisesHighPotentialRate: checks.qualityRaisesHighPotentialRate,
    eliteOutcomesRemainRare: checks.eliteOutcomesRemainRare,
    strongerPathwayBiasRaisesUsableYouth: checks.strongerPathwayBiasRaisesUsableYouth,
    blockedPathwaysRaisePressureOrLoanUsage: checks.blockedPathwaysRaisePressureOrLoanUsage,
    blockedPathwaysIncreaseBoardRisk: checks.blockedPathwaysIncreaseBoardRisk
  }
]);

if (Object.values(checks).some((result) => !result)) {
  console.error("Step 4 manual check failed expected academy-pathway thresholds.");
  process.exit(1);
}
