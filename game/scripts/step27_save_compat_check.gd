extends SceneTree

var _ticks := 0
var _has_run := false

func _initialize() -> void:
    var log_file := FileAccess.open("user://step27.log", FileAccess.WRITE)
    if log_file != null:
        log_file.store_string("")
        log_file.close()
    var err := change_scene_to_file("res://scenes/MainMenu.tscn")
    if err != OK:
        _fail("Could not load a runtime scene for the Step 27 save check")

func _process(_delta: float) -> bool:
    _ticks += 1
    if _has_run or _ticks <= 2:
        return false

    _has_run = true
    print("STEP27_CHECK_START")
    _run_check()
    return false

func _run_check() -> void:
    var world_generator := root.get_node("WorldGenerator")
    var save_system := root.get_node("SaveSystem")
    var game_state := root.get_node("GameState")
    _log("autoloads resolved")
    if world_generator == null or save_system == null or game_state == null:
        _fail("Required autoloads are missing for the Step 27 save check")
        return

    _log("begin current save flow")
    if not world_generator.BeginNewCareer("Save Check", 515151):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Northbridge City"):
        _fail(world_generator.LastStatusMessage)
        return

    game_state.SelectPlayerProfile("Rafael Costa")
    _log("saving current version")

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return

    _log("mutating runtime")
    if not world_generator.BeginNewCareer("Mutation Check", 919191):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return

    _log("loading current version")
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return

    _log("current version loaded")
    if game_state.SelectedClubName != "Northbridge City":
        _fail("Current-version load did not restore the saved club")
        return

    _log("selected club restored")
    if game_state.SelectedPlayerProfileName != "Rafael Costa":
        _fail("Current-version load did not restore the selected player profile context")
        return

    _log("selected profile restored")
    if String(game_state.NextFixtureSummary).find("Matchday") == -1:
        _fail("Current-version load did not restore next-fixture context")
        return

    _log("begin legacy flow")
    if not world_generator.BeginNewCareer("Legacy Builder", 616161):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Harbor County"):
        _fail(world_generator.LastStatusMessage)
        return

    var legacy_payload := {
        "managerName": "Legacy Boss",
        "careerSeed": 616161,
        "careerInitialized": true,
        "worldSeed": 616161,
        "countryPackId": "country-pack-alpha",
        "availableClubs": ["Riverton Athletic", "Northbridge City", "Harbor County", "Eastvale Rovers"],
        "selectedClubName": "Harbor County",
        "nextFixtureSummary": "Legacy fixture context",
        "squadStatusSummary": "23 registered players | morale steady | fans steady | board steady",
        "squadPlayers": [
            { "name": "Sergio Vale", "position": "GK", "age": 30, "form": 69, "morale": 70, "fitness": 88, "isStarting": true },
            { "name": "Bastien Kone", "position": "RB", "age": 27, "form": 66, "morale": 68, "fitness": 85, "isStarting": true },
            { "name": "Marek Duda", "position": "CB", "age": 28, "form": 71, "morale": 72, "fitness": 84, "isStarting": true },
            { "name": "Caleb Hwang", "position": "CB", "age": 24, "form": 68, "morale": 69, "fitness": 87, "isStarting": true },
            { "name": "Nuri Demir", "position": "LB", "age": 26, "form": 65, "morale": 67, "fitness": 86, "isStarting": true },
            { "name": "Joel Aina", "position": "CM", "age": 23, "form": 72, "morale": 73, "fitness": 88, "isStarting": true },
            { "name": "Rui Esteves", "position": "CM", "age": 29, "form": 67, "morale": 69, "fitness": 81, "isStarting": true },
            { "name": "Mason Pike", "position": "AM", "age": 22, "form": 74, "morale": 75, "fitness": 89, "isStarting": true },
            { "name": "Jamal Sarr", "position": "RW", "age": 25, "form": 73, "morale": 74, "fitness": 87, "isStarting": true },
            { "name": "Ilian Petrescu", "position": "ST", "age": 27, "form": 72, "morale": 73, "fitness": 84, "isStarting": true },
            { "name": "Timo Larsen", "position": "LW", "age": 23, "form": 69, "morale": 70, "fitness": 86, "isStarting": true },
            { "name": "Arlo Finch", "position": "GK", "age": 19, "form": 62, "morale": 64, "fitness": 82, "isStarting": false },
            { "name": "Matias Gori", "position": "CB", "age": 24, "form": 64, "morale": 66, "fitness": 81, "isStarting": false },
            { "name": "Yonas Bekele", "position": "CM", "age": 20, "form": 68, "morale": 69, "fitness": 85, "isStarting": false },
            { "name": "Danilo Viera", "position": "ST", "age": 22, "form": 67, "morale": 68, "fitness": 85, "isStarting": false }
        ],
        "tacticalFormation": "4-3-3",
        "pressIntensity": 60,
        "tempo": 58,
        "width": 55,
        "risk": 52,
        "competitionName": "Novara Premier Division",
        "currentMatchday": 3,
        "currentOpponentName": "Eastvale Rovers",
        "teamMorale": 69,
        "fanSentiment": 61,
        "boardConfidence": 58,
        "currentDateIso": "2026-08-17",
        "seasonStartYear": 2026,
        "formSummary": "Form: W D",
        "recentResults": ["W", "D"]
    }

    var legacy_file := FileAccess.open("user://slot-1.json", FileAccess.WRITE)
    if legacy_file == null:
        _fail("Could not open the user save path for legacy migration setup")
        return

    legacy_file.store_string(JSON.stringify(legacy_payload, "\t"))
    legacy_file.close()

    _log("loading legacy payload")
    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return

    if game_state.SelectedClubName != "Harbor County":
        _fail("Legacy migration did not restore the legacy selected club")
        return

    if game_state.CurrentMatchday != 3:
        _fail("Legacy migration did not preserve the stored matchday context")
        return

    if String(game_state.NextFixtureSummary).find("Matchday 3") == -1 or game_state.CurrentOpponentName != "Eastvale Rovers":
        _fail("Legacy migration did not rebuild fixture context from the migrated competition state")
        return

    var migrated_file := FileAccess.open("user://slot-1.json", FileAccess.READ)
    if migrated_file == null:
        _fail("Could not re-open the migrated save file")
        return

    var migrated_json := migrated_file.get_as_text()
    migrated_file.close()

    if migrated_json.find("\"SaveVersion\": 2") == -1 or migrated_json.find("\"CompetitionTable\"") == -1:
        _fail("Legacy migration did not rewrite the save file in the current versioned format")
        return

    print("STEP27_SAVE_COMPAT_PASS")
    quit()

func _fail(message: String) -> void:
    _log("FAIL: %s" % message)
    push_error(message)
    print("STEP27_SAVE_COMPAT_FAIL: " + message)
    quit(1)

func _log(message: String) -> void:
    var log_file := FileAccess.open("user://step27.log", FileAccess.READ_WRITE)
    if log_file == null:
        return

    log_file.seek_end()
    log_file.store_line(message)
    log_file.close()
