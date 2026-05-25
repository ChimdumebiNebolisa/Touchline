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

    if not world_generator.BeginNewCareer("Phase 14 Development Check", 141414, "Manager", "Youth Academy Coach", "National C License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Northbridge City"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidatePhase14PlayerDevelopmentContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return false

    if not world_generator.BeginNewCareer("Phase 14 Development Mutation", 141515, "Manager", "Unknown Upstart", "National C License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return false

    var stored_message := str(game_state.ValidatePhase14StoredPlayerDevelopmentContract())
    if stored_message != "OK":
        _fail(stored_message)
        return false

    game_state.SelectPlayerProfile("Mikel Duarte")
    var err := change_scene_to_file("res://scenes/PlayerProfile.tscn")
    if err != OK:
        _fail("Could not open PlayerProfile")
        return false
    await process_frame
    await process_frame
    var current_scene := root.get_child(root.get_child_count() - 1)
    if current_scene == null or current_scene.name != "PlayerProfile":
        _fail("PlayerProfile did not load")
        return false

    var pathway_label := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PathwayLabel")
    if pathway_label == null:
        _fail("PlayerProfile pathway label is missing")
        return false
    if str(pathway_label.text).find("Development cadence") == -1 and str(pathway_label.text).find("Season development review") == -1:
        _fail("PlayerProfile does not surface development state: %s" % pathway_label.text)
        return false

    print("PHASE14_PLAYER_DEVELOPMENT_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE14_PLAYER_DEVELOPMENT_FAIL: " + message)
    quit(1)
