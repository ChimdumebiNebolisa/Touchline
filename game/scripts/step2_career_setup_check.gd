extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("unable to load CareerSetup scene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        if current_scene == null:
            _fail("CareerSetup scene did not load")
            return false

        var name_input := current_scene.get_node("Center/Panel/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("Center/Panel/SeedInput") as SpinBox
        var start_button := current_scene.get_node("Center/Panel/StartCareerButton") as Button

        if name_input == null or seed_input == null or start_button == null:
            _fail("CareerSetup profile controls are missing")
            return false

        name_input.text = "Casey Doyle"
        seed_input.value = 424242
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("Start Career did not hand off to ChooseClub")
            return false

        var game_state := root.get_node("GameState")
        if game_state == null:
            _fail("GameState singleton was not autoloaded")
            return false

        if not game_state.CareerInitialized:
            _fail("Career was not initialized")
            return false

        if game_state.ManagerName != "Casey Doyle":
            _fail("ManagerName was not stored correctly")
            return false

        if int(game_state.CareerSeed) != 424242:
            _fail("CareerSeed was not stored correctly")
            return false

        if int(game_state.WorldSeed) != 424242:
            _fail("WorldSeed was not stored correctly")
            return false

        if str(game_state.CountryPackId) != "country-pack-alpha":
            _fail("CountryPackId was not stored correctly")
            return false

        var summary_label := current_scene.get_node("Center/Panel/CareerSummaryLabel") as Label
        if summary_label == null:
            _fail("ChooseClub summary label is missing")
            return false

        if summary_label.text.find("Casey Doyle") == -1:
            _fail("ChooseClub summary did not render manager context")
            return false

        if summary_label.text.find("country-pack-alpha") == -1:
            _fail("ChooseClub summary did not render world seed context")
            return false

        print("STEP2_SUBTASK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP2_SUBTASK_FAIL: " + message)
    quit(1)
