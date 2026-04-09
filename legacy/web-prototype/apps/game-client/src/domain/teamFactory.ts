import type { MatchPlayer, MatchTeamInput, TacticalSetup } from "@touchline/sim-core";

function createPlayer(clubKey: string, index: number): MatchPlayer {
  const qualityBand = 66 + ((index * 7 + clubKey.length) % 18);
  return {
    id: `${clubKey}-p${index + 1}`,
    name: `Player ${index + 1}`,
    finishing: qualityBand,
    creativity: qualityBand - 3,
    defending: qualityBand - 7,
    discipline: 68 + ((index * 3) % 20),
    durability: 70 + ((index * 4) % 18),
    condition: 90 - (index % 4)
  };
}

export function createClubSquad(clubId: string): { lineup: MatchPlayer[]; bench: MatchPlayer[] } {
  const players = Array.from({ length: 18 }, (_, index) => createPlayer(clubId, index));
  return {
    lineup: players.slice(0, 11),
    bench: players.slice(11)
  };
}

export function createTeamInput(
  teamId: string,
  name: string,
  tactics: TacticalSetup,
  lineup: MatchPlayer[],
  bench: MatchPlayer[]
): MatchTeamInput {
  return {
    teamId,
    name,
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
