using Godot;

public static class TouchlineUiSection
{
    public static VBoxContainer Create(string sectionName, string title, out Label bodyLabel)
    {
        var section = new VBoxContainer
        {
            Name = sectionName,
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill
        };
        section.AddThemeConstantOverride("separation", 10);

        var heading = new Label { Name = $"{sectionName}Heading", Text = title };
        TouchlineTheme.ApplyEyebrowStyle(heading);
        section.AddChild(heading);

        bodyLabel = new Label
        {
            Name = $"{sectionName}Body",
            AutowrapMode = TextServer.AutowrapMode.WordSmart,
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyMutedStyle(bodyLabel, 14);
        section.AddChild(bodyLabel);

        return section;
    }
}
