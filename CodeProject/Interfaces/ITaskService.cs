using CodeProject.Models;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// ITask service interface
    /// </summary>
    public interface ITaskService
    {
        // 'Task' repreents asynchronous operation
        Task<TaskModel> GetTaskByIdAsync(int id);
        Task<List<TaskModel>> GetTasksAsync();
        Task UpdateTaskStatusByIdAsync(int id, string status);
        Task DeleteTaskByIdAsync(int id);
        Task<TaskModel> CreateTaskAsync(TaskModelDto taskModelDto);
    }
}
