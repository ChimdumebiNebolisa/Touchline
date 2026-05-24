extends SceneTree

var _ticks := 0
var _checked := false

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    _checked = true
    _validate_project_settings()
    return false

func _validate_project_settings() -> void:
    var main_scene := str(ProjectSettings.get_setting("application/run/main_scene"))
    if main_scene != "res://scenes/MainMenu.tscn":
        _fail("Project main scene is not MainMenu: %s" % main_scene)
        return

    var features = ProjectSettings.get_setting("application/config/features")
    if str(features).find("C#") == -1:
        _fail("Project features do not include C#")
        return

    var assembly_name := str(ProjectSettings.get_setting("dotnet/project/assembly_name"))
    if assembly_name != "Touchline":
        _fail("Unexpected .NET assembly name: %s" % assembly_name)
        return

    var required_autoloads: Dictionary = {
        "TouchlineTheme": "*res://scripts/TouchlineTheme.cs",
        "GameState": "*res://scripts/GameState.cs",
        "SaveSystem": "*res://scripts/SaveSystem.cs",
        "CalendarSystem": "*res://scripts/TouchlineCalendarSystem.cs",
        "WorldGenerator": "*res://scripts/TouchlineWorldGenerator.cs"
    }

    for autoload_name in required_autoloads.keys():
        var setting_name := "autoload/%s" % autoload_name
        var value := str(ProjectSettings.get_setting(setting_name))
        if value != required_autoloads[autoload_name]:
            _fail("Autoload %s is misconfigured: %s" % [autoload_name, value])
            return

    if not FileAccess.file_exists("res://data/world-seed.json"):
        _fail("Seed data file is missing from the Godot project")
        return

    print("STEP56_RELEASE_WORKFLOW_PASS")
    quit()

func _fail(message: String) -> void:
    push_error(message)
    print("STEP56_RELEASE_WORKFLOW_FAIL: " + message)
    quit(1)
