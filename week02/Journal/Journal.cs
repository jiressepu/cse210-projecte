using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<JournalEntry> _entries = new List<JournalEntry>();
    private static readonly List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I could do one thing today, what would it be?"
    };

    public void AddEntry()
    {
        Console.WriteLine("\nAnswer the following questions:\n");

        foreach (string prompt in _prompts)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine();

            Console.Write("How was your mood for this answer? (😃, 😐, 😢): ");
            string mood = Console.ReadLine();

            _entries.Add(new JournalEntry(prompt, response, mood));
        }
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToCsv()
    {
        Console.Write("Enter the filename to save (CSV format): ");
        string fileName = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("Date,Prompt,Response,Mood");

            foreach (var entry in _entries)
            {
                writer.WriteLine($"{entry.Date},\"{entry.Prompt}\",\"{entry.Response}\",{entry.Mood}");
            }
        }
    }

    public void LoadFromCsv()
    {
        Console.Write("Enter the filename to load: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            _entries.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (parts.Length == 4)
                {
                    _entries.Add(new JournalEntry(parts[1].Trim('"'), parts[2].Trim('"'), parts[3].Trim())
                    {
                        Date = parts[0].Trim()
                    });
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    public void DisplayRandomEntry()
    {
        if (_entries.Count > 0)
        {
            Random random = new Random();
            JournalEntry randomEntry = _entries[random.Next(_entries.Count)];

            Console.WriteLine("Random Past Entry:");
            Console.WriteLine(randomEntry);
        }
        else
        {
            Console.WriteLine("No journal entries available.");
        }
    }
}
