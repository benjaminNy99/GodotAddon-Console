using Godot;
using Godot.Addons.GodotConsole.Interfaces;
using Godot.Addons.GodotConsole.Scripts;
using System;

namespace Godot.Addons.GodotConsole.Commands;

[Command("console")]
public class ConsoleCommand : ICommand
{
    protected RichTextLabel Output { get; set; }
    protected LineEdit Input { get; set; }

    public void Execute(ConsoleContext ctx, params string[] args)
    {
        var root = LibNodeResolve.ResolveFirst(ctx.RootNode, "console");
        Output = root.GetNodeOrNull<RichTextLabel>("pc_background/rtl_output");
        Input = root.GetNodeOrNull<LineEdit>("pc_background/le_input");

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--font":
                    HandleFont(int.Parse(args[i+1]));
                    i++;
                    break;
            }
        }
    }

    protected void HandleFont(int fontSize)
    {
        Output.AddThemeFontSizeOverride("normal_font_size", fontSize);
        Input.AddThemeFontSizeOverride("font_size", fontSize);
    }
}
