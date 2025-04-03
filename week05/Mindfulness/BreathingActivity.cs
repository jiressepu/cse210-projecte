using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity(int duration)
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly.", duration)
    {
    }

    public void Start()
    {
        StartMessage();
        Pause(3); // Pause before starting
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Pause(4);
            Console.WriteLine("Breathe out...");
            Pause(4);
        }
        EndMessage();
    }
}
