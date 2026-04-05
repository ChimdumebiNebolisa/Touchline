export interface WorldClub {
  id: string;
  name: string;
  strength: number;
}

export interface Fixture {
  id: string;
  matchday: number;
  homeClubId: string;
  awayClubId: string;
}

export interface FixtureResult {
  homeGoals: number;
  awayGoals: number;
}

export interface StandingsRow {
  clubId: string;
  played: number;
  wins: number;
  draws: number;
  losses: number;
  goalsFor: number;
  goalsAgainst: number;
  goalDifference: number;
  points: number;
}

export interface StandingsUpdateInput {
  standings: StandingsRow[];
  fixture: Fixture;
  result: FixtureResult;
}

function createRow(clubId: string): StandingsRow {
  return {
    clubId,
    played: 0,
    wins: 0,
    draws: 0,
    losses: 0,
    goalsFor: 0,
    goalsAgainst: 0,
    goalDifference: 0,
    points: 0
  };
}

function rotateTeams(teamIds: string[]): string[] {
  const fixed = teamIds[0];
  const rotatable = teamIds.slice(1);
  const last = rotatable.pop();
  if (!last) {
    return teamIds;
  }

  return [fixed, last, ...rotatable];
}

export function createRoundRobinFixtures(clubIds: string[]): Fixture[] {
  if (clubIds.length < 2) {
    return [];
  }

  const hasOddCount = clubIds.length % 2 !== 0;
  const working = hasOddCount ? [...clubIds, "__BYE__"] : [...clubIds];
  let roundTeams = [...working];

  const firstLeg: Fixture[] = [];
  const rounds = roundTeams.length - 1;

  for (let round = 0; round < rounds; round += 1) {
    const pairs = roundTeams.length / 2;
    for (let pairIndex = 0; pairIndex < pairs; pairIndex += 1) {
      const homeCandidate = roundTeams[pairIndex];
      const awayCandidate = roundTeams[roundTeams.length - 1 - pairIndex];

      if (homeCandidate === "__BYE__" || awayCandidate === "__BYE__") {
        continue;
      }

      const homeClubId = round % 2 === 0 ? homeCandidate : awayCandidate;
      const awayClubId = round % 2 === 0 ? awayCandidate : homeCandidate;

      firstLeg.push({
        id: `md-${round + 1}-${homeClubId}-${awayClubId}`,
        matchday: round + 1,
        homeClubId,
        awayClubId
      });
    }

    roundTeams = rotateTeams(roundTeams);
  }

  const secondLeg = firstLeg.map((fixture) => ({
    ...fixture,
    id: `md-${fixture.matchday + rounds}-${fixture.awayClubId}-${fixture.homeClubId}`,
    matchday: fixture.matchday + rounds,
    homeClubId: fixture.awayClubId,
    awayClubId: fixture.homeClubId
  }));

  return [...firstLeg, ...secondLeg].sort((a, b) => a.matchday - b.matchday);
}

export function createInitialStandings(clubIds: string[]): StandingsRow[] {
  return clubIds.map((clubId) => createRow(clubId));
}

function applyToRow(row: StandingsRow, goalsFor: number, goalsAgainst: number): StandingsRow {
  const won = goalsFor > goalsAgainst;
  const drawn = goalsFor === goalsAgainst;
  const lost = goalsFor < goalsAgainst;

  const pointsGain = won ? 3 : drawn ? 1 : 0;

  return {
    ...row,
    played: row.played + 1,
    wins: row.wins + (won ? 1 : 0),
    draws: row.draws + (drawn ? 1 : 0),
    losses: row.losses + (lost ? 1 : 0),
    goalsFor: row.goalsFor + goalsFor,
    goalsAgainst: row.goalsAgainst + goalsAgainst,
    goalDifference: row.goalDifference + (goalsFor - goalsAgainst),
    points: row.points + pointsGain
  };
}

function standingsSort(a: StandingsRow, b: StandingsRow): number {
  if (b.points !== a.points) {
    return b.points - a.points;
  }

  if (b.goalDifference !== a.goalDifference) {
    return b.goalDifference - a.goalDifference;
  }

  if (b.goalsFor !== a.goalsFor) {
    return b.goalsFor - a.goalsFor;
  }

  return a.clubId.localeCompare(b.clubId);
}

export function applyResultToStandings(input: StandingsUpdateInput): StandingsRow[] {
  const next = input.standings.map((row) => ({ ...row }));
  const homeIndex = next.findIndex((row) => row.clubId === input.fixture.homeClubId);
  const awayIndex = next.findIndex((row) => row.clubId === input.fixture.awayClubId);

  if (homeIndex < 0 || awayIndex < 0) {
    throw new Error("Fixture clubs missing from standings table.");
  }

  next[homeIndex] = applyToRow(next[homeIndex], input.result.homeGoals, input.result.awayGoals);
  next[awayIndex] = applyToRow(next[awayIndex], input.result.awayGoals, input.result.homeGoals);

  return next.sort(standingsSort);
}
