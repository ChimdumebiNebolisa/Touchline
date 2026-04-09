extends SceneTree

var _stage := 0
var _ticks := 0
var _total_ticks := 0

func _initialize() -> void:
    var world_generator := root.get_node("WorldGenerator")
    var calendar_system := root.get_node("CalendarSystem")
    if world_generator == null:
        _fail("WorldGenerator autoload missing")
        return

    if calendar_system == null:
        _fail("CalendarSystem autoload missing")
        return

    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("unable to load CareerSetup")

func _process(_delta: float) -> bool:
    _ticks += 1
    _total_ticks += 1

    if _total_ticks > 600:
        _fail("Step 25 flow timed out at stage %d" % _stage)
        return false

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "CareerSetup":
            _fail("CareerSetup scene did not load")
            return false

        var manager_name := current_scene.get_node("Center/Panel/Padding/Content/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("Center/Panel/Padding/Content/SeedInput") as SpinBox
        var start_button := current_scene.get_node("Center/Panel/Padding/Content/StartCareerButton") as Button
        if manager_name == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls missing")
            return false

        manager_name.text = "Autoload Check"
        seed_input.value = 778899
        start_button.emit_signal("pressed")
        print("STEP25_STAGE_0_OK")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub scene did not load after career setup")
            return false

        var game_state := root.get_node("GameState")
        if game_state == null or not game_state.CareerInitialized:
            _fail("Career bootstrap did not populate GameState")
            return false

        var confirm_button := current_scene.get_node("Center/Panel/Padding/Content/ConfirmSelectionButton") as Button
        var club_list := current_scene.get_node("Center/Panel/Padding/Content/ClubList") as ItemList
        if confirm_button == null or club_list == null:
            _fail("ChooseClub controls missing")
            return false

        if club_list.item_count == 0:
            _fail("ChooseClub did not receive seeded clubs")
            return false

        club_list.select(0)
        confirm_button.emit_signal("pressed")
        print("STEP25_STAGE_1_OK")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load after club selection")
            return false

        var game_state := root.get_node("GameState")
        if game_state == null or String(game_state.SelectedClubName).is_empty():
            _fail("WorldGenerator did not seed the selected club")
            return false

        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if matchday_button == null:
            _fail("Dashboard matchday button missing")
            return false

        matchday_button.emit_signal("pressed")
        print("STEP25_STAGE_2_OK")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load from the dashboard")
            return false

        var instant_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/InstantResultButton") as Button
        if instant_button == null:
            _fail("Matchday instant-result button missing")
            return false

        instant_button.emit_signal("pressed")
        print("STEP25_STAGE_3_OK")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("PostMatchScene did not load")
            return false

        var continue_button := current_scene.get_node("Center/Shell/Padding/Content/ContinueButton") as Button
        if continue_button == null:
            _fail("PostMatchScene continue button missing")
            return false

        continue_button.emit_signal("pressed")
        print("STEP25_STAGE_4_OK")
        _stage = 5
        _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Continue flow did not return to ClubDashboard")
            return false

        var date_label := current_scene.get_node("Center/Shell/Padding/Content/Header/DateLabel") as Label
        if date_label == null:
            _fail("Dashboard date label missing after continue flow")
            return false

        if date_label.text.find("10 Aug 2026") == -1:
            _fail("CalendarSystem did not advance the date through the post-match continue flow")
            return false

        print("STEP25_AUTOLOAD_FLOW_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP25_AUTOLOAD_FLOW_FAIL: " + message)
    quit(1)
