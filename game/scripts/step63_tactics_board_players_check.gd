extends SceneTree

var _stage := 0
var _ticks := 0

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 4:
        _validate_tactics_board()

    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Tactics Board Check", 636366)
    game_state.SelectClub("Riverton Athletic")
    var err := change_scene_to_file("res://scenes/TacticsScreen.tscn")
    if err != OK:
        _fail("Could not open TacticsScreen")
        return
    _stage = 1
    _ticks = 0

func _validate_tactics_board() -> void:
    var board := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/TacticalBoard")
    if board == null:
        _fail("TacticalBoard drawing control is missing")
        return

    var marker_count := 0
    var named_marker_count := 0
    var role_label_count := 0
    var child_names: Array[String] = []
    var marker_texts: Array[String] = []
    for child in board.get_children():
        child_names.append("%s:%s" % [str(child.name), child.get_class()])
        if not child is PanelContainer:
            continue

        marker_count += 1
        if child.has_meta("player_name"):
            named_marker_count += 1

        var marker_text := _collect_text(child)
        marker_texts.append(marker_text)
        if marker_text.find("GK") != -1 or marker_text.find("LB") != -1 or marker_text.find("CB") != -1 or marker_text.find("RB") != -1 or marker_text.find("CM") != -1 or marker_text.find("AM") != -1 or marker_text.find("LW") != -1 or marker_text.find("ST") != -1 or marker_text.find("RW") != -1:
            role_label_count += 1

    if marker_count < 11:
        _fail("Tactics board does not render 11 tactical slots; children: %s" % ", ".join(child_names))
        return

    if named_marker_count < 11:
        _fail("Tactics board does not render real Starting XI player identities")
        return

    if role_label_count < 8:
        _fail("Tactics board markers do not expose subtle role labels: %s" % " | ".join(marker_texts))
        return

    var status := _label_text("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/StatusLabel")
    var first_marker: Node = null
    for child in board.get_children():
        if child.has_meta("player_name"):
            first_marker = child
            break

    if first_marker == null:
        _fail("No selectable player marker found")
        return

    first_marker.emit_signal("gui_input", _make_click())
    await process_frame
    status = _label_text("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/StatusLabel")
    if status.find("Selected marker") == -1 or status.find("Fitness") == -1:
        _fail("Clicking a tactic marker does not inspect player details: %s" % status)
        return

    print("STEP63_TACTICS_BOARD_PLAYERS_PASS")
    quit()

func _make_click() -> InputEventMouseButton:
    var event := InputEventMouseButton.new()
    event.button_index = MOUSE_BUTTON_LEFT
    event.pressed = true
    return event

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
        return ""
    return node.text

func _collect_text(node: Node) -> String:
    var parts: Array[String] = []
    if node is Label:
        parts.append((node as Label).text)
    elif node is Button:
        parts.append((node as Button).text)
    for child in node.get_children():
        parts.append(_collect_text(child))
    return " ".join(parts)

func _fail(message: String) -> void:
    push_error(message)
    print("STEP63_TACTICS_BOARD_PLAYERS_FAIL: " + message)
    quit(1)
