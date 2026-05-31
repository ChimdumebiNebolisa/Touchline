using Godot;

public static class TouchlineUiChip
{
    public static PanelContainer Create(string name, string text, TouchlineSurfaceVariant variant = TouchlineSurfaceVariant.Accent)
    {
        var chip = new PanelContainer { Name = name };
        var padding = new MarginContainer();
        padding.AddThemeConstantOverride("margin_left", 12);
        padding.AddThemeConstantOverride("margin_top", 6);
        padding.AddThemeConstantOverride("margin_right", 12);
        padding.AddThemeConstantOverride("margin_bottom", 6);
        chip.AddChild(padding);

        var label = new Label
        {
            Name = $"{name}Label",
            Text = text,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        TouchlineTheme.ApplyMutedStyle(label, 13);
        padding.AddChild(label);
        TouchlineTheme.ApplyPanelVariant(chip, variant, 999);
        return chip;
    }
}
