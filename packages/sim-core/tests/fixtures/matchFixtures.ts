import type { MatchInput, MatchPlayer, MatchTeamInput, TacticalSetup } from "../../src/match/types.js";

function createPlayer(id: string, quality: number): MatchPlayer {
  return {
    id,
    name: `Player ${id}`,
    finishing: quality,
    creativity: quality - 2,
    defending: quality - 6,
    discipline: 70,
    durability: 75,
    condition: 92
  };
}

function createLineup(teamPrefix: string, quality: number): MatchPlayer[] {
  return Array.from({ length: 11 }, (_, index) => createPlayer(`${teamPrefix}-${index + 1}`, quality));
}

function createBench(teamPrefix: string, quality: number): MatchPlayer[] {
  return Array.from({ length: 5 }, (_, index) => createPlayer(`${teamPrefix}-b${index + 1}`, quality - 3));
}

export function createTeam(
  teamId: string,
  quality: number,
  tactics: TacticalSetup,
  morale: number
): MatchTeamInput {
  const lineup = createLineup(teamId, quality);
  const bench = createBench(teamId, quality);

  return {
    teamId,
    name: teamId,
    morale,
    lineup,
    bench,
    tactics,
    substitutions: [
      {
        minute: 60,
        playerOutId: lineup[9].id,
        playerIn: bench[0]
      }
    ]
  };
}

export function createMatchInput(seed: number, homeTactics: TacticalSetup, awayTactics: TacticalSetup): MatchInput {
  return {
    seed,
    home: createTeam("Harbor FC", 74, homeTactics, 0.62),
    away: createTeam("Rivergate City", 71, awayTactics, 0.58)
  };
}
