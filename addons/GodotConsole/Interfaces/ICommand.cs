using Godot;
using Godot.Addons.GodotConsole.Scripts;
using System;

namespace Godot.Addons.GodotConsole.Interfaces;
public interface ICommand
{
    void Execute(ConsoleContext ctx, params string[] args);
}
