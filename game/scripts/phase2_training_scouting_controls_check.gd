extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
    if err != OK:
        _fail("Could not open ClubDashboard")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        var world_generator := root.get_node("WorldGenerator")
        if world_generator == null:
            _fail("WorldGenerator singleton missing")
            return false

        if not world_generator.BeginNewCareer("Phase 2 Controls Check", 720002, "Manager", "Tactical Specialist", "National A License"):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Riverton Athletic"):
            _fail(world_generator.LastStatusMessage)
            return false

        var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
        if err != OK:
            _fail("Could not reload ClubDashboard with career state")
            return false

        _advance_stage()
        return false

    if _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load")
            return false

        var training_focus := _option("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingFocusOption")
        var training_intensity := _option("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingIntensityOption")
        var apply_training := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ApplyTrainingButton") as Button
        var scouting_target := _option("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ScoutingTargetOption")
        var scouting_depth := _option("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ScoutingDepthOption")
        var start_scouting := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StartScoutingButton") as Button
        var advance_day := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/AdvanceDayButton") as Button
        var advance_week := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/AdvanceWeekButton") as Button
        var summary := _label("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingScoutingLabel")
        if training_focus == null or training_intensity == null or apply_training == null or scouting_target == null or scouting_depth == null or start_scouting == null or advance_day == null or advance_week == null or summary == null:
            _fail("Dashboard training/scouting controls are missing")
            return false

        _select_text(training_focus, "Pressing")
        _select_text(training_intensity, "Demanding")
        apply_training.emit_signal("pressed")
        summary = _label("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingScoutingLabel")
        if summary.text.find("Pressing") == -1 or summary.text.find("Demanding") == -1:
            _fail("Training controls did not update dashboard state: %s" % summary.text)
            return false

        _select_text(scouting_target, "Specific player: pressing winger")
        _select_text(scouting_depth, "Full report")
        start_scouting.emit_signal("pressed")
        summary = _label("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingScoutingLabel")
        if summary.text.find("Full report") == -1 or summary.text.find("pressing winger") == -1:
            _fail("Scouting controls did not update dashboard state: %s" % summary.text)
            return false

        advance_day.emit_signal("pressed")
        advance_week.emit_signal("pressed")

        var game_state := root.get_node("GameState")
        if game_state == null:
            _fail("GameState singleton missing after dashboard actions")
            return false

        var result := str(game_state.ValidatePhase2TrainingScoutingControlsContract())
        if result != "OK":
            _fail(result)
            return false

        print("PHASE2_TRAINING_SCOUTING_CONTROLS_PASS")
        quit()
        return false

    return false

func _advance_stage() -> void:
    _stage += 1
    _ticks = 0

func _option(path: String) -> OptionButton:
    var node := current_scene.get_node_or_null(path) as OptionButton
    if node == null:
        _fail("Missing option button: %s" % path)
    return node

func _label(path: String) -> Label:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
    return node

func _select_text(option: OptionButton, text: String) -> void:
    for index in range(option.item_count):
        if option.get_item_text(index) == text:
            option.select(index)
            return
    _fail("Option value not found: %s" % text)

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE2_TRAINING_SCOUTING_CONTROLS_FAIL: " + message)
    quit(1)
