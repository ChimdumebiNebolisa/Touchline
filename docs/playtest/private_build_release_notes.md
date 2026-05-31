# Touchline Private Build — Release Notes

**Build label:** Private human playtest  
**Commit:** `79ae9ef` — `test: verify ui ux with windows active capture`  
**Date:** 2026-05-30  
**Platform:** Windows desktop (Godot 4.6.2 Mono)

---

## Build status

**Green — ready for private human playtest** on Windows.

Automated verification passed before this package was assembled. Human testers are now the primary source of UX feedback.

---

## What is playable

End-to-end career loop:

1. Main menu → new career or continue
2. Career setup (role, background, license, seed)
3. Club selection with identity/pressure preview
4. Club dashboard and sidebar navigation
5. Squad and player profiles with partial information
6. Tactics (role-appropriate save or recommend)
7. Training/scouting dashboard actions
8. Recruitment/contracts and job-market dashboard sections
9. Fixtures and standings
10. Matchday — instant result or live tactical playback
11. Post-match report and consequences
12. Save/load continuity
13. Multi-matchday progression and season rollover (seeded short league)

Three playable roles with different authority:

- Assistant Manager
- Head Coach
- Manager

---

## What changed recently

Since the Yellow UI audit:

- Sidebar active route fixed (no stale Matchday highlight)
- Post-match layout rebuilt (dense summary, scroll-safe consequences)
- Main menu and save/load slot cards reflowed (labeled rows, no bad wrap)
- Dashboard copy tightened (shorter, action-oriented)
- Player partial information surfaced (Profile Confidence, Known/Estimated/Unknown)
- Screenshot capture pipeline made trustworthy (scene/nav/anchor gates)
- Role-specific visual evidence for AM/HC/Manager
- **Windows native capture pass** — 35 verified screenshots, 29 unique hashes, 0 mislabels
- Club-selection screenshot added to evidence set
- Live-to-post-match timed screenshot sequence documented

No new major gameplay systems were added in this pass.

---

## What testers should focus on

Priority feedback areas:

1. **First 15 minutes** — career setup and first dashboard impression
2. **Role authority** — do buttons and outcomes feel different per role?
3. **Partial information** — is uncertainty clear and fair on squad/profile?
4. **Match loop** — instant sim, live playback, post-match narrative
5. **Save/load trust** — does Continue Career feel safe?
6. **Honest foundations** — do training/scouting/recruitment sections signal their depth accurately?

Use [playtest_script.md](playtest_script.md) and [feedback_form.md](feedback_form.md).

---

## Known limitations

See [known_limitations.md](known_limitations.md). Highlights:

- Foundation-depth transfers/contracts, job market, training, scouting
- No youth academy, finance ledger, media dialogue trees, or job interviews
- Dashboard-section depth for several management systems
- No committed export preset; run from Godot source for this playtest

---

## Verification summary

| Gate | Result |
|---|---|
| `dotnet build game/Touchline.sln` | Pass |
| Godot headless suite (steps 22–57) | Pass |
| `ACTIVE_PLAYTEST_USER_FLOW_PASS` | Pass |
| Windows GUI capture | 35 screenshots, 29 unique hashes, 0 cross-screen mislabels |
| Audit UI checks (sidebar, post-match, partial info) | Pass |

Evidence reports:

- [for_chatgpt_summary_windows_final.md](../audit/active-playtest/for_chatgpt_summary_windows_final.md)
- [windows_capture_report.md](../audit/active-playtest/windows_capture_report.md)

---

## Suggested test duration

| Session type | Duration |
|---|---|
| Minimum | 45–60 minutes |
| Recommended | 90–120 minutes |
| Thorough (two roles + rollover) | 3+ hours |

---

## How to run

From repository root:

```powershell
dotnet build game/Touchline.sln
Godot_v4.6.2-stable_mono_win64.exe --path game
```

Full instructions: [private_playtest_readme.md](private_playtest_readme.md)

---

## Optional: export a Windows executable

**Status:** No `export_presets.cfg` is committed to this repository. Testers should run from Godot source.

If a developer needs a standalone `.exe` for a non-technical tester:

1. Install Godot 4.6.x **Mono** and .NET 8 SDK.
2. Open `game/project.godot` in Godot.
3. Confirm main scene is `res://scenes/MainMenu.tscn`.
4. Run `Project > Export`.
5. Add a **Windows Desktop** preset if none exists.
6. Install export templates when prompted.
7. Export to a folder **outside** the repo (e.g. `C:\TouchlineDemo\Touchline.exe`).
8. Smoke-test: new career → club → dashboard → matchday → post-match → save/load.

Do not commit machine-specific export paths. See also [docs/Release.md](../Release.md).

---

## Reporting issues

- Feedback: [feedback_form.md](feedback_form.md)
- Bugs: [bug_report_template.md](bug_report_template.md)

---

## Package contents

| File | Description |
|---|---|
| [private_playtest_readme.md](private_playtest_readme.md) | Start here |
| [playtest_script.md](playtest_script.md) | Session guide |
| [feedback_form.md](feedback_form.md) | Structured feedback |
| [known_limitations.md](known_limitations.md) | Honest scope limits |
| [bug_report_template.md](bug_report_template.md) | Bug reporting |
| [private_build_release_notes.md](private_build_release_notes.md) | This file |
