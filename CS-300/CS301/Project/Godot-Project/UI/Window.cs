using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.UI;

/// <summary>
/// Window managed by WindowManager
/// <br/>shows and hides a UI layer or "window"
/// </summary>
[Tool]
public partial class Window : Control
{
    [Export] public bool ShowOnStart = false;
    private WindowManager _wm;

    /// <summary>
    /// Sets the window manager
    /// </summary>
    public void SetWindowManager(WindowManager windowManager)
    {
        _wm = windowManager;
    }

    /// <summary>
    /// Shows the window
    /// </summary>
    public void WShow()
    {
        Visible = true;
    }

    /// <summary>
    /// Only shows this window - does nothing else
    /// </summary>
    public void wShowOnly()
    {
        if (_wm == null)
        {
            GD.PushWarning("Unable to use show only. Window manager is not set...");
            return;
        }

        _wm.ShowWindow(this);
    }

    /// <summary>
    /// Hides the window
    /// </summary>
    public void WHide()
    {
        Visible = false;
    }

    /// <summary>
    /// Shows or Hides window
    /// </summary>
    /// <param name="active">determines if window shown</param>
    public void WSetActive(bool active)
    {
        Visible = active;
    }

    /// <summary>
    /// Gets the transforms name from GODOT_EDITOR
    /// </summary>
    /// <returns>(string) transform name of window</returns>
    public string WGetName()
    {
        return Name;
    }

    /// <summary>
    /// Used for inspector buttons plugin
    /// </summary>
    public Array AddInspectorButtons()
    {
        var buttons = new Array();

        var show = new Dictionary
        {
            { "name", "Show Window" },
            { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
            {
                "pressed", Callable.From(Show)
            }
        };
        buttons.Add(show);

        var hide = new Dictionary
        {
            { "name", "Hide Window" },
            { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
            {
                "pressed", Callable.From(Hide)
            }
        };
        buttons.Add(hide);

        return buttons;
    }
}