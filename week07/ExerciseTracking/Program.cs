using System;
using System.Collections.Generic;

public abstract class Activity
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }

    public Activity(DateTime date, int durationInMinutes)
    {
        Date = date;
        DurationInMinutes = durationInMinutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {GetType().Name} ({DurationInMinutes} min) - Distance: {GetDistance():F1}, Speed: {GetSpeed():F1}, Pace: {GetPace():F2} min per unit";
    }
}

public class Running : Activity
{
    public double Distance { get; set; } // in miles

    public Running(DateTime date, int durationInMinutes, double distance) : base(date, durationInMinutes)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / DurationInMinutes) * 60; // speed in mph
    }

    public override double GetPace()
    {
        return DurationInMinutes / Distance; // pace in min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Pace: {GetPace():F2} min per mile";
    }
}

public class Cycling : Activity
{
    public double Speed { get; set; } // in miles per hour

    public Cycling(DateTime date, int durationInMinutes, double speed) : base(date, durationInMinutes)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * DurationInMinutes) / 60; // distance in miles
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed; // pace in min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Pace: {GetPace():F2} min per mile";
    }
}

public class Swimming : Activity
{
    public int Laps { get; set; } // number of laps

    public Swimming(DateTime date, int durationInMinutes, int laps) : base(date, durationInMinutes)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return (Laps * 50) / 1000.0; // distance in km
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationInMinutes) * 60; // speed in km/h
    }

    public override double GetPace()
    {
        return DurationInMinutes / GetDistance(); // pace in min per km
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Pace: {GetPace():F2} min per km";
    }
}

public class Program
{
    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 30, 15.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
