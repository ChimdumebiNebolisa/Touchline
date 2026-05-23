extends SceneTree

var _ticks := 0
var _checked := false

func _initialize() -> void:
    pass

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    if current_scene == null:
        _checked = true
        _start_matchday_context()
        return false

    _checked = true
    _validate_matchday_context()
    return false

func _start_matchday_context() -> void:
    var world_generator := root.get_node("WorldGenerator")
    if world_generator == null:
        _fail("WorldGenerator singleton missing")
        return

    if not world_generator.BeginNewCareer("Matchday Preparation Check", 494950):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return

    var err := change_scene_to_file("res://scenes/MatchdayScene.tscn")
    if err != OK:
        _fail("Could not open MatchdayScene")
        return

    _checked = false
    _ticks = 0

func _validate_matchday_context() -> void:
    if current_scene == null or current_scene.name != "MatchdayScene":
        _fail("MatchdayScene did not load")
        return

    var competition_label := current_scene.get_node("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CompetitionLabel") as Label
    var fixture_label := current_scene.get_node("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/FixtureLabel") as Label
    var kickoff_context := current_scene.get_node("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/KickoffContextLabel") as Label
    var lineup_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/LineupLabel") as Label
    var pressure_reasons := current_scene.get_node("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/PressureReasonsLabel") as Label
    var tactics_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/TacticsLabel") as Label
    var opponent_focus := current_scene.get_node("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/OpponentFocusLabel") as Label
    var status_label := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StatusLabel") as Label
    var live_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton") as Button
    var instant_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton") as Button

    if competition_label == null or fixture_label == null or kickoff_context == null or lineup_label == null or pressure_reasons == null or tactics_label == null or opponent_focus == null or status_label == null or live_button == null or instant_button == null:
        _fail("Matchday preparation labels are missing")
        return

    if competition_label.text.find("Season") == -1:
        _fail("Matchday competition label does not include season: %s" % competition_label.text)
        return

    if fixture_label.text.find("Matchday") == -1:
        _fail("Matchday fixture label does not include matchday: %s" % fixture_label.text)
        return

    if kickoff_context.text.find("Result feeds table") == -1:
        _fail("Matchday kickoff context does not explain career effects: %s" % kickoff_context.text)
        return

    if lineup_label.text.find("Lineup readiness") == -1:
        _fail("Matchday lineup readiness summary is missing: %s" % lineup_label.text)
        return

    if pressure_reasons.text.find("Board:") == -1 or pressure_reasons.text.find("Fans:") == -1 or pressure_reasons.text.find("Dressing room:") == -1:
        _fail("Matchday pressure context is incomplete: %s" % pressure_reasons.text)
        return

    if tactics_label.text.find("Tactical setup") == -1:
        _fail("Matchday tactical setup summary is missing: %s" % tactics_label.text)
        return

    if opponent_focus.text.find("Opponent context") == -1 or opponent_focus.text.find("seeded XI") == -1:
        _fail("Matchday opponent context is incomplete: %s" % opponent_focus.text)
        return

    if status_label.text.find("Live Match") == -1 or status_label.text.find("Instant Result") == -1 or status_label.text.find("same engine") == -1:
        _fail("Matchday live-vs-instant choice is unclear: %s" % status_label.text)
        return

    if live_button.disabled or instant_button.disabled:
        _fail("Matchday action buttons are disabled for an open fixture")
        return

    print("STEP49_MATCHDAY_PREPARATION_PASS")
    quit()

func _fail(message: String) -> void:
    push_error(message)
    print("STEP49_MATCHDAY_PREPARATION_FAIL: " + message)
    quit(1)
