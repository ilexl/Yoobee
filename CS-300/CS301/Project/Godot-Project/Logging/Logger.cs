using Godot;

namespace ArchitectsInVoid.Logging;

/// <summary>
/// Logger for txt logging outside Godot when publishing this
/// </summary>
public partial class Logger : Node
{
    // TODO: hook into OR replace GD.Print() && GD.PushWarning() && GD.PushError()
}