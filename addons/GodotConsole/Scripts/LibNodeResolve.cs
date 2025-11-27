using Godot;
using System;
using System.Linq;

namespace Godot.Addons.GodotConsole.Scripts;
public static class LibNodeResolve
{
    public static Godot.Collections.Array<Node> Resolve(Node root, string token)
    {
        var nodes = root.GetTree().GetNodesInGroup(token);
        return nodes;
    }

    public static Node ResolveFirst(Node root, string token)
    {
        var nodes = root.GetTree().GetNodesInGroup(token);
        return nodes.FirstOrDefault();
    }

    public static Node ResolveBy(Node root, string token)
    {
        var args = token.Split(':');
        if (args.Length != 2) throw new Exception($"");

        string groupName = args[0];
        string selector = args[1];

        var nodes = root.GetTree().GetNodesInGroup(groupName);

        if (int.TryParse(selector, out int index))
        {
            if (index < 0 || index >= nodes.Count) throw new Exception($"");
            return nodes[index] as Node;
        }

        foreach (var n in nodes)
        {
            if (n is Node node && node.Name == selector) return node;
        }

        throw new Exception($"");
    }
}
