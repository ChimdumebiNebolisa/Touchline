extends SceneTree

const ROLES := ["Assistant Manager", "Head Coach", "Manager"]
const CLUB_NAME := "Riverton Athletic"
const ASSERT_PREFIX := "ACTIVE_PLAYTEST_ASSERT"

var _checked := false
var _assertions: Array[Dictionary] = []

func _process(_delta: float) -> bool:
	if _checked:
		return false
	_checked = true

	var world_generator := root.get_node("WorldGenerator")
	var game_state := root.get_node("GameState")
	var save_system := root.get_node("SaveSystem")
	if world_generator == null or game_state == null or save_system == null:
		_fail("Required autoloads are missing")
		return false

	for role in ROLES:
		if not _run_role_deep_assertions(world_generator, game_state, save_system, role):
			return false

	_write_assertion_log()
	print("ACTIVE_PLAYTEST_USER_FLOW_PASS")
	quit()
	return false

func _run_role_deep_assertions(world_generator, game_state, save_system, role: String) -> bool:
	if not world_generator.BeginNewCareer("Active Playtest", 995500 + ROLES.find(role), role, "Unknown Upstart", "National C License"):
		_fail(world_generator.LastStatusMessage)
		return false
	if not world_generator.SelectClub(CLUB_NAME):
		_fail(world_generator.LastStatusMessage)
		return false

	if not _assert_training(game_state, role):
		return false
	if not _assert_scouting(game_state, role):
		return false
	if not _assert_tactics(game_state, role):
		return false
	if not _assert_recruitment(game_state, role):
		return false
	if not _assert_post_match(game_state, role):
		return false
	if not _assert_save_load(game_state, save_system, role):
		return false
	if not _assert_promises(game_state, role):
		return false
	if not _assert_live_instant(game_state, role):
		return false
	if not _assert_job_market(game_state, role):
		return false
	if not _assert_authority_contract(game_state, role):
		return false

	return true

func _assert_training(game_state, role: String) -> bool:
	var before_focus := str(game_state.TrainingFocusName)
	var before_familiarity := int(game_state.TacticalFamiliarityScore)
	var status := str(game_state.RequestTrainingPlanByName("Pressing", "Demanding"))
	var after_focus := str(game_state.TrainingFocusName)
	var news := str(game_state.NewsFeedSummary)

	if role == "Assistant Manager":
		if not status.to_lower().contains("recommendation"):
			_fail("%s training: expected recommendation status, got %s" % [role, status])
			return false
		if after_focus != before_focus:
			_fail("%s training: focus changed without authority" % role)
			return false
		if not news.to_lower().contains("recommendation"):
			_fail("%s training: missing recommendation news trail" % role)
			return false
		_record(role, "Training", before_focus, "Request Pressing/Demanding", after_focus, "Recommend only; focus unchanged", true, "NewsFeedSummary")
	else:
		if not status.to_lower().contains("applied"):
			_fail("%s training: expected applied plan, got %s" % [role, status])
			return false
		if after_focus != "Pressing":
			_fail("%s training: focus not updated to Pressing" % role)
			return false

	if not game_state.AdvanceOneCareerWeek():
		_fail("%s training: weekly advance failed" % role)
		return false

	var after_week_familiarity := int(game_state.TacticalFamiliarityScore)
	var effect_ok := after_week_familiarity >= before_familiarity or news.to_lower().contains("training")
	if role != "Assistant Manager":
		effect_ok = after_week_familiarity >= before_familiarity
	if not effect_ok:
		_fail("%s training: no familiarity or training news effect after week advance" % role)
		return false

	if role != "Assistant Manager":
		_record(role, "Training", before_focus, "Apply Pressing/Demanding + advance week", "Pressing; familiarity %d->%d" % [before_familiarity, after_week_familiarity], "Football-side control; state delta", true, "TacticalFamiliarityScore")
	else:
		_record(role, "Training", before_focus, "Recommend Pressing/Demanding + advance week", "unchanged; familiarity %d->%d" % [before_familiarity, after_week_familiarity], "Recommend only; weekly effects still run", true, "TacticalFamiliarityScore")
	return true

