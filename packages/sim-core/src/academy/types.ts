export type AcademyProspectTier = "elite" | "high" | "regular";

export type AcademyPathwayRecommendation =
  | "first-team-minutes"
  | "loan-pathway"
  | "academy-development";

export interface AcademyIntakeInput {
  clubId: string;
  seasonYear: number;
  seed: number;
  academyQuality: number;
  pathwayBias: number;
  intakeSize: number;
}

export interface AcademyProspect {
  id: string;
  potential: number;
  readiness: number;
  tier: AcademyProspectTier;
  pathwayRecommendation: AcademyPathwayRecommendation;
}

export interface AcademyIntakeResult {
  clubId: string;
  seasonYear: number;
  prospects: AcademyProspect[];
  eliteCount: number;
  highPotentialCount: number;
  averagePotential: number;
  conciseSummary: string[];
}
