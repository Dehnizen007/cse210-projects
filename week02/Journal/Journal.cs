using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }

    // Save entries as a CSV file
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine("Date,Prompt,Response"); // header row
            foreach (Entry e in _entries)
            {
                string line = $"{EscapeCsv(e._date)},{EscapeCsv(e._prompt)},{EscapeCsv(e._response)}";
                outputFile.WriteLine(line);
            }
        }
    }

    // Load entries from CSV
    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        // skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = ParseCsvLine(lines[i]);
            if (parts.Length == 3)
            {
                Entry e = new Entry(parts[0], parts[1], parts[2]);
                _entries.Add(e);
            }
        }
    }

    // Helper: escape commas/quotes
    private string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\""))
        {
            field = "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        return field;
    }


    private string[] ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"' && !inQuotes)
            {
                inQuotes = true;
            }
            else if (c == '"' && inQuotes)
            {
                inQuotes = false;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }
        fields.Add(current);
        return fields.ToArray();
    }
}
