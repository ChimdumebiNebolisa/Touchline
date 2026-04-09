using Godot;

public partial class MatchdayScene : Control
{
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";
    private const string LiveMatchScenePath = "res://scenes/LiveMatchScene.tscn";

    private Label _competitionLabel = default!;
    private Label _fixtureLabel = default!;
    private Label _stakesLabel = default!;
    private Label _kickoffContextLabel = default!;
    private Label _lineupLabel = default!;
    private Label _benchLabel = default!;
    private Label _formLabel = default!;
    private Label _pressureLabel = default!;
    private Label _tacticsLabel = default!;
    private Label _opponentFocusLabel = default!;
    private Label _readinessLabel = default!;
    private Label _statusLabel = default!;
    private Button _instantResultButton = default!;
    private Button _startMatchButton = default!;

    public override void _Ready()
    {
        _competitionLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/CompetitionLabel");
        _fixtureLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/FixtureLabel");
        _stakesLabel = GetNode<Label>("Center/Shell/Padding/Content/Header/StakesLabel");
        _kickoffContextLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/KickoffContextLabel");
        _lineupLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/LineupLabel");
        _benchLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/BenchLabel");
        _formLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/FormLabel");
        _pressureLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/EventCard/EventPadding/EventContent/PressureLabel");
        _tacticsLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/PlanCard/PlanPadding/PlanContent/TacticsLabel");
        _opponentFocusLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/PlanCard/PlanPadding/PlanContent/OpponentFocusLabel");
        _readinessLabel = GetNode<Label>("Center/Shell/Padding/Content/BodyRow/PlanCard/PlanPadding/PlanContent/ReadinessLabel");
        _statusLabel = GetNode<Label>("Center/Shell/Padding/Content/StatusLabel");
        _instantResultButton = GetNode<Button>("Center/Shell/Padding/Content/ActionsRow/InstantResultButton");
        _startMatchButton = GetNode<Button>("Center/Shell/Padding/Content/ActionsRow/StartMatchButton");

        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            _competitionLabel.Text = "Competition unavailable";
            _fixtureLabel.Text = "Fixture unavailable";
            _stakesLabel.Text = "Match stakes unavailable.";
            _kickoffContextLabel.Text = "Kickoff context unavailable.";
            _lineupLabel.Text = "Lineup readiness unavailable.";
            _benchLabel.Text = "Bench context unavailable.";
            _formLabel.Text = "Form unavailable.";
            _pressureLabel.Text = "Pressure context unavailable.";
            _tacticsLabel.Text = "Tactics unavailable.";
            _opponentFocusLabel.Text = "Opponent focus unavailable.";
            _readinessLabel.Text = "Readiness unavailable.";
            _statusLabel.Text = "Set up a career and club before entering matchday.";
            _instantResultButton.Disabled = true;
            _startMatchButton.Disabled = true;
            return;
        }

        var startingCount = 0;
        var benchCount = 0;
        var totalFitness = 0;
        foreach (var player in GameState.Instance.SquadPlayers)
        {
            if (player.IsStarting)
            {
                startingCount++;
                totalFitness += player.Fitness;
            }
            else
            {
                benchCount++;
            }
        }

        var averageFitness = startingCount == 0 ? 0 : totalFitness / startingCount;
        var fixtureComplete = IsCurrentFixtureComplete();

        _competitionLabel.Text = GameState.Instance.CompetitionName;
        _fixtureLabel.Text =
            $"{GameState.Instance.SelectedClubName} vs {GameState.Instance.CurrentOpponentName} | Matchday {GameState.Instance.CurrentMatchday}";
        _stakesLabel.Text = BuildStakesLabel();
        _kickoffContextLabel.Text =
            $"{GameState.Instance.CurrentDateLabel} kickoff window. The matchday result will immediately feed the table, form line, and club pressure.";
        _lineupLabel.Text = $"{startingCount} players are marked in the XI with average fitness {averageFitness}.";
        _benchLabel.Text = $"{benchCount} players are on the bench and ready to cover the tactical shell.";
        _formLabel.Text = GameState.Instance.FormSummary;
        _pressureLabel.Text =
            $"Club pulse: morale {GameState.Instance.TeamMorale} | fans {GameState.Instance.FanSentiment} | board {GameState.Instance.BoardConfidence}.";
        _tacticsLabel.Text =
            $"Tactical board: {GameState.Instance.TacticalFormation} | Press {GameState.Instance.PressIntensity} | Tempo {GameState.Instance.Tempo} | Width {GameState.Instance.Width} | Risk {GameState.Instance.Risk}";
        _opponentFocusLabel.Text = BuildOpponentFocusLabel();
        _readinessLabel.Text =
            startingCount >= 11
                ? "Readiness: the XI is complete and the tactical board is locked for kickoff."
                : "Readiness: the squad is short of a full XI, so the live engine will fill from the remaining squad list.";
        _statusLabel.Text = fixtureComplete
            ? "This matchday is already recorded. Review the dashboard, then continue the season instead of replaying the same fixture."
            : "Final check complete. Launch the live match when the lineup, shape, and pressure context all feel coherent.";
        _instantResultButton.Disabled = fixtureComplete;
        _startMatchButton.Disabled = fixtureComplete;
    }

    private void OnStartMatchPressed()
    {
        GameState.Instance?.PrepareCurrentMatchResult(true);
        GetTree().ChangeSceneToFile(LiveMatchScenePath);
    }

    private void OnInstantResultPressed()
    {
        if (GameState.Instance == null)
        {
            return;
        }

        GameState.Instance.PrepareCurrentMatchResult(true);
        GameState.Instance.ResolveCurrentMatchInstantly();
        GetTree().ChangeSceneToFile("res://scenes/PostMatchScene.tscn");
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private static string BuildStakesLabel()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return "Match stakes unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.SelectedClubName)
            {
                return $"{row.ClubName} enter the round in position {index + 1} with {row.Points} points and goal difference {FormatSigned(row.GoalDifference)}.";
            }
        }

        return "Match stakes unavailable.";
    }

    private static string BuildOpponentFocusLabel()
    {
        if (GameState.Instance == null)
        {
            return "Opponent focus unavailable.";
        }

        for (var index = 0; index < GameState.Instance.CompetitionTable.Length; index++)
        {
            var row = GameState.Instance.CompetitionTable[index];
            if (row.ClubName == GameState.Instance.CurrentOpponentName)
            {
                return $"{row.ClubName} arrive in position {index + 1} with {row.Points} points from {row.Played} matches.";
            }
        }

        return $"{GameState.Instance.CurrentOpponentName} arrive with pressure but their table context is unavailable.";
    }

    private static bool IsCurrentFixtureComplete()
    {
        if (GameState.Instance == null || string.IsNullOrWhiteSpace(GameState.Instance.SelectedClubName))
        {
            return false;
        }

        foreach (var fixture in GameState.Instance.CompetitionFixtures)
        {
            if (fixture.Matchday == GameState.Instance.CurrentMatchday &&
                (fixture.HomeClubName == GameState.Instance.SelectedClubName || fixture.AwayClubName == GameState.Instance.SelectedClubName))
            {
                return fixture.IsComplete;
            }
        }

        return false;
    }

    private static string FormatSigned(int value)
    {
        return value >= 0 ? $"+{value}" : value.ToString();
    }
}
