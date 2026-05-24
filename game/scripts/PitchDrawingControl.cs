using Godot;

public partial class PitchDrawingControl : Control
{
    [Export] public bool ShowStripes { get; set; } = true;

    public override void _Draw()
    {
        var rect = new Rect2(Vector2.Zero, Size);
        DrawRect(rect, TouchlineTheme.PitchGrass, true);

        if (ShowStripes)
        {
            var stripeWidth = Size.X / 8.0f;
            for (var index = 0; index < 8; index += 2)
            {
                DrawRect(new Rect2(index * stripeWidth, 0, stripeWidth, Size.Y), TouchlineTheme.PitchGrassDark, true);
            }
        }

        var marginX = Size.X * 0.055f;
        var marginY = Size.Y * 0.055f;
        var field = new Rect2(marginX, marginY, Size.X - marginX * 2.0f, Size.Y - marginY * 2.0f);
        var lineColor = TouchlineTheme.PitchLine;
        var lineWidth = 2.0f;

        DrawRect(field, lineColor, false, lineWidth);
        DrawLine(new Vector2(field.Position.X, field.GetCenter().Y), new Vector2(field.End.X, field.GetCenter().Y), lineColor, lineWidth);
        DrawArc(field.GetCenter(), Mathf.Min(field.Size.X, field.Size.Y) * 0.105f, 0, Mathf.Tau, 72, lineColor, lineWidth);
        DrawCircle(field.GetCenter(), 3.0f, lineColor);

        DrawPenaltyArea(field, true, lineColor, lineWidth);
        DrawPenaltyArea(field, false, lineColor, lineWidth);
        DrawGoal(field, true, lineColor);
        DrawGoal(field, false, lineColor);
    }

    private void DrawPenaltyArea(Rect2 field, bool top, Color lineColor, float lineWidth)
    {
        var boxWidth = field.Size.X * 0.42f;
        var boxHeight = field.Size.Y * 0.17f;
        var sixWidth = field.Size.X * 0.22f;
        var sixHeight = field.Size.Y * 0.075f;
        var x = field.GetCenter().X - boxWidth / 2.0f;
        var sixX = field.GetCenter().X - sixWidth / 2.0f;
        var y = top ? field.Position.Y : field.End.Y - boxHeight;
        var sixY = top ? field.Position.Y : field.End.Y - sixHeight;

        DrawRect(new Rect2(x, y, boxWidth, boxHeight), lineColor, false, lineWidth);
        DrawRect(new Rect2(sixX, sixY, sixWidth, sixHeight), lineColor, false, lineWidth);
    }

    private void DrawGoal(Rect2 field, bool top, Color lineColor)
    {
        var goalWidth = field.Size.X * 0.16f;
        var goalDepth = Size.Y * 0.025f;
        var x = field.GetCenter().X - goalWidth / 2.0f;
        var y = top ? field.Position.Y - goalDepth : field.End.Y;
        DrawRect(new Rect2(x, y, goalWidth, goalDepth), lineColor, false, 2.0f);
    }
}
