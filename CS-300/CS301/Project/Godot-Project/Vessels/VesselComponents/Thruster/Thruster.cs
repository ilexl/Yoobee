using System;
using ArchitectsInVoid.VesselComponent;
using Godot;

namespace ArchitectsInVoid.Vessels.VesselComponents.Thruster;

/// <summary>
/// A component that can be scaled and has stats determined by its size and width to length to height ratio.
/// Longer thrusters have better thrust to weight/thrust to volume and spool times but worse resource efficiency.
/// Too extreme at the moment, it should be toned down.
/// </summary>
[Tool]
public partial class Thruster : PlaceableComponent
{

    [Export] private double _previewDimensions = 1.0;
    [Export] private double _engineCornerRadius = 0.5;
    [Export] private double _finSpacing = 0.2;
    [Export] private double _finWidth = 0.05;
   
    
    [Export] private double _nozzleLength = 1.5;

    [Export(PropertyHint.Range, "0,20,")]
    private Vector3 _previewScale
    {
        get
        {
            return new Vector3(_width, _height, _length);
        }
        set
        {
            _width = value.X;
            _height = value.Y;
            _length = value.Z;
            GenerateThrusterPreview();
        }
    }
    
    
    
    private double _width = 5;
    private double _height = 5;
    private double _length = 5;
    
    public double Efficiency;
    public double MaterialUse;


    public double SurfaceArea;

    public double Thrust;
    public double ThrustPerVolume;
    public double ThrustSpoolTime;
    public double Volume;

    private Node3D _thrusterContainerNode;

    public override PlaceableComponentType ComponentType { get; set; } = PlaceableComponentType.DynamicScale;
    public override Component Type { get; set; } = Component.Thruster;
    
    
    private void GenerateThrusterPreview()
    {
        _thrusterContainerNode?.QueueFree();
        _thrusterContainerNode = new Node3D();
        CallDeferred("add_child", _thrusterContainerNode);
            
        GenerateThruster();
    }

    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override PlaceableComponentResult Place(Vector3 position, Vector3 scale, Basis rotation, Vessel vessel) // TODO: Implement rotation
    {
        _width = Math.Abs(scale.X);
        _height = Math.Abs(scale.Y);
        _length = Math.Abs(scale.Z);

        SurfaceArea = _width * _height;
        Volume = SurfaceArea * _length;
        var lengthSquared = _length * _length;

        Efficiency = lengthSquared / SurfaceArea;
        ThrustSpoolTime = SurfaceArea / lengthSquared;
        Thrust = _width * _height * lengthSquared;
        MaterialUse = Thrust * Efficiency;
        ThrustPerVolume = Thrust / Volume;


        Scale = scale;
        _thrusterContainerNode?.QueueFree();
        _thrusterContainerNode = new Node3D();
        Vector3 scaleInverse = new Vector3(1 / scale.X, 1 / scale.Y, 1 / scale.Z);
        _thrusterContainerNode.Scale = scaleInverse;
        AddChild(_thrusterContainerNode);
        
        GenerateThruster();

        GD.Print("Surface area: " + SurfaceArea + " m^2");
        GD.Print("Volume: " + Volume + " m^3");
        GD.Print("Thrust per second: " + Thrust);
        GD.Print("Thrust per volume: " + ThrustPerVolume);
        GD.Print("Resource per second: " + MaterialUse);
        GD.Print("Thrust per resource: " + Thrust / MaterialUse);

        GD.Print("Full thrust time: " + ThrustSpoolTime);

        if (vessel == null)
        {
            GD.Print("Making new vessel");
            return Place(position, scale, rotation);
        }
        GD.Print("Adding to existing vessel");
        return AddToVessel(vessel, position, scale, rotation);
        
    }

    private void GenerateThruster()
    {
        
        
        
        AddRoundedBox(new Vector3(_width - 0.1, _height - 0.1, _length - _nozzleLength),
            new Vector3(0, 0, -_nozzleLength / 2), _engineCornerRadius);

        AddBox(new Vector3(_width, 0.1, _nozzleLength),
            new Vector3(0, _height / 2, (_length - _nozzleLength) / 2));
        AddBox(new Vector3(_width, 0.1, _nozzleLength),
            new Vector3(0, -_height / 2, (_length - _nozzleLength) / 2));
        AddBox(new Vector3(0.1, _height, _nozzleLength),
            new Vector3(_width / 2, 0, (_length - _nozzleLength) / 2));
        AddBox(new Vector3(0.1, _height, _nozzleLength),
            new Vector3(-_width / 2, 0, (_length - _nozzleLength) / 2));

        var startPos = -_height / 2;
        var finCount = (int)(_height / _finSpacing);

        for (var i = 0; i < finCount; i++)
        {
            var pos = startPos + _finSpacing * i + _finSpacing;
            AddBox(new Vector3(_width, _finWidth, _nozzleLength),
                new Vector3(0, pos, (_length - _nozzleLength) / 2));
        }
    }


    private void AddBox(Vector3 scale, Vector3 position)
    {
        var boxMesh = new MeshInstance3D();
        boxMesh.Mesh = new BoxMesh();
        boxMesh.Scale = scale;
        boxMesh.Position = position;
        _thrusterContainerNode.AddChild(boxMesh);
    }

