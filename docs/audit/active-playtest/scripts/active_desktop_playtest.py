#!/usr/bin/env python3
"""Active desktop playtest harness for Touchline.

Launches the Godot game window, drives navigation screenshots, runs the
headless active_playtest_user_flow_check.gd validation, parses its structured
assertion log, and writes a timestamped JSON run summary.
"""

from __future__ import annotations

import argparse
import json
import os
import subprocess
import sys
import time
from dataclasses import dataclass, field
from datetime import datetime, timezone
from pathlib import Path

import mss
import pyautogui
import pygetwindow as gw

REPO_ROOT = Path(__file__).resolve().parents[4]
GAME_DIR = REPO_ROOT / "game"
AUDIT_ROOT = REPO_ROOT / "docs" / "audit" / "active-playtest"
SCREENSHOT_DIR = AUDIT_ROOT / "screenshots"
LOG_DIR = AUDIT_ROOT / "logs"
VIDEO_DIR = AUDIT_ROOT / "videos"

ROLES = ["assistant-manager", "head-coach", "manager"]
ROLE_LABELS = ["Assistant Manager", "Head Coach", "Manager"]

DEFAULT_GODOT = Path(
    os.environ.get(
        "GODOT_CONSOLE",
        r"C:\Users\Chimdumebi\AppData\Local\Microsoft\WinGet\Packages"
        r"\GodotEngine.GodotEngine.Mono_Microsoft.Winget.Source_8wekyb3d8bbwe"
        r"\Godot_v4.6.2-stable_mono_win64\Godot_v4.6.2-stable_mono_win64_console.exe",
    )
)

pyautogui.FAILSAFE = True
pyautogui.PAUSE = 0.35


@dataclass
class PlaytestObservation:
    role: str
    step: str
    note: str = ""
    screenshot: str = ""


@dataclass
class AssertionRow:
    """Parsed row from ACTIVE_PLAYTEST_ASSERT headless output."""

    role: str
    area: str
    before: str
    action: str
    after: str
    expected: str
    passed: bool
    evidence: str


@dataclass
class PlaytestRun:
    started_at: str = field(default_factory=lambda: datetime.now(timezone.utc).isoformat())
    observations: list[PlaytestObservation] = field(default_factory=list)
    assertions: list[AssertionRow] = field(default_factory=list)
    headless_pass: bool | None = None
    headless_log: str = ""
    errors: list[str] = field(default_factory=list)


def log(msg: str) -> None:
    print(msg, flush=True)


def find_godot_console() -> Path:
    if DEFAULT_GODOT.is_file():
        return DEFAULT_GODOT
    for cmd in ("Godot_v4.6.2-stable_mono_win64_console.exe",):
        found = subprocess.run(["where", cmd], capture_output=True, text=True, check=False)
        if found.returncode == 0 and found.stdout.strip():
            return Path(found.stdout.strip().splitlines()[0])
    raise FileNotFoundError("Godot console executable not found. Set GODOT_CONSOLE.")


def parse_assertion_rows(output: str) -> list[AssertionRow]:
    """Extract ACTIVE_PLAYTEST_ASSERT rows from headless log output."""
    rows: list[AssertionRow] = []
    for line in output.splitlines():
        if not line.startswith("ACTIVE_PLAYTEST_ASSERT|"):
            continue
        parts = line.split("|")
        if len(parts) < 8:
            continue
        rows.append(
            AssertionRow(
                role=parts[1],
                area=parts[2],
                before=parts[3],
                action=parts[4],
                after=parts[5],
                expected=parts[6],
                passed=parts[7].strip() == "PASS",
                evidence=parts[8] if len(parts) > 8 else "",
            )
        )
    return rows


def run_headless_check(godot_console: Path, log_path: Path) -> tuple[bool, list[AssertionRow]]:
    cmd = [
        str(godot_console),
        "--headless",
        "--path",
        str(GAME_DIR),
        "-s",
        "res://scripts/active_playtest_user_flow_check.gd",
    ]
    log(f"Running headless active playtest: {' '.join(cmd)}")
    result = subprocess.run(cmd, cwd=REPO_ROOT, capture_output=True, text=True, check=False)
    output = (result.stdout or "") + (result.stderr or "")
    log_path.write_text(output, encoding="utf-8")
    passed = "ACTIVE_PLAYTEST_USER_FLOW_PASS" in output
    assertion_rows = parse_assertion_rows(output)
    if not passed:
        log(output[-4000:] if len(output) > 4000 else output)
    else:
        log(f"Headless check passed. Assertions parsed: {len(assertion_rows)}")
        for row in assertion_rows:
            log(f"  {'PASS' if row.passed else 'FAIL'} | {row.role} | {row.area}")
    return passed, assertion_rows


def focus_game_window(timeout_s: float = 45.0) -> gw.Win32Window:
    deadline = time.time() + timeout_s
    while time.time() < deadline:
        for title_hint in ("Touchline", "Godot"):
            windows = [w for w in gw.getAllWindows() if title_hint.lower() in (w.title or "").lower()]
            if windows:
                win = windows[0]
                try:
                    if win.isMinimized:
                        win.restore()
                    win.activate()
                except Exception:
                    pass
                time.sleep(0.5)
                return win
        time.sleep(0.5)
    raise TimeoutError("Could not find Touchline/Godot game window")


