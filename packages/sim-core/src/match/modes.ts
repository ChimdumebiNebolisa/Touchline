import { simulateMatch } from "./engine.js";
import type { LiveMatchFrame, LiveMatchOutcome, MatchInput, MatchOutcome } from "./types.js";

export function runInstantMatch(input: MatchInput): MatchOutcome {
  return simulateMatch(input, "instant");
}

function toTimelineFrames(outcome: MatchOutcome): LiveMatchFrame[] {
  return outcome.eventLog.map((event, index) => ({
    frame: index + 1,
    minute: event.minute,
    event
  }));
}

export function runLiveMatch(input: MatchInput): LiveMatchOutcome {
  const sharedOutcome = simulateMatch(input, "live");
  return {
    ...sharedOutcome,
    timeline: toTimelineFrames(sharedOutcome)
  };
}
