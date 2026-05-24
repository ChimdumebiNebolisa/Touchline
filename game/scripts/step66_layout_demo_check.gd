extends SceneTree

var _stage := 0
var _ticks := 0

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_main_menu()
    elif _stage == 2 and _ticks > 2:
        _validate_save_load()
    elif _stage == 3 and _ticks > 2:
        _validate_squad()
    elif _stage == 4 and _ticks > 2:
        _validate_profile()

    if _ticks > 120:
        _fail("Layout check timed out at stage %d" % _stage)

    return false

func _start_flow() -> void:
    var game_state := root.get_node("GameState")
    game_state.StartNewCareer("Layout Demo Check", 666667)
    game_state.SelectClub("Riverton Athletic")
    _change_scene("res://scenes/MainMenu.tscn", 1)

func _validate_main_menu() -> void:
    var detail_grid := current_scene.get_node_or_null("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid") as GridContainer
    var new_career := current_scene.get_node_or_null("Center/MenuCard/Padding/Menu/NewCareerButton") as Button
    if detail_grid == null or detail_grid.columns > 3 or new_career == null:
        _fail("Main menu layout controls are missing or too narrow")
        return

    _change_scene("res://scenes/SaveLoadScene.tscn", 2)

func _validate_save_load() -> void:
    var slot_grid := current_scene.get_node_or_null("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid") as GridContainer
    var load_button := current_scene.get_node_or_null("RootMargin/MainColumn/ActionsRow/LoadButton") as Button
    if slot_grid == null or slot_grid.columns > 2 or load_button == null:
        _fail("Save/load slot layout controls are missing or too narrow")
        return

    _change_scene("res://scenes/SquadScreen.tscn", 3)

func _validate_squad() -> void:
    var heading := _label_text("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHeading")
    var rows := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows")
    if heading.find("Bench") == -1 or heading.find("Reserves") == -1 or rows == null:
        _fail("Squad team-sheet layout is missing expected sections")
        return

    _change_scene("res://scenes/PlayerProfile.tscn", 4)

func _validate_profile() -> void:
    var summary_grid := current_scene.get_node_or_null("RootMargin/Shell/MainColumn/SummaryGrid") as GridContainer
    var title := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/PageTitleLabel")
    if summary_grid == null or summary_grid.columns > 2 or title == "":
        _fail("Player profile dossier layout is missing or too narrow")
        return

    print("STEP66_LAYOUT_DEMO_PASS")
    quit()

func _change_scene(path: String, next_stage: int) -> void:
    var err := change_scene_to_file(path)
    if err != OK:
        _fail("Could not open scene: %s" % path)
        return

    _stage = next_stage
    _ticks = 0

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        return ""
    return node.text

func _fail(message: String) -> void:
    push_error(message)
    print("STEP66_LAYOUT_DEMO_FAIL: " + message)
    quit(1)
