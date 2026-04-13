using Godot;

public partial class PlayerProfile : Control
{
    private const string SquadScreenScenePath = "res://scenes/SquadScreen.tscn";

    private Label _badgeLabel = default!;
    private Label _clubNameLabel = default!;
    private Label _roleChipLabel = default!;
    private Label _clubContextLabel = default!;
    private Label _pageTitleLabel = default!;
    private Label _statusLabel = default!;
    private Label _positionValueLabel = default!;
    private Label _positionMetaLabel = default!;
    private Label _ageValueLabel = default!;
    private Label _ageMetaLabel = default!;
    private Label _formValueLabel = default!;
    private Label _formMetaLabel = default!;
    private Label _fitnessValueLabel = default!;
    private Label _fitnessMetaLabel = default!;
    private Label _identityLabel = default!;
    private Label _roleLabel = default!;
    private Label _conditionLabel = default!;
    private Label _pathwayLabel = default!;
    private Label _readinessLabel = default!;
    private PanelContainer _contextCard = default!;
    private PanelContainer _headerCard = default!;
    private PanelContainer _profileCard = default!;
    private PanelContainer _insightCard = default!;
    private PanelContainer _roleChip = default!;
    private Button _backButton = default!;

    public override void _Ready()
    {
        CacheNodes();
        ApplyShellStyles();
        RenderState();
    }

    private void CacheNodes()
    {
        _contextCard = GetNode<PanelContainer>("RootMargin/Shell/ContextColumn/ContextCard");
        _badgeLabel = GetNode<Label>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubBadge/BadgeLabel");
        _clubNameLabel = GetNode<Label>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubNameLabel");
        _roleChip = GetNode<PanelContainer>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/RoleChip");
        _roleChipLabel = GetNode<Label>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/RoleChip/RoleChipPadding/RoleChipLabel");
        _clubContextLabel = GetNode<Label>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubContextLabel");
        _backButton = GetNode<Button>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/BackButton");

        _headerCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/HeaderCard");
        _pageTitleLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/PageTitleLabel");
        _statusLabel = GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/StatusLabel");

        _positionValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardValueLabel");
        _positionMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardMetaLabel");
        _ageValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/AgeCard/CardPadding/CardContent/CardValueLabel");
        _ageMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/AgeCard/CardPadding/CardContent/CardMetaLabel");
        _formValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardValueLabel");
        _formMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardMetaLabel");
        _fitnessValueLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardValueLabel");
        _fitnessMetaLabel = GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardMetaLabel");

        _profileCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/ProfileCard");
        _identityLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/IdentityLabel");
        _roleLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/RoleLabel");
        _conditionLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/ConditionLabel");

        _insightCard = GetNode<PanelContainer>("RootMargin/Shell/MainColumn/ContentRow/InsightCard");
        _pathwayLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/PathwayLabel");
        _readinessLabel = GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/ReadinessLabel");
    }

