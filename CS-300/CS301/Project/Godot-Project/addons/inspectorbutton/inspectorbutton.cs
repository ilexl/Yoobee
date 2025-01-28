#if TOOLS
using System.Collections.Generic;
using Godot;



[Tool]
public partial class inspectorbutton : EditorPlugin
{
    EditorButtonPlugin editorButtonPlugin;
    public override void _EnterTree()
    {
        editorButtonPlugin = new EditorButtonPlugin();
        AddInspectorPlugin(editorButtonPlugin);
    }

    public override void _ExitTree()
    {
        if (IsInstanceValid(editorButtonPlugin))
        {
            RemoveInspectorPlugin(editorButtonPlugin);
            editorButtonPlugin = null;
        }
    }

    public partial class EditorButtonPlugin : EditorInspectorPlugin
    {
        public override bool _CanHandle(GodotObject @object)
        {
            base._CanHandle(@object);
            bool hasMethod = @object.HasMethod("AddInspectorButtons");
            return hasMethod;
        }

        public override void _ParseBegin(GodotObject @object)
        {
            base._ParseBegin(@object);
            if (!@object.HasMethod("AddInspectorButtons"))
            {
                GD.PushError("Error with inspectorbuttons - not continuing...");
                return;
            }
            Godot.Collections.Array buttonsData = @object.Call("AddInspectorButtons").AsGodotArray();
            foreach(var buttonData in buttonsData)
            {
                string name = buttonData.AsGodotDictionary().GetValueOrDefault("name").AsString();
                GodotObject icon = buttonData.AsGodotDictionary().GetValueOrDefault("icon").AsGodotObject();
                Callable pressed = buttonData.AsGodotDictionary().GetValueOrDefault("pressed").AsCallable();

                if(name == null)
                {
                    GD.PushWarning("AddInspectorButtons(): A button does not have a name key. Defaulting to: \"Button\"");
                    name = "Button";
                }
                if(icon == null || icon is not Texture2D)
                {
                    GD.PushWarning($"AddInspectorButtons(): The button {name} icon is not a texture.");
                    icon = null;
                }

                Button button = new Button();
                button.Text = name;
                if(icon != null)
                {
                    button.Icon = (Texture2D)icon;
                    button.ExpandIcon = true;
                }

                button.Connect(BaseButton.SignalName.ButtonDown, pressed);
                AddCustomControl(button);

            }
        }
    }
}
#endif
