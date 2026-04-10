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
        _clubContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ClubContextLabel");
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

        _focusCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/FocusCard");
        _fixturePreviewLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/FixturePreviewLabel");
        _focusContextLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/FocusContextLabel");
        _recommendedMoveLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/RecommendedMoveLabel");
        _actionHintLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/FocusCard/FocusPadding/FocusContent/ActionHintLabel");

        _momentumCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/MomentumCard");
        _formValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/FormValueLabel");
        _lastResultLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/LastResultLabel");
        _tableImpactLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/MomentumCard/MomentumPadding/MomentumContent/TableImpactLabel");

        _pressureCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/PressureCard");
        _pressureValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureValueLabel");
        _pressureReasonsLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/MainStack/LowerRow/PressureCard/PressurePadding/PressureContent/PressureReasonsLabel");

        _insightCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/InsightCard");
        _squadStatusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/SquadStatusLabel");
        _tacticsSummaryLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/TacticsSummaryLabel");
        _priorityLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/PriorityLabel");
        _statusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/StatusLabel");
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

        TouchlineTheme.ApplyButtonVariant(_dashboardButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_squadButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_tacticsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_fixturesButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_standingsButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_matchdayButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_saveButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        _dashboardButton.Disabled = true;

        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_managerLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seasonLabel, 15);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        TouchlineTheme.ApplyMutedStyle(_competitionChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_priorityChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_stateChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_headerStatusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_clubContextLabel, 18);
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
    }

    private void RenderState()
    {
        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            RenderUnavailableState("Career context unavailable.", "Start or load a career to open the club command view.");
            return;
        }

        if (string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState("Club selection missing.", "Choose a club before using the dashboard.");
            return;
        }

        var clubName = GameState.Instance.SelectedClubName!;
        var position = GetClubPosition(clubName);
        var tableSize = GameState.Instance.CompetitionTable.Length;
        var currentRow = GetCompetitionRow(clubName);
        var hasMatchReport = GameState.Instance.LastMatchReport != null;

        _clubBadgeLabel.Text = BuildClubMonogram(clubName);
        _clubNameLabel.Text = clubName;
        _managerLabel.Text = $"Manager {GameState.Instance.ManagerName}";
        _seasonLabel.Text = $"Season {GameState.Instance.SeasonLabel}";
        _competitionChipLabel.Text = GameState.Instance.CompetitionName.ToUpperInvariant();
        _clubContextLabel.Text = $"{clubName} club command | Manager {GameState.Instance.ManagerName}";
        _dateLabel.Text = $"{GameState.Instance.CurrentDateLabel} | Matchday {GameState.Instance.CurrentMatchday}";
        _priorityChipLabel.Text = BuildPriorityTag(GameState.Instance);
        SetStateChip(hasMatchReport ? "POST-MATCH" : "MATCH WEEK", hasMatchReport);
        _headerStatusLabel.Text = hasMatchReport
            ? $"Latest outcome: {GameState.Instance.LastMatchReport!.Scoreline}"
            : "No result logged yet. The opening stretch is still in front of the club.";

        _nextMatchValueLabel.Text = GameState.Instance.CurrentOpponentName;
        _nextMatchMetaLabel.Text = GameState.Instance.NextFixtureSummary;
        _tableValueLabel.Text = position > 0 ? $"{position}/{tableSize}" : "--";
        _tableMetaLabel.Text = currentRow == null
            ? "Table position unavailable."
            : $"{currentRow.Points} pts | GD {FormatSigned(currentRow.GoalDifference)}";
        _moraleValueLabel.Text = $"{GameState.Instance.TeamMorale}";
        _moraleMetaLabel.Text = $"Morale {DescribePulse(GameState.Instance.TeamMorale)}";
        _boardValueLabel.Text = $"{GameState.Instance.BoardConfidence}";
        _boardMetaLabel.Text = $"Fans {GameState.Instance.FanSentiment} | board pulse";
        _shapeValueLabel.Text = GameState.Instance.TacticalFormation;
        _shapeMetaLabel.Text = $"Press {GameState.Instance.PressIntensity} | Tempo {GameState.Instance.Tempo}";

        _fixturePreviewLabel.Text = GameState.Instance.NextFixtureSummary;
        _focusContextLabel.Text = $"{GameState.Instance.CompetitionName} | {BuildTableLine(position, tableSize, currentRow)}";
        _recommendedMoveLabel.Text = BuildPrioritySummary(GameState.Instance);
        _actionHintLabel.Text = "Primary action: launch matchday when squad and tactics feel set.";

        _formValueLabel.Text = BuildCompactForm(GameState.Instance.FormSummary);
        _lastResultLabel.Text = hasMatchReport
            ? $"{GameState.Instance.LastMatchReport!.ResultLabel} {GameState.Instance.LastMatchReport.ConsequenceSummary}"
            : "No completed result yet.";
        _tableImpactLabel.Text = hasMatchReport
            ? GameState.Instance.LastMatchReport!.TableImpactSummary
            : BuildTableLine(position, tableSize, currentRow);

        _pressureValueLabel.Text =
            $"Morale {GameState.Instance.TeamMorale} | Fans {GameState.Instance.FanSentiment} | Board {GameState.Instance.BoardConfidence}";
        _pressureReasonsLabel.Text = PerceptionSystem.BuildPressureReasonSummary(GameState.Instance);

        _squadStatusLabel.Text = GameState.Instance.SquadStatusSummary;
        _tacticsSummaryLabel.Text =
            $"{GameState.Instance.TacticalFormation} | Press {GameState.Instance.PressIntensity} | Tempo {GameState.Instance.Tempo} | Width {GameState.Instance.Width} | Risk {GameState.Instance.Risk}";
        _priorityLabel.Text = BuildPrioritySummary(GameState.Instance);
        _statusLabel.Text = hasMatchReport
            ? $"{GameState.Instance.LastMatchReport!.FixtureLabel}: {GameState.Instance.LastMatchReport.Scoreline}"
            : "Opening week ready. Review the squad, sharpen the board, and head into the first fixture.";
        _saveHintLabel.Text = SaveSystem.Instance == null
            ? "Save unavailable."
            : "Save the live career state before leaving the session.";
    }

    private void RenderUnavailableState(string title, string status)
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _managerLabel.Text = title;
        _seasonLabel.Text = "Season unavailable";
        _competitionChipLabel.Text = "NO COMPETITION";
        _clubContextLabel.Text = title;
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
        _focusContextLabel.Text = "Club command data unavailable.";
        _recommendedMoveLabel.Text = status;
        _actionHintLabel.Text = "Activate a career flow to unlock the dashboard.";
        _formValueLabel.Text = "--";
        _lastResultLabel.Text = "No result context.";
        _tableImpactLabel.Text = "No competition context.";
        _pressureValueLabel.Text = "Pressure unavailable.";
        _pressureReasonsLabel.Text = "Pressure reasons unavailable.";
        _squadStatusLabel.Text = "Squad status unavailable.";
        _tacticsSummaryLabel.Text = "Tactical summary unavailable.";
        _priorityLabel.Text = status;
        _statusLabel.Text = status;
        _saveHintLabel.Text = "Save unavailable.";
        _saveButton.Disabled = true;
        _matchdayButton.Disabled = true;
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
            return "Board pressure is tightening. Stabilize shape and protect the next result.";
        }

        if (state.TeamMorale < 60)
        {
            return "The dressing room needs help. Check the squad pulse before kickoff.";
        }

        if (state.LastMatchReport == null)
        {
            return "Opening week: settle the XI, confirm the plan, and take control of matchday.";
        }

        return "Keep the weekly rhythm moving. Track pressure, review the squad, then go again.";
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
            return;
        }

        SaveSystem.Instance.SaveGame(out var statusMessage);
        _statusLabel.Text = statusMessage;
        _saveHintLabel.Text = statusMessage;
        SetStateChip("CAREER SAVED", true);
    }
}