    private void ApplyShellStyles()
    {
        TouchlineTheme.ApplyPanelVariant(_contextCard, TouchlineSurfaceVariant.Rail, 24);
        TouchlineTheme.ApplyPanelVariant(_headerCard, TouchlineSurfaceVariant.Shell, 24);
        TouchlineTheme.ApplyPanelVariant(_profileCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_insightCard, TouchlineSurfaceVariant.Card, 24);
        TouchlineTheme.ApplyPanelVariant(_roleChip, TouchlineSurfaceVariant.Accent, 999);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/ContextColumn/ContextCard/ContextPadding/ContextContent/ClubBadge"), TouchlineSurfaceVariant.Accent, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/AgeCard"), TouchlineSurfaceVariant.Card, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard"), TouchlineSurfaceVariant.Positive, 20);
        TouchlineTheme.ApplyPanelVariant(GetNode<PanelContainer>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard"), TouchlineSurfaceVariant.Card, 20);

        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/HeaderCard/HeaderPadding/HeaderContent/PageEyebrow"));
        TouchlineTheme.ApplyTitleStyle(_pageTitleLabel, 34);
        TouchlineTheme.ApplyMutedStyle(_statusLabel, 16);
        TouchlineTheme.ApplyTitleStyle(_clubNameLabel, 26);
        TouchlineTheme.ApplyValueStyle(_badgeLabel, 22);
        TouchlineTheme.ApplyMutedStyle(_roleChipLabel, 13);
        TouchlineTheme.ApplyMutedStyle(_clubContextLabel, 15);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/PositionCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/AgeCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FormCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyMutedStyle(GetNode<Label>("RootMargin/Shell/MainColumn/SummaryGrid/FitnessCard/CardPadding/CardContent/CardEyebrow"), 13);
        TouchlineTheme.ApplyValueStyle(_positionValueLabel, 30);
        TouchlineTheme.ApplyValueStyle(_ageValueLabel, 30);
        TouchlineTheme.ApplyPositiveValueStyle(_formValueLabel, 30);
        TouchlineTheme.ApplyAccentValueStyle(_fitnessValueLabel, 30);
        TouchlineTheme.ApplyMutedStyle(_positionMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_ageMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_formMetaLabel, 14);
        TouchlineTheme.ApplyMutedStyle(_fitnessMetaLabel, 14);
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/ProfileCard/ProfilePadding/ProfileContent/ProfileEyebrow"));
        TouchlineTheme.ApplyEyebrowStyle(GetNode<Label>("RootMargin/Shell/MainColumn/ContentRow/InsightCard/InsightPadding/InsightContent/InsightEyebrow"));
        TouchlineTheme.ApplyMutedStyle(_identityLabel, 16);
        TouchlineTheme.ApplyMutedStyle(_roleLabel, 16);
        TouchlineTheme.ApplyMutedStyle(_conditionLabel, 16);
        TouchlineTheme.ApplyMutedStyle(_pathwayLabel, 15);
        TouchlineTheme.ApplyMutedStyle(_readinessLabel, 15);
        TouchlineTheme.ApplyButtonVariant(_backButton, TouchlineButtonVariant.Secondary);
    }

