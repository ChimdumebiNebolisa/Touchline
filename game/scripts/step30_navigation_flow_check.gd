extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    pass

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        var save_system := root.get_node("SaveSystem")
        var world_generator := root.get_node("WorldGenerator")
        var game_state := root.get_node("GameState")
        if save_system == null or world_generator == null or game_state == null:
            _fail("Required autoloads are missing for the Step 30 navigation check")
            return false

        if not world_generator.BeginNewCareer("Navigation Check", 414141):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Northbridge City"):
            _fail(world_generator.LastStatusMessage)
            return false

        if not save_system.TrySaveGame():
            _fail(save_system.LastStatusMessage)
            return false

        var err := change_scene_to_file("res://scenes/MainMenu.tscn")
        if err != OK:
            _fail("Could not open MainMenu for the Step 30 navigation check")
            return false

        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("MainMenu did not load for the Step 30 navigation check")
            return false

        var load_button := current_scene.get_node("Center/MenuCard/Padding/Menu/LoadGameButton") as Button
        if load_button == null or load_button.disabled:
            _fail("MainMenu load button is unavailable for the Step 30 navigation check")
            return false

        load_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "SaveLoadScene":
            _fail("SaveLoadScene did not load from MainMenu")
            return false

        var back_button := current_scene.get_node("Center/Panel/Padding/Content/BackButton") as Button
        var heading := current_scene.get_node("Center/Panel/Padding/Content/Heading") as Label
        if back_button == null or heading == null:
            _fail("SaveLoadScene controls are missing")
            return false

        if heading.text != "Load Game":
            _fail("SaveLoadScene heading does not stay aligned with the main menu load flow")
            return false

        if back_button.text != "Back to Main Menu":
            _fail("SaveLoadScene back button does not return to MainMenu")
            return false

        back_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("SaveLoadScene back flow did not return to MainMenu")
            return false

        var load_button := current_scene.get_node("Center/MenuCard/Padding/Menu/LoadGameButton") as Button
        if load_button == null or load_button.disabled:
            _fail("MainMenu load button is unavailable after returning from SaveLoadScene")
            return false

        load_button.emit_signal("pressed")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "SaveLoadScene":
            _fail("SaveLoadScene did not reopen for resume flow")
            return false

        var load_button := current_scene.get_node("Center/Panel/Padding/Content/LoadButton") as Button
        if load_button == null or load_button.disabled:
            _fail("SaveLoadScene load button is unavailable for resume flow")
            return false

        load_button.emit_signal("pressed")
        _stage = 5
        _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load from SaveLoadScene")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if back_button == null or matchday_button == null:
            _fail("ClubDashboard navigation controls are missing")
            return false

        if back_button.text != "Return to Main Menu":
            _fail("ClubDashboard back button does not return to the main menu")
            return false

        matchday_button.emit_signal("pressed")
        _stage = 6
        _ticks = 0

    elif _stage == 6 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load from ClubDashboard")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/BackButton") as Button
        if back_button == null:
            _fail("MatchdayScene back button is missing")
            return false

        back_button.emit_signal("pressed")
        _stage = 7
        _ticks = 0

    elif _stage == 7 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Matchday back navigation did not return to ClubDashboard")
            return false

        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if matchday_button == null:
            _fail("ClubDashboard matchday button is missing after returning from MatchdayScene")
            return false

        matchday_button.emit_signal("pressed")
        _stage = 8
        _ticks = 0

    elif _stage == 8 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not reopen for instant result flow")
            return false

        var instant_result_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/InstantResultButton") as Button
        if instant_result_button == null or instant_result_button.disabled:
            _fail("Instant result button is unavailable for the Step 30 navigation check")
            return false

        instant_result_button.emit_signal("pressed")
        _stage = 9
        _ticks = 0

    elif _stage == 9 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("PostMatchScene did not load after instant result")
            return false

        var continue_button := current_scene.get_node("Center/Shell/Padding/Content/ContinueButton") as Button
        if continue_button == null:
            _fail("PostMatchScene continue button is missing")
            return false

        continue_button.emit_signal("pressed")
        _stage = 10
        _ticks = 0

    elif _stage == 10 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("PostMatchScene continue flow did not return to ClubDashboard")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if back_button == null:
            _fail("ClubDashboard back button is missing after post-match continue")
            return false

        back_button.emit_signal("pressed")
        _stage = 11
        _ticks = 0

    elif _stage == 11 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("ClubDashboard exit did not return to MainMenu")
            return false

        print("STEP30_NAVIGATION_FLOW_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP30_NAVIGATION_FLOW_FAIL: " + message)
    quit(1)
