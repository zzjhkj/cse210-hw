using System;
using System.Collections.Generic;

// Base Activity class
abstract class Activity
{
    private DateTime _date;
    private int _length; // in minutes

    public Activity(DateTime date, int length)
    {
        _date = date;
        _length = length;
    }

    public DateTime GetDate()
    {
        return _date;
    }

    public int GetLength()
    {
        return _length;
    }

    // Abstract methods to be implemented in derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed(); // calculated as distance / time
    public abstract double GetPace();  // calculated as time / distance

    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} Activity ({_length} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min/mile";
    }
}

// Running class
class Running : Activity
{
    private double _distance; // Distance in miles

    public Running(DateTime date, int length, double distance) : base(date, length)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetLength()) * 60; // Speed = (distance / time) * 60 to get miles per hour
    }

    public override double GetPace()
    {
        return GetLength() / GetDistance(); // Pace = time / distance
    }

    public override string GetSummary()
    {
        return $"{GetDate().ToString("dd MMM yyyy")} Running ({GetLength()} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min/mile";
    }
}

// Cycling class
class Cycling : Activity
{
    private double _speed; // Speed in miles per hour

    public Cycling(DateTime date, int length, double speed) : base(date, length)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * GetLength()) / 60; // Distance = (speed * time) / 60 to get miles
    }

    public override double GetSpeed()
    {
        return _speed; // Speed is provided directly
    }

    public override double GetPace()
    {
        return 60 / GetSpeed(); // Pace = 60 / speed
    }

    public override string GetSummary()
    {
        return $"{GetDate().ToString("dd MMM yyyy")} Cycling ({GetLength()} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min/mile";
    }
}

// Swimming class
class Swimming : Activity
{
    private int _laps; // Number of laps (each lap is 50 meters)

    public Swimming(DateTime date, int length, int laps) : base(date, length)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return (_laps * 50) / 1000.0 * 0.62; // Distance in miles (1 lap = 50 meters, convert to miles)
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetLength()) * 60; // Speed = (distance / time) * 60 to get miles per hour
    }

    public override double GetPace()
    {
        return GetLength() / GetDistance(); // Pace = time / distance
    }

    public override string GetSummary()
    {
        return $"{GetDate().ToString("dd MMM yyyy")} Swimming ({GetLength()} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min/mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create instances of activities with no direct storage of calculated values
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),   // Running 30 mins, covering 3 miles
            new Cycling(new DateTime(2022, 11, 3), 60, 15.0),  // Cycling 60 mins at 15 mph
            new Swimming(new DateTime(2022, 11, 3), 40, 20)    // Swimming 40 mins with 20 laps
        };

        // Display the summary for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}