using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TaskManager.Tests
{
    [TestClass]
    public class TaskManagerTests
    {
        private readonly TaskManager.TaskManager _manager = TaskManager.TaskManager.Instance;

        [TestInitialize]
        public void TestInitialize()
        {
            // Clear tasks before each test
            var tasks = _manager.GetAllTasks().ToList();
            foreach (var task in tasks)
            {
                _manager.RemoveTask(task.Id);
            }
        }

        [TestMethod]
        public void AddTask_ValidInput_CreatesTask()
        {
            // Arrange
            var initialCount = _manager.GetAllTasks().Count();

            // Act
            _manager.AddTask("Test", "Description", DateTime.Now.AddDays(1), TaskManager.TaskPriority.Medium);

            // Assert
            var tasks = _manager.GetAllTasks();
            Assert.AreEqual(initialCount + 1, tasks.Count());
            Assert.AreEqual("Test", tasks.First().Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTask_PastDueDate_ThrowsException()
        {
            _manager.AddTask("Invalid", "Task", DateTime.Now.AddDays(-1), TaskManager.TaskPriority.Low);
        }

        [TestMethod]
        public void UpdateTask_ExistingTask_UpdatesProperties()
        {
            // Arrange
            _manager.AddTask("Original", "Desc", DateTime.Now.AddDays(2), TaskManager.TaskPriority.Low);
            var task = _manager.GetAllTasks().First();

            // Act
            var success = _manager.UpdateTask(task.Id, "Updated", "New Desc", DateTime.Now.AddDays(3), TaskManager.TaskPriority.High);

            // Assert
            Assert.IsTrue(success);
            var updated = _manager.GetAllTasks().First();
            Assert.AreEqual("Updated", updated.Title);
            Assert.AreEqual(TaskManager.TaskPriority.High, updated.Priority);
        }

        [TestMethod]
        public void RemoveTask_ExistingTask_ReturnsTrue()
        {
            // Arrange
            _manager.AddTask("ToRemove", "Desc", DateTime.Now.AddDays(1), TaskManager.TaskPriority.Medium);
            var task = _manager.GetAllTasks().First();

            // Act
            var result = _manager.RemoveTask(task.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _manager.GetAllTasks().Count());
        }

        [TestMethod]
        public void MarkCompleted_ExistingTask_UpdatesStatus()
        {
            // Arrange
            _manager.AddTask("Task", "Desc", DateTime.Now.AddDays(1), TaskManager.TaskPriority.Medium);
            var task = _manager.GetAllTasks().First();

            // Act
            var result = _manager.MarkCompleted(task.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_manager.GetAllTasks().First().Completed);
        }

        [TestMethod]
        public void FilterTasks_PendingFilter_ReturnsOnlyPending()
        {
            // Arrange
            _manager.AddTask("Pending", "1", DateTime.Now.AddDays(1), TaskManager.TaskPriority.Low);
            _manager.AddTask("Completed", "2", DateTime.Now.AddDays(2), TaskManager.TaskPriority.Medium);
            _manager.MarkCompleted(_manager.GetAllTasks().Last().Id);

            // Act
            var filtered = _manager.FilterTasks(new TaskManager.PendingFilter());

            // Assert
            Assert.AreEqual(1, filtered.Count());
            Assert.AreEqual("Pending", filtered.First().Title);
        }
    }
}