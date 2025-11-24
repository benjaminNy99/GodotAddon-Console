using Godot;
using Godot.Addons.GodotConsole.Interfaces;
using Godot.Addons.GodotConsole.Scripts;
using System;

namespace Godot.Addons.GodotConsole.Commands;

[Command("console")]
public class ConsoleCommand : ICommand
{
    public void Execute(params string[] args)
    {
        throw new NotImplementedException();
    }
}
