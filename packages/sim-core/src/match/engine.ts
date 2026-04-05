import { createSeededRandom } from "./rng.js";
import type {
  MatchEvent,
  MatchInput,
  MatchMode,
  MatchOutcome,
  MatchPlayer,
  MatchTeamInput,
  TeamMatchStats
} from "./types.js";

interface TeamRuntimeState {
  readonly input: MatchTeamInput;
  readonly fatigue: Map<string, number>;
  readonly redCardedPlayers: Set<string>;
  readonly injuredPlayers: Set<string>;
  stats: TeamMatchStats;
  attackBias: number;
  chanceCount: number;
}

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

function average(values: number[]): number {
  if (!values.length) {
    return 0;
  }

  const total = values.reduce((sum, value) => sum + value, 0);
  return total / values.length;
}

function createTeamState(input: MatchTeamInput): TeamRuntimeState {
  const fatigue = new Map<string, number>();
  for (const player of input.lineup) {
    fatigue.set(player.id, clamp(100 - player.condition, 0, 100));
  }

  const attackingBase = average(input.lineup.map((player) => (player.finishing + player.creativity) / 2));
  const attackingShape =
    0.78 +
    input.tactics.tempo * 0.16 +
    input.tactics.width * 0.08 +
    input.tactics.risk * 0.12 +
    input.morale * 0.08;

  return {
    input,
    fatigue,
    redCardedPlayers: new Set<string>(),
    injuredPlayers: new Set<string>(),
    stats: {
      goals: 0,
      xg: 0,
      shots: 0,
      shotsOnTarget: 0,
      yellowCards: 0,
      redCards: 0,
      injuries: 0,
      averageFatigue: 0
    },
    attackBias: attackingBase * attackingShape,
    chanceCount: 0
  };
}

function availableLineup(team: TeamRuntimeState): MatchPlayer[] {
  return team.input.lineup.filter(
    (player) => !team.redCardedPlayers.has(player.id) && !team.injuredPlayers.has(player.id)
  );
}

function defensiveStrength(team: TeamRuntimeState): number {
  const lineup = availableLineup(team);
  const defending = average(lineup.map((player) => player.defending));
  const compactness = 0.78 + team.input.tactics.blockHeight * 0.18 - team.input.tactics.risk * 0.1;
  return defending * compactness;
}

function currentFatigue(team: TeamRuntimeState): number {
  return average([...team.fatigue.values()]);
}

function effectiveAttack(team: TeamRuntimeState): number {
  const fatiguePenalty = 1 - currentFatigue(team) / 220;
  const cardPenalty = 1 - team.stats.redCards * 0.12;
  return team.attackBias * fatiguePenalty * cardPenalty;
}

function pickPlayer(team: TeamRuntimeState, random: () => number): MatchPlayer {
  const lineup = availableLineup(team);
  if (!lineup.length) {
    return team.input.lineup[0];
  }
  return lineup[Math.floor(random() * lineup.length)];
}

function applySubstitutions(
  minute: number,
  team: TeamRuntimeState,
  eventLog: MatchEvent[]
): void {
  const substitutions = team.input.substitutions.filter((substitution) => substitution.minute === minute);
  for (const substitution of substitutions) {
    const outIndex = team.input.lineup.findIndex((player) => player.id === substitution.playerOutId);
    if (outIndex < 0) {
      continue;
    }

    team.input.lineup[outIndex] = substitution.playerIn;
    team.fatigue.delete(substitution.playerOutId);
    team.fatigue.set(substitution.playerIn.id, clamp(100 - substitution.playerIn.condition, 0, 100));

    eventLog.push({
      minute,
      teamId: team.input.teamId,
      type: "substitution",
      playerId: substitution.playerIn.id,
      description: `${team.input.name} substitute ${substitution.playerIn.name} for minute ${minute}.`
    });
  }
}

function updateFatigue(team: TeamRuntimeState): void {
  const minuteFatigueGain =
    0.16 +
    team.input.tactics.pressingIntensity * 0.2 +
    team.input.tactics.tempo * 0.15 +
    team.input.tactics.risk * 0.08;

  for (const player of availableLineup(team)) {
    const current = team.fatigue.get(player.id) ?? clamp(100 - player.condition, 0, 100);
    team.fatigue.set(player.id, clamp(current + minuteFatigueGain, 0, 100));
  }
}

function maybeDisciplineOrInjury(
  minute: number,
  team: TeamRuntimeState,
  random: () => number,
  eventLog: MatchEvent[]
): void {
  const target = pickPlayer(team, random);
  const fatigue = team.fatigue.get(target.id) ?? 0;

  const cardRisk =
    0.0025 +
    (1 - target.discipline / 100) * 0.009 +
    team.input.tactics.pressingIntensity * 0.004 +
    team.input.tactics.risk * 0.004;

  if (random() < cardRisk) {
    const redRisk = 0.15 + (1 - target.discipline / 100) * 0.25;
    if (random() < redRisk) {
      team.redCardedPlayers.add(target.id);
      team.stats.redCards += 1;
      eventLog.push({
        minute,
        teamId: team.input.teamId,
        type: "red-card",
        playerId: target.id,
        description: `${target.name} is sent off for ${team.input.name}.`
      });
      return;
    }

    team.stats.yellowCards += 1;
    eventLog.push({
      minute,
      teamId: team.input.teamId,
      type: "yellow-card",
      playerId: target.id,
      description: `${target.name} receives a yellow card for ${team.input.name}.`
    });
  }

  const injuryRisk =
    0.0008 +
    fatigue / 13000 +
    team.input.tactics.pressingIntensity * 0.0015 +
    (1 - target.durability / 100) * 0.004;

  if (random() < injuryRisk) {
    team.injuredPlayers.add(target.id);
    team.stats.injuries += 1;
    eventLog.push({
      minute,
      teamId: team.input.teamId,
      type: "injury",
      playerId: target.id,
      description: `${target.name} goes down injured for ${team.input.name}.`
    });
  }
}

