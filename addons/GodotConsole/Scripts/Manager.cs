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
    /// Split input in command and args
    /// </summary>
    /// <param name="input">Input string</param>
    public void Command(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            GD.Print("Sin comando.");
            return;
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string cmd = parts[0];

        string[] args = parts.Length > 1
            ? parts.Skip(1).ToArray()
            : Array.Empty<string>();

        GD.Print($"Comando: {cmd}");
        GD.Print($"Args: {string.Join(", ", args)}");
        _CommandExecute(cmd, args);
    }

    private void _CommandExecute(string cmd, params string[] args)
    {
        if (string.IsNullOrWhiteSpace(cmd))
        {
            GD.Print("Comando vacio");
            return;
        }

        if (!DicCommands.TryGetValue(cmd, out var command))
        {
            GD.Print($"Comando {cmd} no encontrado");
            return;
        }

        try
        {
            command.Execute(args ?? Array.Empty<string>());
        }
        catch (Exception ex)
        {
            GD.Print($"Error ejecutando {cmd}: {ex.Message}");
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
