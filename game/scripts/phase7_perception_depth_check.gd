extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return false

    game_state.StartNewCareer("Phase 7 Perception Check", 770007)
    game_state.SelectClub("Riverton Athletic")
    var validation_message := str(game_state.ValidatePhase7PerceptionDepthContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    var save_system := root.get_node("SaveSystem")
    if save_system == null:
        _fail("SaveSystem singleton missing")
        return false

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return false

    game_state.StartNewCareer("Phase 7 Mutation", 770107)
    game_state.SelectClub("Harbor County")
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return false

    validation_message = str(game_state.ValidatePhase7StoredPerceptionContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("PHASE7_PERCEPTION_DEPTH_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE7_PERCEPTION_DEPTH_FAIL: " + message)
    quit(1)
