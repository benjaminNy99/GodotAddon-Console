using Godot;
using Godot.Addons.GodotConsole.Interfaces;
using Godot.Addons.GodotConsole.Scripts;
using System;

[Command("text")]
public partial class TextCommand : ICommand
{
    public void Execute(ConsoleContext ctx, params string[] args)
    {
        throw new NotImplementedException();
    }
}
