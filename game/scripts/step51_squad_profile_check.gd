extends SceneTree

var _stage := 0
var _ticks := 0
var _selected_player := ""
var _starting_count_before := 0

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_squad_profile_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_squad_screen()
    elif _stage == 2 and _ticks > 2:
        _validate_player_profile()

    return false

func _start_squad_profile_flow() -> void:
    var world_generator := root.get_node("WorldGenerator")
    var game_state := root.get_node("GameState")
    if world_generator == null or game_state == null:
        _fail("Required autoloads are missing")
        return

    if not world_generator.BeginNewCareer("Squad Profile Check", 515160):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return

    game_state.ResolveCurrentMatchInstantly()

    var err := change_scene_to_file("res://scenes/SquadScreen.tscn")
    if err != OK:
        _fail("Could not open SquadScreen")
        return

    _stage = 1
    _ticks = 0

func _validate_squad_screen() -> void:
    if current_scene == null or current_scene.name != "SquadScreen":
        _fail("SquadScreen did not load")
        return

    var heading := _label_text("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHeading")
    var hint := _label_text("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHintLabel")
    var header_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel")
    var starters_meta := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardMetaLabel")
    var bench_meta := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/BenchCard/CardPadding/CardContent/CardMetaLabel")
    var role_chip := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/RoleChip/RoleChipPadding/RoleChipLabel")
    var form_stat := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/FormStatLabel")
    var morale_stat := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/MoraleStatLabel")
    var fitness_stat := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/FitnessStatLabel")
    var readiness := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ReadinessSummaryLabel")
    var profile_hint := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ProfileHintLabel")
    var squad_status := _label_text("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/SquadStatusLabel")
    var player_name := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PlayerNameLabel")
    var starters_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardValueLabel")

    if heading.find("Starting XI") == -1 or heading.find("Non-Starters") == -1:
        _fail("Squad heading does not separate starters and non-starters: %s" % heading)
        return

    var hint_lower := hint.to_lower()
    if hint_lower.find("bench") == -1 or hint_lower.find("reserve") == -1:
        _fail("Squad hint does not explain bench and reserve depth: %s" % hint)
        return

    if header_status.find("Starting XI") == -1 or header_status.find("non-starters") == -1:
        _fail("Squad header does not explain lineup groups: %s" % header_status)
        return

    if header_status.find("Latest match player state reflected") == -1 or squad_status.find("Latest match player state reflected") == -1:
        _fail("Squad screen does not expose post-match player-state visibility")
        return

    if starters_meta.find("Starting XI") == -1 or bench_meta.find("Non-starters") == -1:
        _fail("Squad summary cards do not label starters and non-starters")
        return

    var rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows")
    var row_text := _collect_text(rows)
    if row_text.find("STARTING XI") == -1 or row_text.find("NON-STARTER") == -1:
        _fail("Player rows do not show both Starting XI and non-starter labels")
        return

    if row_text.find("Fitness") == -1 or row_text.find("Morale") == -1 or row_text.find("Form") == -1:
        _fail("Player rows do not expose condition values clearly")
        return

    if role_chip.find("STARTING XI") == -1 or form_stat.find("Form") == -1 or morale_stat.find("Morale") == -1 or fitness_stat.find("Fitness") == -1:
        _fail("Selected player detail is missing role/form/morale/fitness")
        return

    if readiness.find("lineup status") == -1 or profile_hint.find("form-morale-fitness") == -1:
        _fail("Selected player detail does not explain readiness and profile purpose")
        return

    var action_button := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/LineupActionButton") as Button
    if action_button == null or action_button.disabled:
        _fail("Lineup action button is not available")
        return

    _selected_player = player_name
    _starting_count_before = int(starters_value)
    if _starting_count_before != 11:
        _fail("Expected 11 starters before lineup action, found %d" % _starting_count_before)
        return

    action_button.emit_signal("pressed")

    var starters_after := int(_label_text("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardValueLabel"))
    if starters_after != _starting_count_before:
        _fail("Lineup action changed the total starting count")
        return

    var role_after := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/RoleChip/RoleChipPadding/RoleChipLabel")
    if role_after.find("NON-STARTER") == -1:
        _fail("Lineup action did not move the selected starter into non-starter status: %s" % role_after)
        return

    var profile_button := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/OpenProfileButton") as Button
    if profile_button == null or profile_button.disabled:
        _fail("Profile handoff button is unavailable after lineup action")
        return

    profile_button.emit_signal("pressed")
    _stage = 2
    _ticks = 0

func _validate_player_profile() -> void:
    if current_scene == null or current_scene.name != "PlayerProfile":
        _fail("PlayerProfile did not load")
        return

    var title := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/PageTitleLabel")
    var status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/StatusLabel")
    var role_chip := _label_text("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/RoleChip/RoleChipPadding/RoleChipLabel")
    var club_context := _label_text("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubContextLabel")
    var identity := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel")
    var role := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/RoleLabel")
    var condition := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/ConditionLabel")
    var pathway := _label_text("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/PathwayLabel")
    var readiness := _label_text("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/ReadinessLabel")

    if title != _selected_player:
        _fail("PlayerProfile did not bind the selected player identity")
        return

    if status.find("Age") == -1 or status.find("Form") == -1 or status.find("Morale") == -1 or status.find("Fitness") == -1:
        _fail("PlayerProfile status is missing age/form/morale/fitness: %s" % status)
        return

    if role_chip.find("NON-STARTER") == -1 or club_context.find("Lineup status") == -1:
        _fail("PlayerProfile does not expose lineup status after lineup action")
        return

    if identity.find(_selected_player) == -1 or role.find("Lineup status") == -1:
        _fail("PlayerProfile identity or role summary is incomplete")
        return

    if condition.find("Fitness") == -1 or condition.find("Morale") == -1 or condition.find("Form") == -1:
        _fail("PlayerProfile condition summary is incomplete")
        return

    if pathway.find("Latest match state reflected") == -1:
        _fail("PlayerProfile does not expose post-match state visibility")
        return

    if readiness.find("Match readiness") == -1 or readiness.find("Non-starter") == -1:
        _fail("PlayerProfile readiness summary is incomplete: %s" % readiness)
        return

    print("STEP51_SQUAD_PROFILE_PASS")
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

    for child in node.get_children():
        parts.append(_collect_text(child))

    return " ".join(parts)

func _fail(message: String) -> void:
    push_error(message)
    print("STEP51_SQUAD_PROFILE_FAIL: " + message)
    quit(1)
