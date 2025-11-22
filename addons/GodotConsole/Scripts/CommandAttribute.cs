using Godot;
using System;

namespace Godot.Addons.GodotConsole.Scripts;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public string CommandName { get; }

    public CommandAttribute(string commandName)
    {
        CommandName = commandName;
    }
}
