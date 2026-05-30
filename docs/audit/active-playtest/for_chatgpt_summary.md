# Touchline Evidence Audit Summary for ChatGPT

## Bottom line

Yellow: playable, but evidence shows specific weak areas.

- The logic and state evidence is strong: role-gated actions, save/load persistence, and shared match-engine assertions all passed in the active playtest logs.
- The visual evidence is mixed: `docs/audit/football-feel/` shows a mostly coherent core UI, but the active-playtest screenshot set is unreliable because 42 labeled files collapse to 4 unique images.
- The clearest UI problems visible in real screenshots are the post-match screen looking incomplete, sidebar route highlighting appearing stuck on `Matchday`, and summary cards wrapping key text badly.

## What evidence was reviewed

- Screenshot count: 60 PNG files total
  - 42 in `docs/audit/active-playtest/screenshots/`
  - 18 in `docs/audit/football-feel/`
- Log files reviewed:
  - `docs/audit/active-playtest/logs/headless-active-playtest-20260527-043907.log`
  - `docs/audit/active-playtest/logs/headless-active-playtest-20260527-044157.log`
  - `docs/audit/active-playtest/logs/headless-active-playtest-run.log`
  - `docs/audit/active-playtest/logs/full-godot-suite.log` (present but unreadable during audit because another process held the file lock)
- JSON summaries reviewed:
  - `docs/audit/active-playtest/logs/active-playtest-run-20260527-044036.json`
  - `docs/audit/active-playtest/logs/active-playtest-run-20260527-044322.json`
  - `docs/audit/active-playtest/logs/active-playtest-assertions.json`
- Videos reviewed: none
  - `docs/audit/active-playtest/videos/` exists but contains no video files
- Reports reviewed:
  - `docs/audit/active-playtest/active_playtest_report.md`
  - `AUTONOMOUS_PROGRESS.md`

## Strong evidence

- `docs/audit/football-feel/` clearly shows real renders for main menu, new career, club selection, dashboard, squad, player profile, tactics, fixtures, matchday, live match, post-match, and save/load.
- The active-playtest headless assertions prove role-based behavior differences across Assistant Manager, Head Coach, and Manager for training, tactics, scouting, recruitment/contracts, post-match consequences, save/load, promises, live-match consistency, and job-market state.
- Save/load persistence is supported by logs for all three roles and by a user-facing load screen screenshot in `docs/audit/football-feel/save-load.png`.
- Live match playback is visually evidenced in `docs/audit/football-feel/live-match-01.png` through `live-match-05.png`, and the shared-engine contract is supported by the headless assertions.
- Main navigation, career creation, club selection, and matchday all render without an obvious crash in the visual evidence.
- The audit trail is honest enough to show a failed earlier harness run (`Invalid project path`) followed by a later passing run.

## Weak evidence

- The 42 active-playtest screenshots are not trustworthy for screen-by-screen UX judgment; they produce only 4 unique images and many labels do not match the displayed screen.
- The strongest visual evidence is manager-centric. It does not visibly prove how Assistant Manager and Head Coach wording differ from Manager wording on actual screens.
- Training and scouting UI visibility is not well evidenced visually. The logs prove state changes, but there is no dedicated training/scouting screenshot set to judge human readability.
- Recruitment/contracts/job-market honesty is supported by logs, not by direct UI screenshots.
- Partial player information is not convincingly shown in the sampled player screens. The visible player-profile evidence shows exact values, not estimated bands or unknown markers.
- No videos exist, so motion quality, transition pacing, and live readability under interaction cannot be judged.

## UI/UX issues found from screenshots/videos

