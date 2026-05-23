extends SceneTree

var _stage := 0
var _ticks := 0
var _total_ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("unable to load CareerSetup")

func _process(_delta: float) -> bool:
    _ticks += 1
    _total_ticks += 1

    if _total_ticks > 600:
        _fail("Step 26 flow timed out at stage %d" % _stage)
        return false

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "CareerSetup":
            _fail("CareerSetup scene did not load")
            return false

        var manager_name := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SeedInput") as SpinBox
        var start_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton") as Button
        if manager_name == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls missing")
            return false

        manager_name.text = "Seed Data Check"
        seed_input.value = 424242
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub scene did not load after career setup")
            return false

        var club_rows := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ClubScroll/ClubRows") as VBoxContainer
        if club_rows == null:
            _fail("ChooseClub controls missing")
            return false

        if club_rows.get_child_count() < 4:
            _fail("Seed data did not populate the expected club list")
            return false

        current_scene.call("SelectClubRow", 3)
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub scene did not stay active long enough for preview refresh")
            return false

        var identity_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/IdentityLabel") as Label
        var expectation_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/ExpectationLabel") as Label
        var confirm_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton") as Button
        if identity_label == null or expectation_label == null or confirm_button == null:
            _fail("ChooseClub preview controls missing")
            return false

        if identity_label.text.find("brave wide play") == -1:
            _fail("ChooseClub preview did not load identity text from world-seed.json: %s" % identity_label.text)
            return false

        if expectation_label.text.find("make the season feel upward") == -1:
            _fail("ChooseClub preview did not load expectation text from world-seed.json: %s" % expectation_label.text)
            return false

        confirm_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load after club selection")
            return false

        var club_context := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel") as Label
        var squad_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/SquadButton") as Button
        if club_context == null or squad_button == null:
            _fail("Dashboard controls missing")
            return false

        if club_context.text.find("Eastvale Rovers") == -1:
            _fail("Dashboard did not reflect the seeded club selection")
            return false

        squad_button.emit_signal("pressed")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "SquadScreen":
            _fail("SquadScreen did not load from the dashboard")
            return false

        var player_rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows") as VBoxContainer
        var club_context := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel") as Label
        if player_rows == null or club_context == null:
            _fail("SquadScreen controls missing")
            return false

        if club_context.text.find("Eastvale Rovers") == -1:
            _fail("SquadScreen did not inherit the seeded club context")
            return false

        if player_rows.get_child_count() == 0:
            _fail("SquadScreen did not receive seeded players")
            return false

        var selected_player_label := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PlayerNameLabel") as Label
        if selected_player_label == null or selected_player_label.text.find("Riku Tanaka") == -1:
            _fail("SquadScreen did not render seeded player names from world-seed.json")
            return false

        print("STEP26_SEED_DATA_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP26_SEED_DATA_FAIL: " + message)
    quit(1)
