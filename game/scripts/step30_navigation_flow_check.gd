extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("Could not open MainMenu for the Step 30 navigation check")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("MainMenu did not load for the Step 30 navigation check")
            return false

        var new_career_button := current_scene.get_node("Center/MenuCard/Padding/Menu/NewCareerButton") as Button
        if new_career_button == null:
            _fail("MainMenu new career button is unavailable for the Step 30 navigation check")
            return false

        new_career_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "CareerSetup":
            _fail("CareerSetup did not load from MainMenu")
            return false

        var manager_name_input := current_scene.get_node("Center/Panel/Padding/Content/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("Center/Panel/Padding/Content/SeedInput") as SpinBox
        var start_button := current_scene.get_node("Center/Panel/Padding/Content/StartCareerButton") as Button
        if manager_name_input == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls are missing")
            return false

        manager_name_input.text = "Navigation Check"
        seed_input.value = 414141
        start_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub did not load from CareerSetup")
            return false

        var club_list := current_scene.get_node("Center/Panel/Padding/Content/ClubList") as ItemList
        var identity_label := current_scene.get_node("Center/Panel/Padding/Content/PreviewCard/PreviewPadding/PreviewContent/IdentityLabel") as Label
        var confirm_button := current_scene.get_node("Center/Panel/Padding/Content/ConfirmSelectionButton") as Button
        if club_list == null or identity_label == null or confirm_button == null:
            _fail("ChooseClub controls are missing")
            return false

        if club_list.item_count == 0 or identity_label.text.find("Identity:") == -1:
            _fail("ChooseClub did not surface club preview context")
            return false

        club_list.select(0)
        confirm_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load from ChooseClub")
            return false

        var save_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/SaveButton") as Button
        var club_context := current_scene.get_node("Center/Shell/Padding/Content/Header/ClubContextLabel") as Label
        if save_button == null or club_context == null:
            _fail("ClubDashboard controls are missing after club selection")
            return false

        if club_context.text.find("Navigation Check") == -1:
            _fail("ClubDashboard did not preserve the manager identity from CareerSetup")
            return false

        save_button.emit_signal("pressed")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not remain active after saving")
            return false

        var save_hint := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/SaveHintLabel") as Label
        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if save_hint == null or back_button == null:
            _fail("ClubDashboard save confirmation controls are missing")
            return false

        if save_hint.text.find("Career saved to Slot 1") == -1:
            _fail("ClubDashboard did not surface the save confirmation after saving")
            return false

        back_button.emit_signal("pressed")
        _stage = 5
        _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("ClubDashboard exit did not return to MainMenu before load-flow coverage")
            return false

        var load_button := current_scene.get_node("Center/MenuCard/Padding/Menu/LoadGameButton") as Button
        if load_button == null or load_button.disabled:
            _fail("MainMenu load button is unavailable for the Step 30 navigation check")
            return false

        load_button.emit_signal("pressed")
        _stage = 6
        _ticks = 0

    elif _stage == 6 and _ticks > 2:
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
        _stage = 7
        _ticks = 0

    elif _stage == 7 and _ticks > 2:
        if current_scene == null or current_scene.name != "MainMenu":
            _fail("SaveLoadScene back flow did not return to MainMenu")
            return false

        var load_button := current_scene.get_node("Center/MenuCard/Padding/Menu/LoadGameButton") as Button
        if load_button == null or load_button.disabled:
            _fail("MainMenu load button is unavailable after returning from SaveLoadScene")
            return false

        load_button.emit_signal("pressed")
        _stage = 8
        _ticks = 0

    elif _stage == 8 and _ticks > 2:
        if current_scene == null or current_scene.name != "SaveLoadScene":
            _fail("SaveLoadScene did not reopen for resume flow")
            return false

        var load_button := current_scene.get_node("Center/Panel/Padding/Content/LoadButton") as Button
        if load_button == null or load_button.disabled:
            _fail("SaveLoadScene load button is unavailable for resume flow")
            return false

        load_button.emit_signal("pressed")
        _stage = 9
        _ticks = 0

    elif _stage == 9 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load from SaveLoadScene")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        var squad_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/SquadButton") as Button
        if back_button == null or squad_button == null:
            _fail("ClubDashboard navigation controls are missing")
            return false

        if back_button.text != "Return to Main Menu":
            _fail("ClubDashboard back button does not return to the main menu")
            return false

        squad_button.emit_signal("pressed")
        _stage = 10
        _ticks = 0

    elif _stage == 10 and _ticks > 2:
        if current_scene == null or current_scene.name != "SquadScreen":
            _fail("SquadScreen did not load from ClubDashboard")
            return false

        var club_context := current_scene.get_node("Center/Shell/Padding/Content/Header/ClubContextLabel") as Label
        var player_list := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/SelectionCard/SelectionPadding/SelectionContent/PlayerList") as ItemList
        var open_profile_button := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/DetailCard/DetailPadding/DetailContent/ActionsRow/OpenProfileButton") as Button
        if club_context == null or player_list == null or open_profile_button == null:
            _fail("SquadScreen controls are missing")
            return false

        var game_state := root.get_node("GameState")
        var selected_club_name := "" if game_state == null else String(game_state.SelectedClubName)
        if selected_club_name.is_empty() or club_context.text.find(selected_club_name) == -1:
            _fail("SquadScreen club context did not preserve the selected club")
            return false

        if player_list.item_count == 0 or open_profile_button.disabled:
            _fail("SquadScreen did not surface an actionable player-profile handoff")
            return false

        open_profile_button.emit_signal("pressed")
        _stage = 11
        _ticks = 0

    elif _stage == 11 and _ticks > 2:
        if current_scene == null or current_scene.name != "PlayerProfile":
            _fail("PlayerProfile did not load from SquadScreen")
            return false

        var heading := current_scene.get_node("Center/Panel/Padding/Content/Heading") as Label
        var back_button := current_scene.get_node("Center/Panel/Padding/Content/BackButton") as Button
        if heading == null or back_button == null:
            _fail("PlayerProfile controls are missing")
            return false

        if heading.text == "Player Profile":
            _fail("PlayerProfile did not bind the selected player identity into the screen heading")
            return false

        back_button.emit_signal("pressed")
        _stage = 12
        _ticks = 0

    elif _stage == 12 and _ticks > 2:
        if current_scene == null or current_scene.name != "SquadScreen":
            _fail("PlayerProfile back navigation did not return to SquadScreen")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if back_button == null:
            _fail("SquadScreen back button is missing after returning from PlayerProfile")
            return false

        back_button.emit_signal("pressed")
        _stage = 13
        _ticks = 0

    elif _stage == 13 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("SquadScreen back navigation did not return to ClubDashboard")
            return false

        var tactics_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/TacticsButton") as Button
        if tactics_button == null:
            _fail("ClubDashboard tactics button is missing")
            return false

        tactics_button.emit_signal("pressed")
        _stage = 14
        _ticks = 0

    elif _stage == 14 and _ticks > 2:
        if current_scene == null or current_scene.name != "TacticsScreen":
            _fail("TacticsScreen did not load from ClubDashboard")
            return false

        var club_context := current_scene.get_node("Center/Shell/Padding/Content/Header/ClubContextLabel") as Label
        var back_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/BackButton") as Button
        if club_context == null or back_button == null:
            _fail("TacticsScreen controls are missing")
            return false

        var game_state := root.get_node("GameState")
        var selected_club_name := "" if game_state == null else String(game_state.SelectedClubName)
        if selected_club_name.is_empty() or club_context.text.find(selected_club_name) == -1:
            _fail("TacticsScreen did not preserve the selected club context")
            return false

        back_button.emit_signal("pressed")
        _stage = 15
        _ticks = 0

    elif _stage == 15 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("TacticsScreen back navigation did not return to ClubDashboard")
            return false

        var fixtures_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/FixturesButton") as Button
        if fixtures_button == null:
            _fail("ClubDashboard fixtures button is missing")
            return false

        fixtures_button.emit_signal("pressed")
        _stage = 16
        _ticks = 0

    elif _stage == 16 and _ticks > 2:
        if current_scene == null or current_scene.name != "FixturesScreen":
            _fail("FixturesScreen did not load from ClubDashboard")
            return false

        var competition_label := current_scene.get_node("Center/Shell/Padding/Content/Header/CompetitionLabel") as Label
        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if competition_label == null or back_button == null:
            _fail("FixturesScreen controls are missing")
            return false

        if competition_label.text.find("Novara") == -1:
            _fail("FixturesScreen did not preserve competition context")
            return false

        back_button.emit_signal("pressed")
        _stage = 17
        _ticks = 0

    elif _stage == 17 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("FixturesScreen back navigation did not return to ClubDashboard")
            return false

        var standings_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/StandingsButton") as Button
        if standings_button == null:
            _fail("ClubDashboard standings button is missing")
            return false

        standings_button.emit_signal("pressed")
        _stage = 18
        _ticks = 0

    elif _stage == 18 and _ticks > 2:
        if current_scene == null or current_scene.name != "StandingsScreen":
            _fail("StandingsScreen did not load from ClubDashboard")
            return false

        var club_summary := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/ClubSummaryLabel") as Label
        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if club_summary == null or back_button == null:
            _fail("StandingsScreen controls are missing")
            return false

        var game_state := root.get_node("GameState")
        var selected_club_name := "" if game_state == null else String(game_state.SelectedClubName)
        if selected_club_name.is_empty() or club_summary.text.find(selected_club_name) == -1:
            _fail("StandingsScreen did not preserve club table context")
            return false

        back_button.emit_signal("pressed")
        _stage = 19
        _ticks = 0

    elif _stage == 19 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("StandingsScreen back navigation did not return to ClubDashboard")
            return false

        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if matchday_button == null:
            _fail("ClubDashboard matchday button is missing after hub navigation coverage")
            return false

        matchday_button.emit_signal("pressed")
        _stage = 20
        _ticks = 0

    elif _stage == 20 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load from ClubDashboard")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/BackButton") as Button
        if back_button == null:
            _fail("MatchdayScene back button is missing")
            return false

        back_button.emit_signal("pressed")
        _stage = 21
        _ticks = 0

    elif _stage == 21 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Matchday back navigation did not return to ClubDashboard")
            return false

        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if matchday_button == null:
            _fail("ClubDashboard matchday button is missing after returning from MatchdayScene")
            return false

        matchday_button.emit_signal("pressed")
        _stage = 22
        _ticks = 0

    elif _stage == 22 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not reopen for instant result flow")
            return false

        var instant_result_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/InstantResultButton") as Button
        if instant_result_button == null or instant_result_button.disabled:
            _fail("Instant result button is unavailable for the Step 30 navigation check")
            return false

        instant_result_button.emit_signal("pressed")
        _stage = 23
        _ticks = 0

    elif _stage == 23 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("PostMatchScene did not load after instant result")
            return false

        var continue_button := current_scene.get_node("Center/Shell/Padding/Content/ContinueButton") as Button
        if continue_button == null:
            _fail("PostMatchScene continue button is missing")
            return false

        continue_button.emit_signal("pressed")
        _stage = 24
        _ticks = 0

    elif _stage == 24 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("PostMatchScene continue flow did not return to ClubDashboard")
            return false

        var back_button := current_scene.get_node("Center/Shell/Padding/Content/BackButton") as Button
        if back_button == null:
            _fail("ClubDashboard back button is missing after post-match continue")
            return false

        back_button.emit_signal("pressed")
        _stage = 25
        _ticks = 0

    elif _stage == 25 and _ticks > 2:
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
