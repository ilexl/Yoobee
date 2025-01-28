using Godot;



[Tool]
public partial class TestButton : Node
{
    public void Test()
    {
        GD.Print("This is a successful test!");
    }

    public Godot.Collections.Array AddInspectorButtons()
    {
        Godot.Collections.Array buttons = new Godot.Collections.Array();
        Godot.Collections.Dictionary test = new Godot.Collections.Dictionary
        {
            { "name", "Test Button" },
            { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
            { "pressed", Callable.From(Test)
            }
        };
        buttons.Add(test);

        return buttons;
    }
}