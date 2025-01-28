using System.Runtime.InteropServices;
using ArchitectsInVoid.Debug.Meshes;
using Godot;

namespace ArchitectsInVoid.Debug;

public static class DebugDrawNodeExtensions
{
    #region DebugLine
        // TODO: move documentation to xml file
        /// <summary>
        /// Draws a line from this to a specified endpoint.
        /// </summary>
        /// <example> Calling from another object with a reference to the object that you want to draw from.
        /// <code>
        /// Node3D object;
        /// Vector3 end = object.GlobalPosition + object.Transform.Forward * 10;
        /// object.DebugDrawLine(end); // Draws a line from the object to 10 units in front of it.
        /// // By default will be a wireframe line with 1 frame duration.
        /// </code>
        /// </example>
        /// <example> Calling from the object itself.
        /// <code>
        /// Vector3 target = someObject.GlobalPosition;
        /// DebugDrawLine(target); // Draws a line from this object to the target object.
        /// </code>
        /// </example>
        /// <param name="node">This, the line start position.</param>
        /// <param name="end">End position. Defaults to Vector3.Zero.</param>
        /// <param name="color">Color of line. Defaults to White.</param>
        /// <param name="duration">Time until the line gets destroyed. Defaults to 1 frame.</param>
        /// <param name="thickness">Apparent "radius" of the line. Defaults to 0.</param>
        /// <param name="drawOnTop">Controls whether the object draws on top of everything else.</param>
        /// <param name="type">Auto, Tangible, or Wireframe. Tangible produces a line with a radius, wireframe produces an infinitely small yet visible line. Auto is Wireframe if 0, otherwise tangible. Defaults to Auto.</param>
    public static void DebugDrawLine(this Node3D node, [Optional]Vector3? end, [Optional]Color? color, double duration = DebugDraw.DefaultDuration, double thickness = DebugDraw.DefaultThickness, bool drawOnTop = false, DebugMesh.Type type = DebugMesh.Type.Auto )
    {

        DebugDraw.Line(node.GlobalPosition, end, color, duration, thickness, drawOnTop, type);
        
    }
        /// <summary>
        /// Draws a line from this to another object.
        /// </summary>
        /// <example> Calling from another object with a reference to the object that you want to draw from.
        /// <code>
        /// Node3D object;
        /// Node3D otherObject;
        /// object.DebugDrawLine(otherObject); // Draws a line from the object to the other object.
        /// // By default will be a wireframe line with 1 frame duration.
        /// </code>
        /// </example>
        /// <example> Calling from the object itself.
        /// <code>
        /// Node3D someObject;
        /// DebugDrawLine(someObject); // Draws a line from this object to the target object.
        /// </code>
        /// </example>
        /// <param name="node">This, the line start position.</param>
        /// <param name="endNode">A target object to draw a line to.</param>
        /// <param name="color">Color of line. Defaults to White.</param>
        /// <param name="duration">Time until the line gets destroyed. Defaults to 1 frame.</param>
        /// <param name="thickness">Apparent "radius" of the line. Defaults to 0.</param>
        /// <param name="drawOnTop">Controls whether the object draws on top of everything else.</param>
        /// <param name="type">Auto, Tangible, or Wireframe. Tangible produces a line with a radius, wireframe produces an infinitely small yet visible line. Auto is Wireframe if 0, otherwise tangible. Defaults to Auto.</param>
    public static void DebugDrawLine(this Node3D node, Node3D endNode, [Optional]Color? color, double duration = DebugDraw.DefaultDuration, double thickness = DebugDraw.DefaultThickness, bool drawOnTop = false, DebugMesh.Type type = DebugMesh.Type.Auto )
    {

        DebugDraw.Line(node.GlobalPosition, endNode.GlobalPosition, color, duration, thickness, drawOnTop, type);
        
    }
    #endregion
    
    // TODO: Implement
    #region DebugLabel
    public static void DebugLabel(this Node3D node, [Optional] Color? color, bool alwaysFaceCamera = false, double scale = DebugDraw.DefaultScale, double duration = DebugDraw.DefaultDuration, bool drawOnTop = false)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}