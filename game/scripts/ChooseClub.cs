using Godot;
using System.Collections.Generic;

public partial class ChooseClub : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _careerSummaryLabel = default!;
    private Label _selectionStatusLabel = default!;
    private VBoxContainer _clubRows = default!;
    private Label _previewClubBadgeLabel = default!;
    private Label _previewClubNameLabel = default!;
    private Label _identityTagLabel = default!;
    private Label _pressureTagLabel = default!;
    private Label _identityLabel = default!;
    private Label _expectationLabel = default!;
    private Label _openingFixtureLabel = default!;
    private Button _confirmSelectionButton = default!;
    private Button _backButton = default!;

    private PanelContainer _heroCard = default!;
    private PanelContainer _listCard = default!;
    private PanelContainer _previewCard = default!;
    private PanelContainer _identityTag = default!;
    private PanelContainer _pressureTag = default!;

    private readonly List<string> _visibleClubs = new();
    private string? _selectedClubName;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _heroCard = GetNode<PanelContainer>("RootMargin/MainColumn/HeroCard");
        _listCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/ListCard");
        _previewCard = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PreviewCard");
        _careerSummaryLabel = GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/CareerSummaryLabel");
        _selectionStatusLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/SelectionStatusLabel");
        _clubRows = GetNode<VBoxContainer>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ClubScroll/ClubRows");
        _previewClubBadgeLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewTopRow/Badge/BadgeLabel");
        _previewClubNameLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewTopRow/ClubMeta/ClubNameLabel");
        _identityTag = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/TagRow/IdentityTag");
        _identityTagLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/TagRow/IdentityTag/IdentityTagPadding/IdentityTagLabel");
        _pressureTag = GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/TagRow/PressureTag");
        _pressureTagLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/TagRow/PressureTag/PressureTagPadding/PressureTagLabel");
        _identityLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/IdentityLabel");
        _expectationLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/ExpectationLabel");
        _openingFixtureLabel = GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/OpeningFixtureLabel");
        _confirmSelectionButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/ConfirmSelectionButton");
        _backButton = GetNode<Button>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ActionsRow/BackButton");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_heroCard, TouchlineSurfaceVariant.Shell, 28);
        TouchlineTheme.ApplyPanelVariant(_listCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_previewCard, TouchlineSurfaceVariant.Muted, 24);
        TouchlineTheme.ApplyPanelVariant(_identityTag, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(_pressureTag, TouchlineSurfaceVariant.Positive, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewTopRow/Badge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyButtonVariant(_confirmSelectionButton, TouchlineButtonVariant.Primary);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Tertiary);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/PageTitleLabel"), 40);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/HeroCard/HeroPadding/HeroContent/HeroSubtitleLabel"), 16);
        TouchlineTheme.ApplyMutedStyle(_careerSummaryLabel, 15);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ListHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/ListCard/ListPadding/ListContent/ListHintLabel"), 14);
        TouchlineTheme.ApplyMutedStyle(_selectionStatusLabel, 14);
        TouchlineTheme.ApplyValueStyle(_previewClubBadgeLabel, 20);
        TouchlineTheme.ApplyTitleStyle(_previewClubNameLabel, 28);
        TouchlineTheme.ApplyMutedStyle(_identityTagLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_pressureTagLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_identityLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_expectationLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_openingFixtureLabel, 15);
        TouchlineTheme.ApplyTitleStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHeading"), 24);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/MainColumn/ContentRow/PreviewCard/PreviewPadding/PreviewContent/PreviewHintLabel"), 14);
    }

    private void RenderState()
    {
        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            _careerSummaryLabel.Text = "Career setup is incomplete.";
            _selectionStatusLabel.Text = "Career setup is incomplete.";
            _confirmSelectionButton.Disabled = true;
            RenderFallbackPreview("Club context unavailable.");
            return;
        }

        _careerSummaryLabel.Text =
            $"Manager {GameState.Instance.ManagerName} | Seed {GameState.Instance.WorldSeed} | {GameState.Instance.CountryPackId}";
        _selectionStatusLabel.Text = "Select a club to preview its identity and pressure before you confirm.";
        _confirmSelectionButton.Disabled = false;
        PopulateClubRows();
    }

    public void SelectClubRow(int visibleIndex)
    {
        if (visibleIndex < 0 || visibleIndex >= _visibleClubs.Count)
        {
            return;
        }

        _selectedClubName = _visibleClubs[visibleIndex];
        PopulateClubRows();
        RenderPreview(_selectedClubName);
    }

    private void PopulateClubRows()
    {
        ClearContainer(_clubRows);
        _visibleClubs.Clear();

        if (GameState.Instance == null)
        {
            return;
        }

        foreach (var clubName in GameState.Instance.AvailableClubs)
        {
            _visibleClubs.Add(clubName);
        }

        if (_visibleClubs.Count == 0)
        {
            _selectionStatusLabel.Text = "No clubs are available from seeded data.";
            RenderFallbackPreview("No club loaded.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_selectedClubName))
        {
            _selectedClubName = _visibleClubs[0];
        }

        for (var index = 0; index < _visibleClubs.Count; index++)
        {
            var clubName = _visibleClubs[index];
            var preview = TouchlineWorldGenerator.Instance?.GetClubPreview(clubName);
            _clubRows.AddChild(CreateClubRow(index, clubName, preview, clubName == _selectedClubName));
        }

        RenderPreview(_selectedClubName);
    }

    private Control CreateClubRow(int visibleIndex, string clubName, GameState.ClubPreview? preview, bool selected)
    {
        var rowPanel = new PanelContainer
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyPanelVariant(
            rowPanel,
            selected ? TouchlineSurfaceVariant.Accent : TouchlineSurfaceVariant.Card,
            18);
        rowPanel.MouseDefaultCursorShape = CursorShape.PointingHand;

        var padding = CreateMarginContainer(16, 14, 16, 14);
        padding.Name = "RowPadding";
        rowPanel.AddChild(padding);

        var rowContent = new HBoxContainer
        {
            Name = "RowContent",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        rowContent.AddThemeConstantOverride("separation", 14);
        padding.AddChild(rowContent);

        var badgePanel = new PanelContainer
        {
            CustomMinimumSize = new Vector2(50, 50)
        };
        TouchlineTheme.ApplyPanelVariant(badgePanel, selected ? TouchlineSurfaceVariant.Positive : TouchlineSurfaceVariant.Accent, 18);
        rowContent.AddChild(badgePanel);

        var badgeLabel = new Label
        {
            Text = BuildClubMonogram(clubName),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            AnchorsPreset = (int)Control.LayoutPreset.Center
        };
        badgeLabel.AddThemeFontSizeOverride("font_size", 18);
        badgeLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        badgePanel.AddChild(badgeLabel);

        var body = new VBoxContainer
        {
            Name = "Body",
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        body.AddThemeConstantOverride("separation", 4);
        rowContent.AddChild(body);

        var nameLabel = new Label
        {
            Name = "NameLabel",
            Text = clubName
        };
        nameLabel.AddThemeFontSizeOverride("font_size", 17);
        nameLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        body.AddChild(nameLabel);

        var detailLabel = new Label
        {
            Name = "DetailLabel",
            Text = BuildClubRowDetail(preview),
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        detailLabel.AddThemeFontSizeOverride("font_size", 14);
        detailLabel.AddThemeColorOverride("font_color", TouchlineTheme.TextMuted);
        body.AddChild(detailLabel);

        var tags = new VBoxContainer();
        tags.AddThemeConstantOverride("separation", 6);
        rowContent.AddChild(tags);
        tags.AddChild(CreateTagChip(BuildStyleTag(preview), TouchlineSurfaceVariant.Accent));
        tags.AddChild(CreateTagChip(BuildPressureTag(preview), TouchlineSurfaceVariant.Positive));

        rowPanel.GuiInput += @event =>
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                SelectClubRow(visibleIndex);
            }
        };

        return rowPanel;
    }

    private void RenderPreview(string? clubName)
    {
        if (string.IsNullOrWhiteSpace(clubName) || TouchlineWorldGenerator.Instance == null)
        {
            RenderFallbackPreview("Club context unavailable.");
            return;
        }

        var preview = TouchlineWorldGenerator.Instance.GetClubPreview(clubName);
        _previewClubBadgeLabel.Text = BuildClubMonogram(preview.ClubName);
        _previewClubNameLabel.Text = preview.ClubName;
        _identityTagLabel.Text = BuildStyleTag(preview);
        _pressureTagLabel.Text = BuildPressureTag(preview);
        _identityLabel.Text = $"Identity | {preview.IdentitySummary}";
        _expectationLabel.Text = $"Board line | {preview.ExpectationSummary.Replace("Board line: ", string.Empty)}";
        _openingFixtureLabel.Text = preview.OpeningFixtureSummary;
        _selectionStatusLabel.Text = $"Selected | {preview.ClubName}";
        _confirmSelectionButton.Text = $"Take Charge of {preview.ClubName}";
    }

    private void RenderFallbackPreview(string clubName)
    {
        _previewClubBadgeLabel.Text = "--";
        _previewClubNameLabel.Text = clubName;
        _identityTagLabel.Text = "NO IDENTITY";
        _pressureTagLabel.Text = "NO BRIEF";
        _identityLabel.Text = "Identity unavailable.";
        _expectationLabel.Text = "Board line unavailable.";
        _openingFixtureLabel.Text = "Opening fixture unavailable.";
        _confirmSelectionButton.Text = "Confirm Club Selection";
    }

    private void OnConfirmSelectionPressed()
    {
        if (TouchlineWorldGenerator.Instance == null)
        {
            _selectionStatusLabel.Text = "WorldGenerator singleton is unavailable.";
            return;
        }

        if (string.IsNullOrWhiteSpace(_selectedClubName))
        {
            _selectionStatusLabel.Text = "Select a club before confirming.";
            return;
        }

        if (!TouchlineWorldGenerator.Instance.SelectClub(_selectedClubName))
        {
            _selectionStatusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
            return;
        }

        _selectionStatusLabel.Text = TouchlineWorldGenerator.Instance.LastStatusMessage;
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private static string BuildClubRowDetail(GameState.ClubPreview? preview)
    {
        if (preview == null)
        {
            return "Preview unavailable.";
        }

        return $"{BuildStyleTag(preview)} | {BuildPressureTag(preview)}";
    }

    private static string BuildStyleTag(GameState.ClubPreview? preview)
    {
        if (preview == null)
        {
            return "Unknown";
        }

        var identity = preview.IdentitySummary.ToLowerInvariant();
        if (identity.Contains("proactive") || identity.Contains("quick ball circulation"))
        {
            return "Front-foot";
        }

        if (identity.Contains("ball back within five seconds") || identity.Contains("dictate territory"))
        {
            return "Press-first";
        }

        if (identity.Contains("combative") || identity.Contains("hostile"))
        {
            return "Combative";
        }

        if (identity.Contains("youth-heavy") || identity.Contains("wide play"))
        {
            return "Brave width";
        }

        return "Club identity";
    }

    private static string BuildPressureTag(GameState.ClubPreview? preview)
    {
        if (preview == null)
        {
            return "No brief";
        }

        var expectation = preview.ExpectationSummary.ToLowerInvariant();
        if (expectation.Contains("title conversation"))
        {
            return "High pressure";
        }

        if (expectation.Contains("top half"))
        {
            return "Top-half brief";
        }

        if (expectation.Contains("bottom places"))
        {
            return "Survival brief";
        }

        if (expectation.Contains("upward") || expectation.Contains("progress"))
        {
            return "Growth brief";
        }

        return "Board brief";
    }

    private static PanelContainer CreateTagChip(string text, TouchlineSurfaceVariant variant)
    {
        var panel = new PanelContainer();
        TouchlineTheme.ApplyPanelVariant(panel, variant, 999);
        var margin = CreateMarginContainer(10, 4, 10, 4);
        panel.AddChild(margin);
        var label = new Label
        {
            Text = text,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        label.AddThemeFontSizeOverride("font_size", 12);
        label.AddThemeColorOverride("font_color", TouchlineTheme.TextPrimary);
        margin.AddChild(label);
        return panel;
    }

    private static MarginContainer CreateMarginContainer(int left, int top, int right, int bottom)
    {
        var margin = new MarginContainer();
        margin.AddThemeConstantOverride("margin_left", left);
        margin.AddThemeConstantOverride("margin_top", top);
        margin.AddThemeConstantOverride("margin_right", right);
        margin.AddThemeConstantOverride("margin_bottom", bottom);
        return margin;
    }

    private static void ClearContainer(Container container)
    {
        foreach (Node child in container.GetChildren())
        {
            child.QueueFree();
        }
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

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }
}