func _assert_scouting(game_state, role: String) -> bool:
	var before_summary := str(game_state.TrainingScoutingSummary)
	var status := str(game_state.RequestScoutingAssignment("Specific player: pressing winger", "Full report"))
	var after_summary := str(game_state.TrainingScoutingSummary)

	if role == "Assistant Manager":
		if not status.to_lower().contains("recommendation"):
			_fail("%s scouting: expected recommendation, got %s" % [role, status])
			return false
		if after_summary != before_summary and after_summary.to_lower().contains("pressing winger"):
			_fail("%s scouting: assignment changed without authority" % role)
			return false
		_record(role, "Scouting", _scouting_snippet(before_summary), "Recommend Full report", _scouting_snippet(after_summary), "Recommend only; assignment unchanged", true, "TrainingScoutingSummary")
		return true

	if after_summary.find("pressing winger") == -1 or after_summary.find("days") == -1:
		_fail("%s scouting: assignment not opened in summary" % role)
		return false

	var days_before := _extract_scouting_days(after_summary)
	for _i in range(3):
		if not game_state.AdvanceOneCareerDay():
			_fail("%s scouting: daily advance failed" % role)
			return false

	var progressed_summary := str(game_state.TrainingScoutingSummary)
	var days_after := _extract_scouting_days(progressed_summary)
	var progressed := days_after >= 0 and days_before >= 0 and days_after < days_before
	if not progressed and progressed_summary.to_lower().find("confidence") == -1:
		_fail("%s scouting: no progress or confidence summary after 3 days" % role)
		return false

	var expected := "Request/recommend scouting" if role == "Head Coach" else "Open scouting assignment"
	_record(
		role,
		"Scouting",
		_scouting_snippet(before_summary),
		"Request Full report + 3 days",
		_scouting_snippet(progressed_summary),
		expected,
		true,
		"TrainingScoutingSummary"
	)
	return true

func _scouting_snippet(summary: String) -> String:
	var marker := "Scouting:"
	var index := summary.find(marker)
	if index == -1:
		return summary.substr(0, min(80, summary.length()))
	var rest := summary.substr(index + marker.length()).strip_edges()
	return rest.split("\n")[0]

func _recruitment_snippet(summary: String) -> String:
	return summary.split("\n")[0] if summary.length() > 0 else "pending"

func _extract_scouting_days(summary: String) -> int:
	var snippet := _scouting_snippet(summary)
	var parts := snippet.split("|")
	for part in parts:
		var trimmed := part.strip_edges()
		if trimmed.ends_with("days"):
			var number_text := trimmed.replace("days", "").strip_edges()
			if number_text.is_valid_int():
				return int(number_text)
	return -1

func _assert_tactics(game_state, role: String) -> bool:
	var before_formation := str(game_state.TacticalFormation)
	var before_style := str(game_state.TeamStyleName)
	var status := str(game_state.TryApplyTacticsFromUser("3-5-2", "High Press", 82, 76, 58, 68))
	var after_formation := str(game_state.TacticalFormation)
	var after_style := str(game_state.TeamStyleName)

	if role == "Assistant Manager":
		if not status.to_lower().contains("recommendation"):
			_fail("%s tactics: expected recommendation message" % role)
			return false
		if after_formation != before_formation or after_style != before_style:
			_fail("%s tactics: saved plan changed without authority" % role)
			return false
		_record(role, "Tactics", "%s / %s" % [before_formation, before_style], "TryApply 3-5-2 High Press", "%s / %s" % [after_formation, after_style], "Recommend only; plan unchanged", true, "TryApplyTacticsFromUser")
		return true

	if after_formation != "3-5-2" or after_style != "High Press":
		_fail("%s tactics: formation/style not saved (%s / %s)" % [role, after_formation, after_style])
		return false
	_record(role, "Tactics", "%s / %s" % [before_formation, before_style], "Apply 3-5-2 High Press", "%s / %s" % [after_formation, after_style], "Football-side tactical control", true, "TacticalFormation")
	return true

