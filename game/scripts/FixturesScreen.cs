using Godot;

public partial class FixturesScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    private Label _clubBadgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _managerLabel = default!;
    private Label _seasonLabel = default!;
    private Label _competitionChipLabel = default!;
    private Label _competitionLabel = default!;
    private Label _scheduleStatusLabel = default!;
    private Label _weekChipLabel = default!;
    private Label _stateChipLabel = default!;
    private Label _headerStatusLabel = default!;
    private Label _nextMatchValueLabel = default!;
    private Label _nextMatchMetaLabel = default!;
    private Label _matchdayValueLabel = default!;
    private Label _matchdayMetaLabel = default!;
    private Label _seasonValueLabel = default!;
    private Label _seasonMetaLabel = default!;
    private Label _formValueLabel = default!;
    private Label _formMetaLabel = default!;
    private Label _tableValueLabel = default!;
    private Label _tableMetaLabel = default!;
    private VBoxContainer _clubFixtureRows = default!;
    private VBoxContainer _leagueFixtureRows = default!;
    private Label _nextFixtureLabel = default!;
    private Label _formLabel = default!;
    private Label _statusLabel = default!;
    private Label _timelineNoteLabel = default!;
    private Label _railHintLabel = default!;

    private PanelContainer _railCard = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _competitionChip = default!;
    private PanelContainer _weekChip = default!;
    private PanelContainer _stateChip = default!;
    private PanelContainer _clubTimelineCard = default!;
    private PanelContainer _leagueTimelineCard = default!;
    private PanelContainer _contextCard = default!;

    private Button _dashboardButton = default!;
    private Button _squadButton = default!;
    private Button _tacticsButton = default!;
    private Button _fixturesButton = default!;
    private Button _standingsButton = default!;
    private Button _matchdayButton = default!;
    private Button _backButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _railCard = GetNode<PanelContainer>("RootMargin/Shell/RailCard");
        _clubBadgeLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge/BadgeLabel");
        _clubNameLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/ClubMeta/ClubNameLabel");
        _managerLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/ClubMeta/ManagerLabel");
        _seasonLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/SeasonLabel");
        _competitionChip = GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/CompetitionChip");
        _competitionChipLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/CompetitionChip/CompetitionChipPadding/CompetitionChipLabel");

        _dashboardButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/DashboardButton");
        _squadButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/SquadButton");
        _tacticsButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/TacticsButton");
        _fixturesButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/FixturesButton");
        _standingsButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/StandingsButton");
        _matchdayButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons/MatchdayButton");
        _railHintLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/RailHintLabel");
        _backButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/BackButton");

        _headerCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard");
        _competitionLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CompetitionLabel");
        _scheduleStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ScheduleStatusLabel");
        _weekChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/WeekChip");
        _weekChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/WeekChip/WeekChipPadding/WeekChipLabel");
        _stateChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip");
        _stateChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip/StateChipPadding/StateChipLabel");
        _headerStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel");

        _nextMatchValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardValueLabel");
        _nextMatchMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel");
        _matchdayValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardValueLabel");
        _matchdayMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardMetaLabel");
        _seasonValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/SeasonCard/CardPadding/CardContent/CardValueLabel");
        _seasonMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/SeasonCard/CardPadding/CardContent/CardMetaLabel");
        _formValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardValueLabel");
        _formMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardMetaLabel");
        _tableValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardValueLabel");
        _tableMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardMetaLabel");

        _clubTimelineCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/ClubTimelineCard");
        _clubFixtureRows = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/ClubTimelineCard/TimelinePadding/TimelineContent/TimelineScroll/ClubFixtureRows");
        _leagueTimelineCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LeagueTimelineCard");
        _leagueFixtureRows = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LeagueTimelineCard/TimelinePadding/TimelineContent/TimelineScroll/LeagueFixtureRows");
        _contextCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/ContextCard");
        _nextFixtureLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/NextFixtureLabel");
        _formLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/FormLabel");
        _statusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/StatusLabel");
        _timelineNoteLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/TimelineNoteLabel");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_railCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_competitionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_weekChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
        TouchlineTheme.ApplyPanelVariant(_clubTimelineCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_leagueTimelineCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(_contextCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard"), TouchlineSurfaceVariant.Shell, 22);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/SeasonCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard"), TouchlineSurfaceVariant.Card, 20);

        TouchlineTheme.ApplyButtonVariant(_dashboardButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_squadButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_tacticsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_fixturesButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_standingsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_matchdayButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        _fixturesButton.Disabled = true;

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_competitionLabel, 18);
        TouchlineTheme.ApplyMutedStyle(_scheduleStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_weekChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_stateChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyValueStyle(_nextMatchValueLabel, 24);
        TouchlineTheme.ApplyAccentValueStyle(_matchdayValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_seasonValueLabel, 30);
        TouchlineTheme.ApplyPositiveValueStyle(_formValueLabel, 28);
        TouchlineTheme.ApplyValueStyle(_tableValueLabel, 30);
        TouchlineTheme.ApplyMutedStyle(_nextMatchMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_matchdayMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_seasonMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_formMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tableMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_nextFixtureLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_formLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_timelineNoteLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_railHintLabel, 14);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/SectionLabel"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageTitleLabel"), 36);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MatchdayCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/SeasonCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/ClubTimelineCard/TimelinePadding/TimelineContent/TimelineHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/ClubTimelineCard/TimelinePadding/TimelineContent/TimelineHintLabel"), 14);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LeagueTimelineCard/TimelinePadding/TimelineContent/TimelineHeading"), 22);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LeagueTimelineCard/TimelinePadding/TimelineContent/TimelineHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ContextHeading"), 18);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ContextHintLabel"), 14);
    }

    private void RenderState()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState();
            return;
        }

        var state = GameState.Instance;
        var clubName = state.SelectedClubName!;
        var currentClubFixture = GetCurrentClubFixture(state);
        var currentRow = GetCompetitionRow(clubName);
        var position = GetClubPosition(clubName);

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"Manager {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CompetitionName.ToUpperInvariant();
        _competitionLabel.Text = $"{state.CompetitionName} fixture desk";
        _scheduleStatusLabel.Text = $"{state.CurrentDateLabel} | Matchday {state.CurrentMatchday}";
        _weekChipLabel.Text = $"MD {state.CurrentMatchday}";
        SetStateChip(currentClubFixture);
        _headerStatusLabel.Text = "Club fixtures stay separated from the rest of the round so the season rhythm scans immediately.";

        _nextMatchValueLabel.Text = state.CurrentOpponentName;
        _nextMatchMetaLabel.Text = state.NextFixtureSummary;
        _matchdayValueLabel.Text = state.CurrentMatchday.ToString();
        _matchdayMetaLabel.Text = "Current round";
        _seasonValueLabel.Text = state.SeasonLabel;
        _seasonMetaLabel.Text = state.CurrentDateLabel;
        _formValueLabel.Text = BuildCompactForm(state.FormSummary);
        _formMetaLabel.Text = "Recent run";
        _tableValueLabel.Text = position > 0 ? $"{position}" : "--";
        _tableMetaLabel.Text = currentRow == null ? "Table unavailable." : $"{currentRow.Points} pts | GD {FormatSigned(currentRow.GoalDifference)}";

        _nextFixtureLabel.Text = state.NextFixtureSummary;
        _formLabel.Text = BuildFormLine(state.FormSummary);
        _statusLabel.Text = BuildStatusLabel(position, currentRow);
        _timelineNoteLabel.Text = currentClubFixture == null
            ? "Fixture note unavailable."
            : currentClubFixture.IsComplete
                ? $"Latest club result logged: {currentClubFixture.Scoreline}."
                : "The next club fixture is still open. Use this desk to prepare the week.";
        _railHintLabel.Text = "Use fixtures to track the round, then move into standings or launch matchday.";

        _matchdayButton.Disabled = false;
        PopulateFixtureSections();
    }

    private void RenderUnavailableState()
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = "Manager unavailable";
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _competitionLabel.Text = "Competition unavailable.";
        _scheduleStatusLabel.Text = "Fixture status unavailable.";
        _weekChipLabel.Text = "NO WEEK";
        _stateChipLabel.Text = "NO FIXTURE";
        _headerStatusLabel.Text = "Load a career to open the fixture desk.";
        _nextMatchValueLabel.Text = "--";
        _nextMatchMetaLabel.Text = "Fixture unavailable.";
        _matchdayValueLabel.Text = "--";
        _matchdayMetaLabel.Text = "Matchday unavailable.";
        _seasonValueLabel.Text = "--";
        _seasonMetaLabel.Text = "Season unavailable.";
        _formValueLabel.Text = "--";
        _formMetaLabel.Text = "Form unavailable.";
        _tableValueLabel.Text = "--";
        _tableMetaLabel.Text = "Table unavailable.";
        _nextFixtureLabel.Text = "Next fixture unavailable.";
        _formLabel.Text = "Form unavailable.";
        _statusLabel.Text = "Status unavailable.";
        _timelineNoteLabel.Text = "Fixture note unavailable.";
        _railHintLabel.Text = "Return to the dashboard once a club is active.";
        _matchdayButton.Disabled = true;
        ClearContainer(_clubFixtureRows);
        ClearContainer(_leagueFixtureRows);
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
    }

    private void PopulateFixtureSections()
    {
        ClearContainer(_clubFixtureRows);
        ClearContainer(_leagueFixtureRows);

        if (GameState.Instance == null)
        {
            return;
        }

        foreach (var fixture in GameState.Instance.CompetitionFixtures)
        {
            var isClubFixture = fixture.HomeClubName == GameState.Instance.SelectedClubName || fixture.AwayClubName == GameState.Instance.SelectedClubName;
            var row = CreateFixtureRow(fixture, isClubFixture);
            if (isClubFixture)
            {
                _clubFixtureRows.AddChild(row);
            }
            else
            {
                _leagueFixtureRows.AddChild(row);
            }
        }
    }

    private Control CreateFixtureRow(GameState.CompetitionFixture fixture, bool isClubFixture)
    {
        var isCurrentWeek = GameState.Instance != null && fixture.Matchday == GameState.Instance.CurrentMatchday;
        var rowPanel = new PanelContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyPanelVariant(
            rowPanel,
            isClubFixture && isCurrentWeek
                ? TouchlineSurfaceVariant.Accent
                : isClubFixture
                    ? TouchlineSurfaceVariant.Card
                    : TouchlineSurfaceVariant.Muted,
            16);

        var padding = CreateMarginContainer(16, 12, 16, 12);
        rowPanel.AddChild(padding);

        var rowLayout = new HBoxContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        rowLayout.AddThemeConstantOverride("separation", 14);
        padding.AddChild(rowLayout);

        var matchdayLabel = new Label
        {
            Text = $"MD {fixture.Matchday}",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            CustomMinimumSize = new Vector2(66, 0)
        };
        matchdayLabel.AddThemeFontSizeOverride("font_size", 14);
        matchdayLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextQuiet);
        rowLayout.AddChild(matchdayLabel);

        var body = new VBoxContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        body.AddThemeConstantOverride("separation", 4);
        rowLayout.AddChild(body);

        var titleLabel = new Label
        {
            Text = BuildFixtureTitle(fixture, isClubFixture),
            AutowrapMode = TextServer.AutowrapMode.WordSmart,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        titleLabel.AddThemeFontSizeOverride("font_size", 16);
        titleLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        body.AddChild(titleLabel);

        var metaLabel = new Label
        {
            Text = BuildFixtureMeta(fixture, isClubFixture),
            AutowrapMode = TextServer.AutowrapMode.WordSmart,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        metaLabel.AddThemeFontSizeOverride("font_size", 14);
        metaLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextMuted);
        body.AddChild(metaLabel);

        var rightColumn = new VBoxContainer();
        rightColumn.AddThemeConstantOverride("separation", 6);
        rowLayout.AddChild(rightColumn);

        rightColumn.AddChild(CreateStatusChip(BuildFixtureChipText(fixture, isCurrentWeek), ResolveFixtureChipVariant(fixture, isCurrentWeek)));

        var resultLabel = new Label
        {
            Text = fixture.IsComplete ? fixture.Scoreline : (isClubFixture ? BuildVenueTag(fixture) : "League round"),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        resultLabel.AddThemeFontSizeOverride("font_size", 14);
        resultLabel.AddThemeColorOverride("font_color", fixture.IsComplete ? TouchlineTheme.TextPrimary : TouchlineTheme.TextMuted);
        rightColumn.AddChild(resultLabel);

        return rowPanel;
    }

    private static PanelContainer CreateStatusChip(string text, TouchlineSurfaceVariant variant)
    {
        var panel = new PanelContainer();
        TouchlineTheme.ApplyPanelVariant(panel, variant, 999);

        var margin = CreateMarginContainer(10, 4, 10, 4);
        panel.AddChild(margin);

        var label = new Label
        {
            Text = text,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        label.AddThemeFontSizeOverride("font_size", 12);
        label.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        margin.AddChild(label);

        return panel;
    }

    private static MarginContainer CreateMarginContainer(int left, int top, int right, int bottom)
    {
        var margin = new MarginContainer();
        margin.AddThemeConstantOverride("margin_left", left);
        margin.AddThemeConstantOverride("margin_top", top);
        margin.AddThemeConstantOverride("margin_right", right);
        margin.AddThemeConstantOverride("margin_bottom", bottom);
        return margin;
    }

    private static void ClearContainer(Container container)
    {
        foreach (Node child in container.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void SetStateChip(GameState.CompetitionFixture? fixture)
    {
        if (fixture == null)
        {
            _stateChipLabel.Text = "NO FIXTURE";
            TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
            return;
        }

        if (fixture.IsComplete)
        {
            _stateChipLabel.Text = "RESULT LOGGED";
            TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Positive, 999);
            return;
        }

        _stateChipLabel.Text = "UPCOMING";
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Accent, 999);
    }

    private static string BuildFixtureTitle(GameState.CompetitionFixture fixture, bool isClubFixture)
    {
        if (!isClubFixture || GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return fixture.IsComplete ? fixture.ResultSummary : $"{fixture.HomeClubName} vs {fixture.AwayClubName}";
        }

        var clubName = GameState.Instance.SelectedClubName!;
        var opponent = fixture.HomeClubName == clubName ? fixture.AwayClubName : fixture.HomeClubName;
        return fixture.IsComplete ? fixture.ResultSummary : opponent;
    }

    private static string BuildFixtureMeta(GameState.CompetitionFixture fixture, bool isClubFixture)
    {
        if (!isClubFixture || GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return fixture.IsComplete ? "Round companion result" : "Other fixture in the same round";
        }

        return fixture.IsComplete
            ? $"{BuildVenueTag(fixture)} | Result recorded"
            : $"{BuildVenueTag(fixture)} | Next club fixture";
    }

    private static string BuildFixtureChipText(GameState.CompetitionFixture fixture, bool isCurrentWeek)
    {
        if (fixture.IsComplete)
        {
            return isCurrentWeek ? "FT" : "DONE";
        }

        return isCurrentWeek ? "NEXT" : "UP";
    }

    private static TouchlineSurfaceVariant ResolveFixtureChipVariant(GameState.CompetitionFixture fixture, bool isCurrentWeek)
    {
        if (fixture.IsComplete)
        {
            return TouchlineSurfaceVariant.Positive;
        }

        return isCurrentWeek ? TouchlineSurfaceVariant.Accent : TouchlineSurfaceVariant.Muted;
    }

    private static string BuildVenueTag(GameState.CompetitionFixture fixture)
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return "Fixture venue unavailable";
        }

        return fixture.HomeClubName == GameState.Instance.SelectedClubName ? $"Home vs {fixture.AwayClubName}" : $"Away at {fixture.HomeClubName}";
    }

    private static string BuildCompactForm(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? formSummary["Form: ".Length..] : formSummary;
    }

    private static string BuildFormLine(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? $"Recent form | {formSummary["Form: ".Length..]}" : formSummary;
    }

    private static string BuildClubMonogram(string clubName)
    {
        var words = clubName.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0)
        {
            return "--";
        }

        if (words.Length == 1)
        {
            return words[0].Length >= 2 ? words[0][..2].ToUpperInvariant() : words[0].ToUpperInvariant();
        }

        return $"{char.ToUpperInvariant(words[0][0])}{char.ToUpperInvariant(words[^1][0])}";
    }

    private GameState.CompetitionFixture? GetCurrentClubFixture(GameState state)
    {
        foreach (var fixture in state.CompetitionFixtures)
        {
            if (fixture.Matchday != state.CurrentMatchday)
            {
                continue;
            }

            if (fixture.HomeClubName == state.SelectedClubName || fixture.AwayClubName == state.SelectedClubName)
            {
                return fixture;
            }
        }

        return null;
    }

    private int GetClubPosition(string clubName)
    {
        if (GameState.Instance == null)
        {
            return -1;
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            if (GameState.Instance.CompetitionTable[index].ClubName == clubName)
            {
                return index + 1;
            }
        }

        return -1;
    }

    private GameState.CompetitionRow? GetCompetitionRow(string clubName)
    {
        if (GameState.Instance == null)
        {
            return null;
        }

        foreach (var row in GameState.Instance.CompetitionTable)
        {
            if (row.ClubName == clubName)
            {
                return row;
            }
        }

        return null;
    }

    private static string BuildStatusLabel(int position, GameState.CompetitionRow? row)
    {
        if (row == null || position <= 0)
        {
            return "Club standing is unavailable.";
        }

        return $"{row.ClubName} sit {position} with {row.Points} points from {row.Played} matches.";
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void OnDashboardPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void OnSquadPressed()
    {
        GetTree().ChangeSceneToFile(SquadScreenScenePath);
    }

    private void OnTacticsPressed()
    {
        GetTree().ChangeSceneToFile(TacticsScreenScenePath);
    }

    private void OnStandingsPressed()
    {
        GetTree().ChangeSceneToFile(StandingsScreenScenePath);
    }

    private void OnMatchdayPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
    }
}
