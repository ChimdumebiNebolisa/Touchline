extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    var world_generator := root.get_node("WorldGenerator")
    var game_state := root.get_node("GameState")
    if world_generator == null or game_state == null:
        _fail("Required autoloads are missing")
        return false

    if not world_generator.BeginNewCareer("Phase 1 Visibility Check", 710001, "Manager", "Tactical Specialist", "Pro License"):
        _fail(world_generator.LastStatusMessage)
        return false

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return false

    var result := str(game_state.ValidatePhase1InformationVisibilityContract())
    if result != "OK":
        _fail(result)
        return false

    print("PHASE1_INFORMATION_VISIBILITY_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE1_INFORMATION_VISIBILITY_FAIL: " + message)
    quit(1)