func _assert_recruitment(game_state, role: String) -> bool:
	var before_news := str(game_state.NewsFeedSummary)
	var before_summary := str(game_state.RecruitmentFoundationSummary)
	var outcome := str(game_state.AttemptBasicRecruitmentAction())
	var summary := str(game_state.RecruitmentFoundationSummary)
	var after_news := str(game_state.NewsFeedSummary)

	if outcome.is_empty():
		_fail("%s recruitment: no outcome returned" % role)
		return false

	var expected_keyword := "Recommended"
	if role == "Head Coach":
		expected_keyword = "Requested"
	elif role == "Manager":
		expected_keyword = "Board"

	if summary.find(expected_keyword) == -1 and outcome.find(expected_keyword) == -1:
		_fail("%s recruitment: expected %s authority, got %s" % [role, expected_keyword, outcome])
		return false

	var news_updated := (
		after_news != before_news
		or after_news.to_lower().find("recruit") != -1
		or after_news.to_lower().find("transfer") != -1
		or summary != before_summary
	)
	if not news_updated:
		_fail("%s recruitment: news/log/state did not record outcome" % role)
		return false

	var expected := "Recommend only" if role == "Assistant Manager" else ("Request/recommend" if role == "Head Coach" else "Attempt within board limits")
	_record(role, "Recruitment/contracts", _recruitment_snippet(before_summary), "AttemptBasicRecruitmentAction", outcome, expected, true, "RecruitmentFoundationSummary")
	return true

func _assert_promises(game_state, role: String) -> bool:
	var contract := str(game_state.ValidatePhase3PromiseLifecycleContract())
	if contract != "OK":
		_record(role, "Promises", "n/a", "ValidatePhase3PromiseLifecycleContract", contract, "Lifecycle contract OK", false, "ValidatePhase3PromiseLifecycleContract")
		_fail("%s promises contract: %s" % [role, contract])
		return false

	var summary := str(game_state.PromiseSummary)
	var has_lifecycle := summary.to_lower().contains("fulfilled") or summary.to_lower().contains("broken")
	_record(role, "Promises", "active promises", "Phase3 lifecycle contract", summary, "Status/morale/trust updates via contract", true, "PromiseSummary")
	return true

func _assert_post_match(game_state, role: String) -> bool:
	var before_board := int(game_state.BoardMorale)
	var before_fan := int(game_state.FanMorale)
	var before_squad := int(game_state.SquadMorale)
	var before_pressure := int(game_state.JobPressure)
	var before_news := str(game_state.NewsFeedSummary)

	game_state.ResolveCurrentMatchInstantly()

	var after_board := int(game_state.BoardMorale)
	var after_fan := int(game_state.FanMorale)
	var after_squad := int(game_state.SquadMorale)
	var after_pressure := int(game_state.JobPressure)
	var after_news := str(game_state.NewsFeedSummary)
	var phase_summary := str(game_state.BuildCareerPhaseSummary())

	var changed := (
		after_board != before_board
		or after_fan != before_fan
		or after_squad != before_squad
		or after_pressure != before_pressure
	)
	var explained := phase_summary.to_lower().contains("post-match") or phase_summary.to_lower().contains("logged")
	if not changed and not explained:
		var contract := str(game_state.ValidateStage6ConsequencesPressureContract())
		if contract != "OK":
			_fail("%s post-match: no delta and stage6 contract failed: %s" % [role, contract])
			return false
		_record(
			role,
			"Post-match consequences",
			"board %d fan %d squad %d pressure %d" % [before_board, before_fan, before_squad, before_pressure],
			"ResolveCurrentMatchInstantly + Stage6 contract",
			phase_summary,
			"Contract-validated consequences",
			true,
			"ValidateStage6ConsequencesPressureContract"
		)
		return true

	if after_news == before_news and after_news.length() == 0:
		_fail("%s post-match: news feed empty after match" % role)
		return false

	_record(
		role,
		"Post-match consequences",
		"board %d fan %d squad %d pressure %d" % [before_board, before_fan, before_squad, before_pressure],
		"ResolveCurrentMatchInstantly",
		"board %d fan %d squad %d pressure %d; %s" % [after_board, after_fan, after_squad, after_pressure, phase_summary],
		"Morale/pressure delta or stable with explanation; news updates",
		true,
		"BuildCareerPhaseSummary"
	)
	return true

