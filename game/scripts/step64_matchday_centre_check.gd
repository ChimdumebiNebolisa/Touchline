extends SceneTree

var _stage := 0
var _ticks := 0

func _process(_delta: float) -> bool:
    _ticks += 1
    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_matchday()
    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Match Centre Check", 646466)
    game_state.SelectClub("Riverton Athletic")
    var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
    if err != OK:
        _fail("Could not open MatchdayScene")
        return
    _stage = 1
    _ticks = 0

func _validate_matchday() -> void:
    var fixture := _label_text("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/FixtureLabel")
    var competition := _label_text("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CompetitionLabel")
    var brief := _label_text("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/KickoffContextLabel")
    var team_news := _label_text("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/LineupLabel")
    var bench := _label_text("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/BenchLabel")
    var club_mood := _label_text("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/PressureReasonsLabel")
    var plan := _label_text("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/TacticsLabel")
    var controls := _label_text("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StatusLabel")
    var live_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton") as Button
    var instant_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton") as Button
    var back_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/BackButton") as Button

    if fixture.find("vs") == -1 or competition.find("Matchday") == -1:
        _fail("Compact fixture header is missing")
        return

    for token in ["Dressing Room Brief", "Team News", "Club Mood", "Match Plan", "Match Controls"]:
        var haystack := " ".join([brief, team_news, bench, club_mood, plan, controls])
        if haystack.find(token) == -1:
            _fail("Matchday centre missing section token %s" % token)
            return

    if live_button == null or instant_button == null or back_button == null:
        _fail("Match controls are missing")
        return

    if live_button.text != "Watch Live Match" or instant_button.text != "Instant Result" or back_button.text != "Back to Manager Hub":
        _fail("Match controls are not football-framed")
        return

    print("STEP64_MATCHDAY_CENTRE_PASS")
    quit()

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
        return ""
    return node.text

func _fail(message: String) -> void:
    push_error(message)
    print("STEP64_MATCHDAY_CENTRE_FAIL: " + message)
    quit(1)
