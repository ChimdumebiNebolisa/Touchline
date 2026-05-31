# Touchline Private Playtest — Read Me First

## What Touchline is

Touchline is a **local-first, single-player football management game** built with Godot 4.6 and C#. You start a fictional career as an **Assistant Manager**, **Head Coach**, or **Manager**, choose a club, and run the week: read the dashboard, manage squad and tactics, advance time, play matches (instant sim or live tactical playback), read post-match consequences, and save your progress locally.

The game focuses on **role authority**, **partial player information**, **tactical identity**, and **football pressure** — not arcade on-pitch controls.

## What this private test is for

This build is for **human UX feedback** before wider sharing. We want to know:

- Can you understand what to do next?
- Does your role feel different from other roles?
- Is partial player information honest and readable?
- Do matches, post-match, and save/load feel trustworthy?
- Where do you get confused, bored, or lost?

Use the companion files in this folder:

| File | Purpose |
|---|---|
| [playtest_script.md](playtest_script.md) | Step-by-step session guide |
| [feedback_form.md](feedback_form.md) | Structured feedback to fill in |
| [known_limitations.md](known_limitations.md) | What the game does **not** include yet |
| [bug_report_template.md](bug_report_template.md) | How to report bugs clearly |
| [private_build_release_notes.md](private_build_release_notes.md) | Build status and recent changes |

## What this test is **not** for

- Judging final art, audio, or licensed real-world content (everything is fictional).
- Expecting a full transfer market, youth academy, finance ledger, or media dialogue trees.
- Testing multiplayer, online services, or mobile builds.
- Balancing a full multi-season competitive meta — the seeded demo league is small by design.

## How to run the game locally

**Requirements**

- Windows 10/11 (recommended; this build was verified on Windows)
- [Godot 4.6.x Mono/.NET](https://godotengine.org/download) installed
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

**Steps**

1. Clone or unzip the Touchline repository.
2. Open PowerShell at the repository root (`C:\Users\Chimdumebi\Touchline` or your path).
3. Build once:

   ```powershell
   dotnet build game/Touchline.sln
   ```

4. Launch the game (adjust the Godot path if yours differs):

   ```powershell
   Godot_v4.6.2-stable_mono_win64.exe --path game
   ```

   If only the console binary is available:

   ```powershell
   Godot_v4.6.2-stable_mono_win64_console.exe --path game
   ```

5. Alternatively, open `game/project.godot` in the Godot editor and press **Run** (F5).

**First launch tip:** Godot may compile C# on first run. Wait for the main menu to appear.

### Optional: Windows executable export

This repository does **not** ship a committed export preset (`export_presets.cfg`). There is no pre-built `.exe` in the repo. If you need a standalone executable, a developer can export manually from the Godot editor — see [private_build_release_notes.md](private_build_release_notes.md) for editor export steps. For this playtest, **running from source via Godot is the supported path**.

## Recommended screen size

- **1280×720** minimum (verified during automated capture)
- **1920×1080** or larger is fine; UI uses scalable containers
- Use **windowed or maximized** mode; ultra-narrow windows may clip dashboard text

## Expected playtest length

- **Minimum useful session:** 45–60 minutes (one role, 2–3 matchdays)
- **Recommended session:** 90–120 minutes (try two roles or finish a short season arc)
- **Stretch goal:** Complete one seeded season and test save/load after rollover

## What feedback matters most

1. **Clarity** — Do you always know where you are and what to do next?
2. **Role feel** — Does Assistant Manager feel different from Manager in authority and buttons?
3. **Partial information** — Do player profiles honestly show Known / Estimated / Unknown?
4. **Match loop** — Instant sim vs live playback vs post-match: does the story make sense?
5. **Trust** — After save/load, does the career state feel preserved?
6. **Honesty** — Do training, scouting, recruitment, and job-market sections feel like foundations rather than finished systems?

Fill in [feedback_form.md](feedback_form.md) when done.

## Where to report bugs

Use [bug_report_template.md](bug_report_template.md). Send completed reports to the person who invited you to this playtest (email/Discord/issue tracker — they will tell you where).

Attach when possible:

- Screenshot of the problem
- Steps to reproduce
- Your role, club, and screen resolution
- Save file if the bug involves persistence (`%APPDATA%\Godot\app_userdata\Touchline\` or Godot `user://` slot data)

## Current verification status

This private build passed **Green** on Windows evidence (commit `79ae9ef`):

| Check | Result |
|---|---|
| Verified screenshots | 35 |
| Unique screenshot hashes | 29 |
| Cross-screen mislabels | 0 |
| `dotnet build game/Touchline.sln` | Pass |
| Full Godot headless suite (steps 22–57) | Pass |
| `ACTIVE_PLAYTEST_USER_FLOW_PASS` | Pass |
| Sidebar, post-match, slot cards, dashboard copy, partial-info UI | Verified on Windows PNGs |

Details: [docs/audit/active-playtest/for_chatgpt_summary_windows_final.md](../audit/active-playtest/for_chatgpt_summary_windows_final.md)

Automated checks prove logic and UI wiring; **your human feedback** is what this playtest adds.
