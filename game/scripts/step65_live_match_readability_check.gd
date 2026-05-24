extends SceneTree

var _stage := 0
var _ticks := 0
var _saw_banner := false
var _saw_trail := false
var _saw_target := false
var _saw_pitch := false
var _saw_readable_control := false

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 2:
        _open_live_match()
    elif _stage == 2:
        _inspect_live_match()

    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Live Readability Check", 656566)
    game_state.SelectClub("Riverton Athletic")
    var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
    if err != OK:
        _fail("Could not open MatchdayScene")
        return
    _stage = 1
    _ticks = 0

func _open_live_match() -> void:
    var live_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton") as Button
    live_button.emit_signal("pressed")
    _stage = 2
    _ticks = 0

func _inspect_live_match() -> void:
    if current_scene == null or current_scene.name != "LiveMatchScene":
        if _ticks > 60:
            _fail("LiveMatchScene did not load")
        return

    var markers := current_scene.get_node_or_null("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/MarkersLayer")
    if markers == null:
        _fail("MarkersLayer missing")
        return

    _saw_pitch = _saw_pitch or markers.get_node_or_null("PitchMarkings") != null

    var banner = _find_label_by_name(markers, "ActionBannerLabel")
    if banner != null and (banner.text.find(":") != -1 or banner.text == "KICKOFF"):
        _saw_banner = true

    var trail := markers.get_node_or_null("PlaybackActionTrail") as Line2D
    if trail != null and trail.visible and trail.points.size() >= 2:
        _saw_trail = true

    var target := markers.get_node_or_null("PlaybackReceiverTarget") as Control
    if target != null and target.visible:
        _saw_target = true

    var control_label := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/ControlLabel") as Label
    if control_label != null and control_label.text.find("Carrier |") != -1 and control_label.text.find("Target |") != -1:
        _saw_readable_control = true

    if _saw_pitch and _saw_banner and _saw_trail and _saw_target and _saw_readable_control:
        print("STEP65_LIVE_MATCH_READABILITY_PASS")
        quit()
        return

    if _ticks > 1800:
        _fail("Live match readability cues were not all observed")

func _find_label_by_name(node: Node, label_name: String):
    if node is Label and str(node.name) == label_name:
        return node as Label
    for child in node.get_children():
        var found = _find_label_by_name(child, label_name)
        if found != null:
            return found
    return null

func _fail(message: String) -> void:
    push_error(message)
    print("STEP65_LIVE_MATCH_READABILITY_FAIL: " + message)
    quit(1)
