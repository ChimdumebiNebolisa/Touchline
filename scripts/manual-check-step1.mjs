import {
  applyPostMatchFallout,
  buildTransferFollowUpEvent,
  runInstantMatch,
  runLiveMatch
} from "../packages/sim-core/dist/src/index.js";

function createPlayer(teamPrefix, index, quality) {
  return {
    id: `${teamPrefix}-p${index + 1}`,
    name: `Player ${index + 1}`,
    finishing: quality,
    creativity: quality - 3,
    defending: quality - 6,
    discipline: 70,
    durability: 74,
    condition: 92
  };
}

function createTeam(teamId, quality, tactics) {
  const lineup = Array.from({ length: 11 }, (_, index) => createPlayer(teamId, index, quality));
  const bench = Array.from({ length: 5 }, (_, index) => createPlayer(`${teamId}-b`, index, quality - 3));

  return {
    teamId,
    name: teamId,
    morale: 0.6,
    lineup,
    bench,
    tactics,
    substitutions: [
      {
        minute: 65,
        playerOutId: lineup[10].id,
        playerIn: bench[0]
      }
    ]
  };
}

const tactics = {
  blockHeight: 0.56,
  pressingIntensity: 0.61,
  width: 0.54,
  tempo: 0.57,
  risk: 0.5
};

const matchInput = {
  seed: 9206,
  home: createTeam("Harbor FC", 73, tactics),
  away: createTeam("Rivergate City", 71, tactics)
};

const instant = runInstantMatch(matchInput);
const live = runLiveMatch(matchInput);

const parityOk =
  JSON.stringify(instant.result) === JSON.stringify(live.result) &&
  JSON.stringify(instant.stats) === JSON.stringify(live.stats);

const fallout = applyPostMatchFallout({
  state: {
    boardConfidence: 54,
    fanSentiment: 53,
    teamMorale: 55,
    managerReputation: 52
  },
  matchOutcome: instant,
  context: {
    expectationBand: "competitive",
    identityStyleFit: 0.69,
    financialPressure: 0.52,
    mediaHeat: 0.45,
    isDerby: false,
    brokenPromise: true,
    mediaCommentTone: "neutral"
  }
});

const transferEvent = buildTransferFollowUpEvent(
  {
    id: "target-voss",
    name: "Aurel Voss",
    roleFit: 0.76,
    projectFit: 0.74,
    wageDemand: 185,
    pathwayPreference: 0.7,
    competitionTolerance: 0.52,
    reputationSensitivity: 0.82
  },
  {
    clubWageBudget: 230,
    clubStature: 0.56,
    managerReputation: fallout.nextState.managerReputation,
    squadCompetition: 0.52,
    pathwayClarity: 0.68,
    recentPromiseBreak: true,
    boardWageDiscipline: 0.58
  }
);

const changedValues = [
  fallout.deltas.boardConfidence,
  fallout.deltas.fanSentiment,
  fallout.deltas.teamMorale,
  fallout.deltas.managerReputation
].filter((delta) => delta !== 0).length;

console.log("Step 1 Manual Check");
console.log("- Instant/Live parity:", parityOk ? "PASS" : "FAIL");
console.log("- Match score:", `${instant.result.homeGoals}-${instant.result.awayGoals}`);
console.log("- Fallout deltas:", fallout.deltas);
console.log("- Transfer event accepted:", transferEvent.accepted);
console.log("- Transfer reasons:", transferEvent.reasonSummary.join(" | "));

if (!parityOk || changedValues < 2 || transferEvent.reasonSummary.length < 2) {
  console.error("Step 1 manual check failed expected thresholds.");
  process.exit(1);
}
