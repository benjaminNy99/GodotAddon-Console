using Godot;
using Godot.Addons.GodotConsole.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Godot.Addons.GodotConsole.Scripts;

[GlobalClass]
public partial class Manager : Node
{
    private System.Collections.Generic.Dictionary<string, ICommand> DicCommands;
    private RichTextLabel OutPut { get; }
    private LineEdit Input { get; }

    public Manager(RichTextLabel output, LineEdit input)
    {
        OutPut = output;
        Input = input;
    }

    public override void _Ready()
    {
        _LoadCommands();
    }

    /// <summary>
    /// Parses the input into command name and arguments, then executes the command.
    /// </summary>
    /// <param name="input">Raw input string containing the command and its arguments.</param>
    public void Command(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            OutPut.AppendText(LibConsole.PrintWarning("No command entered.") + "\n");
            return;
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string cmd = parts[0];

        string[] args = parts.Length > 1
            ? parts.Skip(1).ToArray()
            : Array.Empty<string>();

        _CommandExecute(cmd, args);
    }

    /// <summary>
    /// Executes a single command by its name and arguments.
    /// </summary>
    /// <param name="cmd">Command name.</param>
    /// <param name="args">Command arguments.</param>
    private void _CommandExecute(string cmd, params string[] args)
    {
        if (string.IsNullOrWhiteSpace(cmd))
        {
            OutPut.AppendText(LibConsole.PrintWarning("Empty command.") + "\n");
            return;
        }

        if (!DicCommands.TryGetValue(cmd, out var command))
        {
            OutPut.AppendText(LibConsole.PrintWarning($"Command '{cmd}' not found.") + "\n");
            return;
        }

        try
        {
            command.Execute(args ?? Array.Empty<string>());
        }
        catch (Exception ex)
        {
            OutPut.AppendText(
                LibConsole.PrintError($"Error executing '{cmd}': {ex.Message}") + "\n"
            );
        }
    }

    /// <summary>
    /// Load all class of ICommand in to private Dictionary
    /// </summary>
    private void _LoadCommands()
    {
        var commands = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                typeof(ICommand).IsAssignableFrom(t) &&
                t.GetCustomAttribute<CommandAttribute>() != null &&
                !t.IsAbstract)
            .Select(t =>
            {
                var attrs = t.GetCustomAttribute<CommandAttribute>();
                var instance = (ICommand)Activator.CreateInstance(t);
                return new
                {
                    Key = attrs.CommandName,
                    Value = instance,
                };
            })
            .ToDictionary(x => x.Key, x => x.Value);

        DicCommands = commands;
    }
}
