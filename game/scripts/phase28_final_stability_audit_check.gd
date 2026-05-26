extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    var game_state := root.get_node("GameState")
    var world_generator := root.get_node("WorldGenerator")
    if game_state == null or world_generator == null:
        _fail("Required autoloads missing")
        return false

    if not world_generator.BeginNewCareer("Phase 28 Audit Check", 282828, "Manager", "Tactical Specialist", "National A License"):
        _fail(world_generator.LastStatusMessage)
        return false
    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return false

    var validation_message := str(game_state.ValidatePhase28StabilityAuditContract())
    if validation_message != "OK":
        _fail(validation_message)
        return false

    print("PHASE28_FINAL_STABILITY_AUDIT_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("PHASE28_FINAL_STABILITY_AUDIT_FAIL: " + message)
    quit(1)
