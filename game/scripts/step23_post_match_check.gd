extends SceneTree

var _stage := 0
var _ticks := 0
var _bootstrapped := false

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("unable to load runtime scene for post-match bootstrap")

func _process(_delta: float) -> bool:
    _ticks += 1

    if not _bootstrapped and _ticks > 2:
        var world_generator := root.get_node("WorldGenerator")
        if world_generator == null:
            _fail("WorldGenerator singleton missing")
            return false

        if not world_generator.BeginNewCareer("Post Match Check", 445566):
            _fail(world_generator.LastStatusMessage)
            return false

        if not world_generator.SelectClub("Riverton Athletic"):
            _fail(world_generator.LastStatusMessage)
            return false

        var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
        if err != OK:
            _fail("unable to load MatchdayScene")
            return false

        _bootstrapped = true
        _ticks = 0
        return false

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load")
            return false

        var instant_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton") as Button
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

        var table_impact := current_scene.get_node("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TableImpactLabel") as Label
        var tactical := current_scene.get_node("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TacticalLabel") as Label
        var pressure := current_scene.get_node("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/PressureLabel") as Label
        var continue_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ContinueButton") as Button

        if table_impact == null or tactical == null or pressure == null or continue_button == null:
            _fail("Post-match consequence controls are missing")
            return false

        if table_impact.text.find("unavailable") != -1 or tactical.text.find("unavailable") != -1 or pressure.text.find("unavailable") != -1:
            if _ticks <= 10:
                return false

            _fail("Post-match consequence labels still contain placeholder content")
            return false

        continue_button.emit_signal("pressed")
        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "ClubDashboard":
            if _ticks <= 10:
                return false

            _fail("Continue flow did not return to ClubDashboard")
            return false

        var date_label := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/DateLabel") as Label
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
