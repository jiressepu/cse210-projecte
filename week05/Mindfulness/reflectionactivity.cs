using System;
using System.Collections.Generic;
using System.Threading;

public class ReflectionActivity : Activity
{
    private List<string> prompts;
    private List<string> questions;

    public ReflectionActivity(int duration)
        : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.", duration)
    {
        prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need."
        };

        questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?"
        };
    }

    public void Start()
    {
        StartMessage();
        Pause(3);
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(5);
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine(question);
            Pause(4);
        }
        EndMessage();
    }
}
