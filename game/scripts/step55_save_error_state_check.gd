extends SceneTree

const SAVE_PATH := "user://slot-1.json"

var _stage := 0
var _ticks := 0
var _had_original_save := false
var _original_save_text := ""
var _selected_club := "Northbridge City"

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_save_error_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_empty_save_load_state()
    elif _stage == 2 and _ticks > 2:
        _validate_main_menu_preview()
    elif _stage == 3 and _ticks > 2:
        _validate_save_load_preview_and_load()
    elif _stage == 4 and _ticks > 2:
        _validate_loaded_dashboard()

    return false

func _start_save_error_flow() -> void:
    _backup_existing_save()
    _delete_save()

    var err := change_scene_to_file("res://scenes/SaveLoadScene.tscn")
    if err != OK:
        _fail("Could not open SaveLoadScene")
        return

    _stage = 1
    _ticks = 0

func _validate_empty_save_load_state() -> void:
    if current_scene == null or current_scene.name != "SaveLoadScene":
        _fail("SaveLoadScene did not load for empty slot")
        return

    var slot_summary := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/SlotSummaryLabel")
    var status := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/StatusLabel")
    var load_button := current_scene.get_node("RootMargin/MainColumn/ActionsRow/LoadButton") as Button

    if slot_summary.find("Slot 1 unavailable") == -1 or slot_summary.find("No local save found") == -1:
        _fail("Empty save slot summary is unclear: %s" % slot_summary)
        return

    if status.find("Load is disabled") == -1 or load_button == null or not load_button.disabled:
        _fail("Empty save slot does not disable load clearly")
        return

    var game_state := root.get_node("GameState")
    var save_system := root.get_node("SaveSystem")
    if game_state == null or save_system == null:
        _fail("Save test autoloads are missing")
        return

    game_state.StartNewCareer("Save Error Check", 555560)
    game_state.SelectClub(_selected_club)

    _write_raw_save("{ broken json")
    if save_system.TryLoadGame():
        _fail("Corrupt save unexpectedly loaded")
        return
    _assert_load_failure("corrupt")
    _assert_runtime_unchanged(game_state)

    _write_save_json({
        "saveVersion": 99,
        "careerInitialized": true,
        "selectedClubName": _selected_club
    })
    if save_system.TryLoadGame():
        _fail("Future-version save unexpectedly loaded")
        return
    _assert_load_failure("newer build")
    _assert_runtime_unchanged(game_state)

    _write_save_json({
        "saveVersion": 2,
        "managerName": "Incomplete Manager",
        "careerInitialized": true,
        "availableClubs": ["Riverton Athletic", "Northbridge City", "Harbor County", "Eastvale Rovers"],
        "selectedClubName": _selected_club,
        "currentMatchday": 1,
        "currentDateIso": "2026-08-03",
        "seasonStartYear": 2026,
        "squadPlayers": [
            { "name": "Test Player", "position": "GK", "age": 24, "form": 70, "morale": 70, "fitness": 90, "isStarting": true }
        ]
    })
    if save_system.TryLoadGame():
        _fail("Incomplete current save unexpectedly loaded")
        return
    _assert_load_failure("incomplete")
    _assert_runtime_unchanged(game_state)

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return

    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("Could not open MainMenu for save preview")
        return

    _stage = 2
    _ticks = 0

func _validate_main_menu_preview() -> void:
    if current_scene == null or current_scene.name != "MainMenu":
        _fail("MainMenu did not load for save preview")
        return

    var summary := _label_text("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeSummaryLabel")
    var status := _label_text("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeStatusLabel")
    var season := _label_text("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/SeasonRow/SeasonValueLabel")

    if summary.find("Slot 1 ready") == -1 or status.find("Local career ready") == -1:
        _fail("MainMenu save preview does not clearly show a loadable slot")
        return

    if season.find("Matchday") == -1:
        _fail("MainMenu save preview does not include matchday context: %s" % season)
        return

    var err := change_scene_to_file("res://scenes/SaveLoadScene.tscn")
    if err != OK:
        _fail("Could not reopen SaveLoadScene for valid preview")
        return

    _stage = 3
    _ticks = 0

func _validate_save_load_preview_and_load() -> void:
    if current_scene == null or current_scene.name != "SaveLoadScene":
        _fail("SaveLoadScene did not load for valid slot")
        return

    var slot_summary := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/SlotSummaryLabel")
    var status := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/StatusLabel")
    var season := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SeasonRow/SeasonValueLabel")
    var save_version := _label_text("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SaveRow/SaveValueLabel")
    var load_button := current_scene.get_node("RootMargin/MainColumn/ActionsRow/LoadButton") as Button

    if slot_summary.find("Slot 1 ready") == -1 or slot_summary.find(_selected_club) == -1:
        _fail("SaveLoad valid slot summary is unclear: %s" % slot_summary)
        return

    if status.find("Career summary") == -1 or status.find("pts") == -1 or season.find("Matchday") == -1 or save_version.find("Save v") == -1:
        _fail("SaveLoad valid slot details are incomplete")
        return

    if load_button == null or load_button.disabled:
        _fail("Load button is disabled for a valid save")
        return

    load_button.emit_signal("pressed")
    _stage = 4
    _ticks = 0

func _validate_loaded_dashboard() -> void:
    if current_scene == null or current_scene.name != "ClubDashboard":
        _fail("Valid save did not load into ClubDashboard")
        return

    var game_state := root.get_node("GameState")
    if game_state == null or str(game_state.SelectedClubName) != _selected_club:
        _fail("Loaded save did not restore the selected club")
        return

    _restore_existing_save()
    print("STEP55_SAVE_ERROR_STATE_PASS")
    quit()

func _assert_load_failure(expected_text: String) -> void:
    var save_system := root.get_node("SaveSystem")
    var status := str(save_system.LastStatusMessage).to_lower()
    if status.find(expected_text.to_lower()) == -1:
        _fail("Expected load failure containing '%s', got '%s'" % [expected_text, save_system.LastStatusMessage])

func _assert_runtime_unchanged(game_state: Node) -> void:
    if str(game_state.SelectedClubName) != _selected_club:
        _fail("Failed load changed the active runtime club")

func _backup_existing_save() -> void:
    if not FileAccess.file_exists(SAVE_PATH):
        return

    var save_file := FileAccess.open(SAVE_PATH, FileAccess.READ)
    if save_file == null:
        return

    _had_original_save = true
    _original_save_text = save_file.get_as_text()
    save_file.close()

func _restore_existing_save() -> void:
    if _had_original_save:
        _write_raw_save(_original_save_text)
    else:
        _delete_save()

func _delete_save() -> void:
    if FileAccess.file_exists(SAVE_PATH):
        DirAccess.remove_absolute(ProjectSettings.globalize_path(SAVE_PATH))

func _write_raw_save(content: String) -> void:
    var save_file := FileAccess.open(SAVE_PATH, FileAccess.WRITE)
    if save_file == null:
        _fail("Could not write test save data")
        return

    save_file.store_string(content)
    save_file.close()

func _write_save_json(payload: Dictionary) -> void:
    _write_raw_save(JSON.stringify(payload, "\t"))

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
        return ""

    return node.text

func _fail(message: String) -> void:
    _restore_existing_save()
    push_error(message)
    print("STEP55_SAVE_ERROR_STATE_FAIL: " + message)
    quit(1)
