import {
  applyMatchPreparationCommand,
  applyPostMatchFallout,
  assertValidCountryPack,
  buildTransferFollowUpEvent,
  runInstantMatch,
  runLiveMatch
} from "@touchline/sim-core";
import type {
  ClubPerceptionState,
  CountryPack,
  MatchInput,
  MatchPreparationState,
  MatchTeamInput,
  TacticalSetup,
  TransferTargetProfile
} from "@touchline/sim-core";
import { useMemo, useState } from "react";

import firstCountryPackJson from "../../../packages/sim-core/content/countries/first-country.json";

import { createClubSquad, createTeamInput } from "./domain/teamFactory";
import { MatchScreen } from "./screens/MatchScreen";
import { PostMatchScreen } from "./screens/PostMatchScreen";
import { SquadScreen } from "./screens/SquadScreen";
import type { MatchConfig, MatchRunResult, SquadConfig, ViewStep } from "./types";

const defaultTactics: TacticalSetup = {
  blockHeight: 0.55,
  pressingIntensity: 0.58,
  width: 0.54,
  tempo: 0.56,
  risk: 0.52
};

const basePerceptionState: ClubPerceptionState = {
  boardConfidence: 55,
  fanSentiment: 52,
  teamMorale: 57,
  managerReputation: 54
};

const transferTarget: TransferTargetProfile = {
  id: "target-aurel-voss",
  name: "Aurel Voss",
  roleFit: 0.76,
  projectFit: 0.74,
  wageDemand: 185,
  pathwayPreference: 0.7,
  competitionTolerance: 0.53,
  reputationSensitivity: 0.82
};

function findPlayableClubs(pack: CountryPack): Array<{ id: string; name: string }> {
  const clubs: Array<{ id: string; name: string }> = [];
  for (const division of pack.divisions) {
    for (const club of division.clubs) {
      if (club.isPlayable) {
        clubs.push({ id: club.id, name: club.name });
      }
    }
  }

  return clubs;
}

function pickOpponent(pack: CountryPack, selectedClubId: string): { id: string; name: string } {
  const topTier = pack.divisions.find((division) => division.tier === 1) ?? pack.divisions[0];
  const opponent = topTier.clubs.find((club) => club.id !== selectedClubId) ?? topTier.clubs[0];
  return { id: opponent.id, name: opponent.name };
}

function createPreparedTeam(team: MatchTeamInput): MatchTeamInput {
  const prepState: MatchPreparationState = { team };

  const tacticsResult = applyMatchPreparationCommand(prepState, {
    type: "set-tactics",
    tactics: team.tactics
  });

  if (!tacticsResult.applied) {
    throw new Error(tacticsResult.reason);
  }

  const lineupResult = applyMatchPreparationCommand(tacticsResult.state, {
    type: "set-lineup",
    lineupPlayerIds: team.lineup.map((player) => player.id)
  });

  if (!lineupResult.applied) {
    throw new Error(lineupResult.reason);
  }

  return lineupResult.state.team;
}

