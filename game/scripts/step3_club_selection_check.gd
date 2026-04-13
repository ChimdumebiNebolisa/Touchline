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

        var name_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SeedInput") as SpinBox
        var start_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton") as Button

        if name_input == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls are missing")
            return false

        name_input.text = "Morgan Vale"
        seed_input.value = 314159
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("Career handoff did not open ChooseClub")
            return false

        var club_rows := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ClubScroll/ClubRows") as VBoxContainer
        var confirm_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton") as Button

        if club_rows == null or confirm_button == null:
            _fail("ChooseClub list controls are missing")
            return false

        if club_rows.get_child_count() < 2:
            _fail("Expected seeded clubs to be populated")
            return false

        current_scene.call("SelectClubRow", 1)
        confirm_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Club confirmation did not open ClubDashboard")
            return false

        var game_state := root.get_node("GameState")
        if game_state == null:
            _fail("GameState singleton was not autoloaded")
            return false

        if str(game_state.SelectedClubName) != "Northbridge City":
            _fail("SelectedClubName was not persisted correctly")
            return false

        var dashboard_label := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel") as Label
        if dashboard_label == null or dashboard_label.text.find("Northbridge City") == -1:
            _fail("ClubDashboard context did not include selected club")
            return false

        print("STEP3_SUBTASK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP3_SUBTASK_FAIL: " + message)
    quit(1)
