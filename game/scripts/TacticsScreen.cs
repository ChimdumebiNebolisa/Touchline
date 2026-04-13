using Godot;

public partial class TacticsScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";
    private const string FixturesScreenScenePath = "res://scenes/FixturesScreen.tscn";
    private const string StandingsScreenScenePath = "res://scenes/StandingsScreen.tscn";
    private const string MatchdayScenePath = "res://scenes/MatchdayScene.tscn";

    private Label _clubBadgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _managerLabel = default!;
    private Label _seasonLabel = default!;
    private Label _competitionChipLabel = default!;
    private Label _clubContextLabel = default!;
    private Label _matchPlanLabel = default!;
    private Label _tacticalChipLabel = default!;
    private Label _readinessChipLabel = default!;
    private Label _headerStatusLabel = default!;
    private Label _formationValueLabel = default!;
    private Label _formationMetaLabel = default!;
    private Label _pressValueLabel = default!;
    private Label _pressMetaLabel = default!;
    private Label _tempoValueLabel = default!;
    private Label _tempoMetaLabel = default!;
    private Label _widthValueLabel = default!;
    private Label _widthMetaLabel = default!;
    private Label _riskValueLabel = default!;
    private Label _riskMetaLabel = default!;
    private Label _formationBadgeLabel = default!;
    private Label _pitchSummaryLabel = default!;
    private Label _shapeSummaryLabel = default!;
    private Label _previewSummaryLabel = default!;
    private Label _statusLabel = default!;
    private Label _pressPreviewLabel = default!;
    private Label _tempoPreviewLabel = default!;
    private Label _widthPreviewLabel = default!;
    private Label _riskPreviewLabel = default!;
    private Label _controlHintLabel = default!;
    private Label _controlSummaryLabel = default!;
    private Label _savedPlanLabel = default!;
    private Label _saveHintLabel = default!;
    private Label _frontRowLabel = default!;
    private Label _attackBandLabel = default!;
    private Label _midfieldBandLabel = default!;
    private Label _backLineLabel = default!;
    private Label _keeperLabel = default!;
    private Label _leftChannelLabel = default!;
    private Label _centerChannelLabel = default!;
    private Label _rightChannelLabel = default!;

    private PanelContainer _railCard = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _competitionChip = default!;
    private PanelContainer _tacticalChip = default!;
    private PanelContainer _readinessChip = default!;
    private PanelContainer _pitchCard = default!;
    private PanelContainer _pitchPanel = default!;
    private PanelContainer _controlsCard = default!;
    private PanelContainer _notesCard = default!;

    private Button _dashboardButton = default!;
    private Button _squadButton = default!;
    private Button _tacticsButton = default!;
    private Button _fixturesButton = default!;
    private Button _standingsButton = default!;
    private Button _matchdayButton = default!;
    private Button _saveButton = default!;
    private Button _resetButton = default!;
    private Button _backButton = default!;

    private OptionButton _formationOption = default!;
    private SpinBox _pressSpin = default!;
    private SpinBox _tempoSpin = default!;
    private SpinBox _widthSpin = default!;
    private SpinBox _riskSpin = default!;

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
        _resetButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/ResetButton");
        _saveHintLabel = GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/SaveHintLabel");
        _backButton = GetNode<Button>("RootMargin/Shell/RailCard/RailPadding/RailContent/FooterActions/BackButton");
        _headerCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard");
        _clubContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel");
        _matchPlanLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/MatchPlanLabel");
        _tacticalChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/TacticalChip");
        _tacticalChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/TacticalChip/TacticalChipPadding/TacticalChipLabel");
        _readinessChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/ReadinessChip");
        _readinessChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/ReadinessChip/ReadinessChipPadding/ReadinessChipLabel");
        _headerStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel");
        _formationValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormationCard/CardPadding/CardContent/CardValueLabel");
        _formationMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormationCard/CardPadding/CardContent/CardMetaLabel");
        _pressValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PressCard/CardPadding/CardContent/CardValueLabel");
        _pressMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PressCard/CardPadding/CardContent/CardMetaLabel");
        _tempoValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TempoCard/CardPadding/CardContent/CardValueLabel");
        _tempoMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/TempoCard/CardPadding/CardContent/CardMetaLabel");
        _widthValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/WidthCard/CardPadding/CardContent/CardValueLabel");
        _widthMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/WidthCard/CardPadding/CardContent/CardMetaLabel");
        _riskValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/RiskCard/CardPadding/CardContent/CardValueLabel");
        _riskMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/RiskCard/CardPadding/CardContent/CardMetaLabel");
        _pitchCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/PitchCard");
        _formationBadgeLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/TopMeta/FormationBadgeLabel");
        _pitchSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/TopMeta/PitchSummaryLabel");
        _pitchPanel = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel");
        _frontRowLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows/FrontRow/FrontRowLabel");
        _attackBandLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows/AttackBand/AttackBandLabel");
        _midfieldBandLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows/MidfieldBand/MidfieldBandLabel");
        _backLineLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows/BackLine/BackLineLabel");
        _keeperLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows/KeeperRow/KeeperLabel");
        _leftChannelLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/ChannelRow/LeftChannel");
        _centerChannelLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/ChannelRow/CenterChannel");
        _rightChannelLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/ChannelRow/RightChannel");
        _shapeSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/ShapeSummaryLabel");
        _previewSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PreviewSummaryLabel");
        _statusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/StatusLabel");
        _controlsCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard");
        _controlHintLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/ControlsHintLabel");
        _formationOption = GetNode<OptionButton>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/FormationOption");
        _pressSpin = GetNode<SpinBox>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/PressSpin");
        _tempoSpin = GetNode<SpinBox>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/TempoSpin");
        _widthSpin = GetNode<SpinBox>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/WidthSpin");
        _riskSpin = GetNode<SpinBox>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/RiskSpin");
        _controlSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/ControlsCard/ControlsPadding/ControlsContent/ControlSummaryLabel");
        _notesCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard");
        _pressPreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/PressPreviewLabel");
        _tempoPreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/TempoPreviewLabel");
        _widthPreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/WidthPreviewLabel");
        _riskPreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/RiskPreviewLabel");
        _savedPlanLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SideStack/NotesCard/NotesPadding/NotesContent/SavedPlanLabel");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_railCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_competitionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_tacticalChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_readinessChip, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard"), TouchlineSurfaceVariant.Shell, 22);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/FormationCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/PressCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/TempoCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/WidthCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/RiskCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(_pitchCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_pitchPanel, TouchlineSurfaceVariant.Positive, 28);
        TouchlineTheme.ApplyPanelVariant(_controlsCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_notesCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyButtonVariant(_dashboardButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_squadButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_tacticsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_fixturesButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_standingsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_matchdayButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_saveButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_resetButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        _tacticsButton.Disabled = true;

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_tacticalChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_readinessChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_clubContextLabel, 18);
        TouchlineTheme.ApplyMutedStyle(_matchPlanLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyValueStyle(_formationValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_pressValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_tempoValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_widthValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_riskValueLabel, 30);
        TouchlineTheme.ApplyMutedStyle(_formationMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pressMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tempoMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_widthMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_riskMetaLabel, 14);
        TouchlineTheme.ApplyValueStyle(_formationBadgeLabel, 24);
        TouchlineTheme.ApplyMutedStyle(_pitchSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_shapeSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_previewSummaryLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_controlHintLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_controlSummaryLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pressPreviewLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tempoPreviewLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_widthPreviewLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_riskPreviewLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_savedPlanLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_saveHintLabel, 14);
        TouchlineTheme.ApplyEyebrowStyle(_leftChannelLabel);
        TouchlineTheme.ApplyEyebrowStyle(_centerChannelLabel);
        TouchlineTheme.ApplyEyebrowStyle(_rightChannelLabel);
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

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"Manager {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CompetitionName.ToUpperInvariant();
        _clubContextLabel.Text = $"{clubName} tactical board";
        _matchPlanLabel.Text = $"{clubName} vs {state.CurrentOpponentName} | Matchday {state.CurrentMatchday}";

        var formationIndex = FindFormationIndex(state.TacticalFormation);
        _formationOption.Select(formationIndex);
        _pressSpin.Value = state.PressIntensity;
        _tempoSpin.Value = state.Tempo;
        _widthSpin.Value = state.Width;
        _riskSpin.Value = state.Risk;

        _savedPlanLabel.Text = BuildSavedPlanSummary(state);
        _saveHintLabel.Text = "Save to lock this plan into the next fixture.";
        RefreshBoard();
    }

    private void RenderUnavailableState()
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = "Manager unavailable";
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _clubContextLabel.Text = "Tactical context unavailable.";
        _matchPlanLabel.Text = "Match plan unavailable.";
        _tacticalChipLabel.Text = "OFFLINE";
        SetReadinessChip("UNAVAILABLE", false);
        _headerStatusLabel.Text = "Set up a career and club before editing tactics.";
        _formationValueLabel.Text = "--";
        _formationMetaLabel.Text = "Formation unavailable.";
        _pressValueLabel.Text = "--";
        _pressMetaLabel.Text = "Press unavailable.";
        _tempoValueLabel.Text = "--";
        _tempoMetaLabel.Text = "Tempo unavailable.";
        _widthValueLabel.Text = "--";
        _widthMetaLabel.Text = "Width unavailable.";
        _riskValueLabel.Text = "--";
        _riskMetaLabel.Text = "Risk unavailable.";
        _formationBadgeLabel.Text = "No tactical shape loaded";
        _pitchSummaryLabel.Text = "Pitch board unavailable.";
        _frontRowLabel.Text = " ";
        _attackBandLabel.Text = " ";
        _midfieldBandLabel.Text = " ";
        _backLineLabel.Text = " ";
        _keeperLabel.Text = " ";
        _leftChannelLabel.Text = "LEFT";
        _centerChannelLabel.Text = "CENTRE";
        _rightChannelLabel.Text = "RIGHT";
        _shapeSummaryLabel.Text = "Shape note unavailable.";
        _previewSummaryLabel.Text = "Tactical summary unavailable.";
        _statusLabel.Text = "Set up a career and club before editing tactics.";
        _controlHintLabel.Text = "Controls will unlock when a club is active.";
        _controlSummaryLabel.Text = "No tactical state is loaded.";
        _pressPreviewLabel.Text = "Press note unavailable.";
        _tempoPreviewLabel.Text = "Tempo note unavailable.";
        _widthPreviewLabel.Text = "Width note unavailable.";
        _riskPreviewLabel.Text = "Risk note unavailable.";
        _savedPlanLabel.Text = "No saved tactical plan.";
        _saveHintLabel.Text = "Save unavailable.";
        SetControlsDisabled(true);
        _saveButton.Disabled = true;
        _resetButton.Disabled = true;
        _matchdayButton.Disabled = true;
    }

    private void RefreshBoard()
    {
        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        _tacticalChipLabel.Text = BuildTacticalChipLabel(formation, press, tempo);
        SetReadinessChip(BuildReadinessLabel(press, tempo, risk), true);
        _headerStatusLabel.Text = "Shape, intensity, and width update live before you save the match plan.";

        _formationValueLabel.Text = formation;
        _formationMetaLabel.Text = BuildFormationMeta(formation);
        _pressValueLabel.Text = DescribePress(press);
        _pressMetaLabel.Text = BuildPressMeta(press);
        _tempoValueLabel.Text = DescribeTempo(tempo);
        _tempoMetaLabel.Text = BuildTempoMeta(tempo);
        _widthValueLabel.Text = DescribeWidth(width);
        _widthMetaLabel.Text = BuildWidthMeta(width);
        _riskValueLabel.Text = DescribeRisk(risk);
        _riskMetaLabel.Text = BuildRiskMeta(risk);

        _formationBadgeLabel.Text = $"{formation} match shell";
        _pitchSummaryLabel.Text = BuildPitchSummary(formation);
        ApplyFormationRows(formation, width);
        _shapeSummaryLabel.Text = BuildShapeSummary(formation);
        _previewSummaryLabel.Text = BuildPreviewSummary(formation, press, tempo, width, risk);
        _statusLabel.Text = "Adjust the board, read the pitch shape, then save when the plan fits the next fixture.";
        _controlHintLabel.Text = "One clear call: shape the next match, then lock it in.";
        _controlSummaryLabel.Text = BuildControlSummary(formation, press, tempo, width, risk);
        _pressPreviewLabel.Text = $"Press line: {DescribePress(press)}. {BuildPressPreview(press)}";
        _tempoPreviewLabel.Text = $"Ball speed: {DescribeTempo(tempo)}. {BuildTempoPreview(tempo)}";
        _widthPreviewLabel.Text = $"Pitch use: {DescribeWidth(width)}. {BuildWidthPreview(width)}";
        _riskPreviewLabel.Text = $"Commitment: {DescribeRisk(risk)}. {BuildRiskPreview(risk)}";
    }

    private void ApplyFormationRows(string formation, int width)
    {
        switch (formation)
        {
            case "4-2-3-1":
                _frontRowLabel.Text = "ST";
                _attackBandLabel.Text = "LW    AM    RW";
                _midfieldBandLabel.Text = "CM        CM";
                _backLineLabel.Text = "LB    CB    CB    RB";
                break;
            case "3-5-2":
                _frontRowLabel.Text = "ST          ST";
                _attackBandLabel.Text = "AM";
                _midfieldBandLabel.Text = "LWB   CM   CM   RWB";
                _backLineLabel.Text = "CB    CB    CB";
                break;
            default:
                _frontRowLabel.Text = "LW    ST    RW";
                _attackBandLabel.Text = "AM";
                _midfieldBandLabel.Text = "CM        CM";
                _backLineLabel.Text = "LB    CB    CB    RB";
                break;
        }

        _keeperLabel.Text = "GK";
        _leftChannelLabel.Text = width >= 55 ? "LEFT OVERLOAD" : "LEFT HALF-SPACE";
        _centerChannelLabel.Text = width < 40 ? "COMPACT CENTRE" : "CENTRAL ACCESS";
        _rightChannelLabel.Text = width >= 55 ? "RIGHT OVERLOAD" : "RIGHT HALF-SPACE";
    }

    private void OnSavePressed()
    {
        if (GameState.Instance == null)
        {
            _statusLabel.Text = "GameState singleton is unavailable.";
            _saveHintLabel.Text = "Save unavailable.";
            SetReadinessChip("SAVE OFFLINE", false);
            return;
        }

        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        GameState.Instance.UpdateTactics(formation, press, tempo, width, risk);
        _savedPlanLabel.Text = BuildSavedPlanSummary(GameState.Instance);
        RefreshBoard();
        _statusLabel.Text = $"Saved tactical board: {formation} | {DescribePress(press)} | {DescribeTempo(tempo)} | {DescribeWidth(width)} | {DescribeRisk(risk)}";
        _saveHintLabel.Text = "Tactical board saved into the next fixture.";
        SetReadinessChip("PLAN SAVED", true);
    }

    private void OnResetPressed()
    {
        if (GameState.Instance == null)
        {
            _statusLabel.Text = "No saved tactical state is available.";
            return;
        }

        var formationIndex = FindFormationIndex(GameState.Instance.TacticalFormation);
        _formationOption.Select(formationIndex);
        _pressSpin.Value = GameState.Instance.PressIntensity;
        _tempoSpin.Value = GameState.Instance.Tempo;
        _widthSpin.Value = GameState.Instance.Width;
        _riskSpin.Value = GameState.Instance.Risk;
        RefreshBoard();
        _statusLabel.Text = "Reverted the board to the currently saved tactical plan.";
        _saveHintLabel.Text = "Controls reset to the last saved plan.";
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

    private void OnTacticControlChanged(double _value)
    {
        RefreshBoard();
        _statusLabel.Text = "Preview updated. Save when the board matches the next match plan.";
    }

    private void OnFormationSelected(long _index)
    {
        RefreshBoard();
        _statusLabel.Text = "Formation changed on the board. Save to lock the new shape.";
    }

    private void SetControlsDisabled(bool disabled)
    {
        _formationOption.Disabled = disabled;
        _pressSpin.Editable = !disabled;
        _tempoSpin.Editable = !disabled;
        _widthSpin.Editable = !disabled;
        _riskSpin.Editable = !disabled;
    }

    private void SetReadinessChip(string text, bool positive)
    {
        _readinessChipLabel.Text = text;
        TouchlineTheme.ApplyPanelVariant(_readinessChip, positive ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Muted, 999);
    }

    private int FindFormationIndex(string formation)
    {
        for (var index = 0; index < _formationOption.ItemCount; index++)
        {
            if (_formationOption.GetItemText(index) == formation)
            {
                return index;
            }
        }

        return 0;
    }

    private static string BuildSavedPlanSummary(GameState state)
    {
        return $"Saved plan: {state.TacticalFormation} | Press {state.PressIntensity} | Tempo {state.Tempo} | Width {state.Width} | Risk {state.Risk}";
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

    private static string BuildTacticalChipLabel(string formation, int press, int tempo)
    {
        if (press >= 70 && tempo >= 65)
        {
            return $"{formation} FRONT-FOOT";
        }

        if (press < 40 && tempo < 40)
        {
            return $"{formation} CONTROL BLOCK";
        }

        return $"{formation} MATCH PLAN";
    }

    private static string BuildReadinessLabel(int press, int tempo, int risk)
    {
        if (press >= 70 && risk >= 65)
        {
            return "AGGRESSIVE";
        }

        if (tempo < 40 && risk < 40)
        {
            return "CONTROLLED";
        }

        return "READY";
    }

    private static string BuildFormationMeta(string formation)
    {
        return formation switch
        {
            "4-2-3-1" => "Double pivot with a dedicated 10",
            "3-5-2" => "Back three with wing-back width",
            _ => "Wide front three and central triangle"
        };
    }

    private static string BuildPressMeta(int value)
    {
        return value switch
        {
            >= 75 => "Front line jumps early",
            >= 55 => "Midfield engagement on cue",
            >= 35 => "Measured jumps from shape",
            _ => "Deep rest-defense first"
        };
    }

    private static string BuildTempoMeta(int value)
    {
        return value switch
        {
            >= 75 => "Early release into transitions",
            >= 55 => "Positive vertical circulation",
            >= 35 => "Balanced possession rhythm",
            _ => "Calm buildup and reset"
        };
    }

    private static string BuildWidthMeta(int value)
    {
        return value switch
        {
            >= 75 => "Touchline width stays open",
            >= 55 => "Wing access stays available",
            >= 35 => "Half-space balance",
            _ => "Inside lanes prioritized"
        };
    }

    private static string BuildRiskMeta(int value)
    {
        return value switch
        {
            >= 75 => "Extra runners beyond the ball",
            >= 55 => "Progressive support behind attacks",
            >= 35 => "Balanced rest-defense cover",
            _ => "Structure before volume"
        };
    }

    private static string BuildPitchSummary(string formation)
    {
        return formation switch
        {
            "4-2-3-1" => "The pitch board shows a single striker, a line of three creators, and a protective double pivot.",
            "3-5-2" => "The pitch board leans into central overloads with wing-backs stretching the next line.",
            _ => "The pitch board uses a wide front line with midfield support arriving underneath."
        };
    }

    private static string BuildShapeSummary(string formation)
    {
        return formation switch
        {
            "4-2-3-1" => "Shape note: the double pivot protects rest defense while the 10 attacks the space between opposition lines.",
            "3-5-2" => "Shape note: the back three stabilizes buildup and lets the wing-backs decide whether the side stretches or compresses play.",
            _ => "Shape note: the wide forwards pin the back line while the midfield pair supports second balls and central access."
        };
    }

    private static string BuildPreviewSummary(string formation, int press, int tempo, int width, int risk)
    {
        return $"Match plan: {formation} with {DescribePress(press).ToLowerInvariant()}, {DescribeTempo(tempo).ToLowerInvariant()}, {DescribeWidth(width).ToLowerInvariant()}, and {DescribeRisk(risk).ToLowerInvariant()}.";
    }

    private static string BuildControlSummary(string formation, int press, int tempo, int width, int risk)
    {
        return $"{formation} | {DescribePress(press)} | {DescribeTempo(tempo)} | {DescribeWidth(width)} | {DescribeRisk(risk)}";
    }

    private static string DescribePress(int value)
    {
        return value switch
        {
            >= 75 => "High press",
            >= 55 => "Active press",
            >= 35 => "Measured press",
            _ => "Deep block"
        };
    }

    private static string BuildPressPreview(int value)
    {
        return value switch
        {
            >= 75 => "The front line steps early and tries to trap play before midfield settles.",
            >= 55 => "The side engages in midfield and looks to recover quickly after turnovers.",
            >= 35 => "The team holds its distances before jumping, protecting the center first.",
            _ => "The side drops into shape and waits for clearer interception moments."
        };
    }

    private static string DescribeTempo(int value)
    {
        return value switch
        {
            >= 75 => "Fast tempo",
            >= 55 => "Positive tempo",
            >= 35 => "Balanced tempo",
            _ => "Patient tempo"
        };
    }

    private static string BuildTempoPreview(int value)
    {
        return value switch
        {
            >= 75 => "Possession should release early into transitions and attack unsettled lines.",
            >= 55 => "The ball moves with intent without abandoning structure after each pass.",
            >= 35 => "The team can recycle and reset before forcing the next vertical action.",
            _ => "Possession slows down to secure control and reduce loose exchanges."
        };
    }

    private static string DescribeWidth(int value)
    {
        return value switch
        {
            >= 75 => "Very wide shape",
            >= 55 => "Wide shape",
            >= 35 => "Balanced width",
            _ => "Narrow shape"
        };
    }

    private static string BuildWidthPreview(int value)
    {
        return value switch
        {
            >= 75 => "Outside lanes stay open to isolate full-backs and stretch defensive cover.",
            >= 55 => "The team looks to hold the wings and create room for central arrivals.",
            >= 35 => "Attacks can use both the half-spaces and the touchline without overcommitting either.",
            _ => "The side compresses play inside and asks runners to combine through central lanes."
        };
    }

    private static string DescribeRisk(int value)
    {
        return value switch
        {
            >= 75 => "High risk",
            >= 55 => "Progressive risk",
            >= 35 => "Balanced risk",
            _ => "Low risk"
        };
    }

    private static string BuildRiskPreview(int value)
    {
        return value switch
        {
            >= 75 => "More runners join attacks and the rest defense will live with larger spaces behind the ball.",
            >= 55 => "The side supports attacks with extra bodies while still trying to keep one recovery layer.",
            >= 35 => "The team balances support runs with enough cover to stop immediate transitions.",
            _ => "The shape values structure first and sends fewer bodies ahead of the play."
        };
    }
}
