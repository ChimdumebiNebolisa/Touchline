extends SceneTree

const CLUB_NAME := "Riverton Athletic"

var _ticks := 0
var _started := false

func _process(_delta: float) -> bool:
	_ticks += 1
	if not _started and _ticks > 2:
		_started = true
		_run()
	return false

func _run() -> void:
	var args := OS.get_cmdline_user_args()
	var role := _read_arg(args, "--role", "Manager")
	var manager_name := _read_arg(args, "--manager", "Audit Capture")
	var seed_text := _read_arg(args, "--seed", "903100")
	var seed := int(seed_text) if String(seed_text).is_valid_int() else 903100

	var world_generator := root.get_node_or_null("WorldGenerator")
	var game_state := root.get_node_or_null("GameState")
	var save_system := root.get_node_or_null("SaveSystem")
	if world_generator == null or game_state == null or save_system == null:
		_fail("Required autoloads are missing")
		return

	if not world_generator.BeginNewCareer(manager_name, seed, role, "Unknown Upstart", "National C License"):
		_fail(str(world_generator.LastStatusMessage))
		return

	if not world_generator.SelectClub(CLUB_NAME):
		_fail(str(world_generator.LastStatusMessage))
		return

	if not save_system.TrySaveGame():
		_fail(str(save_system.LastStatusMessage))
		return

	print("AUDIT_CAPTURE_PREP_PASS|%s|%s|%d" % [role, CLUB_NAME, seed])
	quit()

func _read_arg(args: PackedStringArray, flag: String, fallback: String) -> String:
	for index in range(args.size() - 1):
		if args[index] == flag:
			return args[index + 1]
	return fallback

func _fail(message: String) -> void:
	push_error(message)
	print("AUDIT_CAPTURE_PREP_FAIL: " + message)
	quit(1)
