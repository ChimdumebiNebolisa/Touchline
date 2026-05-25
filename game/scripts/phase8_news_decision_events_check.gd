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

    game_state.StartNewCareer("Phase 8 Event Check", 880008)
    game_state.SelectClub("Riverton Athletic")

    var validation_message := str(game_state.ValidatePhase8DecisionEventsContract())
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

    game_state.StartNewCareer("Phase 8 Mutation", 880108)
    game_state.SelectClub("Harbor County")
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return false

    validation_message = str(game_state.ValidatePhase8StoredDecisionEventsContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("PHASE8_NEWS_DECISION_EVENTS_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE8_NEWS_DECISION_EVENTS_FAIL: " + message)
    quit(1)