| Issue | Screen/artifact | Evidence path | Severity | Why it matters | Recommended fix |
|---|---|---|---|---|---|
| Sidebar active state appears stuck on `Matchday` even when other screens are shown | Dashboard, Squad, Tactics, Fixtures | `docs/audit/football-feel/dashboard.png`, `docs/audit/football-feel/squad.png`, `docs/audit/football-feel/tactics.png`, `docs/audit/football-feel/fixtures.png` | P1 misleading | Users can lose route context if the highlighted section does not match the page they are on | Bind sidebar highlight to the active scene/screen instead of leaving `Matchday` highlighted |
| Post-match screen looks incomplete and clipped | Post-match report | `docs/audit/football-feel/post-match.png` | P1 misleading | Post-match is a core consequence screen; the current capture shows a huge empty upper area and truncated pressure content, so the summary reads unfinished | Rebuild the post-match layout so key summary, reaction, and next-step panels fill the screen and text is not cut off |
| Main-menu and save/load summary cards wrap fixture data into hard-to-scan vertical stacks | Main menu, Save/load | `docs/audit/football-feel/main-menu.png`, `docs/audit/football-feel/save-load.png` | P2 confusing | The slot summary is readable, but the `Next Fixture` column breaks names and dates awkwardly, slowing scan speed | Reflow slot metadata into wider columns or stacked rows with clearer labels |
| Dashboard is playable but text-heavy and repetitive | Dashboard | `docs/audit/football-feel/dashboard.png` | P2 confusing | Important actions exist, but long prose in `Next Decision` and `Club Notes` dilutes the main call to action | Compress repeated context into shorter bullets or a tighter summary block |
| Partial player information is not visually obvious | Squad, Player profile | `docs/audit/football-feel/squad.png`, `docs/audit/football-feel/player-profile.png` | P2 confusing | Partial knowledge is supposed to be a core fantasy; these captures do not visibly show estimates, uncertainty, or scouting confidence | Surface estimated ranges, unknown markers, and scouting-confidence language in visible profile blocks |
| Role-labeled active-playtest screenshots do not match the shown role/screen | Active-playtest screenshot set | `docs/audit/active-playtest/screenshots/`, `docs/audit/active-playtest/logs/active-playtest-run-20260527-044322.json` | P1 misleading | The audit pipeline claims role-specific visual proof that the files do not actually provide | Add scene-name and anchor-text assertions before each screenshot, then recapture |

## Screenshot-by-screenshot notes

| Screenshot | What it shows | Good | Problem | Follow-up |
|---|---|---|---|---|
| `main-menu.png`, `save-load.png` | Resume/load entry points and slot summary | Buttons are obvious and contrast is good | Slot metadata is cramped; fixture text wraps vertically and column pairing is weak | Rework slot-summary layout and retest at this width |
| `new-career.png` | Career identity setup | Clear hierarchy, readable form fields, simple primary action | No visible role selection in the captured screen, so role authority setup is still not visually proven | Add captures for role/background/license selection states |
| `club-selection.png` | Club-choice briefing | Club list and right-side club snapshot are well grouped and readable | Still manager-centric; no visible role framing or authority warning | Add role-aware club-entry copy or screenshot variants for all three roles |
| `dashboard.png` | Club hub overview | Strong card grouping, readable contrast, clear left-nav actions | `Matchday` looks highlighted while viewing the dashboard; notes are prose-heavy | Fix active-nav state and trim repeated copy |
| `squad.png`, `player-profile.png` | Squad list and selected-player view | Clear selected-player panel, easy to read starter/readiness labels | Partial-information system is not obvious; visible sample leans on exact values | Add estimated/unknown data states to sampled captures |
| `tactics.png` | Tactical shape and numeric inputs | Pitch board and numeric controls are understandable | Sidebar highlight again suggests the wrong active route; role authority wording is not shown | Fix route state and add role-aware tactics copy samples |
| `fixtures.png` | Fixture desk and schedule framing | Core structure is understandable: next opponent, season, club fixtures, other fixtures | Sidebar highlight mismatch persists; some list content is cropped or only partly visible in the capture | Retake full-height fixtures view and verify active route highlight |
| `matchday.png` | Pre-match decision screen | Strongest screen in the set: clear opponent context, readiness, and two obvious actions | Heavy paragraph blocks in kickoff context and match plan could be tighter | Trim copy and keep the primary choice dominant |
| `live-match-01.png` to `live-match-05.png` | 2D live playback and event log | Score, clock, possession, and recent events are readable; the match is legible as a tactical playback | Still images cannot prove pacing, animation smoothness, or whether updates are too abrupt during play | Add a short video or timed capture sequence next pass |
| `post-match.png` | Post-match summary | Confirms a post-match surface exists | It looks visually incomplete and is the weakest core UX screen in the capture set | Fix the layout before claiming post-match quality is coherent |
| `docs/audit/active-playtest/screenshots/*` | Role-labeled audit capture set | Confirms the harness wrote files | Screen labels are unreliable because 42 files reduce to 4 unique frames | Treat this set as a capture-pipeline issue, not UX proof |

## Role authority assessment

