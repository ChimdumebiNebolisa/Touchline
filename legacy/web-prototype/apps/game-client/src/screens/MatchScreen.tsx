import type { MatchConfig } from "../types";

interface MatchScreenProps {
  clubName: string;
  config: MatchConfig;
  running: boolean;
  error: string | null;
  onBack: () => void;
  onConfigChange: (next: MatchConfig) => void;
  onPlay: () => void;
}

export function MatchScreen(props: MatchScreenProps) {
  return (
    <section className="panel">
      <header className="panel-header">
        <p className="kicker">Step 2</p>
        <h1>{props.clubName}: Matchday</h1>
        <p>Run the same shared engine either instantly or in live timeline mode.</p>
      </header>

      <div className="field-row">
        <label>Match Mode</label>
        <div className="segmented">
          <button
            type="button"
            className={props.config.mode === "instant" ? "active" : ""}
            onClick={() => props.onConfigChange({ ...props.config, mode: "instant" })}
          >
            Instant Sim
          </button>
          <button
            type="button"
            className={props.config.mode === "live" ? "active" : ""}
            onClick={() => props.onConfigChange({ ...props.config, mode: "live" })}
          >
            Live 2D Timeline
          </button>
        </div>
      </div>

      <div className="field-row inline-options">
        <label>
          <input
            type="checkbox"
            checked={props.config.promiseRisk}
            onChange={(event) =>
              props.onConfigChange({
                ...props.config,
                promiseRisk: event.target.checked
              })
            }
          />
          Apply broken-promise pressure
        </label>

        <label>
          Media tone
          <select
            value={props.config.mediaTone}
            onChange={(event) =>
              props.onConfigChange({
                ...props.config,
                mediaTone: event.target.value as MatchConfig["mediaTone"]
              })
            }
          >
            <option value="calm">Calm</option>
            <option value="neutral">Neutral</option>
            <option value="provocative">Provocative</option>
          </select>
        </label>
      </div>

      {props.error ? <p className="error-message">{props.error}</p> : null}

      <div className="action-row">
        <button type="button" className="ghost" onClick={props.onBack} disabled={props.running}>
          Back
        </button>
        <button type="button" className="primary" onClick={props.onPlay} disabled={props.running}>
          {props.running ? "Running Match..." : "Play Match"}
        </button>
      </div>
    </section>
  );
}
