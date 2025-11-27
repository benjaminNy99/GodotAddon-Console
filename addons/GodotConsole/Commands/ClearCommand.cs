using Godot;
using Godot.Addons.GodotConsole.Interfaces;
using Godot.Addons.GodotConsole.Scripts;
using System;

namespace Godot.Addons.GodotConsole.Commands;

[Command("clear")]
public class ClearCommmand : ICommand
{
    public void Execute(ConsoleContext ctx, params string[] args)
    {
        var root = LibNodeResolve.ResolveFirst(ctx.RootNode, "console");
        var output = root.GetNodeOrNull<RichTextLabel>("pc_background/rtl_output");

        output.Clear();
    }
}
