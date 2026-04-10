using Godot;

public enum TouchlineSurfaceVariant
{
    Shell,
    Rail,
    Card,
    Muted,
    Accent,
    Positive
}

public enum TouchlineButtonVariant
{
    Primary,
    Secondary,
    Tertiary
}

public partial class TouchlineTheme : Node
{
    private static Theme? _cachedTheme;

    public static Color BackgroundBase => new(0.035f, 0.055f, 0.071f);
    public static Color BackgroundBand => new(0.051f, 0.082f, 0.114f);
    public static Color SurfaceBase => new(0.086f, 0.118f, 0.161f);
    public static Color SurfaceRaised => new(0.106f, 0.145f, 0.196f);
    public static Color SurfaceMuted => new(0.064f, 0.094f, 0.129f);
    public static Color RailSurface => new(0.057f, 0.085f, 0.117f);
    public static Color AccentSurface => new(0.089f, 0.173f, 0.247f);
    public static Color PositiveSurface => new(0.074f, 0.145f, 0.118f);
    public static Color AccentBlue => new(0.259f, 0.561f, 0.867f);
    public static Color AccentBlueHover => new(0.341f, 0.627f, 0.902f);
    public static Color AccentBluePressed => new(0.212f, 0.463f, 0.741f);
    public static Color PositiveGreen => new(0.235f, 0.671f, 0.439f);
    public static Color PositiveGreenHover => new(0.298f, 0.737f, 0.498f);
    public static Color PositiveGreenPressed => new(0.188f, 0.549f, 0.357f);
    public static Color BorderSoft => new(0.239f, 0.333f, 0.431f);
    public static Color BorderAccent => new(0.329f, 0.604f, 0.898f);
    public static Color BorderPositive => new(0.267f, 0.639f, 0.443f);
    public static Color TextPrimary => new(0.929f, 0.945f, 0.965f);
    public static Color TextMuted => new(0.655f, 0.725f, 0.800f);
    public static Color TextQuiet => new(0.525f, 0.600f, 0.686f);

    public override void _EnterTree()
    {
        if (GetTree().Root != null)
        {
            GetTree().Root.Theme = BuildTheme();
        }

        RenderingServer.SetDefaultClearColor(BackgroundBase);
    }

    public static void ApplyPanelVariant(Control control, TouchlineSurfaceVariant variant, int radius = 20)
    {
        control.AddThemeStyleboxOverride("panel", CreatePanelStyle(variant, radius));
    }

    public static void ApplyButtonVariant(Button button, TouchlineButtonVariant variant)
    {
        var palette = ResolveButtonPalette(variant);
        button.AddThemeColorOverride("font_color", TextPrimary);
        button.AddThemeColorOverride("font_hover_color", TextPrimary);
        button.AddThemeColorOverride("font_pressed_color", TextPrimary);
        button.AddThemeColorOverride("font_disabled_color", TextQuiet);
        button.AddThemeFontSizeOverride("font_size", 17);
        button.AddThemeStyleboxOverride("normal", CreateButtonStyle(palette.normalBackground, palette.normalBorder));
        button.AddThemeStyleboxOverride("hover", CreateButtonStyle(palette.hoverBackground, palette.hoverBorder));
        button.AddThemeStyleboxOverride("pressed", CreateButtonStyle(palette.pressedBackground, palette.pressedBorder));
        button.AddThemeStyleboxOverride("focus", CreateButtonStyle(palette.hoverBackground, BorderAccent));
        button.AddThemeStyleboxOverride("disabled", CreateButtonStyle(SurfaceMuted, BorderSoft));
    }

    public static void ApplyEyebrowStyle(Label label)
    {
        label.AddThemeColorOverride("font_color", TextQuiet);
        label.AddThemeFontSizeOverride("font_size", 13);
    }

