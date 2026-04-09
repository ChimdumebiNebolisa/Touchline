using Godot;

public partial class FixturesScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _competitionLabel = default!;
    private Label _seasonLabel = default!;
    private Label _dateLabel = default!;
    private ItemList _fixtureList = default!;
    private Label _nextFixtureLabel = default!;
    private Label _formLabel = default!;
    private Label _statusLabel = default!;

    public override void _Ready()
    {
        _competitionLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/CompetitionLabel");
        _seasonLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/SeasonLabel");
        _dateLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/DateLabel");
        _fixtureList = GetNode<ItemList>("Center/Shell/Padding/Content/BodyRow/TimelineCard/TimelinePadding/TimelineContent/FixtureList");
        _nextFixtureLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/NextFixtureLabel");
        _formLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/FormLabel");
        _statusLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/StatusLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _competitionLabel.Text = "Competition unavailable";
            _seasonLabel.Text = "Season unavailable";
            _dateLabel.Text = "Date unavailable";
            _nextFixtureLabel.Text = "Next fixture unavailable";
            _formLabel.Text = "Form unavailable";
            _statusLabel.Text = "Competition timeline unavailable.";
            return;
        }

        _competitionLabel.Text = GameState.Instance.CompetitionName;
        _seasonLabel.Text = $"Season {GameState.Instance.SeasonLabel}";
        _dateLabel.Text = GameState.Instance.CurrentDateLabel;
        _nextFixtureLabel.Text = GameState.Instance.NextFixtureSummary;
        _formLabel.Text = GameState.Instance.FormSummary;
        _statusLabel.Text = BuildStatusLabel();

        PopulateFixtureList();
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void PopulateFixtureList()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return;
        }

        _fixtureList.Clear();

        foreach (var fixture in GameState.Instance.CompetitionFixtures)
        {
            var marker = fixture.Matchday == GameState.Instance.CurrentMatchday
                ? (fixture.IsComplete ? "[FT]" : "[NEXT]")
                : (fixture.IsComplete ? "[DONE]" : "[UP]");
            var clubTag = fixture.HomeClubName == GameState.Instance.SelectedClubName || fixture.AwayClubName == GameState.Instance.SelectedClubName
                ? "YOU"
                : "LEAGUE";
            var body = fixture.IsComplete
                ? fixture.ResultSummary
                : $"{fixture.HomeClubName} vs {fixture.AwayClubName}";

            _fixtureList.AddItem($"{marker} MD{fixture.Matchday} | {clubTag} | {body}");
        }
    }

    private static string BuildStatusLabel()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return "Competition timeline unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.SelectedClubName)
            {
                return
                    $"{row.ClubName} sit {index + 1} of {GameState.Instance.CompetitionTable.Length} with {row.Points} points from {row.Played} matches.";
            }
        }

        return "Club standing is unavailable.";
    }
}
