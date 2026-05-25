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

    game_state.StartNewCareer("Stage 5 Check", 620005)
    game_state.SelectClub("Riverton Athletic")
    var result := str(game_state.ValidateStage5MatchEngineAlignmentContract())
    if result != "OK":
        _fail(result)
        return false

    print("STAGE5_MATCH_ALIGNMENT_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STAGE5_MATCH_ALIGNMENT_FAIL: " + message)
    quit(1)
