extends SceneTree

var _stage := 0
var _ticks := 0

func _initialize() -> void:
    var err := change_scene_to_file("res://scenes/CareerSetup.tscn")
    if err != OK:
        _fail("unable to load CareerSetup scene")

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _submit_career_setup()
    elif _stage == 1 and _ticks > 2:
        _select_club()
    elif _stage == 2 and _ticks > 2:
        _validate_dashboard()
    elif _stage == 3 and _ticks > 2:
        _validate_loaded_state()

    return false

func _submit_career_setup() -> void:
    if current_scene == null or current_scene.name != "CareerSetup":
        _fail("CareerSetup scene did not load")
        return

    var name_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/ManagerNameInput") as LineEdit
    var seed_input := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/SeedInput") as SpinBox
    var role_option := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/RoleOption") as OptionButton
    var background_option := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/BackgroundOption") as OptionButton
    var license_option := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/LicenseOption") as OptionButton
    var start_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormLayout/FormScroll/FormContent/ActionsRow/StartCareerButton") as Button

    if name_input == null or seed_input == null or role_option == null or background_option == null or license_option == null or start_button == null:
        _fail("CareerSetup Stage 1 controls are missing")
        return

    name_input.text = "Ari Lane"
    seed_input.value = 737373
    role_option.select(0)
    background_option.select(5)
    license_option.select(2)
    start_button.emit_signal("pressed")
    _stage = 1
    _ticks = 0

func _select_club() -> void:
    if current_scene == null or current_scene.name != "ChooseClub":
        _fail("Start Career did not hand off to ChooseClub")
        return

    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton was not autoloaded")
        return

    if str(game_state.CurrentRoleName) != "Assistant Manager":
        _fail("Role was not saved in game state")
        return

    if str(game_state.ManagerBackgroundName) != "Tactical Specialist":
        _fail("Manager background was not saved in game state")
        return

    if str(game_state.LicenseName) != "National B License":
        _fail("Starting license was not saved in game state")
        return

    var summary_label := current_scene.get_node("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/CareerSummaryLabel") as Label
    if summary_label == null or summary_label.text.find("Assistant Manager") == -1 or summary_label.text.find("Tactical Specialist") == -1 or summary_label.text.find("National B License") == -1:
        _fail("ChooseClub summary did not render role, background, and license")
        return

    current_scene.SelectClubRow(3)
    var confirm_button := current_scene.get_node("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton") as Button
    if confirm_button == null:
        _fail("Confirm button is missing from ChooseClub")
        return

    confirm_button.emit_signal("pressed")
    _stage = 2
    _ticks = 0

func _validate_dashboard() -> void:
    if current_scene == null or current_scene.name != "ClubDashboard":
        _fail("ClubDashboard did not load after club selection")
        return

    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing on dashboard")
        return

    var contract_status := str(game_state.ValidateStage1CareerFoundationContract())
    if contract_status != "OK":
        _fail(contract_status)
        return

    if str(game_state.SelectedClubName) != "Eastvale Rovers":
        _fail("Selected club was not stored")
        return

    if str(game_state.ClubArchetypeName) != "Youth Academy Club":
        _fail("Club archetype is not inspectable")
        return

    if str(game_state.BoardPhilosophyName) != "Youth Development Board":
        _fail("Board philosophy is not inspectable")
        return

    if str(game_state.FanCultureName) != "Academy Loyalists":
        _fail("Fan culture is not inspectable")
        return

    if str(game_state.DirectorOfFootballStyleName) != "Academy Builder":
        _fail("Director of Football style is not inspectable")
        return

    if not _assert_label_contains(
        "RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/ClubMeta/ManagerLabel",
        "Assistant Manager",
        "dashboard manager role"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CareerFoundationLabel",
        "Tactical Specialist",
        "dashboard career foundation"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubFoundationLabel",
        "Academy Builder",
        "dashboard club foundation"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureValueLabel",
        "Pressure | job",
        "dashboard pressure"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/RoleAuthorityLabel",
        "Can suggest",
        "dashboard role authority"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ObjectivesLabel",
        "Main objectives",
        "dashboard objectives"):
        return
    if not _assert_label_contains(
        "RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/NewsFeedLabel",
        "News feed",
        "dashboard news feed"):
        return

    var save_system := root.get_node("SaveSystem")
    var world_generator := root.get_node("WorldGenerator")
    if save_system == null or world_generator == null:
        _fail("SaveSystem or WorldGenerator singleton is missing")
        return

    if not save_system.TrySaveGame():
        _fail(save_system.LastStatusMessage)
        return

    if not world_generator.BeginNewCareer("Mutation Check", 919191):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Northbridge City"):
        _fail(world_generator.LastStatusMessage)
        return

    if not save_system.TryLoadGame():
        _fail(save_system.LastStatusMessage)
        return

    var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
    if err != OK:
        _fail("Could not reload ClubDashboard after save/load")
        return

    _stage = 3
    _ticks = 0

func _validate_loaded_state() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing after save/load")
        return

    if str(game_state.ManagerName) != "Ari Lane":
        _fail("Save/load did not preserve manager name")
        return

    if str(game_state.CurrentRoleName) != "Assistant Manager":
        _fail("Save/load did not preserve role")
        return

    if str(game_state.ManagerBackgroundName) != "Tactical Specialist":
        _fail("Save/load did not preserve manager background")
        return

    if str(game_state.LicenseName) != "National B License":
        _fail("Save/load did not preserve starting license")
        return

    if str(game_state.SelectedClubName) != "Eastvale Rovers":
        _fail("Save/load did not preserve selected club")
        return

    if str(game_state.ClubArchetypeName) != "Youth Academy Club" or str(game_state.DirectorOfFootballStyleName) != "Academy Builder":
        _fail("Save/load did not preserve club foundation metadata")
        return

    var contract_status := str(game_state.ValidateStage1CareerFoundationContract())
    if contract_status != "OK":
        _fail("Loaded Stage 1 contract failed: %s" % contract_status)
        return

    print("STAGE1_CAREER_FOUNDATION_PASS")
    quit()

func _assert_label_contains(path: String, expected: String, label_name: String) -> bool:
    var label := current_scene.get_node(path) as Label
    if label == null:
        _fail("%s label is missing" % label_name)
        return false

    if label.text.find(expected) == -1:
        _fail("%s label did not contain '%s': %s" % [label_name, expected, label.text])
        return false

    return true

func _fail(message: String) -> void:
    push_error(message)
    print("STAGE1_CAREER_FOUNDATION_FAIL: " + message)
    quit(1)
