#!/usr/bin/env python3
"""Reliable active desktop playtest harness for Touchline.

This version prepares deterministic role saves headlessly, drives the GUI
through an audit-only Godot command bridge, verifies the expected scene/anchor
before every capture, and writes validation metadata plus duplicate-hash
analysis.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import os
import platform
import shutil
import signal
import subprocess
import sys
import time
import uuid
from dataclasses import asdict, dataclass, field
from datetime import datetime, timezone
from pathlib import Path
from typing import Iterable

IS_WINDOWS = platform.system() == "Windows"
if IS_WINDOWS:
    import ctypes
    from ctypes import wintypes

REPO_ROOT = Path(__file__).resolve().parents[4]
GAME_DIR = REPO_ROOT / "game"
AUDIT_ROOT = REPO_ROOT / "docs" / "audit" / "active-playtest"
SCREENSHOT_DIR = AUDIT_ROOT / "screenshots"
ARCHIVE_ROOT = AUDIT_ROOT / "archive"
LOG_DIR = AUDIT_ROOT / "logs"
VIDEO_DIR = AUDIT_ROOT / "videos"

CURRENT_UI_STATE_PATH = LOG_DIR / "current-ui-state.json"
COMMAND_PATH = LOG_DIR / "audit-command.json"
COMMAND_RESULT_PATH = LOG_DIR / "audit-command-result.json"
VALIDATION_PATH = LOG_DIR / "screenshot-capture-validation.json"
RUN_SUMMARY_PATTERN = "active-playtest-run-{ts}.json"
SCREENSHOT_REPORT_PATH = AUDIT_ROOT / "screenshot_capture_report.md"

ROLES = [
    ("assistant-manager", "Assistant Manager", 903101),
    ("head-coach", "Head Coach", 903102),
    ("manager", "Manager", 903103),
]

DEFAULT_GODOT = Path(
    os.environ.get(
        "GODOT_CONSOLE",
        r"C:\Users\Chimdumebi\AppData\Local\Microsoft\WinGet\Packages"
        r"\GodotEngine.GodotEngine.Mono_Microsoft.Winget.Source_8wekyb3d8bbwe"
        r"\Godot_v4.6.2-stable_mono_win64\Godot_v4.6.2-stable_mono_win64_console.exe",
    )
)

BUTTON_PATHS = {
    "mainmenu_new_career": "Center/MenuCard/Padding/Menu/NewCareerButton",
    "mainmenu_load": "Center/MenuCard/Padding/Menu/LoadGameButton",
    "career_start": "RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton",
    "career_role_option": "RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/RoleOption",
    "saveload_load": "RootMargin/MainColumn/ActionsRow/LoadButton",
    "dashboard_tactics": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/TacticsButton",
    "dashboard_squad": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/SquadButton",
    "dashboard_fixtures": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/FixturesButton",
    "dashboard_standings": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/StandingsButton",
    "dashboard_matchday": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/MatchdayButton",
    "dashboard_training": "RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/ApplyTrainingButton",
    "dashboard_scouting": "RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/StartScoutingButton",
    "dashboard_recruitment": "RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/RecruitmentButton",
    "dashboard_contract": "RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/ContractButton",
    "dashboard_job_market": "RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/JobMarketButton",
    "tactics_dashboard": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/DashboardButton",
    "tactics_squad": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/SquadButton",
    "squad_fixtures": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/FixturesButton",
    "squad_open_profile": "RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/OpenProfileButton",
    "profile_back": "RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/BackButton",
    "fixtures_standings": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/StandingsButton",
    "standings_matchday": "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/MatchdayButton",
    "matchday_start_live": "RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton",
    "matchday_instant_result": "RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton",
    "live_continue": "Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/BackButton",
}


@dataclass
class AssertionRow:
    role: str
    area: str
    before: str
    action: str
    after: str
    expected: str
    passed: bool
    evidence: str


@dataclass
class UiState:
    scene_name: str
    timestamp_utc: str
    role_text: str
    selected_nav: str
    anchors: list[str]


@dataclass
class CaptureObservation:
    role: str
    label: str
    expected_screen: str
    actual_detected_screen: str
    expected_nav: str
    actual_nav: str
    timestamp_utc: str
    passed: bool
    visible_anchor_used: str
    screenshot: str = ""
    image_hash: str = ""
    note: str = ""


@dataclass
class PlaytestRun:
    started_at: str = field(default_factory=lambda: datetime.now(timezone.utc).isoformat())
    observations: list[CaptureObservation] = field(default_factory=list)
    assertions: list[AssertionRow] = field(default_factory=list)
    headless_pass: bool | None = None
    headless_log: str = ""
    errors: list[str] = field(default_factory=list)
    old_screenshot_count: int = 0
    old_unique_hash_count: int = 0
    new_screenshot_count: int = 0
    new_unique_hash_count: int = 0
    archive_path: str = ""
    duplicate_hash_groups: list[dict] = field(default_factory=list)
    video_capture_skipped_reason: str = ""


@dataclass
class GameWindow:
    hwnd: int
    pid: int
    title: str
    left: int
    top: int
    right: int
    bottom: int

    @property
    def width(self) -> int:
        return self.right - self.left

    @property
    def height(self) -> int:
        return self.bottom - self.top


if IS_WINDOWS:
    USER32 = ctypes.windll.user32
    SW_RESTORE = 9
    SW_MAXIMIZE = 3


def log(message: str) -> None:
    print(message, flush=True)


def find_godot_console() -> Path:
    if DEFAULT_GODOT.is_file():
        return DEFAULT_GODOT
    lookup_cmd = (
        ["where", "Godot_v4.6.2-stable_mono_win64_console.exe"]
        if IS_WINDOWS
        else ["bash", "-lc", "command -v godot || command -v Godot"]
    )
    found = subprocess.run(lookup_cmd, capture_output=True, text=True, check=False)
    if found.returncode == 0 and found.stdout.strip():
        return Path(found.stdout.strip().splitlines()[0])
    raise FileNotFoundError("Godot console executable not found. Set GODOT_CONSOLE.")


def parse_assertion_rows(output: str) -> list[AssertionRow]:
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
    rows = parse_assertion_rows(output)
    if passed:
        log(f"Headless check passed. Assertions parsed: {len(rows)}")
    else:
        log(output[-4000:] if len(output) > 4000 else output)
    return passed, rows


def run_prepare_slot(godot_console: Path, role_label: str, seed: int) -> None:
    cmd = [
        str(godot_console),
        "--headless",
        "--path",
        str(GAME_DIR),
        "-s",
        "res://scripts/audit_prepare_capture_state.gd",
        "--",
        "--role",
        role_label,
        "--manager",
        "Audit Capture",
        "--seed",
        str(seed),
    ]
    log(f"Preparing slot for {role_label}: {' '.join(cmd)}")
    result = subprocess.run(cmd, cwd=REPO_ROOT, capture_output=True, text=True, check=False)
    output = (result.stdout or "") + (result.stderr or "")
    if "AUDIT_CAPTURE_PREP_PASS" not in output:
        raise RuntimeError(output.strip() or f"Slot prep failed for {role_label}")


def launch_gui_game(godot_console: Path) -> subprocess.Popen[str]:
    gui = Path(str(godot_console).replace("_console", ""))
    if not gui.is_file():
        gui = godot_console
    cmd = [str(gui), "--path", str(GAME_DIR), "--resolution", "1280x720"]
    log(f"Launching GUI game: {' '.join(cmd)}")
    popen_kwargs: dict = {"cwd": REPO_ROOT}
    if not IS_WINDOWS:
        popen_kwargs["start_new_session"] = True
    return subprocess.Popen(cmd, **popen_kwargs)


def terminate_process_tree(process: subprocess.Popen[str]) -> None:
    if process.poll() is not None:
        return
    if IS_WINDOWS:
        subprocess.run(
            ["taskkill", "/F", "/T", "/PID", str(process.pid)],
            cwd=REPO_ROOT,
            capture_output=True,
            text=True,
            check=False,
        )
    else:
        try:
            os.killpg(os.getpgid(process.pid), signal.SIGTERM)
        except ProcessLookupError:
            process.terminate()
    try:
        process.wait(timeout=5)
    except subprocess.TimeoutExpired:
        process.kill()


def _get_window_rect(hwnd: int) -> tuple[int, int, int, int]:
    if not IS_WINDOWS:
        raise OSError("Window rect lookup is only implemented on Windows.")
    rect = wintypes.RECT()
    if not USER32.GetWindowRect(hwnd, ctypes.byref(rect)):
        raise OSError(f"Could not read window rect for hwnd {hwnd}")
    return rect.left, rect.top, rect.right, rect.bottom


def _enumerate_windows() -> list[tuple[int, int, str]]:
    if not IS_WINDOWS:
        return []
    windows: list[tuple[int, int, str]] = []

    @ctypes.WINFUNCTYPE(ctypes.c_bool, wintypes.HWND, wintypes.LPARAM)
    def enum_proc(hwnd: int, _lparam: int) -> bool:
        if not USER32.IsWindowVisible(hwnd):
            return True
        length = USER32.GetWindowTextLengthW(hwnd)
        if length <= 0:
            return True
        buffer = ctypes.create_unicode_buffer(length + 1)
        USER32.GetWindowTextW(hwnd, buffer, length + 1)
        title = buffer.value
        if title:
            pid = wintypes.DWORD()
            USER32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
            windows.append((hwnd, int(pid.value), title))
        return True

    USER32.EnumWindows(enum_proc, 0)
    return windows


def _focus_game_window_linux(process: subprocess.Popen[str], timeout_s: float = 45.0) -> GameWindow:
    deadline = time.time() + timeout_s
    while time.time() < deadline:
        search = subprocess.run(
            ["xdotool", "search", "--pid", str(process.pid)],
            capture_output=True,
            text=True,
            check=False,
        )
        if search.returncode == 0 and search.stdout.strip():
            window_id = search.stdout.strip().splitlines()[0]
            subprocess.run(
                ["xdotool", "windowactivate", "--sync", window_id],
                capture_output=True,
                text=True,
                check=False,
            )
            time.sleep(0.75)
            geometry = subprocess.run(
                ["xdotool", "getwindowgeometry", "--shell", window_id],
                capture_output=True,
                text=True,
                check=False,
            )
            left = top = width = height = 0
            if geometry.returncode == 0:
                for line in geometry.stdout.splitlines():
                    if line.startswith("X="):
                        left = int(line.split("=", 1)[1])
                    elif line.startswith("Y="):
                        top = int(line.split("=", 1)[1])
                    elif line.startswith("WIDTH="):
                        width = int(line.split("=", 1)[1])
                    elif line.startswith("HEIGHT="):
                        height = int(line.split("=", 1)[1])
            return GameWindow(
                hwnd=int(window_id),
                pid=process.pid,
                title="Touchline",
                left=left,
                top=top,
                right=left + width,
                bottom=top + height,
            )
        time.sleep(0.5)
    raise TimeoutError(f"Could not find Touchline window for process {process.pid}")


def focus_game_window(process: subprocess.Popen[str], timeout_s: float = 45.0) -> GameWindow:
    if not IS_WINDOWS:
        return _focus_game_window_linux(process, timeout_s)
    deadline = time.time() + timeout_s
    while time.time() < deadline:
        matches = [
            (hwnd, pid, title)
            for hwnd, pid, title in _enumerate_windows()
            if pid == process.pid
        ]
        if matches:
            hwnd, pid, title = matches[0]
            try:
                USER32.ShowWindow(hwnd, SW_RESTORE)
                USER32.ShowWindow(hwnd, SW_MAXIMIZE)
                USER32.SetForegroundWindow(hwnd)
            except Exception:
                pass
            time.sleep(0.75)
            left, top, right, bottom = _get_window_rect(hwnd)
            return GameWindow(hwnd=hwnd, pid=pid, title=title, left=left, top=top, right=right, bottom=bottom)
        time.sleep(0.5)
    raise TimeoutError(f"Could not find Touchline window for process {process.pid}")


def sha256_file(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(65536), b""):
            digest.update(chunk)
    return digest.hexdigest()


def read_ui_state() -> UiState | None:
    if not CURRENT_UI_STATE_PATH.exists():
        return None
    try:
        payload = json.loads(CURRENT_UI_STATE_PATH.read_text(encoding="utf-8"))
    except json.JSONDecodeError:
        return None
    return UiState(
        scene_name=str(payload.get("SceneName", "")),
        timestamp_utc=str(payload.get("TimestampUtc", "")),
        role_text=str(payload.get("RoleText", "")),
        selected_nav=str(payload.get("SelectedNav", "")),
        anchors=[str(anchor) for anchor in payload.get("Anchors", [])],
    )


def clear_runtime_state_files() -> None:
    for path in (CURRENT_UI_STATE_PATH, COMMAND_PATH, COMMAND_RESULT_PATH):
        if path.exists():
            path.unlink()


def find_anchor(state: UiState, candidates: Iterable[str]) -> str:
    haystacks = [state.scene_name, state.selected_nav, state.role_text, *state.anchors]
    lowered = [value.lower() for value in haystacks if value]
    for candidate in candidates:
        candidate_lower = candidate.lower()
        if any(candidate_lower in value for value in lowered):
            return candidate
    return ""


def wait_for_ui_state(
    *,
    expected_scene: str,
    expected_nav: str = "",
    anchor_candidates: Iterable[str] = (),
    role_anchor: str = "",
    timeout_s: float = 20.0,
    newer_than: str = "",
) -> tuple[UiState | None, str]:
    anchor_list = list(anchor_candidates)
    deadline = time.time() + timeout_s
    while time.time() < deadline:
        state = read_ui_state()
        if state is None:
            time.sleep(0.15)
            continue
        if newer_than and state.timestamp_utc <= newer_than:
            time.sleep(0.15)
            continue
        if state.scene_name != expected_scene:
            time.sleep(0.15)
            continue
        if expected_nav and state.selected_nav != expected_nav:
            time.sleep(0.15)
            continue
        if role_anchor and role_anchor.lower() not in state.role_text.lower():
            time.sleep(0.15)
            continue
        matched_anchor = find_anchor(state, anchor_list)
        if anchor_list and not matched_anchor:
            time.sleep(0.15)
            continue
        return state, matched_anchor or expected_scene
    return None, ""


def wait_for_state_change(previous_state: UiState, timeout_s: float = 20.0) -> UiState | None:
    deadline = time.time() + timeout_s
    while time.time() < deadline:
        state = read_ui_state()
        if state is None:
            time.sleep(0.15)
            continue
        if state.scene_name != previous_state.scene_name:
            return state
        if state.timestamp_utc > previous_state.timestamp_utc and (
            state.anchors != previous_state.anchors
            or state.role_text != previous_state.role_text
            or state.selected_nav != previous_state.selected_nav
        ):
            return state
        time.sleep(0.15)
    return None


def issue_command(action: str, path: str, *, expected_scene: str, value: str = "", timeout_s: float = 12.0) -> dict:
    command_id = str(uuid.uuid4())
    payload = {
        "Id": command_id,
        "Action": action,
        "Path": path,
        "Value": value,
        "ExpectedScene": expected_scene,
    }
    if COMMAND_RESULT_PATH.exists():
        COMMAND_RESULT_PATH.unlink()
    COMMAND_PATH.write_text(json.dumps(payload, indent=2), encoding="utf-8")

    deadline = time.time() + timeout_s
    while time.time() < deadline:
        if not COMMAND_RESULT_PATH.exists():
            time.sleep(0.1)
            continue
        try:
            result = json.loads(COMMAND_RESULT_PATH.read_text(encoding="utf-8"))
        except json.JSONDecodeError:
            time.sleep(0.1)
            continue
        if result.get("Id") != command_id:
            time.sleep(0.1)
            continue
        return result
    raise TimeoutError(f"Timed out waiting for audit command result: {action} {path}")


def capture_verified_step(
    run: PlaytestRun,
    window: GameWindow,
    *,
    role_slug: str,
    label: str,
    filename: str,
    expected_scene: str,
    expected_nav: str = "",
    anchor_candidates: Iterable[str] = (),
    role_anchor: str = "",
    command: tuple[str, str] | None = None,
    note: str = "",
    require_state_change_from: UiState | None = None,
    timeout_s: float = 20.0,
) -> UiState | None:
    previous_timestamp = require_state_change_from.timestamp_utc if require_state_change_from else ""
    if command is not None:
        action, path = command
        command_scene = require_state_change_from.scene_name if require_state_change_from else expected_scene
        result = issue_command(action, path, expected_scene=command_scene)
        if not result.get("Success", False):
            run.errors.append(f"{role_slug}:{label} command failed: {result.get('Message', '')}")
            run.observations.append(
                CaptureObservation(
                    role=role_slug,
                    label=label,
                    expected_screen=expected_scene,
                    actual_detected_screen=str(result.get("SceneName", "")),
                    expected_nav=expected_nav,
                    actual_nav="",
                    timestamp_utc=datetime.now(timezone.utc).isoformat(),
                    passed=False,
                    visible_anchor_used="",
                    note=result.get("Message", ""),
                )
            )
            return None

    state, matched_anchor = wait_for_ui_state(
        expected_scene=expected_scene,
        expected_nav=expected_nav,
        anchor_candidates=anchor_candidates,
        role_anchor=role_anchor,
        timeout_s=timeout_s,
        newer_than=previous_timestamp,
    )
    if state is None:
        actual_state = read_ui_state()
        run.errors.append(f"{role_slug}:{label} expected {expected_scene} was not visible before capture")
        run.observations.append(
            CaptureObservation(
                role=role_slug,
                label=label,
                expected_screen=expected_scene,
                actual_detected_screen=actual_state.scene_name if actual_state else "",
                expected_nav=expected_nav,
                actual_nav=actual_state.selected_nav if actual_state else "",
                timestamp_utc=datetime.now(timezone.utc).isoformat(),
                passed=False,
                visible_anchor_used="",
                note=note or "Expected screen/anchor was not visible.",
            )
        )
        return None

    time.sleep(0.75)
    screenshot_path = SCREENSHOT_DIR / filename
    result = issue_command("capture_screenshot", str(screenshot_path.resolve()), expected_scene=state.scene_name)
    if not result.get("Success", False):
        run.errors.append(f"{role_slug}:{label} screenshot failed: {result.get('Message', '')}")
        run.observations.append(
            CaptureObservation(
                role=role_slug,
                label=label,
                expected_screen=expected_scene,
                actual_detected_screen=state.scene_name,
                expected_nav=expected_nav,
                actual_nav=state.selected_nav,
                timestamp_utc=datetime.now(timezone.utc).isoformat(),
                passed=False,
                visible_anchor_used=matched_anchor,
                note=result.get("Message", ""),
            )
        )
        return None
    image_hash = sha256_file(screenshot_path)
    run.observations.append(
        CaptureObservation(
            role=role_slug,
            label=label,
            expected_screen=expected_scene,
            actual_detected_screen=state.scene_name,
            expected_nav=expected_nav,
            actual_nav=state.selected_nav,
            timestamp_utc=state.timestamp_utc,
            passed=True,
            visible_anchor_used=matched_anchor,
            screenshot=str(screenshot_path.relative_to(REPO_ROOT)),
            image_hash=image_hash,
            note=note,
        )
    )
    return state


def capture_from_known_state(
    run: PlaytestRun,
    window: GameWindow,
    *,
    role_slug: str,
    label: str,
    filename: str,
    state: UiState,
    expected_screen: str,
    expected_nav: str = "",
    visible_anchor: str = "",
    note: str = "",
) -> None:
    time.sleep(0.75)
    screenshot_path = SCREENSHOT_DIR / filename
    result = issue_command("capture_screenshot", str(screenshot_path.resolve()), expected_scene=state.scene_name)
    if not result.get("Success", False):
        run.errors.append(f"{role_slug}:{label} screenshot failed: {result.get('Message', '')}")
        run.observations.append(
            CaptureObservation(
                role=role_slug,
                label=label,
                expected_screen=expected_screen,
                actual_detected_screen=state.scene_name,
                expected_nav=expected_nav,
                actual_nav=state.selected_nav,
                timestamp_utc=datetime.now(timezone.utc).isoformat(),
                passed=False,
                visible_anchor_used=visible_anchor or state.scene_name,
                note=result.get("Message", ""),
            )
        )
        return
    image_hash = sha256_file(screenshot_path)
    run.observations.append(
        CaptureObservation(
            role=role_slug,
            label=label,
            expected_screen=expected_screen,
            actual_detected_screen=state.scene_name,
            expected_nav=expected_nav,
            actual_nav=state.selected_nav,
            timestamp_utc=state.timestamp_utc,
            passed=True,
            visible_anchor_used=visible_anchor or state.scene_name,
            screenshot=str(screenshot_path.relative_to(REPO_ROOT)),
            image_hash=image_hash,
            note=note,
        )
    )


def archive_existing_screenshots(timestamp: str) -> tuple[int, int, str]:
    SCREENSHOT_DIR.mkdir(parents=True, exist_ok=True)
    existing_files = sorted(SCREENSHOT_DIR.glob("*.png"))
    if not existing_files:
        return 0, 0, ""

    hashes = [sha256_file(path) for path in existing_files]
    archive_dir = ARCHIVE_ROOT / f"screenshots-{timestamp}"
    archive_dir.mkdir(parents=True, exist_ok=True)
    for file_path in existing_files:
        shutil.move(str(file_path), archive_dir / file_path.name)

    note_path = archive_dir / "README.txt"
    note_path.write_text(
        "Archived by active_desktop_playtest.py before recapturing verified screenshots.\n",
        encoding="utf-8",
    )
    return len(existing_files), len(set(hashes)), str(archive_dir.relative_to(REPO_ROOT))


def compute_duplicate_groups(observations: list[CaptureObservation]) -> list[dict]:
    groups: dict[str, list[CaptureObservation]] = {}
    for observation in observations:
        if not observation.passed or not observation.image_hash:
            continue
        groups.setdefault(observation.image_hash, []).append(observation)

    duplicates: list[dict] = []
    for image_hash, items in groups.items():
        if len(items) <= 1:
            continue
        duplicates.append(
            {
                "hash": image_hash,
                "count": len(items),
                "screens": sorted({item.expected_screen for item in items}),
                "labels": [item.label for item in items],
                "files": [item.screenshot for item in items],
            }
        )
    return duplicates


def capture_manager_club_selection(run: PlaytestRun, godot_console: Path) -> None:
    """Capture ChooseClub from the new-career path (manager context, no pre-prepared save)."""
    role_slug = "manager"
    clear_runtime_state_files()

    proc: subprocess.Popen[str] | None = None
    try:
        proc = launch_gui_game(godot_console)
        window = focus_game_window(proc)

        main_state, _ = wait_for_ui_state(
            expected_scene="MainMenu",
            anchor_candidates=["Slot 1", "Touchline Career", "Riverton", "Continue Career"],
            timeout_s=45.0,
        )
        if main_state is None:
            run.errors.append("manager:club-selection MainMenu did not become visible")
            return

        result = issue_command(
            "press_button",
            BUTTON_PATHS["mainmenu_new_career"],
            expected_scene="MainMenu",
        )
        if not result.get("Success", False):
            run.errors.append(f"manager:club-selection new career failed: {result.get('Message', '')}")
            return

        setup_state, _ = wait_for_ui_state(
            expected_scene="CareerSetup",
            anchor_candidates=["Start Career", "Manager", "Role"],
            timeout_s=20.0,
            newer_than=main_state.timestamp_utc,
        )
        if setup_state is None:
            run.errors.append("manager:club-selection CareerSetup did not become visible")
            return

        role_result = issue_command(
            "select_option",
            BUTTON_PATHS["career_role_option"],
            expected_scene="CareerSetup",
            value="Manager",
        )
        if not role_result.get("Success", False):
            run.errors.append(f"manager:club-selection role select failed: {role_result.get('Message', '')}")
            return

        start_result = issue_command(
            "press_button",
            BUTTON_PATHS["career_start"],
            expected_scene="CareerSetup",
        )
        if not start_result.get("Success", False):
            run.errors.append(f"manager:club-selection start career failed: {start_result.get('Message', '')}")
            return

        capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="club-selection",
            filename=f"{role_slug}-club-selection.png",
            expected_scene="ChooseClub",
            anchor_candidates=[
                "Riverton Athletic",
                "Select a club",
                "Confirm Club Selection",
                "Take Charge of",
            ],
            note="New-career club selection screen with seeded club list and preview",
            require_state_change_from=setup_state,
            timeout_s=30.0,
        )
    finally:
        if proc is not None and proc.poll() is None:
            terminate_process_tree(proc)


def capture_role_flow(run: PlaytestRun, godot_console: Path, role_slug: str, role_label: str, seed: int) -> None:
    run_prepare_slot(godot_console, role_label, seed)
    clear_runtime_state_files()

    proc: subprocess.Popen[str] | None = None
    try:
        proc = launch_gui_game(godot_console)
        window = focus_game_window(proc)

        main_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="main-menu-slot-card",
            filename=f"{role_slug}-main-menu-slot-card.png",
            expected_scene="MainMenu",
            anchor_candidates=[role_label],
            note="Reflowed main-menu slot card",
            role_anchor="",
            timeout_s=30.0,
        )
        if main_state is None:
            return

        save_load_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="save-load-slot-card",
            filename=f"{role_slug}-save-load-slot-card.png",
            expected_scene="SaveLoadScene",
            anchor_candidates=[role_label],
            command=("press_button", BUTTON_PATHS["mainmenu_load"]),
            note="Reflowed save/load slot card",
            require_state_change_from=main_state,
        )
        if save_load_state is None:
            return

        dashboard_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="dashboard",
            filename=f"{role_slug}-dashboard.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=[role_label, "Next best action", "Training/scouting"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["saveload_load"]),
            note="Dashboard with corrected active route",
            require_state_change_from=save_load_state,
        )
        if dashboard_state is None:
            return

        scouting_button_text = {
            "assistant-manager": "Recommend Scouting Priority",
            "head-coach": "Request Scouting Priority",
            "manager": "Start Scouting Assignment",
        }[role_slug]
        training_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="training-scouting",
            filename=f"{role_slug}-training-scouting.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=[scouting_button_text, "Training/scouting"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_scouting"]),
            note="Training/scouting evidence from the dashboard section",
            require_state_change_from=dashboard_state,
        )
        if training_state is None:
            return

        contract_button_text = {
            "assistant-manager": "Recommend Contract Terms",
            "head-coach": "Request Contract Review",
            "manager": "Review Contract Terms",
        }[role_slug]
        recruitment_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="recruitment-contracts",
            filename=f"{role_slug}-recruitment-contracts.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=[contract_button_text, "Recruitment/contracts"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_contract"]),
            note="Recruitment/contracts evidence from the dashboard section",
            require_state_change_from=training_state,
        )
        if recruitment_state is None:
            return

        tactics_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="tactics",
            filename=f"{role_slug}-tactics.png",
            expected_scene="TacticsScreen",
            expected_nav="Tactics",
            anchor_candidates=["Submit Tactical Recommendation" if role_slug == "assistant-manager" else "Save Tactical Plan"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_tactics"]),
            note="Role-specific tactics screen",
            require_state_change_from=recruitment_state,
        )
        if tactics_state is None:
            return

        if role_slug != "manager":
            return

        return_dashboard_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="dashboard-job-market",
            filename=f"{role_slug}-job-market.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=["Job market event generated", "Career/job market"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["tactics_dashboard"]),
            note="Returned dashboard before job-market action",
            require_state_change_from=tactics_state,
        )
        if return_dashboard_state is None:
            return

        job_market_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="career-job-market",
            filename=f"{role_slug}-career-job-market.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=["Job market event generated", "Career/job market"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_job_market"]),
            note="Career/job-market dashboard evidence",
            require_state_change_from=return_dashboard_state,
        )
        if job_market_state is None:
            return

        squad_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="squad",
            filename=f"{role_slug}-squad.png",
            expected_scene="SquadScreen",
            expected_nav="Squad",
            anchor_candidates=["Profile Confidence:", "Known:", "Estimated:"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_squad"]),
            note="Squad screen with explicit partial-information cues",
            require_state_change_from=job_market_state,
        )
        if squad_state is None:
            return

        profile_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="player-profile",
            filename=f"{role_slug}-player-profile.png",
            expected_scene="PlayerProfile",
            anchor_candidates=["Profile Confidence:", "Unknown:", "Visibility |"],
            command=("press_button", BUTTON_PATHS["squad_open_profile"]),
            note="Partial-information player profile",
            require_state_change_from=squad_state,
        )
        if profile_state is None:
            return

        squad_return_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="squad-return",
            filename=f"{role_slug}-squad-return.png",
            expected_scene="SquadScreen",
            expected_nav="Squad",
            anchor_candidates=["Profile Confidence:", "Known:"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["profile_back"]),
            note="Return from player profile to squad",
            require_state_change_from=profile_state,
        )
        if squad_return_state is None:
            return

        fixtures_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="fixtures",
            filename=f"{role_slug}-fixtures.png",
            expected_scene="FixturesScreen",
            expected_nav="Fixtures",
            anchor_candidates=["Fixture List", "Next fixture"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["squad_fixtures"]),
            note="Fixtures screen with corrected active route",
            require_state_change_from=squad_return_state,
        )
        if fixtures_state is None:
            return

        standings_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="standings",
            filename=f"{role_slug}-standings.png",
            expected_scene="StandingsScreen",
            expected_nav="Standings",
            anchor_candidates=["League Table", "Read the table first"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["fixtures_standings"]),
            note="Standings screen with corrected active route",
            require_state_change_from=fixtures_state,
        )
        if standings_state is None:
            return

        matchday_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="matchday",
            filename=f"{role_slug}-matchday.png",
            expected_scene="MatchdayScene",
            anchor_candidates=["Watch Live Match", "Match Plan"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["standings_matchday"]),
            note="Matchday handoff before live playback",
            require_state_change_from=standings_state,
        )
        if matchday_state is None:
            return

        live_start_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="live-match-kickoff",
            filename=f"{role_slug}-live-match-kickoff.png",
            expected_scene="LiveMatchScene",
            anchor_candidates=["00'", "01'", "Kickoff", "Kick-off", " vs ", "0 - 0", "Playback model"],
            command=("press_button", BUTTON_PATHS["matchday_start_live"]),
            note="Timed screenshot sequence: kickoff",
            require_state_change_from=matchday_state,
            timeout_s=60.0,
        )
        if live_start_state is None:
            return

        changed_state = wait_for_state_change(live_start_state, timeout_s=20.0)
        if changed_state is None:
            run.errors.append("manager:live-match-mid did not change state before timeout")
            return

        capture_from_known_state(
            run,
            window,
            role_slug=role_slug,
            label="live-match-mid",
            filename=f"{role_slug}-live-match-mid.png",
            state=changed_state,
            expected_screen="LiveMatchScene",
            visible_anchor=changed_state.anchors[0] if changed_state.anchors else "LiveMatchScene",
            note="Timed screenshot sequence: live-match pacing sample",
        )

        full_time_state, _ = wait_for_ui_state(
            expected_scene="LiveMatchScene",
            anchor_candidates=["Continue to Post-Match", "FT", "Full time", "Playback complete"],
            timeout_s=120.0,
            newer_than=live_start_state.timestamp_utc,
        )
        if full_time_state is None:
            run.errors.append("manager:live-match-full-time did not reach FT before timeout")
            return

        capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="live-match-full-time",
            filename=f"{role_slug}-live-match-full-time.png",
            expected_scene="LiveMatchScene",
            anchor_candidates=["Continue to Post-Match", "FT", "Full time", "Playback complete"],
            note="Timed screenshot sequence: full-time state",
            require_state_change_from=live_start_state,
        )

    finally:
        if proc is not None and proc.poll() is None:
            terminate_process_tree(proc)


def capture_manager_post_match_review(run: PlaytestRun, godot_console: Path) -> None:
    role_slug = "manager"
    role_label = "Manager"
    seed = 903103

    run_prepare_slot(godot_console, role_label, seed)
    clear_runtime_state_files()

    proc: subprocess.Popen[str] | None = None
    try:
        proc = launch_gui_game(godot_console)
        window = focus_game_window(proc)

        main_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="post-match-main-menu",
            filename=f"{role_slug}-post-match-main-menu.png",
            expected_scene="MainMenu",
            anchor_candidates=[role_label],
            note="Manager slot before deterministic post-match capture",
            timeout_s=30.0,
        )
        if main_state is None:
            return

        save_load_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="post-match-save-load",
            filename=f"{role_slug}-post-match-save-load.png",
            expected_scene="SaveLoadScene",
            anchor_candidates=[role_label],
            command=("press_button", BUTTON_PATHS["mainmenu_load"]),
            note="Save/load before deterministic post-match capture",
            require_state_change_from=main_state,
        )
        if save_load_state is None:
            return

        dashboard_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="post-match-dashboard",
            filename=f"{role_slug}-post-match-dashboard.png",
            expected_scene="ClubDashboard",
            expected_nav="Dashboard",
            anchor_candidates=["Manager", "Next best action"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["saveload_load"]),
            note="Dashboard before direct post-match capture",
            require_state_change_from=save_load_state,
        )
        if dashboard_state is None:
            return

        matchday_state = capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="post-match-matchday",
            filename=f"{role_slug}-post-match-matchday.png",
            expected_scene="MatchdayScene",
            anchor_candidates=["Watch Live Match", "Instant Result"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["dashboard_matchday"]),
            note="Matchday before deterministic post-match capture",
            require_state_change_from=dashboard_state,
        )
        if matchday_state is None:
            return

        capture_verified_step(
            run,
            window,
            role_slug=role_slug,
            label="post-match",
            filename=f"{role_slug}-post-match.png",
            expected_scene="PostMatchScene",
            anchor_candidates=["Key stats", "Next action", "Tactical review"],
            role_anchor=role_label,
            command=("press_button", BUTTON_PATHS["matchday_instant_result"]),
            note="Corrected post-match screen from the shared instant-result path",
            require_state_change_from=matchday_state,
            timeout_s=45.0,
        )
    finally:
        if proc is not None and proc.poll() is None:
            terminate_process_tree(proc)


def build_validation_payload(run: PlaytestRun) -> dict:
    return {
        "started_at": run.started_at,
        "headless_pass": run.headless_pass,
        "headless_log": run.headless_log,
        "old_screenshot_count": run.old_screenshot_count,
        "old_unique_hash_count": run.old_unique_hash_count,
        "new_screenshot_count": run.new_screenshot_count,
        "new_unique_hash_count": run.new_unique_hash_count,
        "archive_path": run.archive_path,
        "video_capture_skipped_reason": run.video_capture_skipped_reason,
        "duplicate_hash_groups": run.duplicate_hash_groups,
        "errors": run.errors,
        "assertions": [asdict(row) for row in run.assertions],
        "captures": [asdict(observation) for observation in run.observations],
    }


def write_capture_report(run: PlaytestRun) -> None:
    lines = [
        "# Touchline Screenshot Capture Report",
        "",
        f"- Run started: `{run.started_at}`",
        f"- Headless active playtest: `{'PASS' if run.headless_pass else 'FAIL'}`",
        f"- Old screenshot count: `{run.old_screenshot_count}`",
        f"- Old unique hash count: `{run.old_unique_hash_count}`",
        f"- New screenshot count: `{run.new_screenshot_count}`",
        f"- New unique hash count: `{run.new_unique_hash_count}`",
        f"- Archived prior screenshots: `{run.archive_path or 'none'}`",
        f"- Video capture: skipped. {run.video_capture_skipped_reason}",
        "",
        "## Duplicate Hash Review",
    ]
    if run.duplicate_hash_groups:
        for group in run.duplicate_hash_groups:
            lines.append(
                f"- Hash `{group['hash'][:12]}` repeated `{group['count']}` times across `{', '.join(group['labels'])}`"
            )
    else:
        lines.append("- No duplicate hash groups were found in the new capture set.")

    lines.extend(
        [
            "",
            "## Capture Results",
            "",
            "| Role | Label | Expected screen | Actual screen | Nav | Anchor | Pass | File |",
            "|---|---|---|---|---|---|---|---|",
        ]
    )
    for observation in run.observations:
        file_ref = observation.screenshot or "not saved"
        lines.append(
            f"| {observation.role} | {observation.label} | {observation.expected_screen} | {observation.actual_detected_screen or '--'} | "
            f"{observation.actual_nav or '--'} | {observation.visible_anchor_used or '--'} | "
            f"{'PASS' if observation.passed else 'FAIL'} | `{file_ref}` |"
        )

    if run.errors:
        lines.extend(["", "## Errors"])
        lines.extend([f"- {error}" for error in run.errors])

    SCREENSHOT_REPORT_PATH.write_text("\n".join(lines) + "\n", encoding="utf-8")


def write_run_summary(run: PlaytestRun, timestamp: str) -> None:
    summary_path = LOG_DIR / RUN_SUMMARY_PATTERN.format(ts=timestamp)
    summary_path.write_text(json.dumps(build_validation_payload(run), indent=2), encoding="utf-8")
    log(f"Wrote run summary: {summary_path}")


def is_ffmpeg_available() -> bool:
    return shutil.which("ffmpeg") is not None


def main() -> int:
    parser = argparse.ArgumentParser(description="Touchline active desktop playtest")
    parser.add_argument("--skip-gui", action="store_true", help="Only run headless validation")
    parser.add_argument("--skip-headless", action="store_true", help="Only run GUI screenshots")
    args = parser.parse_args()

    SCREENSHOT_DIR.mkdir(parents=True, exist_ok=True)
    LOG_DIR.mkdir(parents=True, exist_ok=True)
    VIDEO_DIR.mkdir(parents=True, exist_ok=True)
    ARCHIVE_ROOT.mkdir(parents=True, exist_ok=True)

    run = PlaytestRun()
    godot_console = find_godot_console()
    timestamp = datetime.now().strftime("%Y%m%d-%H%M%S")
    headless_log = LOG_DIR / f"headless-active-playtest-{timestamp}.log"

    run.old_screenshot_count, run.old_unique_hash_count, run.archive_path = archive_existing_screenshots(timestamp)
    clear_runtime_state_files()
    run.video_capture_skipped_reason = (
        "No local ffmpeg binary is available, so the harness captured a verified timed screenshot sequence "
        "for Live Match to Post-Match instead of a video."
        if not is_ffmpeg_available()
        else "Video was still skipped in this run; timed verified screenshots were used instead."
    )

    if not args.skip_headless:
        try:
            passed, assertions = run_headless_check(godot_console, headless_log)
            run.headless_pass = passed
            run.assertions = assertions
            run.headless_log = str(headless_log.relative_to(REPO_ROOT))
            if not passed:
                run.errors.append("Headless active_playtest_user_flow_check failed")
        except Exception as exc:  # pragma: no cover - environment-dependent
            run.headless_pass = False
            run.errors.append(f"Headless check error: {exc}")

    if not args.skip_gui:
        for role_slug, role_label, seed in ROLES:
            if role_slug == "manager":
                try:
                    capture_manager_club_selection(run, godot_console)
                except Exception as exc:  # pragma: no cover - environment-dependent
                    run.errors.append(f"GUI club-selection capture error: {exc}")
            try:
                capture_role_flow(run, godot_console, role_slug, role_label, seed)
            except Exception as exc:  # pragma: no cover - environment-dependent
                run.errors.append(f"GUI playtest error for {role_slug}: {exc}")
        try:
            capture_manager_post_match_review(run, godot_console)
        except Exception as exc:  # pragma: no cover - environment-dependent
            run.errors.append(f"GUI post-match review capture error: {exc}")

    new_files = sorted(SCREENSHOT_DIR.glob("*.png"))
    run.new_screenshot_count = len(new_files)
    new_hashes = [sha256_file(path) for path in new_files]
    run.new_unique_hash_count = len(set(new_hashes))
    run.duplicate_hash_groups = compute_duplicate_groups(run.observations)

    cross_screen_duplicates = [
        group for group in run.duplicate_hash_groups if len(set(group["screens"])) > 1
    ]
    if cross_screen_duplicates:
        run.errors.append("Duplicate screenshot hashes were reused across different expected screens.")

    VALIDATION_PATH.write_text(json.dumps(build_validation_payload(run), indent=2), encoding="utf-8")
    write_capture_report(run)
    write_run_summary(run, timestamp)

    if run.headless_pass is False:
        return 1
    if any(not observation.passed for observation in run.observations):
        return 1
    if run.errors:
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
