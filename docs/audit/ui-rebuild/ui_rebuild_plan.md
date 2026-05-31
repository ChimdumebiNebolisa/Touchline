# UI Rebuild Plan

## What failed

The prior UI polish pass optimized for audit pass lines and screenshot capture plumbing, not viewport-bounded layout at 1280×720.

- Rail screens use a bare `VBoxContainer` `MainColumn` without an outer `ScrollContainer`, so stacked cards and labels exceed 720px.
- Domain summaries are assigned to single `Label` nodes (dashboard insight, player profile identity), producing debug-dump walls of text.
- `CareerSetup` injects many option fields with no scroll region; controls disappear below the window.
- Headless checks assert text tokens and node existence, not control bounds or visual hierarchy.
- The Windows “Green” summary overclaimed readiness while screenshots still showed overflow, clipping, and empty panels.

**Core decision:** Keep `GameState`, save/load, match simulation, and tests. Rebuild scenes and presentation scripts only.

## Research rules being enforced

1. Use **Containers** for layout (Margin → HBox/VBox), not free-positioned controls.
2. Use **ScrollContainer** for any long content; pin primary actions outside the scroll.
3. Use **cards** for grouped information (eyebrow + value + 1–2 meta lines).
4. Use **concise labels** and bullet caps; no multi-section paragraphs in one label.
5. Show **status summary before details** (compact cards, then scrollable sections).
6. Use **consistent nav** with `TouchlineTheme.ApplyRailNavigation` and matching active route.
7. Each screen has a **primary action** in a footer or action card.

## New layout shell

Shared kit: `game/scripts/ui/` + `game/scenes/ui/TouchlineManagementShell.tscn`

| Region | Role |
|--------|------|
| Top bar | Screen title, season/date chip, save status |
| Left nav rail | Identity + nav + save/back (220px) |
| Main column | Page header (fixed) + `MainScroll` (expand) + content cards |
| Optional right panel | Squad list / context |
| Footer bar | Status line + primary CTA |

Viewport budget at 1280×720: top ~52px, footer ~44px, rail 220px, remaining width for scrollable main content.

## Screen rebuild list

| Screen | Layout |
|--------|--------|
| Main menu / save-load | Centered card, labeled slot rows, menu scroll if needed |
| New career | Left form scroll + right preview + pinned actions |
| Choose club | Scroll list + preview card |
| Dashboard | Summary card row → next-action card → scroll sections (news, objectives, training, recruitment) |
| Post-match | Score headline, stat chips, scroll consequences, pinned Continue |
| Squad | Scroll list + detail panel + profile CTA |
| Player profile | Summary cards + Known/Estimated/Unknown/Confidence cards + chips |
| Tactics | Formation + instruction cards + apply CTA |
| Fixtures / standings | Header cards + scroll table/timeline |
| Matchday / live match | Summary cards + pitch/controls within 720p |
| Training/scouting, recruitment, job market | Dashboard section cards (not prose dumps) |

## Acceptance checks

Screenshots at 1280×720 must show:

- No content clipped outside the window
- Visible scroll regions where content is long
- Card hierarchy (not one giant label)
- Dashboard summary cards above detail scroll
- Career setup scrollable form
- Post-match complete (score, stats, reactions, next action)
- Player profile partial-info blocks
- Sidebar active route matches screen

Automated: `audit_ui_rebuild_layout_check.gd`, existing step/audit scripts, `active_playtest_user_flow_check.gd`, Windows `active_desktop_playtest.py`.

## Implementation phases

1. Shared UI shell helpers + management shell scene
2. Critical: main menu, save/load, career setup, dashboard, post-match
3. Core gameplay screens (squad, profile, tactics, fixtures, matchday, live match, standings)
4. Dashboard foundation section cards
5. Visual proof docs and screenshot validation