function maybeCreateChance(
  minute: number,
  attackingTeam: TeamRuntimeState,
  defendingTeam: TeamRuntimeState,
  random: () => number,
  eventLog: MatchEvent[]
): void {
  const attackQuality = effectiveAttack(attackingTeam);
  const defenseQuality = defensiveStrength(defendingTeam);
  const chanceProbability = clamp(0.022 + (attackQuality - defenseQuality) / 1000, 0.01, 0.12);

  if (random() > chanceProbability) {
    return;
  }

  const shooter = pickPlayer(attackingTeam, random);
  const chanceBase =
    0.04 +
    attackingTeam.input.tactics.risk * 0.12 +
    attackingTeam.input.tactics.tempo * 0.07 +
    shooter.finishing / 700;
  const pressurePenalty = defendingTeam.input.tactics.pressingIntensity * 0.04;
  const xg = clamp(chanceBase - pressurePenalty + random() * 0.1, 0.02, 0.65);

  attackingTeam.chanceCount += 1;
  attackingTeam.stats.shots += 1;
  attackingTeam.stats.xg += xg;

  eventLog.push({
    minute,
    teamId: attackingTeam.input.teamId,
    type: "chance",
    playerId: shooter.id,
    xg,
    description: `${attackingTeam.input.name} create an opening through ${shooter.name}.`
  });

  const onTargetProbability = clamp(0.28 + shooter.finishing / 210, 0.25, 0.8);
  if (random() <= onTargetProbability) {
    attackingTeam.stats.shotsOnTarget += 1;
  }

  const conversionBoost = shooter.finishing / 180 + attackingTeam.input.morale * 0.07;
  const goalProbability = clamp(xg + conversionBoost - currentFatigue(attackingTeam) / 250, 0.02, 0.85);
  if (random() <= goalProbability) {
    attackingTeam.stats.goals += 1;
    eventLog.push({
      minute,
      teamId: attackingTeam.input.teamId,
      type: "goal",
      playerId: shooter.id,
      xg,
      description: `Goal ${attackingTeam.input.name}: ${shooter.name} scores.`
    });
  }
}

function buildReasonSummary(home: TeamRuntimeState, away: TeamRuntimeState): string[] {
  const summaries: string[] = [];

  if (home.stats.xg > away.stats.xg) {
    summaries.push(`${home.input.name} generated the higher chance quality (${home.stats.xg.toFixed(2)} xG).`);
  } else if (away.stats.xg > home.stats.xg) {
    summaries.push(`${away.input.name} generated the higher chance quality (${away.stats.xg.toFixed(2)} xG).`);
  } else {
    summaries.push("Both teams produced similar expected-goal quality.");
  }

  if (home.stats.redCards || away.stats.redCards) {
    summaries.push(
      `Disciplinary impact: ${home.stats.redCards + away.stats.redCards} red card(s) shifted shape and chance flow.`
    );
  }

  const fatigueGap = Math.abs(home.stats.averageFatigue - away.stats.averageFatigue);
  if (fatigueGap >= 3) {
    const moreFatiguedTeam =
      home.stats.averageFatigue > away.stats.averageFatigue ? home.input.name : away.input.name;
    summaries.push(`${moreFatiguedTeam} carried heavier fatigue and lost late efficiency.`);
  }

  if (!summaries.length) {
    summaries.push("Match flow stayed balanced across chance quality, discipline, and fatigue.");
  }

  return summaries;
}

export function simulateMatch(input: MatchInput, mode: MatchMode): MatchOutcome {
  const randomSource = createSeededRandom(input.seed);
  const random = () => randomSource.next();

  const home = createTeamState({
    ...input.home,
    lineup: [...input.home.lineup],
    bench: [...input.home.bench],
    substitutions: [...input.home.substitutions]
  });
  const away = createTeamState({
    ...input.away,
    lineup: [...input.away.lineup],
    bench: [...input.away.bench],
    substitutions: [...input.away.substitutions]
  });
  const eventLog: MatchEvent[] = [];

  for (let minute = 1; minute <= 90; minute += 1) {
    applySubstitutions(minute, home, eventLog);
    applySubstitutions(minute, away, eventLog);

    maybeCreateChance(minute, home, away, random, eventLog);
    maybeCreateChance(minute, away, home, random, eventLog);

    maybeDisciplineOrInjury(minute, home, random, eventLog);
    maybeDisciplineOrInjury(minute, away, random, eventLog);

    updateFatigue(home);
    updateFatigue(away);
  }

  home.stats.averageFatigue = currentFatigue(home);
  away.stats.averageFatigue = currentFatigue(away);

  return {
    mode,
    seed: input.seed,
    result: {
      homeGoals: home.stats.goals,
      awayGoals: away.stats.goals
    },
    stats: {
      home: home.stats,
      away: away.stats
    },
    eventLog,
    reasonSummary: buildReasonSummary(home, away)
  };
}