    private void RenderState()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            RenderUnavailableState("Player Profile", "Select a player from the squad workspace.");
            return;
        }

        var player = GameState.Instance.GetSelectedPlayerProfile();
        if (player == null)
        {
            RenderUnavailableState("Player Profile", "No player is currently selected for inspection.");
            _clubNameLabel.Text = GameState.Instance.SelectedClubName!;
            _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} | Squad context only";
            _badgeLabel.Text = BuildClubMonogram(GameState.Instance.SelectedClubName!);
            return;
        }

        var lineupStatus = player.IsStarting ? "STARTING XI" : "BENCH OPTION";
        _badgeLabel.Text = BuildClubMonogram(GameState.Instance.SelectedClubName!);
        _clubNameLabel.Text = GameState.Instance.SelectedClubName!;
        _roleChipLabel.Text = lineupStatus;
        _clubContextLabel.Text = $"{GameState.Instance.SelectedClubName} | {player.Position} | Age {player.Age}";
        _pageTitleLabel.Text = player.Name;
        _statusLabel.Text = BuildStatusSummary(player);

        _positionValueLabel.Text = player.Position;
        _positionMetaLabel.Text = player.IsStarting ? "Current starter in the active shape." : "Rotation option for the next selection call.";
        _ageValueLabel.Text = player.Age.ToString();
        _ageMetaLabel.Text = DescribeAgeBand(player.Age);
        _formValueLabel.Text = player.Form.ToString();
        _formMetaLabel.Text = DescribeMetric(player.Form, "form");
        _fitnessValueLabel.Text = player.Fitness.ToString();
        _fitnessMetaLabel.Text = DescribeFitness(player.Fitness);

        _identityLabel.Text = $"Identity | {player.Position} | Age {player.Age} | Morale {player.Morale} | Value stays tied to the live squad state.";
        _roleLabel.Text = $"Current role | {lineupStatus} | Matchday contribution follows this selection state.";
        _conditionLabel.Text = $"Condition | Fitness {player.Fitness} | Morale {player.Morale} | Form {player.Form}";
        _pathwayLabel.Text = BuildTrajectorySummary(player);
        _readinessLabel.Text = BuildReadinessSummary(player);
    }

    private void RenderUnavailableState(string title, string status)
    {
        _badgeLabel.Text = "--";
        _clubNameLabel.Text = "Club unavailable";
        _roleChipLabel.Text = "NO PLAYER";
        _clubContextLabel.Text = "Club context unavailable.";
        _pageTitleLabel.Text = title;
        _statusLabel.Text = status;
        _positionValueLabel.Text = "--";
        _positionMetaLabel.Text = "Role unavailable.";
        _ageValueLabel.Text = "--";
        _ageMetaLabel.Text = "Age band unavailable.";
        _formValueLabel.Text = "--";
        _formMetaLabel.Text = "Form trend unavailable.";
        _fitnessValueLabel.Text = "--";
        _fitnessMetaLabel.Text = "Availability unavailable.";
        _identityLabel.Text = "Identity unavailable.";
        _roleLabel.Text = "Role unavailable.";
        _conditionLabel.Text = "Condition unavailable.";
        _pathwayLabel.Text = "Trajectory unavailable.";
        _readinessLabel.Text = "Readiness unavailable.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(SquadScreenScenePath);
    }

    private static string BuildTrajectorySummary(GameState.SquadPlayer player)
    {
        if (player.Age <= 21)
        {
            return "Trajectory | Early-career player. Minutes and morale will shape the next development step.";
        }

        if (player.Age >= 29)
        {
            return "Trajectory | Senior squad piece. Short-term reliability matters as much as long-term planning.";
        }

        return "Trajectory | Prime-cycle squad player. Current-season usage should drive immediate output.";
    }

    private static string BuildReadinessSummary(GameState.SquadPlayer player)
    {
        if (player.Fitness >= 85 && player.Morale >= 65)
        {
            return "Match readiness | Green. Ready for heavy minutes in the next fixture.";
        }

        if (player.Fitness >= 72 && player.Morale >= 55)
        {
            return "Match readiness | Stable. Usable now, but workload should stay controlled.";
        }

        return "Match readiness | Watch closely. Fitness or confidence is suppressing selection certainty.";
    }

    private static string BuildStatusSummary(GameState.SquadPlayer player)
    {
        var role = player.IsStarting ? "starting selection" : "bench selection";
        return $"{player.Position} | {role} | Form {player.Form} | Fitness {player.Fitness}";
    }

    private static string DescribeMetric(int value, string metric)
    {
        if (value >= 75)
        {
            return $"Strong {metric} level.";
        }

        if (value >= 60)
        {
            return $"Steady {metric} level.";
        }

        if (value >= 45)
        {
            return $"{metric.FirstCharToUpper()} is fragile.";
        }

        return $"{metric.FirstCharToUpper()} is under strain.";
    }

    private static string DescribeFitness(int fitness)
    {
        if (fitness >= 88)
        {
            return "Fully available for the next match.";
        }

        if (fitness >= 74)
        {
            return "Available with manageable load.";
        }

        if (fitness >= 60)
        {
            return "Usable, but minutes should be controlled.";
        }

        return "Fitness risk is high for immediate selection.";
    }

    private static string DescribeAgeBand(int age)
    {
        if (age <= 21)
        {
            return "Youth-side growth window.";
        }

        if (age >= 29)
        {
            return "Senior-cycle experience band.";
        }

        return "Prime competitive years.";
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
}

internal static class TouchlineStringExtensions
{
    public static string FirstCharToUpper(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return input.Length == 1 ? input.ToUpperInvariant() : $"{char.ToUpperInvariant(input[0])}{input[1..]}";
    }
}
