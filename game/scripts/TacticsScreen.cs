using Godot;
using System;
using System.Collections.Generic;

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
    private Control _pitchRows = default!;
    private Control _pitchField = default!;
    private PitchDrawingControl _tacticalBoard = default!;

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
    private OptionButton _styleOption = default!;
    private SpinBox _pressSpin = default!;
    private SpinBox _tempoSpin = default!;
    private SpinBox _widthSpin = default!;
    private SpinBox _riskSpin = default!;
    private string? _selectedTacticPlayerName;

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
        _pitchField = GetNode<Control>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField");
        _pitchRows = GetNode<Control>("RootMargin/Shell/MainColumn/ContentRow/PitchCard/PitchPadding/PitchContent/PitchPanel/PitchPanelPadding/PitchField/PitchRows");
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
        EnsureTeamStyleOption();
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
        _pitchRows.Visible = false;
        if (_tacticalBoard == null)
        {
            _tacticalBoard = new PitchDrawingControl
            {
                Name = "TacticalBoard",
                CustomMinimumSize = new Vector2(0, 330),
                SizeFlagsHorizontal = SizeFlags.ExpandFill,
                SizeFlagsVertical = SizeFlags.ExpandFill,
                ClipContents = true
            };
            _pitchField.AddChild(_tacticalBoard);
        }
        TouchlineTheme.ApplyRailNavigation(
            _dashboardButton,
            _squadButton,
            _tacticsButton,
            _fixturesButton,
            _standingsButton,
            _matchdayButton,
            TouchlineRailRoute.Tactics);
        TouchlineTheme.ApplyButtonVariant(_saveButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_resetButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);

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

    private void EnsureTeamStyleOption()
    {
        var parent = _formationOption.GetParent();
        var existing = parent.GetNodeOrNull<OptionButton>("TeamStyleOption");
        if (existing != null)
        {
            _styleOption = existing;
            return;
        }

        var label = new Label
        {
            Name = "TeamStyleLabel",
            Text = "Team Style"
        };
        label.AddThemeFontSizeOverride("font_size", 14);
        label.AddThemeColorOverride("font_color", TouchlineTheme.TextMuted);

        _styleOption = new OptionButton
        {
            Name = "TeamStyleOption",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        foreach (var style in new[]
        {
            "Balanced",
            "Possession",
            "Direct Play",
            "Counterattack",
            "High Press",
            "Low Block",
            "Wide Attack",
            "Central Overload",
            "Defensive Solidity"
        })
        {
            _styleOption.AddItem(style);
        }

        _styleOption.ItemSelected += OnStyleSelected;
        var insertIndex = _formationOption.GetIndex() + 1;
        parent.AddChild(label);
        parent.MoveChild(label, insertIndex);
        parent.AddChild(_styleOption);
        parent.MoveChild(_styleOption, insertIndex + 1);
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
        _managerLabel.Text = $"{state.CurrentRoleName} {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CompetitionName.ToUpperInvariant();
        _clubContextLabel.Text = $"{clubName} tactical board";
        _matchPlanLabel.Text = $"{clubName} vs {state.CurrentOpponentName} | Matchday {state.CurrentMatchday}";

        var formationIndex = FindFormationIndex(state.TacticalFormation);
        _formationOption.Select(formationIndex);
        _styleOption.Select(FindStyleIndex(state.TeamStyleName));
        _pressSpin.Value = state.PressIntensity;
        _tempoSpin.Value = state.Tempo;
        _widthSpin.Value = state.Width;
        _riskSpin.Value = state.Risk;

        _savedPlanLabel.Text = BuildSavedPlanSummary(state);
        _saveHintLabel.Text = state.CareerProfile.Role == ManagerRole.AssistantManager
            ? "Unsaved preview: submit tactical recommendations without changing the saved match plan."
            : "Unsaved preview: adjust the board, then save to apply it to the shared match engine.";
        _saveButton.Text = state.CareerProfile.Role == ManagerRole.AssistantManager
            ? "Submit Tactical Recommendation"
            : "Save Tactical Plan";
        RefreshBoard();
        WriteAuditState();
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
        WriteAuditState();
    }

    private void RefreshBoard()
    {
        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var style = _styleOption.GetItemText(_styleOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        _tacticalChipLabel.Text = BuildTacticalChipLabel(formation, style, press, tempo);
        SetReadinessChip(BuildReadinessLabel(press, tempo, risk), true);
        _headerStatusLabel.Text = "Shape, team style, instructions, familiarity, fit, and risk update live before you save the match plan.";

        _formationValueLabel.Text = formation;
        _formationMetaLabel.Text = $"{BuildFormationMeta(formation)} | {style}";
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
        _previewSummaryLabel.Text = BuildPreviewSummary(formation, style, press, tempo, width, risk);
        _statusLabel.Text = "Preview mode: these values explain the next match plan before they are saved.";
        _controlHintLabel.Text = "Preview values are unsaved until Save Tactical Plan applies them.";
        _controlSummaryLabel.Text = BuildControlSummary(formation, style, press, tempo, width, risk);
        _pressPreviewLabel.Text = $"Pressing Intensity: {DescribePress(press)}. {BuildPressPreview(press)}";
        _tempoPreviewLabel.Text = $"Tempo: {DescribeTempo(tempo)}. {BuildTempoPreview(tempo)}";
        _widthPreviewLabel.Text = $"Pitch use: {DescribeWidth(width)}. {BuildWidthPreview(width)}";
        _riskPreviewLabel.Text = $"Mentality: {DescribeRisk(risk)}. {BuildRiskPreview(risk)}";
        WriteAuditState();
    }

    private void ApplyFormationRows(string formation, int width)
    {
        _frontRowLabel.Text = string.Empty;
        _attackBandLabel.Text = string.Empty;
        _midfieldBandLabel.Text = string.Empty;
        _backLineLabel.Text = string.Empty;
        _keeperLabel.Text = string.Empty;
        _leftChannelLabel.Text = width >= 55 ? "LEFT OVERLOAD" : "LEFT HALF-SPACE";
        _centerChannelLabel.Text = width < 40 ? "COMPACT CENTRE" : "CENTRAL ACCESS";
        _rightChannelLabel.Text = width >= 55 ? "RIGHT OVERLOAD" : "RIGHT HALF-SPACE";
        RenderTacticalBoard(formation);
    }

    private void RenderTacticalBoard(string formation)
    {
        if (_tacticalBoard == null)
        {
            return;
        }

        foreach (Node child in _tacticalBoard.GetChildren())
        {
            child.QueueFree();
        }

        if (GameState.Instance == null)
        {
            return;
        }

        var assigned = new HashSet<int>();
        var slots = BuildTacticSlots(formation);
        for (var slotIndex = 0; slotIndex < slots.Length; slotIndex++)
        {
            var slot = slots[slotIndex];
            var playerIndex = FindPlayerForSlot(GameState.Instance, slot.Role, assigned);
            if (playerIndex >= 0)
            {
                assigned.Add(playerIndex);
                var player = GameState.Instance.SquadPlayers[playerIndex];
                _tacticalBoard.AddChild(CreateTacticMarker(slot, player, slotIndex));
            }
            else
            {
                _tacticalBoard.AddChild(CreateEmptyTacticMarker(slot, slotIndex));
            }
        }

        _tacticalBoard.QueueRedraw();
    }

    private Control CreateTacticMarker(TacticSlot slot, GameState.SquadPlayer player, int slotIndex)
    {
        var selected = _selectedTacticPlayerName == player.Name;
        var panel = CreateMarkerPanel(slot, slotIndex, selected);
        panel.Name = $"TacticMarker_{SanitizeNodeName(player.Name)}";
        panel.TooltipText = $"{player.Name} | {player.Position} | Age {player.Age} | Form {player.Form} | Morale {player.Morale} | Fitness {player.Fitness}";
        panel.SetMeta("player_name", player.Name);
        panel.SetMeta("role", slot.Role);

        var content = CreateMarkerContent();
        panel.AddChild(content.margin);
        content.nameLabel.Text = BuildShortPlayerName(player);
        content.roleLabel.Text = slot.Role;

        panel.GuiInput += @event =>
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                _selectedTacticPlayerName = player.Name;
                GameState.Instance?.SelectPlayerProfile(player.Name);
                _statusLabel.Text = $"Selected marker: {player.Name} | {player.Position} | Age {player.Age} | Form {player.Form} | Morale {player.Morale} | Fitness {player.Fitness}";
                RenderTacticalBoard(_formationOption.GetItemText(_formationOption.Selected));
            }
        };

        return panel;
    }

    private Control CreateEmptyTacticMarker(TacticSlot slot, int slotIndex)
    {
        var panel = CreateMarkerPanel(slot, slotIndex, false);
        panel.Name = $"TacticMarker_Empty_{slot.Role}_{slotIndex}";
        panel.TooltipText = $"Empty {slot.Role} slot";
        panel.SetMeta("empty_slot", true);

        var content = CreateMarkerContent();
        panel.AddChild(content.margin);
        content.nameLabel.Text = "EMPTY";
        content.roleLabel.Text = slot.Role;
        content.nameLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextQuiet);
        return panel;
    }

    private static PanelContainer CreateMarkerPanel(TacticSlot slot, int slotIndex, bool selected)
    {
        var panel = new PanelContainer
        {
            CustomMinimumSize = new Vector2(86, 52),
            MouseDefaultCursorShape = CursorShape.PointingHand,
            ZIndex = 10 + slotIndex
        };
        panel.AnchorLeft = slot.Position.X;
        panel.AnchorRight = slot.Position.X;
        panel.AnchorTop = slot.Position.Y;
        panel.AnchorBottom = slot.Position.Y;
        panel.OffsetLeft = -43;
        panel.OffsetRight = 43;
        panel.OffsetTop = -26;
        panel.OffsetBottom = 26;
        TouchlineTheme.ApplyTokenStyle(panel, selected);
        return panel;
    }

    private static (MarginContainer margin, Label nameLabel, Label roleLabel) CreateMarkerContent()
    {
        var margin = CreateMarginContainer(8, 5, 8, 5);
        var stack = new VBoxContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
            SizeFlagsVertical = SizeFlags.ExpandFill
        };
        stack.AddThemeConstantOverride("separation", 1);
        margin.AddChild(stack);

        var nameLabel = new Label
        {
            Name = "PlayerNameLabel",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        nameLabel.AddThemeFontSizeOverride("font_size", 13);
        nameLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        stack.AddChild(nameLabel);

        var roleLabel = new Label
        {
            Name = "RoleLabel",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        roleLabel.AddThemeFontSizeOverride("font_size", 10);
        roleLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextMuted);
        stack.AddChild(roleLabel);
        return (margin, nameLabel, roleLabel);
    }

    private static TacticSlot[] BuildTacticSlots(string formation)
    {
        return formation switch
        {
            "4-2-3-1" => new[]
            {
                new TacticSlot("GK", new Vector2(0.50f, 0.88f)),
                new TacticSlot("LB", new Vector2(0.18f, 0.72f)),
                new TacticSlot("CB", new Vector2(0.38f, 0.74f)),
                new TacticSlot("CB", new Vector2(0.62f, 0.74f)),
                new TacticSlot("RB", new Vector2(0.82f, 0.72f)),
                new TacticSlot("CM", new Vector2(0.42f, 0.58f)),
                new TacticSlot("CM", new Vector2(0.58f, 0.58f)),
                new TacticSlot("LW", new Vector2(0.24f, 0.38f)),
                new TacticSlot("AM", new Vector2(0.50f, 0.34f)),
                new TacticSlot("RW", new Vector2(0.76f, 0.38f)),
                new TacticSlot("ST", new Vector2(0.50f, 0.18f))
            },
            "3-5-2" => new[]
            {
                new TacticSlot("GK", new Vector2(0.50f, 0.88f)),
                new TacticSlot("CB", new Vector2(0.32f, 0.72f)),
                new TacticSlot("CB", new Vector2(0.50f, 0.74f)),
                new TacticSlot("CB", new Vector2(0.68f, 0.72f)),
                new TacticSlot("LWB", new Vector2(0.16f, 0.52f)),
                new TacticSlot("CM", new Vector2(0.36f, 0.54f)),
                new TacticSlot("CM", new Vector2(0.50f, 0.48f)),
                new TacticSlot("AM", new Vector2(0.64f, 0.54f)),
                new TacticSlot("RWB", new Vector2(0.84f, 0.52f)),
                new TacticSlot("ST", new Vector2(0.43f, 0.20f)),
                new TacticSlot("ST", new Vector2(0.57f, 0.20f))
            },
            _ => new[]
            {
                new TacticSlot("GK", new Vector2(0.50f, 0.88f)),
                new TacticSlot("LB", new Vector2(0.18f, 0.72f)),
                new TacticSlot("CB", new Vector2(0.38f, 0.74f)),
                new TacticSlot("CB", new Vector2(0.62f, 0.74f)),
                new TacticSlot("RB", new Vector2(0.82f, 0.72f)),
                new TacticSlot("CM", new Vector2(0.38f, 0.56f)),
                new TacticSlot("CM", new Vector2(0.62f, 0.56f)),
                new TacticSlot("AM", new Vector2(0.50f, 0.42f)),
                new TacticSlot("LW", new Vector2(0.24f, 0.24f)),
                new TacticSlot("ST", new Vector2(0.50f, 0.18f)),
                new TacticSlot("RW", new Vector2(0.76f, 0.24f))
            }
        };
    }

    private static int FindPlayerForSlot(GameState state, string role, HashSet<int> assigned)
    {
        var exact = FindStarterByPredicate(state, assigned, player => PositionFitsRole(player.Position, role));
        if (exact >= 0)
        {
            return exact;
        }

        var family = RoleFamily(role);
        var familyMatch = FindStarterByPredicate(state, assigned, player => PositionFamily(player.Position) == family);
        if (familyMatch >= 0)
        {
            return familyMatch;
        }

        return FindStarterByPredicate(state, assigned, _ => true);
    }

    private static int FindStarterByPredicate(GameState state, HashSet<int> assigned, Func<GameState.SquadPlayer, bool> predicate)
    {
        for (var index = 0; index < state.SquadPlayers.Length; index++)
        {
            var player = state.SquadPlayers[index];
            if (!player.IsStarting || assigned.Contains(index) || !predicate(player))
            {
                continue;
            }

            return index;
        }

        return -1;
    }

    private static bool PositionFitsRole(string position, string role)
    {
        if (position == role)
        {
            return true;
        }

        return role switch
        {
            "LWB" => position == "LB",
            "RWB" => position == "RB",
            "AM" => position is "AM" or "CM",
            _ => false
        };
    }

    private static string RoleFamily(string role)
    {
        return role switch
        {
            "GK" => "GK",
            "LB" or "CB" or "RB" or "LWB" or "RWB" => "DEF",
            "CM" or "AM" => "MID",
            _ => "FWD"
        };
    }

    private static string PositionFamily(string position)
    {
        return position switch
        {
            "GK" => "GK",
            "LB" or "CB" or "RB" => "DEF",
            "CM" or "AM" => "MID",
            _ => "FWD"
        };
    }

    private static string BuildShortPlayerName(GameState.SquadPlayer player)
    {
        var parts = player.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return player.Position;
        }

        var surname = parts[^1];
        if (surname.Length <= 8)
        {
            return surname;
        }

        return $"{char.ToUpperInvariant(parts[0][0])}. {surname[..Math.Min(6, surname.Length)]}";
    }

    private static string SanitizeNodeName(string text)
    {
        var chars = text.ToCharArray();
        for (var index = 0; index < chars.Length; index++)
        {
            if (!char.IsLetterOrDigit(chars[index]))
            {
                chars[index] = '_';
            }
        }

        return new string(chars);
    }

    private readonly struct TacticSlot
    {
        public TacticSlot(string role, Vector2 position)
        {
            Role = role;
            Position = position;
        }

        public string Role { get; }
        public Vector2 Position { get; }
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

    private void OnSavePressed()
    {
        if (GameState.Instance == null)
        {
            _statusLabel.Text = "GameState singleton is unavailable.";
            _saveHintLabel.Text = "Save unavailable.";
            SetReadinessChip("SAVE OFFLINE", false);
            WriteAuditState();
            return;
        }

        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var style = _styleOption.GetItemText(_styleOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        var status = GameState.Instance.TryApplyTacticsFromUser(formation, style, press, tempo, width, risk);
        _savedPlanLabel.Text = BuildSavedPlanSummary(GameState.Instance);
        RefreshBoard();
        _statusLabel.Text = status;
        if (GameState.Instance.CareerProfile.Role == ManagerRole.AssistantManager)
        {
            _saveHintLabel.Text = status;
            SetReadinessChip("SUGGESTED", true);
            WriteAuditState();
            return;
        }

        _saveHintLabel.Text = "Saved plan is now the matchday tactical setup.";
        SetReadinessChip("PLAN SAVED", true);
        WriteAuditState();
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
        _styleOption.Select(FindStyleIndex(GameState.Instance.TeamStyleName));
        _pressSpin.Value = GameState.Instance.PressIntensity;
        _tempoSpin.Value = GameState.Instance.Tempo;
        _widthSpin.Value = GameState.Instance.Width;
        _riskSpin.Value = GameState.Instance.Risk;
        RefreshBoard();
        _statusLabel.Text = "Preview reset to the currently saved tactical setup.";
        _saveHintLabel.Text = "Save is only needed after new tactical changes.";
        WriteAuditState();
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

    private void OnStyleSelected(long _index)
    {
        RefreshBoard();
        _statusLabel.Text = "Team style changed on the board. Save to lock the new tactical layer.";
    }

    private void SetControlsDisabled(bool disabled)
    {
        _formationOption.Disabled = disabled;
        _styleOption.Disabled = disabled;
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

    private void WriteAuditState()
    {
        AuditUiStateWriter.Write(
            nameof(TacticsScreen),
            _managerLabel.Text,
            TouchlineRailRoute.Tactics,
            _clubContextLabel.Text,
            _matchPlanLabel.Text,
            _controlSummaryLabel.Text,
            _savedPlanLabel.Text,
            _saveButton.Text,
            _saveHintLabel.Text,
            _statusLabel.Text,
            _formationBadgeLabel.Text,
            _previewSummaryLabel.Text);
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

    private int FindStyleIndex(string style)
    {
        for (var index = 0; index < _styleOption.ItemCount; index++)
        {
            if (_styleOption.GetItemText(index) == style)
            {
                return index;
            }
        }

        return 0;
    }

    private static string BuildSavedPlanSummary(GameState state)
    {
        return $"Saved tactical setup: {state.BuildTacticalPlanSummary()} | Shared match engine input: Formation {state.TacticalFormation} | Style {state.TeamStyleName} | Familiarity {state.TacticalFamiliarityName} | Pressing {state.PressIntensity} | Tempo {state.Tempo} | Width {state.Width} | Mentality {state.Risk}\n{state.TacticsFoundationSummary}";
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

    private static string BuildTacticalChipLabel(string formation, string style, int press, int tempo)
    {
        if (style == "High Press" || style == "Counterattack")
        {
            return $"{formation} {style.ToUpperInvariant()}";
        }

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

    private static string BuildPreviewSummary(string formation, string style, int press, int tempo, int width, int risk)
    {
        return $"Shared match engine preview: {formation}, {style}, pressing {press} ({DescribePress(press).ToLowerInvariant()}), tempo {tempo} ({DescribeTempo(tempo).ToLowerInvariant()}), width {width} ({DescribeWidth(width).ToLowerInvariant()}), mentality {risk} ({DescribeRisk(risk).ToLowerInvariant()}), set pieces {BuildPreviewSetPiece(style, risk)}, and opponent prep {BuildPreviewOpponentPrep(style, press, width, risk)}. Role fit and player familiarity refresh when the plan is saved.";
    }

    private static string BuildControlSummary(string formation, string style, int press, int tempo, int width, int risk)
    {
        return $"Preview values | Formation {formation} | Style {style} | Pressing {press} ({DescribePress(press)}) | Tempo {tempo} ({DescribeTempo(tempo)}) | Width {width} ({DescribeWidth(width)}) | Mentality {risk} ({DescribeRisk(risk)})";
    }

    private static string BuildPreviewSetPiece(string style, int risk)
    {
        if (style == "Possession")
        {
            return "short routines";
        }

        if (style == "Direct Play" || style == "Wide Attack")
        {
            return risk >= 60 ? "near-post attack" : "far-post attack";
        }

        if (style == "Low Block" || style == "Defensive Solidity")
        {
            return "defensive security";
        }

        return "balanced routines";
    }

    private static string BuildPreviewOpponentPrep(string style, int press, int width, int risk)
    {
        if (risk >= 72)
        {
            return "rest defense";
        }

        if (press >= 72)
        {
            return "press triggers";
        }

        if (width >= 68 || style == "Wide Attack")
        {
            return "wide containment";
        }

        if (style == "Central Overload")
        {
            return "central containment";
        }

        if (style == "Direct Play")
        {
            return "direct-defense brief";
        }

        return "balanced brief";
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
