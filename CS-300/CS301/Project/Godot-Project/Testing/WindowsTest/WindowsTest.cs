using ArchitectsInVoid.UI;
using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.Testing.WindowsTest;

/// <summary>
/// Tests the functionality of Window and WindowManager
/// </summary>
[Tool]
public partial class WindowsTest : CanvasLayer
{
    private Node _win1, _win2, _win3;

    private WindowManager _wm;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _wm = (WindowManager)GetNode("WindowManager");
        if (_wm != null)
        {
            _win1 = GetNode(GetPath() + "/WindowManager/Window1");
            _win2 = GetNode(GetPath() + "/WindowManager/Window2");
            _win3 = GetNode(GetPath() + "/WindowManager/Window3");

            var but1 = _win1.GetNode("Button");
            var but2 = _win2.GetNode("Button");
            var but3 = _win3.GetNode("Button");

            but1.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Button1));
            but2.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Button2));
            but3.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Button3));
        }
        else
        {
            GD.PushError("No window manager found");
        }
    }

    private void Button1()
    {
        _wm.ShowWindow("Window2");
    }

    private void Button2()
    {
        _wm.ShowWindow("Window3");
    }

    private void Button3()
    {
        _wm.ShowWindow("Window1");
    }

    public Array AddInspectorButtons()
    {
        var buttons = new Array();

        var wt = new Dictionary
        {
            { "name", "Get Windows (Children)" },
            { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
            { "pressed", Callable.From(_Ready) }
        };
        buttons.Add(wt);

        return buttons;
    }
}