using Godot;
using System;

/// <summary>
/// Sound manager for Fmod sounds
/// </summary>
public partial class FmodManager : Node
{

	bool loaded = false; // temp
	[Export] PackedScene _soundTemp; // temp 
	double time = 10; // temp

	/// <summary>
	/// Temporarily uses a timer to load a sound every 10 seconds
	/// </summary>
	public override void _Process(double delta)
	{
		// all of this is temp
        time -= delta;
        if (time < 0)
        {
			loaded = false;
			time = 10;
        }

        if (loaded) return;
		var _s = _soundTemp.Instantiate();
		AddChild(_s);
		loaded = true;

    }
	
	
	
	
}
