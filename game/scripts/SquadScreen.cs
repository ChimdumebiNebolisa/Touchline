using Godot;
using System;
using System.Collections.Generic;

public partial class SquadScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string PlayerProfileScenePath = "res://scenes/PlayerProfile.tscn";
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
    private Label _lineupSummaryLabel = default!;
    private Label _filterChipLabel = default!;
    private Label _readinessChipLabel = default!;
    private Label _headerStatusLabel = default!;
    private Label _startersValueLabel = default!;
    private Label _startersMetaLabel = default!;
    private Label _benchValueLabel = default!;
    private Label _benchMetaLabel = default!;
    private Label _moraleValueLabel = default!;
    private Label _moraleMetaLabel = default!;
    private Label _fitnessValueLabel = default!;
    private Label _fitnessMetaLabel = default!;
    private Label _nextMatchValueLabel = default!;
    private Label _nextMatchMetaLabel = default!;
    private OptionButton _positionFilter = default!;
    private VBoxContainer _playerRows = default!;
    private Label _playerNameLabel = default!;
    private Label _detailMetaLabel = default!;
    private Label _roleChipLabel = default!;
    private Label _statusChipLabel = default!;
    private Label _formStatLabel = default!;
    private Label _moraleStatLabel = default!;
    private Label _fitnessStatLabel = default!;
    private Label _readinessSummaryLabel = default!;
    private Label _profileHintLabel = default!;
    private Label _lineupStatusLabel = default!;
    private Label _squadStatusLabel = default!;
    private Label _filterStatusLabel = default!;
    private Label _actionHintLabel = default!;
    private Label _railHintLabel = default!;
    private Button _lineupActionButton = default!;
    private Button _openProfileButton = default!;

    private PanelContainer _railCard = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _competitionChip = default!;
    private PanelContainer _filterChip = default!;
    private PanelContainer _readinessChip = default!;
    private PanelContainer _selectionCard = default!;
    private PanelContainer _detailCard = default!;
    private PanelContainer _actionCard = default!;
    private PanelContainer _roleChip = default!;
    private PanelContainer _statusChip = default!;

    private Button _dashboardButton = default!;
    private Button _squadButton = default!;
    private Button _tacticsButton = default!;
    private Button _fixturesButton = default!;
    private Button _standingsButton = default!;
    private Button _matchdayButton = default!;
    private Button _backButton = default!;

    private readonly List<int> _visiblePlayerIndexes = new();
    private int _currentVisibleSelectionIndex = -1;
    private string? _selectedPlayerName;

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
        _clubContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel");
        _lineupSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/LineupSummaryLabel");
        _filterChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/FilterChip");
        _filterChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/FilterChip/FilterChipPadding/FilterChipLabel");
        _readinessChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/ReadinessChip");
        _readinessChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/ChipRow/ReadinessChip/ReadinessChipPadding/ReadinessChipLabel");
        _headerStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/HeaderStatusLabel");

        _startersValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardValueLabel");
        _startersMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardMetaLabel");
        _benchValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/BenchCard/CardPadding/CardContent/CardValueLabel");
        _benchMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/BenchCard/CardPadding/CardContent/CardMetaLabel");
        _moraleValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard/CardPadding/CardContent/CardValueLabel");
        _moraleMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard/CardPadding/CardContent/CardMetaLabel");
        _fitnessValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardValueLabel");
        _fitnessMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardMetaLabel");
        _nextMatchValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardValueLabel");
        _nextMatchMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardMetaLabel");

        _selectionCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard");
        _positionFilter = GetNode<OptionButton>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/FilterRow/PositionFilter");
        _playerRows = GetNode<VBoxContainer>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/PlayerScroll/PlayerRows");

        _detailCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/DetailCard");
        _playerNameLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/PlayerNameLabel");
        _detailMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/DetailMetaLabel");
        _roleChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/RoleChip");
        _roleChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/RoleChip/RoleChipPadding/RoleChipLabel");
        _statusChip = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/StatusChip");
        _statusChipLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ChipRow/StatusChip/StatusChipPadding/StatusChipLabel");
        _formStatLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/FormStatLabel");
        _moraleStatLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/MoraleStatLabel");
        _fitnessStatLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard/StatsPadding/StatsContent/FitnessStatLabel");
        _readinessSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ReadinessSummaryLabel");
        _profileHintLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/ProfileHintLabel");

        _actionCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/ActionCard");
        _lineupStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/LineupStatusLabel");
        _squadStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/SquadStatusLabel");
        _filterStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/FilterStatusLabel");
        _actionHintLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ActionHintLabel");
        _lineupActionButton = GetNode<Button>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/LineupActionButton");
        _openProfileButton = GetNode<Button>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/OpenProfileButton");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_railCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_competitionChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_filterChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_readinessChip, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(_selectionCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_detailCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_actionCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(_roleChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_statusChip, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard"), TouchlineSurfaceVariant.Shell, 22);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/RailCard/RailPadding/RailContent/IdentityCard/IdentityPadding/IdentityContent/IdentityTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/BenchCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/StatsCard"), TouchlineSurfaceVariant.Muted, 20);

        TouchlineTheme.ApplyButtonVariant(_dashboardButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_squadButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_tacticsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_fixturesButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_standingsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_matchdayButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_lineupActionButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_openProfileButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        _squadButton.Disabled = true;

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_clubContextLabel, 18);
        TouchlineTheme.ApplyMutedStyle(_lineupSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_filterChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_readinessChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyPositiveValueStyle(_startersValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_benchValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_moraleValueLabel, 30);
        TouchlineTheme.ApplyAccentValueStyle(_fitnessValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_nextMatchValueLabel, 24);
        TouchlineTheme.ApplyMutedStyle(_startersMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_benchMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_moraleMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_fitnessMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_nextMatchMetaLabel, 14);
        TouchlineTheme.ApplyTitleStyle(_playerNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_detailMetaLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_roleChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_statusChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_formStatLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_moraleStatLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_fitnessStatLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_readinessSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_profileHintLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_lineupStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_squadStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_filterStatusLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_actionHintLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_railHintLabel, 14);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/RailCard/RailPadding/RailContent/SectionLabel"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageTitleLabel"), 36);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/StartersCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/BenchCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/MoraleCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/NextMatchCard/CardPadding/CardContent/CardEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/SelectionHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/SelectionCard/SelectionPadding/SelectionContent/FilterRow/FilterLabel"), 14);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/DetailCard/DetailPadding/DetailContent/DetailHeading"), 20);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ActionHeading"), 18);
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
        var starters = CountStarters(state);
        var bench = state.SquadPlayers.Length - starters;
        var averageMorale = CalculateAverage(state, player => player.Morale);
        var averageFitness = CalculateAverage(state, player => player.Fitness);

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"Manager {state.ManagerName}";
        _seasonLabel.Text = $"Season {state.SeasonLabel}";
        _competitionChipLabel.Text = state.CompetitionName.ToUpperInvariant();
        _clubContextLabel.Text = $"{clubName} squad control";
        _lineupSummaryLabel.Text = $"Next assignment: {state.CurrentOpponentName} | Matchday {state.CurrentMatchday}";
        _headerStatusLabel.Text = "Use the roster list to scan readiness quickly, then move the selected player into or out of the XI.";

        _startersValueLabel.Text = starters.ToString();
        _startersMetaLabel.Text = "Players in the XI";
        _benchValueLabel.Text = bench.ToString();
        _benchMetaLabel.Text = "Bench options";
        _moraleValueLabel.Text = averageMorale.ToString();
        _moraleMetaLabel.Text = $"Average morale {DescribePulse(averageMorale)}";
        _fitnessValueLabel.Text = averageFitness.ToString();
        _fitnessMetaLabel.Text = $"Average fitness {DescribeReadiness(averageFitness)}";
        _nextMatchValueLabel.Text = state.CurrentOpponentName;
        _nextMatchMetaLabel.Text = state.NextFixtureSummary;
        _squadStatusLabel.Text = state.SquadStatusSummary;
        _actionHintLabel.Text = "Primary action: settle the XI, then open player context only when you need the deeper profile.";
        _railHintLabel.Text = "Review the roster, adjust the XI, then move into tactics or launch the next matchday.";

        _matchdayButton.Disabled = false;
        PopulatePlayerRows((int)_positionFilter.GetSelectedId(), _selectedPlayerName);
    }

    private void RenderUnavailableState()
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = "Manager unavailable";
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _clubContextLabel.Text = "Squad control unavailable.";
        _lineupSummaryLabel.Text = "Lineup summary unavailable.";
        _filterChipLabel.Text = "NO FILTER";
        _readinessChipLabel.Text = "OFFLINE";
        _headerStatusLabel.Text = "Load a career to open the squad workspace.";
        _startersValueLabel.Text = "--";
        _startersMetaLabel.Text = "No XI loaded.";
        _benchValueLabel.Text = "--";
        _benchMetaLabel.Text = "No bench loaded.";
        _moraleValueLabel.Text = "--";
        _moraleMetaLabel.Text = "Morale unavailable.";
        _fitnessValueLabel.Text = "--";
        _fitnessMetaLabel.Text = "Fitness unavailable.";
        _nextMatchValueLabel.Text = "--";
        _nextMatchMetaLabel.Text = "Fixture unavailable.";
        _playerNameLabel.Text = "Select a player";
        _detailMetaLabel.Text = "Player detail unavailable.";
        _roleChipLabel.Text = "NO ROLE";
        _statusChipLabel.Text = "NO STATUS";
        _formStatLabel.Text = "Form unavailable.";
        _moraleStatLabel.Text = "Morale unavailable.";
        _fitnessStatLabel.Text = "Fitness unavailable.";
        _readinessSummaryLabel.Text = "Readiness summary unavailable.";
        _profileHintLabel.Text = "Player profiles unlock once a club is active.";
        _lineupStatusLabel.Text = "Lineup changes unavailable.";
        _squadStatusLabel.Text = "Squad status unavailable.";
        _filterStatusLabel.Text = "Filter unavailable.";
        _actionHintLabel.Text = "Open a live career before making selection changes.";
        _railHintLabel.Text = "Return to the dashboard once a club is active.";
        _lineupActionButton.Disabled = true;
        _openProfileButton.Disabled = true;
        _matchdayButton.Disabled = true;
        ClearContainer(_playerRows);
        TouchlineTheme.ApplyPanelVariant(_readinessChip, TouchlineSurfaceVariant.Muted, 999);
        TouchlineTheme.ApplyPanelVariant(_roleChip, TouchlineSurfaceVariant.Muted, 999);
        TouchlineTheme.ApplyPanelVariant(_statusChip, TouchlineSurfaceVariant.Muted, 999);
    }

    private void OnFilterSelected(long index)
    {
        var selectedId = _positionFilter.GetItemId((int)index);
        PopulatePlayerRows(selectedId, null);
    }

    private void PopulatePlayerRows(int filterId, string? preferredPlayerName)
    {
        if (GameState.Instance == null)
        {
            return;
        }

        ClearContainer(_playerRows);
        _visiblePlayerIndexes.Clear();
        _currentVisibleSelectionIndex = -1;
        _selectedPlayerName = null;

        _filterChipLabel.Text = BuildFilterLabel(filterId);
        _filterStatusLabel.Text = $"Filter | {BuildFilterSummary(filterId)}";

        for (var index = 0; index < GameState.Instance.SquadPlayers.Length; index++)
        {
            var player = GameState.Instance.SquadPlayers[index];
            if (!MatchesFilter(player.Position, filterId))
            {
                continue;
            }

            _visiblePlayerIndexes.Add(index);
        }

        if (_visiblePlayerIndexes.Count == 0)
        {
            SetReadinessChip("FILTER EMPTY", false);
            _playerNameLabel.Text = "No players found";
            _detailMetaLabel.Text = "Adjust the filter to return to the active roster.";
            _lineupStatusLabel.Text = "No lineup action is available for the current filter.";
            _lineupActionButton.Disabled = true;
            _openProfileButton.Disabled = true;
            return;
        }

        var selectedVisibleIndex = 0;
        if (!string.IsNullOrWhiteSpace(preferredPlayerName))
        {
            for (var visibleIndex = 0; visibleIndex < _visiblePlayerIndexes.Count; visibleIndex++)
            {
                if (GameState.Instance.SquadPlayers[_visiblePlayerIndexes[visibleIndex]].Name == preferredPlayerName)
                {
                    selectedVisibleIndex = visibleIndex;
                    break;
                }
            }
        }

        _currentVisibleSelectionIndex = selectedVisibleIndex;
        _selectedPlayerName = GameState.Instance.SquadPlayers[_visiblePlayerIndexes[selectedVisibleIndex]].Name;

        for (var visibleIndex = 0; visibleIndex < _visiblePlayerIndexes.Count; visibleIndex++)
        {
            var player = GameState.Instance.SquadPlayers[_visiblePlayerIndexes[visibleIndex]];
            _playerRows.AddChild(CreatePlayerRow(visibleIndex, player, visibleIndex == selectedVisibleIndex));
        }

        RenderPlayerDetailByVisibleIndex(selectedVisibleIndex);
    }

    private Control CreatePlayerRow(int visibleIndex, GameState.SquadPlayer player, bool selected)
    {
        var rowPanel = new PanelContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyPanelVariant(
            rowPanel,
            selected
                ? TouchlineSurfaceVariant.Accent
                : player.IsStarting ? TouchlineSurfaceVariant.Card : TouchlineSurfaceVariant.Muted,
            16);
        rowPanel.MouseDefaultCursorShape = CursorShape.PointingHand;

        var padding = CreateMarginContainer(16, 12, 16, 12);
        padding.Name = "RowPadding";
        rowPanel.AddChild(padding);

        var rowContent = new HBoxContainer
        {
            Name = "RowContent",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        rowContent.AddThemeConstantOverride("separation", 14);
        padding.AddChild(rowContent);

        rowContent.AddChild(CreateStateChip(player.IsStarting ? "XI" : "BENCH", player.IsStarting));

        var body = new VBoxContainer
        {
            Name = "Body",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        body.AddThemeConstantOverride("separation", 4);
        rowContent.AddChild(body);

        var nameLabel = new Label
        {
            Name = "NameLabel",
            Text = player.Name,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        nameLabel.AddThemeFontSizeOverride("font_size", 16);
        nameLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        body.AddChild(nameLabel);

        var metaLabel = new Label
        {
            Name = "MetaLabel",
            Text = $"{player.Position} | Age {player.Age} | {BuildPlayerStatusLine(player)}",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        metaLabel.AddThemeFontSizeOverride("font_size", 14);
        metaLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextMuted);
        body.AddChild(metaLabel);

        var rightColumn = new VBoxContainer();
        rightColumn.AddThemeConstantOverride("separation", 4);
        rowContent.AddChild(rightColumn);

        var readinessLabel = new Label
        {
            Text = BuildReadinessLabel(player),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        readinessLabel.AddThemeFontSizeOverride("font_size", 14);
        readinessLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        rightColumn.AddChild(readinessLabel);

        var conditionLabel = new Label
        {
            Text = $"Fit {player.Fitness} | Mor {player.Morale} | Form {player.Form}",
            HorizontalAlignment = HorizontalAlignment.Right
        };
        conditionLabel.AddThemeFontSizeOverride("font_size", 13);
        conditionLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextQuiet);
        rightColumn.AddChild(conditionLabel);

        rowPanel.GuiInput += @event =>
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                PopulatePlayerRows((int)_positionFilter.GetSelectedId(), player.Name);
            }
        };

        return rowPanel;
    }

    private static PanelContainer CreateStateChip(string text, bool positive)
    {
        var panel = new PanelContainer();
        TouchlineTheme.ApplyPanelVariant(panel, positive ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Muted, 999);

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

    private void RenderPlayerDetailByVisibleIndex(int visibleIndex)
    {
        if (GameState.Instance == null || visibleIndex < 0 || visibleIndex >= _visiblePlayerIndexes.Count)
        {
            _playerNameLabel.Text = "Select a player";
            _detailMetaLabel.Text = "Player details unavailable.";
            _lineupActionButton.Disabled = true;
            _openProfileButton.Disabled = true;
            return;
        }

        _currentVisibleSelectionIndex = visibleIndex;
        var index = _visiblePlayerIndexes[visibleIndex];
        var player = GameState.Instance.SquadPlayers[index];
        _selectedPlayerName = player.Name;

        _playerNameLabel.Text = player.Name;
        _detailMetaLabel.Text = $"{player.Position} | Age {player.Age} | {BuildPlayerStatusLine(player)}";
        _roleChipLabel.Text = player.IsStarting ? "STARTING XI" : "BENCH UNIT";
        _statusChipLabel.Text = BuildReadinessLabel(player).ToUpperInvariant();
        TouchlineTheme.ApplyPanelVariant(_roleChip, player.IsStarting ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_statusChip, ResolveReadinessVariant(player), 999);

        _formStatLabel.Text = $"Form | {player.Form} | {DescribeForm(player.Form)}";
        _moraleStatLabel.Text = $"Morale | {player.Morale} | {DescribePulse(player.Morale)}";
        _fitnessStatLabel.Text = $"Fitness | {player.Fitness} | {DescribeReadiness(player.Fitness)}";
        _readinessSummaryLabel.Text = BuildReadinessSummary(player);
        _profileHintLabel.Text = "Open the player profile for the longer arc. Keep squad work here focused on match readiness and role.";
        _lineupStatusLabel.Text = player.IsStarting
            ? $"{player.Name} currently holds a starting role. Use the primary action only if you want to rotate the XI."
            : $"{player.Name} is currently outside the XI. Promote only if the readiness level fits the next fixture.";
        _lineupActionButton.Disabled = false;
        _lineupActionButton.Text = player.IsStarting ? $"Move {player.Name} to Bench" : $"Promote {player.Name} to XI";
        _openProfileButton.Disabled = false;
        _openProfileButton.Text = $"Open {player.Name} Profile";
        SetReadinessChip(BuildSquadReadinessChip(player), true);
    }

    private void OnLineupActionPressed()
    {
        if (GameState.Instance == null || _currentVisibleSelectionIndex < 0 || _currentVisibleSelectionIndex >= _visiblePlayerIndexes.Count)
        {
            return;
        }

        var selectedIndex = _visiblePlayerIndexes[_currentVisibleSelectionIndex];
        var selectedPlayerName = GameState.Instance.SquadPlayers[selectedIndex].Name;
        _lineupStatusLabel.Text = GameState.Instance.TogglePlayerLineupStatus(selectedPlayerName);
        RenderState();
        PopulatePlayerRows((int)_positionFilter.GetSelectedId(), selectedPlayerName);
    }

    private void OnOpenProfilePressed()
    {
        if (GameState.Instance == null || _currentVisibleSelectionIndex < 0 || _currentVisibleSelectionIndex >= _visiblePlayerIndexes.Count)
        {
            return;
        }

        var index = _visiblePlayerIndexes[_currentVisibleSelectionIndex];
        GameState.Instance.SelectPlayerProfile(GameState.Instance.SquadPlayers[index].Name);
        GetTree().ChangeSceneToFile(PlayerProfileScenePath);
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

    private static bool MatchesFilter(string position, int filterId)
    {
        return filterId switch
        {
            0 => true,
            1 => position == "GK",
            2 => position is "RB" or "CB" or "LB",
            3 => position is "CM" or "AM",
            4 => position is "RW" or "LW" or "ST",
            _ => true
        };
    }

    private void SetReadinessChip(string text, bool positive)
    {
        _readinessChipLabel.Text = text;
        TouchlineTheme.ApplyPanelVariant(_readinessChip, positive ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Muted, 999);
    }

    private static string BuildFilterLabel(int filterId)
    {
        return filterId switch
        {
            1 => "GOALKEEPERS",
            2 => "DEFENDERS",
            3 => "MIDFIELD",
            4 => "FORWARDS",
            _ => "FULL SQUAD"
        };
    }

    private static string BuildFilterSummary(int filterId)
    {
        return filterId switch
        {
            1 => "Goalkeepers only",
            2 => "Defensive unit",
            3 => "Midfield unit",
            4 => "Attacking unit",
            _ => "Whole roster"
        };
    }

    private static string BuildPlayerStatusLine(GameState.SquadPlayer player)
    {
        return player.IsStarting ? "Current XI role" : "Rotation option";
    }

    private static string BuildReadinessLabel(GameState.SquadPlayer player)
    {
        var combined = (player.Fitness + player.Morale + player.Form) / 3;
        return combined switch
        {
            >= 78 => "Ready",
            >= 62 => "Stable",
            >= 48 => "Manage",
            _ => "Watch"
        };
    }

    private static TouchlineSurfaceVariant ResolveReadinessVariant(GameState.SquadPlayer player)
    {
        var combined = (player.Fitness + player.Morale + player.Form) / 3;
        return combined switch
        {
            >= 78 => TouchlineSurfaceVariant.Positive,
            >= 62 => TouchlineSurfaceVariant.Accent,
            _ => TouchlineSurfaceVariant.Muted
        };
    }

    private static string BuildSquadReadinessChip(GameState.SquadPlayer player)
    {
        if (player.IsStarting && player.Fitness >= 70 && player.Morale >= 65)
        {
            return "XI READY";
        }

        if (!player.IsStarting && player.Fitness >= 70)
        {
            return "BENCH READY";
        }

        return "NEEDS REVIEW";
    }

    private static string BuildReadinessSummary(GameState.SquadPlayer player)
    {
        return $"{player.Name} reads as {BuildReadinessLabel(player).ToLowerInvariant()} for the next fixture: form {player.Form}, morale {player.Morale}, fitness {player.Fitness}.";
    }

    private static int CountStarters(GameState state)
    {
        var starters = 0;
        foreach (var player in state.SquadPlayers)
        {
            if (player.IsStarting)
            {
                starters++;
            }
        }

        return starters;
    }

    private static int CalculateAverage(GameState state, Func<GameState.SquadPlayer, int> selector)
    {
        if (state.SquadPlayers.Length == 0)
        {
            return 0;
        }

        var total = 0;
        foreach (var player in state.SquadPlayers)
        {
            total += selector(player);
        }

        return total / state.SquadPlayers.Length;
    }

    private static string DescribePulse(int value)
    {
        return value switch
        {
            >= 75 => "surging",
            >= 60 => "steady",
            >= 45 => "uneasy",
            _ => "under strain"
        };
    }

    private static string DescribeReadiness(int value)
    {
        return value switch
        {
            >= 80 => "sharp",
            >= 65 => "playable",
            >= 50 => "managed",
            _ => "fragile"
        };
    }

    private static string DescribeForm(int value)
    {
        return value switch
        {
            >= 80 => "in rhythm",
            >= 65 => "steady",
            >= 50 => "mixed",
            _ => "cold"
        };
    }

    private static string BuildClubMonogram(string clubName)
    {
        var words = clubName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void OnDashboardPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
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
}
