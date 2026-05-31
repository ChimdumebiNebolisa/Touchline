# Before / After Summary

## Before (research-only pass)

- Theme and audit scripts passed while UI still overflowed at 1280×720.
- Dashboard `InsightCard` stacked long domain strings in single labels.
- Career setup had no form scroll; fields fell below the window.
- Player profile buried partial info inside one `IdentityLabel` paragraph.
- Rail screens duplicated shell trees without a bounded `MainScroll`.
- Windows summary claimed Green without viewport-bounded layout proof.

## After (this rebuild)

- **Scroll regions:** `MainScroll` on rail screens; `FormScroll` on career setup; `InsightScroll` on dashboard notes.
- **Hierarchy:** Dashboard summary grid (3 columns) stays above scrollable detail; post-match action card pinned outside scroll.
- **Partial information:** Player profile exposes `ProfileConfidenceLabel`, `KnownLabel`, `EstimatedLabel`, `UnknownLabel`.
- **Copy density:** Dashboard render paths cap bullets via `TakeLines`; shorter meta lines on summary cards.
- **Nav:** `ApplyRailNavigation` runs after scene tree fixes; audit sidebar check passes.
- **Harness:** `active_desktop_playtest.py` button paths updated for new node tree.

## Visual expectation

| Screen | Before | After |
|--------|--------|-------|
| Career setup | Clipped form | Scrollable form + pinned Begin Career |
| Dashboard | Wall of text in insight column | Section eyebrows + scrollable cards |
| Player profile | One identity paragraph | Four partial-info rows + trait line |
| Post-match | Empty/clipped bands | Header + summary grid + scroll + footer CTA |

Refresh PNGs in `docs/audit/active-playtest/screenshots/` to confirm on your machine.
