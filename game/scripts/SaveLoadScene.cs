using Godot;

public partial class SaveLoadScene : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private PanelContainer _heroCard = default!;
    private PanelContainer _slotCard = default!;
    private Label _slotSummaryLabel = default!;
    private Label _statusLabel = default!;
    private Label _managerValueLabel = default!;
    private Label _seasonValueLabel = default!;
    private Label _fixtureValueLabel = default!;
    private Label _tableValueLabel = default!;
    private Button _loadButton = default!;
    private Button _backButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _heroCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeroCard");
        _slotCard = GetNode<PanelContainer>("RootMargin/MainColumn/SlotCard");
        _slotSummaryLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/SlotSummaryLabel");
        _statusLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/StatusLabel");
        _managerValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/ManagerValueLabel");
        _seasonValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/SeasonValueLabel");
        _fixtureValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/FixtureValueLabel");
        _tableValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/TableValueLabel");
        _loadButton = GetNode<Button>("RootMargin/MainColumn/ActionsRow/LoadButton");
        _backButton = GetNode<Button>("RootMargin/MainColumn/ActionsRow/BackButton");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_heroCard, TouchlineSurfaceVariant.Shell, 28);
        TouchlineTheme.ApplyPanelVariant(_slotCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyButtonVariant(_loadButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageTitleLabel"), 38);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/HeroSubtitleLabel"), 16);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/SlotHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(_slotSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/ManagerLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/SeasonLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/FixtureLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailGrid/TableLabel"), 13);
        TouchlineTheme.ApplyValueStyle(_managerValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_seasonValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_fixtureValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_tableValueLabel, 16);
    }

    private void RenderState()
    {
        if (SaveSystem.Instance == null)
        {
            _slotSummaryLabel.Text = "Save system unavailable.";
            _statusLabel.Text = "Load is unavailable until the save singleton is active.";
            _managerValueLabel.Text = "--";
            _seasonValueLabel.Text = "--";
            _fixtureValueLabel.Text = "No live fixture";
            _tableValueLabel.Text = "--";
            _loadButton.Text = "Continue Career";
            _loadButton.Disabled = true;
            return;
        }

        if (!SaveSystem.Instance.TryGetSlotPreview(out var saveData, out var statusMessage))
        {
            _slotSummaryLabel.Text = statusMessage;
            _statusLabel.Text = "No local save found yet. Start a career and save from the dashboard.";
            _managerValueLabel.Text = "--";
            _seasonValueLabel.Text = "--";
            _fixtureValueLabel.Text = "No live fixture";
            _tableValueLabel.Text = "--";
            _loadButton.Text = "Continue Career";
            _loadButton.Disabled = true;
            return;
        }

        _slotSummaryLabel.Text = $"{saveData.SelectedClubName} | {saveData.CompetitionName}";
        _statusLabel.Text = "Slot 1 is ready to continue from the latest local save.";
        _managerValueLabel.Text = saveData.ManagerName;
        _seasonValueLabel.Text = $"{saveData.SeasonStartYear}/{((saveData.SeasonStartYear + 1) % 100):00} | {saveData.CurrentDateIso}";
        _fixtureValueLabel.Text = saveData.NextFixtureSummary;
        _tableValueLabel.Text = BuildTableValue(saveData);
        _loadButton.Text = "Continue Career";
        _loadButton.Disabled = false;
    }

    private static string BuildTableValue(SaveSlotData saveData)
    {
        if (string.IsNullOrWhiteSpace(saveData.SelectedClubName) || saveData.CompetitionTable == null)
        {
            return "Position unavailable";
        }

        for (var index = 0; index < saveData.CompetitionTable.Length; index++)
        {
            var row = saveData.CompetitionTable[index];
            if (row.ClubName == saveData.SelectedClubName)
            {
                return $"{index + 1}/{saveData.CompetitionTable.Length} | {row.Points} pts";
            }
        }

        return "Position unavailable";
    }

    private void OnLoadPressed()
    {
        if (SaveSystem.Instance == null)
        {
            _statusLabel.Text = "Save system unavailable.";
            return;
        }

        if (SaveSystem.Instance.LoadGame(out var statusMessage))
        {
            GetTree().ChangeSceneToFile(ClubDashboardScenePath);
            return;
        }

        _statusLabel.Text = statusMessage;
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
