using Godot;

namespace ArchitectsInVoid.VesselComponent.Armour;

/// <summary>
/// Scalable basic box to represent armour. Should be replaced with voxels at a future time.
/// </summary>
[Tool]
public partial class Armour : PlaceableComponent
{
    public override PlaceableComponentType ComponentType { get; set; } = PlaceableComponentType.DynamicScale;
    public override Component Type { get; set; } = Component.Armour;
    public override void _Ready()
    {
    }
    

    /*public override void Place(Vector3 position, Vector3 scale)
    {
        Shape.Scale = scale;
        Shape.Position = position;
        _mesh.Scale = scale;
        _mesh.Position = position;
        _rigidBody.Mass = scale.LengthSquared() * _density;
        GD.Print("Mass: " + _rigidBody.Mass / 1000 + " tonnes");
    }*/
}