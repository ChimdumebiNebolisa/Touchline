# Touchline Screenshot Capture Report

- Run started: `2026-05-30T22:05:51.603798+00:00`
- Headless active playtest: `PASS`
- Audit baseline screenshot count: `42`
- Audit baseline unique hash count: `4`
- Archived pre-recapture local screenshot count: `29`
- Archived pre-recapture local unique hash count: `25`
- New screenshot count: `34`
- New unique hash count: `28`
- Archived prior screenshots: `docs\audit\active-playtest\archive\screenshots-20260530-170551`
- Video capture: skipped. No local ffmpeg binary is available, so the harness captured a verified timed screenshot sequence for Live Match to Post-Match instead of a video.
- Duplicate-hash policy: no duplicate hash is allowed across different expected screens. This run reported only same-screen duplicates, so capture validation stayed `PASS`.

## Duplicate Hash Review
- Hash `b251379942b7` repeated `3` times across `tactics, tactics, tactics`
- Hash `dee99d6bcaa6` repeated `2` times across `main-menu-slot-card, post-match-main-menu`
- Hash `78f1ed0f309a` repeated `2` times across `save-load-slot-card, post-match-save-load`
- Hash `864f067a9e48` repeated `2` times across `recruitment-contracts, dashboard-job-market`
- Hash `5bf2d4c05484` repeated `2` times across `squad, squad-return`

## Capture Results

| Role | Label | Expected screen | Actual screen | Nav | Anchor | Pass | File |
|---|---|---|---|---|---|---|---|
| assistant-manager | main-menu-slot-card | MainMenu | MainMenu | -- | Assistant Manager | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-main-menu-slot-card.png` |
| assistant-manager | save-load-slot-card | SaveLoadScene | SaveLoadScene | -- | Assistant Manager | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-save-load-slot-card.png` |
| assistant-manager | dashboard | ClubDashboard | ClubDashboard | Dashboard | Assistant Manager | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-dashboard.png` |
| assistant-manager | training-scouting | ClubDashboard | ClubDashboard | Dashboard | Recommend Scouting Priority | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-training-scouting.png` |
| assistant-manager | recruitment-contracts | ClubDashboard | ClubDashboard | Dashboard | Recommend Contract Terms | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-recruitment-contracts.png` |
| assistant-manager | tactics | TacticsScreen | TacticsScreen | Tactics | Submit Tactical Recommendation | PASS | `docs\audit\active-playtest\screenshots\assistant-manager-tactics.png` |
| head-coach | main-menu-slot-card | MainMenu | MainMenu | -- | Head Coach | PASS | `docs\audit\active-playtest\screenshots\head-coach-main-menu-slot-card.png` |
| head-coach | save-load-slot-card | SaveLoadScene | SaveLoadScene | -- | Head Coach | PASS | `docs\audit\active-playtest\screenshots\head-coach-save-load-slot-card.png` |
| head-coach | dashboard | ClubDashboard | ClubDashboard | Dashboard | Head Coach | PASS | `docs\audit\active-playtest\screenshots\head-coach-dashboard.png` |
| head-coach | training-scouting | ClubDashboard | ClubDashboard | Dashboard | Request Scouting Priority | PASS | `docs\audit\active-playtest\screenshots\head-coach-training-scouting.png` |
| head-coach | recruitment-contracts | ClubDashboard | ClubDashboard | Dashboard | Request Contract Review | PASS | `docs\audit\active-playtest\screenshots\head-coach-recruitment-contracts.png` |
| head-coach | tactics | TacticsScreen | TacticsScreen | Tactics | Save Tactical Plan | PASS | `docs\audit\active-playtest\screenshots\head-coach-tactics.png` |
| manager | main-menu-slot-card | MainMenu | MainMenu | -- | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-main-menu-slot-card.png` |
| manager | save-load-slot-card | SaveLoadScene | SaveLoadScene | -- | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-save-load-slot-card.png` |
| manager | dashboard | ClubDashboard | ClubDashboard | Dashboard | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-dashboard.png` |
| manager | training-scouting | ClubDashboard | ClubDashboard | Dashboard | Start Scouting Assignment | PASS | `docs\audit\active-playtest\screenshots\manager-training-scouting.png` |
| manager | recruitment-contracts | ClubDashboard | ClubDashboard | Dashboard | Review Contract Terms | PASS | `docs\audit\active-playtest\screenshots\manager-recruitment-contracts.png` |
| manager | tactics | TacticsScreen | TacticsScreen | Tactics | Save Tactical Plan | PASS | `docs\audit\active-playtest\screenshots\manager-tactics.png` |
| manager | dashboard-job-market | ClubDashboard | ClubDashboard | Dashboard | Career/job market | PASS | `docs\audit\active-playtest\screenshots\manager-job-market.png` |
| manager | career-job-market | ClubDashboard | ClubDashboard | Dashboard | Career/job market | PASS | `docs\audit\active-playtest\screenshots\manager-career-job-market.png` |
| manager | squad | SquadScreen | SquadScreen | Squad | Profile Confidence: | PASS | `docs\audit\active-playtest\screenshots\manager-squad.png` |
| manager | player-profile | PlayerProfile | PlayerProfile | -- | Profile Confidence: | PASS | `docs\audit\active-playtest\screenshots\manager-player-profile.png` |
| manager | squad-return | SquadScreen | SquadScreen | Squad | Profile Confidence: | PASS | `docs\audit\active-playtest\screenshots\manager-squad-return.png` |
| manager | fixtures | FixturesScreen | FixturesScreen | Fixtures | Fixture List | PASS | `docs\audit\active-playtest\screenshots\manager-fixtures.png` |
| manager | standings | StandingsScreen | StandingsScreen | Standings | League Table | PASS | `docs\audit\active-playtest\screenshots\manager-standings.png` |
| manager | matchday | MatchdayScene | MatchdayScene | -- | Watch Live Match | PASS | `docs\audit\active-playtest\screenshots\manager-matchday.png` |
| manager | live-match-kickoff | LiveMatchScene | LiveMatchScene | -- | 01' | PASS | `docs\audit\active-playtest\screenshots\manager-live-match-kickoff.png` |
| manager | live-match-mid | LiveMatchScene | LiveMatchScene | -- | Riverton Athletic vs Northbridge City | PASS | `docs\audit\active-playtest\screenshots\manager-live-match-mid.png` |
| manager | live-match-full-time | LiveMatchScene | LiveMatchScene | -- | FT | PASS | `docs\audit\active-playtest\screenshots\manager-live-match-full-time.png` |
| manager | post-match-main-menu | MainMenu | MainMenu | -- | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-post-match-main-menu.png` |
| manager | post-match-save-load | SaveLoadScene | SaveLoadScene | -- | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-post-match-save-load.png` |
| manager | post-match-dashboard | ClubDashboard | ClubDashboard | Dashboard | Manager | PASS | `docs\audit\active-playtest\screenshots\manager-post-match-dashboard.png` |
| manager | post-match-matchday | MatchdayScene | MatchdayScene | -- | Watch Live Match | PASS | `docs\audit\active-playtest\screenshots\manager-post-match-matchday.png` |
| manager | post-match | PostMatchScene | PostMatchScene | -- | Key stats | PASS | `docs\audit\active-playtest\screenshots\manager-post-match.png` |
