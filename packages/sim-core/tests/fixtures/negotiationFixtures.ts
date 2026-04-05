import type { TransferEvaluationContext, TransferTargetProfile } from "../../src/index.js";

export const calibrationTarget: TransferTargetProfile = {
  id: "target-calibration-1",
  name: "Luca Marin",
  roleFit: 0.69,
  projectFit: 0.64,
  wageDemand: 188,
  pathwayPreference: 0.74,
  competitionTolerance: 0.52,
  reputationSensitivity: 0.84
};

export const calibrationBaseContext: TransferEvaluationContext = {
  clubWageBudget: 240,
  clubStature: 0.58,
  managerReputation: 62,
  squadCompetition: 0.52,
  pathwayClarity: 0.7,
  recentPromiseBreak: false,
  boardWageDiscipline: 0.55
};

export const calibrationPromiseVariants = [
  { pathwayClarity: 0.78, squadCompetition: 0.52 },
  { pathwayClarity: 0.7, squadCompetition: 0.56 },
  { pathwayClarity: 0.62, squadCompetition: 0.58 },
  { pathwayClarity: 0.58, squadCompetition: 0.61 }
];

export const calibrationReputationBands = [82, 62, 28];

export const calibrationLogContexts: TransferEvaluationContext[] = [
  {
    ...calibrationBaseContext,
    managerReputation: 82,
    pathwayClarity: 0.84,
    squadCompetition: 0.5,
    recentPromiseBreak: false
  },
  {
    ...calibrationBaseContext,
    managerReputation: 62,
    pathwayClarity: 0.68,
    squadCompetition: 0.56,
    recentPromiseBreak: false
  },
  {
    ...calibrationBaseContext,
    managerReputation: 28,
    pathwayClarity: 0.35,
    squadCompetition: 0.8,
    recentPromiseBreak: true
  }
];

export const calibrationEqualFeeContexts = {
  first: {
    ...calibrationBaseContext,
    managerReputation: 68,
    pathwayClarity: 0.88,
    squadCompetition: 0.48,
    recentPromiseBreak: false
  },
  second: {
    ...calibrationBaseContext,
    managerReputation: 68,
    pathwayClarity: 0.28,
    squadCompetition: 0.85,
    recentPromiseBreak: false
  }
};