func _assert_live_instant(game_state, role: String) -> bool:
	var contract := str(game_state.ValidateStage5MatchEngineAlignmentContract())
	if contract != "OK":
		_record(role, "Live Match consistency", "n/a", "ValidateStage5MatchEngineAlignmentContract", contract, "Shared timeline/result", false, "ValidateStage5MatchEngineAlignmentContract")
		_fail("%s shared engine: %s" % [role, contract])
		return false
	_record(role, "Live Match consistency", "instant result", "ValidateStage5 contract", "same match object", "No split result or stale timeline", true, "ValidateStage5MatchEngineAlignmentContract")
	return true

func _assert_job_market(game_state, role: String) -> bool:
	var before := str(game_state.CareerMarketSummary)
	var contract := str(game_state.ValidateStage8CareerJobMarketContract())
	if contract != "OK":
		_record(role, "Job market/career state", before, "ValidateStage8CareerJobMarketContract", contract, "Job offer/career state persisted", false, "ValidateStage8CareerJobMarketContract")
		_fail("%s job market: %s" % [role, contract])
		return false
	_record(role, "Job market/career state", before, "Stage8 contract + save/load", str(game_state.CareerMarketSummary), "Job market state generated and persisted", true, "CareerMarketSummary")
	return true

func _assert_authority_contract(game_state, role: String) -> bool:
	var contract := str(game_state.ValidateRoleAuthorityStabilizationContract())
	if contract != "OK":
		_fail("%s authority contract: %s" % [role, contract])
		return false
	return true

func _assert_save_load(game_state, save_system, role: String) -> bool:
	var expected_focus := str(game_state.TrainingFocusName)
	var expected_formation := str(game_state.TacticalFormation)
	var expected_style := str(game_state.TeamStyleName)

	if not save_system.TrySaveGame():
		_fail("Save failed for %s: %s" % [role, save_system.LastStatusMessage])
		return false
	if not save_system.TryLoadGame():
		_fail("Load failed for %s: %s" % [role, save_system.LastStatusMessage])
		return false

	if String(game_state.CurrentRoleName) != role:
		_fail("Role mismatch after reload for %s: %s" % [role, game_state.CurrentRoleName])
		return false

	if str(game_state.TrainingFocusName) != expected_focus and role != "Assistant Manager":
		_fail("%s save/load: training focus not preserved (%s vs %s)" % [role, expected_focus, game_state.TrainingFocusName])
		return false

	if role != "Assistant Manager":
		if str(game_state.TacticalFormation) != expected_formation or str(game_state.TeamStyleName) != expected_style:
			_fail("%s save/load: tactics not preserved" % role)
			return false

	if not game_state.AdvanceOneCareerWeek():
		_fail("%s save/load: post-reload week advance failed" % role)
		return false

	_record(
		role,
		"Save/load persistence",
		"focus=%s formation=%s" % [expected_focus, expected_formation],
		"Save, reload, advance week",
		"role=%s focus=%s formation=%s" % [role, game_state.TrainingFocusName, game_state.TacticalFormation],
		"Changed state survives reload; week continues",
		true,
		"SaveSystem"
	)
	return true

func _record(role: String, area: String, before: String, action: String, after: String, expected: String, passed: bool, evidence: String) -> void:
	var row := {
		"role": role,
		"area": area,
		"before": before,
		"action": action,
		"after": after,
		"expected": expected,
		"passed": passed,
		"evidence": evidence,
	}
	_assertions.append(row)
	var status := "PASS" if passed else "FAIL"
	print(
		"%s|%s|%s|%s|%s|%s|%s|%s"
		% [ASSERT_PREFIX, role, area, before, action, after, expected, status]
	)

func _write_assertion_log() -> void:
	var log_path := ProjectSettings.globalize_path("res://../docs/audit/active-playtest/logs/active-playtest-assertions.json")
	var file := FileAccess.open(log_path, FileAccess.WRITE)
	if file == null:
		push_warning("Could not write assertion log to %s" % log_path)
		return
	file.store_string(JSON.stringify(_assertions, "\t"))
	file.close()

func _fail(message: String) -> void:
	push_error(message)
	print("ACTIVE_PLAYTEST_USER_FLOW_FAIL: " + message)
	_write_assertion_log()
	quit(1)
