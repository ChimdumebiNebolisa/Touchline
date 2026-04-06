import { generateAcademyIntake } from "../../src/index.js";
import type {
  AcademyIntakeInput,
  AcademyPathwayBlockageRisk,
  NegotiationScenarioVariant
} from "../../src/index.js";

export interface AcademySeasonOutputSummaryRow {
  seasonYear: number;
  eliteCount: number;
  highPotentialCount: number;
  averagePotential: number;
  blockageScore: number;
  blockageRisk: AcademyPathwayBlockageRisk;
  firstTeamCandidateCount: number;
  loanCandidateCount: number;
  loanRecommendationCount: number;
  conciseSummary: string[];
}

export interface AcademySeasonOutputSummaryArtifact {
  rows: AcademySeasonOutputSummaryRow[];
  averageBlockageScore: number;
  averagePotential: number;
  totalLoanRecommendations: number;
  conciseSummary: string[];
}

export interface AcademyLoanPathwayProgressionArtifact {
  rows: Array<{
    seasonYear: number;
    loanRecommendationCount: number;
    blockageScore: number;
    blockageRisk: AcademyPathwayBlockageRisk;
  }>;
  firstSeasonLoanRecommendations: number;
  finalSeasonLoanRecommendations: number;
  totalLoanRecommendations: number;
  conciseSummary: string[];
}

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function buildIntakeInput(
  clubId: string,
  seasonYear: number,
  seed: number,
  academyQuality: number,
  pathwayBias: number,
  intakeSize: number
): AcademyIntakeInput {
  return {
    clubId,
    seasonYear,
    seed,
    academyQuality,
    pathwayBias,
    intakeSize
  };
}

export function buildLoanPathFixtureWindow(options: {
  clubId: string;
  seed: number;
  startSeasonYear: number;
  seasons: number;
  academyQuality: number;
  pathwayBias: number;
  intakeSize: number;
}): AcademyIntakeInput[] {
  return Array.from({ length: options.seasons }, (_, seasonOffset) =>
    buildIntakeInput(
      options.clubId,
      options.startSeasonYear + seasonOffset,
      options.seed,
      options.academyQuality,
      options.pathwayBias,
      options.intakeSize
    )
  );
}

export function buildSeasonAcademyOutputSummaryArtifact(
  fixtures: AcademyIntakeInput[]
): AcademySeasonOutputSummaryArtifact {
  if (!fixtures.length) {
    throw new Error("Season academy output summary requires at least one intake fixture.");
  }

  const rows = fixtures.map((fixture) => {
    const intake = generateAcademyIntake(fixture);
    const loanRecommendationCount = intake.prospects.filter(
      (prospect) => prospect.pathwayRecommendation === "loan-pathway"
    ).length;

    return {
      seasonYear: fixture.seasonYear,
      eliteCount: intake.eliteCount,
      highPotentialCount: intake.highPotentialCount,
      averagePotential: intake.averagePotential,
      blockageScore: intake.pathwayPressure.blockageScore,
      blockageRisk: intake.pathwayPressure.blockageRisk,
      firstTeamCandidateCount: intake.pathwayPressure.firstTeamCandidateCount,
      loanCandidateCount: intake.pathwayPressure.loanCandidateCount,
      loanRecommendationCount,
      conciseSummary: intake.conciseSummary
    };
  });

  const averageBlockageScore = rows.reduce((sum, row) => sum + row.blockageScore, 0) / rows.length;
  const averagePotential = rows.reduce((sum, row) => sum + row.averagePotential, 0) / rows.length;
  const totalLoanRecommendations = rows.reduce((sum, row) => sum + row.loanRecommendationCount, 0);

  return {
    rows,
    averageBlockageScore,
    averagePotential,
    totalLoanRecommendations,
    conciseSummary: [
      `Seasons sampled: ${rows.length}.`,
      `Average blockage score ${averageBlockageScore.toFixed(2)} with average potential ${averagePotential.toFixed(2)}.`,
      `Total loan-pathway recommendations across window: ${totalLoanRecommendations}.`
    ]
  };
}

export function buildTransferVariantsFromAcademySummary(
  artifact: AcademySeasonOutputSummaryArtifact
): NegotiationScenarioVariant[] {
  return artifact.rows.map((row) => ({
    pathwayClarity: clamp(1 - row.blockageScore * 0.85, 0.05, 0.95),
    squadCompetition: clamp(0.35 + row.blockageScore * 0.55, 0.05, 0.98),
    recentPromiseBreak: row.blockageRisk === "high"
  }));
}

export function deriveBoardFinancialPressureFromAcademySummary(
  artifact: AcademySeasonOutputSummaryArtifact,
  baselineFinancialPressure: number
): number {
  const loanRecommendationDensity =
    artifact.totalLoanRecommendations / Math.max(1, artifact.rows.length * 20);

  return clamp(
    baselineFinancialPressure + artifact.averageBlockageScore * 0.2 + loanRecommendationDensity * 0.1,
    0,
    1
  );
}

export function buildLoanPathwayProgressionArtifact(
  fixtures: AcademyIntakeInput[]
): AcademyLoanPathwayProgressionArtifact {
  const summary = buildSeasonAcademyOutputSummaryArtifact(fixtures);
  const rows = summary.rows.map((row) => ({
    seasonYear: row.seasonYear,
    loanRecommendationCount: row.loanRecommendationCount,
    blockageScore: row.blockageScore,
    blockageRisk: row.blockageRisk
  }));

  const firstSeasonLoanRecommendations = rows[0].loanRecommendationCount;
  const finalSeasonLoanRecommendations = rows[rows.length - 1].loanRecommendationCount;

  return {
    rows,
    firstSeasonLoanRecommendations,
    finalSeasonLoanRecommendations,
    totalLoanRecommendations: summary.totalLoanRecommendations,
    conciseSummary: [
      `Loan-pathway progression sampled across ${rows.length} seasons.`,
      `First season loan recommendations ${firstSeasonLoanRecommendations}, final season ${finalSeasonLoanRecommendations}.`,
      `Total loan-pathway recommendations across window: ${summary.totalLoanRecommendations}.`
    ]
  };
}
