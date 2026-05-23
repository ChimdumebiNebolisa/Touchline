extends SceneTree

var _stage := 0
var _ticks := 0
var _total_ticks := 0
var _saved_opponent := ""

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("Could not open CareerSetup")

func _process(_delta: float) -> bool:
    _ticks += 1
    _total_ticks += 1

    if _total_ticks > 900:
        _fail("Step 50 flow timed out at stage %d" % _stage)
        return false

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "CareerSetup":
            _fail("CareerSetup did not load")
            return false

        var manager_name := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SeedInput") as SpinBox
        var start_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton") as Button
        if manager_name == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls missing")
            return false

        manager_name.text = "Flow Check"
        seed_input.value = 505050
        start_button.emit_signal("pressed")
        _advance_stage()

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub did not load")
            return false

        var confirm_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton") as Button
        if confirm_button == null:
            _fail("ChooseClub confirm button missing")
            return false

        current_scene.call("SelectClubRow", 0)
        confirm_button.emit_signal("pressed")
        _advance_stage()

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load")
            return false

        var header_status := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel") as Label
        var pressure_reasons := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureReasonsLabel") as Label
        var matchday_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/MatchdayButton") as Button
        if header_status == null or pressure_reasons == null or matchday_button == null:
            _fail("Dashboard context controls missing")
            return false

        if header_status.text.find("New season") == -1 and header_status.text.find("Ready for matchday") == -1:
            _fail("Dashboard context did not render before matchday: %s" % header_status.text)
            return false

        if pressure_reasons.text.find("Board:") == -1:
            _fail("Dashboard pressure context missing before matchday")
            return false

        matchday_button.emit_signal("pressed")
        _advance_stage()

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load")
            return false

        var status_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StatusLabel") as Label
        var instant_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton") as Button
        if status_label == null or instant_button == null:
            _fail("Matchday action controls missing")
            return false

        if status_label.text.find("Live Match") == -1 or status_label.text.find("Instant Result") == -1:
            _fail("Matchday action choice did not render clearly")
            return false

        instant_button.emit_signal("pressed")
        _advance_stage()

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("PostMatchScene did not load after instant result")
            return false

        var score_label := current_scene.get_node("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ScoreLabel") as Label
        var continue_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ContinueButton") as Button
        if score_label == null or continue_button == null:
            _fail("PostMatch controls missing")
            return false

        if score_label.text.find("-") == -1:
            _fail("PostMatch score did not render")
            return false

        continue_button.emit_signal("pressed")
        _advance_stage()

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Dashboard did not reload after post-match continue")
            return false

        var game_state := root.get_node("GameState")
        var save_system := root.get_node("SaveSystem")
        if game_state == null or save_system == null:
            _fail("GameState or SaveSystem missing for context save/load")
            return false

        var date_label := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/DateLabel") as Label
        var next_match_meta := current_scene.get_node("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel") as Label
        if date_label == null or next_match_meta == null:
            _fail("Post-continue dashboard context missing")
            return false

        if date_label.text.find("Matchday 2") == -1 or next_match_meta.text.find("Next fixture") == -1:
            _fail("Dashboard did not show coherent post-match next context")
            return false

        _saved_opponent = String(game_state.CurrentOpponentName)
        if not save_system.TrySaveGame():
            _fail(save_system.LastStatusMessage)
            return false

        var world_generator := root.get_node("WorldGenerator")
        if world_generator == null:
            _fail("WorldGenerator missing for save/load mutation")
            return false

        if not world_generator.BeginNewCareer("Flow Mutation", 505151):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Harbor County"):
            _fail(world_generator.LastStatusMessage)
            return false

        if not save_system.TryLoadGame():
            _fail(save_system.LastStatusMessage)
            return false

        var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
        if err != OK:
            _fail("Could not reload ClubDashboard after save/load")
            return false

        _advance_stage()

    elif _stage == 6 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Dashboard did not reload after save/load")
            return false

        var game_state := root.get_node("GameState")
        var date_label := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/DateLabel") as Label
        var status_label := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/StatusLabel") as Label
        if game_state == null or date_label == null or status_label == null:
            _fail("Loaded dashboard context controls missing")
            return false

        if game_state.ManagerName != "Flow Check":
            _fail("Save/load did not preserve manager identity")
            return false

        if String(game_state.CurrentOpponentName) != _saved_opponent:
            _fail("Save/load did not preserve next opponent context")
            return false

        if date_label.text.find("Season") == -1 or status_label.text.find("Opponent context") == -1:
            _fail("Loaded dashboard did not render manager-facing context")
            return false

        print("STEP50_END_TO_END_USER_FLOW_PASS")
        quit()

    return false

func _advance_stage() -> void:
    _stage += 1
    _ticks = 0

func _fail(message: String) -> void:
    push_error(message)
    print("STEP50_END_TO_END_USER_FLOW_FAIL: " + message)
    quit(1)
