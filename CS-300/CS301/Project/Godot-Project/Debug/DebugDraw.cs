using System.Collections.Generic;
using System.Runtime.InteropServices;
using ArchitectsInVoid.Debug.Meshes;
using Godot;

namespace ArchitectsInVoid.Debug;

public sealed partial class DebugDraw : Node
{


    
    public static SceneTree SceneTree = (SceneTree)Engine.GetMainLoop();
    public static Window Root = SceneTree.GetRoot();
    
    
    DebugDraw()
    {
        Root.AddChild(this);
    }

    #region Singleton
    private static DebugDraw _instance = null;
    private static readonly object Padlock = new object();

    private static DebugDraw Instance
    {
        get
        {
            lock (Padlock)
            {
                if (_instance == null)
                {
                    _instance = new DebugDraw();
                }

                return _instance;
            }
        }
    }
    #endregion
    #region Instanced

    #region TODO: This shouldn't be here
    private bool _cameraNormalThisFrame = false;
    private Vector3 _cameraNormal;

    internal Vector3 CameraNormal
    {
        get
        {
            if (!_cameraNormalThisFrame)
            {
                _cameraNormal = Root.GetCamera3D().GlobalTransform.Basis.Z;
                _cameraNormalThisFrame = true;
            }
            return _cameraNormal;
        }
    }

    private bool _cameraPositionThisFrame = false;
    private Vector3 _cameraPosition;

    internal Vector3 CameraPosition
    {
        get
        {
            if (!_cameraPositionThisFrame)
            {
                _cameraPosition = Root.GetCamera3D().GlobalPosition;
                _cameraPositionThisFrame = true;
            }
            return _cameraPosition;
        }
    }

    private bool _cameraRotationThisFrame = false;
    private Vector3 _cameraRotation;
    public Vector3 CameraWorldRotation
    {
        get
        {
            if (!_cameraRotationThisFrame)
            {
                _cameraRotation = Root.GetCamera3D(). GlobalTransform.Basis.GetEuler();;
                _cameraRotationThisFrame = true;
            }
           
            return _cameraRotation;
        }
    }
#endregion

    private double _lastDelta = 0;
    private List<Meshes.DebugMesh> _debugObjects = new();
    public override void _Process(double delta)
    {
        _lastDelta = delta;
        _cameraNormalThisFrame = false;
        _cameraPositionThisFrame = false;
        _cameraRotationThisFrame = false;
        for (var index = _debugObjects.Count - 1; index >= 0; index--)
        {
            var mesh = _debugObjects[index];
            bool shouldRemove = mesh.Update(delta);
            if (shouldRemove)
            {
                mesh.QueueFree();
                _debugObjects.RemoveAt(index);
            }
        }

        // Cheaper to reuse both lists than to instantiate a new one
    }
    #endregion

    private static void InstantiateDebugMesh(Meshes.DebugMesh mesh)
    {
        Root.AddChild(mesh);
        Instance._debugObjects.Add(mesh);
    }
        
    #region DebugDefaults
            
    internal const double DefaultDuration = 1.0 / 60; // TODO: Replace this with update rate from project settings
    internal const double DefaultThickness = 0;
    internal const double DefaultRadius = 1;
    internal const double DefaultScale = 1;
    internal const double DefaultSpacing = 0.1;
    internal const double DefaultCount = 10;
    internal const int DefaultPrecision = 20;

    
    internal static readonly Color DefaultColor = new Color(255f, 255f, 255f);
    internal static readonly Vector3 DefaultPos = Vector3.Zero;
    #endregion
        
