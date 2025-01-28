#if TOOLS
using Godot;
using System;

[Tool]
public partial class scenepreviewextractor : EditorPlugin
{
	
	public static void GetPreview(string scenePath, GodotObject receiver, string receiverFunc, Variant userData)
	{
		EditorInterface.Singleton.GetResourcePreviewer().QueueResourcePreview(scenePath, receiver, receiverFunc, userData);
	}
	
	public override void _Process(double delta)
	{
		if (Engine.IsEditorHint())
		{
			
		}
	}
}
#endif
