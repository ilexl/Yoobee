using Godot;

namespace ArchitectsInVoid.Debug.Meshes;

public partial class DebugLine : DebugMesh
{

    
    private Vector3 _start;
    private Vector3 _end;
    private double _thickness = 0;
    
    // Prevents IDE from getting angry, should never use parameterless constructor for this class
    public DebugLine()
    {
    }

    public DebugLine(Vector3 start, Vector3 end, Color color, double duration, double thickness, bool drawOnTop, Type type, DebugDraw instance) : base(color, duration, drawOnTop, type, instance)
    {
        _start = start;
        _end = end;
        _thickness = thickness;
        
        type = (type == Type.Auto) ? (thickness == 0 ?Type.Wireframe : Type.Solid) : type;
        this.type = type;
        
        
        switch (type)
        {
            case Type.Solid:
                GenerateTangibleLine();
                break;
            case Type.Wireframe:
                GenerateWireframeLine();
                break;
        }
    }
    public override bool Update(double delta)
    {
        
        switch (type)
        {
            case Type.Solid:
                GenerateTangibleLine();
                break;
            case Type.Wireframe:
                break;
        }

        return base.Update(delta);
    }
    
    private void GenerateWireframeLine()
    {
        IM.SurfaceBegin(Mesh.PrimitiveType.Lines);
        IM.SurfaceSetColor(Color);
        IM.SurfaceAddVertex(_start);
        IM.SurfaceAddVertex(_end);
        IM.SurfaceEnd();
    }
    
    private void GenerateTangibleLine()
    {
        
        IM.ClearSurfaces();

        Vector3 cameraPos = Instance.CameraPosition;
        Vector3 upFacingDirection = (_start - cameraPos);
        Vector3 lineVec = (_end - _start);
        Vector3 lineSide = lineVec.Cross(upFacingDirection).Normalized();
                
        Vector3 cornerA = _start + lineSide * _thickness;
        Vector3 cornerB = _end + lineSide * _thickness;
        Vector3 cornerC = _start - lineSide * _thickness;
        Vector3 cornerD = _end - lineSide * _thickness;
        IM.SurfaceBegin(Mesh.PrimitiveType.TriangleStrip);
        IM.SurfaceSetColor(Color);
        IM.SurfaceAddVertex(cornerA);
        IM.SurfaceAddVertex(cornerB);
        IM.SurfaceAddVertex(cornerC);
        IM.SurfaceAddVertex(cornerD);
        IM.SurfaceEnd();
    }
}