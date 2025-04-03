using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    public sealed class TaskManager
    {
        private static readonly Lazy<TaskManager> instance = 
            new Lazy<TaskManager>(() => new TaskManager());
        
        private readonly List<Task> tasks = new List<Task>();
        private int nextId = 1;

        public static TaskManager Instance => instance.Value;

        private TaskManager() { }

        public void AddTask(string title, string description, DateTime dueDate, TaskPriority priority)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            if (description == null) throw new ArgumentNullException(nameof(description));
            
            var task = new Task(nextId++, title, description, dueDate, priority);
            tasks.Add(task);
        }

        public bool UpdateTask(int id, string title, string description, DateTime dueDate, TaskPriority priority)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            if (description == null) throw new ArgumentNullException(nameof(description));
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;

            task.Title = title;
            task.Description = description;
            task.DueDate = dueDate;
            task.Priority = priority;
            return true;
        }

        public bool RemoveTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;

            return tasks.Remove(task);
        }

        public bool MarkCompleted(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;

            task.Completed = true;
            return true;
        }

        public IEnumerable<Task> GetAllTasks() => tasks;

        public IEnumerable<Task> FilterTasks(IFilterStrategy filterStrategy)
        {
            return filterStrategy.ApplyFilter(tasks);
        }
    }
}