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
    private Label _roleValueLabel = default!;
    private Label _seasonValueLabel = default!;
    private Label _fixtureValueLabel = default!;
    private Label _saveValueLabel = default!;
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
        _managerValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/ManagerRow/ManagerValueLabel");
        _roleValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/RoleRow/RoleValueLabel");
        _seasonValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SeasonRow/SeasonValueLabel");
        _fixtureValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/FixtureRow/FixtureValueLabel");
        _saveValueLabel = GetNode<Label>("RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SaveRow/SaveValueLabel");
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
        var detailLabelPaths = new[]
        {
            "RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/ManagerRow/ManagerLabel",
            "RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/RoleRow/RoleLabel",
            "RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SeasonRow/SeasonLabel",
            "RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/FixtureRow/FixtureLabel",
            "RootMargin/MainColumn/SlotCard/SlotPadding/SlotContent/DetailRows/SaveRow/SaveLabel"
        };
        foreach (var path in detailLabelPaths)
        {
            TouchlineTheme.ApplyMutedStyle(GetNode<Label>(path), 13);
        }

        TouchlineTheme.ApplyValueStyle(_managerValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_roleValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_seasonValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_fixtureValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_saveValueLabel, 16);
    }

    private void RenderState()
    {
        if (SaveSystem.Instance == null)
        {
            _slotSummaryLabel.Text = "Save system unavailable.";
            _statusLabel.Text = "Load is unavailable until the save singleton is active.";
            _managerValueLabel.Text = "--";
            _roleValueLabel.Text = "Role unavailable";
            _seasonValueLabel.Text = "Date unavailable";
            _fixtureValueLabel.Text = "No live fixture";
            _saveValueLabel.Text = "Slot 1 | No save";
            _loadButton.Text = "Continue Career";
            _loadButton.Disabled = true;
            WriteAuditState();
            return;
        }

        if (!SaveSystem.Instance.TryGetSlotPreview(out var saveData, out var statusMessage))
        {
            _slotSummaryLabel.Text = $"Slot 1 unavailable | {statusMessage}";
            _statusLabel.Text = "Load is disabled because no complete local career can be restored.";
            _managerValueLabel.Text = "--";
            _roleValueLabel.Text = "Role unavailable";
            _seasonValueLabel.Text = "Date unavailable";
            _fixtureValueLabel.Text = "No live fixture";
            _saveValueLabel.Text = "Slot 1 | No save";
            _loadButton.Text = "Continue Career";
            _loadButton.Disabled = true;
            WriteAuditState();
            return;
        }

        _slotSummaryLabel.Text = $"Slot 1 ready | {saveData.SelectedClubName} | {saveData.CompetitionName}";
        _statusLabel.Text = BuildCareerSummary(saveData);
        _managerValueLabel.Text = saveData.ManagerName;
        _roleValueLabel.Text = saveData.CareerProfile?.RoleName ?? "Role unavailable";
        _seasonValueLabel.Text = $"{saveData.SeasonStartYear}/{((saveData.SeasonStartYear + 1) % 100):00} | {saveData.CurrentDateIso} | Matchday {saveData.CurrentMatchday}";
        _fixtureValueLabel.Text = saveData.NextFixtureSummary;
        _saveValueLabel.Text = $"Slot 1 | Save v{saveData.SaveVersion}";
        _loadButton.Text = "Continue Career";
        _loadButton.Disabled = false;
        WriteAuditState();
    }

    private static string BuildCareerSummary(SaveSlotData saveData)
    {
        if (string.IsNullOrWhiteSpace(saveData.SelectedClubName) || saveData.CompetitionTable == null)
        {
            return $"Career summary | {saveData.CompetitionName} | Table unavailable | {TrimForm(saveData.FormSummary)}";
        }

        for (var index = 0; index < saveData.CompetitionTable.Length; index++)
        {
            var row = saveData.CompetitionTable[index];
            if (row.ClubName == saveData.SelectedClubName)
            {
                return $"Career summary | Table {index + 1}/{saveData.CompetitionTable.Length}, {row.Points} pts | {TrimForm(saveData.FormSummary)}";
            }
        }

        return $"Career summary | {saveData.CompetitionName} | Table unavailable | {TrimForm(saveData.FormSummary)}";
    }

    private static string TrimForm(string formSummary)
    {
        return formSummary.StartsWith("Form: ") ? formSummary["Form: ".Length..] : formSummary;
    }

    private void OnLoadPressed()
    {
        if (SaveSystem.Instance == null)
        {
            _statusLabel.Text = "Save system unavailable.";
            WriteAuditState();
            return;
        }

        if (SaveSystem.Instance.LoadGame(out var statusMessage))
        {
            GetTree().ChangeSceneToFile(ClubDashboardScenePath);
            return;
        }

        _statusLabel.Text = statusMessage;
        WriteAuditState();
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }

    private void WriteAuditState()
    {
        AuditUiStateWriter.Write(
            nameof(SaveLoadScene),
            _roleValueLabel.Text,
            TouchlineRailRoute.None,
            _slotSummaryLabel.Text,
            _statusLabel.Text,
            _managerValueLabel.Text,
            _roleValueLabel.Text,
            _seasonValueLabel.Text,
            _fixtureValueLabel.Text,
            _saveValueLabel.Text);
    }
}
