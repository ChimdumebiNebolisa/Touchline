using Godot;

public partial class PostMatchScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private Label _continueHintLabel = default!;

    public override void _Ready()
    {
        var fixtureLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/FixtureLabel");
        var scoreLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ScoreLabel");
        var resultLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ResultLabel");
        var deltasLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/DeltasLabel");
        var tableImpactLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TableImpactLabel");
        var tacticalLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/TacticalLabel");
        var pressureLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ConsequencesCard/ConsequencesPadding/ConsequencesContent/PressureLabel");
        var eventsLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventsCard/EventsPadding/EventsContent/EventsLabel");
        _continueHintLabel = GetNode<Label>("Center/Shell/Padding/Content/ContinueHintLabel");

        if (GameState.Instance?.LastMatchReport == null)
        {
            fixtureLabel.Text = "Post-match context unavailable";
            scoreLabel.Text = "0 - 0";
            resultLabel.Text = "No completed result is ready to review.";
            deltasLabel.Text = "Morale +0 | Fans +0 | Board +0";
            tableImpactLabel.Text = "Table impact unavailable.";
            tacticalLabel.Text = "Tactical summary unavailable.";
            pressureLabel.Text = "Pressure summary unavailable.";
            eventsLabel.Text = "No key events recorded.";
            _continueHintLabel.Text = "Complete a match before reviewing the aftermath.";
            return;
        }

        var report = GameState.Instance.LastMatchReport;
        fixtureLabel.Text = report.FixtureLabel;
        scoreLabel.Text = report.Scoreline;
        resultLabel.Text = report.ResultLabel;
        deltasLabel.Text = report.ConsequenceSummary;
        tableImpactLabel.Text = report.TableImpactSummary;
        tacticalLabel.Text = report.TacticalSummary;
        pressureLabel.Text = report.PressureSummary;
        eventsLabel.Text = string.Join("\n", report.KeyEvents);
        _continueHintLabel.Text = "Continue to roll the calendar forward and carry these consequences back into the dashboard.";
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
}