1. Does Assistant Manager look like recommendation/influence only?
Not from screenshots. The logs strongly support this, but the visual evidence does not show a trustworthy Assistant Manager UI sample.

2. Does Head Coach look football-side focused?
Again, mostly from logs, not from screenshots. The visual set does not give a clean Head Coach-specific screen sequence.

3. Does Manager look broad but still limited by club structure?
Partially. The manager-centric visuals show broad football control screens, and the logs show board/Director constraints on recruitment, but the screenshots do not directly display those constraints.

4. Any wording that blurs those roles?
Yes. The current visual evidence blurs them because the reliable screenshots are mostly manager-only, while the active-playtest role-labeled screenshots do not reliably show the claimed role contexts.

## UI density and readability assessment

1. Is the dashboard too dense?
Moderately dense, not unusable. The issue is not grid overload; it is long prose blocks competing with the primary action.

2. Are cards/sections visually grouped well?
Yes. The general card structure and contrast hierarchy are coherent across dashboard, club selection, matchday, and live match.

3. Is text readable?
Mostly yes. Contrast and font size are generally fine. The recurring readability problem is wrapping and overly long paragraphs, not raw legibility.

4. Are important actions easy to identify?
Yes on main menu, club selection, save/load, and matchday. Less so on the dashboard, where the action path is present but diluted by repeated text.

5. Are any screens visually boring, cramped, or unclear?
Yes. The post-match screen is unclear, and the main-menu/save-load slot summary is cramped. The rest are functional but somewhat sober and text-heavy rather than visually rich.

## Active playtest quality assessment

1. Did the active playtest actually test gameplay actions?
Yes, at the logic/state level. The headless assertions show real behavior checks, not just route opening.

2. Did it mostly test navigation?
The GUI screenshot portion mostly tested navigation and capture, and it did that unreliably. The headless portion tested gameplay state much more convincingly than the screenshot portion tested UX.

3. Which parts need deeper UI action assertions?
Sidebar active-state correctness, role-specific wording, partial-information display, training/scouting panels, recruitment/contracts outcome messaging, and post-match render completeness.

4. Which screenshots should be retaken or expanded?
Retake the entire `docs/audit/active-playtest/screenshots/` set with scene assertions. Add explicit role-specific dashboard/tactics captures, training/scouting captures, recruitment/contracts captures, job-market captures, and a corrected post-match full-screen capture.

## Recommended next Cursor task

Fix UI wording/layout issues first.

## Top 10 next fixes or checks

| Rank | Fix/check | Why | Evidence | Priority |
|---:|---|---|---|---|
| 1 | Fix post-match layout completeness | It is the clearest user-facing UX failure in the visual evidence | `docs/audit/football-feel/post-match.png` | P1 |
| 2 | Fix sidebar active-route highlighting | The current highlight appears misleading across multiple screens | `dashboard.png`, `squad.png`, `tactics.png`, `fixtures.png` | P1 |
| 3 | Reflow main-menu/save-load slot metadata | Fixture/date text currently stacks awkwardly and slows scan speed | `main-menu.png`, `save-load.png` | P2 |
| 4 | Shorten dashboard prose and emphasize next action | The dashboard works, but repeated text buries the action path | `dashboard.png` | P2 |
| 5 | Surface partial-information cues in visible player UI | Core product fantasy is not obvious in the sampled player screens | `squad.png`, `player-profile.png` | P2 |
| 6 | Add role-specific screenshot coverage for all three roles | Current visual evidence does not prove wording differences | Active-playtest screenshots + football-feel manager-only set | P2 |
| 7 | Add dedicated training/scouting UI captures | Current judgment here relies on logs, not visuals | No matching visual artifacts in current sets | P2 |
| 8 | Add recruitment/contracts/job-market UI captures | Current honesty judgment relies on logs only | No matching visual artifacts in current sets | P2 |
| 9 | Repair the active-playtest capture pipeline | The current harness produces mislabeled duplicate screenshots | 42 screenshots, 4 unique hashes | P1 |
| 10 | Add a short video for live-match to post-match flow | Still images cannot judge pacing and transitions | No video files present | P3 |

## Final note to ChatGPT

Ask ChatGPT to help turn this evidence into a narrow UI polish plan, not a feature brainstorm. The highest-value targets are the incomplete post-match layout, the misleading sidebar active state, the cramped main-menu/save-load slot summary, and the missing visual expression of partial player information and role authority differences.
