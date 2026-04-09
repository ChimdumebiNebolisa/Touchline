extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("Could not load a runtime scene for the Step 28 rollover check")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        var world_generator := root.get_node("WorldGenerator")
        var calendar_system := root.get_node("CalendarSystem")
        var game_state := root.get_node("GameState")
        if world_generator == null or calendar_system == null or game_state == null:
            _fail("Required autoloads are missing for the Step 28 rollover check")
            return false

        if not world_generator.BeginNewCareer("Rollover Check", 828282):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Eastvale Rovers"):
            _fail(world_generator.LastStatusMessage)
            return false

        for _index in range(6):
            if not calendar_system.AdvanceCareerDate():
                _fail(calendar_system.LastStatusMessage)
                return false

        game_state.SelectPlayerProfile("Mikel Duarte")
        var err := change_scene_to_file("res://scenes/PlayerProfile.tscn")
        if err != OK:
            _fail("Could not open PlayerProfile after season rollover")
            return false

        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "PlayerProfile":
            _fail("PlayerProfile did not load after season rollover")
            return false

        var game_state := root.get_node("GameState")
        if game_state == null:
            _fail("GameState missing during rollover verification")
            return false

        var identity_label := current_scene.get_node("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel") as Label
        var role_label := current_scene.get_node("Center/Panel/Padding/Content/ProfileCard/ProfilePadding/ProfileContent/RoleLabel") as Label
        if identity_label == null or role_label == null:
            _fail("PlayerProfile labels missing during rollover verification")
            return false

        if game_state.SeasonStartYear != 2027 or game_state.CurrentMatchday != 1:
            _fail("Season rollover did not reset the timeline into the next campaign")
            return false

        if String(game_state.CurrentDateLabel).find("3 Aug 2027") == -1:
            _fail("Season rollover did not reset the date to the new campaign start")
            return false

        if game_state.FormSummary != "Form: new season reset.":
            _fail("Season rollover did not reset the visible form summary")
            return false

        if identity_label.text.find("Age 20") == -1:
            _fail("Player age did not advance across season rollover")
            return false

        if role_label.text.find("form 77") != -1:
            _fail("Player development did not change visible squad state across season rollover")
            return false

        print("STEP28_SEASON_ROLLOVER_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP28_SEASON_ROLLOVER_FAIL: " + message)
    quit(1)
