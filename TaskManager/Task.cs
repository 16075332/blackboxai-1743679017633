using System;

namespace TaskManager
{
    public class Task
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public bool Completed { get; set; }

        public Task(int id, string title, string description, DateTime dueDate, TaskPriority priority)
        {
            if (dueDate < DateTime.Today)
                throw new ArgumentException("Due date cannot be in the past");
            
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required");

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Completed = false;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Title} (Due: {DueDate:yyyy-MM-dd}, Priority: {Priority}, Status: {(Completed ? "Completed" : "Pending")})";
        }
    }
}