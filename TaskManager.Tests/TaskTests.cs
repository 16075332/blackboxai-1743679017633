using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TaskManager.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TaskConstructor_ValidInput_CreatesTask()
        {
            // Arrange & Act
            var task = new TaskManager.Task(
                1,
                "Valid Title",
                "Description",
                DateTime.Now.AddDays(1),
                TaskManager.TaskPriority.High);

            // Assert
            Assert.AreEqual("Valid Title", task.Title);
            Assert.AreEqual(TaskManager.TaskPriority.High, task.Priority);
            Assert.IsFalse(task.Completed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TaskConstructor_EmptyTitle_ThrowsException()
        {
            new TaskManager.Task(
                1,
                "",
                "Description",
                DateTime.Now.AddDays(1),
                TaskManager.TaskPriority.Medium);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TaskConstructor_PastDueDate_ThrowsException()
        {
            new TaskManager.Task(
                1,
                "Title",
                "Description",
                DateTime.Now.AddDays(-1),
                TaskManager.TaskPriority.Low);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            var task = new TaskManager.Task(
                1,
                "Test",
                "Desc",
                new DateTime(2023, 12, 31),
                TaskManager.TaskPriority.Medium);

            // Act
            var result = task.ToString();

            // Assert
            StringAssert.Contains(result, "[ID: 1]");
            StringAssert.Contains(result, "Test");
            StringAssert.Contains(result, "2023-12-31");
            StringAssert.Contains(result, "Medium");
            StringAssert.Contains(result, "Pending");
        }
    }
}