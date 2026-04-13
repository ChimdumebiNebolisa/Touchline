using Godot;

public partial class StandingsScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    private Label _clubBadgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _managerLabel = default!;
    private Label _seasonLabel = default!;
    private Label _competitionChipLabel = default!;
    private Label _competitionLabel = default!;
    private Label _tableStatusLabel = default!;
    private Label _positionChipLabel = default!;
    private Label _stateChipLabel = default!;
    private Label _headerStatusLabel = default!;
    private Label _positionValueLabel = default!;
    private Label _positionMetaLabel = default!;
    private Label _pointsValueLabel = default!;
    private Label _pointsMetaLabel = default!;
    private Label _goalDifferenceValueLabel = default!;
    private Label _goalDifferenceMetaLabel = default!;
    private Label _paceValueLabel = default!;
    private Label _paceMetaLabel = default!;
    private Label _nextMatchValueLabel = default!;
    private Label _nextMatchMetaLabel = default!;
    private VBoxContainer _tableRows = default!;
    private Label _clubSummaryLabel = default!;
    private Label _formLabel = default!;
    private Label _nextFixtureLabel = default!;
    private Label _tableNoteLabel = default!;
    private Label _railHintLabel = default!;

    private PanelContainer _railCard = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _competitionChip = default!;
    private PanelContainer _positionChip = default!;
    private PanelContainer _stateChip = default!;
    private PanelContainer _tableCard = default!;
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
        _tableStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/TableStatusLabel");
        _positionChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/PositionChip");
        _positionChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/PositionChip/PositionChipPadding/PositionChipLabel");
        _stateChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip");
        _stateChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip/StateChipPadding/StateChipLabel");
        _headerStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel");

        _positionValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardValueLabel");
        _positionMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardMetaLabel");
        _pointsValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PointsCard/CardPadding/CardContent/CardValueLabel");
        _pointsMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PointsCard/CardPadding/CardContent/CardMetaLabel");
        _goalDifferenceValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/GoalDifferenceCard/CardPadding/CardContent/CardValueLabel");
        _goalDifferenceMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/GoalDifferenceCard/CardPadding/CardContent/CardMetaLabel");
        _paceValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PaceCard/CardPadding/CardContent/CardValueLabel");
        _paceMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PaceCard/CardPadding/CardContent/CardMetaLabel");
        _nextMatchValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardValueLabel");
        _nextMatchMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel");

        _tableCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/TableCard");
        _tableRows = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableScroll/TableRows");
        _contextCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/ContextCard");
        _clubSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ClubSummaryLabel");
        _formLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/FormLabel");
        _nextFixtureLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/NextFixtureLabel");
        _tableNoteLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/TableNoteLabel");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_railCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_competitionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
        TouchlineTheme.ApplyPanelVariant(_tableCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_contextCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard"), TouchlineSurfaceVariant.Shell, 22);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/PointsCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/GoalDifferenceCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/PaceCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard"), TouchlineSurfaceVariant.Card, 20);

        TouchlineTheme.ApplyButtonVariant(_dashboardButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_squadButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_tacticsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_fixturesButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_standingsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_matchdayButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        _standingsButton.Disabled = true;

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_competitionLabel, 18);
        TouchlineTheme.ApplyMutedStyle(_tableStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_positionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_stateChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyValueStyle(_positionValueLabel, 30);
        TouchlineTheme.ApplyPositiveValueStyle(_pointsValueLabel, 30);
        TouchlineTheme.ApplyAccentValueStyle(_goalDifferenceValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_paceValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_nextMatchValueLabel, 24);
        TouchlineTheme.ApplyMutedStyle(_positionMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pointsMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_goalDifferenceMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_paceMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_nextMatchMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_clubSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_formLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_nextFixtureLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_tableNoteLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_railHintLabel, 14);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/SectionLabel"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageTitleLabel"), 36);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PointsCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/GoalDifferenceCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PaceCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/TableCard/TablePadding/TableContent/TableHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ContextHeading"), 18);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ContextHintLabel"), 14);

        var headerCells = new[]
        {
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
        };

        foreach (var path in headerCells)
        {
            TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>(path));
        }
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
        var position = GetClubPosition(clubName);
        var tableSize = state.CompetitionTable.Length;
        var row = GetCompetitionRow(clubName);
        var pointsPerMatch = row == null || row.Played == 0 ? 0.0 : (double)row.Points / row.Played;

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"Manager {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CompetitionName.ToUpperInvariant();
        _competitionLabel.Text = $"{state.CompetitionName} standings desk";
        _tableStatusLabel.Text = $"{state.CurrentDateLabel} | Matchday {state.CurrentMatchday}";
        SetPositionChip(position, tableSize);
        SetStateChip(row);
        _headerStatusLabel.Text = "The live table updates from completed rounds. Goal difference remains the first separator on level points.";

        _positionValueLabel.Text = position > 0 ? $"{position}/{tableSize}" : "--";
        _positionMetaLabel.Text = position > 0 ? BuildPositionMeta(position, tableSize) : "Position unavailable.";
        _pointsValueLabel.Text = row == null ? "--" : row.Points.ToString();
        _pointsMetaLabel.Text = row == null ? "Points unavailable." : $"{row.Won}W {row.Drawn}D {row.Lost}L";
        _goalDifferenceValueLabel.Text = row == null ? "--" : FormatSigned(row.GoalDifference);
        _goalDifferenceMetaLabel.Text = row == null ? "Goal difference unavailable." : $"{row.GoalsFor} GF | {row.GoalsAgainst} GA";
        _paceValueLabel.Text = row == null ? "--" : $"{pointsPerMatch:0.00}";
        _paceMetaLabel.Text = row == null ? "Points pace unavailable." : "Points per match";
        _nextMatchValueLabel.Text = state.CurrentOpponentName;
        _nextMatchMetaLabel.Text = state.NextFixtureSummary;

        _clubSummaryLabel.Text = BuildClubSummary(position, tableSize, row);
        _formLabel.Text = BuildFormLine(state.FormSummary);
        _nextFixtureLabel.Text = state.NextFixtureSummary;
        _tableNoteLabel.Text = row == null
            ? "Table note unavailable."
            : $"Season pulse: {row.Played} played, {row.Points} points, {row.GoalsFor} scored, {row.GoalsAgainst} conceded.";
        _railHintLabel.Text = "Read the table first, then move into fixtures or launch the next matchday.";

        _matchdayButton.Disabled = false;
        PopulateTable();
    }

    private void RenderUnavailableState()
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = "Manager unavailable";
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _competitionLabel.Text = "Competition unavailable.";
        _tableStatusLabel.Text = "Table status unavailable.";
        _positionChipLabel.Text = "OFFLINE";
        _stateChipLabel.Text = "NO TABLE";
        _headerStatusLabel.Text = "Load a career to open the standings desk.";
        _positionValueLabel.Text = "--";
        _positionMetaLabel.Text = "Position unavailable.";
        _pointsValueLabel.Text = "--";
        _pointsMetaLabel.Text = "Points unavailable.";
        _goalDifferenceValueLabel.Text = "--";
        _goalDifferenceMetaLabel.Text = "Goal difference unavailable.";
        _paceValueLabel.Text = "--";
        _paceMetaLabel.Text = "Points pace unavailable.";
        _nextMatchValueLabel.Text = "--";
        _nextMatchMetaLabel.Text = "Fixture unavailable.";
        _clubSummaryLabel.Text = "Club summary unavailable.";
        _formLabel.Text = "Form unavailable.";
        _nextFixtureLabel.Text = "Next fixture unavailable.";
        _tableNoteLabel.Text = "Table note unavailable.";
        _railHintLabel.Text = "Return to the dashboard after a career is active.";
        _matchdayButton.Disabled = true;
        ClearContainer(_tableRows);
        TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Muted, 999);
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
    }

    private void PopulateTable()
    {
        ClearContainer(_tableRows);

        if (GameState.Instance == null)
        {
            return;
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            var isSelectedClub = row.ClubName == GameState.Instance.SelectedClubName;
            _tableRows.AddChild(CreateTableRow(index + 1, row, isSelectedClub, index));
        }
    }

    private Control CreateTableRow(int position, GameState.CompetitionRow row, bool isSelectedClub, int index)
    {
        var rowPanel = new PanelContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyPanelVariant(
            rowPanel,
            isSelectedClub
                ? TouchlineSurfaceVariant.Accent
                : index % 2 == 0 ? TouchlineSurfaceVariant.Card : TouchlineSurfaceVariant.Muted,
            16);

        var padding = CreateMarginContainer(16, 12, 16, 12);
        rowPanel.AddChild(padding);

        var rowLayout = new HBoxContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        rowLayout.AddThemeConstantOverride("separation", 10);
        padding.AddChild(rowLayout);

        rowLayout.AddChild(CreateTableCell(position.ToString(), 48, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.ClubName, 220, HorizontalAlignment.Left, true, isSelectedClub, false));
        rowLayout.AddChild(CreateTableCell(row.Played.ToString(), 44, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.Won.ToString(), 44, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.Drawn.ToString(), 44, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.Lost.ToString(), 44, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.GoalsFor.ToString(), 48, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.GoalsAgainst.ToString(), 48, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(FormatSigned(row.GoalDifference), 52, HorizontalAlignment.Center, false, isSelectedClub, true));
        rowLayout.AddChild(CreateTableCell(row.Points.ToString(), 52, HorizontalAlignment.Center, false, isSelectedClub, true, true));
        return rowPanel;
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

    private static Label CreateTableCell(
        string text,
        float minWidth,
        HorizontalAlignment alignment,
        bool expand,
        bool selected,
        bool numeric,
        bool accent = false)
    {
        var label = new Label
        {
            Text = text,
            HorizontalAlignment = alignment,
            VerticalAlignment = VerticalAlignment.Center,
            CustomMinimumSize = new Vector2(minWidth, 0),
            SizeFlagsHorizontal = expand ? SizeFlags.ExpandFill : SizeFlags.ShrinkBegin
        };

        label.AddThemeFontSizeOverride("font_size", numeric ? 15 : 16);
        label.AddThemeColorOverride(
            "font_color",
            selected
                ? accent ? TouchlineTheme.TextPrimary : TouchlineTheme.TextPrimary
                : accent ? TouchlineTheme.AccentBlueHover : TouchlineTheme.TextMuted);
        return label;
    }

    private static void ClearContainer(Container container)
    {
        foreach (Node child in container.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void SetPositionChip(int position, int tableSize)
    {
        if (position <= 0 || tableSize <= 0)
        {
            _positionChipLabel.Text = "NO POSITION";
            TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Muted, 999);
            return;
        }

        if (position == 1)
        {
            _positionChipLabel.Text = "LEADERS";
            TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Positive, 999);
            return;
        }

        if (position <= 4)
        {
            _positionChipLabel.Text = "TOP FOUR";
            TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Accent, 999);
            return;
        }

        if (position <= (tableSize + 1) / 2)
        {
            _positionChipLabel.Text = "IN CHASE";
            TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Accent, 999);
            return;
        }

        _positionChipLabel.Text = "NEED POINTS";
        TouchlineTheme.ApplyPanelVariant(_positionChip, TouchlineSurfaceVariant.Muted, 999);
    }

    private void SetStateChip(GameState.CompetitionRow? row)
    {
        if (row == null)
        {
            _stateChipLabel.Text = "NO TABLE";
            TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
            return;
        }

        if (row.GoalDifference >= 5)
        {
            _stateChipLabel.Text = "STRONG TREND";
            TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Positive, 999);
            return;
        }

        if (row.GoalDifference >= 0)
        {
            _stateChipLabel.Text = "STABLE";
            TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Accent, 999);
            return;
        }

        _stateChipLabel.Text = "UNDER WATCH";
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Muted, 999);
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

    private static string BuildClubSummary(int position, int tableSize, GameState.CompetitionRow? row)
    {
        if (row == null || position <= 0 || tableSize <= 0)
        {
            return "Club summary unavailable.";
        }

        return $"{row.ClubName} sit {position} of {tableSize} with {row.Points} points and a {FormatSigned(row.GoalDifference)} goal difference.";
    }

    private static string BuildFormLine(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? $"Recent form | {formSummary["Form: ".Length..]}" : formSummary;
    }

    private static string BuildPositionMeta(int position, int tableSize)
    {
        if (position == 1)
        {
            return "Top of the division";
        }

        if (position <= 4)
        {
            return "Upper-table pace";
        }

        return position <= (tableSize + 1) / 2 ? "Mid-table control" : "Lower-table pressure";
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

    private void OnFixturesPressed()
    {
        GetTree().ChangeSceneToFile(FixturesScreenScenePath);
    }

    private void OnMatchdayPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
    }
}
