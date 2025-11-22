using Godot;
using System;

namespace Godot.Addons.GodotConsole.Interfaces;
public interface ICommand
{
    void Execute(params string[] args);
}
