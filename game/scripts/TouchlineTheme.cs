using Godot;

public partial class TouchlineTheme : Node
{
    private static Theme? _cachedTheme;

    public override void _EnterTree()
    {
        if (GetTree().Root != null)
        {
            GetTree().Root.Theme = BuildTheme();
        }

        RenderingServer.SetDefaultClearColor(new Color(0.047f, 0.071f, 0.094f));
    }

    private static Theme BuildTheme()
    {
        if (_cachedTheme != null)
        {
            return _cachedTheme;
        }

        var theme = new Theme();

        var textPrimary = new Color(0.929f, 0.945f, 0.965f);
        var textMuted = new Color(0.671f, 0.733f, 0.804f);
        var surface = new Color(0.086f, 0.118f, 0.161f);
        var surfaceRaised = new Color(0.122f, 0.165f, 0.216f);
        var surfacePressed = new Color(0.063f, 0.094f, 0.133f);
        var accent = new Color(0.204f, 0.663f, 0.443f);
        var accentHover = new Color(0.255f, 0.753f, 0.518f);
        var accentPressed = new Color(0.157f, 0.529f, 0.349f);
        var border = new Color(0.259f, 0.361f, 0.463f);

        theme.SetColor("font_color", "Label", textPrimary);
        theme.SetColor("font_shadow_color", "Label", new Color(0f, 0f, 0f, 0.35f));
        theme.SetConstant("shadow_offset_x", "Label", 0);
        theme.SetConstant("shadow_offset_y", "Label", 1);
        theme.SetFontSize("font_size", "Label", 18);

        theme.SetColor("font_color", "Button", textPrimary);
        theme.SetColor("font_hover_color", "Button", textPrimary);
        theme.SetColor("font_pressed_color", "Button", textPrimary);
        theme.SetColor("font_disabled_color", "Button", textMuted);
        theme.SetFontSize("font_size", "Button", 18);
        theme.SetStylebox("normal", "Button", BuildButtonStyle(surfaceRaised, border));
        theme.SetStylebox("hover", "Button", BuildButtonStyle(accentHover, border));
        theme.SetStylebox("pressed", "Button", BuildButtonStyle(accentPressed, border));
        theme.SetStylebox("focus", "Button", BuildButtonStyle(accent, accentHover));
        theme.SetStylebox("disabled", "Button", BuildButtonStyle(surfacePressed, border));

        theme.SetColor("font_color", "LineEdit", textPrimary);
        theme.SetColor("font_placeholder_color", "LineEdit", textMuted);
        theme.SetFontSize("font_size", "LineEdit", 18);
        theme.SetStylebox("normal", "LineEdit", BuildInputStyle(surface, border));
        theme.SetStylebox("focus", "LineEdit", BuildInputStyle(surface, accentHover));
        theme.SetStylebox("read_only", "LineEdit", BuildInputStyle(surfacePressed, border));

        theme.SetColor("font_color", "OptionButton", textPrimary);
        theme.SetFontSize("font_size", "OptionButton", 18);

        theme.SetColor("font_color", "ItemList", textPrimary);
        theme.SetColor("font_selected_color", "ItemList", textPrimary);
        theme.SetColor("guide_color", "ItemList", border);
        theme.SetFontSize("font_size", "ItemList", 17);
        theme.SetStylebox("panel", "ItemList", BuildPanelStyle(surface, border));
        theme.SetStylebox("selected", "ItemList", BuildSelectionStyle(accent));
        theme.SetStylebox("cursor", "ItemList", BuildSelectionStyle(accentHover));

        theme.SetStylebox("panel", "PanelContainer", BuildPanelStyle(surface, border));

        theme.SetConstant("h_separation", "HBoxContainer", 16);
        theme.SetConstant("v_separation", "VBoxContainer", 12);

        _cachedTheme = theme;
        return theme;
    }

    private static StyleBoxFlat BuildButtonStyle(Color background, Color border)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            CornerRadiusTopLeft = 10,
            CornerRadiusTopRight = 10,
            CornerRadiusBottomRight = 10,
            CornerRadiusBottomLeft = 10,
            ContentMarginLeft = 18,
            ContentMarginTop = 12,
            ContentMarginRight = 18,
            ContentMarginBottom = 12
        };
    }

    private static StyleBoxFlat BuildInputStyle(Color background, Color border)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            CornerRadiusTopLeft = 8,
            CornerRadiusTopRight = 8,
            CornerRadiusBottomRight = 8,
            CornerRadiusBottomLeft = 8,
            ContentMarginLeft = 14,
            ContentMarginTop = 10,
            ContentMarginRight = 14,
            ContentMarginBottom = 10
        };
    }

    private static StyleBoxFlat BuildPanelStyle(Color background, Color border)
    {
        return new StyleBoxFlat
        {
            BgColor = background,
            BorderColor = border,
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            CornerRadiusTopLeft = 18,
            CornerRadiusTopRight = 18,
            CornerRadiusBottomRight = 18,
            CornerRadiusBottomLeft = 18,
            ContentMarginLeft = 24,
            ContentMarginTop = 24,
            ContentMarginRight = 24,
            ContentMarginBottom = 24
        };
    }

    private static StyleBoxFlat BuildSelectionStyle(Color background)
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
}
