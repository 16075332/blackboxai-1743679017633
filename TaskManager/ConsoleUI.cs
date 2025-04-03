using System;
using System.Collections.Generic;

namespace TaskManager
{
    public class ConsoleUI
    {
        private readonly TaskManager taskManager = TaskManager.Instance;

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                var choice = GetMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        UpdateTask();
                        break;
                    case 3:
                        RemoveTask();
                        break;
                    case 4:
                        MarkCompleted();
                        break;
                    case 5:
                        ListTasks();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Task Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Update Task");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Mark Completed");
            Console.WriteLine("5. List Tasks");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        }

        private int GetMenuChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.Write("Invalid input. Enter a number between 1-6: ");
            }
            return choice;
        }

        private void AddTask()
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter description: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter due date (yyyy-mm-dd): ");
            DateTime dueDate;
            while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.Write("Invalid date format. Try again (yyyy-mm-dd): ");
            }

            Console.Write("Enter priority (High/Medium/Low): ");
            TaskPriority priority;
            while (!Enum.TryParse(Console.ReadLine(), true, out priority))
            {
                Console.Write("Invalid priority. Enter High/Medium/Low: ");
            }

            try
            {
                taskManager.AddTask(title, description, dueDate, priority);
                Console.WriteLine("Task added successfully!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void UpdateTask()
        {
            ListTasks();
            Console.Write("Enter task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            Console.Write("Enter new title: ");
            var title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter new description: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter new due date (yyyy-mm-dd): ");
            DateTime dueDate;
            while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.Write("Invalid date format. Try again (yyyy-mm-dd): ");
            }

            Console.Write("Enter new priority (High/Medium/Low): ");
            TaskPriority priority;
            while (!Enum.TryParse(Console.ReadLine(), true, out priority))
            {
                Console.Write("Invalid priority. Enter High/Medium/Low: ");
            }

            if (taskManager.UpdateTask(id, title, description, dueDate, priority))
            {
                Console.WriteLine("Task updated successfully!");
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RemoveTask()
        {
            ListTasks();
            Console.Write("Enter task ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            if (taskManager.RemoveTask(id))
            {
                Console.WriteLine("Task removed successfully!");
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void MarkCompleted()
        {
            ListTasks();
            Console.Write("Enter task ID to mark as completed: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            if (taskManager.MarkCompleted(id))
            {
                Console.WriteLine("Task marked as completed!");
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListTasks()
        {
            Console.WriteLine("\n1. All Tasks\n2. Pending Tasks\n3. Completed Tasks");
            Console.Write("Choose filter: ");
            int filterChoice;
            while (!int.TryParse(Console.ReadLine(), out filterChoice) || filterChoice < 1 || filterChoice > 3)
            {
                Console.Write("Invalid choice. Enter 1-3: ");
            }

            IEnumerable<Task> tasks;
            switch (filterChoice)
            {
                case 1:
                    tasks = taskManager.GetAllTasks();
                    break;
                case 2:
                    tasks = taskManager.FilterTasks(new PendingFilter());
                    break;
                case 3:
                    tasks = taskManager.FilterTasks(new CompletedFilter());
                    break;
                default:
                    tasks = taskManager.GetAllTasks();
                    break;
            }

            Console.WriteLine("\nTasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
            Console.WriteLine();
        }
    }
}