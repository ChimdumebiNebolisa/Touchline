extends SceneTree

var _stage := 0
var _ticks := 0
var _instant_scoreline := ""

func _initialize() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    game_state.StartNewCareer("Engine Check", 112233)
    game_state.SelectClub("Riverton Athletic")

    var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
    if err != OK:
        _fail("unable to load MatchdayScene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not load for instant route")
            return false

        var instant_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/InstantResultButton") as Button
        if instant_button == null:
            _fail("InstantResultButton missing on MatchdayScene")
            return false

        instant_button.emit_signal("pressed")
        _stage = 1
        _ticks = 0

    elif _stage == 1 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("Instant route did not open PostMatchScene")
            return false

        var score_label := current_scene.get_node("Center/Panel/ScoreLabel") as Label
        if score_label == null:
            _fail("Post-match score label missing after instant route")
            return false

        _instant_scoreline = score_label.text

        var game_state := root.get_node("GameState")
        game_state.StartNewCareer("Engine Check", 112233)
        game_state.SelectClub("Riverton Athletic")

        var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
        if err != OK:
            _fail("unable to reload MatchdayScene for live route")
            return false

        _stage = 2
        _ticks = 0

    elif _stage == 2 and _ticks > 2:
        if current_scene == null or current_scene.name != "MatchdayScene":
            _fail("MatchdayScene did not reload for live route")
            return false

        var live_button := current_scene.get_node("Center/Shell/Padding/Content/ActionsRow/StartMatchButton") as Button
        if live_button == null:
            _fail("StartMatchButton missing on MatchdayScene")
            return false

        live_button.emit_signal("pressed")
        _stage = 3
        _ticks = 0

    elif _stage == 3 and _ticks > 2:
        if current_scene == null or current_scene.name != "LiveMatchScene":
            _fail("Live route did not open LiveMatchScene")
            return false

        _stage = 4
        _ticks = 0

    elif _stage == 4:
        if current_scene == null or current_scene.name != "LiveMatchScene":
            _fail("LiveMatchScene closed before playback finished")
            return false

        var back_button := current_scene.get_node("Margin/Root/ContentRow/SidebarCard/SidebarPadding/SidebarContent/BackButton") as Button
        if back_button == null:
            _fail("Back button missing on LiveMatchScene")
            return false

        if back_button.text == "Continue to Post-Match":
            back_button.emit_signal("pressed")
            _stage = 5
            _ticks = 0

    elif _stage == 5 and _ticks > 2:
        if current_scene == null or current_scene.name != "PostMatchScene":
            _fail("Live route did not hand off to PostMatchScene")
            return false

        var score_label := current_scene.get_node("Center/Panel/ScoreLabel") as Label
        if score_label == null:
            _fail("Post-match score label missing after live route")
            return false

        if score_label.text != _instant_scoreline:
            _fail("Live and instant routes produced different final scores: %s vs %s" % [_instant_scoreline, score_label.text])
            return false

        print("STEP22_SHARED_ENGINE_PASS")
        quit()

    return false

func _fail(message: String) -> void:
    push_error(message)
    print("STEP22_SHARED_ENGINE_FAIL: " + message)
    quit(1)