export function App() {
  const [step, setStep] = useState<ViewStep>("squad");
  const [runningMatch, setRunningMatch] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [commandMessage, setCommandMessage] = useState<string | null>(null);

  const countryPack = useMemo(() => {
    return assertValidCountryPack(firstCountryPackJson as CountryPack);
  }, []);

  const playableClubs = useMemo(() => findPlayableClubs(countryPack), [countryPack]);

  const [squadConfig, setSquadConfig] = useState<SquadConfig>(() => {
    const initialClub = playableClubs[0];
    return {
      clubId: initialClub?.id ?? "",
      clubName: initialClub?.name ?? "",
      tactics: { ...defaultTactics }
    };
  });

  const [matchConfig, setMatchConfig] = useState<MatchConfig>({
    mode: "instant",
    promiseRisk: false,
    mediaTone: "neutral"
  });

  const [currentPerception, setCurrentPerception] = useState<ClubPerceptionState>(basePerceptionState);
  const [runResult, setRunResult] = useState<MatchRunResult | null>(null);

  if (!playableClubs.length) {
    return (
      <main className="app-shell">
        <section className="panel">
          <h1>No Playable Clubs</h1>
          <p>The loaded country pack has no playable club entries.</p>
        </section>
      </main>
    );
  }

  const handleClubChange = (clubId: string): void => {
    const selected = playableClubs.find((club) => club.id === clubId);
    if (!selected) {
      return;
    }
    setSquadConfig((prev) => ({ ...prev, clubId: selected.id, clubName: selected.name }));
  };

  const handleTacticChange = (key: keyof TacticalSetup, value: number): void => {
    setSquadConfig((prev) => ({
      ...prev,
      tactics: {
        ...prev.tactics,
        [key]: value
      }
    }));
  };

  const handleContinueToMatch = (): void => {
    setErrorMessage(null);
    setCommandMessage("Lineup and tactic commands validated in sim-core.");
    setStep("match");
  };

  const handlePlayMatch = (): void => {
    setRunningMatch(true);
    setErrorMessage(null);

    try {
      const homeSquad = createClubSquad(squadConfig.clubId);
      const opponent = pickOpponent(countryPack, squadConfig.clubId);
      const awaySquad = createClubSquad(opponent.id);

      const homeTeam = createPreparedTeam(
        createTeamInput(
          squadConfig.clubId,
          squadConfig.clubName,
          squadConfig.tactics,
          homeSquad.lineup,
          homeSquad.bench
        )
      );

      const awayTeam = createPreparedTeam(
        createTeamInput(
          opponent.id,
          opponent.name,
          {
            blockHeight: 0.5,
            pressingIntensity: 0.52,
            width: 0.5,
            tempo: 0.48,
            risk: 0.46
          },
          awaySquad.lineup,
          awaySquad.bench
        )
      );

      const input: MatchInput = {
        seed: 4040,
        home: homeTeam,
        away: awayTeam
      };

      const match = matchConfig.mode === "live" ? runLiveMatch(input) : runInstantMatch(input);

      const fallout = applyPostMatchFallout({
        state: currentPerception,
        matchOutcome: match,
        context: {
          expectationBand: "competitive",
          identityStyleFit: (squadConfig.tactics.tempo + squadConfig.tactics.pressingIntensity) / 2,
          financialPressure: 0.55,
          mediaHeat: 0.4,
          isDerby: false,
          brokenPromise: matchConfig.promiseRisk,
          mediaCommentTone: matchConfig.mediaTone
        }
      });

      const nextPerception = fallout.nextState;
      const transferEvent = buildTransferFollowUpEvent(transferTarget, {
        clubWageBudget: 230,
        clubStature: 0.56,
        managerReputation: nextPerception.managerReputation,
        squadCompetition: 0.52,
        pathwayClarity: 0.68,
        recentPromiseBreak: matchConfig.promiseRisk,
        boardWageDiscipline: 0.58
      });

      const result: MatchRunResult = {
        match,
        fallout,
        transferEvent,
        nextPerception
      };

      setCurrentPerception(nextPerception);
      setRunResult(result);
      setStep("post-match");
    } catch (error) {
      const message = error instanceof Error ? error.message : "Match setup failed.";
      setErrorMessage(message);
    } finally {
      setRunningMatch(false);
    }
  };

  const handleRestart = (): void => {
    setRunResult(null);
    setStep("squad");
  };

  return (
    <main className="app-shell">
      {step === "squad" ? (
        <SquadScreen
          clubs={playableClubs}
          config={squadConfig}
          commandMessage={commandMessage}
          onClubChange={handleClubChange}
          onTacticChange={handleTacticChange}
          onContinue={handleContinueToMatch}
        />
      ) : null}

      {step === "match" ? (
        <MatchScreen
          clubName={squadConfig.clubName}
          config={matchConfig}
          running={runningMatch}
          error={errorMessage}
          onBack={() => setStep("squad")}
          onConfigChange={setMatchConfig}
          onPlay={handlePlayMatch}
        />
      ) : null}

      {step === "post-match" && runResult ? (
        <PostMatchScreen data={runResult} onRestart={handleRestart} />
      ) : null}
    </main>
  );
}
