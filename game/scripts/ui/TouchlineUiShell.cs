using Godot;

public static class TouchlineUiShell
{
    public const int ViewportWidth = 1280;
    public const int ViewportHeight = 720;
    public const int LabelColumnWidth = 140;
    public const int RailWidth = 220;
    public const int TopBarHeight = 52;
    public const int FooterHeight = 44;

    public static void ConfigureViewport(Control root)
    {
        var viewport = root.GetViewport();
        if (viewport == null)
        {
            return;
        }

        if (root.GetWindow() != null)
        {
            root.GetWindow().Size = new Vector2I(ViewportWidth, ViewportHeight);
        }
    }

    public static ScrollContainer? FindMainScroll(Node sceneRoot)
    {
        return sceneRoot.FindChild("MainScroll", true, false) as ScrollContainer
            ?? sceneRoot.FindChild("FormScroll", true, false) as ScrollContainer
            ?? sceneRoot.FindChild("MenuScroll", true, false) as ScrollContainer;
    }

    public static bool SummaryPrecedesScroll(Node mainColumn)
    {
        var summary = mainColumn.GetNodeOrNull("SummaryGrid");
        var scroll = mainColumn.GetNodeOrNull("MainScroll");
        if (summary == null || scroll == null)
        {
            return false;
        }

        return summary.GetIndex() < scroll.GetIndex();
    }

    public static PanelContainer CreateFooterBar(string statusText, string primaryButtonText, out Label statusLabel, out Button primaryButton)
    {
        var footer = new PanelContainer { Name = "FooterBar" };
        footer.CustomMinimumSize = new Vector2(0, FooterHeight);
        TouchlineTheme.ApplyPanelVariant(footer, TouchlineSurfaceVariant.Shell, 18);

        var padding = new MarginContainer();
        padding.AddThemeConstantOverride("margin_left", 16);
        padding.AddThemeConstantOverride("margin_top", 8);
        padding.AddThemeConstantOverride("margin_right", 16);
        padding.AddThemeConstantOverride("margin_bottom", 8);
        footer.AddChild(padding);

        var row = new HBoxContainer();
        row.AddThemeConstantOverride("separation", 12);
        padding.AddChild(row);

        statusLabel = new Label
        {
            Name = "FooterStatusLabel",
            Text = statusText,
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill,
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        TouchlineTheme.ApplyMutedStyle(statusLabel, 14);
        row.AddChild(statusLabel);

        primaryButton = new Button
        {
            Name = "FooterPrimaryButton",
            Text = primaryButtonText,
            CustomMinimumSize = new Vector2(180, 0)
        };
        TouchlineTheme.ApplyButtonVariant(primaryButton, TouchlineButtonVariant.Primary);
        row.AddChild(primaryButton);

        return footer;
    }
}
