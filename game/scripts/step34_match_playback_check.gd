extends SceneTree

func _initialize() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    game_state.StartNewCareer("Playback Check", 112233)
    game_state.SelectClub("Riverton Athletic")

    var validation_message := str(game_state.ValidateCurrentMatchPlaybackContract())
    if validation_message != "OK":
        _fail(validation_message)
        return

    print("STEP34_MATCH_PLAYBACK_PASS")
    quit()

func _fail(message: String) -> void:
    push_error(message)
    print("STEP34_MATCH_PLAYBACK_FAIL: " + message)
    quit(1)
