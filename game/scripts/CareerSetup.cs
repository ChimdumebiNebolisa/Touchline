using Godot;

public partial class CareerSetup : Control
{
    private const string MainMenuScenePath = "res://scenes/MainMenu.tscn";
    private const string ChooseClubScenePath = "res://scenes/ChooseClub.tscn";

    private PanelContainer _heroCard = default!;
    private PanelContainer _formCard = default!;
    private PanelContainer _previewCard = default!;
    private LineEdit _managerNameInput = default!;
    private SpinBox _seedInput = default!;
    private OptionButton _roleOption = default!;
    private OptionButton _backgroundOption = default!;
    private OptionButton _licenseOption = default!;
    private OptionButton _strictRealismOption = default!;
    private OptionButton _dramaFrequencyOption = default!;
    private OptionButton _scoutingDifficultyOption = default!;
    private OptionButton _sackingStrictnessOption = default!;
    private OptionButton _transferDifficultyOption = default!;
    private OptionButton _hiddenInfoOption = default!;
    private OptionButton _matchRandomnessOption = default!;
    private OptionButton _financeDifficultyOption = default!;
    private Label _statusLabel = default!;
    private Label _managerPreviewLabel = default!;
    private Label _seedPreviewLabel = default!;
    private Label _rolePreviewLabel = default!;
    private Label _backgroundPreviewLabel = default!;
    private Label _licensePreviewLabel = default!;
    private Label _authorityPreviewLabel = default!;
    private Label _worldPackPreviewLabel = default!;
    private Label _startDatePreviewLabel = default!;
    private Label _persistencePreviewLabel = default!;
    private Label _seedImpactPreviewLabel = default!;
    private Label _difficultyPreviewLabel = default!;
    private Button _startCareerButton = default!;
    private Button _backButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RefreshPreview();
    }

    private void CacheNodes()
    {
        _heroCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeroCard");
        _formCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/FormCard");
        _previewCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PreviewCard");
        _managerNameInput = GetNode<LineEdit>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ManagerNameInput");
        _seedInput = GetNode<SpinBox>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SeedInput");
        EnsureCareerFoundationControls();
        _roleOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/RoleOption");
        _backgroundOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/BackgroundOption");
        _licenseOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/LicenseOption");
        _strictRealismOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/StrictRealismOption");
        _dramaFrequencyOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/DramaFrequencyOption");
        _scoutingDifficultyOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ScoutingDifficultyOption");
        _sackingStrictnessOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SackingStrictnessOption");
        _transferDifficultyOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/TransferDifficultyOption");
        _hiddenInfoOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/HiddenInfoOption");
        _matchRandomnessOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/MatchRandomnessOption");
        _financeDifficultyOption = GetNode<OptionButton>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/FinanceDifficultyOption");
        _statusLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/StatusLabel");
        _managerPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/ManagerPreviewLabel");
        _seedPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/SeedPreviewLabel");
        _rolePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/RolePreviewLabel");
        _backgroundPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/BackgroundPreviewLabel");
        _licensePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/LicensePreviewLabel");
        _authorityPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/AuthorityPreviewLabel");
        _worldPackPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/WorldPackPreviewLabel");
        _startDatePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/StartDatePreviewLabel");
        _persistencePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PersistencePreviewLabel");
        _seedImpactPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/SeedImpactPreviewLabel");
        _difficultyPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/DifficultyPreviewLabel");
        _startCareerButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton");
        _backButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/BackButton");
    }

    private void EnsureCareerFoundationControls()
    {
        var formContent = GetNode<VBoxContainer>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent");
        var statusNode = formContent.GetNode("StatusLabel");
        var insertIndex = statusNode.GetIndex();
        EnsureSelectionField(formContent, ref insertIndex, "RoleLabel", "Role", "RoleOption", CareerFoundation.RoleDisplayNames, 2);
        EnsureSelectionField(formContent, ref insertIndex, "BackgroundLabel", "Manager background", "BackgroundOption", CareerFoundation.BackgroundDisplayNames, 1);
        EnsureSelectionField(formContent, ref insertIndex, "LicenseLabel", "Starting license", "LicenseOption", CareerFoundation.LicenseDisplayNames, 1);
        EnsureSelectionField(formContent, ref insertIndex, "StrictRealismLabel", "Strict realism", "StrictRealismOption", CareerDifficultyOptions.StrictRealism, 1);
        EnsureSelectionField(formContent, ref insertIndex, "DramaFrequencyLabel", "Drama frequency", "DramaFrequencyOption", CareerDifficultyOptions.DramaFrequency, 1);
        EnsureSelectionField(formContent, ref insertIndex, "ScoutingDifficultyLabel", "Scouting difficulty", "ScoutingDifficultyOption", CareerDifficultyOptions.ScoutingDifficulty, 1);
        EnsureSelectionField(formContent, ref insertIndex, "SackingStrictnessLabel", "Sacking strictness", "SackingStrictnessOption", CareerDifficultyOptions.SackingStrictness, 1);
        EnsureSelectionField(formContent, ref insertIndex, "TransferDifficultyLabel", "Transfer difficulty", "TransferDifficultyOption", CareerDifficultyOptions.TransferDifficulty, 1);
        EnsureSelectionField(formContent, ref insertIndex, "HiddenInfoLabel", "Hidden information", "HiddenInfoOption", CareerDifficultyOptions.HiddenInfo, 1);
        EnsureSelectionField(formContent, ref insertIndex, "MatchRandomnessLabel", "Match randomness", "MatchRandomnessOption", CareerDifficultyOptions.MatchRandomness, 1);
        EnsureSelectionField(formContent, ref insertIndex, "FinanceDifficultyLabel", "Finance difficulty", "FinanceDifficultyOption", CareerDifficultyOptions.FinanceDifficulty, 1);

        var previewContent = GetNode<VBoxContainer>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent");
        var worldPackNode = previewContent.GetNode("WorldPackPreviewLabel");
        var previewIndex = worldPackNode.GetIndex();
        EnsurePreviewLabel(previewContent, ref previewIndex, "RolePreviewLabel");
        EnsurePreviewLabel(previewContent, ref previewIndex, "BackgroundPreviewLabel");
        EnsurePreviewLabel(previewContent, ref previewIndex, "LicensePreviewLabel");
        EnsurePreviewLabel(previewContent, ref previewIndex, "AuthorityPreviewLabel");
        EnsurePreviewLabel(previewContent, ref previewIndex, "DifficultyPreviewLabel");
    }

    private void EnsureSelectionField(
        VBoxContainer formContent,
        ref int insertIndex,
        string labelName,
        string labelText,
        string optionName,
        string[] options,
        int selectedIndex)
    {
        var label = formContent.GetNodeOrNull<Label>(labelName);
        if (label == null)
        {
            label = new Label
            {
                Name = labelName,
                Text = labelText
            };
            formContent.AddChild(label);
            formContent.MoveChild(label, insertIndex++);
        }

        var option = formContent.GetNodeOrNull<OptionButton>(optionName);
        if (option == null)
        {
            option = new OptionButton
            {
                Name = optionName,
                SizeFlagsHorizontal = SizeFlags.ExpandFill
            };
            foreach (var item in options)
            {
                option.AddItem(item);
            }

            option.Select(selectedIndex);
            option.ItemSelected += OnCareerFoundationOptionSelected;
            formContent.AddChild(option);
            formContent.MoveChild(option, insertIndex++);
        }
    }

    private static void EnsurePreviewLabel(VBoxContainer previewContent, ref int insertIndex, string labelName)
    {
        var label = previewContent.GetNodeOrNull<Label>(labelName);
        if (label != null)
        {
            return;
        }

        label = new Label
        {
            Name = labelName,
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        previewContent.AddChild(label);
        previewContent.MoveChild(label, insertIndex++);
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_heroCard, TouchlineSurfaceVariant.Shell, 28);
        TouchlineTheme.ApplyPanelVariant(_formCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_previewCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyButtonVariant(_startCareerButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageTitleLabel"), 40);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/HeroSubtitleLabel"), 16);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/FormHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/FormHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ManagerNameLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/SeedLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/RoleLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/BackgroundLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/LicenseLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 14);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(_managerPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seedPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_rolePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_backgroundPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_licensePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_authorityPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_worldPackPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_startDatePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_persistencePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seedImpactPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_difficultyPreviewLabel, 15);
    }

    private void RefreshPreview()
    {
        var managerName = _managerNameInput.Text.StripEdges();
        if (managerName.Length == 0)
        {
            managerName = "Manager";
        }

        var seed = (int)_seedInput.Value;
        var roleName = GetSelectedText(_roleOption);
        var backgroundName = GetSelectedText(_backgroundOption);
        var licenseName = GetSelectedText(_licenseOption);
        var role = CareerFoundation.ParseRole(roleName);

        if (!WorldSeedDataLoader.TryLoad(out var seedData, out var errorMessage))
        {
            _statusLabel.Text = errorMessage;
            _managerPreviewLabel.Text = $"Manager | {managerName}";
            _seedPreviewLabel.Text = $"Seed | {seed}";
            _rolePreviewLabel.Text = $"Role | {roleName}";
            _backgroundPreviewLabel.Text = $"Background | {backgroundName}";
            _licensePreviewLabel.Text = $"License | {licenseName}";
            _authorityPreviewLabel.Text = $"Authority | {CareerFoundation.GetRoleAuthoritySummary(role)}";
            _worldPackPreviewLabel.Text = "World pack unavailable.";
            _startDatePreviewLabel.Text = "Start date unavailable.";
            _persistencePreviewLabel.Text = "Persistence preview unavailable.";
            _seedImpactPreviewLabel.Text = "Seed impact unavailable.";
            _difficultyPreviewLabel.Text = "Difficulty | unavailable";
            WriteAuditState();
            return;
        }

        WorldSeedDataLoader.TryParseStartDate(seedData.StartDateIso, out var startDate);
        _statusLabel.Text = "Start a career to initialize the world, then move straight into club selection.";
        _managerPreviewLabel.Text = $"Manager | {managerName}";
        _seedPreviewLabel.Text = $"Seed | {seed}";
        _rolePreviewLabel.Text = $"Role | {roleName}";
        _backgroundPreviewLabel.Text = $"Background | {backgroundName}";
        _licensePreviewLabel.Text = $"License | {licenseName}";
        _authorityPreviewLabel.Text = $"Authority | {CareerFoundation.GetRoleAuthoritySummary(role)}";
        _worldPackPreviewLabel.Text = $"World pack | {seedData.CountryPackId}";
        _startDatePreviewLabel.Text = $"Start date | {startDate:ddd d MMM yyyy}";
        _persistencePreviewLabel.Text = "Persistence | Career state, squad, fixtures, and season context save to Slot 1.";
        _seedImpactPreviewLabel.Text = "Seed impact | Clubs, competition start, tactical defaults, and future saves stay anchored to this world.";
        _difficultyPreviewLabel.Text = BuildSelectedDifficultyProfile().Summary;
        WriteAuditState();
    }

    private void OnManagerNameChanged(string _newText)
    {
        RefreshPreview();
    }

    private void OnSeedValueChanged(double _value)
    {
        RefreshPreview();
    }

    private void OnCareerFoundationOptionSelected(long _index)
    {
        RefreshPreview();
    }

    private void OnStartCareerPressed()
    {
        var managerName = _managerNameInput.Text.StripEdges();
        if (managerName.Length == 0)
        {
            managerName = "Manager";
        }

        var seed = (int)_seedInput.Value;

        if (TouchlineWorldGenerator.Instance == null)
        {
            _statusLabel.Text = "WorldGenerator singleton is unavailable.";
            WriteAuditState();
            return;
        }

        if (!TouchlineWorldGenerator.Instance.BeginNewCareer(
            managerName,
            seed,
            GetSelectedText(_roleOption),
            GetSelectedText(_backgroundOption),
            GetSelectedText(_licenseOption),
            BuildSelectedDifficultyProfile()))
        {
            _statusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
            WriteAuditState();
            return;
        }

        _statusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
        GetTree().ChangeSceneToFile(ChooseClubScenePath);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }

    private static string GetSelectedText(OptionButton option)
    {
        return option.GetItemText(option.Selected);
    }

    private CareerDifficultyProfile BuildSelectedDifficultyProfile()
    {
        return new CareerDifficultyProfile
        {
            StrictRealism = CareerDifficultyProfile.ParseStrictRealism(GetSelectedText(_strictRealismOption)),
            DramaFrequency = CareerDifficultyProfile.ParseDramaFrequency(GetSelectedText(_dramaFrequencyOption)),
            ScoutingDifficulty = CareerDifficultyProfile.ParseScoutingDifficulty(GetSelectedText(_scoutingDifficultyOption)),
            SackingStrictness = CareerDifficultyProfile.ParseSackingStrictness(GetSelectedText(_sackingStrictnessOption)),
            TransferDifficulty = CareerDifficultyProfile.ParseTransferDifficulty(GetSelectedText(_transferDifficultyOption)),
            HiddenInfo = CareerDifficultyProfile.ParseHiddenInfo(GetSelectedText(_hiddenInfoOption)),
            MatchRandomness = CareerDifficultyProfile.ParseMatchRandomness(GetSelectedText(_matchRandomnessOption)),
            FinanceDifficulty = CareerDifficultyProfile.ParseFinanceDifficulty(GetSelectedText(_financeDifficultyOption))
        };
    }

    private void WriteAuditState()
    {
        AuditUiStateWriter.Write(
            nameof(CareerSetup),
            _rolePreviewLabel.Text,
            TouchlineRailRoute.None,
            _statusLabel.Text,
            _managerPreviewLabel.Text,
            _seedPreviewLabel.Text,
            _rolePreviewLabel.Text,
            _backgroundPreviewLabel.Text,
            _licensePreviewLabel.Text,
            _authorityPreviewLabel.Text,
            _difficultyPreviewLabel.Text);
    }
}
