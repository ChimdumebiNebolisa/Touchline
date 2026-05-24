extends SceneTree

var _stage := 0
var _ticks := 0
var _scene_index := 0

var _scenes := [
    {"path": "res://scenes/ClubDashboard.tscn", "selected": "DashboardButton"},
    {"path": "res://scenes/SquadScreen.tscn", "selected": "SquadButton"},
    {"path": "res://scenes/TacticsScreen.tscn", "selected": "TacticsButton"},
    {"path": "res://scenes/FixturesScreen.tscn", "selected": "FixturesButton"},
    {"path": "res://scenes/StandingsScreen.tscn", "selected": "StandingsButton"}
]

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_scene()

    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Vocabulary Nav Check", 616166)
    game_state.SelectClub("Riverton Athletic")
    _open_next_scene()

func _open_next_scene() -> void:
    if _scene_index >= _scenes.size():
        print("STEP61_FOOTBALL_VOCABULARY_NAV_PASS")
        quit()
        return

    var err := change_scene_to_file(_scenes[_scene_index]["path"])
    if err != OK:
        _fail("Could not open scene: %s" % _scenes[_scene_index]["path"])
        return

    _stage = 1
    _ticks = 0

func _validate_scene() -> void:
    var section_label := current_scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/SectionLabel") as Label
    if section_label == null or section_label.text != "CLUB OFFICE":
        _fail("Rail section is not football-framed")
        return

    var selected_button := current_scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/%s" % _scenes[_scene_index]["selected"]) as Button
    var matchday_button := current_scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/MatchdayButton") as Button
    var manager_button := current_scene.get_node_or_null("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/DashboardButton") as Button
    if selected_button == null or matchday_button == null or manager_button == null:
        _fail("Navigation buttons are missing")
        return

    if manager_button.text != "Manager Hub":
        _fail("Manager Hub navigation label missing")
        return

    if selected_button.disabled != true:
        _fail("Current page is not visually selected/disabled")
        return

    if matchday_button.text != "Go to Matchday":
        _fail("Matchday CTA label is not distinct from navigation")
        return

    if _scenes[_scene_index]["selected"] != "MatchdayButton" and matchday_button.disabled:
        _fail("Matchday CTA is disabled on an active career scene")
        return

    var visible_text := _collect_text(current_scene).to_lower()
    for banned in ["operations", "club dashboard", "current focus", "kickoff context", "match actions", "fixture desk", "standings desk", "ball speed", "commitment", "press line"]:
        if visible_text.find(banned) != -1:
            _fail("Visible UI still contains admin term: %s" % banned)
            return

    _scene_index += 1
    _open_next_scene()

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
    print("STEP61_FOOTBALL_VOCABULARY_NAV_FAIL: " + message)
    quit(1)
