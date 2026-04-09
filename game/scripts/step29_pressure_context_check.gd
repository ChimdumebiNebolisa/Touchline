extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("Could not load a runtime scene for the Step 29 pressure check")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        var world_generator := root.get_node("WorldGenerator")
        if world_generator == null:
            _fail("WorldGenerator autoload missing for the Step 29 pressure check")
            return false

        if not world_generator.BeginNewCareer("Pressure Check", 939393):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Northbridge City"):
            _fail(world_generator.LastStatusMessage)
            return false

        var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
        if err != OK:
            _fail("Could not open ClubDashboard for the Step 29 pressure check")
            return false

        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load for the Step 29 pressure check")
            return false

        var pressure_reasons := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/SnapshotCard/SnapshotPadding/SnapshotContent/PressureReasonsLabel") as Label
        var matchday_button := current_scene.get_node("Center/Shell/Padding/Content/DashboardRow/OperationsCard/OperationsPadding/OperationsContent/ActionsGrid/MatchdayButton") as Button
        if pressure_reasons == null or matchday_button == null:
            _fail("Dashboard pressure controls are missing")
            return false

        if pressure_reasons.text.find("Board:") == -1 or pressure_reasons.text.find("Fans:") == -1 or pressure_reasons.text.find("Dressing room:") == -1:
            _fail("Dashboard does not surface the richer pressure reasons: %s" % pressure_reasons.text)
            return false

        matchday_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load for the Step 29 pressure check")
            return false

        var pressure_reasons := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/PressureReasonsLabel") as Label
        if pressure_reasons == null:
            _fail("Matchday pressure reasons label is missing")
            return false

        if pressure_reasons.text.find("Board:") == -1 or pressure_reasons.text.find("Fans:") == -1 or pressure_reasons.text.find("Dressing room:") == -1:
            _fail("Matchday does not surface the richer pressure reasons: %s" % pressure_reasons.text)
            return false

        print("STEP29_PRESSURE_CONTEXT_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP29_PRESSURE_CONTEXT_FAIL: " + message)
    quit(1)
