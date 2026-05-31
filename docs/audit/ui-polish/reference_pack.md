# Touchline UI Reference Pack

Research pack for Cursor Cloud Agents polishing Touchline’s Godot 4.6 Mono UI. External repos and docs inform **layout, hierarchy, feedback, and capture discipline** only. Do not copy third-party game code into Touchline.

## Sources reviewed

| Source | What it is | Why it matters for Touchline | Useful rules extracted |
|---|---|---|---|
| [Godot UI — Using Containers](https://docs.godotengine.org/en/stable/tutorials/ui/gui_containers.html) | Official guide to `VBoxContainer`, `HBoxContainer`, `MarginContainer`, `ScrollContainer`, sizing flags | Touchline’s club shell is card + rail + scroll stacks; post-match and dashboard density depend on containers, not manual pixel placement | Prefer nested containers over free-positioned controls; use `size_flags_horizontal = expand_fill` for primary text; put long consequence copy in `ScrollContainer` |
| [Godot UI — Size and anchors](https://docs.godotengine.org/en/stable/tutorials/ui/size_and_anchors.html) | Anchor presets and minimum sizes | Main menu slot cards and post-match grids must survive 1280×720 and 1920×1200 without clipping | Use full-rect anchors on shell roots; set sensible `custom_minimum_size` on value columns; avoid ultra-narrow `HBox` columns for fixture strings |
| [Godot UI — Introduction to GUI skinning / Theme](https://docs.godotengine.org/en/stable/tutorials/ui/gui_skinning.html) | `Theme`, styleboxes, font overrides | Touchline centralizes look in `TouchlineTheme.cs` | Keep visual state in theme helpers (`ApplyButtonVariant`, `ApplyRailNavigation`); do not hard-code per-screen colors for nav selection |
| [Godot UI — Control node reference](https://docs.godotengine.org/en/stable/classes/class_control.html) | Base UI node behavior, focus, minimum size | Rail buttons, chips, and profile blocks are all `Control` derivatives | Disabled selected nav buttons must still **look** selected via stylebox, not accidental “active” siblings |
| [Godot — Command line tutorial](https://docs.godotengine.org/en/stable/tutorials/editor/command_line_tutorial.html) | `--headless`, `--path`, `-s` scripts, `--write-movie` | All Touchline verification runs headless GDScript checks from repo root | Use `--headless --path game -s res://scripts/...`; never treat editor-only paths as CI truth |
| [Godot demo projects](https://github.com/godotengine/godot-demo-projects) | Official sample games (2D, GUI, viewport) | Shows idiomatic scene trees: shallow roots, labeled sections, scroll for overflow | GUI demos favor **one primary column + sidebar**; duplicate that pattern for dashboard / post-match, not empty spacer panels |
| [Awesome Gamedev (Calinou)](https://github.com/Calinou/awesome-gamedev) | Curated engines, tools, UX articles | Points to UX writing and tooling for playtesting | Treat screenshot/video capture as part of QA tooling, not an afterthought |
| [Game Programming Patterns](https://gameprogrammingpatterns.com/) (munificent) | Architecture patterns (State, Observer, Facade) | Touchline separates `GameState` / domain from scenes | UI scenes **render** state; they do not own match, save, or scouting rules—Facade/Observer boundaries keep nav and copy honest |
| Nielsen Norman — visibility of system status | UX heuristic | Sidebar highlight, save slot preview, post-match “next step” | User must see **where they are** (route), **what changed** (post-match), and **what to do next** (dashboard CTA) |
| Nielsen Norman — recognition over recall | UX heuristic | Slot cards, partial player info | Label rows (`Manager`, `Next fixture`, `Save`) so users scan columns, not decode wrapped prose |
| Touchline Yellow audit (`for_chatgpt_summary.md`, `evidence_review_table.md`) | Internal evidence | Defines P1/P2 issues this pass must address | Fix capture pipeline before trusting role-labeled PNGs |

## Godot UI rules for Touchline

### 1. Sidebar active-state highlighting

- Call `TouchlineTheme.ApplyRailNavigation(..., activeRoute)` on every rail screen with the **scene’s** route (`Dashboard`, `Squad`, `Tactics`, `Fixtures`, `Standings`).
- On non-matchday scenes, `Matchday` is a **CTA** (`Go to Matchday`), not a second selected item.
- Write audit state with `AuditUiStateWriter.Write(scene, role, activeRoute, anchors...)` so captures assert `SelectedNav`.
- Headless gate: `res://scripts/audit_sidebar_active_route_check.gd`.

### 2. Post-match layout

- Header row: fixture, result, score, key stats chips—no large empty top band.
- Middle: scrollable consequences (`ScrollContainer` + `VBox`) for deltas, table impact, tactical note, pressure.
- Right or lower column: key events; footer: explicit **Continue** + next-step sentence.
- Long strings use autowrap on labels inside scroll, not clipped single-line labels.

### 3. Dashboard information hierarchy

- Order: **next best action** → competition/table/morale chips → short bullets → role authority line.
- Cap “club notes” / “next decision” to bullets; avoid multi-sentence paragraphs in primary cards.
- Training/scouting and recruitment/contracts sections use role-specific button labels (recommend vs request vs direct action).

### 4. Save/load slot cards

- Use labeled rows (`Manager`, `Role`, `Season`, `Next fixture`, `Save`) in a `VBox`, not a cramped multi-column grid.
- Fixture line: `date · matchday · home vs away` on one row with `autowrap` and generous minimum width.
- Empty slot: explicit placeholder values (`—`) so alignment stays stable.

### 5. Player profile partial-information display

- Always show `Profile Confidence`, `Known`, `Estimated`, `Unknown` blocks when `PlayerInformationReport` supplies them.
- Use `?` or “estimated” language in attribute lines for low visibility; never show exact values when knowledge tier forbids it.
- Squad detail panel repeats confidence one line above readiness summary.

### 6. Training/scouting panels

- Dashboard insight card hosts training + scouting actions; button text must reflect role authority.
- Anchors for capture: `Recommend Scouting Priority`, `Request Scouting Priority`, `Start Scouting Assignment`.
- No new screen required until product docs expand scope—screenshot proof comes from dashboard section state after button press.

### 7. Recruitment/contracts panels

- Same dashboard card pattern; anchors: `Recommend Contract Terms`, `Request Contract Review`, `Review Contract Terms`.
- Job market: separate capture with anchor `Career/job market` or `Job market` after opening the section.

### 8. Screenshot capture reliability

- **Never** save PNGs on sleep alone; require `current-ui-state.json` scene + nav + anchor match.
- Godot `AuditCommandBridge`: defer capture until scene matches `ExpectedScene` and **wait several frames** after layout (`GetViewport().GetTexture().GetImage()`).
- Python harness: issue `capture_screenshot` only after `wait_for_ui_state` passes; archive prior PNGs before rerun.
- Reject duplicate hashes across **different** expected screens; allow duplicates only for intentional same-state recaptures.
- Prefer Godot viewport PNG over OS desktop grab when possible (avoids wrong window / stale frame).

## Patterns to apply

| Problem | Reference lesson | Touchline implementation rule |
|---|---|---|
| Sidebar stuck on Matchday | Visibility of system status (NN/g) + single source of truth for selection | `ApplyRailNavigation` with per-scene `TouchlineRailRoute`; audit writes `SelectedNav` |
| Post-match empty / clipped | Containers + ScrollContainer (Godot docs) | Rebuild post-match scene tree with summary grid + scroll stacks; audit layout script |
| Slot metadata wraps badly | Recognition over recall; MarginContainer spacing | Labeled rows in `MainMenu` / `SaveLoadScene`; wide value labels with autowrap |
| Dashboard prose-heavy | Information hierarchy (dashboard UX) | Short bullets in `ClubDashboard.Render*`; lead with CTA labels |
| Partial info invisible | Honest feedback / explainable outcomes (Touchline guardrails) | `PlayerProfile` + `SquadScreen` surface confidence tiers |
| 42 PNGs → 4 hashes | Testability + Observer on UI state | `AuditUiStateWriter` + verified capture steps + hash report |
| Role wording not evidenced | Facade per role (patterns) | Role-specific button strings; per-role screenshot filenames |
| Training/scouting only in logs | Proof-driven QA (awesome-gamedev tooling mindset) | Capture dashboard section after applying training/scouting action |
| Stale screenshot file | Command queue / deferred work (game loop) | Deferred screenshot in bridge after N frames; python waits for newer timestamp |

## Anti-patterns to avoid

- Misleading active nav highlight (Matchday selected while on Dashboard).
- Screenshot files saved before the screen is actually visible (sleep-only harness).
- Huge blank panels (post-match spacer cards with no content).
- Truncated consequence text (non-scrolling labels for long pressure strings).
- Prose-heavy dashboard cards (duplicate context in `Next Decision` and `Club Notes`).
- Slot metadata squeezed into narrow columns (fixture names stacked one character wide).
- Exact-only player profile display when partial knowledge is supposed to matter.
- UI labels implying more authority than the role actually has (Assistant Manager labeled like outright approval).
- Copying external repo UI code or assets into Touchline.
- Duplicating business rules in scene `_Ready` instead of `GameState` / services.

## Implementation plan

Ordered work for the evidence-backed Yellow → improved pass:

| Step | Issue | Action | Verification |
|---:|---|---|---|
| 1 | P1 screenshot pipeline | Frame-deferred viewport capture in `AuditCommandBridge`; assert scene/nav/anchor in Python; archive old PNGs | `screenshot_capture_report.md`, hash validation JSON |
| 2 | P1 sidebar active route | Confirm `ApplyRailNavigation` on all rail scenes; matchday CTA elsewhere | `audit_sidebar_active_route_check.gd` |
| 3 | P1 post-match layout | Scroll + summary grid in `PostMatchScene` / scene file | `audit_post_match_layout_check.gd`, `manager-post-match.png` |
| 4 | P2 slot metadata | Labeled rows in `MainMenu`, `SaveLoadScene` | `STEP55_SAVE_ERROR_STATE_PASS`, slot-card PNGs |
| 5 | P2 dashboard density | Tighten copy in `ClubDashboard` | `STEP48_DASHBOARD_CONTEXT_PASS` |
| 6 | P2 partial information | Confidence blocks on squad + profile | `audit_partial_information_check.gd` |
| 7 | P2 role screenshots | Per-role flow in `active_desktop_playtest.py` | `assistant-manager-*.png`, `head-coach-*.png` |
| 8 | P2 training/scouting + recruitment/job market | Dashboard section captures with role anchors | `*training-scouting.png`, `*recruitment-contracts.png`, `manager-job-market.png` |
| 9 | Regression | Full headless suite + active playtest | `full-godot-suite.log`, `ACTIVE_PLAYTEST_USER_FLOW_PASS` |

### Platform notes (Cloud Agent / Linux)

- Godot binary: `$GODOT` or `$GODOT_CONSOLE` (Linux has no `_console.exe` suffix).
- Desktop harness: use Linux window focus (`xdotool`) when `platform.system() != "Windows"`.
- Video: optional; timed live-match PNG sequence is acceptable when `ffmpeg` is missing.

## Related Touchline files

| Area | Files |
|---|---|
| Theme / nav | `game/scripts/TouchlineTheme.cs` |
| Audit capture | `game/scripts/AuditCommandBridge.cs`, `game/scripts/AuditUiStateWriter.cs` |
| Screens | `game/scripts/ClubDashboard.cs`, `PostMatchScene.cs`, `MainMenu.cs`, `SaveLoadScene.cs`, `SquadScreen.cs`, `PlayerProfile.cs` |
| Harness | `docs/audit/active-playtest/scripts/active_desktop_playtest.py` |
| Evidence | `docs/audit/active-playtest/for_chatgpt_summary.md`, `evidence_review_table.md` |
