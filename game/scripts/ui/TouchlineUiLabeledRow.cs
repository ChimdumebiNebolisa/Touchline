using Godot;

public static class TouchlineUiLabeledRow
{
    public static HBoxContainer Create(string labelText, string valueName, string valueText = "—")
    {
        var row = new HBoxContainer { Name = valueName.Replace("ValueLabel", "Row") };
        row.AddThemeConstantOverride("separation", 12);

        var label = new Label
        {
            Name = valueName.Replace("Value", "Label").Replace("LabelLabel", "Label"),
            Text = labelText,
            CustomMinimumSize = new Vector2(TouchlineUiShell.LabelColumnWidth, 0)
        };
        TouchlineTheme.ApplyMutedStyle(label, 13);
        row.AddChild(label);

        var value = new Label
        {
            Name = valueName,
            Text = valueText,
            AutowrapMode = TextServer.AutowrapMode.WordSmart,
            SizeFlagsHorizontal = Godot.Control.SizeFlags.ExpandFill
        };
        TouchlineTheme.ApplyValueStyle(value, 16);
        row.AddChild(value);

        return row;
    }
}
