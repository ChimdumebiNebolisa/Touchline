import type { TacticalSetup } from "@touchline/sim-core";

import type { SquadConfig } from "../types";

interface ClubOption {
  id: string;
  name: string;
}

interface SquadScreenProps {
  clubs: ClubOption[];
  config: SquadConfig;
  commandMessage: string | null;
  onClubChange: (clubId: string) => void;
  onTacticChange: (key: keyof TacticalSetup, value: number) => void;
  onContinue: () => void;
}

const tacticLabels: Record<keyof TacticalSetup, string> = {
  blockHeight: "Block Height",
  pressingIntensity: "Pressing",
  width: "Width",
  tempo: "Tempo",
  risk: "Risk"
};

export function SquadScreen(props: SquadScreenProps) {
  const tacticKeys = Object.keys(props.config.tactics) as Array<keyof TacticalSetup & string>;
  const canContinue = props.config.clubId.length > 0;

  return (
    <section className="panel">
      <header className="panel-header">
        <p className="kicker">Step 1</p>
        <h1>Squad And Tactics</h1>
        <p>Choose your playable club, then set tactical intent before kickoff.</p>
      </header>

      <div className="field-row">
        <label htmlFor="club-select">Playable Club</label>
        <select
          id="club-select"
          value={props.config.clubId}
          onChange={(event) => props.onClubChange(event.target.value)}
        >
          {props.clubs.map((club) => (
            <option key={club.id} value={club.id}>
              {club.name}
            </option>
          ))}
        </select>
      </div>

      <div className="tactic-grid">
        {tacticKeys.map((key) => (
          <label key={key} className="slider-field">
            <span>{tacticLabels[key]}</span>
            <input
              type="range"
              min={0}
              max={1}
              step={0.01}
              value={props.config.tactics[key]}
              onChange={(event) => props.onTacticChange(key, Number(event.target.value))}
            />
            <strong>{props.config.tactics[key].toFixed(2)}</strong>
          </label>
        ))}
      </div>

      {props.commandMessage ? (
        <p className="inline-message" role="status" aria-live="polite">
          {props.commandMessage}
        </p>
      ) : null}

      <button className="primary" type="button" onClick={props.onContinue} disabled={!canContinue}>
        Continue To Match
      </button>
    </section>
  );
}
