using System;
using System.Collections.Generic;
using TODOCommon;
using TODOService;

namespace TODOApplication
{
    class Program
    {
        private static TaskService taskService = new TaskService();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                DisplayHeader();
                DisplayMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllTasks();
                        break;
                    case "2":
                        DisplayTasksByStatus();
                        break;
                    case "3":
                        AddNewTask();
                        break;
                    case "4":
                        UpdateTaskStatus();
                        break;
                    case "5":
                        UpdateTaskDescription();
                        break;
                    case "6":
                        DeleteTask();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║             TODO APPLICATION           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. View all tasks");
            Console.WriteLine("2. View tasks by status");
            Console.WriteLine("3. Add new task");
            Console.WriteLine("4. Update task status");
            Console.WriteLine("5. Update task description");
            Console.WriteLine("6. Delete task");
            Console.WriteLine("7. Exit");
            Console.Write("\nEnter your choice (1-7): ");
        }

        private static void DisplayAllTasks()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("ALL TASKS:");
            Console.WriteLine("-------------------------------------");

            List<TODOCommon.Task> tasks = taskService.GetAllTasks();
            DisplayTaskList(tasks);

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void DisplayTasksByStatus()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("SELECT STATUS:");
            Console.WriteLine("1. Not Started");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Done");
            Console.Write("\nEnter your choice (1-3): ");

            string choice = Console.ReadLine();
            string status = "";

            switch (choice)
            {
                case "1":
                    status = "Not Started";
                    break;
                case "2":
                    status = "In Progress";
                    break;
                case "3":
                    status = "Done";
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    return;
            }

            Console.Clear();
            DisplayHeader();
            Console.WriteLine($"TASKS WITH STATUS: {status}");
            Console.WriteLine("-------------------------------------");

            List<TODOCommon.Task> tasks = taskService.GetAllTasksByStatus(status);
            DisplayTaskList(tasks);

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void AddNewTask()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("ADD NEW TASK:");
            Console.WriteLine("-------------------------------------");

            // Get task description from user
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get task status from user
            Console.WriteLine("\nSelect task status:");
            Console.WriteLine("1. Not Started");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Done");
            Console.Write("\nEnter your choice (1-3): ");

            string choice = Console.ReadLine();
            string status = "";

            switch (choice)
            {
                case "1":
                    status = "Not Started";
                    break;
                case "2":
                    status = "In Progress";
                    break;
                case "3":
                    status = "Done";
                    break;
                default:
                    Console.WriteLine("Invalid option. Using default status 'Not Started'.");
                    status = "Not Started";
                    break;
            }

            // Add the new task
            taskService.AddTask(description, status);

            Console.WriteLine("\nTask added successfully!");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void UpdateTaskStatus()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("UPDATE TASK STATUS:");
            Console.WriteLine("-------------------------------------");

            DisplayAllTasksCompact();

            Console.Write("\nEnter task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int taskId))
            {
                Console.WriteLine("Invalid task ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nSelect new status:");
            Console.WriteLine("1. Not Started");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Done");
            Console.Write("\nEnter your choice (1-3): ");

            string choice = Console.ReadLine();
            string newStatus = "";

            switch (choice)
            {
                case "1":
                    newStatus = "Not Started";
                    break;
                case "2":
                    newStatus = "In Progress";
                    break;
                case "3":
                    newStatus = "Done";
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    return;
            }

            taskService.UpdateTaskStatus(taskId, newStatus);
            Console.WriteLine($"Task status updated successfully!");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void UpdateTaskDescription()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("UPDATE TASK DESCRIPTION:");
            Console.WriteLine("-------------------------------------");

            DisplayAllTasksCompact();

            Console.Write("\nEnter task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int taskId))
            {
                Console.WriteLine("Invalid task ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Description cannot be empty. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            taskService.UpdateTaskDescription(taskId, newDescription);
            Console.WriteLine($"Task description updated successfully!");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void DeleteTask()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("DELETE TASK:");
            Console.WriteLine("-------------------------------------");

            DisplayAllTasksCompact();

            Console.Write("\nEnter task ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int taskId))
            {
                Console.WriteLine("Invalid task ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            taskService.DeleteTask(taskId);
            Console.WriteLine($"Task deleted successfully!");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        private static void DisplayTaskList(List<TODOCommon.Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Description",-30} {"Status",-15} {"Creation Date",-20} {"Modified Date",-20}");
            Console.WriteLine(new string('-', 90));

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.TaskId,-5} {task.Description,-30} {task.Status,-15} {task.CreationDate,-20:g} {task.ModifiedDate,-20:g}");
            }
        }

        private static void DisplayAllTasksCompact()
        {
            List<TODOCommon.Task> tasks = taskService.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Description",-30} {"Status",-15}");
            Console.WriteLine(new string('-', 50));

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.TaskId,-5} {task.Description,-30} {task.Status,-15}");
            }
        }
    }
}