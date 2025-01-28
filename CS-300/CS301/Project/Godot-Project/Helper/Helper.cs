using Godot;
using System;
using System.Collections.Generic;

public partial class Helper : Node
{
    public static List<T> SearchRecursive<T>(Node n)
    {
        if (n.GetChildren().Count == 0) return null; // need to check for null later

        List<T> list = new List<T>();

        foreach (var child in n.GetChildren())
        {
            if (child is T s) { list.Add(s); } // if child is a sprite2D or can be
            if (child.GetChildren().Count != 0) // see if child has sub children
            {
                List<T> sublist = SearchRecursive<T>(child);
                if (sublist == null || sublist.Count == 0) { continue; } // no children
                foreach (var subchild in sublist)
                {
                    list.Add(subchild);
                }
            }
        }

        return list;
    }
}
