using System;
using System.Text;

public class Entry
{
    private readonly string _date;     // stored as short date string
    private readonly string _prompt;
    private readonly string _response;
    private readonly int    _mood;     // 0 = not provided

    public Entry(string prompt, string response, int mood = 0)
    {
        _date    = DateTime.Now.ToShortDateString();
        _prompt  = prompt;
        _response = response;
        _mood     = mood;
    }

    // Factory for CSV load
    public static Entry? FromCsv(string line)
    {
        string[] parts = ParseCsvLine(line);
        if (parts.Length < 3) return null;

        int mood = parts.Length >= 4 && int.TryParse(parts[3], out int m) ? m : 0;
        Entry e = new(parts[1], parts[2], mood) { _date = parts[0] };
        return e;
    }

    public string GetDisplayText()
    {
        StringBuilder sb = new();
        sb.AppendLine($"Date: {_date}");
        sb.AppendLine($"Prompt: {_prompt}");
        sb.AppendLine($"Response: {_response}");
        if (_mood != 0) sb.AppendLine($"Mood: {_mood}/10");
        sb.AppendLine(new string('-', 30));
        return sb.ToString();
    }

    // --- CSV helpers -------------------------------------------------

    public string ToCsv()
    {
        return string.Join(',',
            Escape(_date),
            Escape(_prompt),
            Escape(_response),
            _mood.ToString());
    }

    private static string Escape(string s)
    {
        if (s.Contains('"')) s = s.Replace("\"", "\"\"");
        return s.Contains(',') || s.Contains('"')
            ? $"\"{s}\""
            : s;
    }

    private static string[] ParseCsvLine(string line)
    {
        // Simple CSV parser good enough for this assignment
        var items = new System.Collections.Generic.List<string>();
        StringBuilder field = new();
        bool inQuotes = false;

        foreach (char c in line)
        {
            if (inQuotes)
            {
                if (c == '"')
                {
                    inQuotes = false;
                }
                else
                {
                    field.Append(c);
                }
            }
            else
            {
                if (c == ',')
                {
                    items.Add(field.ToString());
                    field.Clear();
                }
                else if (c == '"')
                {
                    inQuotes = true;
                }
                else
                {
                    field.Append(c);
                }
            }
        }
        items.Add(field.ToString());
        return items.ToArray();
    }
}
