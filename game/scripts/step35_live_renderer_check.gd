extends SceneTree

var _stage := -1
var _ticks := 0
var _saw_ball := false
var _saw_action_line := false
var _saw_carrier_marker := false
var _saw_status_detail := false
var _saw_event_alignment := false

func _initialize() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == -1 and _ticks > 2:
        var game_state := root.get_node("GameState")
        game_state.StartNewCareer("Renderer Check", 112233)
        game_state.SelectClub("Riverton Athletic")

        var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
        if err != OK:
            _fail("unable to load MatchdayScene")
            return false

        _stage = 0
        _ticks = 0

    elif _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load")
            return false

        var live_button := current_scene.get_node_or_null("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton") as Button
        if live_button == null:
            _fail("StartMatchButton missing on MatchdayScene")
            return false

        live_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1:
        if current_scene == null or current_scene.name != "LiveMatchScene":
            if _ticks > 60:
                _fail("LiveMatchScene did not load")
            return false

        _inspect_live_renderer()

        var back_button := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/BackButton") as Button
        if back_button == null:
            _fail("Back button missing on LiveMatchScene")
            return false

        if back_button.text == "Continue to Post-Match":
            _assert_live_renderer_observed()
            back_button.emit_signal("pressed")
            _stage = 2
            _ticks = 0
            return false

        if _ticks > 1800:
            _fail("LiveMatchScene did not reach full time")
            return false

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("LiveMatchScene did not hand off to PostMatchScene")
            return false

        print("STEP35_LIVE_RENDERER_PASS")
        quit()

    return false

func _inspect_live_renderer() -> void:
    var markers_layer := current_scene.get_node_or_null("Margin/Root/ContentRow/PitchColumn/PitchFrame/Pitch/MarkersLayer")
    if markers_layer == null:
        _fail("MarkersLayer missing on LiveMatchScene")
        return

    var ball := markers_layer.get_node_or_null("PlaybackBall")
    var ball_halo := markers_layer.get_node_or_null("PlaybackBallHalo")
    if ball != null and ball_halo != null and ball.visible and ball_halo.visible:
        _saw_ball = true

    var action_line := markers_layer.get_node_or_null("PlaybackActionLine") as ColorRect
    if action_line != null and action_line.visible and action_line.size.x > 8.0:
        _saw_action_line = true

    var marker_count := 0
    var child_descriptions: Array[String] = []
    for child in markers_layer.get_children():
        child_descriptions.append("%s:%s" % [str(child.name), child.get_class()])
        if not str(child.name).begins_with("Marker_"):
            continue

        marker_count += 1
        var marker := child as Button
        if marker == null:
            continue

        if marker.custom_minimum_size.x > 30.0 and marker.tooltip_text.find("Intent:") != -1:
            _saw_carrier_marker = true

    if marker_count < 22:
        if _ticks > 30:
            var status_label := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/StatusLabel") as Label
            var status_text := status_label.text if status_label != null else "missing status"
            _fail("LiveMatchScene did not render all 22 player markers; saw %d marker nodes among %d children: %s; status: %s" % [marker_count, markers_layer.get_child_count(), ", ".join(child_descriptions), status_text])
        return

    var control_label := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/ControlLabel") as Label
    if control_label != null:
        var text := control_label.text
        if text.find("Action |") != -1 and text.find("Possession |") != -1 and text.find("Ball |") != -1 and text.find("Carrier |") != -1:
            _saw_status_detail = true

    var status_label := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/StatusLabel") as Label
    var feed_label := current_scene.get_node_or_null("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/EventFeedLabel") as Label
    if status_label != null and feed_label != null and status_label.text.find("'") != -1 and feed_label.text.find(status_label.text) != -1:
        _saw_event_alignment = true

func _assert_live_renderer_observed() -> void:
    if not _saw_ball:
        _fail("ball and halo were not visible during live playback")
        return

    if not _saw_action_line:
        _fail("frame-derived action line was not visible during live playback")
        return

    if not _saw_carrier_marker:
        _fail("carrier or intent marker emphasis was not visible during live playback")
        return

    if not _saw_status_detail:
        _fail("status area did not expose action, possession, ball state, and carrier")
        return

    if not _saw_event_alignment:
        _fail("active event summary did not align with the event feed")

func _fail(message: String) -> void:
    push_error(message)
    print("STEP35_LIVE_RENDERER_FAIL: " + message)
    quit(1)
