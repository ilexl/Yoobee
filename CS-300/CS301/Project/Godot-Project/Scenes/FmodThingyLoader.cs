using Godot;
using System;

public partial class FmodThingyLoader : Node3D
{
	[Export] private PackedScene _sceneToSpawn;
	[Export] bool on;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(on)
		{
			AddChild(_sceneToSpawn.Instantiate());
		}
	}
}
