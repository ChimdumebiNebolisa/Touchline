extends SceneTree

var _ticks := 0
var _checked := false

func _initialize() -> void:
    if root.get_node("GameState") == null:
        _fail("GameState singleton missing")

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Regression Check", 990011)
    game_state.SelectClub("Riverton Athletic")

    var validation_message := str(game_state.ValidateMultiMatchRegressionContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("STEP44_MULTI_MATCH_REGRESSION_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP44_MULTI_MATCH_REGRESSION_FAIL: " + message)
    quit(1)
