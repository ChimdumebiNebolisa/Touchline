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
    private Label _seasonValueLabel = default!;
    private Label _fixtureValueLabel = default!;
    private Label _tableValueLabel = default!;
    private Label _formValueLabel = default!;
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
        _managerValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/ManagerValueLabel");
        _seasonValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/SeasonValueLabel");
        _fixtureValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/FixtureValueLabel");
        _tableValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/TableValueLabel");
        _formValueLabel = GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/FormValueLabel");
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
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/ManagerLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/SeasonLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/FixtureLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/TableLabel"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("Center/MenuCard/Padding/Menu/ResumeCard/ResumePadding/ResumeContent/DetailGrid/FormLabel"), 13);
        TouchlineTheme.ApplyValueStyle(_managerValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_seasonValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_fixtureValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_tableValueLabel, 16);
        TouchlineTheme.ApplyValueStyle(_formValueLabel, 16);
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
            return;
        }

        if (!SaveSystem.Instance.TryGetSlotPreview(out var saveData, out var statusMessage))
        {
            RenderUnavailableState("No saved career found.", "Start a new touchline career to create your first persistent club journey.");
            _resumeStatusLabel.Text = statusMessage;
            _loadGameButton.Text = "Continue Career";
            _loadGameButton.Disabled = true;
            TouchlineTheme.ApplyButtonVariant(_newCareerButton, TouchlineButtonVariant.Primary);
            TouchlineTheme.ApplyButtonVariant(_loadGameButton, TouchlineButtonVariant.Secondary);
            TouchlineTheme.ApplyButtonVariant(_exitButton, TouchlineButtonVariant.Tertiary);
            return;
        }

        _clubBadgeLabel.Text = BuildClubMonogram(saveData.SelectedClubName ?? "TC");
        _clubNameLabel.Text = saveData.SelectedClubName ?? "Club unavailable";
        _resumeSummaryLabel.Text = $"Manager {saveData.ManagerName} | {saveData.CompetitionName}";
        _resumeStatusLabel.Text = "Local career ready. Continue is the primary path back into the club week.";
        _managerValueLabel.Text = saveData.ManagerName;
        _seasonValueLabel.Text = $"{saveData.SeasonStartYear}/{((saveData.SeasonStartYear + 1) % 100):00} | {saveData.CurrentDateIso}";
        _fixtureValueLabel.Text = saveData.NextFixtureSummary;
        _tableValueLabel.Text = BuildTableValue(saveData);
        _formValueLabel.Text = saveData.FormSummary.StartsWith("Form: ") ? saveData.FormSummary["Form: ".Length..] : saveData.FormSummary;
        _loadGameButton.Text = "Continue Career";
        _loadGameButton.Disabled = false;
        TouchlineTheme.ApplyButtonVariant(_loadGameButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_newCareerButton, TouchlineButtonVariant.Secondary);
        TouchlineTheme.ApplyButtonVariant(_exitButton, TouchlineButtonVariant.Tertiary);
    }

    private void RenderUnavailableState(string summary, string status)
    {
        _clubBadgeLabel.Text = "--";
        _clubNameLabel.Text = "Touchline Career";
        _resumeSummaryLabel.Text = summary;
        _resumeStatusLabel.Text = status;
        _managerValueLabel.Text = "--";
        _seasonValueLabel.Text = "--";
        _fixtureValueLabel.Text = "No live fixture";
        _tableValueLabel.Text = "--";
        _formValueLabel.Text = "--";
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
