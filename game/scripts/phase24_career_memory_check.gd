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

    if not world_generator.BeginNewCareer("Phase 24 Memory Check", 242424, "Manager", "Former Club Legend", "National B License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidatePhase24CareerMemoryContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return false

    if not world_generator.BeginNewCareer("Phase 24 Mutation", 242525, "Manager", "Unknown Upstart", "National C License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Northbridge City"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return false

    var stored_message := str(game_state.ValidatePhase24StoredCareerMemoryContract())
    if stored_message != "OK":
        _fail(stored_message)
        return false

    print("PHASE24_CAREER_MEMORY_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE24_CAREER_MEMORY_FAIL: " + message)
    quit(1)
