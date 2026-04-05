import type { MatchPlayer, MatchTeamInput, PlannedSubstitution, TacticalSetup } from "./types.js";

export interface MatchPreparationState {
  team: MatchTeamInput;
}

export type MatchPreparationCommand =
  | {
      type: "set-tactics";
      tactics: TacticalSetup;
    }
  | {
      type: "set-lineup";
      lineupPlayerIds: string[];
    }
  | {
      type: "plan-substitution";
      substitution: PlannedSubstitution;
    };

export interface MatchPreparationCommandResult {
  applied: boolean;
  reason: string;
  state: MatchPreparationState;
}

function clonePlayer(player: MatchPlayer): MatchPlayer {
  return { ...player };
}

function cloneState(state: MatchPreparationState): MatchPreparationState {
  return {
    team: {
      ...state.team,
      tactics: { ...state.team.tactics },
      lineup: state.team.lineup.map(clonePlayer),
      bench: state.team.bench.map(clonePlayer),
      substitutions: state.team.substitutions.map((substitution) => ({
        ...substitution,
        playerIn: clonePlayer(substitution.playerIn)
      }))
    }
  };
}

function isNormalizedValue(value: number): boolean {
  return Number.isFinite(value) && value >= 0 && value <= 1;
}

function validateTactics(tactics: TacticalSetup): string | null {
  const entries: Array<[keyof TacticalSetup, number]> = [
    ["blockHeight", tactics.blockHeight],
    ["pressingIntensity", tactics.pressingIntensity],
    ["width", tactics.width],
    ["tempo", tactics.tempo],
    ["risk", tactics.risk]
  ];

  for (const [key, value] of entries) {
    if (!isNormalizedValue(value)) {
      return `Invalid tactics: ${key} must be between 0 and 1.`;
    }
  }

  return null;
}

function findPlayerById(players: MatchPlayer[], playerId: string): MatchPlayer | null {
  return players.find((player) => player.id === playerId) ?? null;
}

function allSquadPlayers(team: MatchTeamInput): MatchPlayer[] {
  return [...team.lineup, ...team.bench];
}

function applySetTactics(
  state: MatchPreparationState,
  tactics: TacticalSetup
): MatchPreparationCommandResult {
  const validationError = validateTactics(tactics);
  if (validationError) {
    return {
      applied: false,
      reason: validationError,
      state
    };
  }

  const nextState = cloneState(state);
  nextState.team.tactics = { ...tactics };
  return {
    applied: true,
    reason: "Tactics updated.",
    state: nextState
  };
}

function applySetLineup(
  state: MatchPreparationState,
  lineupPlayerIds: string[]
): MatchPreparationCommandResult {
  if (lineupPlayerIds.length !== 11) {
    return {
      applied: false,
      reason: "Lineup must contain exactly 11 players.",
      state
    };
  }

  const uniqueIds = new Set(lineupPlayerIds);
  if (uniqueIds.size !== lineupPlayerIds.length) {
    return {
      applied: false,
      reason: "Lineup cannot contain duplicate players.",
      state
    };
  }

  const squad = allSquadPlayers(state.team);
  const nextLineup: MatchPlayer[] = [];
  for (const playerId of lineupPlayerIds) {
    const player = findPlayerById(squad, playerId);
    if (!player) {
      return {
        applied: false,
        reason: `Unknown player in lineup command: ${playerId}.`,
        state
      };
    }
    nextLineup.push(player);
  }

  const nextBench = squad.filter((player) => !uniqueIds.has(player.id));

  const nextState = cloneState(state);
  nextState.team.lineup = nextLineup.map(clonePlayer);
  nextState.team.bench = nextBench.map(clonePlayer);

  return {
    applied: true,
    reason: "Lineup updated.",
    state: nextState
  };
}

function applyPlanSubstitution(
  state: MatchPreparationState,
  substitution: PlannedSubstitution
): MatchPreparationCommandResult {
  if (!Number.isInteger(substitution.minute) || substitution.minute <= 0 || substitution.minute > 120) {
    return {
      applied: false,
      reason: "Substitution minute must be an integer between 1 and 120.",
      state
    };
  }

  const outPlayer = findPlayerById(state.team.lineup, substitution.playerOutId);
  if (!outPlayer) {
    return {
      applied: false,
      reason: `Cannot substitute out unknown lineup player: ${substitution.playerOutId}.`,
      state
    };
  }

  const inPlayer = findPlayerById(state.team.bench, substitution.playerIn.id);
  if (!inPlayer) {
    return {
      applied: false,
      reason: `Cannot substitute in non-bench player: ${substitution.playerIn.id}.`,
      state
    };
  }

  const nextState = cloneState(state);
  nextState.team.substitutions = [
    ...nextState.team.substitutions,
    {
      ...substitution,
      playerIn: clonePlayer(substitution.playerIn)
    }
  ];

  return {
    applied: true,
    reason: "Substitution planned.",
    state: nextState
  };
}

export function applyMatchPreparationCommand(
  state: MatchPreparationState,
  command: MatchPreparationCommand
): MatchPreparationCommandResult {
  switch (command.type) {
    case "set-tactics":
      return applySetTactics(state, command.tactics);
    case "set-lineup":
      return applySetLineup(state, command.lineupPlayerIds);
    case "plan-substitution":
      return applyPlanSubstitution(state, command.substitution);
    default:
      return {
        applied: false,
        reason: "Unsupported match preparation command.",
        state
      };
  }
}
