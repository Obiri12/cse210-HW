using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Base class for all types of goals
abstract class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool Completed { get; set; }

    public abstract void RecordEvent();
}

// Simple goal class
class SimpleGoal : Goal
{
    public override void RecordEvent()
    {
        Completed = true;
    }
}

// Eternal goal class
class EternalGoal : Goal
{
    public override void RecordEvent()
    {
        Value += 100; // For example, every time you read scriptures, you gain 100 points
    }
}

// Checklist goal class
class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _totalRequired;

    public ChecklistGoal(int totalRequired)
    {
        _totalRequired = totalRequired;
    }

    public override void RecordEvent()
    {
        _timesCompleted++;
        Value += 50; // For example, every time you attend the temple, you gain 50 points
        if (_timesCompleted == _totalRequired)
        {
            Value += 500; // Bonus for completing all checklist items
            Completed = true;
        }
    }

    public override string ToString()
    {
        return $"Completed {_timesCompleted}/{_totalRequired} times";
    }
}

// Main program
class Program
{
    private static List<Goal> goals = new List<Goal>();

    static void Main(string[] args)
    {
        LoadGoals(); // Load previously saved goals
        int choice;
        do
        {
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter your choice:");
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddGoal();
                    break;
                case 2:
                    RecordEvent();
                    break;
                case 3:
                    ShowGoals();
                    break;
                case 4:
                    ShowScore();
                    break;
                case 5:
                    SaveGoals();
                    break;
                case 6:
                    Console.WriteLine("Exiting program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }
        } while (choice != 6);
    }

    static void AddGoal()
    {
        Console.WriteLine("Enter the name of the goal:");
        string name = Console.ReadLine();
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        int type;
        if (!int.TryParse(Console.ReadLine(), out type) || type < 1 || type > 3)
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
            return;
        }

        Goal goal;
        switch (type)
        {
            case 1:
                goal = new SimpleGoal();
                break;
            case 2:
                goal = new EternalGoal();
                break;
            case 3:
                Console.WriteLine("Enter the total number of times required to complete the checklist:");
                int totalRequired;
                if (!int.TryParse(Console.ReadLine(), out totalRequired) || totalRequired <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                    return;
                }
                goal = new ChecklistGoal(totalRequired);
                break;
            default:
                throw new NotImplementedException();
        }

        goal.Name = name;
        goals.Add(goal);
        Console.WriteLine("Goal added successfully.");
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals added yet.");
            return;
        }

        Console.WriteLine("Select a goal to record event for:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        int index;
        if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > goals.Count)
        {
            Console.WriteLine("Invalid input. Please enter a number corresponding to a goal.");
            return;
        }

        goals[index - 1].RecordEvent();
        Console.WriteLine("Event recorded successfully.");
    }

    static void ShowGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals added yet.");
            return;
        }

        Console.WriteLine("Current Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} - {(goals[i].Completed ? "[X]" : "[ ]")} {(goals[i] is ChecklistGoal ? goals[i].ToString() : "")}");
        }
    }

    static void ShowScore()
    {
        int totalScore = goals.Sum(g => g.Value);
        Console.WriteLine($"Total Score: {totalScore}");
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Value},{goal.Completed}");
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Goal goal;
                    switch (parts[0])
                    {
                        case nameof(SimpleGoal):
                            goal = new SimpleGoal();
                            break;
                        case nameof(EternalGoal):
                            goal = new EternalGoal();
                            break;
                        case nameof(ChecklistGoal):
                            int totalRequired = int.Parse(parts[1]);
                            goal = new ChecklistGoal(totalRequired);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    goal.Name = parts[1];
                    goal.Value = int.Parse(parts[2]);
                    goal.Completed = bool.Parse(parts[3]);
                    goals.Add(goal);
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}