    private void AddCylinder(Vector3 scale, Vector3 position, Vector3 rotation)
    {
        var cylinderMesh = new MeshInstance3D();
        cylinderMesh.Mesh = new CylinderMesh();
        cylinderMesh.Scale = scale;
        cylinderMesh.Position = position;
        cylinderMesh.Rotation = rotation;
        _thrusterContainerNode.AddChild(cylinderMesh);
    }

    private void AddSphere(double scale, Vector3 position)
    {
        var sphereMesh = new MeshInstance3D();
        sphereMesh.Mesh = new SphereMesh();
        sphereMesh.Scale = new Vector3(scale, scale, scale);
        sphereMesh.Position = position;
        _thrusterContainerNode.AddChild(sphereMesh);
    }

    private void AddRoundedBox(Vector3 scale, Vector3 position, double radius)
    {
        var diameter = radius * 2;
        AddBox(new Vector3(scale.X, scale.Y - diameter, scale.Z - diameter), position);
        AddBox(new Vector3(scale.X - diameter, scale.Y, scale.Z - diameter), position);
        AddBox(new Vector3(scale.X - diameter, scale.Y - diameter, scale.Z), position);

        var cylinderScaleA = new Vector3(diameter, scale.Y / 2 - radius, diameter);
        var cylinderRotA = new Vector3(0, 0, 0);
        var cylinderPosAa =
            new Vector3(position.X + scale.X / 2 - radius, position.Y, position.Z + scale.Z / 2 - radius);
        var cylinderPosAb =
            new Vector3(position.X + scale.X / 2 - radius, position.Y, position.Z - scale.Z / 2 + radius);
        var cylinderPosAc =
            new Vector3(position.X - scale.X / 2 + radius, position.Y, position.Z - scale.Z / 2 + radius);
        var cylinderPosAd =
            new Vector3(position.X - scale.X / 2 + radius, position.Y, position.Z + scale.Z / 2 - radius);
        AddCylinder(cylinderScaleA, cylinderPosAa, cylinderRotA);
        AddCylinder(cylinderScaleA, cylinderPosAb, cylinderRotA);
        AddCylinder(cylinderScaleA, cylinderPosAc, cylinderRotA);
        AddCylinder(cylinderScaleA, cylinderPosAd, cylinderRotA);

        var cylinderScaleB = new Vector3(diameter, scale.X / 2 - radius, diameter);
        var cylinderRotB = new Vector3(0, 0, Math.PI / 2);
        var cylinderPosBa =
            new Vector3(position.X, position.Y + scale.Y / 2 - radius, position.Z + scale.Z / 2 - radius);
        var cylinderPosBb =
            new Vector3(position.X, position.Y + scale.Y / 2 - radius, position.Z - scale.Z / 2 + radius);
        var cylinderPosBc =
            new Vector3(position.X, position.Y - scale.Y / 2 + radius, position.Z - scale.Z / 2 + radius);
        var cylinderPosBd =
            new Vector3(position.X, position.Y - scale.Y / 2 + radius, position.Z + scale.Z / 2 - radius);
        AddCylinder(cylinderScaleB, cylinderPosBa, cylinderRotB);
        AddCylinder(cylinderScaleB, cylinderPosBb, cylinderRotB);
        AddCylinder(cylinderScaleB, cylinderPosBc, cylinderRotB);
        AddCylinder(cylinderScaleB, cylinderPosBd, cylinderRotB);

        var cylinderScaleC = new Vector3(diameter, scale.Z / 2 - radius, diameter);
        var cylinderRotC = new Vector3(Math.PI / 2, 0, 0);
        var cylinderPosCa =
            new Vector3(position.X + scale.X / 2 - radius, position.Y + scale.Y / 2 - radius, position.Z);
        var cylinderPosCb =
            new Vector3(position.X + scale.X / 2 - radius, position.Y - scale.Y / 2 + radius, position.Z);
        var cylinderPosCc =
            new Vector3(position.X - scale.X / 2 + radius, position.Y - scale.Y / 2 + radius, position.Z);
        var cylinderPosCd =
            new Vector3(position.X - scale.X / 2 + radius, position.Y + scale.Y / 2 - radius, position.Z);

        AddCylinder(cylinderScaleC, cylinderPosCa, cylinderRotC);
        AddCylinder(cylinderScaleC, cylinderPosCb, cylinderRotC);
        AddCylinder(cylinderScaleC, cylinderPosCc, cylinderRotC);
        AddCylinder(cylinderScaleC, cylinderPosCd, cylinderRotC);

        var hs = new Vector3(scale.X / 2 - radius, scale.Y / 2 - radius, scale.Z / 2 - radius);
        var ca = position + new Vector3(hs.X, hs.Y, hs.Z);
        var cb = position + new Vector3(-hs.X, hs.Y, hs.Z);
        var cc = position + new Vector3(hs.X, -hs.Y, hs.Z);
        var cd = position + new Vector3(-hs.X, -hs.Y, hs.Z);
        var ce = position + new Vector3(hs.X, hs.Y, -hs.Z);
        var cf = position + new Vector3(-hs.X, hs.Y, -hs.Z);
        var cg = position + new Vector3(hs.X, -hs.Y, -hs.Z);
        var ch = position + new Vector3(-hs.X, -hs.Y, -hs.Z);


        AddSphere(diameter, ca);
        AddSphere(diameter, cb);
        AddSphere(diameter, cc);
        AddSphere(diameter, cd);
        AddSphere(diameter, ce);
        AddSphere(diameter, cf);
        AddSphere(diameter, cg);
        AddSphere(diameter, ch);
    }
    
    
}