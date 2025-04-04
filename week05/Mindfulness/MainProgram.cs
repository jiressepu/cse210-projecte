using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");
        Console.WriteLine("Choose an activity: ");
        Console.WriteLine("1. Breathing");
        Console.WriteLine("2. Reflection");
        Console.WriteLine("3. Listing");

        string activityChoice = Console.ReadLine();

        Console.WriteLine("Enter the duration for the activity in seconds: ");
        int duration = int.Parse(Console.ReadLine());

        Activity activity = null;

        switch (activityChoice)
        {
            case "1":
                activity = new BreathingActivity(duration);
                break;
            case "2":
                activity = new ReflectionActivity(duration);
                break;
            case "3":
                activity = new ListingActivity(duration);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        if (activity != null)
        {
            // Cast to specific type and call Start method
            if (activity is BreathingActivity breathingActivity)
            {
                breathingActivity.Start();
            }
            else if (activity is ReflectionActivity reflectionActivity)
            {
                reflectionActivity.Start();
            }
            else if (activity is ListingActivity listingActivity)
            {
                listingActivity.Start();
            }
        }
    }
}
