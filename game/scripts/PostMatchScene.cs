using Godot;

public partial class PostMatchScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private PanelContainer _headerCard = default!;
    private PanelContainer _scoreCard = default!;
    private PanelContainer _consequencesCard = default!;
    private PanelContainer _eventsCard = default!;
    private PanelContainer _actionCard = default!;
    private Label _fixtureLabel = default!;
    private Label _resultLabel = default!;
    private Label _scoreLabel = default!;
    private Label _continueHintLabel = default!;
    private Label _deltaValueLabel = default!;
    private Label _deltaMetaLabel = default!;
    private Label _tableValueLabel = default!;
    private Label _tableMetaLabel = default!;
    private Label _pressureValueLabel = default!;
    private Label _pressureMetaLabel = default!;
    private Label _deltasLabel = default!;
    private Label _tableImpactLabel = default!;
    private Label _tacticalLabel = default!;
    private Label _pressureLabel = default!;
    private Label _eventsLabel = default!;
    private Label _nextStepLabel = default!;
    private Button _continueButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _headerCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeaderCard");
        _fixtureLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/FixtureLabel");
        _resultLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/ResultLabel");
        _scoreCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard");
        _scoreLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ScoreLabel");
        _continueHintLabel = GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ContinueHintLabel");

        _deltaValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/DeltaCard/CardPadding/CardContent/CardValueLabel");
        _deltaMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/DeltaCard/CardPadding/CardContent/CardMetaLabel");
        _tableValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardValueLabel");
        _tableMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardMetaLabel");
        _pressureValueLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PressureCard/CardPadding/CardContent/CardValueLabel");
        _pressureMetaLabel = GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PressureCard/CardPadding/CardContent/CardMetaLabel");

        _consequencesCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/ConsequencesCard");
        _deltasLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/DeltasLabel");
        _tableImpactLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TableImpactLabel");
        _tacticalLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TacticalLabel");
        _pressureLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/PressureLabel");

        _eventsCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/EventsCard");
        _eventsLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/EventsCard/EventsPadding/EventsContent/EventsLabel");

        _actionCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/ActionCard");
        _nextStepLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/NextStepLabel");
        _continueButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ContinueButton");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_scoreCard, TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/DeltaCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/TableCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/SummaryGrid/PressureCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(_consequencesCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_eventsCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(_actionCard, TouchlineSurfaceVariant.Rail, 24);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/HeaderInfo/PageTitleLabel"), 34);
        TouchlineTheme.ApplyMutedStyle(_fixtureLabel, 17);
        TouchlineTheme.ApplyMutedStyle(_resultLabel, 16);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeaderCard/HeaderPadding/HeaderContent/ScoreCard/ScorePadding/ScoreContent/ScoreEyebrow"));
        TouchlineTheme.ApplyValueStyle(_scoreLabel, 40);
        TouchlineTheme.ApplyMutedStyle(_continueHintLabel, 14);

        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/DeltaCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/TableCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SummaryGrid/PressureCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyPositiveValueStyle(_deltaValueLabel, 28);
        TouchlineTheme.ApplyAccentValueStyle(_tableValueLabel, 28);
        TouchlineTheme.ApplyValueStyle(_pressureValueLabel, 26);
        TouchlineTheme.ApplyMutedStyle(_deltaMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_tableMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_pressureMetaLabel, 14);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/ConsequencesEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/EventsCard/EventsPadding/EventsContent/EventsEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ActionCard/ActionPadding/ActionContent/ActionEyebrow"));
        TouchlineTheme.ApplyMutedStyle(_deltasLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_tableImpactLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_tacticalLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_pressureLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_eventsLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_nextStepLabel, 15);
        TouchlineTheme.ApplyButtonVariant(_continueButton, TouchlineButtonVariant.Primary);
    }

    private void RenderState()
    {
        if (GameState.Instance?.LastMatchReport == null)
        {
            RenderUnavailableState();
            return;
        }

        var report = GameState.Instance.LastMatchReport;
        _fixtureLabel.Text = report.FixtureLabel;
        _resultLabel.Text = report.ResultLabel;
        _scoreLabel.Text = report.Scoreline;
        _continueHintLabel.Text = "Result recorded. Advance to roll the club week forward.";

        _deltaValueLabel.Text = BuildDeltaScore(report.ConsequenceSummary);
        _deltaMetaLabel.Text = report.ConsequenceSummary;
        _tableValueLabel.Text = BuildTableImpactHeadline(report.TableImpactSummary);
        _tableMetaLabel.Text = report.TableImpactSummary;
        _pressureValueLabel.Text = BuildPressureHeadline(report.PressureSummary);
        _pressureMetaLabel.Text = report.PressureSummary;

        _deltasLabel.Text = report.ConsequenceSummary;
        _tableImpactLabel.Text = report.TableImpactSummary;
        _tacticalLabel.Text = report.TacticalSummary;
        _pressureLabel.Text = report.PressureSummary;
        _eventsLabel.Text = string.Join("\n", report.KeyEvents);
        _nextStepLabel.Text = "Advance to Club Dashboard and carry this result into the next planning cycle.";
    }

    private void RenderUnavailableState()
    {
        _fixtureLabel.Text = "Post-match context unavailable";
        _resultLabel.Text = "No completed result is ready to review.";
        _scoreLabel.Text = "0 - 0";
        _continueHintLabel.Text = "Complete a match before reviewing the aftermath.";
        _deltaValueLabel.Text = "--";
        _deltaMetaLabel.Text = "Consequence summary unavailable.";
        _tableValueLabel.Text = "--";
        _tableMetaLabel.Text = "Table summary unavailable.";
        _pressureValueLabel.Text = "--";
        _pressureMetaLabel.Text = "Pressure summary unavailable.";
        _deltasLabel.Text = "Morale +0 | Fans +0 | Board +0";
        _tableImpactLabel.Text = "Table impact unavailable.";
        _tacticalLabel.Text = "Tactical summary unavailable.";
        _pressureLabel.Text = "Pressure summary unavailable.";
        _eventsLabel.Text = "No key events recorded.";
        _nextStepLabel.Text = "Advance to Club Dashboard";
    }

    private void OnContinuePressed()
    {
        if (TouchlineCalendarSystem.Instance == null)
        {
            _continueHintLabel.Text = "CalendarSystem singleton is unavailable.";
            return;
        }

        if (!TouchlineCalendarSystem.Instance.AdvanceCareerDate())
        {
            _continueHintLabel.Text = TouchlineCalendarSystem.Instance.LastStatusMessage;
            return;
        }

        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private static string BuildDeltaScore(string consequenceSummary)
    {
        if (string.IsNullOrWhiteSpace(consequenceSummary))
        {
            return "--";
        }

        return consequenceSummary.Replace("Morale ", "M ").Replace("Fans ", "F ").Replace("Board ", "B ");
    }

    private static string BuildTableImpactHeadline(string tableImpactSummary)
    {
        if (string.IsNullOrWhiteSpace(tableImpactSummary))
        {
            return "--";
        }

        var pipeIndex = tableImpactSummary.IndexOf('|');
        return pipeIndex > 0 ? tableImpactSummary[..pipeIndex].Trim() : tableImpactSummary;
    }

    private static string BuildPressureHeadline(string pressureSummary)
    {
        if (string.IsNullOrWhiteSpace(pressureSummary))
        {
            return "--";
        }

        var pipeIndex = pressureSummary.IndexOf('|');
        return pipeIndex > 0 ? pressureSummary[..pipeIndex].Trim() : pressureSummary;
    }
}
