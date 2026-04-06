export type AcademyProspectTier = "elite" | "high" | "regular";

export type AcademyPathwayRecommendation =
  | "first-team-minutes"
  | "loan-pathway"
  | "academy-development";

export type AcademyPathwayBlockageRisk = "low" | "moderate" | "high";

export interface AcademyIntakeInput {
  clubId: string;
  seasonYear: number;
  seed: number;
  academyQuality: number;
  pathwayBias: number;
  squadCongestion?: number;
  intakeSize: number;
}

export interface AcademyProspect {
  id: string;
  potential: number;
  readiness: number;
  tier: AcademyProspectTier;
  pathwayRecommendation: AcademyPathwayRecommendation;
}

export interface AcademyPathwayPressureSignal {
  firstTeamCandidateCount: number;
  loanCandidateCount: number;
  promotionPressure: number;
  loanPressure: number;
  blockageScore: number;
  blockageRisk: AcademyPathwayBlockageRisk;
  reasonSummary: string[];
}

export interface AcademyIntakeResult {
  clubId: string;
  seasonYear: number;
  prospects: AcademyProspect[];
  eliteCount: number;
  highPotentialCount: number;
  averagePotential: number;
  pathwayPressure: AcademyPathwayPressureSignal;
  conciseSummary: string[];
}
