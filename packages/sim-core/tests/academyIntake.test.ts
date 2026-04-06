import { describe, expect, it } from "vitest";

import { generateAcademyIntake } from "../src/index.js";

describe("academy intake", () => {
  it("produces deterministic intake output for identical inputs", () => {
    const firstRun = generateAcademyIntake({
      clubId: "club-a",
      seasonYear: 2028,
      seed: 991,
      academyQuality: 0.62,
      pathwayBias: 0.58,
      intakeSize: 8
    });
    const secondRun = generateAcademyIntake({
      clubId: "club-a",
      seasonYear: 2028,
      seed: 991,
      academyQuality: 0.62,
      pathwayBias: 0.58,
      intakeSize: 8
    });

    expect(secondRun).toEqual(firstRun);
    expect(firstRun.prospects).toHaveLength(8);
    expect(firstRun.conciseSummary[0]).toContain("elite");
    expect(firstRun.pathwayPressure.reasonSummary.length).toBeGreaterThan(0);
  });

  it("raises average potential and high-potential yield for stronger academy quality", () => {
    const lowQualityRuns = Array.from({ length: 120 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-b",
        seasonYear: 2030 + seasonOffset,
        seed: 707,
        academyQuality: 0.2,
        pathwayBias: 0.55,
        intakeSize: 10
      })
    );
    const highQualityRuns = Array.from({ length: 120 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-b",
        seasonYear: 2030 + seasonOffset,
        seed: 707,
        academyQuality: 0.85,
        pathwayBias: 0.55,
        intakeSize: 10
      })
    );

    const lowQualityAveragePotential =
      lowQualityRuns.reduce((sum, run) => sum + run.averagePotential, 0) / lowQualityRuns.length;
    const highQualityAveragePotential =
      highQualityRuns.reduce((sum, run) => sum + run.averagePotential, 0) / highQualityRuns.length;

    const lowQualityHighPotentialRate =
      lowQualityRuns.reduce((sum, run) => sum + run.highPotentialCount, 0) /
      lowQualityRuns.reduce((sum, run) => sum + run.prospects.length, 0);
    const highQualityHighPotentialRate =
      highQualityRuns.reduce((sum, run) => sum + run.highPotentialCount, 0) /
      highQualityRuns.reduce((sum, run) => sum + run.prospects.length, 0);

    expect(highQualityAveragePotential).toBeGreaterThan(lowQualityAveragePotential);
    expect(highQualityHighPotentialRate).toBeGreaterThan(lowQualityHighPotentialRate);
  });

  it("keeps elite outcomes rare in long-run intake calibration", () => {
    const runs = Array.from({ length: 400 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-c",
        seasonYear: 2040 + seasonOffset,
        seed: 313,
        academyQuality: 0.6,
        pathwayBias: 0.5,
        intakeSize: 10
      })
    );

    const totalProspects = runs.reduce((sum, run) => sum + run.prospects.length, 0);
    const eliteCount = runs.reduce((sum, run) => sum + run.eliteCount, 0);
    const eliteRate = eliteCount / totalProspects;

    expect(eliteRate).toBeGreaterThan(0);
    expect(eliteRate).toBeLessThan(0.06);
  });

  it("keeps elite rarity calibrated across academy-quality bands", () => {
    const runBand = (academyQuality: number) => {
      const runs = Array.from({ length: 320 }, (_, seasonOffset) =>
        generateAcademyIntake({
          clubId: `club-band-${academyQuality}`,
          seasonYear: 2100 + seasonOffset,
          seed: 177,
          academyQuality,
          pathwayBias: 0.52,
          intakeSize: 10
        })
      );

      const totalProspects = runs.reduce((sum, run) => sum + run.prospects.length, 0);
      const eliteCount = runs.reduce((sum, run) => sum + run.eliteCount, 0);

      return eliteCount / totalProspects;
    };

    const lowBandEliteRate = runBand(0.2);
    const midBandEliteRate = runBand(0.55);
    const highBandEliteRate = runBand(0.85);

    expect(lowBandEliteRate).toBeLessThan(midBandEliteRate);
    expect(midBandEliteRate).toBeLessThan(highBandEliteRate);
    expect(highBandEliteRate).toBeLessThan(0.07);
  });

  it("increases first-team recommendations with stronger pathway bias", () => {
    const lowBiasRuns = Array.from({ length: 140 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-d",
        seasonYear: 2050 + seasonOffset,
        seed: 919,
        academyQuality: 0.68,
        pathwayBias: 0.2,
        intakeSize: 8
      })
    );
    const highBiasRuns = Array.from({ length: 140 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-d",
        seasonYear: 2050 + seasonOffset,
        seed: 919,
        academyQuality: 0.68,
        pathwayBias: 0.85,
        intakeSize: 8
      })
    );

    const firstTeamCountForRuns = (runs: ReturnType<typeof generateAcademyIntake>[]) =>
      runs.reduce(
        (sum, run) =>
          sum +
          run.prospects.filter((prospect) => prospect.pathwayRecommendation === "first-team-minutes").length,
        0
      );

    const lowBiasFirstTeamRate = firstTeamCountForRuns(lowBiasRuns) /
      lowBiasRuns.reduce((sum, run) => sum + run.prospects.length, 0);
    const highBiasFirstTeamRate = firstTeamCountForRuns(highBiasRuns) /
      highBiasRuns.reduce((sum, run) => sum + run.prospects.length, 0);

    expect(highBiasFirstTeamRate).toBeGreaterThan(lowBiasFirstTeamRate);
  });

  it("keeps pathway-pressure signals deterministic for identical intake inputs", () => {
    const firstRun = generateAcademyIntake({
      clubId: "club-e",
      seasonYear: 2064,
      seed: 122,
      academyQuality: 0.74,
      pathwayBias: 0.51,
      intakeSize: 9
    });
    const secondRun = generateAcademyIntake({
      clubId: "club-e",
      seasonYear: 2064,
      seed: 122,
      academyQuality: 0.74,
      pathwayBias: 0.51,
      intakeSize: 9
    });

    expect(secondRun.pathwayPressure).toEqual(firstRun.pathwayPressure);
    expect(firstRun.pathwayPressure.blockageScore).toBeGreaterThanOrEqual(0);
    expect(firstRun.pathwayPressure.blockageScore).toBeLessThanOrEqual(1);
  });

  it("raises pathway pressure under higher academy-quality intake demand", () => {
    const lowQualityRuns = Array.from({ length: 120 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-f",
        seasonYear: 2070 + seasonOffset,
        seed: 455,
        academyQuality: 0.25,
        pathwayBias: 0.5,
        intakeSize: 10
      })
    );
    const highQualityRuns = Array.from({ length: 120 }, (_, seasonOffset) =>
      generateAcademyIntake({
        clubId: "club-f",
        seasonYear: 2070 + seasonOffset,
        seed: 455,
        academyQuality: 0.85,
        pathwayBias: 0.5,
        intakeSize: 10
      })
    );

    const averageBlockageScore = (runs: ReturnType<typeof generateAcademyIntake>[]) =>
      runs.reduce((sum, run) => sum + run.pathwayPressure.blockageScore, 0) / runs.length;

    expect(averageBlockageScore(highQualityRuns)).toBeGreaterThan(averageBlockageScore(lowQualityRuns));
  });

  it("rejects intake requests without any prospect slots", () => {
    expect(() =>
      generateAcademyIntake({
        clubId: "club-z",
        seasonYear: 2035,
        seed: 11,
        academyQuality: 0.5,
        pathwayBias: 0.5,
        intakeSize: 0
      })
    ).toThrow("Academy intake size must be greater than zero.");
  });
});
