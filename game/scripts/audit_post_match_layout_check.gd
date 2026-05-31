extends SceneTree

var _ticks := 0
var _stage := 0

func _process(_delta: float) -> bool:
	_ticks += 1
	if _stage == 0 and _ticks > 2:
		_start_flow()
	elif _stage == 1 and _ticks > 3:
		_validate_post_match()
	return false

func _start_flow() -> void:
	var world_generator := root.get_node("WorldGenerator")
	var game_state := root.get_node("GameState")
	if not world_generator.BeginNewCareer("Audit Post Match", 808303, "Manager", "Unknown Upstart", "National C License"):
		_fail(world_generator.LastStatusMessage)
		return
	if not world_generator.SelectClub("Riverton Athletic"):
		_fail(world_generator.LastStatusMessage)
		return

	game_state.ResolveCurrentMatchInstantly()
	var err := change_scene_to_file("res://scenes/PostMatchScene.tscn")
	if err != OK:
		_fail("Could not open PostMatchScene")
		return

	_stage = 1
	_ticks = 0

func _validate_post_match() -> void:
	var score := _label_text("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ScoreLabel")
	var stats := _label_text("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/StatsLabel")
	var tactical := _label_text("RootMargin/MainColumn/MainScroll/ScrollContent/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/ConsequencesScroll/ConsequencesStack/TacticalLabel")
	var pressure := _label_text("RootMargin/MainColumn/MainScroll/ScrollContent/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/ConsequencesScroll/ConsequencesStack/PressureLabel")
	var next_step := _label_text("RootMargin/MainColumn/ActionCard/ActionPadding/ActionContent/NextStepLabel")
	var continue_hint := _label_text("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ContinueHintLabel")

	if score == "0 - 0":
		_fail("Post-match score was not populated")
		return

	for token in ["Key stats", "Shots", "Saves"]:
		if stats.find(token) == -1:
			_fail("Post-match stats block missing token %s" % token)
			return

	for token in ["Tactical review", "Tactical read |"]:
		if tactical.find(token) == -1:
			_fail("Post-match tactical block missing token %s" % token)
			return

	if pressure.find("Pressure + reactions") == -1:
		_fail("Post-match pressure block missing reaction heading")
		return

	if next_step.find("Next action |") == -1:
		_fail("Post-match next action is not explicit")
		return

	if continue_hint.find("Review the aftermath below") == -1:
		_fail("Post-match continue hint was not updated")
		return

	var consequences_scroll := current_scene.get_node_or_null("RootMargin/MainColumn/MainScroll/ScrollContent/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/ConsequencesScroll") as ScrollContainer
	var events_scroll := current_scene.get_node_or_null("RootMargin/MainColumn/MainScroll/ScrollContent/ContentRow/EventsCard/EventsPadding/EventsContent/EventsScroll") as ScrollContainer
	if consequences_scroll == null or events_scroll == null:
		_fail("Scrollable post-match containers are missing")
		return

	print("AUDIT_POST_MATCH_LAYOUT_PASS")
	quit()

func _label_text(path: String) -> String:
	var node := current_scene.get_node_or_null(path) as Label
	if node == null:
		_fail("Missing label: %s" % path)
		return ""
	return node.text

func _fail(message: String) -> void:
	push_error(message)
	print("AUDIT_POST_MATCH_LAYOUT_FAIL: " + message)
	quit(1)
