using ArchitectsInVoid.JsonDataConversion;
using ArchitectsInVoid.VesselComponent;
using ArchitectsInVoid.WorldData;
using Godot;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ArchitectsInVoid;
/// <summary>
/// A class for containing information on a vessel and all PlaceableComponents attached to it.
/// </summary>
public partial class Vessel : Node
{
    [Export] public RigidBody3D RigidBody;
    [Export] public Node ComponentData;

    public class Data
    {
        [JsonInclude] public JTransform Transform { get; set; }
        // [JsonInclude] public JVector3 Position { get; set; }
        [JsonInclude] public List<PlaceableComponent.Data> Components { get; set; }
        
    }

    private Data _saveData;
    public Data SaveData
    {
        get
        {
            SetData();
            return _saveData;
        }
    }

    private void SetData()
    {
        _saveData = new Data();
        _saveData.Transform = RigidBody.Transform;
        //_saveData.Position = RigidBody.Position;
        _saveData.Components = new List<PlaceableComponent.Data>();

        foreach(var child in GetChild(0).GetChildren())
        {
            if(child is PlaceableComponent component)
            {
                _saveData.Components.Add(component.SaveData);
            }
        }

    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
    }
    
}