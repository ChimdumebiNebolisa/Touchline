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

    game_state.StartNewCareer("Stage 3 Check", 620003)
    game_state.SelectClub("Riverton Athletic")
    var result := str(game_state.ValidateStage3TacticsFoundationContract())
    if result != "OK":
        _fail(result)
        return false

    print("STAGE3_TACTICS_FOUNDATION_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STAGE3_TACTICS_FOUNDATION_FAIL: " + message)
    quit(1)
