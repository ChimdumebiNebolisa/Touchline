extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    _run_final_smoke()
    return false

func _run_final_smoke() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    _assert_contract(game_state, "match playback", "ValidateCurrentMatchPlaybackContract", 575701)
    _assert_contract(game_state, "opponent squad sourcing", "ValidateOpponentSquadSourcing", 575702)
    _assert_contract(game_state, "match variation", "ValidateMatchVariationContract", 575703)
    _assert_contract(game_state, "post-match report", "ValidatePostMatchReportContract", 575704)
    _assert_contract(game_state, "matchday progression", "ValidateMatchdayProgressionContract", 575705)
    _assert_contract(game_state, "player condition", "ValidatePlayerConditionContract", 575706)
    _assert_contract(game_state, "multi-match progression", "ValidateMultiMatchRegressionContract", 575707)
    _assert_contract(game_state, "season rollover", "ValidateSeasonRolloverContract", 575708)
    _assert_contract(game_state, "season development", "ValidateSeasonDevelopmentContract", 575709)
    _assert_contract(game_state, "full season regression", "ValidateFullSeasonRegressionContract", 575710)

    print("STEP57_FINAL_REGRESSION_PASS")
    quit()

func _assert_contract(game_state: Node, label: String, method_name: String, seed: int) -> void:
    game_state.StartNewCareer("Final Regression Check", seed)
    game_state.SelectClub("Riverton Athletic")
    var result := str(game_state.call(method_name))
    if result != "OK":
        _fail("%s contract failed: %s" % [label, result])

func _fail(message: String) -> void:
    push_error(message)
    print("STEP57_FINAL_REGRESSION_FAIL: " + message)
    quit(1)
