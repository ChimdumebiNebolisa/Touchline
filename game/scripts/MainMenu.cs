using Godot;

public partial class MainMenu : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string SaveLoadScenePath = "res://scenes/SaveLoadScene.tscn";

    private PanelContainer _menuCard = default!;
    private PanelContainer _resumeCard = default!;
    private Label _clubBadgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _resumeSummaryLabel = default!;
    private Label _resumeStatusLabel = default!;
    private Label _managerValueLabel = default!;
    private Label _roleValueLabel = default!;
    private Label _seasonValueLabel = default!;
    private Label _fixtureValueLabel = default!;
    private Label _saveValueLabel = default!;
    private Button _newCareerButton = default!;
    private Button _loadGameButton = default!;
    private Button _exitButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyMenuStyles();
        RenderMenuState();
    }

    private void CacheNodes()
    {
        _menuCard = GetNode<PanelContainer>("Center/MenuCard");
        _resumeCard = GetNode<PanelContainer>("Center/MenuCard/Padding/Menu/ResumeCard");
        _clubBadgeLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeTopRow/Badge/BadgeLabel");
        _clubNameLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeTopRow/ResumeMeta/ClubNameLabel");
        _resumeSummaryLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeSummaryLabel");
        _resumeStatusLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeStatusLabel");
        _managerValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/ManagerRow/ManagerValueLabel");
        _roleValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/RoleRow/RoleValueLabel");
        _seasonValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/SeasonRow/SeasonValueLabel");
        _fixtureValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/FixtureRow/FixtureValueLabel");
        _saveValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/SaveRow/SaveValueLabel");
        _newCareerButton = GetNode<Button>("Center/MenuCard/Padding/Menu/NewCareerButton");
        _loadGameButton = GetNode<Button>("Center/MenuCard/Padding/Menu/LoadGameButton");
        _exitButton = GetNode<Button>("Center/MenuCard/Padding/Menu/ExitButton");
    }

    private void ApplyMenuStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_menuCard, TouchlineSurfaceVariant.Shell, 28);
        TouchlineTheme.ApplyPanelVariant(_resumeCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/Eyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/Title"), 48);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/Subtitle"), 16);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeTopRow/ResumeMeta/ClubNameLabel"), 28);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/ResumeTopRow/ResumeMeta/ResumeHeading"), 14);
        TouchlineTheme.ApplyMutedStyle(_resumeSummaryLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_resumeStatusLabel, 14);
        TouchlineTheme.ApplyValueStyle(_clubBadgeLabel, 20);
        var detailLabelPaths = new[]
        {
            "Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/ManagerRow/ManagerLabel",
            "Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/RoleRow/RoleLabel",
            "Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/SeasonRow/SeasonLabel",
            "Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/FixtureRow/FixtureLabel",
            "Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailRows/SaveRow/SaveLabel"
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
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/Footer"), 14);
    }

    private void RenderMenuState()
    {
        if (SaveSystem.Instance == null)
        {
            RenderUnavailableState("Save system unavailable.", "Resume flow is offline until the save singleton is active.");
            _loadGameButton.Disabled = true;
            TouchlineTheme.ApplyButtonVariant(_newCareerButton, TouchlineButtonVariant.Primary);
            TouchlineTheme.ApplyButtonVariant(_loadGameButton, TouchlineButtonVariant.Secondary);
            TouchlineTheme.ApplyButtonVariant(_exitButton, TouchlineButtonVariant.Tertiary);
            WriteAuditState();
            return;
        }

        if (!SaveSystem.Instance.TryGetSlotPreview(out var saveData, out var statusMessage))
        {
            RenderUnavailableState("Slot 1 unavailable.", $"No complete local career can be resumed: {statusMessage}");
            _resumeStatusLabel.Text = $"No complete local career can be resumed: {statusMessage}";
            _loadGameButton.Text = "Continue Career";
            _loadGameButton.Disabled = true;
            TouchlineTheme.ApplyButtonVariant(_newCareerButton, TouchlineButtonVariant.Primary);
            TouchlineTheme.ApplyButtonVariant(_loadGameButton, TouchlineButtonVariant.Secondary);
            TouchlineTheme.ApplyButtonVariant(_exitButton, TouchlineButtonVariant.Tertiary);
            WriteAuditState();
            return;
        }

        _clubBadgeLabel.Text = BuildClubMonogram(saveData.SelectedClubName ?? "TC");
        _clubNameLabel.Text = saveData.SelectedClubName ?? "Club unavailable";
        _resumeSummaryLabel.Text = BuildCareerSummary(saveData);
        _resumeStatusLabel.Text = $"Local career ready for {saveData.SelectedClubName}. Continue returns to the club week.";
        _managerValueLabel.Text = saveData.ManagerName;
        _roleValueLabel.Text = saveData.CareerProfile?.RoleName ?? "Role unavailable";
        _seasonValueLabel.Text = $"{saveData.SeasonStartYear}/{((saveData.SeasonStartYear + 1) % 100):00} | {saveData.CurrentDateIso} | Matchday {saveData.CurrentMatchday}";
        _fixtureValueLabel.Text = saveData.NextFixtureSummary;
        _saveValueLabel.Text = $"Slot 1 | Save v{saveData.SaveVersion}";
        _loadGameButton.Text = "Continue Career";
        _loadGameButton.Disabled = false;
        TouchlineTheme.ApplyButtonVariant(_loadGameButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_newCareerButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_exitButton, TouchlineButtonVariant.Tertiary);
        WriteAuditState();
    }

    private void RenderUnavailableState(string summary, string status)
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Touchline Career";
        _resumeSummaryLabel.Text = summary;
        _resumeStatusLabel.Text = status;
        _managerValueLabel.Text = "--";
        _roleValueLabel.Text = "Role unavailable";
        _seasonValueLabel.Text = "Date unavailable";
        _fixtureValueLabel.Text = "No live fixture";
        _saveValueLabel.Text = "Slot 1 | No save";
    }

    private static string BuildCareerSummary(SaveSlotData saveData)
    {
        if (string.IsNullOrWhiteSpace(saveData.SelectedClubName) || saveData.CompetitionTable == null)
        {
            return $"Slot 1 ready | {saveData.CompetitionName} | Table unavailable | {TrimForm(saveData.FormSummary)}";
        }

        for (var index = 0; index < saveData.CompetitionTable.Length; index++)
        {
            var row = saveData.CompetitionTable[index];
            if (row.ClubName == saveData.SelectedClubName)
            {
                return $"Slot 1 ready | {saveData.CompetitionName} | Table {index + 1}/{saveData.CompetitionTable.Length}, {row.Points} pts | {TrimForm(saveData.FormSummary)}";
            }
        }

        return $"Slot 1 ready | {saveData.CompetitionName} | Table unavailable | {TrimForm(saveData.FormSummary)}";
    }

    private static string TrimForm(string formSummary)
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

    private void WriteAuditState()
    {
        AuditUiStateWriter.Write(
            nameof(MainMenu),
            _roleValueLabel.Text,
            TouchlineRailRoute.None,
            _clubNameLabel.Text,
            _resumeSummaryLabel.Text,
            _resumeStatusLabel.Text,
            _managerValueLabel.Text,
            _roleValueLabel.Text,
            _seasonValueLabel.Text,
            _fixtureValueLabel.Text,
            _saveValueLabel.Text);
    }

    private void OnNewCareerPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }

    private void OnLoadGamePressed()
    {
        GetTree().ChangeSceneToFile(SaveLoadScenePath);
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}
