using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationDbContext _context;
        public TaskService(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            // Validate the input id parameter first
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be 0 or a negative integer", "Id");
            }
            
            var task = await _context.Tasks.FindAsync(id);

            return task ?? throw new KeyNotFoundException($"Task with id {id} not found");
        }

        /// <summary>
        /// Get all tasks from the table
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskModel>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Create task as per the values of the data transfer object supplied
        /// </summary>
        /// <param name="taskModelDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TaskModel> CreateTaskAsync(TaskModelDto taskModelDto)
        {
            if (string.IsNullOrWhiteSpace(taskModelDto.Status) || string.IsNullOrWhiteSpace(taskModelDto.Title))
            {
                throw new ArgumentException($"Fields like Title and Status cannot be null/empty");
            }

            var task = new TaskModel { Title = taskModelDto.Title, Description = taskModelDto.Description, Status = taskModelDto.Status, DueDateTime = taskModelDto.DueDateTime };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Update task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateTaskStatusByIdAsync(int id, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException($"Status cannot be null/empty");
            }
            var task = await GetTaskByIdAsync(id);
            task.Status = status;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTaskByIdAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
