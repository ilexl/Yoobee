using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace ArchitectsInVoid.UI;

/// <summary>
/// Window Manager which manages the windows defined in the array _windows
/// </summary>
[Tool]
public partial class WindowManager : Node
{
    [Export] private bool _startFeature = true; // if true - enables the start feature for managed windows
    [Export] private Window[] _windows;

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game

        if (_startFeature)
        {
            foreach (var window in _windows)
            {
                if (window == null) continue;
                if (window.ShowOnStart)
                    window.Show();
                else
                    window.Hide();
            }
        }

        if(_windows == null || _windows.Length == 0 || _windows[0] == null)
        {
            ManualRefresh();
        }

    }

    public void ManualRefresh()
    {
        List<Window> temp = new List<Window>();
        foreach (var child in GetChildren())
        {
            if (child is Window w)
            {
                temp.Add(w);
            }
        }
        _windows = temp.ToArray();
    }
    
    #region wmFunctions

    // Show window functions (hides all others)

    /// <summary>
    ///     Shows the individual window - Hides all other windows the WM manages
    /// </summary>
    /// <param name="window">The window to show</param>
    public void ShowWindow(Window window)
    {
        if (window == null) GD.PushError("Rebuild is needed for windows to work...");
        foreach (var _window in _windows)
        {
            if (_window == null)
            {
                return;
            }
            if (_window == window)
                _window.Show(); // Shows the required window
            else
                _window.Hide(); // Hides all others
        }
    }

    /// <summary>
    ///     Shows the individual window - Hides all other windows the WM manages
    /// </summary>
    /// <param name="windowIndex">The index in windows to show</param>
    public void ShowWindow(int windowIndex)
    {
        if (windowIndex >= _windows.Length)
            GD.PushError($"Index out of range - {windowIndex} for Windows in WM...");
        else
            ShowWindow(_windows[windowIndex]);
    }

    /// <summary>
    ///     Shows the individual window - Hides all other windows the WM manages
    /// </summary>
    /// <param name="windowName">The transform name of the window to show</param>
    public void ShowWindow(string windowName)
    {
        var found = false;
        foreach (var window in _windows)
            if (window.WGetName() == windowName)
            {
                ShowWindow(window);
                found = true;
            }

        if (!found) GD.PushError($"Window: {windowName} NOT found...");
    }

    // Show window functions (leaves all others as current)

    /// <summary>
    ///     Shows the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="window">The window to show</param>
    public void ShowOnly(Window window)
    {
        if (window == null) GD.PushError("Rebuild is needed for windows to work...");
        foreach (var _window in _windows)
            if (_window == window)
                _window.Show(); // Shows the required window
    }

    /// <summary>
    ///     Shows the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="windowIndex">The index in windows to show</param>
    public void ShowOnly(int windowIndex)
    {
        if (windowIndex >= _windows.Length)
            GD.PushError($"Index out of range - {windowIndex} for Windows in WM...");
        else
            ShowOnly(_windows[windowIndex]);
    }

    /// <summary>
    ///     Shows the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="windowName">The transform name of the window to show</param>
    public void ShowOnly(string windowName)
    {
        var found = false;
        foreach (var window in _windows)
            if (window.WGetName() == windowName)
            {
                ShowOnly(window);
                found = true;
            }

        if (!found) GD.PushError($"Window not found - {windowName}");
    }

    // Hide All Windows Function

    /// <summary>
    ///     Hides all the windows managed by the WM
    /// </summary>
    public void HideAll()
    {
        foreach (var window in _windows)
        {
            if (window == null) GD.PushError("Rebuild is needed for windows to work...");
            window.Hide();
        }
    }

    // Hide Specific Window (Individually Hide)

    /// <summary>
    ///     Hides the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="window">The window to hide</param>
    public void HideOnly(Window window)
    {
        if (window == null) GD.PushError("Rebuild is needed for windows to work...");
        foreach (var thisWindow in _windows)
            if (thisWindow == window)
            {
                if (thisWindow == null) GD.PushError("Rebuild is needed for windows to work...");
                thisWindow.Hide(); // Shows the required window
            }
    }

    /// <summary>
    ///     Hides the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="windowIndex">The index in windows to Hide</param>
    public void HideOnly(int windowIndex)
    {
        if (windowIndex >= _windows.Length)
            GD.PushError($"Index out of range - {windowIndex} for Windows in WM...");
        else
            HideOnly(_windows[windowIndex]);
    }

    /// <summary>
    ///     Hides the individual window - Leaves all other windows the WM manages
    /// </summary>
    /// <param name="windowName">The transform name of the window to Hide</param>
    public void HideOnly(string windowName)
    {
        var found = false;
        foreach (var window in _windows)
            if (window.WGetName() == windowName)
            {
                HideOnly(window);
                found = true;
            }

        if (!found) GD.PushError($"Window not found - {windowName}");
    }

    /// <summary>
    ///     Gets all the windows as an array (readonly)
    /// </summary>
    /// <returns>WM windows</returns>
    public Window[] GetWindows()
    {
        return _windows;
    }

    #endregion

    /// <summary>
    /// Used for inspector buttons plugin
    /// </summary>
    public Array AddInspectorButtons()
    {
        var buttons = new Array();

        var btnRefresh = new Dictionary
            {
                { "name", "Manual Refresh" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(ManualRefresh) }
            };
        buttons.Add(btnRefresh);

        if (_windows == null || _windows.Length == 0) return buttons;

        foreach (var w in _windows)
        {
            if (w == null) continue;
            w.SetWindowManager(this);
            var windowButtonShowOnly = new Dictionary
            {
                { "name", $"Show Only {w.WGetName()}" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(w.wShowOnly) }
            };
            buttons.Add(windowButtonShowOnly);
        }

        return buttons;
    }
}