    public static void ApplyTitleStyle(Label label, int size)
    {
        label.AddThemeColorOverride("font_color", TextPrimary);
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public static void ApplyMutedStyle(Label label, int size = 15)
    {
        label.AddThemeColorOverride("font_color", TextMuted);
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public static void ApplyValueStyle(Label label, int size = 28)
    {
        label.AddThemeColorOverride("font_color", TextPrimary);
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public static void ApplyPositiveValueStyle(Label label, int size = 28)
    {
        label.AddThemeColorOverride("font_color", PositiveGreenHover);
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public static void ApplyAccentValueStyle(Label label, int size = 28)
    {
        label.AddThemeColorOverride("font_color", AccentBlueHover);
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public static StyleBoxFlat CreatePanelStyle(TouchlineSurfaceVariant variant, int radius = 18)
    {
        var (background, border) = ResolveSurfacePalette(variant);
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            CornerRadiusTopLeft = radius,
            CornerRadiusTopRight = radius,
            CornerRadiusBottomRight = radius,
            CornerRadiusBottomLeft = radius,
            ContentMarginLeft = 20,
            ContentMarginTop = 18,
            ContentMarginRight = 20,
            ContentMarginBottom = 18
        };
    }

    private static Theme BuildTheme()
    {
        if (_cachedTheme != null)
        {
            return _cachedTheme;
        }

        var theme = new Theme();

        theme.SetColor("font_color", "Label", TextPrimary);
        theme.SetColor("font_shadow_color", "Label", new Color(0f, 0f, 0f, 0.3f));
        theme.SetConstant("shadow_offset_x", "Label", 0);
        theme.SetConstant("shadow_offset_y", "Label", 1);
        theme.SetFontSize("font_size", "Label", 17);

        theme.SetColor("font_color", "Button", TextPrimary);
        theme.SetColor("font_hover_color", "Button", TextPrimary);
        theme.SetColor("font_pressed_color", "Button", TextPrimary);
        theme.SetColor("font_disabled_color", "Button", TextQuiet);
        theme.SetFontSize("font_size", "Button", 17);
        theme.SetStylebox("normal", "Button", CreateButtonStyle(SurfaceRaised, BorderSoft));
        theme.SetStylebox("hover", "Button", CreateButtonStyle(new Color(0.136f, 0.186f, 0.247f), BorderAccent));
        theme.SetStylebox("pressed", "Button", CreateButtonStyle(SurfaceMuted, BorderSoft));
        theme.SetStylebox("focus", "Button", CreateButtonStyle(SurfaceRaised, BorderAccent));
        theme.SetStylebox("disabled", "Button", CreateButtonStyle(SurfaceMuted, BorderSoft));

        theme.SetColor("font_color", "LineEdit", TextPrimary);
        theme.SetColor("font_placeholder_color", "LineEdit", TextQuiet);
        theme.SetFontSize("font_size", "LineEdit", 17);
        theme.SetStylebox("normal", "LineEdit", CreateInputStyle(SurfaceBase, BorderSoft));
        theme.SetStylebox("focus", "LineEdit", CreateInputStyle(SurfaceBase, BorderAccent));
        theme.SetStylebox("read_only", "LineEdit", CreateInputStyle(SurfaceMuted, BorderSoft));

        theme.SetColor("font_color", "OptionButton", TextPrimary);
        theme.SetFontSize("font_size", "OptionButton", 17);

        theme.SetColor("font_color", "ItemList", TextPrimary);
        theme.SetColor("font_selected_color", "ItemList", TextPrimary);
        theme.SetColor("guide_color", "ItemList", BorderSoft);
        theme.SetFontSize("font_size", "ItemList", 16);
        theme.SetStylebox("panel", "ItemList", CreatePanelStyle(TouchlineSurfaceVariant.Card, 16));
        theme.SetStylebox("selected", "ItemList", CreateSelectionStyle(PositiveGreen));
        theme.SetStylebox("cursor", "ItemList", CreateSelectionStyle(AccentBlue));

        theme.SetStylebox("panel", "PanelContainer", CreatePanelStyle(TouchlineSurfaceVariant.Card));

        theme.SetConstant("h_separation", "HBoxContainer", 16);
        theme.SetConstant("v_separation", "VBoxContainer", 12);

        _cachedTheme = theme;
        return theme;
    }

    private static StyleBoxFlat CreateButtonStyle(Color background, Color border)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            CornerRadiusTopLeft = 12,
            CornerRadiusTopRight = 12,
            CornerRadiusBottomRight = 12,
            CornerRadiusBottomLeft = 12,
            ContentMarginLeft = 18,
            ContentMarginTop = 12,
            ContentMarginRight = 18,
            ContentMarginBottom = 12
        };
    }

    private static StyleBoxFlat CreateInputStyle(Color background, Color border)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 1,
            BorderWidthTop = 1,
            BorderWidthRight = 1,
            BorderWidthBottom = 1,
            CornerRadiusTopLeft = 10,
            CornerRadiusTopRight = 10,
            CornerRadiusBottomRight = 10,
            CornerRadiusBottomLeft = 10,
            ContentMarginLeft = 14,
            ContentMarginTop = 10,
            ContentMarginRight = 14,
            ContentMarginBottom = 10
        };
    }

    private static StyleBoxFlat CreateSelectionStyle(Color background)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            CornerRadiusTopLeft = 8,
            CornerRadiusTopRight = 8,
            CornerRadiusBottomRight = 8,
            CornerRadiusBottomLeft = 8,
            ContentMarginLeft = 6,
            ContentMarginTop = 4,
            ContentMarginRight = 6,
            ContentMarginBottom = 4
        };
    }

    private static (Color background, Color border) ResolveSurfacePalette(TouchlineSurfaceVariant variant)
    {
        return variant switch
        {
            TouchlineSurfaceVariant.Shell => (SurfaceBase, BorderSoft),
            TouchlineSurfaceVariant.Rail => (RailSurface, BorderSoft),
            TouchlineSurfaceVariant.Muted => (SurfaceMuted, BorderSoft),
            TouchlineSurfaceVariant.Accent => (AccentSurface, BorderAccent),
            TouchlineSurfaceVariant.Positive => (PositiveSurface, BorderPositive),
            _ => (SurfaceRaised, BorderSoft)
        };
    }

    private static (Color normalBackground, Color normalBorder, Color hoverBackground, Color hoverBorder, Color pressedBackground, Color pressedBorder) ResolveButtonPalette(TouchlineButtonVariant variant)
    {
        return variant switch
        {
            TouchlineButtonVariant.Primary => (AccentBlue, BorderAccent, AccentBlueHover, BorderAccent, AccentBluePressed, BorderAccent),
            TouchlineButtonVariant.Tertiary => (new Color(0f, 0f, 0f, 0f), BorderSoft, SurfaceMuted, BorderSoft, SurfaceMuted, BorderSoft),
            _ => (SurfaceRaised, BorderSoft, new Color(0.136f, 0.186f, 0.247f), BorderAccent, SurfaceMuted, BorderSoft)
        };
    }
}
