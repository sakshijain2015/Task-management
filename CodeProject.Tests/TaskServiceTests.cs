using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeProject.Data;
using CodeProject.Models;
using CodeProject.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CodeProject.Tests
{
    public class TaskServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            // Each test class instance gets a fresh in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _taskService = new TaskService(_context);
        }

        // Positive Tests

        [Fact]
        public async Task CreateTask_ShouldAddNewTask()
        {
            // Arrange
            var taskDto = new TaskModelDto
            {
                Title = "Test Task",
                Description = "Test Description",
                Status = "Pending",
                DueDateTime = DateTime.Now.AddDays(1)
            };

            // Act
            var task = await _taskService.CreateTaskAsync(taskDto);

            // Assert
            var savedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.NotNull(savedTask);
            Assert.Equal(taskDto.Title, savedTask.Title);
            Assert.Equal(taskDto.Description, savedTask.Description);
            Assert.Equal(taskDto.Status, savedTask.Status);
        }

        [Fact]
        public async Task GetTaskById_ShouldReturnCorrectTask()
        {
            // Arrange
            var task = new TaskModel { Title = "GetById Task", Status = "Pending", DueDateTime = DateTime.Now };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var fetchedTask = await _taskService.GetTaskByIdAsync(task.Id);

            // Assert
            Assert.NotNull(fetchedTask);
            Assert.Equal(task.Title, fetchedTask.Title);
        }

        [Fact]
        public async Task GetTasks_ShouldReturnAllTasks()
        {
            // Arrange
            _context.Tasks.AddRange(
                new TaskModel { Title = "Task1", Status = "Pending", DueDateTime = DateTime.Now },
                new TaskModel { Title = "Task2", Status = "Completed", DueDateTime = DateTime.Now }
            );
            await _context.SaveChangesAsync();

            // Act
            var tasks = await _taskService.GetTasksAsync();

            // Assert
            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public async Task UpdateTaskStatus_ShouldChangeStatus()
        {
            // Arrange
            var task = new TaskModel { Title = "UpdateStatus Task", Status = "Pending", DueDateTime = DateTime.Now };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            await _taskService.UpdateTaskStatusByIdAsync(task.Id, "Completed");

            // Assert
            var updatedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.Equal("Completed", updatedTask.Status);
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTask()
        {
            // Arrange
            var task = new TaskModel { Title = "Delete Task", Status = "Pending", DueDateTime = DateTime.Now };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            await _taskService.DeleteTaskByIdAsync(task.Id);

            // Assert
            var deletedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.Null(deletedTask);
        }

        // Negative Tests

        [Fact]
        public async Task GetTaskById_InvalidId_ShouldThrowKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _taskService.GetTaskByIdAsync(999);
            });
        }

        [Fact]
        public async Task GetTaskById_NegativeOrZeroId_ShouldThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.GetTaskByIdAsync(0));
            await Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.GetTaskByIdAsync(-5));
        }

        [Fact]
        public async Task CreateTask_WithEmptyTitleOrStatus_ShouldThrowArgumentException()
        {
            var invalidTask1 = new TaskModelDto { Title = "", Status = "Pending", DueDateTime = DateTime.Now };
            var invalidTask2 = new TaskModelDto { Title = "Task", Status = "", DueDateTime = DateTime.Now };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateTaskAsync(invalidTask1));
            await Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.CreateTaskAsync(invalidTask2));
        }

        [Fact]
        public async Task UpdateTaskStatus_InvalidId_ShouldThrowKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _taskService.UpdateTaskStatusByIdAsync(999, "Completed");
            });
        }

        [Fact]
        public async Task UpdateTaskStatus_EmptyStatus_ShouldThrowArgumentException()
        {
            var task = new TaskModel { Title = "Test Task", Status = "Pending", DueDateTime = DateTime.Now };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _taskService.UpdateTaskStatusByIdAsync(task.Id, "");
            });
        }

        [Fact]
        public async Task DeleteTask_InvalidId_ShouldThrowKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _taskService.DeleteTaskByIdAsync(999);
            });
        }
    }
}
