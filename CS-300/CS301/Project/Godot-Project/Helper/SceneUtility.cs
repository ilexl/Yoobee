using System.IO;
using Godot;

namespace ArchitectsInVoid.Helper;

public static class SceneUtility
{
    const string ResPrefix = "res://";
    public static string GetSceneDirectory(string scenePath)
    {
        scenePath = StripResPrefix(scenePath);
        string directory = Path.GetDirectoryName(scenePath)?.Replace("\\", "/");
        return EnsureResPrefix(directory);
    }

    public static string StripResPrefix(string path)
    {
        
        // Remove the prefix if the path starts with "res://"
        if (path.StartsWith(ResPrefix))
        {
            return path.Substring(ResPrefix.Length);
        }
        
        return path;
    }
    private static string EnsureResPrefix(string path)
    {
        
        
        // If the path does not already start with "res://", add the prefix
        if (!string.IsNullOrEmpty(path) && !path.StartsWith(ResPrefix))
        {
            return ResPrefix + path;
        }
        
        return path;
    }
}