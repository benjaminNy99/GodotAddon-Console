using Godot;
using System;

namespace Godot.Addons.GodotConsole.Scripts;
public static class LibConsole
{
    // Basic colors
    public static string ColorError = "red";
    public static string ColorWarning = "yellow";
    public static string ColorSuccess = "green";
    public static string ColorInfo = "white";
    public static string ColorDebug = "gray";
    public static string ColorCommand = "cyan";
    public static string ColorSystem = "orange";
    public static string ColorMuted = "#888888";

    // ---------------------------------------------------------------------
    // Generic helpers
    // ---------------------------------------------------------------------

    /// <summary>
    /// Wraps the given text inside a BBCode color tag.
    /// Use this to color any text displayed in a RichTextLabel.
    /// </summary>
    public static string Color(string msg, string color)
        => $"[color={color}]{msg}[/color]";

    /// <summary>
    /// Wraps the given text with BBCode bold tags.
    /// Use this for highlighting important words.
    /// </summary>
    public static string Bold(string msg)
        => $"[b]{msg}[/b]";

    /// <summary>
    /// Wraps the given text with BBCode italic tags.
    /// Use this for emphasis or notes.
    /// </summary>
    public static string Italic(string msg)
        => $"[i]{msg}[/i]";

    /// <summary>
    /// Wraps the given text with BBCode underline tags.
    /// Use this for drawing attention to key elements.
    /// </summary>
    public static string Underline(string msg)
        => $"[u]{msg}[/u]";

    /// <summary>
    /// Wraps the text inside a BBCode code block.
    /// Useful for displaying commands or debug values.
    /// </summary>
    public static string Code(string msg)
        => $"[code]{msg}[/code]";

    /// <summary>
    /// Renders text with a reduced font size.
    /// Useful for metadata, timestamps, or low-priority details.
    /// </summary>
    public static string Small(string msg, int size = 10)
        => $"[font_size={size}]{msg}[/font_size]";

    /// <summary>
    /// Prepends a timestamp to the message.
    /// Provide a specific time or leave null to use current system time.
    /// </summary>
    public static string WithTimestamp(string msg, DateTime? time = null)
    {
        var t = time ?? DateTime.Now;
        var ts = Color($"[{t:HH:mm:ss}]", ColorMuted);
        return $"{ts} {msg}";
    }

    // ---------------------------------------------------------------------
    // Message categories
    // ---------------------------------------------------------------------

    /// <summary>
    /// Formats an error message using red color and bold ERROR label.
    /// </summary>
    public static string PrintError(string msg)
        => Color($"{Bold("ERROR")} {msg}", ColorError);

    /// <summary>
    /// Formats a warning message using yellow color and bold WARN label.
    /// </summary>
    public static string PrintWarning(string msg)
        => Color($"{Bold("WARN")} {msg}", ColorWarning);

    /// <summary>
    /// Formats a general info message using white color and bold INFO label.
    /// </summary>
    public static string PrintInfo(string msg)
        => Color($"{Bold("INFO")} {msg}", ColorInfo);

    /// <summary>
    /// Formats a success message using green color and bold OK label.
    /// </summary>
    public static string PrintSuccess(string msg)
        => Color($"{Bold("OK")} {msg}", ColorSuccess);

    /// <summary>
    /// Formats a debug message using gray color and bold DEBUG label.
    /// </summary>
    public static string PrintDebug(string msg)
        => Color($"{Bold("DEBUG")} {msg}", ColorDebug);

    /// <summary>
    /// Formats a system-level message using orange color and bold SYSTEM label.
    /// Use this for internal engine notifications or startup messages.
    /// </summary>
    public static string PrintSystem(string msg)
        => Color($"{Bold("SYSTEM")} {msg}", ColorSystem);

    // ---------------------------------------------------------------------
    // Console I/O formatting
    // ---------------------------------------------------------------------

    /// <summary>
    /// Formats a user input line (e.g., commands typed by the player).
    /// Useful for echoing commands back to the console.
    /// </summary>
    public static string PrintUserInput(string msg)
        => Color($"> {msg}", ColorCommand);

    /// <summary>
    /// Displays a command as a code-styled echo, typically for console history.
    /// </summary>
    public static string PrintCommandEcho(string commandLine)
        => Color($"> {Code(commandLine)}", ColorCommand);

    // ---------------------------------------------------------------------
    // Section titles
    // ---------------------------------------------------------------------

    /// <summary>
    /// Displays a section title using uppercase bold text and system color.
    /// Use this to visually separate groups of messages or categories.
    /// </summary>
    public static string PrintSectionTitle(string title)
        => Color(Bold(title.ToUpper()), ColorSystem);

    /// <summary>
    /// Displays a smaller title in bold without additional color.
    /// Useful for local grouping or subheaders.
    /// </summary>
    public static string PrintSubTitle(string title)
        => Bold(title);

    // ---------------------------------------------------------------------
    // Key/value formatting
    // ---------------------------------------------------------------------

    /// <summary>
    /// Formats a key/value pair using bold keys.
    /// Use this for displaying configuration or variable states.
    /// </summary>
    public static string PrintKeyValue(string key, object value)
        => $"{Bold($"{key}:")} {value}";

    /// <summary>
    /// Formats a key/value pair using muted colors.
    /// Useful for displaying low-priority data or system introspection values.
    /// </summary>
    public static string PrintKeyValueMuted(string key, object value)
        => $"{Color(Bold($"{key}:"),
                    ColorMuted)} {Color(value.ToString(), ColorInfo)}";
}
