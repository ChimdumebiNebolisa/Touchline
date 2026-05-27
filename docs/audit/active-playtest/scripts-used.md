# Active Playtest Scripts

## Headless state check

**File:** `game/scripts/active_playtest_user_flow_check.gd`

Run via:
```
<godot_console> --headless --path game -s res://scripts/active_playtest_user_flow_check.gd
```

What it checks per role (Assistant Manager, Head Coach, Manager):

- Training: before/after focus and familiarity delta for role-appropriate authority
- Scouting: before/after TrainingScoutingSummary; assignment opened or recommendation logged
- Tactics: before/after TacticalFormation and TeamStyleName; assistant recommend-only; HC/Manager finalize
- Recruitment/contracts: authority keyword in RecruitmentFoundationSummary; news/log updated
- Post-match consequences: board/fan/squad morale and job pressure delta after ResolveCurrentMatchInstantly
- Save/load persistence: training focus and formation survive save+reload; week advance continues
- Promises: ValidatePhase3PromiseLifecycleContract + PromiseSummary has lifecycle statuses
- Live Match consistency: ValidateStage5MatchEngineAlignmentContract for shared timeline
- Job market/career state: ValidateStage8CareerJobMarketContract with save/load
- Role authority contract: ValidateRoleAuthorityStabilizationContract per role

Expected output on success: `ACTIVE_PLAYTEST_USER_FLOW_PASS`

Each assertion emits: `ACTIVE_PLAYTEST_ASSERT|<role>|<area>|<before>|<action>|<after>|<expected>|PASS`

## Desktop automation harness

**File:** `docs/audit/active-playtest/scripts/active_desktop_playtest.py`

Run via:
```
py docs/audit/active-playtest/scripts/active_desktop_playtest.py
```

Options:
- `--skip-gui` — headless check only
- `--skip-headless` — GUI screenshots only

What it does:

1. Runs the headless active playtest check and captures `ACTIVE_PLAYTEST_ASSERT` rows.
2. Launches the GUI game window and drives navigation screenshots per role.
3. Writes a timestamped JSON summary to `docs/audit/active-playtest/logs/active-playtest-run-<ts>.json`.
   The JSON includes `assertions` (from the headless check) and `observations` (GUI navigation steps).

Required Python packages: `mss`, `pyautogui`, `pygetwindow`
