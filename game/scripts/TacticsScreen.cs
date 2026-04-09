using Godot;

public partial class TacticsScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _clubContextLabel = default!;
    private OptionButton _formationOption = default!;
    private SpinBox _pressSpin = default!;
    private SpinBox _tempoSpin = default!;
    private SpinBox _widthSpin = default!;
    private SpinBox _riskSpin = default!;
    private Label _statusLabel = default!;

    public override void _Ready()
    {
        _clubContextLabel = GetNode<Label>("Center/Panel/ClubContextLabel");
        _formationOption = GetNode<OptionButton>("Center/Panel/FormationOption");
        _pressSpin = GetNode<SpinBox>("Center/Panel/PressSpin");
        _tempoSpin = GetNode<SpinBox>("Center/Panel/TempoSpin");
        _widthSpin = GetNode<SpinBox>("Center/Panel/WidthSpin");
        _riskSpin = GetNode<SpinBox>("Center/Panel/RiskSpin");
        _statusLabel = GetNode<Label>("Center/Panel/StatusLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _clubContextLabel.Text = "Club context unavailable.";
            _statusLabel.Text = "Set up a career and club before editing tactics.";
            return;
        }

        _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} tactical profile";

        var formationIndex = FindFormationIndex(GameState.Instance.TacticalFormation);
        _formationOption.Select(formationIndex);
        _pressSpin.Value = GameState.Instance.PressIntensity;
        _tempoSpin.Value = GameState.Instance.Tempo;
        _widthSpin.Value = GameState.Instance.Width;
        _riskSpin.Value = GameState.Instance.Risk;
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
        _statusLabel.Text = $"Saved: {formation} | Press {press} | Tempo {tempo} | Width {width} | Risk {risk}";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
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
}
