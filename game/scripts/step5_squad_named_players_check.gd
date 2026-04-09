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
            _fail("CareerSetup controls are missing")
            return false

        name_input.text = "Avery Quinn"
        seed_input.value = 13579
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub did not load")
            return false

        var club_list := current_scene.get_node("Center/Panel/ClubList") as ItemList
        var confirm_button := current_scene.get_node("Center/Panel/ConfirmSelectionButton") as Button

        if club_list == null or confirm_button == null:
            _fail("ChooseClub controls are missing")
            return false

        club_list.select(0)
        confirm_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load")
            return false

        var squad_button := current_scene.get_node("Center/Panel/SquadButton") as Button
        if squad_button == null:
            _fail("SquadButton is missing on ClubDashboard")
            return false

        squad_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "SquadScreen":
            _fail("SquadScreen did not load")
            return false

        var filter := current_scene.get_node("Center/Panel/PositionFilter") as OptionButton
        var player_list := current_scene.get_node("Center/Panel/PlayerList") as ItemList
        var detail_label := current_scene.get_node("Center/Panel/PlayerDetailLabel") as Label

        if filter == null or player_list == null or detail_label == null:
            _fail("SquadScreen controls are missing")
            return false

        if player_list.item_count < 5:
            _fail("Expected named players list to be populated")
            return false

        var first_item_text := player_list.get_item_text(0)
        if first_item_text.find("Player ") != -1:
            _fail("Placeholder player identity detected")
            return false

        if first_item_text.find("[XI]") == -1:
            _fail("Lineup marker missing from squad list")
            return false

        if first_item_text.find("Mateo Silva") == -1:
            _fail("Expected seeded named player not found")
            return false

        if detail_label.text.find("Age") == -1 or detail_label.text.find("Form") == -1 or detail_label.text.find("Morale") == -1:
            _fail("Player detail label missing expected stats")
            return false

        filter.select(1)
        filter.emit_signal("item_selected", 1)

        if player_list.item_count < 2:
            _fail("Goalkeeper filter did not return expected entries")
            return false

        var filtered_first := player_list.get_item_text(0)
        if filtered_first.find("GK") == -1:
            _fail("Goalkeeper filter did not narrow by position")
            return false

        print("STEP5_SUBTASK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP5_SUBTASK_FAIL: " + message)
    quit(1)
