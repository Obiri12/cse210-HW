using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum Priority {
    Low,
    Medium,
    High
}

public class Task {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }

    public Task(string title, string description, DateTime dueDate, Priority priority) {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        Priority = priority;
    }

    public void MarkAsCompleted() {
        IsCompleted = true;
    }

    public void UpdateDescription(string newDescription) {
        Description = newDescription;
    }

    public void UpdateDueDate(DateTime newDueDate) {
        DueDate = newDueDate;
    }

    public string GetTaskInfo() {
        return $"Title: {Title}\nDescription: {Description}\nDue Date: {DueDate}\nPriority: {Priority}\nCompleted: {IsCompleted}";
    }

    public string SerializeTask() {
        return $"{Title},{Description},{DueDate},{IsCompleted},{Priority}";
    }

    public static Task DeserializeTask(string taskString) {
        string[] parts = taskString.Split(',');
        string title = parts[0];
        string description = parts[1];
        DateTime dueDate = DateTime.Parse(parts[2]);
        bool isCompleted = bool.Parse(parts[3]);
        Priority priority = (Priority)Enum.Parse(typeof(Priority), parts[4]);
        return new Task(title, description, dueDate, priority) { IsCompleted = isCompleted };
    }
}

public class Program {
    static List<Task> tasks = new List<Task>();
    static string filePath = "tasks.txt";

    static void Main(string[] args) {
        LoadTasksFromFile();

        while (true) {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. List tasks");
            Console.WriteLine("3. Mark task as completed");
            Console.WriteLine("4. Update task due date");
            Console.WriteLine("5. Update task description");
            Console.WriteLine("6. Delete task");
            Console.WriteLine("7. Save tasks");
            Console.WriteLine("8. Exit");

            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input) {
                case "1":
                    CreateTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted();
                    break;
                case "4":
                    UpdateTaskDueDate();
                    break;
                case "5":
                    UpdateTaskDescription();
                    break;
                case "6":
                    DeleteTask();
                    break;
                case "7":
                    SaveTasksToFile();
                    break;
                case "8":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 8.");
                    break;
            }
        }
    }

    static void LoadTasksFromFile() {
        if (File.Exists(filePath)) {
            string[] lines = File.ReadAllLines(filePath);
            tasks.Clear();
            foreach (string line in lines) {
                tasks.Add(Task.DeserializeTask(line));
            }
        }
    }

    static void SaveTasksToFile() {
        List<string> lines = new List<string>();
        foreach (Task task in tasks) {
            lines.Add(task.SerializeTask());
        }
        File.WriteAllLines(filePath, lines);
        Console.WriteLine("Tasks saved successfully.");
    }

    static void CreateTask() {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        Console.Write("Enter due date (yyyy-mm-dd): ");
        DateTime dueDate;
        if (!DateTime.TryParse(Console.ReadLine(), out dueDate)) {
            Console.WriteLine("Invalid date format. Task creation failed.");
            return;
        }
        Console.Write("Enter task priority (Low, Medium, High): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out Priority priority)) {
            Console.WriteLine("Invalid priority. Task creation failed.");
            return;
        }

        tasks.Add(new Task(title, description, dueDate, priority));
        Console.WriteLine("Task created successfully!");
    }

    static void ListTasks() {
        Console.WriteLine("\nAll tasks:");
        foreach (Task task in tasks) {
            Console.WriteLine(task.GetTaskInfo());
            Console.WriteLine();
        }
    }

    static void MarkTaskAsCompleted() {
        Console.Write("Enter task title to mark as completed: ");
        string title = Console.ReadLine();
        Task task = tasks.FirstOrDefault(t => t.Title == title);
        if (task == null) {
            Console.WriteLine($"Task '{title}' not found.");
            return;
        }
        task.MarkAsCompleted();
        Console.WriteLine($"Task '{title}' marked as completed.");
    }

    static void UpdateTaskDueDate() {
        Console.Write("Enter task title to update due date: ");
        string title = Console.ReadLine();
        Task task = tasks.FirstOrDefault(t => t.Title == title);
        if (task == null) {
            Console.WriteLine($"Task '{title}' not found.");
            return;
        }
        Console.Write("Enter new due date (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDueDate)) {
            Console.WriteLine("Invalid date format. Due date update failed.");
            return;
        }
        task.UpdateDueDate(newDueDate);
        Console.WriteLine($"Due date updated successfully for task '{title}'.");
    }

    static void UpdateTaskDescription() {
        Console.Write("Enter task title to update description: ");
        string title = Console.ReadLine();
        Task task = tasks.FirstOrDefault(t => t.Title == title);
        if (task == null) {
            Console.WriteLine($"Task '{title}' not found.");
            return;
        }
        Console.Write("Enter new description: ");
        string newDescription = Console.ReadLine();
        task.UpdateDescription(newDescription);
        Console.WriteLine($"Description updated successfully for task '{title}'.");
    }

    static void DeleteTask() {
        Console.Write("Enter task title to delete: ");
        string title = Console.ReadLine();
        Task task = tasks.FirstOrDefault(t => t.Title == title);
        if (task == null) {
            Console.WriteLine($"Task '{title}' not found.");
            return;
        }
        tasks.Remove(task);
        Console.WriteLine($"Task '{title}' deleted successfully.");
    }
}