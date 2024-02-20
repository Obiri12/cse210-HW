using System;
using System.Collections.Generic;

// Enum to represent task priorities
public enum Priority {
    Low,
    Medium,
    High
}

// Task class representing individual tasks
class Task {
    // Properties
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }

    // Constructor
    public Task(string title, string description, DateTime dueDate, Priority priority) {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        Priority = priority;
    }

    // Method to mark task as completed
    public void MarkAsCompleted() {
        IsCompleted = true;
    }

    // Method to update task description
    public void UpdateDescription(string newDescription) {
        Description = newDescription;
    }

    // Method to update due date
    public void UpdateDueDate(DateTime newDueDate) {
        DueDate = newDueDate;
    }

    // Method to get task information
    public string GetTaskInfo() {
        return $"Title: {Title}\nDescription: {Description}\nDue Date: {DueDate}\nPriority: {Priority}\nCompleted: {IsCompleted}";
    }
}

// Category class representing task categories
class Category {
    // Properties
    public string Name { get; set; }
    public List<Task> Tasks { get; }

    // Constructor
    public Category(string name) {
        Name = name;
        Tasks = new List<Task>();
    }

    // Method to add task to category
    public void AddTask(Task task) {
        Tasks.Add(task);
    }

    // Method to remove task from category
    public void RemoveTask(Task task) {
        Tasks.Remove(task);
    }
}

// User class representing users of the task management application
class User {
    // Properties
    public string Username { get; }
    public List<Task> AssignedTasks { get; }

    // Constructor
    public User(string username) {
        Username = username;
        AssignedTasks = new List<Task>();
    }

    // Method to assign task to user
    public void AssignTask(Task task) {
        AssignedTasks.Add(task);
    }

    // Method to unassign task from user
    public void UnassignTask(Task task) {
        AssignedTasks.Remove(task);
    }
}

class Program {
    static void Main(string[] args) {
        // Initialize categories and users
        List<Category> categories = new List<Category>();
        List<User> users = new List<User>();

        while (true) {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Assign task to user");
            Console.WriteLine("3. Update task description");
            Console.WriteLine("4. Mark task as completed");
            Console.WriteLine("5. Display all tasks");
            Console.WriteLine("6. Exit");

            // Get user input
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input) {
                case "1":
                    // Create task
                    Console.Write("Enter task title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter due date (yyyy-mm-dd): ");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter task priority (Low, Medium, High): ");
                    Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);

                    // Create task object
                    Task newTask = new Task(title, description, dueDate, priority);
                    Console.WriteLine("Task created successfully!");
                    break;

                case "2":
                    // Assign task to user
                    if (categories.Count == 0 || users.Count == 0) {
                        Console.WriteLine("Please create at least one category and one user first.");
                        break;
                    }

                    Console.Write("Enter user's username: ");
                    string username = Console.ReadLine();
                    User selectedUser = users.Find(u => u.Username == username);
                    if (selectedUser == null) {
                        Console.WriteLine($"User '{username}' not found.");
                        break;
                    }

                    Console.Write("Enter task title to assign: ");
                    string taskTitle = Console.ReadLine();
                    Task selectedTask = categories.SelectMany(c => c.Tasks).FirstOrDefault(t => t.Title == taskTitle);
                    if (selectedTask == null) {
                        Console.WriteLine($"Task '{taskTitle}' not found.");
                        break;
                    }

                    selectedUser.AssignTask(selectedTask);
                    Console.WriteLine($"Task '{taskTitle}' assigned to user '{username}'.");
                    break;

                case "3":
                    // Update task description
                    Console.Write("Enter task title to update description: ");
                    string taskTitleToUpdate = Console.ReadLine();
                    Task taskToUpdate = categories.SelectMany(c => c.Tasks).FirstOrDefault(t => t.Title == taskTitleToUpdate);
                    if (taskToUpdate == null) {
                        Console.WriteLine($"Task '{taskTitleToUpdate}' not found.");
                        break;
                    }

                    Console.Write("Enter new description: ");
                    string newDescription = Console.ReadLine();
                    taskToUpdate.UpdateDescription(newDescription);
                    Console.WriteLine("Task description updated successfully.");
                    break;

                case "4":
                    // Mark task as completed
                    Console.Write("Enter task title to mark as completed: ");
                    string taskTitleToComplete = Console.ReadLine();
                    Task taskToComplete = categories.SelectMany(c => c.Tasks).FirstOrDefault(t => t.Title == taskTitleToComplete);
                    if (taskToComplete == null) {
                        Console.WriteLine($"Task '{taskTitleToComplete}' not found.");
                        break;
                    }

                    taskToComplete.MarkAsCompleted();
                    Console.WriteLine($"Task '{taskTitleToComplete}' marked as completed.");
                    break;

                case "5":
                    // Display all tasks
                    Console.WriteLine("\nAll tasks:");
                    foreach (Category category in categories) {
                        Console.WriteLine($"Category: {category.Name}");
                        foreach (Task task in category.Tasks) {
                            Console.WriteLine(task.GetTaskInfo());
                        }
                    }
                    break;

                case "6":
                    // Exit program
                    Console.WriteLine("Exiting program...");
                    return;

                default:
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 6.");
                    break;
            }
        }
    }
}