import type { AcademyIntakeInput } from "../../src/index.js";

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
