extends SceneTree

var _stage := 0
var _ticks := 0

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_tactics_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_initial_tactics_context()
    elif _stage == 2 and _ticks > 2:
        _validate_saved_tactics_context()

    return false

func _start_tactics_flow() -> void:
    var world_generator := root.get_node("WorldGenerator")
    if world_generator == null:
        _fail("WorldGenerator autoload is missing")
        return

    if not world_generator.BeginNewCareer("Tactics Context Check", 525260):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return

    var err := change_scene_to_file("res://scenes/TacticsScreen.tscn")
    if err != OK:
        _fail("Could not open TacticsScreen")
        return

    _stage = 1
    _ticks = 0

func _validate_initial_tactics_context() -> void:
    if current_scene == null or current_scene.name != "TacticsScreen":
        _fail("TacticsScreen did not load")
        return

    var formation_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/FormationCard/CardPadding/CardContent/CardValueLabel")
    var press_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/PressCard/CardPadding/CardContent/CardValueLabel")
    var tempo_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/TempoCard/CardPadding/CardContent/CardValueLabel")
    var width_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/WidthCard/CardPadding/CardContent/CardValueLabel")
    var risk_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/RiskCard/CardPadding/CardContent/CardValueLabel")
    var preview_summary := _label_text("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PreviewSummaryLabel")
    var control_summary := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/ControlSummaryLabel")
    var press_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/PressPreviewLabel")
    var tempo_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/TempoPreviewLabel")
    var width_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/WidthPreviewLabel")
    var risk_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/RiskPreviewLabel")
    var saved_plan := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/SavedPlanLabel")
    var save_hint := _label_text("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveHintLabel")

    if formation_value == "--" or press_value == "--" or tempo_value == "--" or width_value == "--" or risk_value == "--":
        _fail("Tactics summary cards do not show all tactical values")
        return

    if preview_summary.find("Shared match engine preview") == -1:
        _fail("Preview summary does not connect tactics to the shared match engine: %s" % preview_summary)
        return

    for token in ["Formation", "Pressing", "Tempo", "Width", "Mentality"]:
        if control_summary.find(token) == -1:
            _fail("Control summary is missing %s: %s" % [token, control_summary])
            return

    if press_note.find("Pressing Intensity") == -1 or tempo_note.find("Tempo") == -1 or width_note.find("Pitch use") == -1 or risk_note.find("Mentality") == -1:
        _fail("Tactical interpretation notes are incomplete")
        return

    if saved_plan.find("Saved tactical setup") == -1 or saved_plan.find("Shared match engine input") == -1:
        _fail("Saved plan summary is not explicit: %s" % saved_plan)
        return

    if save_hint.to_lower().find("unsaved preview") == -1 or save_hint.to_lower().find("shared match engine") == -1:
        _fail("Save hint does not explain preview/apply state: %s" % save_hint)
        return

    var formation := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/FormationOption") as OptionButton
    var press_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/PressSpin") as SpinBox
    var tempo_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/TempoSpin") as SpinBox
    var width_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/WidthSpin") as SpinBox
    var risk_spin := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/RiskSpin") as SpinBox
    var save_button := current_scene.get_node("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveButton") as Button

    if formation == null or press_spin == null or tempo_spin == null or width_spin == null or risk_spin == null or save_button == null:
        _fail("Tactics controls are missing")
        return

    formation.select(2)
    formation.emit_signal("item_selected", 2)
    press_spin.value = 82
    tempo_spin.value = 77
    width_spin.value = 68
    risk_spin.value = 73
    save_button.emit_signal("pressed")

    _stage = 2
    _ticks = 0

func _validate_saved_tactics_context() -> void:
    if current_scene == null or current_scene.name != "TacticsScreen":
        _fail("TacticsScreen was not available after save")
        return

    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    if str(game_state.TacticalFormation) != "3-5-2":
        _fail("Saved formation did not persist into GameState")
        return

    if int(game_state.PressIntensity) != 82 or int(game_state.Tempo) != 77 or int(game_state.Width) != 68 or int(game_state.Risk) != 73:
        _fail("Saved tactical values did not persist into GameState")
        return

    var saved_plan := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/SavedPlanLabel")
    var status := _label_text("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/StatusLabel")
    var save_hint := _label_text("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveHintLabel")
    var control_summary := _label_text("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/ControlSummaryLabel")

    if saved_plan.find("Formation 3-5-2") == -1 or saved_plan.find("Pressing 82") == -1 or saved_plan.find("Tempo 77") == -1 or saved_plan.find("Width 68") == -1 or saved_plan.find("Mentality 73") == -1:
        _fail("Saved plan label does not show all persisted tactical values: %s" % saved_plan)
        return

    if status.to_lower().find("shared match engine") == -1:
        _fail("Save status does not explain the shared engine connection: %s" % status)
        return

    if save_hint.find("matchday tactical setup") == -1:
        _fail("Save hint does not confirm saved/apply state: %s" % save_hint)
        return

    if control_summary.find("Pressing 82") == -1 or control_summary.find("Tempo 77") == -1 or control_summary.find("Width 68") == -1 or control_summary.find("Mentality 73") == -1:
        _fail("Control summary does not show saved preview values: %s" % control_summary)
        return

    print("STEP52_TACTICS_CONTEXT_PASS")
    quit()

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
        return ""

    return node.text

func _fail(message: String) -> void:
    push_error(message)
    print("STEP52_TACTICS_CONTEXT_FAIL: " + message)
    quit(1)