    #region DebugLine
    /// <summary>
    /// Draws a line between two points with optional arguments.
    /// </summary>
    /// <param name="start">Start position of line. Defaults to Vector3.Zero.</param>
    /// <param name="end">End position of line. Defaults to Vector3.Zero.</param>
    /// <param name="color">Color of line. Defaults to White.</param>
    /// <param name="duration">Time until the line gets destroyed. Defaults to 1 frame.</param>
    /// <param name="thickness">Apparent "radius" of the line. Defaults to 0.</param>
    /// <param name="drawOnTop">Controls whether the object draws on top of everything else.</param>
    /// <param name="type">Auto, Tangible, or Wireframe. Tangible produces a line with a radius, wireframe produces an infinitely small yet visible line. Auto is Wireframe if 0, otherwise tangible. Defaults to Auto.</param>
    public static void Line([Optional]Vector3? start, [Optional]Vector3? end, [Optional]Color? color, double duration = DefaultDuration, double thickness = DefaultThickness, bool drawOnTop = false, DebugMesh.Type type = DebugMesh.Type.Auto )
    {
        Vector3 a = start ?? DefaultPos;
        Vector3 b = end ?? DefaultPos;
        Color finalColor = color ?? DefaultColor;



        DebugLine mesh = new DebugLine(a, b, finalColor, duration + Instance._lastDelta / 2, thickness, drawOnTop, type, Instance);
        

        InstantiateDebugMesh(mesh);
        
    }
    
    #endregion
    #region DebugCircle
        
        /// <summary>
        /// Draws a circle at a position with optional arguments.
        /// </summary>
        /// <param name="position">The position to draw the circle. Defaults to Vector3.Zero.</param>
        /// <param name="color">The color of the circle. Defaults to white.</param>
        /// <param name="duration">The time after which the circle will be destroyed. Defaults to one frame.</param>
        /// <param name="radius">The radius of the circle. Defaults to 1.</param>
        /// <param name="precision">The amount of points to construct the circle with. Defaults to 20.</param>
        /// <param name="drawOnTop">Controls whether the object draws on top of everything else.</param>
        /// <param name="type">The type of the circle, being solid or wireframe. Defaults to solid.</param>
    public static void Circle([Optional]Vector3? position, [Optional]Color? color, double duration = DefaultDuration, double radius = DefaultRadius, int precision = DefaultPrecision, bool drawOnTop = false, DebugMesh.Type type = DebugMesh.Type.Auto )
    {
        Vector3 pos = position ?? DefaultPos;
        Color finalColor = color ?? DefaultColor;
    
    
    
        DebugCircle mesh = new DebugCircle(pos, finalColor, duration, radius, precision, drawOnTop, type, Instance);
        
    
        InstantiateDebugMesh(mesh);
        
    }
    #endregion
    #region DebugRaycast
    public static void Ray(PhysicsRayQueryParameters3D query, Godot.Collections.Dictionary result, double duration = DefaultDuration)
    {
        Vector3 start = query.From;
        Vector3 end = query.To;
        

        if (result.Count > 0)
        {
            Vector3 hit = (Vector3)result["position"];
            Vector3 normal = (Vector3)result["normal"];
            Line(start, hit, Colors.Red, duration, drawOnTop:true);
            Line(hit, end, Colors.Green, duration, drawOnTop:true);
            Line(hit, hit + normal, Colors.Aqua, duration, drawOnTop:true);
            return;
        }
        Line(start, end, Colors.Red, duration, drawOnTop:true);
    }    
        
    #endregion
        // TODO: Implement DebugSphere
    #region DebugSphere
    #endregion
        // TODO: Implement DebugLabel
    #region DebugLabel
    public static void Label(Vector3 position, [Optional] Basis? rotation, [Optional] Color? color, bool alwaysFaceCamera = false, double scale = DefaultScale, 
        double duration = DefaultDuration, bool drawOnTop = false)
    {
        throw new System.NotImplementedException();
    }


    
#endregion
    #region DebugBox
        
