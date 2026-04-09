using Godot;

public partial class ChooseClub : Control
{
    private const string CareerSetupScenePath = "res://scenes/CareerSetup.tscn";
    private const string ClubDashboardScenePath = "res://scenes/ClubDashboard.tscn";

    private ItemList _clubList = default!;
    private Label _selectionStatusLabel = default!;
    private Label _identityLabel = default!;
    private Label _expectationLabel = default!;
    private Label _openingFixtureLabel = default!;

    public override void _Ready()
    {
        var summaryLabel = GetNode<Label>("Center/Panel/Padding/Content/CareerSummaryLabel");
        _clubList = GetNode<ItemList>("Center/Panel/Padding/Content/ClubList");
        _selectionStatusLabel = GetNode<Label>("Center/Panel/Padding/Content/SelectionStatusLabel");
        _identityLabel = GetNode<Label>("Center/Panel/Padding/Content/PreviewCard/PreviewPadding/PreviewContent/IdentityLabel");
        _expectationLabel = GetNode<Label>("Center/Panel/Padding/Content/PreviewCard/PreviewPadding/PreviewContent/ExpectationLabel");
        _openingFixtureLabel = GetNode<Label>("Center/Panel/Padding/Content/PreviewCard/PreviewPadding/PreviewContent/OpeningFixtureLabel");

        if (GameState.Instance == null || !GameState.Instance.CareerInitialized)
        {
            summaryLabel.Text = "Career setup is incomplete.";
            _selectionStatusLabel.Text = "Career setup is incomplete.";
            RenderFallbackPreview();
            return;
        }

        if (GameState.Instance.WorldSeed <= 0)
        {
            summaryLabel.Text = "Career world seed context is missing.";
            _selectionStatusLabel.Text = "Career world seed context is missing.";
            RenderFallbackPreview();
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
            RenderPreviewByIndex(0);
        }
        else
        {
            _selectionStatusLabel.Text = "No clubs are available from seeded data.";
            RenderFallbackPreview();
        }
    }

    private void OnClubSelected(long index)
    {
        RenderPreviewByIndex((int)index);
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

    private void RenderPreviewByIndex(int index)
    {
        if (GameState.Instance == null || index < 0 || index >= _clubList.ItemCount)
        {
            RenderFallbackPreview();
            return;
        }

        var preview = GameState.Instance.GetClubPreview(_clubList.GetItemText(index));
        _identityLabel.Text = $"Identity: {preview.IdentitySummary}";
        _expectationLabel.Text = $"Expectation: {preview.ExpectationSummary}";
        _openingFixtureLabel.Text = preview.OpeningFixtureSummary;
    }

    private void RenderFallbackPreview()
    {
        _identityLabel.Text = "Identity: club context unavailable.";
        _expectationLabel.Text = "Expectation: unavailable.";
        _openingFixtureLabel.Text = "Opening fixture unavailable.";
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile(CareerSetupScenePath);
    }
}
