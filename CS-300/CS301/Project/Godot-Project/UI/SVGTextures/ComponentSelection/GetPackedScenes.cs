using ArchitectsInVoid.VesselComponent;
using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class GetPackedScenes : Node
{
    public override void _Ready()
    {
        foreach(var child in GetChildren())
        {
            var c = child as PlaceableComponent;
            c.Hide();
        }
    }

    public List<PackedScene> GetAll()
    {
        List<PackedScene> l = new List<PackedScene>();
        foreach (var child in GetChildren())
        {
            var scene = GD.Load<PackedScene>($"{child.SceneFilePath}");
            l.Add(scene);
        }
        return l;
    }




}
