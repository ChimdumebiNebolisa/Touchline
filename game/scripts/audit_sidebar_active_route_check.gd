extends SceneTree

var _stage := 0
var _ticks := 0
var _scene_index := 0

var _scenes := [
	{"path": "res://scenes/ClubDashboard.tscn", "selected": "DashboardButton"},
	{"path": "res://scenes/SquadScreen.tscn", "selected": "SquadButton"},
	{"path": "res://scenes/TacticsScreen.tscn", "selected": "TacticsButton"},
	{"path": "res://scenes/FixturesScreen.tscn", "selected": "FixturesButton"},
	{"path": "res://scenes/StandingsScreen.tscn", "selected": "StandingsButton"},
	{"path": "res://scenes/ClubDashboard.tscn", "selected": "DashboardButton"}
]

func _process(_delta: float) -> bool:
	_ticks += 1

	if _stage == 0 and _ticks > 2:
		_start_flow()
	elif _stage == 1 and _ticks > 2:
		_validate_scene()

	return false

func _start_flow() -> void:
	var world_generator := root.get_node("WorldGenerator")
	if not world_generator.BeginNewCareer("Audit Rail Check", 808101, "Manager", "Unknown Upstart", "National C License"):
		_fail(world_generator.LastStatusMessage)
		return
	if not world_generator.SelectClub("Riverton Athletic"):
		_fail(world_generator.LastStatusMessage)
		return
	_open_next_scene()

func _open_next_scene() -> void:
	if _scene_index >= _scenes.size():
		print("AUDIT_SIDEBAR_ACTIVE_ROUTE_PASS")
		quit()
		return

	var err := change_scene_to_file(_scenes[_scene_index]["path"])
	if err != OK:
		_fail("Could not open scene: %s" % _scenes[_scene_index]["path"])
		return

	_stage = 1
	_ticks = 0

func _validate_scene() -> void:
	var nav_root := "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/"
	var selected_button := current_scene.get_node_or_null(nav_root + _scenes[_scene_index]["selected"]) as Button
	var matchday_button := current_scene.get_node_or_null(nav_root + "MatchdayButton") as Button
	if selected_button == null or matchday_button == null:
		_fail("Navigation buttons missing on %s" % current_scene.name)
		return

	if not selected_button.disabled:
		_fail("Expected %s to be disabled/selected on %s" % [_scenes[_scene_index]["selected"], current_scene.name])
		return

	for button_name in ["DashboardButton", "SquadButton", "TacticsButton", "FixturesButton", "StandingsButton"]:
		var button := current_scene.get_node_or_null(nav_root + button_name) as Button
		if button == null:
			_fail("Missing navigation button %s" % button_name)
			return
		if button_name != _scenes[_scene_index]["selected"] and button.disabled:
			_fail("Unexpected disabled nav button %s on %s" % [button_name, current_scene.name])
			return

	if matchday_button.text != "Go to Matchday":
		_fail("Matchday CTA text is wrong on %s: %s" % [current_scene.name, matchday_button.text])
		return

	if matchday_button.disabled:
		_fail("Matchday CTA should not be selected on %s" % current_scene.name)
		return

	_scene_index += 1
	_open_next_scene()

func _fail(message: String) -> void:
	push_error(message)
	print("AUDIT_SIDEBAR_ACTIVE_ROUTE_FAIL: " + message)
	quit(1)
