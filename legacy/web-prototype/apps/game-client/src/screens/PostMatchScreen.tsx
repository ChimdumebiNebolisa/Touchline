import type { MatchRunResult } from "../types";

interface PostMatchScreenProps {
  data: MatchRunResult;
  onRestart: () => void;
}

export function PostMatchScreen(props: PostMatchScreenProps) {
  const { data } = props;
  const { match, fallout, transferEvent } = data;

  return (
    <section className="panel">
      <header className="panel-header">
        <p className="kicker">Step 3</p>
        <h1>Post-Match Fallout</h1>
        <p>Persistent pressure and transfer consequences after one shared-engine match.</p>
      </header>

      <div className="score-banner">
        <strong>{match.result.homeGoals}</strong>
        <span>:</span>
        <strong>{match.result.awayGoals}</strong>
      </div>

      <div className="stat-grid">
        <article>
          <h2>Board Confidence</h2>
          <p>{fallout.nextState.boardConfidence.toFixed(1)}</p>
          <small>{fallout.deltas.boardConfidence >= 0 ? "+" : ""}{fallout.deltas.boardConfidence.toFixed(2)}</small>
        </article>
        <article>
          <h2>Fan Sentiment</h2>
          <p>{fallout.nextState.fanSentiment.toFixed(1)}</p>
          <small>{fallout.deltas.fanSentiment >= 0 ? "+" : ""}{fallout.deltas.fanSentiment.toFixed(2)}</small>
        </article>
        <article>
          <h2>Team Morale</h2>
          <p>{fallout.nextState.teamMorale.toFixed(1)}</p>
          <small>{fallout.deltas.teamMorale >= 0 ? "+" : ""}{fallout.deltas.teamMorale.toFixed(2)}</small>
        </article>
        <article>
          <h2>Manager Reputation</h2>
          <p>{fallout.nextState.managerReputation.toFixed(1)}</p>
          <small>
            {fallout.deltas.managerReputation >= 0 ? "+" : ""}
            {fallout.deltas.managerReputation.toFixed(2)}
          </small>
        </article>
      </div>

      <article className="event-card">
        <h2>Transfer Follow-Up</h2>
        <p>
          {transferEvent.targetName}: {transferEvent.accepted ? "Open To Join" : "Move Rejected"}
        </p>
        <ul>
          {transferEvent.reasonSummary.map((reason) => (
            <li key={reason}>{reason}</li>
          ))}
        </ul>
      </article>

      <article className="event-card">
        <h2>Match Event Log ({match.mode})</h2>
        <ul className="event-log">
          {match.eventLog.slice(0, 14).map((event, index) => (
            <li key={`${event.minute}-${event.type}-${index}`}>
              <span>{event.minute}'</span>
              <span>{event.description}</span>
            </li>
          ))}
        </ul>
      </article>

      <button type="button" className="primary" onClick={props.onRestart}>
        Run Another Match
      </button>
    </section>
  );
}
