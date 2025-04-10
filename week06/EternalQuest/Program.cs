// Program.cs
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.LoadGoals("goals.txt");

        // Add the 4 default goals if none exist
        if (manager.GoalCount() == 0)
        {
            manager.AddDefaultGoals();
            manager.SaveGoals("goals.txt");
        }

        int choice = -1;
        while (choice != 0)
        {
            Console.WriteLine("\n--- Eternal Quest Menu ---");
            Console.WriteLine("1. Display Goals");
            Console.WriteLine("2. Create New Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    manager.DisplayGoals();
                    break;
                case 2:
                    manager.CreateGoal();
                    break;
                case 3:
                    manager.RecordEvent();
                    break;
                case 4:
                    manager.SaveGoals("goals.txt");
                    break;
                case 5:
                    manager.LoadGoals("goals.txt");
                    break;
            }
        }
    }
}

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public abstract string GetStatus();
    public abstract bool IsComplete();
    public abstract void RecordEvent();
    public abstract string Serialize();
}

class SimpleGoal : Goal
{
    private bool _completed = false;

    public override string GetStatus() => _completed ? "[X]" : "[ ]";
    public override bool IsComplete() => _completed;
    public override void RecordEvent() => _completed = true;
    public override string Serialize() => $"Simple:{Name},{Description},{Points},{_completed}";

    public static SimpleGoal Deserialize(string[] parts)
    {
        return new SimpleGoal
        {
            Name = parts[0],
            Description = parts[1],
            Points = int.Parse(parts[2]),
            _completed = bool.Parse(parts[3])
        };
    }
}

class EternalGoal : Goal
{
    public override string GetStatus() => "[∞]";
    public override bool IsComplete() => false;
    public override void RecordEvent() => Console.WriteLine($"Gained {Points} points!");
    public override string Serialize() => $"Eternal:{Name},{Description},{Points}";

    public static EternalGoal Deserialize(string[] parts)
    {
        return new EternalGoal
        {
            Name = parts[0],
            Description = parts[1],
            Points = int.Parse(parts[2])
        };
    }
}

class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Bonus { get; set; }

    public override string GetStatus() => $"[{CurrentCount}/{TargetCount}]";
    public override bool IsComplete() => CurrentCount >= TargetCount;

    public override void RecordEvent()
    {
        if (!IsComplete())
        {
            CurrentCount++;
            if (IsComplete())
                Console.WriteLine($"Bonus! Gained {Bonus} extra points!");
        }
    }

    public override string Serialize() => $"Checklist:{Name},{Description},{Points},{CurrentCount},{TargetCount},{Bonus}";

    public static ChecklistGoal Deserialize(string[] parts)
    {
        return new ChecklistGoal
        {
            Name = parts[0],
            Description = parts[1],
            Points = int.Parse(parts[2]),
            CurrentCount = int.Parse(parts[3]),
            TargetCount = int.Parse(parts[4]),
            Bonus = int.Parse(parts[5])
        };
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void DisplayGoals()
    {
        Console.WriteLine($"\nScore: {_score}");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].Name} - {_goals[i].Description}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        int type = int.Parse(Console.ReadLine());

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case 1:
                _goals.Add(new SimpleGoal { Name = name, Description = desc, Points = points });
                break;
            case 2:
                _goals.Add(new EternalGoal { Name = name, Description = desc, Points = points });
                break;
            case 3:
                Console.Write("Target Count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal { Name = name, Description = desc, Points = points, TargetCount = target, Bonus = bonus });
                break;
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Which goal did you complete?");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].Name}");
        }
        int index = int.Parse(Console.ReadLine()) - 1;
        _goals[index].RecordEvent();
        _score += _goals[index].Points;
        if (_goals[index] is ChecklistGoal g && g.IsComplete()) _score += g.Bonus;
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (Goal g in _goals)
            {
                writer.WriteLine(g.Serialize());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename)) return;
        string[] lines = File.ReadAllLines(filename);
        if (lines.Length == 0) return;

        _score = int.TryParse(lines[0], out int parsedScore) ? parsedScore : 0;
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line) || !line.Contains(":")) continue;

            string[] split = line.Split(":");
            if (split.Length != 2) continue;

            string type = split[0];
            string[] parts = split[1].Split(",");

            try
            {
                if (type == "Simple" && parts.Length == 4)
                    _goals.Add(SimpleGoal.Deserialize(parts));
                else if (type == "Eternal" && parts.Length == 3)
                    _goals.Add(EternalGoal.Deserialize(parts));
                else if (type == "Checklist" && parts.Length == 6)
                    _goals.Add(ChecklistGoal.Deserialize(parts));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goal on line {i + 1}: {ex.Message}");
            }
        }
    }

    public int GoalCount() => _goals.Count;

    public void AddDefaultGoals()
    {
        _goals.Add(new SimpleGoal { Name = "Wake up at 6am", Description = "Start the day at 6:00 AM", Points = 50 });
        _goals.Add(new EternalGoal { Name = "Read the Book of Mormon", Description = "Read for 40 minutes daily", Points = 10 });
        _goals.Add(new ChecklistGoal { Name = "Exercise", Description = "Workout 3 times a week", Points = 30, TargetCount = 3, Bonus = 20 });
        _goals.Add(new ChecklistGoal { Name = "Pastoral service", Description = "Do pastoral service twice a week", Points = 25, TargetCount = 2, Bonus = 15 });
    }
}
