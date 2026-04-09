extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    game_state.StartNewCareer("Post Match Check", 445566)
    game_state.SelectClub("Riverton Athletic")

    var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
    if err != OK:
        _fail("unable to load MatchdayScene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load")
            return false

        var instant_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/InstantResultButton") as Button
        if instant_button == null:
            _fail("InstantResultButton missing")
            return false

        instant_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("PostMatchScene did not load")
            return false

        var table_impact := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TableImpactLabel") as Label
        var tactical := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TacticalLabel") as Label
        var pressure := current_scene.get_node("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/PressureLabel") as Label
        var continue_button := current_scene.get_node("Center/Shell/Padding/Content/ContinueButton") as Button

        if table_impact == null or tactical == null or pressure == null or continue_button == null:
            _fail("Post-match consequence controls are missing")
            return false

        if table_impact.text.find("unavailable") != -1 or tactical.text.find("unavailable") != -1 or pressure.text.find("unavailable") != -1:
            _fail("Post-match consequence labels still contain placeholder content")
            return false

        continue_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            _fail("Continue flow did not return to ClubDashboard")
            return false

        var date_label := current_scene.get_node("Center/Shell/Padding/Content/Header/DateLabel") as Label
        if date_label == null:
            _fail("Dashboard date label missing after post-match continue")
            return false

        if date_label.text.find("10 Aug 2026") == -1:
            _fail("Post-match continue did not advance the calendar as expected")
            return false

        print("STEP23_POST_MATCH_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP23_POST_MATCH_FAIL: " + message)
    quit(1)
