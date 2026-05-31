extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    var game_state := root.get_node("GameState")
    var world_generator := root.get_node("WorldGenerator")
    var save_system := root.get_node("SaveSystem")
    if game_state == null or world_generator == null or save_system == null:
        _fail("Required autoloads missing")
        return false

    if not world_generator.BeginNewCareer("Phase 17 Cup Check", 171717, "Manager", "Unknown Upstart", "National C License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidatePhase17CupCompetitionContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return false

    if not world_generator.BeginNewCareer("Phase 17 Cup Mutation", 171818, "Manager", "Unknown Upstart", "National C License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Northbridge City"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return false

    var stored_message := str(game_state.ValidatePhase17StoredCupCompetitionContract())
    if stored_message != "OK":
        _fail(stored_message)
        return false

    var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
    if err != OK:
        _fail("Could not open ClubDashboard")
        return false
    await process_frame
    await process_frame
    var current_scene := root.get_child(root.get_child_count() - 1)
    if current_scene == null or current_scene.name != "ClubDashboard":
        _fail("ClubDashboard did not load")
        return false

    var career_market_label := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/CareerMarketLabel")
    if career_market_label == null:
        _fail("Dashboard career/cup label is missing")
        return false
    var text := str(career_market_label.text)
    if text.find("Cup competitions") == -1 or text.find("Novara National Cup") == -1 or text.find("Cup history") == -1:
        _fail("Dashboard cup summary is incomplete: %s" % text)
        return false

    err = change_scene_to_file("res://scenes/FixturesScreen.tscn")
    if err != OK:
        _fail("Could not open FixturesScreen")
        return false
    await process_frame
    await process_frame
    current_scene = root.get_child(root.get_child_count() - 1)
    if current_scene == null or current_scene.name != "FixturesScreen":
        _fail("FixturesScreen did not load")
        return false

    var found_cup_text := false
    var stack := [current_scene]
    while not stack.is_empty():
        var node = stack.pop_back()
        if node is Label and str(node.text).find("National Cup") != -1:
            found_cup_text = true
            break
        for child in node.get_children():
            stack.push_back(child)
    if not found_cup_text:
        _fail("FixturesScreen does not surface cup fixtures")
        return false

    print("PHASE17_CUP_COMPETITION_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE17_CUP_COMPETITION_FAIL: " + message)
    quit(1)
