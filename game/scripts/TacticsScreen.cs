using Godot;

public partial class TacticsScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _clubContextLabel = default!;
    private Label _matchPlanLabel = default!;
    private OptionButton _formationOption = default!;
    private SpinBox _pressSpin = default!;
    private SpinBox _tempoSpin = default!;
    private SpinBox _widthSpin = default!;
    private SpinBox _riskSpin = default!;
    private Label _formationBadgeLabel = default!;
    private Label _formationBoardLabel = default!;
    private Label _shapeSummaryLabel = default!;
    private Label _pressPreviewLabel = default!;
    private Label _tempoPreviewLabel = default!;
    private Label _widthPreviewLabel = default!;
    private Label _riskPreviewLabel = default!;
    private Label _previewSummaryLabel = default!;
    private Button _saveButton = default!;
    private Label _statusLabel = default!;

    public override void _Ready()
    {
        _clubContextLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/ClubContextLabel");
        _matchPlanLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/MatchPlanLabel");
        _formationOption = GetNode<OptionButton>("Center/Shell/Padding/Content/BoardRow/ControlsCard/ControlsPadding/ControlsContent/FormationOption");
        _pressSpin = GetNode<SpinBox>("Center/Shell/Padding/Content/BoardRow/ControlsCard/ControlsPadding/ControlsContent/PressSpin");
        _tempoSpin = GetNode<SpinBox>("Center/Shell/Padding/Content/BoardRow/ControlsCard/ControlsPadding/ControlsContent/TempoSpin");
        _widthSpin = GetNode<SpinBox>("Center/Shell/Padding/Content/BoardRow/ControlsCard/ControlsPadding/ControlsContent/WidthSpin");
        _riskSpin = GetNode<SpinBox>("Center/Shell/Padding/Content/BoardRow/ControlsCard/ControlsPadding/ControlsContent/RiskSpin");
        _formationBadgeLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/FormationBadgeLabel");
        _formationBoardLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/FormationBoardLabel");
        _shapeSummaryLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/ShapeSummaryLabel");
        _pressPreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/PressPreviewLabel");
        _tempoPreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/TempoPreviewLabel");
        _widthPreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/WidthPreviewLabel");
        _riskPreviewLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/RiskPreviewLabel");
        _previewSummaryLabel = GetNode<Label>("Center/Shell/Padding/Content/BoardRow/PreviewCard/PreviewPadding/PreviewContent/PreviewSummaryLabel");
        _saveButton = GetNode<Button>("Center/Shell/Padding/Content/ActionsRow/SaveButton");
        _statusLabel = GetNode<Label>("Center/Shell/Padding/Content/StatusLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _clubContextLabel.Text = "Club context unavailable.";
            _matchPlanLabel.Text = "Match plan unavailable.";
            _formationBadgeLabel.Text = "No tactical shape loaded";
            _formationBoardLabel.Text = "Set up a career and club before editing the tactical board.";
            _shapeSummaryLabel.Text = "Shape note unavailable.";
            _pressPreviewLabel.Text = "Press note unavailable.";
            _tempoPreviewLabel.Text = "Tempo note unavailable.";
            _widthPreviewLabel.Text = "Width note unavailable.";
            _riskPreviewLabel.Text = "Risk note unavailable.";
            _previewSummaryLabel.Text = "Tactical summary unavailable.";
            SetControlsDisabled(true);
            _saveButton.Disabled = true;
            _statusLabel.Text = "Set up a career and club before editing tactics.";
            return;
        }

        _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} tactical profile";
        _matchPlanLabel.Text =
            $"Next assignment: {GameState.Instance.SelectedClubName} vs {GameState.Instance.CurrentOpponentName} on Matchday {GameState.Instance.CurrentMatchday}.";

        var formationIndex = FindFormationIndex(GameState.Instance.TacticalFormation);
        _formationOption.Select(formationIndex);
        _pressSpin.Value = GameState.Instance.PressIntensity;
        _tempoSpin.Value = GameState.Instance.Tempo;
        _widthSpin.Value = GameState.Instance.Width;
        _riskSpin.Value = GameState.Instance.Risk;
        RefreshBoard();
    }

    private void OnSavePressed()
    {
        if (GameState.Instance == null)
        {
            _statusLabel.Text = "GameState singleton is unavailable.";
            return;
        }

        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        GameState.Instance.UpdateTactics(formation, press, tempo, width, risk);
        RefreshBoard();
        _statusLabel.Text =
            $"Saved tactical board: {formation} | {DescribePress(press)} | {DescribeTempo(tempo)} | {DescribeWidth(width)} | {DescribeRisk(risk)}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void OnTacticControlChanged(double _value)
    {
        RefreshBoard();
        _statusLabel.Text = "Adjust the board, review the match plan, then save the changes into the next fixture.";
    }

    private void OnFormationSelected(long _index)
    {
        RefreshBoard();
        _statusLabel.Text = "Formation changed on the board. Save when the structure matches the next match plan.";
    }

    private int FindFormationIndex(string formation)
    {
        for (var i = 0; i < _formationOption.ItemCount; i++)
        {
            if (_formationOption.GetItemText(i) == formation)
            {
                return i;
            }
        }

        return 0;
    }

    private void RefreshBoard()
    {
        var formation = _formationOption.GetItemText(_formationOption.Selected);
        var press = (int)_pressSpin.Value;
        var tempo = (int)_tempoSpin.Value;
        var width = (int)_widthSpin.Value;
        var risk = (int)_riskSpin.Value;

        _formationBadgeLabel.Text = $"{formation} tactical shell";
        _formationBoardLabel.Text = BuildFormationBoard(formation);
        _shapeSummaryLabel.Text = BuildShapeSummary(formation);
        _pressPreviewLabel.Text = $"Press line: {DescribePress(press)}. {BuildPressPreview(press)}";
        _tempoPreviewLabel.Text = $"Ball speed: {DescribeTempo(tempo)}. {BuildTempoPreview(tempo)}";
        _widthPreviewLabel.Text = $"Pitch use: {DescribeWidth(width)}. {BuildWidthPreview(width)}";
        _riskPreviewLabel.Text = $"Commitment: {DescribeRisk(risk)}. {BuildRiskPreview(risk)}";
        _previewSummaryLabel.Text =
            $"Match plan: {formation} with {DescribePress(press).ToLowerInvariant()}, {DescribeTempo(tempo).ToLowerInvariant()}, {DescribeWidth(width).ToLowerInvariant()}, and {DescribeRisk(risk).ToLowerInvariant()}.";
    }

    private void SetControlsDisabled(bool disabled)
    {
        _formationOption.Disabled = disabled;
        _pressSpin.Editable = !disabled;
        _tempoSpin.Editable = !disabled;
        _widthSpin.Editable = !disabled;
        _riskSpin.Editable = !disabled;
    }

    private static string BuildFormationBoard(string formation)
    {
        return formation switch
        {
            "4-2-3-1" =>
                "Front line   : LW   AM   RW\nFinisher     : ST\nMidfield base: CM   CM\nBack line    : LB   CB   CB   RB\nGoalkeeper   : GK",
            "3-5-2" =>
                "Front line   : ST   ST\nLink player  : AM\nEngine room  : LWB  CM  CM  RWB\nBack line    : CB   CB   CB\nGoalkeeper   : GK",
            _ =>
                "Front line   : LW   ST   RW\nMidfield line: CM   AM   CM\nBack line    : LB   CB   CB   RB\nGoalkeeper   : GK"
        };
    }

    private static string BuildShapeSummary(string formation)
    {
        return formation switch
        {
            "4-2-3-1" =>
                "Shape note: the double pivot protects rest defense while the 10 works between the lines.",
            "3-5-2" =>
                "Shape note: the back three stabilizes buildup and the wing-backs decide whether the team stretches or compresses play.",
            _ =>
                "Shape note: the wide forwards pin the back line while the midfield triangle controls second balls and support runs."
        };
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
