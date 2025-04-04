using System;
using System.Threading;

public class Activity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

    public Activity(string name, string description, int duration)
    {
        Name = name;
        Description = description;
        Duration = duration;
    }

    public void StartMessage()
    {
        Console.WriteLine($"Starting the {Name} activity!");
        Console.WriteLine(Description);
        Console.WriteLine($"Set your duration: {Duration} seconds.");
        Console.WriteLine("Prepare yourself...");
    }

    public void EndMessage()
    {
        Console.WriteLine($"Well done! You have completed the {Name} activity.");
        Console.WriteLine($"Total time spent: {Duration} seconds.");
        Console.WriteLine("Thank you for participating!");
    }

    public void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".", Console.CursorLeft = 0); // Animate by printing a dot each second
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}
