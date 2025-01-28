using System;
using System.Collections.Generic;
using Godot;

namespace ArchitectsInVoid.Debug.Meshes;

public partial class DebugCircle : DebugMesh
{

    
    private Vector3 _position;
    private double _radius = 0;
    private double _precision = 0;

    private List<Vector3> _points;
    
    // Prevents IDE from getting angry, should never use parameterless constructor for this class
    public DebugCircle()
    {
    }

    public DebugCircle(Vector3 position, Color color, double duration, double radius, double precision, bool drawOnTop, Type type, DebugDraw instance) : base(color, duration, drawOnTop, type, instance)
    {
        Position = position;
        _position = position;
        _radius = radius;
        _precision = precision;
        
        type = type == Type.Auto ? Type.Solid : type;
        this.type = type;

        _points = GetCirclePoints();
        switch (type)
        {
            case Type.Solid:
                GenerateFilledCircle();
                break;
            case Type.Wireframe:
                GenerateWireframeCircle();
                break;
        }
    }
    public override bool Update(double delta)
    {
        Rotation = Instance.CameraWorldRotation;
        // switch (type)
        // {
        //     case Type.Solid:
        //         GenerateFilledCircle();
        //         break;
        //     case Type.Wireframe:
        //         GenerateWireframeCircle();
        //         break;
        // }

        return base.Update(delta);
    }

    private void GenerateWireframeCircle()
    {
        IM.ClearSurfaces();
        
        IM.SurfaceBegin(Mesh.PrimitiveType.Lines);
        IM.SurfaceSetColor(Color);
        
        for (var index = 0; index < _points.Count; index++)
        {
            var point = _points[index];
            var pointNext = _points[(index + 1) % _points.Count ];
            IM.SurfaceAddVertex(point);
            IM.SurfaceAddVertex(pointNext);
        }
        IM.SurfaceEnd();
    }

    private void GenerateFilledCircle()
    {
        IM.ClearSurfaces();
        
        IM.SurfaceBegin(Mesh.PrimitiveType.Triangles);
        IM.SurfaceSetColor(Color);

        for (var index = 0; index < _points.Count; index++)
        {
            var point = _points[index];
            var pointNext = _points[(index + 1) % _points.Count ];
            IM.SurfaceAddVertex(point);
            IM.SurfaceAddVertex(pointNext);
            IM.SurfaceAddVertex(Vector3.Zero);
        }

        IM.SurfaceEnd();
    }

    private List<Vector3> GetCirclePoints()
    {
        double step = Math.PI * 2 / _precision;

        List<Vector3> points = new();
        for (double i = 0; i < Math.PI * 2; i += step)
        {
            double y = Math.Sin(i);
            double x = Math.Cos(i);
            Vector3 point = new Vector3(x * _radius, y * _radius, 0);
            points.Add(point);
        }

        return points;

    }
}