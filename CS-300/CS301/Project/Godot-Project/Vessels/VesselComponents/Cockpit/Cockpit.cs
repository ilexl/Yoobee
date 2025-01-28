using System;
using ArchitectsInVoid.VesselComponent;
using Godot;

namespace ArchitectsInVoid.Vessels.VesselComponents.Cockpit;

[Tool]
public partial class Cockpit : PlaceableComponent
{
	public override PlaceableComponentType ComponentType { get; set; } = PlaceableComponentType.FixedScale;
    public override Component Type { get; set; } = Component.Cockpit;
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