def screenshot_window(win: gw.Win32Window, path: Path) -> None:
    left, top, width, height = win.left, win.top, win.width, win.height
    if width <= 0 or height <= 0:
        pyautogui.screenshot(str(path))
        return
    with mss.MSS() as sct:
        monitor = {"left": int(left), "top": int(top), "width": int(width), "height": int(height)}
        img = sct.grab(monitor)
        mss.tools.to_png(img.rgb, img.size, output=str(path))


def click_fraction(win: gw.Win32Window, fx: float, fy: float) -> None:
    x = int(win.left + win.width * fx)
    y = int(win.top + win.height * fy)
    pyautogui.click(x, y)


def drive_role_flow(run: PlaytestRun, role_slug: str, role_index: int, win: gw.Win32Window) -> None:
    """Drive navigation screenshots and action evidence per role."""
    nav_steps = [
        ("main-menu", 0.50, 0.42, "Main menu before new career"),
        ("new-career-click", 0.50, 0.48, "Open career setup"),
        ("career-setup", 0.50, 0.35, "Career setup screen"),
        ("begin-career", 0.72, 0.82, "Begin career"),
        ("choose-club", 0.35, 0.40, "Club list"),
        ("confirm-club", 0.35, 0.88, "Confirm club"),
        ("dashboard", 0.62, 0.45, "Manager hub dashboard"),
        ("squad-nav", 0.08, 0.30, "Open squad"),
        ("squad-screen", 0.55, 0.45, "Squad screen"),
        ("tactics-nav", 0.08, 0.36, "Open tactics"),
        ("tactics-screen", 0.55, 0.45, "Tactics screen"),
        ("fixtures-nav", 0.08, 0.42, "Open fixtures"),
        ("matchday-nav", 0.08, 0.48, "Open matchday"),
        ("matchday-screen", 0.55, 0.45, "Matchday screen"),
    ]

    if role_index > 0:
        click_fraction(win, 0.08, 0.92)
        time.sleep(1.0)
        click_fraction(win, 0.50, 0.48)
        time.sleep(1.5)

    for step_name, fx, fy, note in nav_steps:
        if step_name == "career-setup" and role_index >= 0:
            for _ in range(role_index):
                pyautogui.press("tab")
                time.sleep(0.1)
            pyautogui.press("down")
            time.sleep(0.2)

        click_fraction(win, fx, fy)
        time.sleep(1.2 if "nav" not in step_name else 0.9)
        path = SCREENSHOT_DIR / f"{role_slug}-{step_name}.png"
        screenshot_window(win, path)
        run.observations.append(
            PlaytestObservation(
                role=role_slug,
                step=step_name,
                note=note,
                screenshot=str(path.relative_to(REPO_ROOT)),
            )
        )


def launch_gui_game(godot_console: Path) -> subprocess.Popen:
    gui = Path(str(godot_console).replace("_console", ""))
    if not gui.is_file():
        gui = godot_console
    cmd = [str(gui), "--path", str(GAME_DIR)]
    log(f"Launching GUI game: {' '.join(cmd)}")
    return subprocess.Popen(cmd, cwd=REPO_ROOT)


def main() -> int:
    parser = argparse.ArgumentParser(description="Touchline active desktop playtest")
    parser.add_argument("--skip-gui", action="store_true", help="Only run headless validation")
    parser.add_argument("--skip-headless", action="store_true", help="Only run GUI screenshots")
    args = parser.parse_args()

    SCREENSHOT_DIR.mkdir(parents=True, exist_ok=True)
    LOG_DIR.mkdir(parents=True, exist_ok=True)
    VIDEO_DIR.mkdir(parents=True, exist_ok=True)

    run = PlaytestRun()
    godot_console = find_godot_console()
    ts = datetime.now().strftime("%Y%m%d-%H%M%S")
    headless_log = LOG_DIR / f"headless-active-playtest-{ts}.log"

    if not args.skip_headless:
        try:
            passed, assertions = run_headless_check(godot_console, headless_log)
            run.headless_pass = passed
            run.assertions = assertions
            run.headless_log = str(headless_log.relative_to(REPO_ROOT))
            if not passed:
                run.errors.append("Headless active_playtest_user_flow_check failed")
        except Exception as exc:
            run.headless_pass = False
            run.errors.append(f"Headless check error: {exc}")

    if not args.skip_gui:
        proc = None
        try:
            proc = launch_gui_game(godot_console)
            time.sleep(6.0)
            win = focus_game_window()
            for idx, role_slug in enumerate(ROLES):
                drive_role_flow(run, role_slug, idx, win)
        except Exception as exc:
            run.errors.append(f"GUI playtest error: {exc}")
        finally:
            if proc is not None and proc.poll() is None:
                proc.terminate()
                try:
                    proc.wait(timeout=5)
                except subprocess.TimeoutExpired:
                    proc.kill()

    summary_path = LOG_DIR / f"active-playtest-run-{ts}.json"
    summary_path.write_text(
        json.dumps(
            {
                "started_at": run.started_at,
                "headless_pass": run.headless_pass,
                "headless_log": run.headless_log,
                "errors": run.errors,
                "assertions": [
                    {
                        "role": a.role,
                        "area": a.area,
                        "before": a.before,
                        "action": a.action,
                        "after": a.after,
                        "expected": a.expected,
                        "passed": a.passed,
                        "evidence": a.evidence,
                    }
                    for a in run.assertions
                ],
                "observations": [obs.__dict__ for obs in run.observations],
            },
            indent=2,
        ),
        encoding="utf-8",
    )
    log(f"Wrote run summary: {summary_path}")

    if run.headless_pass is False:
        return 1
    if run.errors and run.headless_pass is None:
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
