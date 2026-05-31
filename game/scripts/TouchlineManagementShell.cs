using Godot;

/// <summary>
/// Documents stable node names for management screens using the shared shell layout.
/// Rail screens: RootMargin/Shell/RailCard + RootMargin/Shell/MainColumn/MainScroll/...
/// </summary>
public partial class TouchlineManagementShell : Control
{
    public const string MainScrollPath = "RootMargin/Shell/MainColumn/MainScroll";
    public const string ScrollContentPath = "RootMargin/Shell/MainColumn/MainScroll/ScrollContent";
    public const string RailNavPath = "RootMargin/Shell/RailCard/RailPadding/RailContent/NavButtons";
}
