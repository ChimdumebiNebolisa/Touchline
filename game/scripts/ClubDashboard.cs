using Godot;

public partial class ClubDashboard : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string TacticsScreenScenePath = "res://scenes/TacticsScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    private Label _clubBadgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _managerLabel = default!;
    private Label _seasonLabel = default!;
    private Label _competitionChipLabel = default!;
    private Label _clubContextLabel = default!;
    private Label _careerFoundationLabel = default!;
    private Label _clubFoundationLabel = default!;
    private Label _dateLabel = default!;
    private Label _priorityChipLabel = default!;
    private Label _stateChipLabel = default!;
    private Label _headerStatusLabel = default!;
    private Label _nextMatchValueLabel = default!;
    private Label _nextMatchMetaLabel = default!;
    private Label _tableValueLabel = default!;
    private Label _tableMetaLabel = default!;
    private Label _moraleValueLabel = default!;
    private Label _moraleMetaLabel = default!;
    private Label _boardValueLabel = default!;
    private Label _boardMetaLabel = default!;
    private Label _shapeValueLabel = default!;
    private Label _shapeMetaLabel = default!;
    private Label _fixturePreviewLabel = default!;
    private Label _focusContextLabel = default!;
    private Label _recommendedMoveLabel = default!;
    private Label _actionHintLabel = default!;
    private Label _formValueLabel = default!;
    private Label _lastResultLabel = default!;
    private Label _tableImpactLabel = default!;
    private Label _pressureValueLabel = default!;
    private Label _pressureReasonsLabel = default!;
    private Label _squadStatusLabel = default!;
    private Label _tacticsSummaryLabel = default!;
    private Label _roleAuthorityLabel = default!;
    private Label _objectivesLabel = default!;
    private Label _staffLabel = default!;
    private Label _newsFeedLabel = default!;
    private Label _trainingScoutingLabel = default!;
    private Label _youthAcademyLabel = default!;
    private Label _recruitmentLabel = default!;
    private Label _careerMarketLabel = default!;
    private Label _priorityLabel = default!;
    private Label _statusLabel = default!;
    private Label _saveHintLabel = default!;
    private PanelContainer _competitionChip = default!;
    private PanelContainer _priorityChip = default!;
    private PanelContainer _stateChip = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _focusCard = default!;
    private PanelContainer _momentumCard = default!;
    private PanelContainer _pressureCard = default!;
    private PanelContainer _insightCard = default!;
    private PanelContainer _railCard = default!;
    private Button _dashboardButton = default!;
    private Button _squadButton = default!;
    private Button _tacticsButton = default!;
    private Button _fixturesButton = default!;
    private Button _standingsButton = default!;
    private Button _matchdayButton = default!;
    private Button _saveButton = default!;
    private OptionButton _trainingFocusOption = default!;
    private OptionButton _trainingIntensityOption = default!;
    private Button _applyTrainingButton = default!;
    private OptionButton _scoutingTargetOption = default!;
    private OptionButton _scoutingDepthOption = default!;
    private Button _startScoutingButton = default!;
    private Button _advanceDayButton = default!;
    private Button _advanceWeekButton = default!;
    private Button _staffMarketButton = default!;
    private Button _youthAcademyButton = default!;
    private Button _recruitmentButton = default!;
    private Button _contractButton = default!;
    private Button _jobMarketButton = default!;
    private Button _resolveEventButton = default!;
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
        _saveButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveButton");
        _saveHintLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveHintLabel");
        _backButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/BackButton");

        _headerCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard");
        EnsureStage1DashboardLabels();
        _clubContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel");
        _careerFoundationLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CareerFoundationLabel");
        _clubFoundationLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubFoundationLabel");
        _dateLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/DateLabel");
        _priorityChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/PriorityChip");
        _priorityChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/PriorityChip/PriorityChipPadding/PriorityChipLabel");
        _stateChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip");
        _stateChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/StateChip/StateChipPadding/StateChipLabel");
        _headerStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel");

        _nextMatchValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardValueLabel");
        _nextMatchMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel");
        _tableValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardValueLabel");
        _tableMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardMetaLabel");
        _moraleValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard/CardPadding/CardContent/CardValueLabel");
        _moraleMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard/CardPadding/CardContent/CardMetaLabel");
        _boardValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/BoardCard/CardPadding/CardContent/CardValueLabel");
        _boardMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/BoardCard/CardPadding/CardContent/CardMetaLabel");
        _shapeValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/ShapeCard/CardPadding/CardContent/CardValueLabel");
        _shapeMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/ShapeCard/CardPadding/CardContent/CardMetaLabel");

        _focusCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/FocusCard");
        _fixturePreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/FixturePreviewLabel");
        _focusContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/FocusContextLabel");
        _recommendedMoveLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/RecommendedMoveLabel");
        _actionHintLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/ActionHintLabel");

        _momentumCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/MomentumCard");
        _formValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/FormValueLabel");
        _lastResultLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/LastResultLabel");
        _tableImpactLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/TableImpactLabel");

        _pressureCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/PressureCard");
        _pressureValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureValueLabel");
        _pressureReasonsLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureReasonsLabel");

        _insightCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard");
        _squadStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/SquadStatusLabel");
        _tacticsSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TacticsSummaryLabel");
        _roleAuthorityLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/RoleAuthorityLabel");
        _objectivesLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ObjectivesLabel");
        _staffLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StaffLabel");
        _newsFeedLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/NewsFeedLabel");
        _trainingScoutingLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingScoutingLabel");
        _trainingFocusOption = GetNode<OptionButton>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingFocusOption");
        _trainingIntensityOption = GetNode<OptionButton>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/TrainingIntensityOption");
        _applyTrainingButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ApplyTrainingButton");
        _scoutingTargetOption = GetNode<OptionButton>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ScoutingTargetOption");
        _scoutingDepthOption = GetNode<OptionButton>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ScoutingDepthOption");
        _startScoutingButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StartScoutingButton");
        _advanceDayButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/AdvanceDayButton");
        _advanceWeekButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/AdvanceWeekButton");
        _staffMarketButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StaffMarketButton");
        _youthAcademyLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/YouthAcademyLabel");
        _youthAcademyButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/YouthAcademyButton");
        _recruitmentLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/RecruitmentLabel");
        _careerMarketLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/CareerMarketLabel");
        _recruitmentButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/RecruitmentButton");
        _contractButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ContractButton");
        _jobMarketButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/JobMarketButton");
        _resolveEventButton = GetNode<Button>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/ResolveEventButton");
        _priorityLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/PriorityLabel");
        _statusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent/StatusLabel");
    }

    private void EnsureStage1DashboardLabels()
    {
        var headerInfo = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo");
        var dateNode = headerInfo.GetNode("DateLabel");
        var headerInsertIndex = dateNode.GetIndex();
        EnsureLabel(headerInfo, ref headerInsertIndex, "CareerFoundationLabel");
        EnsureLabel(headerInfo, ref headerInsertIndex, "ClubFoundationLabel");

        var insightContent = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/MainScroll/ScrollContent/ContentRow/InsightCard/InsightPadding/InsightScroll/InsightContent");
        var priorityNode = insightContent.GetNode("PriorityLabel");
        var insightInsertIndex = priorityNode.GetIndex();
        EnsureLabel(insightContent, ref insightInsertIndex, "RoleAuthorityLabel");
        EnsureLabel(insightContent, ref insightInsertIndex, "ObjectivesLabel");
        EnsureLabel(insightContent, ref insightInsertIndex, "StaffLabel");
        EnsureLabel(insightContent, ref insightInsertIndex, "NewsFeedLabel");
        EnsureSectionEyebrow(insightContent, ref insightInsertIndex, "TrainingSectionEyebrow", "TRAINING / SCOUTING");
        EnsureLabel(insightContent, ref insightInsertIndex, "TrainingScoutingLabel");
        EnsureOptionButton(insightContent, ref insightInsertIndex, "TrainingFocusOption");
        EnsureOptionButton(insightContent, ref insightInsertIndex, "TrainingIntensityOption");
        EnsureButton(insightContent, ref insightInsertIndex, "ApplyTrainingButton", "Apply Training Plan");
        EnsureOptionButton(insightContent, ref insightInsertIndex, "ScoutingTargetOption");
        EnsureOptionButton(insightContent, ref insightInsertIndex, "ScoutingDepthOption");
        EnsureButton(insightContent, ref insightInsertIndex, "StartScoutingButton", "Start Scouting Assignment");
        EnsureButton(insightContent, ref insightInsertIndex, "AdvanceDayButton", "Advance Day");
        EnsureButton(insightContent, ref insightInsertIndex, "AdvanceWeekButton", "Advance Week");
        EnsureButton(insightContent, ref insightInsertIndex, "StaffMarketButton", "Review Staff Market");
        EnsureLabel(insightContent, ref insightInsertIndex, "YouthAcademyLabel");
        EnsureButton(insightContent, ref insightInsertIndex, "YouthAcademyButton", "Review Youth Academy");
        EnsureSectionEyebrow(insightContent, ref insightInsertIndex, "RecruitmentSectionEyebrow", "RECRUITMENT / CONTRACTS");
        EnsureLabel(insightContent, ref insightInsertIndex, "RecruitmentLabel");
        EnsureButton(insightContent, ref insightInsertIndex, "RecruitmentButton", "Progress Recruitment Foundation");
        EnsureButton(insightContent, ref insightInsertIndex, "ContractButton", "Review Contract Terms");
        EnsureSectionEyebrow(insightContent, ref insightInsertIndex, "CareerMarketSectionEyebrow", "CAREER / JOB MARKET");
        EnsureLabel(insightContent, ref insightInsertIndex, "CareerMarketLabel");
        EnsureButton(insightContent, ref insightInsertIndex, "JobMarketButton", "Generate Job Market Event");
        EnsureButton(insightContent, ref insightInsertIndex, "ResolveEventButton", "Resolve Decision Event");
    }

    private static void EnsureLabel(VBoxContainer container, ref int insertIndex, string labelName)
    {
        var label = container.GetNodeOrNull<Label>(labelName);
        if (label != null)
        {
            return;
        }

        label = new Label
        {
            Name = labelName,
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        container.AddChild(label);
        container.MoveChild(label, insertIndex++);
    }

    private static void EnsureSectionEyebrow(VBoxContainer container, ref int insertIndex, string labelName, string text)
    {
        EnsureLabel(container, ref insertIndex, labelName);
        var label = container.GetNode<Label>(labelName);
        label.Text = text;
        TouchlineTheme.ApplyEyebrowStyle(label);
    }

    private static void EnsureButton(VBoxContainer container, ref int insertIndex, string buttonName, string text)
    {
        var button = container.GetNodeOrNull<Button>(buttonName);
        if (button != null)
        {
            return;
        }

        button = new Button
        {
            Name = buttonName,
            Text = text,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        container.AddChild(button);
        container.MoveChild(button, insertIndex++);
    }

    private static void EnsureOptionButton(VBoxContainer container, ref int insertIndex, string optionName)
    {
        var option = container.GetNodeOrNull<OptionButton>(optionName);
        if (option != null)
        {
            return;
        }

        option = new OptionButton
        {
            Name = optionName,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        container.AddChild(option);
        container.MoveChild(option, insertIndex++);
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_railCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_focusCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_momentumCard, TouchlineSurfaceVariant.Muted, 20);
        TouchlineTheme.ApplyPanelVariant(_pressureCard, TouchlineSurfaceVariant.Muted, 20);
        TouchlineTheme.ApplyPanelVariant(_insightCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/TableCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/BoardCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/ShapeCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(_competitionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_priorityChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_stateChip, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard"), TouchlineSurfaceVariant.Shell, 22);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);

        TouchlineTheme.ApplyRailNavigation(
            _dashboardButton,
            _squadButton,
            _tacticsButton,
            _fixturesButton,
            _standingsButton,
            _matchdayButton,
            TouchlineRailRoute.Dashboard);
        TouchlineTheme.ApplyButtonVariant(_saveButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_priorityChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_stateChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_clubContextLabel, 18);
        TouchlineTheme.ApplyMutedStyle(_careerFoundationLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_clubFoundationLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_dateLabel, 15);
        TouchlineTheme.ApplyValueStyle(_nextMatchValueLabel, 30);
        TouchlineTheme.ApplyAccentValueStyle(_tableValueLabel, 30);
        TouchlineTheme.ApplyPositiveValueStyle(_moraleValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_boardValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_shapeValueLabel, 30);
        TouchlineTheme.ApplyMutedStyle(_nextMatchMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tableMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_moraleMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_boardMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_shapeMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_focusContextLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_actionHintLabel, 14);
        TouchlineTheme.ApplyValueStyle(_formValueLabel, 26);
        TouchlineTheme.ApplyMutedStyle(_lastResultLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tableImpactLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pressureReasonsLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_saveHintLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_roleAuthorityLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_objectivesLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_staffLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_newsFeedLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_trainingScoutingLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_youthAcademyLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_recruitmentLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_careerMarketLabel, 14);
        TouchlineTheme.ApplyButtonVariant(_applyTrainingButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_startScoutingButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_advanceDayButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_advanceWeekButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_staffMarketButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_youthAcademyButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_recruitmentButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_contractButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_jobMarketButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_resolveEventButton, TouchlineButtonVariant.Secondary);
        _applyTrainingButton.Pressed += OnApplyTrainingPressed;
        _startScoutingButton.Pressed += OnStartScoutingPressed;
        _advanceDayButton.Pressed += OnAdvanceDayPressed;
        _advanceWeekButton.Pressed += OnAdvanceWeekPressed;
        _staffMarketButton.Pressed += OnStaffMarketPressed;
        _youthAcademyButton.Pressed += OnYouthAcademyPressed;
        _recruitmentButton.Pressed += OnRecruitmentPressed;
        _contractButton.Pressed += OnContractPressed;
        _jobMarketButton.Pressed += OnJobMarketPressed;
        _resolveEventButton.Pressed += OnResolveEventPressed;
    }

    private void RenderState()
    {
        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            RenderUnavailableState("Career context unavailable.", "Start or load a career to open the Manager Hub.");
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState("Club selection missing.", "Choose a club before using the Manager Hub.");
            return;
        }

        var state = GameState.Instance;
        var clubName = state.SelectedClubName!;
        var position = GetClubPosition(clubName);
        var tableSize = state.CompetitionTable.Length;
        var currentRow = GetCompetitionRow(clubName);
        var hasMatchReport = state.LastMatchReport != null;
        var careerPhase = state.BuildCareerPhaseSummary();

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"{state.CurrentRoleName} {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CurrentFixtureCompetitionName.ToUpperInvariant();
        _clubContextLabel.Text = $"{clubName} Manager Hub | {state.CurrentRoleName} {state.ManagerName}";
        _careerFoundationLabel.Text =
            $"Background: {state.ManagerBackgroundName} | License: {state.LicenseName}";
        _clubFoundationLabel.Text =
            $"Archetype: {state.ClubArchetypeName} | Board: {state.BoardPhilosophyName} | Fans: {state.FanCultureName} | Director of Football: {state.DirectorOfFootballStyleName} ({state.DirectorRelationshipName})";
        _dateLabel.Text = $"Season {state.SeasonLabel} | {state.CurrentDateLabel} | Matchday {state.CurrentMatchday}";
        _priorityChipLabel.Text = BuildPriorityTag(state);
        SetStateChip(hasMatchReport ? "POST-MATCH" : "MATCH WEEK", hasMatchReport);
        _headerStatusLabel.Text = careerPhase;

        _nextMatchValueLabel.Text = state.CurrentOpponentName;
        _nextMatchMetaLabel.Text = $"Next fixture | {state.NextFixtureSummary}";
        _tableValueLabel.Text = position > 0 ? $"{position}/{tableSize}" : "--";
        _tableMetaLabel.Text = currentRow == null
            ? "Table position unavailable."
            : $"{currentRow.Points} pts | GD {FormatSigned(currentRow.GoalDifference)} | {currentRow.Played} played";
        _moraleValueLabel.Text = $"{state.SquadMorale}";
        _moraleMetaLabel.Text = $"Morale {DescribePulse(state.SquadMorale)} | Fans {state.FanMorale}";
        _boardValueLabel.Text = $"{state.BoardMorale}";
        _boardMetaLabel.Text = $"Trust {state.CareerProfile.BoardTrust} | Pressure {state.JobPressure}";
        _shapeValueLabel.Text = state.TacticalFormation;
        _shapeMetaLabel.Text = $"{state.TeamStyleName} | Press {state.PressIntensity} | Tempo {state.Tempo} | Risk {state.Risk}";

        _fixturePreviewLabel.Text = $"Next match\n{state.NextFixtureSummary}";
        _focusContextLabel.Text = state.CurrentFixtureIsCup
            ? $"{state.CurrentFixtureCompetitionName} | {state.CurrentFixtureRoundName} | {state.BuildLeaguePositionSummary()}"
            : $"{state.BuildLeaguePositionSummary()} | Board {state.BoardPhilosophyName}";
        _recommendedMoveLabel.Text = $"Next best action | {BuildPrioritySummary(state)}";
        _actionHintLabel.Text = state.IsCurrentClubFixtureComplete()
            ? "Post-match logged. Review it, then advance the week."
            : "Check the XI, lock the plan, then go to Matchday.";

        _formValueLabel.Text = BuildCompactForm(state.FormSummary);
        _lastResultLabel.Text = hasMatchReport
            ? $"Last match | {state.LastMatchReport!.Scoreline} | {state.LastMatchReport.ResultLabel}"
            : state.BuildRecentResultsSummary();
        _tableImpactLabel.Text = hasMatchReport
            ? $"{state.LastMatchReport!.TableImpactSummary} | {state.LastMatchReport.StatsSummary}"
            : BuildTableLine(position, tableSize, currentRow);

        _pressureValueLabel.Text = state.PressureCategorySummary;
        _pressureReasonsLabel.Text = PerceptionSystem.BuildPressureReasonSummary(state);

        _squadStatusLabel.Text = TakeLines($"{state.BuildLineupReadinessSummary()}\n{state.SquadStatusSummary}", 2);
        _tacticsSummaryLabel.Text = TakeLines($"{state.BuildTacticalPlanSummary()}\n{state.TacticsFoundationSummary}", 2);
        _roleAuthorityLabel.Text = $"Role authority | {TakeLines(state.RoleAuthoritySummary, 1)}";
        _objectivesLabel.Text = TakeLines(state.MainObjectivesSummary, 3);
        _staffLabel.Text = TakeLines($"{state.StaffSummary}\n{state.StaffImpactSummary}", 2);
        _newsFeedLabel.Text = TakeLines($"{state.NewsFeedSummary}\n{state.DecisionEventSummary}", 2);
        _trainingScoutingLabel.Text = TakeLines(state.TrainingScoutingSummary, 3);
        PopulateTrainingScoutingControls(state);
        _youthAcademyLabel.Text = $"Youth academy\n{TakeLines(state.YouthAcademySummary, 3)}";
        _youthAcademyButton.Disabled = false;
        ApplyRoleActionLabels(state);
        _recruitmentLabel.Text = TakeLines($"{state.RecruitmentFoundationSummary}\n{state.PromiseSummary}", 3);
        _careerMarketLabel.Text = TakeLines($"{state.CareerMarketSummary}\n{state.CareerHistorySummary}", 3);
        _priorityLabel.Text = BuildPrioritySummary(state);
        _statusLabel.Text = hasMatchReport
            ? $"{state.LastMatchReport!.FixtureLabel}: {state.LastMatchReport.Scoreline} | Cause: {state.LastMatchReport.CauseSummary}"
            : $"{careerPhase} | {state.BuildOpponentContextSummary()}";
        _saveHintLabel.Text = SaveSystem.Instance == null
            ? "Save unavailable."
            : "Save the live career state before leaving the session.";
        WriteAuditState();
    }

    private void RenderUnavailableState(string title, string status)
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = title;
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _clubContextLabel.Text = title;
        _careerFoundationLabel.Text = "Career foundation unavailable.";
        _clubFoundationLabel.Text = "Club foundation unavailable.";
        _dateLabel.Text = "Date unavailable";
        _priorityChipLabel.Text = "SETUP";
        SetStateChip("OFFLINE", false);
        _headerStatusLabel.Text = status;
        _nextMatchValueLabel.Text = "--";
        _nextMatchMetaLabel.Text = "Fixture unavailable.";
        _tableValueLabel.Text = "--";
        _tableMetaLabel.Text = "Table unavailable.";
        _moraleValueLabel.Text = "--";
        _moraleMetaLabel.Text = "Morale unavailable.";
        _boardValueLabel.Text = "--";
        _boardMetaLabel.Text = "Board unavailable.";
        _shapeValueLabel.Text = "--";
        _shapeMetaLabel.Text = "Tactics unavailable.";
        _fixturePreviewLabel.Text = "No match context is active.";
        _focusContextLabel.Text = "Manager Hub data unavailable.";
        _recommendedMoveLabel.Text = status;
        _actionHintLabel.Text = "Activate a career flow to unlock the Manager Hub.";
        _formValueLabel.Text = "--";
        _lastResultLabel.Text = "No result context.";
        _tableImpactLabel.Text = "No competition context.";
        _pressureValueLabel.Text = "Pressure unavailable.";
        _pressureReasonsLabel.Text = "Pressure reasons unavailable.";
        _squadStatusLabel.Text = "Squad status unavailable.";
        _tacticsSummaryLabel.Text = "Tactical summary unavailable.";
        _roleAuthorityLabel.Text = "Role authority unavailable.";
        _objectivesLabel.Text = "Objectives unavailable.";
        _staffLabel.Text = "Staff foundation unavailable.";
        _newsFeedLabel.Text = "News feed unavailable.";
        _trainingScoutingLabel.Text = "Training and scouting unavailable.";
        _youthAcademyLabel.Text = "Youth academy unavailable.";
        _trainingFocusOption.Disabled = true;
        _trainingIntensityOption.Disabled = true;
        _applyTrainingButton.Disabled = true;
        _scoutingTargetOption.Disabled = true;
        _scoutingDepthOption.Disabled = true;
        _startScoutingButton.Disabled = true;
        _advanceDayButton.Disabled = true;
        _advanceWeekButton.Disabled = true;
        _youthAcademyButton.Disabled = true;
        _staffMarketButton.Disabled = true;
        _recruitmentLabel.Text = "Recruitment unavailable.";
        _careerMarketLabel.Text = "Career market unavailable.";
        _priorityLabel.Text = status;
        _statusLabel.Text = status;
        _saveHintLabel.Text = "Save unavailable.";
        _saveButton.Disabled = true;
        _recruitmentButton.Disabled = true;
        _contractButton.Disabled = true;
        _jobMarketButton.Disabled = true;
        _matchdayButton.Disabled = true;
        WriteAuditState();
    }

    private void PopulateTrainingScoutingControls(GameState state)
    {
        PopulateOptionButton(
            _trainingFocusOption,
            state.TrainingFocusName,
            "Attacking movement",
            "Defensive shape",
            "Pressing",
            "Possession",
            "Counterattack",
            "Set pieces",
            "Fitness",
            "Recovery",
            "Team cohesion",
            "Youth integration");
        PopulateOptionButton(
            _trainingIntensityOption,
            state.TrainingIntensityName,
            "Controlled",
            "Standard",
            "Demanding");
        PopulateOptionButton(
            _scoutingTargetOption,
            state.CurrentScoutingAssignment?.Target ?? "Position need: versatile midfielder",
            "Position need: versatile midfielder",
            "Specific player: pressing winger",
            "Specific player: central midfielder",
            "Opponent style: next fixture",
            "Loan watch: young forward");
        PopulateOptionButton(
            _scoutingDepthOption,
            state.ScoutingReportDepthName,
            "Quick look",
            "Standard report",
            "Full report");

        _trainingFocusOption.Disabled = false;
        _trainingIntensityOption.Disabled = false;
        _applyTrainingButton.Disabled = false;
        _scoutingTargetOption.Disabled = false;
        _scoutingDepthOption.Disabled = false;
        _startScoutingButton.Disabled = false;
        _advanceDayButton.Disabled = false;
        _advanceWeekButton.Disabled = false;
    }

    private void ApplyRoleActionLabels(GameState state)
    {
        if (state.CareerProfile.Role == ManagerRole.AssistantManager)
        {
            _applyTrainingButton.Text = "Recommend Training Focus";
            _startScoutingButton.Text = "Recommend Scouting Priority";
            _staffMarketButton.Text = "Recommend Staff Review";
            _youthAcademyButton.Text = "Recommend Youth Review";
            _recruitmentButton.Text = "Recommend Recruitment Target";
            _contractButton.Text = "Recommend Contract Terms";
            return;
        }

        if (state.CareerProfile.Role == ManagerRole.HeadCoach)
        {
            _applyTrainingButton.Text = "Apply Training Plan";
            _startScoutingButton.Text = "Request Scouting Priority";
            _staffMarketButton.Text = "Request Staff Review";
            _youthAcademyButton.Text = "Request Youth Promotion Review";
            _recruitmentButton.Text = "Request Recruitment Review";
            _contractButton.Text = "Request Contract Review";
            return;
        }

        _applyTrainingButton.Text = "Apply Training Plan";
        _startScoutingButton.Text = "Start Scouting Assignment";
        _staffMarketButton.Text = "Review Staff Market";
        _youthAcademyButton.Text = "Review Youth Academy";
        _recruitmentButton.Text = "Progress Recruitment Approach";
        _contractButton.Text = "Review Contract Terms";
    }

    private static void PopulateOptionButton(OptionButton option, string selectedValue, params string[] values)
    {
        option.Clear();
        var selectedIndex = 0;
        for (var index = 0; index < values.Length; index++)
        {
            option.AddItem(values[index], index);
            if (values[index] == selectedValue)
            {
                selectedIndex = index;
            }
        }

        option.Select(selectedIndex);
    }

    private static string GetSelectedOptionText(OptionButton option)
    {
        var selectedIndex = option.Selected;
        return selectedIndex < 0 ? string.Empty : option.GetItemText(selectedIndex);
    }

    private void SetStateChip(string text, bool positive)
    {
        _stateChipLabel.Text = text;
        TouchlineTheme.ApplyPanelVariant(_stateChip, positive ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Muted, 999);
    }

    private static string BuildPrioritySummary(GameState state)
    {
        if (state.BoardConfidence < 50)
        {
            return "Board pressure is rising; protect the next result.";
        }

        if (state.TeamMorale < 60)
        {
            return "Squad morale is fragile; check the dressing room pulse.";
        }

        if (state.LastMatchReport == null)
        {
            return "Opening week: settle the XI and confirm the plan.";
        }

        return "Track pressure, review the squad, then go again.";
    }

    private static string BuildPriorityTag(GameState state)
    {
        if (state.BoardConfidence < 50)
        {
            return "BOARD PRESSURE";
        }

        if (state.TeamMorale < 60)
        {
            return "SQUAD PULSE";
        }

        return state.LastMatchReport == null ? "OPENING WEEK" : "SEASON RHYTHM";
    }

    private static string BuildCompactForm(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? formSummary["Form: ".Length..] : formSummary;
    }

    private static string TakeLines(string text, int maxLines)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return "Unavailable.";
        }

        var lines = text.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return text.Trim();
        }

        var visibleCount = System.Math.Min(maxLines, lines.Length);
        return string.Join("\n", lines[..visibleCount]).Trim();
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

    private static string BuildTableLine(int position, int tableSize, GameState.CompetitionRow? row)
    {
        if (position <= 0 || row == null)
        {
            return "Table context unavailable.";
        }

        return $"Position {position} of {tableSize} | {row.Points} pts | {row.GoalsFor} GF | {row.GoalsAgainst} GA";
    }

    private static string DescribePulse(int value)
    {
        return value switch
        {
            >= 75 => "surging",
            >= 60 => "steady",
            >= 45 => "edgy",
            _ => "under strain"
        };
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }

    private void WriteAuditState()
    {
        AuditUiStateWriter.Write(
            nameof(ClubDashboard),
            _managerLabel.Text,
            TouchlineRailRoute.Dashboard,
            _clubContextLabel.Text,
            _fixturePreviewLabel.Text,
            _recommendedMoveLabel.Text,
            _actionHintLabel.Text,
            _roleAuthorityLabel.Text,
            _trainingScoutingLabel.Text,
            _recruitmentLabel.Text,
            _careerMarketLabel.Text,
            _applyTrainingButton.Text,
            _startScoutingButton.Text,
            _recruitmentButton.Text,
            _contractButton.Text,
            _jobMarketButton.Text,
            _statusLabel.Text);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
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

    private void OnStandingsPressed()
    {
        GetTree().ChangeSceneToFile(StandingsScreenScenePath);
    }

    private void OnMatchdayPressed()
    {
        GetTree().ChangeSceneToFile(MatchdayScenePath);
    }

    private void OnSavePressed()
    {
        if (SaveSystem.Instance == null)
        {
            _statusLabel.Text = "Save system unavailable.";
            _saveHintLabel.Text = "Save system unavailable.";
            SetStateChip("SAVE OFFLINE", false);
            WriteAuditState();
            return;
        }

        SaveSystem.Instance.SaveGame(out var statusMessage);
        _statusLabel.Text = statusMessage;
        _saveHintLabel.Text = statusMessage;
        SetStateChip("CAREER SAVED", true);
        WriteAuditState();
    }

    private void OnApplyTrainingPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        var status = GameState.Instance.RequestTrainingPlanByName(
            GetSelectedOptionText(_trainingFocusOption),
            GetSelectedOptionText(_trainingIntensityOption));
        RenderState();
        _statusLabel.Text = status;
        WriteAuditState();
    }

    private void OnStartScoutingPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        var status = GameState.Instance.RequestScoutingAssignment(
            GetSelectedOptionText(_scoutingTargetOption),
            GetSelectedOptionText(_scoutingDepthOption));
        RenderState();
        _statusLabel.Text = status;
        WriteAuditState();
    }

    private void OnAdvanceDayPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AdvanceOneCareerDay()
            ? "Advanced one career day; training and scouting state progressed."
            : "Career day could not advance without an active club.";
        RenderState();
        WriteAuditState();
    }

    private void OnAdvanceWeekPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AdvanceOneCareerWeek()
            ? "Advanced one career week; training, scouting, pressure, and news updated."
            : "Career week could not advance without an active club.";
        RenderState();
        WriteAuditState();
    }

    private void OnStaffMarketPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AttemptStaffMarketAction();
        RenderState();
        WriteAuditState();
    }

    private void OnYouthAcademyPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AdvanceYouthAcademyAction();
        RenderState();
        WriteAuditState();
    }

    private void OnRecruitmentPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AttemptBasicRecruitmentAction();
        RenderState();
        WriteAuditState();
    }

    private void OnContractPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AttemptBasicContractNegotiation();
        RenderState();
        WriteAuditState();
    }

    private void OnJobMarketPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.AdvanceCareerJobMarketAction();
        RenderState();
        WriteAuditState();
    }

    private void OnResolveEventPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        _statusLabel.Text = GameState.Instance.ResolveActiveDecisionEvent();
        RenderState();
        WriteAuditState();
    }
}
