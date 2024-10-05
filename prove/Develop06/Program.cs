using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public virtual string DisplayProgress() => $"{Name} - {Description}";
}

// SimpleGoal class for one-time goals
class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points) : base(name, description, points) {}

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return Points;
        }
        return 0; 
    }

    public override bool IsComplete() => _isComplete;

    public override string DisplayProgress() => $"{Name}: {(_isComplete ? "[X]" : "[ ]")}";
}

// EternalGoal class for goals that are repeated
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) {}

    public override int RecordEvent() => Points; 

    public override bool IsComplete() => false;

    public override string DisplayProgress() => $"{Name}: Eternal Goal (ongoing)";
}

// ChecklistGoal class for goals that must be completed a certain number of times
class ChecklistGoal : Goal
{
    public int RequiredCount { get; set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, string description, int points, int requiredCount, int bonusPoints)
        : base(name, description, points)
    {
        RequiredCount = requiredCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (CurrentCount < RequiredCount)
        {
            CurrentCount++;
            if (CurrentCount == RequiredCount)
                return Points + BonusPoints; 
            return Points;
        }
        return 0; 
    }

    public override bool IsComplete() => CurrentCount >= RequiredCount;

    public override string DisplayProgress() => $"{Name}: Completed {CurrentCount}/{RequiredCount} times.";
}

class EternalQuestProgram
{
    private List<Goal> goals = new List<Goal>();
    private int totalPoints = 0;

    public void Start()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine($"Total Points: {totalPoints}");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. Show all goals");
            Console.WriteLine("4. Save progress");
            Console.WriteLine("5. Load progress");
            Console.WriteLine("6. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    ShowGoals();
                    break;
                case "4":
                    SaveProgress();
                    break;
                case "5":
                    LoadProgress();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for this goal: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter number of times to complete this goal: ");
                int requiredCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points for completing: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, description, points, requiredCount, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    private void RecordEvent()
    {
        Console.WriteLine("Select a goal to record progress:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].DisplayProgress()}");
        }

        int goalIndex = int.Parse(Console.ReadLine()) - 1;
        int pointsEarned = goals[goalIndex].RecordEvent();
        totalPoints += pointsEarned;

        if (pointsEarned > 0)
            Console.WriteLine($"You earned {pointsEarned} points! Total: {totalPoints}");
        else
            Console.WriteLine("No points awarded, goal already completed.");
        Console.ReadLine();
    }

    private void ShowGoals()
    {
        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal.DisplayProgress());
        }
        Console.ReadLine();
    }

    private void SaveProgress()
    {
        using (StreamWriter writer = new StreamWriter("progress.txt"))
        {
            writer.WriteLine(totalPoints);
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}|{goal.Name}|{goal.Description}|{goal.Points}");
            }
        }
        Console.WriteLine("Progress saved.");
        Console.ReadLine();
    }

    private void LoadProgress()
    {
        if (File.Exists("progress.txt"))
        {
            using (StreamReader reader = new StreamReader("progress.txt"))
            {
                totalPoints = int.Parse(reader.ReadLine());
                goals.Clear();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string goalType = parts[0];
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);

                    if (goalType == "SimpleGoal")
                        goals.Add(new SimpleGoal(name, description, points));
                    else if (goalType == "EternalGoal")
                        goals.Add(new EternalGoal(name, description, points));
                    else if (goalType == "ChecklistGoal")
                        goals.Add(new ChecklistGoal(name, description, points, 0, 0)); 
                }
            }
            Console.WriteLine("Progress loaded.");
        }
        else
        {
            Console.WriteLine("No saved progress found.");
        }
        Console.ReadLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuestProgram questProgram = new EternalQuestProgram();
        questProgram.Start();
    }
}