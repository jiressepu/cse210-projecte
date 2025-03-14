using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; } // New: Mood added

    public JournalEntry(string prompt, string response, string mood)
    {
        Date = DateTime.Now.ToShortDateString();
        Prompt = prompt;
        Response = response;
        Mood = mood;
    }

    public override string ToString()
    {
        return $"{Date} | {Prompt} | {Response} | {Mood}";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private static readonly List<string> prompts = new List<string>
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
        foreach (string prompt in prompts)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            Console.Write("How was your mood for this answer? (😃, 😐, 😢): ");
            string mood = Console.ReadLine();
            entries.Add(new JournalEntry(prompt, response, mood));
        }
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToCSV()
    {
        Console.Write("Enter the filename to save (CSV format): ");
        string fileName = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("Date,Prompt,Response,Mood");
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date},\"{entry.Prompt}\",\"{entry.Response}\",{entry.Mood}");
            }
        }
    }

    public void LoadFromCSV()
    {
        Console.Write("Enter the filename to load: ");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            entries.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length == 4)
                {
                    entries.Add(new JournalEntry(parts[1].Trim('"'), parts[2].Trim('"'), parts[3].Trim()) { Date = parts[0].Trim() });
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
        if (entries.Count > 0)
        {
            Random rnd = new Random();
            JournalEntry randomEntry = entries[rnd.Next(entries.Count)];
            Console.WriteLine("Random Past Entry:");
            Console.WriteLine(randomEntry);
        }
        else
        {
            Console.WriteLine("No journal entries available.");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan; // Improved readability
        Journal journal = new Journal();
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to CSV");
            Console.WriteLine("4. Load journal from CSV");
            Console.WriteLine("5. Display a random past entry");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": journal.AddEntry(); break;
                case "2": journal.DisplayEntries(); break;
                case "3": journal.SaveToCSV(); break;
                case "4": journal.LoadFromCSV(); break;
                case "5": journal.DisplayRandomEntry(); break;
                case "6": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }
}
