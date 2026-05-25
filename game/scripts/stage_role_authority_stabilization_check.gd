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
        _fail("Required autoloads are missing")
        return false

    for role in ["Assistant Manager", "Head Coach", "Manager"]:
        if not world_generator.BeginNewCareer("Role Audit", 730001, role, "Unknown Upstart", "National C License"):
            _fail(world_generator.LastStatusMessage)
            return false
        if not world_generator.SelectClub("Northbridge City"):
            _fail(world_generator.LastStatusMessage)
            return false

        var result := str(game_state.ValidateRoleAuthorityStabilizationContract())
        if result != "OK":
            _fail("%s: %s" % [role, result])
            return false

    print("STAGE_ROLE_AUTHORITY_STABILIZATION_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STAGE_ROLE_AUTHORITY_STABILIZATION_FAIL: " + message)
    quit(1)
