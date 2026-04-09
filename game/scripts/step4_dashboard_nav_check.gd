extends SceneTree

var _stage := 0
var _ticks := 0
var _route_index := 0

var _routes := [
    {"button": "SquadButton", "scene": "SquadScreen"},
    {"button": "TacticsButton", "scene": "TacticsScreen"},
    {"button": "FixturesButton", "scene": "FixturesScreen"},
    {"button": "StandingsButton", "scene": "StandingsScreen"},
    {"button": "MatchdayButton", "scene": "MatchdayScene"}
]

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("unable to load CareerSetup scene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        if current_scene == null:
            _fail("CareerSetup scene did not load")
            return false

        var name_input := current_scene.get_node("Center/Panel/ManagerNameInput") as LineEdit
        var seed_input := current_scene.get_node("Center/Panel/SeedInput") as SpinBox
        var start_button := current_scene.get_node("Center/Panel/StartCareerButton") as Button

        if name_input == null or seed_input == null or start_button == null:
            _fail("CareerSetup controls are missing")
            return false

        name_input.text = "Jordan Miles"
        seed_input.value = 987654
        start_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "ChooseClub":
            _fail("Career handoff did not open ChooseClub")
            return false

        var club_list := current_scene.get_node("Center/Panel/ClubList") as ItemList
        var confirm_button := current_scene.get_node("Center/Panel/ConfirmSelectionButton") as Button

        if club_list == null or confirm_button == null:
            _fail("ChooseClub controls are missing")
            return false

        club_list.select(0)
        confirm_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("ClubDashboard did not load")
            return false

        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Expected ClubDashboard before route navigation")
            return false

        var route = _routes[_route_index]
        var button_path = "Center/Panel/" + route.button
        var nav_button := current_scene.get_node(button_path) as Button
        if nav_button == null:
            _fail("Dashboard button missing: " + route.button)
            return false

        nav_button.emit_signal("pressed")
        _stage = 4
        _ticks = 0

    elif _stage == 4 and _ticks > 2:
        var route = _routes[_route_index]
        if current_scene == null or current_scene.name != route.scene:
            _fail("Navigation failed for route to " + route.scene)
            return false

        var back_button := current_scene.get_node("Center/Panel/BackButton") as Button
        if back_button == null:
            _fail("Back button missing on " + route.scene)
            return false

        back_button.emit_signal("pressed")
        _stage = 5
        _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Back navigation did not return to ClubDashboard")
            return false

        _route_index += 1
        if _route_index >= _routes.size():
            print("STEP4_SUBTASK_PASS")
            quit()
            return false

        _stage = 3
        _ticks = 0

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP4_SUBTASK_FAIL: " + message)
    quit(1)
