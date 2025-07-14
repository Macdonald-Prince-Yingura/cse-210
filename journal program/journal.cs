using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private readonly List<Entry> _entries = new();

    public void AddEntry(Entry entry) => _entries.Add(entry);

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\n(No entries yet)");
            return;
        }

        Console.WriteLine("\n=== Journal Entries ===\n");
        foreach (Entry e in _entries)
        {
            Console.WriteLine(e.GetDisplayText());
        }
    }

    public void SaveToFile(string? filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Save cancelled (no filename).");
            return;
        }

        try
        {
            using StreamWriter writer = new(filename);
            foreach (Entry e in _entries)
            {
                writer.WriteLine(e.ToCsv());
            }
            Console.WriteLine("Journal saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save failed: {ex.Message}");
        }
    }

    public void LoadFromFile(string? filename)
    {
        if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
        {
            Console.WriteLine("Load failed (file not found).");
            return;
        }

        try
        {
            _entries.Clear();
            foreach (string line in File.ReadAllLines(filename))
            {
                Entry? entry = Entry.FromCsv(line);
                if (entry != null) _entries.Add(entry);
            }
            Console.WriteLine("Journal loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load failed: {ex.Message}");
        }
    }
}
