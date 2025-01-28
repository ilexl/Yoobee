using ArchitectsInVoid.Player.ComponentCreation;
using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectsInVoid.WorldData;

[Tool]
public partial class VesselData : Node
{
    [Export] PackedScene _vesselBlank;

    public static VesselData _VesselData;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        if (_vesselBlank == null)
        {
            _vesselBlank = (PackedScene)GD.Load("res://Scenes/BlankShip.tscn");
            if (_vesselBlank == null)
            {
                GD.PushError("PlayerData: No Packed Scene found for shipBlank...");
                return;
            }
        }

        if (_VesselData == null) _VesselData = this;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _Load(FileAccess file)
    {
        // get amount of ships
        string json = file.GetVar().AsString();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
        if(json == "NULL") { return; }
        List<Vessel.Data> data = JsonSerializer.Deserialize<List<Vessel.Data>>(json, options);
        foreach(var vesselData in data)
        {
            Vessel vessel = null;
            foreach(var componentData in vesselData.Components)
            {
                // in theory adds each component to the vessel
                Vessel temp = ComponentCreator.Singleton.PlaceFromData(componentData.Position, componentData.Scale, componentData.Basis, componentData.JComponent, vessel); 
                if(temp != null)
                {
                    GD.Print("vessel exists now");
                    vessel = temp;
                }
            }
            if (vessel == null)
            {
                GD.PushError("FAILED TO CREATE VESSEL???");
                continue;
            }
            vessel.RigidBody.Transform = vesselData.Transform;
            
            //vessel.RigidBody.Position = vesselData.Position;
        }
    }

    public void _DiscardLoadPast(FileAccess file)
    {
        // get amount of players
        _ = file.GetVar();
    }

    public void _Save(FileAccess file)
    {
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
        string test = "NULL";
        List <Vessel.Data> data = new List<Vessel.Data>();
        foreach(var child in GetChildren())
        {
            
            if(child is Vessel vessel)
            {
                data.Add(vessel.SaveData);
                
            }
        }

        if(data.Count != 0)
        {
            test = JsonSerializer.Serialize(data, options);
        }
        file.StoreVar(test);
    }

    public Vessel CreateVessel(Vector3 position)
    {
        Vessel newVessel = (Vessel)_vesselBlank.Instantiate();
        newVessel.RigidBody.Position = position;
        AddChild(newVessel);
        return newVessel;
    }

    internal void _NewGame(FileAccess file)
    {
        string data = "NULL";
        file.StoreVar(data);
    }
}