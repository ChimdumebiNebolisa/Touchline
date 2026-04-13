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

        name_input.text = "Avery Quinn"
        seed_input.value = 13579
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub did not load")
            return false

        var club_rows := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ClubScroll/ClubRows") as VBoxContainer
        var confirm_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton") as Button

        if club_rows == null or confirm_button == null:
            _fail("ChooseClub controls are missing")
            return false

        current_scene.call("SelectClubRow", 0)
        confirm_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load")
            return false

        var squad_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/SquadButton") as Button
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

        var filter := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/FilterRow/PositionFilter") as OptionButton
        var player_rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows") as VBoxContainer
        var name_label := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PlayerNameLabel") as Label
        var detail_label := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/DetailMetaLabel") as Label

        if filter == null or player_rows == null or name_label == null or detail_label == null:
            _fail("SquadScreen controls are missing")
            return false

        if player_rows.get_child_count() < 5:
            _fail("Expected named players list to be populated")
            return false

        var first_row := player_rows.get_child(0) as PanelContainer
        if first_row == null:
            _fail("Expected a structured player row")
            return false

        var first_name := first_row.get_node("RowPadding/RowContent/Body/NameLabel") as Label
        var first_meta := first_row.get_node("RowPadding/RowContent/Body/MetaLabel") as Label
        if first_name == null or first_meta == null:
            _fail("Structured player row labels are missing")
            return false

        if first_name.text.find("Player ") != -1:
            _fail("Placeholder player identity detected")
            return false

        var first_state_chip := first_row.get_child(0)
        if first_state_chip == null:
            _fail("Lineup chip missing from squad row")
            return false

        if first_name.text.find("Mateo Silva") == -1:
            _fail("Expected seeded named player not found")
            return false

        if name_label.text.find("Mateo Silva") == -1 or detail_label.text.find("Age") == -1 or detail_label.text.find("XI") == -1:
            _fail("Player detail label missing expected stats")
            return false

        filter.select(1)
        filter.emit_signal("item_selected", 1)

        if player_rows.get_child_count() < 2:
            _fail("Goalkeeper filter did not return expected entries")
            return false

        var filtered_row := player_rows.get_child(0) as PanelContainer
        var filtered_meta := filtered_row.get_node("RowPadding/RowContent/Body/MetaLabel") as Label
        if filtered_meta == null or filtered_meta.text.find("GK") == -1:
            _fail("Goalkeeper filter did not narrow by position")
            return false

        print("STEP5_SUBTASK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP5_SUBTASK_FAIL: " + message)
    quit(1)
