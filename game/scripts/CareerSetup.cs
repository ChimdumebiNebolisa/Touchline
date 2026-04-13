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
    private Label _statusLabel = default!;
    private Label _managerPreviewLabel = default!;
    private Label _seedPreviewLabel = default!;
    private Label _worldPackPreviewLabel = default!;
    private Label _startDatePreviewLabel = default!;
    private Label _persistencePreviewLabel = default!;
    private Label _seedImpactPreviewLabel = default!;
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
        _statusLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/StatusLabel");
        _managerPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/ManagerPreviewLabel");
        _seedPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/SeedPreviewLabel");
        _worldPackPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/WorldPackPreviewLabel");
        _startDatePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/StartDatePreviewLabel");
        _persistencePreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PersistencePreviewLabel");
        _seedImpactPreviewLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/SeedImpactPreviewLabel");
        _startCareerButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/StartCareerButton");
        _backButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/FormCard/FormPadding/FormContent/ActionsRow/BackButton");
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
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 14);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(_managerPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seedPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_worldPackPreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_startDatePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_persistencePreviewLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_seedImpactPreviewLabel, 15);
    }

    private void RefreshPreview()
    {
        var managerName = _managerNameInput.Text.StripEdges();
        if (managerName.Length == 0)
        {
            managerName = "Manager";
        }

        var seed = (int)_seedInput.Value;

        if (!WorldSeedDataLoader.TryLoad(out var seedData, out var errorMessage))
        {
            _statusLabel.Text = errorMessage;
            _managerPreviewLabel.Text = $"Manager | {managerName}";
            _seedPreviewLabel.Text = $"Seed | {seed}";
            _worldPackPreviewLabel.Text = "World pack unavailable.";
            _startDatePreviewLabel.Text = "Start date unavailable.";
            _persistencePreviewLabel.Text = "Persistence preview unavailable.";
            _seedImpactPreviewLabel.Text = "Seed impact unavailable.";
            return;
        }

        WorldSeedDataLoader.TryParseStartDate(seedData.StartDateIso, out var startDate);
        _statusLabel.Text = "Start a career to initialize the world, then move straight into club selection.";
        _managerPreviewLabel.Text = $"Manager | {managerName}";
        _seedPreviewLabel.Text = $"Seed | {seed}";
        _worldPackPreviewLabel.Text = $"World pack | {seedData.CountryPackId}";
        _startDatePreviewLabel.Text = $"Start date | {startDate:ddd d MMM yyyy}";
        _persistencePreviewLabel.Text = "Persistence | Career state, squad, fixtures, and season context save to Slot 1.";
        _seedImpactPreviewLabel.Text = "Seed impact | Clubs, competition start, tactical defaults, and future saves stay anchored to this world.";
    }

    private void OnManagerNameChanged(string _newText)
    {
        RefreshPreview();
    }

    private void OnSeedValueChanged(double _value)
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
            return;
        }

        if (!TouchlineWorldGenerator.Instance.BeginNewCareer(managerName, seed))
        {
            _statusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
            return;
        }

        _statusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
        GetTree().ChangeSceneToFile(ChooseClubScenePath);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(MainMenuScenePath);
    }
}
