using Godot;

public partial class MatchdayScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string LiveMatchScenePath = "res://scenes/LiveMatchScene.tscn";

    private PanelContainer _headerCard = default!;
    private PanelContainer _matchStateChip = default!;
    private PanelContainer _contextCard = default!;
    private PanelContainer _planCard = default!;
    private PanelContainer _actionCard = default!;
    private Label _competitionLabel = default!;
    private Label _fixtureLabel = default!;
    private Label _matchStateChipLabel = default!;
    private Label _stakesLabel = default!;
    private Label _opponentValueLabel = default!;
    private Label _opponentMetaLabel = default!;
    private Label _formValueLabel = default!;
    private Label _formMetaLabel = default!;
    private Label _pulseValueLabel = default!;
    private Label _pulseMetaLabel = default!;
    private Label _readinessValueLabel = default!;
    private Label _readinessMetaLabel = default!;
    private Label _kickoffContextLabel = default!;
    private Label _lineupLabel = default!;
    private Label _benchLabel = default!;
    private Label _pressureReasonsLabel = default!;
    private Label _pressureLabel = default!;
    private Label _tacticsLabel = default!;
    private Label _opponentFocusLabel = default!;
    private Label _readinessLabel = default!;
    private Label _statusLabel = default!;
    private Button _backButton = default!;
    private Button _instantResultButton = default!;
    private Button _startMatchButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _headerCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeaderCard");
        _matchStateChip = GetNode<PanelContainer>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/MatchStateChip");
        _competitionLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/CompetitionLabel");
        _fixtureLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/FixtureLabel");
        _matchStateChipLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/MatchStateChip/MatchStateChipPadding/MatchStateChipLabel");
        _stakesLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderStatus/StakesLabel");

        _opponentValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/OpponentCard/CardPadding/CardContent/CardValueLabel");
        _opponentMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/OpponentCard/CardPadding/CardContent/CardMetaLabel");
        _formValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardValueLabel");
        _formMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardMetaLabel");
        _pulseValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PulseCard/CardPadding/CardContent/CardValueLabel");
        _pulseMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PulseCard/CardPadding/CardContent/CardMetaLabel");
        _readinessValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/ReadinessCard/CardPadding/CardContent/CardValueLabel");
        _readinessMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/ReadinessCard/CardPadding/CardContent/CardMetaLabel");

        _contextCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/ContextCard");
        _kickoffContextLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/KickoffContextLabel");
        _lineupLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/LineupLabel");
        _benchLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/BenchLabel");
        _pressureReasonsLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/PressureReasonsLabel");

        _planCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PlanCard");
        _pressureLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/PressureLabel");
        _tacticsLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/TacticsLabel");
        _opponentFocusLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/OpponentFocusLabel");
        _readinessLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/ReadinessLabel");

        _actionCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/ActionCard");
        _statusLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StatusLabel");
        _backButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/BackButton");
        _instantResultButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/InstantResultButton");
        _startMatchButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/StartMatchButton");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_matchStateChip, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/OpponentCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/FormCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/PulseCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/ReadinessCard"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(_contextCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_planCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(_actionCard, TouchlineSurfaceVariant.Rail, 24);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageTitleLabel"), 34);
        TouchlineTheme.ApplyMutedStyle(_competitionLabel, 17);
        TouchlineTheme.ApplyMutedStyle(_fixtureLabel, 16);
        TouchlineTheme.ApplyMutedStyle(_matchStateChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_stakesLabel, 15);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/OpponentCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PulseCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/ReadinessCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyValueStyle(_opponentValueLabel, 28);
        TouchlineTheme.ApplyPositiveValueStyle(_formValueLabel, 28);
        TouchlineTheme.ApplyValueStyle(_pulseValueLabel, 26);
        TouchlineTheme.ApplyAccentValueStyle(_readinessValueLabel, 26);
        TouchlineTheme.ApplyMutedStyle(_opponentMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_formMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pulseMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_readinessMetaLabel, 14);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ContextCard/ContextPadding/ContextContent/ContextEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PlanCard/PlanPadding/PlanContent/PlanEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ActionEyebrow"));
        TouchlineTheme.ApplyMutedStyle(_kickoffContextLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_lineupLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_benchLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_pressureReasonsLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pressureLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_tacticsLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_opponentFocusLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_readinessLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 15);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);
        TouchlineTheme.ApplyButtonVariant(_instantResultButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_startMatchButton, TouchlineButtonVariant.Primary);
        _backButton.Text = "Back to Manager Hub";
        _instantResultButton.Text = "Instant Result";
        _startMatchButton.Text = "Watch Live Match";
    }

    private void RenderState()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState();
            return;
        }

        var state = GameState.Instance;
        var startingCount = 0;
        var benchCount = 0;
        var totalFitness = 0;
        foreach (var player in state.SquadPlayers)
        {
            if (player.IsStarting)
            {
                startingCount++;
                totalFitness += player.Fitness;
            }
            else
            {
                benchCount++;
            }
        }

        var averageFitness = startingCount == 0 ? 0 : totalFitness / startingCount;
        var fixtureComplete = IsCurrentFixtureComplete();
        var registrationBlocked = !state.CanPlayCurrentFixture;
        var readinessValue = startingCount >= 11 ? "XI READY" : $"XI {startingCount}/11";

        _competitionLabel.Text = state.CurrentFixtureIsCup
            ? $"{state.CurrentFixtureCompetitionName} | {state.CurrentFixtureRoundName} | {state.CurrentDateLabel}"
            : $"Matchday {state.CurrentMatchday} | {state.CurrentDateLabel} | {state.CompetitionName}";
        _fixtureLabel.Text = $"{state.SelectedClubName} vs {state.CurrentOpponentName}";
        _stakesLabel.Text = BuildStakesLabel();
        SetMatchStateChip(fixtureComplete ? "RECORDED" : "READY", fixtureComplete);

        _opponentValueLabel.Text = state.CurrentOpponentName;
        _opponentMetaLabel.Text = state.BuildOpponentContextSummary();
        _formValueLabel.Text = BuildCompactForm(state.FormSummary);
        _formMetaLabel.Text = state.BuildRecentResultsSummary();
        _pulseValueLabel.Text = $"{state.TeamMorale}/{state.FanSentiment}/{state.BoardConfidence}";
        _pulseMetaLabel.Text = "Morale / Fans / Board";
        _readinessValueLabel.Text = readinessValue;
        _readinessMetaLabel.Text = state.CanPlayCurrentFixture
            ? $"Avg fitness {averageFitness} | Bench {benchCount}"
            : $"{state.RegistrationStatusSummary} | {state.RegistrationIssuesSummary}";

        _kickoffContextLabel.Text = $"Dressing Room Brief | {state.BuildCareerPhaseSummary()} Keep the message tight before kickoff.";
        _lineupLabel.Text = $"Team News | {state.BuildLineupReadinessSummary()}";
        _benchLabel.Text = $"Team News | Bench {benchCount} | Average XI fitness {averageFitness} | Missing slots {System.Math.Max(0, 11 - startingCount)}";
        _pressureReasonsLabel.Text = $"Club Mood | {PerceptionSystem.BuildPressureReasonSummary(state)}";

        _pressureLabel.Text = $"Club Mood | Morale {state.TeamMorale} | Fans {state.FanSentiment} | Board {state.BoardConfidence}\n{state.PressureCategorySummary}";
        _tacticsLabel.Text = $"Match Plan | {state.BuildTacticalPlanSummary()}";
        _opponentFocusLabel.Text = $"Opponent Brief | {state.BuildOpponentContextSummary()}";
        _readinessLabel.Text = state.CanPlayCurrentFixture
            ? "Selection call | The XI is complete and ready for kickoff."
            : $"Registration call | {state.RegistrationIssuesSummary}";

        _statusLabel.Text = fixtureComplete
            ? "Fixture complete: this match is already in the season record. Return to the Manager Hub and advance instead of replaying it."
            : registrationBlocked
                ? "Match Controls | Registration or matchday XI validation blocks kickoff until the issue is fixed."
                : "Match Controls | Watch Live Match uses the 2D replay; Instant Result resolves the same shared engine immediately.";
        _instantResultButton.Disabled = fixtureComplete || registrationBlocked;
        _startMatchButton.Disabled = fixtureComplete || registrationBlocked;
    }

    private void RenderUnavailableState()
    {
        _competitionLabel.Text = "Competition unavailable";
        _fixtureLabel.Text = "Fixture unavailable";
        _stakesLabel.Text = "Set up a career and club before entering matchday.";
        _opponentValueLabel.Text = "--";
        _opponentMetaLabel.Text = "Context unavailable.";
        _formValueLabel.Text = "--";
        _formMetaLabel.Text = "Recent run unavailable.";
        _pulseValueLabel.Text = "--";
        _pulseMetaLabel.Text = "Pulse context unavailable.";
        _readinessValueLabel.Text = "--";
        _readinessMetaLabel.Text = "Selection state unavailable.";
        _kickoffContextLabel.Text = "Dressing Room Brief unavailable.";
        _lineupLabel.Text = "Team News unavailable.";
        _benchLabel.Text = "Bench count unavailable.";
        _pressureReasonsLabel.Text = "Club Mood unavailable.";
        _pressureLabel.Text = "Club Mood unavailable.";
        _tacticsLabel.Text = "Match Plan unavailable.";
        _opponentFocusLabel.Text = "Opponent focus unavailable.";
        _readinessLabel.Text = "Readiness unavailable.";
        _statusLabel.Text = "Match Controls unavailable until a career and club are active.";
        SetMatchStateChip("OFFLINE", false);
        _instantResultButton.Disabled = true;
        _startMatchButton.Disabled = true;
    }

    private void SetMatchStateChip(string text, bool positive)
    {
        _matchStateChipLabel.Text = text;
        TouchlineTheme.ApplyPanelVariant(_matchStateChip, positive ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Muted, 999);
    }

    private void OnStartMatchPressed()
    {
        GameState.Instance?.PrepareCurrentMatchResult(true);
        GetTree().ChangeSceneToFile(LiveMatchScenePath);
    }

    private void OnInstantResultPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        GameState.Instance.PrepareCurrentMatchResult(true);
        GameState.Instance.ResolveCurrentMatchInstantly();
        GetTree().ChangeSceneToFile("res://scenes/PostMatchScene.tscn");
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private static string BuildCompactForm(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? formSummary["Form: ".Length..] : formSummary;
    }

    private static string BuildStakesLabel()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return "Match stakes unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.SelectedClubName)
            {
                return $"Table stake | Position {index + 1} | {row.Points} pts | GD {FormatSigned(row.GoalDifference)}";
            }
        }

        return "Match stakes unavailable.";
    }

    private static string BuildOpponentFocusLabel()
    {
        if (GameState.Instance == null)
        {
            return "Opponent focus unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.CurrentOpponentName)
            {
                return $"Opponent read | Position {index + 1} | {row.Points} pts | {row.GoalsFor} GF | {row.GoalsAgainst} GA";
            }
        }

        return $"{GameState.Instance.CurrentOpponentName} arrive with pressure, but their table line is unavailable.";
    }

    private static bool IsCurrentFixtureComplete()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return false;
        }

        foreach (var fixture in GameState.Instance.CompetitionFixtures)
        {
            if (fixture.Matchday == GameState.Instance.CurrentMatchday &&
                (fixture.HomeClubName == GameState.Instance.SelectedClubName || fixture.AwayClubName == GameState.Instance.SelectedClubName))
            {
                return fixture.IsComplete;
            }
        }

        return false;
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }
}
