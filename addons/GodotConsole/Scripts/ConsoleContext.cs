using Godot;
using System;

namespace Godot.Addons.GodotConsole.Scripts;
public class ConsoleContext
{
    public Node RootNode;
    public Action<string> Print;
}
