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
        _fail("Required autoloads missing")
        return false

    if not world_generator.BeginNewCareer("Engine Check", 112233):
        _fail(world_generator.LastStatusMessage)
        return false

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidateStage5MatchEngineAlignmentContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("STEP22_SHARED_ENGINE_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP22_SHARED_ENGINE_FAIL: " + message)
    quit(1)
