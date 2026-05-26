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
        _fail("Required autoloads are missing for the Step 28 rollover check")
        return false

    if not world_generator.BeginNewCareer("Rollover Check", 828282):
        _fail(world_generator.LastStatusMessage)
        return false

    if not world_generator.SelectClub("Eastvale Rovers"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidateSeasonRolloverContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("STEP28_SEASON_ROLLOVER_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP28_SEASON_ROLLOVER_FAIL: " + message)
    quit(1)
