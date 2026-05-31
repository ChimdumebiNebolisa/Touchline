extends SceneTree

const VIEWPORT := Vector2i(1280, 720)

var _stage := 0
var _ticks := 0
var _scene_index := 0

var _scenes := [
	{"path": "res://scenes/MainMenu.tscn", "scroll": "MenuScroll", "extra": "menu"},
	{"path": "res://scenes/CareerSetup.tscn", "scroll": "FormScroll", "extra": "career"},
	{"path": "res://scenes/ClubDashboard.tscn", "scroll": "MainScroll", "extra": "dashboard"},
	{"path": "res://scenes/PostMatchScene.tscn", "scroll": "MainScroll", "extra": "postmatch"},
	{"path": "res://scenes/PlayerProfile.tscn", "scroll": "MainScroll", "extra": "profile"},
]

func _initialize() -> void:
	var world_generator := root.get_node("WorldGenerator")
	var game_state := root.get_node("GameState")
	if world_generator != null:
		world_generator.BeginNewCareer("UI Rebuild Audit", 909090, "Manager", "Unknown Upstart", "National C License")
		world_generator.SelectClub("Riverton Athletic")
	if game_state != null:
		game_state.ResolveCurrentMatchInstantly()

func _process(_delta: float) -> bool:
	_ticks += 1
	if _stage == 0 and _ticks > 2:
		_open_next()
	elif _stage == 1 and _ticks > 3:
		_validate_current()
	return false

func _open_next() -> void:
	if _scene_index >= _scenes.size():
		print("AUDIT_UI_REBUILD_LAYOUT_PASS")
		quit()
		return

	var entry: Dictionary = _scenes[_scene_index]
	var err := change_scene_to_file(str(entry.path))
	if err != OK:
		_fail("Could not open %s" % entry.path)
		return

	_stage = 1
	_ticks = 0

func _validate_current() -> void:
	if current_scene == null:
		_fail("Scene did not load")
		return

	var viewport := current_scene.get_viewport()
	if viewport != null:
		viewport.size = VIEWPORT

	var entry: Dictionary = _scenes[_scene_index]
	var scroll_name := str(entry.scroll)
	var scroll := current_scene.find_child(scroll_name, true, false) as ScrollContainer
	if scroll == null and scroll_name != "MenuScroll":
		_fail("%s missing %s" % [current_scene.name, scroll_name])
		return

	match str(entry.extra):
		"career":
			_validate_career_setup()
		"dashboard":
			_validate_dashboard()
		"postmatch":
			_validate_post_match()
		"profile":
			_validate_player_profile()
		"menu":
			pass

	_scene_index += 1
	_stage = 0
	_ticks = 0

func _validate_career_setup() -> void:
	var start := current_scene.get_node_or_null(
		"RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/ActionsRow/StartCareerButton"
	)
	if start == null:
		_fail("Career setup pinned actions missing")

func _validate_dashboard() -> void:
	var main_column := current_scene.get_node_or_null("RootMargin/Shell/MainColumn")
	if main_column == null:
		_fail("Dashboard main column missing")
		return
	var summary := main_column.get_node_or_null("SummaryGrid")
	var scroll := main_column.get_node_or_null("MainScroll")
	if summary == null or scroll == null or summary.get_index() >= scroll.get_index():
		_fail("Dashboard summary grid must precede MainScroll")

func _validate_post_match() -> void:
	var score := _label_text(
		"RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ScoreLabel"
	)
	var stats := _label_text(
		"RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/StatsLabel"
	)
	var pressure := _label_text(
		"RootMargin/MainColumn/MainScroll/ScrollContent/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/ConsequencesScroll/ConsequencesStack/PressureLabel"
	)
	var next_step := _label_text("RootMargin/MainColumn/ActionCard/ActionPadding/ActionContent/NextStepLabel")
	if score == "0 - 0":
		_fail("Post-match score not populated for audit")
	if stats.find("Key stats") == -1:
		_fail("Post-match stats missing")
	if pressure.find("Pressure") == -1:
		_fail("Post-match pressure block missing")
	if next_step.find("Next action") == -1 and next_step.find("Advance") == -1:
		_fail("Post-match next action missing")

func _validate_player_profile() -> void:
	for label_name in ["ProfileConfidenceLabel", "KnownLabel", "EstimatedLabel", "UnknownLabel"]:
		if current_scene.find_child(label_name, true, false) == null:
			_fail("Player profile missing %s" % label_name)

func _label_text(path: String) -> String:
	var node := current_scene.get_node_or_null(path) as Label
	return node.text if node != null else ""

func _fail(message: String) -> void:
	push_error(message)
	print("AUDIT_UI_REBUILD_LAYOUT_FAIL: " + message)
	quit(1)
