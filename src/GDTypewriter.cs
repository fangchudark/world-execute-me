using System;
using System.Text;
using System.Threading.Tasks;
using Godot;
public partial class GDTypewriter : IConsoleTypewriter
{    
    private const char cursorChar = '▮';
    private readonly RichTextLabel _label;
    private readonly StringBuilder _currentText;
    private const int DefaultBufferSize = 256;
    public GDTypewriter(RichTextLabel label)
    {
        _label = label;
        _currentText = new StringBuilder(DefaultBufferSize);
    }

    public async Task WriteTypewriter(
        string prefix,
        string text,
        TimeSpan totalMs,
        bool lineFeed = true
    )
    {
        if (!GodotObject.IsInstanceValid(_label))
            return;
        text ??= string.Empty;
            
        if (totalMs.TotalMilliseconds <= 0)
        {
            totalMs = TimeSpan.FromMilliseconds(1);
        }
    
        var pureText = RemoveBBCodeTags(text);
        var baseText = _label.Text.Replace(cursorChar.ToString(), string.Empty);
        _currentText.Clear();        
    
        var delayMs = TimeSpan.FromMilliseconds(totalMs.TotalMilliseconds / Math.Max(1, pureText.Length));
        var cursorStr = cursorChar.ToString();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                var endPos = text.IndexOf(']', i);
                if (endPos != -1)
                {
                    var colorTag = text.Substring(i, endPos - i + 1);
                    _currentText.Append(colorTag);
                    i += colorTag.Length - 1;
                }
                else
                {
                    _currentText.Append(text[i]);
                }
            }
            else
            {
                _currentText.Append(text[i]);
            }

            bool showCursor = (i < text.Length - 1) || !lineFeed;

            try
            {
                _label.Text = BuildLabelText(baseText, prefix, showCursor ? cursorChar : '\0');
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            await Task.Delay(delayMs);
        }
    
        if (lineFeed)
        {
            if (!_label.Text.EndsWith(cursorStr))
            {
                AppendLineFeed(cursorChar);
            }
        }
    }
    
    private static string RemoveBBCodeTags(string textWithTags)
    {
        return MyRegex().Replace(textWithTags, "");
    }
    public void NewLine()
    {
        _label.Text = _label.Text.Replace(cursorChar.ToString(), string.Empty) + '\n' + cursorChar;        
    }

    private string BuildLabelText(string baseText, string prefix, char cursor)
        => $"{baseText}{prefix}{_currentText}{(cursor != '\0' ? $"[color=#ffffff]{cursor}[/color]" : string.Empty)}";

    private void AppendLineFeed(char cursorChar)
    {
        _label.Text += $"\n{cursorChar}";        
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"\[.*?\]")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();

}
