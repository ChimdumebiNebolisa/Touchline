extends SceneTree

var _stage := 0
var _ticks := 0

func _process(_delta: float) -> bool:
	_ticks += 1

	if _stage == 0 and _ticks > 2:
		_start_flow()
	elif _stage == 1 and _ticks > 2:
		_validate_squad()
	elif _stage == 2 and _ticks > 2:
		_validate_profile()

	return false

func _start_flow() -> void:
	var world_generator := root.get_node("WorldGenerator")
	if not world_generator.BeginNewCareer("Audit Partial Info", 808202, "Manager", "Unknown Upstart", "National C License"):
		_fail(world_generator.LastStatusMessage)
		return
	if not world_generator.SelectClub("Riverton Athletic"):
		_fail(world_generator.LastStatusMessage)
		return

	var err := change_scene_to_file("res://scenes/SquadScreen.tscn")
	if err != OK:
		_fail("Could not open SquadScreen")
		return

	_stage = 1
	_ticks = 0

func _validate_squad() -> void:
	var detail_meta := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/DetailMetaLabel")
	var profile_hint := _label_text("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ProfileHintLabel")
	for token in ["Profile Confidence:", "Visibility |", "Known:", "Estimated:", "Unknown:", "Tactical fit", "Personality", "Risk:"]:
		if detail_meta.find("Profile Confidence:") == -1 and token == "Profile Confidence:":
			_fail("Squad detail meta is missing profile confidence")
			return
		if profile_hint.find(token) == -1:
			_fail("Squad profile hint missing token %s" % token)
			return

	var profile_button := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/OpenProfileButton") as Button
	if profile_button == null or profile_button.disabled:
		_fail("Open profile button unavailable")
		return

	profile_button.emit_signal("pressed")
	_stage = 2
	_ticks = 0

func _validate_profile() -> void:
	if current_scene == null or current_scene.name != "PlayerProfile":
		_fail("PlayerProfile did not load")
		return

	var status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/StatusLabel")
	var identity := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel")
	var role := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/RoleLabel")
	var condition := _label_text("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/ConditionLabel")
	var pathway := _label_text("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/PathwayLabel")
	var readiness := _label_text("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/ReadinessLabel")

	for token in ["Profile Confidence:", "Known:", "Estimated:", "Unknown:", "Visibility |"]:
		if identity.find(token) == -1:
			_fail("Player profile identity block missing token %s" % token)
			return

	for token in ["Tactical fit", "Personality"]:
		if role.find(token) == -1:
			_fail("Player profile role block missing token %s" % token)
			return

	if condition.find("Risk:") == -1:
		_fail("Player profile condition block missing risk summary")
		return

	if pathway.find("Trajectory |") == -1:
		_fail("Player profile pathway block missing trajectory context")
		return

	if readiness.find("Scouting/staff note |") == -1:
		_fail("Player profile readiness block missing scouting/staff note")
		return

	if status.find("Profile Confidence:") == -1:
		_fail("Player profile header status missing profile confidence")
		return

	print("AUDIT_PARTIAL_INFORMATION_PASS")
	quit()

func _label_text(path: String) -> String:
	var node := current_scene.get_node_or_null(path) as Label
	if node == null:
		_fail("Missing label: %s" % path)
		return ""
	return node.text

func _fail(message: String) -> void:
	push_error(message)
	print("AUDIT_PARTIAL_INFORMATION_FAIL: " + message)
	quit(1)
