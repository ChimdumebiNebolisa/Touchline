using Godot;

public sealed class TouchlineUiScrollPanelRefs
{
    public ScrollContainer Scroll { get; init; } = default!;
    public VBoxContainer Content { get; init; } = default!;
}

public static class TouchlineUiScrollPanel
{
    public static TouchlineUiScrollPanelRefs Create(string scrollName = "MainScroll")
    {
        var scroll = new ScrollContainer
        {
            Name = scrollName,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill,
            HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
            VerticalScrollMode = ScrollContainer.ScrollMode.Auto
        };

        var content = new VBoxContainer
        {
            Name = "ScrollContent",
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill
        };
        content.AddThemeConstantOverride("separation", 14);
        scroll.AddChild(content);

        return new TouchlineUiScrollPanelRefs { Scroll = scroll, Content = content };
    }
}
