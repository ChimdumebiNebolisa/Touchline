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

    var world_generator := root.get_node("WorldGenerator")
    if world_generator == null:
        _fail("WorldGenerator singleton missing")
        return false

    for role in ["Assistant Manager", "Head Coach", "Manager"]:
        if not world_generator.BeginNewCareer("Stage 7 Check", 620007, role, "Unknown Upstart", "National C License"):
            _fail(world_generator.LastStatusMessage)
            return false
        if not world_generator.SelectClub("Riverton Athletic"):
            _fail(world_generator.LastStatusMessage)
            return false
        var result := str(game_state.ValidateStage7RecruitmentContract())
        if result != "OK":
            _fail("%s: %s" % [role, result])
            return false

    print("STAGE7_RECRUITMENT_CONTRACTS_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STAGE7_RECRUITMENT_CONTRACTS_FAIL: " + message)
    quit(1)
