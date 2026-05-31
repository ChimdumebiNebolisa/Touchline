# Touchline — Known Limitations (Private Build)

This list is intentionally honest. Touchline is a **coherent playable slice** of a larger design, not a finished Football Manager competitor.

If something below sounds missing, it may be **planned but not implemented yet** rather than a bug. Still report it if the UI implies otherwise.

---

## Scope and platform

- **Single-player, local-first only** — no online accounts, cloud saves, or multiplayer.
- **Desktop only** — Windows verified; other platforms not supported for this playtest.
- **Small seeded demo league** — four fictional clubs in a short season loop, not a full multi-division world.
- **No real teams or players** — all clubs, names, and leagues are fictional.
- **No standalone `.exe` in repo** — run from Godot source unless a developer exports manually.

---

## Match and gameplay

- **Live match is tactical playback**, not playable on-pitch football controls.
- **Instant sim and live match share one engine** — they should tell the same story, but pacing differs.
- **No continuous video capture** in audit evidence — motion proof uses a timed screenshot sequence only.
- **Short season** — season rollover exists, but long multi-season balance is not tuned for human meta.

---

## Management systems (foundation depth)

These areas exist as **dashboard sections or foundations**, not full dedicated screens:

| Area | Current depth |
|---|---|
| **Transfers / contracts** | Foundation — recruitment/contracts buttons on dashboard; no full transfer market UI |
| **Youth academy** | Not implemented as a playable system |
| **Job market / career moves** | Foundation — dashboard job-market section and career state; no full interview flow |
| **Training** | Simple — weekly focus and familiarity; not a deep session planner |
| **Scouting** | Simple — assignments and reports at standard depth; not a full scouting network |
| **Finance** | No full finance ledger, wage negotiation, or budget simulation UI |
| **Media** | News and pressure events exist; **no full media dialogue trees** |
| **Job interviews** | **No full job interview system** |
| **Injuries / medical** | Limited or abstracted in player condition, not a full medical department |
| **Promotions / relegation** | Not part of the current demo league scope |
| **Cups / multi-competition calendar** | Not in current demo scope |

---

## UI and UX

- **Training, scouting, recruitment, and job market** are accessed from **dashboard insight cards**, not standalone full screens.
- **Player profile FORM card** may feel text-dense on smaller windows.
- **Some screens are information-rich** — dashboard and post-match pack a lot of text by design.
- **Export preset not committed** — developers must configure Godot export locally if a binary is needed.

---

## What *is* in this build

For clarity, this private build **does** include:

- New career → role/background/license/club selection
- Dashboard with pressure, morale, and next-action context
- Squad and player profile with partial information (Known / Estimated / Unknown)
- Tactics board with role-appropriate save/recommend flows
- Fixtures, standings, matchday
- Instant result and live tactical playback
- Post-match report with stats, causes, and consequences
- Save/load and continue career from main menu
- Season progression and rollover in the seeded loop
- Role-specific authority differences (Assistant Manager / Head Coach / Manager)

---

## How to interpret bugs vs limitations

| Report when… | Example |
|---|---|
| **Bug** | Sidebar highlights wrong screen; save loses tactics; crash on matchday |
| **Limitation** | No youth academy screen; no transfer bid UI; no press conference dialogue |
| **UX honesty issue** | Button promises a full transfer deal but only logs a foundation message |

When in doubt, file a bug report and mark severity **P1** if the game **misled** you about what was available.

---

## Related docs

- Product scope: [docs/PRD.md](../PRD.md)
- QA known limits: [docs/QA.md](../QA.md) (Manual Demo Checklist section)
- Windows verification: [docs/audit/active-playtest/for_chatgpt_summary_windows_final.md](../audit/active-playtest/for_chatgpt_summary_windows_final.md)
