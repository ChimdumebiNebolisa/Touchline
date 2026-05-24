extends SceneTree

var _stage := 0
var _ticks := 0
var _scene_index := 0

var _scenes := [
    "res://scenes/MainMenu.tscn",
    "res://scenes/ClubDashboard.tscn",
    "res://scenes/SquadScreen.tscn",
    "res://scenes/TacticsScreen.tscn",
    "res://scenes/FixturesScreen.tscn",
    "res://scenes/StandingsScreen.tscn",
    "res://scenes/MatchdayScene.tscn",
    "res://scenes/SaveLoadScene.tscn"
]

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_ui_consistency_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_current_scene()

    return false

func _start_ui_consistency_flow() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    game_state.StartNewCareer("UI Consistency Check", 545460)
    game_state.SelectClub("Riverton Athletic")

    _open_next_scene()

func _open_next_scene() -> void:
    if _scene_index >= _scenes.size():
        print("STEP54_UI_CONSISTENCY_PASS")
        quit()
        return

    var err := change_scene_to_file(_scenes[_scene_index])
    if err != OK:
        _fail("Could not open scene: %s" % _scenes[_scene_index])
        return

    _stage = 1
    _ticks = 0

func _validate_current_scene() -> void:
    if current_scene == null:
        _fail("Scene did not load: %s" % _scenes[_scene_index])
        return

    var visible_text := _collect_text(current_scene)
    var visible_lower := visible_text.to_lower()
    for banned in ["prototype", "debug", "web prototype", "npm", "launch matchday"]:
        if visible_lower.find(banned) != -1:
            _fail("Visible UI text contains stale/debug wording '%s' on %s" % [banned, current_scene.name])
            return

    if _has_operations_rail(current_scene):
        _validate_operations_rail(current_scene)

    _scene_index += 1
    _open_next_scene()

func _has_operations_rail(scene: Node) -> bool:
    return scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons") != null

func _validate_operations_rail(scene: Node) -> void:
    var expected := {
        "DashboardButton": "Dashboard",
        "SquadButton": "Squad",
        "TacticsButton": "Tactics",
        "FixturesButton": "Fixtures",
        "StandingsButton": "Standings",
        "MatchdayButton": "Matchday"
    }

    var section_label := scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/SectionLabel") as Label
    if section_label == null or section_label.text != "OPERATIONS":
        _fail("Operations rail section label is inconsistent on %s" % scene.name)
        return

    for button_name in expected.keys():
        var button := scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/%s" % button_name) as Button
        if button == null:
            _fail("Missing rail button %s on %s" % [button_name, scene.name])
            return

        if button.text != expected[button_name]:
            _fail("Rail button %s on %s uses '%s' instead of '%s'" % [button_name, scene.name, button.text, expected[button_name]])
            return

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
    print("STEP54_UI_CONSISTENCY_FAIL: " + message)
    quit(1)
