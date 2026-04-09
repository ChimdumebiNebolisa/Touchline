using Godot;

public partial class StandingsScreen : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private Label _competitionLabel = default!;
    private Label _tableContextLabel = default!;
    private ItemList _tableList = default!;
    private Label _clubSummaryLabel = default!;
    private Label _formLabel = default!;
    private Label _nextFixtureLabel = default!;

    public override void _Ready()
    {
        _competitionLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/CompetitionLabel");
        _tableContextLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/TableContextLabel");
        _tableList = GetNode<ItemList>("Center/Shell/Padding/Content/BodyRow/TableCard/TablePadding/TableContent/TableList");
        _clubSummaryLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/ClubSummaryLabel");
        _formLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/FormLabel");
        _nextFixtureLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/ContextCard/ContextPadding/ContextContent/NextFixtureLabel");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _competitionLabel.Text = "Competition unavailable";
            _tableContextLabel.Text = "Table context unavailable.";
            _clubSummaryLabel.Text = "Club summary unavailable.";
            _formLabel.Text = "Form unavailable.";
            _nextFixtureLabel.Text = "Next fixture unavailable.";
            return;
        }

        _competitionLabel.Text = GameState.Instance.CompetitionName;
        _tableContextLabel.Text =
            $"Table update through Matchday {GameState.Instance.CurrentMatchday}. Goal difference separates clubs level on points.";
        _clubSummaryLabel.Text = BuildClubSummary();
        _formLabel.Text = GameState.Instance.FormSummary;
        _nextFixtureLabel.Text = GameState.Instance.NextFixtureSummary;

        PopulateTable();
    }

    private void PopulateTable()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return;
        }

        _tableList.Clear();

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            var marker = row.ClubName == GameState.Instance.SelectedClubName ? ">" : " ";
            var line =
                $"{index + 1}. {marker} {row.ClubName.PadRight(20)} P{row.Played} W{row.Won} D{row.Drawn} L{row.Lost} GD{FormatSigned(row.GoalDifference)} Pts{row.Points}";
            _tableList.AddItem(line);
        }
    }

    private static string BuildClubSummary()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return "Club summary unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.SelectedClubName)
            {
                return
                    $"{row.ClubName} are {index + 1} of {GameState.Instance.CompetitionTable.Length} with {row.Points} points, {row.GoalsFor} scored, and {row.GoalDifference} goal difference.";
            }
        }

        return "Club summary unavailable.";
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }
}
