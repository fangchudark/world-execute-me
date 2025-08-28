using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
public interface IConsoleTypewriter
{    
    Task WriteTypewriter(
        string prefix,
        string text,
        TimeSpan totalMs,
        bool lineFeed = true
    );

    void NewLine();
}
public struct SimulationMessage
{
    public TimeSpan StartTime { get; private set; }
    public string Prefix { get; private set; }
    public string Text { get; private set; }
    public TimeSpan TotalTime { get; private set; }
    public string Color { get; private set; }
    public string CursorColor { get; private set; }
    public bool LineFeed { get; private set; }

    public SimulationMessage(
        int min,
        int secs,
        int millis,
        string prefix,
        string text,
        TimeSpan totalTime,
        bool lineFeed = true)
    {
        StartTime = TimeSpan.FromSeconds(min * 60 + secs) + TimeSpan.FromMilliseconds(millis);
        Prefix = prefix;
        Text = text;
        TotalTime = totalTime;
        LineFeed = lineFeed;
    }
}

public static class StringExtensions
{
    public static string Color(this string text, Godot.Color color)
        => $"[color=#{color.ToHtml()}]{text}[/color]";
    public static string Color(this string text, string color)
        => $"[color={color}]{text}[/color]";
    public static string Bold(this string text)
        => $"[b]{text}[/b]";
    
    public static string Italic(this string text)
        => $"[i]{text}[/i]";
}