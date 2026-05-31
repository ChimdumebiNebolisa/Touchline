using Godot;

public sealed class TouchlineUiCardRefs
{
    public PanelContainer Root { get; init; } = default!;
    public Label Eyebrow { get; init; } = default!;
    public Label Value { get; init; } = default!;
    public Label Meta { get; init; } = default!;
}

public static class TouchlineUiCard
{
    public static TouchlineUiCardRefs Create(
        string cardName,
        string eyebrow,
        string value = "--",
        string meta = "",
        TouchlineSurfaceVariant variant = TouchlineSurfaceVariant.Card,
        int radius = 20)
    {
        var card = new PanelContainer { Name = cardName };
        var padding = new MarginContainer { Name = "CardPadding" };
        padding.AddThemeConstantOverride("margin_left", 18);
        padding.AddThemeConstantOverride("margin_top", 16);
        padding.AddThemeConstantOverride("margin_right", 18);
        padding.AddThemeConstantOverride("margin_bottom", 16);
        card.AddChild(padding);

        var content = new VBoxContainer { Name = "CardContent" };
        content.AddThemeConstantOverride("separation", 8);
        padding.AddChild(content);

        var eyebrowLabel = new Label { Name = "CardEyebrow", Text = eyebrow };
        TouchlineTheme.ApplyMutedStyle(eyebrowLabel, 13);
        content.AddChild(eyebrowLabel);

        var valueLabel = new Label { Name = "CardValueLabel", Text = value };
        TouchlineTheme.ApplyValueStyle(valueLabel, 28);
        content.AddChild(valueLabel);

        var metaLabel = new Label
        {
            Name = "CardMetaLabel",
            Text = meta,
            AutowrapMode = TextServer.AutowrapMode.WordSmart
        };
        TouchlineTheme.ApplyMutedStyle(metaLabel, 14);
        content.AddChild(metaLabel);

        TouchlineTheme.ApplyPanelVariant(card, variant, radius);
        return new TouchlineUiCardRefs
        {
            Root = card,
            Eyebrow = eyebrowLabel,
            Value = valueLabel,
            Meta = metaLabel
        };
    }
}
