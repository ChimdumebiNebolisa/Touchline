extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("unable to load MainMenu scene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        var menu := current_scene
        if menu == null:
            _fail("MainMenu scene did not load")
            return false

        var new_career_button := menu.get_node("Center/Menu/NewCareerButton") as Button
        if new_career_button == null:
            _fail("NewCareerButton not found")
            return false

        new_career_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "CareerSetup":
            _fail("New Career navigation did not open CareerSetup")
            return false

        var back_button := current_scene.get_node("Center/Panel/BackButton") as Button
        if back_button == null:
            _fail("CareerSetup back button not found")
            return false

        back_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("Back navigation did not return to MainMenu")
            return false

        var load_game_button := current_scene.get_node("Center/Menu/LoadGameButton") as Button
        if load_game_button == null:
            _fail("LoadGameButton not found")
            return false

        load_game_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "SaveLoadScene":
            _fail("Load Game navigation did not open SaveLoadScene")
            return false

        print("STEP1_CHECK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP1_CHECK_FAIL: " + message)
    quit(1)
