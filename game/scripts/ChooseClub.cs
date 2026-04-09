using Godot;

public partial class ChooseClub : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private ItemList _clubList = default!;
    private Label _selectionStatusLabel = default!;

    public override void _Ready()
    {
        var summaryLabel = GetNode<Label>("Center/Panel/CareerSummaryLabel");
        _clubList = GetNode<ItemList>("Center/Panel/ClubList");
        _selectionStatusLabel = GetNode<Label>("Center/Panel/SelectionStatusLabel");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            summaryLabel.Text = "Career setup is incomplete.";
            _selectionStatusLabel.Text = "Career setup is incomplete.";
            return;
        }

        if (GameState.Instance.WorldSeed <= 0)
        {
            summaryLabel.Text = "Career world seed context is missing.";
            _selectionStatusLabel.Text = "Career world seed context is missing.";
            return;
        }

        summaryLabel.Text =
            $"Manager: {GameState.Instance.ManagerName} | Seed: {GameState.Instance.WorldSeed} | Pack: {GameState.Instance.CountryPackId}";

        _clubList.Clear();
        foreach (var clubName in GameState.Instance.AvailableClubs)
        {
            _clubList.AddItem(clubName);
        }

        if (_clubList.ItemCount > 0)
        {
            _clubList.Select(0);
            _selectionStatusLabel.Text = "Select a club and confirm to continue.";
        }
        else
        {
            _selectionStatusLabel.Text = "No clubs are available from seeded data.";
        }
    }

    private void OnConfirmSelectionPressed()
    {
        if (GameState.Instance == null)
        {
            _selectionStatusLabel.Text = "GameState singleton is unavailable.";
            return;
        }

        var selectedItems = _clubList.GetSelectedItems();
        if (selectedItems.Length == 0)
        {
            _selectionStatusLabel.Text = "Select a club before confirming.";
            return;
        }

        var selectedIndex = selectedItems[0];
        var selectedClubName = _clubList.GetItemText(selectedIndex);
        GameState.Instance.SelectClub(selectedClubName);
        _selectionStatusLabel.Text = $"Selected club: {selectedClubName}";
        GetTree().ChangeSceneToFile(ClubDashboardScenePath);
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }
}
