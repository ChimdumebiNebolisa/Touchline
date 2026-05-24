extends SceneTree

var _stage := 0
var _ticks := 0
var _selected_club := "Riverton Athletic"

func _process(_delta: float) -> bool:
    _ticks += 1

    if _stage == 0 and _ticks > 2:
        _start_competition_flow()
    elif _stage == 1 and _ticks > 2:
        _validate_fixtures_after_result()
    elif _stage == 2 and _ticks > 2:
        _validate_standings_context()
    elif _stage == 3 and _ticks > 2:
        _validate_rollover_fixture_state()

    return false

func _start_competition_flow() -> void:
    var game_state := root.get_node("GameState")
    if game_state == null:
        _fail("GameState singleton missing")
        return

    game_state.StartNewCareer("Competition Surfaces Check", 535360)
    game_state.SelectClub(_selected_club)
    game_state.ResolveCurrentMatchInstantly()
    game_state.AdvanceDate()

    var err := change_scene_to_file("res://scenes/FixturesScreen.tscn")
    if err != OK:
        _fail("Could not open FixturesScreen")
        return

    _stage = 1
    _ticks = 0

func _validate_fixtures_after_result() -> void:
    if current_scene == null or current_scene.name != "FixturesScreen":
        _fail("FixturesScreen did not load")
        return

    var schedule_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ScheduleStatusLabel")
    var header_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel")
    var matchday_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardValueLabel")
    var matchday_meta := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardMetaLabel")
    var season_meta := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/SeasonCard/CardPadding/CardContent/CardMetaLabel")
    var timeline_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/TimelineNoteLabel")

    if schedule_status.find("Season") == -1 or schedule_status.find("Matchday") == -1:
        _fail("Fixtures header does not show season and matchday: %s" % schedule_status)
        return

    if header_status.find("completed results") == -1 or header_status.find("upcoming rounds") == -1:
        _fail("Fixtures header does not explain completed/upcoming state: %s" % header_status)
        return

    if int(matchday_value) != 2 or matchday_meta.find("Current matchday") == -1 or season_meta.find("Current season") == -1:
        _fail("Fixtures summary does not show current season/matchday context")
        return

    if timeline_note.find(_selected_club) == -1:
        _fail("Fixtures context does not name the selected club: %s" % timeline_note)
        return

    var club_rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/MainStack/ClubTimelineCard/TimelinePadding/TimelineContent/TimelineScroll/ClubFixtureRows")
    var league_rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/MainStack/LeagueTimelineCard/TimelinePadding/TimelineContent/TimelineScroll/LeagueFixtureRows")
    if club_rows == null or league_rows == null:
        _fail("Fixture row containers are missing")
        return

    var row_text := _collect_text(club_rows) + " " + _collect_text(league_rows)
    if row_text.find("COMPLETED") == -1 or row_text.find("NEXT") == -1 or row_text.find("UPCOMING") == -1:
        _fail("Fixtures do not distinguish completed, next, and upcoming rows")
        return

    if _collect_text(club_rows).find(_selected_club) == -1:
        _fail("Selected club is not visible in club fixture rows")
        return

    for row in club_rows.get_children() + league_rows.get_children():
        var text := _collect_text(row)
        if text.find("COMPLETED") != -1 and text.find(" - ") == -1:
            _fail("Completed fixture row does not show a scoreline: %s" % text)
            return
        if (text.find("NEXT") != -1 or text.find("UPCOMING") != -1) and text.find(" - ") != -1:
            _fail("Upcoming fixture row is showing a completed scoreline: %s" % text)
            return

    var err := change_scene_to_file("res://scenes/StandingsScreen.tscn")
    if err != OK:
        _fail("Could not open StandingsScreen")
        return

    _stage = 2
    _ticks = 0

func _validate_standings_context() -> void:
    if current_scene == null or current_scene.name != "StandingsScreen":
        _fail("StandingsScreen did not load")
        return

    var table_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/TableStatusLabel")
    var header_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel")
    var club_summary := _label_text("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ClubSummaryLabel")
    var table_note := _label_text("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/TableNoteLabel")
    var table_rows := current_scene.get_node("RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableScroll/TableRows")

    if table_status.find("Season") == -1 or table_status.find("Matchday") == -1:
        _fail("Standings header does not show season and matchday: %s" % table_status)
        return

    for token in ["P, W, D, L, GF, GA, GD, Pts", "selected club"]:
        if header_status.find(token) == -1:
            _fail("Standings header is missing %s: %s" % [token, header_status])
            return

    if club_summary.find("Selected club:") == -1 or club_summary.find(_selected_club) == -1:
        _fail("Standings context does not identify the selected club: %s" % club_summary)
        return

    if table_note.find("Table columns:") == -1 or table_note.find("Pts points") == -1:
        _fail("Standings note does not explain table columns: %s" % table_note)
        return

    var rows_text := _collect_text(table_rows)
    if rows_text.find(_selected_club) == -1 or rows_text.find("(selected club)") == -1:
        _fail("Selected club row is not clearly labelled in the standings table")
        return

    for path in [
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/PosHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/ClubHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/PHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/WHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/DHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/LHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/GFHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/GAHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/GDHeader",
        "RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeaderRow/PtsHeader"
    ]:
        if _label_text(path).strip_edges() == "":
            _fail("Standings table header column is blank: %s" % path)
            return

    var game_state := root.get_node("GameState")
    var guard := 0
    while int(game_state.CurrentMatchday) != 1 and guard < 10:
        game_state.ResolveCurrentMatchInstantly()
        game_state.AdvanceDate()
        guard += 1

    if int(game_state.CurrentMatchday) != 1:
        _fail("Season rollover did not return to Matchday 1")
        return

    var err := change_scene_to_file("res://scenes/FixturesScreen.tscn")
    if err != OK:
        _fail("Could not reopen FixturesScreen after rollover")
        return

    _stage = 3
    _ticks = 0

func _validate_rollover_fixture_state() -> void:
    if current_scene == null or current_scene.name != "FixturesScreen":
        _fail("FixturesScreen did not reload after rollover")
        return

    var schedule_status := _label_text("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ScheduleStatusLabel")
    var matchday_value := _label_text("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardValueLabel")
    var row_text := _collect_text(current_scene)

    if int(matchday_value) != 1 or schedule_status.find("Matchday 1") == -1:
        _fail("Rollover fixture state does not show Matchday 1")
        return

    if row_text.find("NEXT") == -1 or row_text.find("UPCOMING") == -1:
        _fail("Rollover fixture state does not show next/upcoming rows")
        return

    if row_text.find(_selected_club) == -1:
        _fail("Rollover fixture state lost selected club context")
        return

    print("STEP53_COMPETITION_SURFACES_PASS")
    quit()

func _label_text(path: String) -> String:
    var node := current_scene.get_node_or_null(path) as Label
    if node == null:
        _fail("Missing label: %s" % path)
        return ""

    return node.text

func _collect_text(node: Node) -> String:
    var parts: Array[String] = []
    if node is Label:
        parts.append((node as Label).text)

    for child in node.get_children():
        parts.append(_collect_text(child))

    return " ".join(parts)

func _fail(message: String) -> void:
    push_error(message)
    print("STEP53_COMPETITION_SURFACES_FAIL: " + message)
    quit(1)