    public static void Box([Optional]Vector3? cornerA, [Optional]Vector3? cornerB, [Optional] Basis? rotation, [Optional]Color? color, double duration = DefaultDuration, double thickness = DefaultThickness, bool drawOnTop = false, DebugMesh.Type type = DebugMesh.Type.Auto )
    {
        Vector3 start = cornerA ?? DefaultPos;
        Vector3 end = cornerB ?? DefaultPos;
        Vector3 center = start.Lerp(end, 0.5);
        start -= center;
        end -= center;

        ;
        Color finalColor = color ?? DefaultColor;
        Basis finalRotation = rotation ?? Basis.Identity;

        start *= finalRotation;
        end *= finalRotation;
        // In practice, these values will almost never evaluate to their human given names here but it makes it easier to visualize
        double bottom = start.X;
        double front = start.Y;
        double left = start.Z;
        
        double top = end.X;
        double rear = end.Y;
        double right = end.Z;
        
        // Define a position for each corner of the box
        Vector3 bottomFrontLeft = finalRotation * new Vector3(bottom, front, left) + center;
        Vector3 bottomFrontRight = finalRotation * new Vector3(bottom, front, right) + center;
        Vector3 bottomRearLeft = finalRotation * new Vector3(bottom, rear, left) + center;
        Vector3 bottomRearRight = finalRotation * new Vector3(bottom, rear, right) + center;
        Vector3 topFrontLeft = finalRotation * new Vector3(top, front, left) + center;
        Vector3 topFrontRight = finalRotation * new Vector3(top, front, right) + center;
        Vector3 topRearLeft = finalRotation * new Vector3(top, rear, left) + center;
        Vector3 topRearRight = finalRotation * new Vector3(top, rear, right) + center;
        
        
        // Draw lines for each edge of the box, most sensible way to do this is to use 4 non adjacent corners as roots and then draw lines to their adjacent corners
        /*
         *             |
         *             |
         *             |
         *     ------- x
         *               x    
         *                 x
         * Essentially we construct the box out of 4 of these */
        
        
        
        // Bottom front left corner
        Line(bottomFrontLeft, topFrontLeft, finalColor, duration, thickness, drawOnTop, type);
        Line(bottomFrontLeft, bottomRearLeft, finalColor, duration, thickness, drawOnTop, type);
        Line(bottomFrontLeft, bottomFrontRight, finalColor, duration, thickness, drawOnTop, type);

        // top front right corner
        Line(topFrontRight, bottomFrontRight, finalColor, duration, thickness, drawOnTop, type);
        Line(topFrontRight, topRearRight, finalColor, duration, thickness, drawOnTop, type);
        Line(topFrontRight, topFrontLeft, finalColor, duration, thickness, drawOnTop, type);
        
        // top rear left
        Line(topRearLeft, bottomRearLeft, finalColor, duration, thickness, drawOnTop, type);
        Line(topRearLeft, topFrontLeft, finalColor, duration, thickness, drawOnTop, type);
        Line(topRearLeft, topRearRight, finalColor, duration, thickness, drawOnTop, type);
        
        // bottom rear right
        Line(bottomRearRight, topRearRight, finalColor, duration, thickness, drawOnTop, type);
        Line(bottomRearRight, bottomFrontRight, finalColor, duration, thickness, drawOnTop, type);
        Line(bottomRearRight, bottomRearLeft, finalColor, duration, thickness, drawOnTop, type);

    }
    #endregion
        // TODO: Make use grid object in future
    #region DebugGrid
    public static void Grid(Vector3 center, Vector3 up, Vector3 right, [Optional] Color? color, double spacing = DefaultSpacing, double countUp = DefaultCount, double countRight = DefaultCount,
        double duration = DefaultDuration, double thickness = DefaultThickness, bool drawOnTop = false,
        DebugMesh.Type type = DebugMesh.Type.Auto)
    {
        double totalSizeUp = spacing * countUp * 2;
        double totalSizeRight = spacing * countRight * 2;

        Vector3 start = center - up * totalSizeUp / 2 - right * totalSizeRight / 2;
        for (double i = 0; i < totalSizeUp; i+= spacing)
        {
            Vector3 upPos = start + up * i;
            
            Line(upPos, upPos + right * totalSizeRight, color, duration, thickness, drawOnTop, type);
        }
        for (double i = 0; i < totalSizeRight; i+= spacing)
        {
            Vector3 rightPos = start + right * i;
            
            Line(rightPos, rightPos + up * totalSizeUp, color, duration, thickness, drawOnTop, type);
        }
    }
#endregion
}