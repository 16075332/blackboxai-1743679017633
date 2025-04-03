using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TaskManager.Tests
{
    [TestClass]
    public class FilterStrategyTests
    {
        [TestMethod]
        public void PendingFilter_ReturnsOnlyIncompleteTasks()
        {
            // Arrange
            var tasks = new System.Collections.Generic.List<TaskManager.Task>
            {
                new TaskManager.Task(1, "Pending 1", "Desc", DateTime.Now.AddDays(1), TaskManager.TaskPriority.High),
                new TaskManager.Task(2, "Completed", "Desc", DateTime.Now.AddDays(2), TaskManager.TaskPriority.Medium)
                {
                    Completed = true
                },
                new TaskManager.Task(3, "Pending 2", "Desc", DateTime.Now.AddDays(3), TaskManager.TaskPriority.Low)
            };

            var filter = new TaskManager.PendingFilter();

            // Act
            var result = filter.ApplyFilter(tasks);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(t => !t.Completed));
        }

        [TestMethod]
        public void CompletedFilter_ReturnsOnlyCompletedTasks()
        {
            // Arrange
            var tasks = new System.Collections.Generic.List<TaskManager.Task>
            {
                new TaskManager.Task(1, "Pending", "Desc", DateTime.Now.AddDays(1), TaskManager.TaskPriority.High),
                new TaskManager.Task(2, "Completed 1", "Desc", DateTime.Now.AddDays(2), TaskManager.TaskPriority.Medium)
                {
                    Completed = true
                },
                new TaskManager.Task(3, "Completed 2", "Desc", DateTime.Now.AddDays(3), TaskManager.TaskPriority.Low)
                {
                    Completed = true
                }
            };

            var filter = new TaskManager.CompletedFilter();

            // Act
            var result = filter.ApplyFilter(tasks);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(t => t.Completed));
        }

        [TestMethod]
        public void Filter_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var emptyList = Enumerable.Empty<TaskManager.Task>();
            var pendingFilter = new TaskManager.PendingFilter();
            var completedFilter = new TaskManager.CompletedFilter();

            // Act & Assert
            Assert.AreEqual(0, pendingFilter.ApplyFilter(emptyList).Count());
            Assert.AreEqual(0, completedFilter.ApplyFilter(emptyList).Count());
        }
    }
}