extends SceneTree

var _ticks := 0
var _checked := false

func _initialize() -> void:
    pass

func _process(_delta: float) -> bool:
    _ticks += 1
    if _checked or _ticks <= 2:
        return false

    if current_scene == null:
        _checked = true
        _start_dashboard_context()
        return false

    _checked = true
    _validate_dashboard_context()
    return false

func _start_dashboard_context() -> void:
    var world_generator := root.get_node("WorldGenerator")
    if world_generator == null:
        _fail("WorldGenerator singleton missing")
        return

    if not world_generator.BeginNewCareer("Dashboard Context Check", 484850):
        _fail(world_generator.LastStatusMessage)
        return

    if not world_generator.SelectClub("Riverton Athletic"):
        _fail(world_generator.LastStatusMessage)
        return

    var err := change_scene_to_file("res://scenes/ClubDashboard.tscn")
    if err != OK:
        _fail("Could not open ClubDashboard")
        return

    _checked = false
    _ticks = 0

func _validate_dashboard_context() -> void:
    if current_scene == null or current_scene.name != "ClubDashboard":
        _fail("ClubDashboard did not load")
        return

    var date_label := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/DateLabel") as Label
    var header_status := current_scene.get_node("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel") as Label
    var next_match_meta := current_scene.get_node("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel") as Label
    var table_meta := current_scene.get_node("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardMetaLabel") as Label
    var pressure_reasons := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureReasonsLabel") as Label
    var squad_status := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/SquadStatusLabel") as Label
    var tactics_summary := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TacticsSummaryLabel") as Label
    var status_label := current_scene.get_node("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StatusLabel") as Label

    if date_label == null or header_status == null or next_match_meta == null or table_meta == null or pressure_reasons == null or squad_status == null or tactics_summary == null or status_label == null:
        _fail("Dashboard context labels are missing")
        return

    if date_label.text.find("Season") == -1 or date_label.text.find("Matchday") == -1:
        _fail("Dashboard date label does not expose season and matchday: %s" % date_label.text)
        return

    if header_status.text.find("New season") == -1 and header_status.text.find("Ready for matchday") == -1 and header_status.text.find("Post-match") == -1:
        _fail("Dashboard header does not expose career phase: %s" % header_status.text)
        return

    if next_match_meta.text.find("Next fixture") == -1:
        _fail("Dashboard next fixture context is unclear: %s" % next_match_meta.text)
        return

    if table_meta.text.find("pts") == -1 or table_meta.text.find("GD") == -1:
        _fail("Dashboard table summary is incomplete: %s" % table_meta.text)
        return

    if pressure_reasons.text.find("Board:") == -1 or pressure_reasons.text.find("Fans:") == -1 or pressure_reasons.text.find("Dressing room:") == -1:
        _fail("Dashboard pressure context is incomplete: %s" % pressure_reasons.text)
        return

    if squad_status.text.find("Lineup readiness") == -1:
        _fail("Dashboard squad readiness summary is missing: %s" % squad_status.text)
        return

    if tactics_summary.text.find("Tactical setup") == -1:
        _fail("Dashboard tactical setup summary is missing: %s" % tactics_summary.text)
        return

    if status_label.text.find("Opponent context") == -1 and status_label.text.find("Cause:") == -1:
        _fail("Dashboard status does not explain next context or last report: %s" % status_label.text)
        return

    print("STEP48_DASHBOARD_CONTEXT_PASS")
    quit()

func _fail(message: String) -> void:
    push_error(message)
    print("STEP48_DASHBOARD_CONTEXT_FAIL: " + message)
    quit(1)
