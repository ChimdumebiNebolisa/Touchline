extends SceneTree

var _stage := 0
var _ticks := 0
var _selected_player := ""

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_squad()
    elif _stage == 2 and _ticks > 2:
        _validate_profile()

    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Squad Layout Check", 626266)
    game_state.SelectClub("Riverton Athletic")
    var err := change_scene_to_file("res://scenes/SquadScreen.tscn")
    if err != OK:
        _fail("Could not open SquadScreen")
        return
    _stage = 1
    _ticks = 0

func _validate_squad() -> void:
    var heading := _label_text("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHeading")
    if heading.find("Starting XI") == -1 or heading.find("Bench") == -1 or heading.find("Reserves") == -1:
        _fail("Team sheet heading does not separate XI, bench, reserves")
        return

    var rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows")
    var row_text := _collect_text(rows)
    for token in ["Starting XI", "Bench", "Reserves", "STARTING XI", "BENCH/RESERVE", "Form", "Morale", "Fitness"]:
        if row_text.find(token) == -1:
            _fail("Team sheet rows missing token %s" % token)
            return

    _selected_player = _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PlayerNameLabel")
    var profile_button := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/OpenProfileButton") as Button
    if profile_button == null or profile_button.disabled:
        _fail("Open profile button unavailable")
        return

    profile_button.emit_signal("pressed")
    _stage = 2
    _ticks = 0

func _validate_profile() -> void:
    if current_scene == null or current_scene.name != "PlayerProfile":
        _fail("PlayerProfile did not load")
        return

    var title := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/PageTitleLabel")
    var status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/StatusLabel")
    var club_context := _label_text("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubContextLabel")
    var identity := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel")
    var role := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/RoleLabel")
    var condition := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/ConditionLabel")

    if title != _selected_player:
        _fail("Player dossier title did not bind selected player")
        return

    for token in ["Age", "Form", "Morale", "Fitness"]:
        if status.find(token) == -1:
            _fail("Player dossier status missing %s" % token)
            return

    if club_context.find("Squad status") == -1 or identity.find("Player dossier") == -1 or role.find("Squad status") == -1:
        _fail("Player dossier identity/status copy missing")
        return

    if condition.find("Match sharpness") == -1:
        _fail("Player dossier condition row is not football-framed")
        return

    print("STEP62_SQUAD_PROFILE_LAYOUT_PASS")
    quit()

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
    print("STEP62_SQUAD_PROFILE_LAYOUT_FAIL: " + message)
    quit(1)
