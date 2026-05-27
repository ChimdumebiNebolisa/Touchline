extends SceneTree

const ROLES := ["Assistant Manager", "Head Coach", "Manager"]
const CLUB_NAME := "Riverton Athletic"

var _checked := false

func _process(_delta: float) -> bool:
    if _checked:
        return false
    _checked = true

    var world_generator := root.get_node("WorldGenerator")
    var game_state := root.get_node("GameState")
    var save_system := root.get_node("SaveSystem")
    if world_generator == null or game_state == null or save_system == null:
        _fail("Required autoloads are missing")
        return false

    for role in ROLES:
        if not world_generator.BeginNewCareer("Active Playtest", 995500 + ROLES.find(role), role, "Unknown Upstart", "National C License"):
            _fail(world_generator.LastStatusMessage)
            return false
        if not world_generator.SelectClub(CLUB_NAME):
            _fail(world_generator.LastStatusMessage)
            return false

        var authority := str(game_state.ValidateRoleAuthorityStabilizationContract())
        if authority != "OK":
            _fail("%s authority contract failed: %s" % [role, authority])
            return false

        var shared_engine := str(game_state.ValidateStage5MatchEngineAlignmentContract())
        if shared_engine != "OK":
            _fail("%s shared engine contract failed: %s" % [role, shared_engine])
            return false

        if not save_system.TrySaveGame():
            _fail("Save failed for %s: %s" % [role, save_system.LastStatusMessage])
            return false
        if not save_system.TryLoadGame():
            _fail("Load failed for %s: %s" % [role, save_system.LastStatusMessage])
            return false

        if String(game_state.CurrentRoleName) != role:
            _fail("Role mismatch after reload for %s: %s" % [role, game_state.CurrentRoleName])
            return false

    print("ACTIVE_PLAYTEST_USER_FLOW_PASS")
    quit()
    return false

func _fail(message: String) -> void:
    push_error(message)
    print("ACTIVE_PLAYTEST_USER_FLOW_FAIL: " + message)
    quit(1)
