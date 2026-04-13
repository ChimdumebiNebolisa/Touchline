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

        var name_input := current_scene.get_node("Center/Panel/Padding/Content/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("Center/Panel/Padding/Content/SeedInput") as SpinBox
        var start_button := current_scene.get_node("Center/Panel/Padding/Content/StartCareerButton") as Button

        if name_input == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls are missing")
            return false

        name_input.text = "Riley Hart"
        seed_input.value = 24680
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("ChooseClub did not load")
            return false

        var club_list := current_scene.get_node("Center/Panel/Padding/Content/ClubList") as ItemList
        var confirm_button := current_scene.get_node("Center/Panel/Padding/Content/ConfirmSelectionButton") as Button

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

        var tactics_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/TacticsButton") as Button
        if tactics_button == null:
            _fail("TacticsButton missing on ClubDashboard")
            return false

        tactics_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "TacticsScreen":
            _fail("TacticsScreen did not load")
            return false

        var formation := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/FormationOption") as OptionButton
        var press_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/PressSpin") as SpinBox
        var tempo_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/TempoSpin") as SpinBox
        var width_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/WidthSpin") as SpinBox
        var risk_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/RiskSpin") as SpinBox
        var save_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveButton") as Button
        var back_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/BackButton") as Button

        if formation == null or press_spin == null or tempo_spin == null or width_spin == null or risk_spin == null or save_button == null or back_button == null:
            _fail("TacticsScreen controls are missing")
            return false

        formation.select(2)
        press_spin.value = 72
        tempo_spin.value = 64
        width_spin.value = 59
        risk_spin.value = 67
        save_button.emit_signal("pressed")

        var game_state := root.get_node("GameState")
        if game_state == null:
            _fail("GameState singleton missing")
            return false

        if str(game_state.TacticalFormation) != "3-5-2":
            _fail("TacticalFormation not persisted")
            return false

        if int(game_state.PressIntensity) != 72 or int(game_state.Tempo) != 64 or int(game_state.Width) != 59 or int(game_state.Risk) != 67:
            _fail("Tactical numeric fields not persisted")
            return false

        back_button.emit_signal("pressed")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Back from TacticsScreen did not return to ClubDashboard")
            return false

        var tactics_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/TacticsButton") as Button
        if tactics_button == null:
            _fail("TacticsButton missing on ClubDashboard")
            return false

        tactics_button.emit_signal("pressed")
        _stage = 5
        _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "TacticsScreen":
            _fail("TacticsScreen did not reload")
            return false

        var formation := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/FormationOption") as OptionButton
        var press_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/PressSpin") as SpinBox
        var tempo_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/TempoSpin") as SpinBox
        var width_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/WidthSpin") as SpinBox
        var risk_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/RiskSpin") as SpinBox

        if formation == null or press_spin == null or tempo_spin == null or width_spin == null or risk_spin == null:
            _fail("TacticsScreen controls missing on reload")
            return false

        if formation.get_item_text(formation.selected) != "3-5-2":
            _fail("Formation control did not reload persisted value")
            return false

        if int(press_spin.value) != 72 or int(tempo_spin.value) != 64 or int(width_spin.value) != 59 or int(risk_spin.value) != 67:
            _fail("Tactical controls did not reload persisted numeric values")
            return false

        print("STEP6_SUBTASK_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP6_SUBTASK_FAIL: " + message)
    quit(1)
