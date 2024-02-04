using System;
using System.Threading;

// Base class for all activities
class MindfulnessActivity
{
    protected string name;
    protected string description;
    protected int duration;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public virtual void StartActivity()
    {
        Console.WriteLine($"Welcome to the {name} activity!");
        Console.Write($"{description}\nEnter the duration of the activity in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Get ready to begin...");
        Thread.Sleep(3000); // Pause for preparation
    }

    public virtual void EndActivity()
    {
        Console.WriteLine("Great job! You've completed the activity.");
        Console.WriteLine($"You've completed the {name} activity for {duration} seconds.");
        Thread.Sleep(3000); // Pause before finishing
    }
}

// Derived class for Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();

        while (duration > 0)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000); // Pause for breathing in

            duration -= 3;

            if (duration <= 0)
                break;

            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000); // Pause for breathing out

            duration -= 3;
        }

        base.EndActivity();
    }
}

// Derived class for Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private string[] reflectionPrompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();

        while (duration > 0)
        {
            string prompt = reflectionPrompts[new Random().Next(reflectionPrompts.Length)];
            Console.WriteLine($"Think of a time when: {prompt}");
            Thread.Sleep(3000); // Pause for reflection prompt

            duration -= 3;

            AskRandomReflectionQuestion();
            duration -= 3;
        }

        base.EndActivity();
    }

    private void AskRandomReflectionQuestion()
    {
        string question = reflectionQuestions[new Random().Next(reflectionQuestions.Length)];
        Console.WriteLine(question);
        Thread.Sleep(3000); // Pause for reflection question
    }
}

// Derived class for Listing Activity
class ListingActivity : MindfulnessActivity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void StartActivity()
    {
        base.StartActivity();

        string prompt = listingPrompts[new Random().Next(listingPrompts.Length)];
        Console.WriteLine($"List as many things as you can about: {prompt}");
        Thread.Sleep(5000); // Pause for initial reflection

        Console.WriteLine("Start listing...");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item))
                break;

            itemCount++;
        }

        Console.WriteLine($"You listed {itemCount} items.");

        base.EndActivity();
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            ShowMenu();
            int choice = GetMenuChoice();

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    Console.WriteLine("Thank you for using the Mindfulness App. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select again.");
                    break;
            }

            activity?.StartActivity();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Quit");
    }

    static int GetMenuChoice()
    {
        Console.Write("Enter your choice: ");
        return Convert.ToInt32(Console.ReadLine());
    }
}