using CodeProject.Interfaces;
using CodeProject.Models;
using CodeProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Controllers
{
    /// <summary>
    /// Endpoints for the task
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// task service
        /// </summary>
        private readonly ITaskService _taskService;

        /// <summary>
        /// Task controller constructor
        /// </summary>
        /// <param name="taskService"></param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTaskById")]
        public async Task<IActionResult> GetTaskByIdAsync(int id)
        {
            try {
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not found",
                    Detail = $"No record found with {id} id",
                });
            }
        }

        /// <summary>
        /// Gets all the task records
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllTasks")]
        public async Task<IActionResult> GetTasksAsync()
        {

            var taskList = await _taskService.GetTasksAsync();
            return Ok(taskList);

        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="taskModelDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost(Name = "CreateTask")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskModelDto taskModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _taskService.CreateTaskAsync(taskModelDto);

            return CreatedAtRoute(
                routeName: "GetTaskById",
                routeValues: new { id = task.Id },
                value: task);
        }

        /// <summary>
        /// Update a task record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTaskModelDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPatch("{id}/status", Name = "UpdateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatusByIdAsync(int id, [FromBody] UpdateTaskModelDto updateTaskModelDto)
        {
            try
            {
                await _taskService.UpdateTaskStatusByIdAsync(id, updateTaskModelDto.Status);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Incorrect input params",
                    Detail = ex.Message,
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not found",
                    Detail = $"No record found with {id} id",
                });
            }
        }

        /// <summary>
        /// Deletes a task by input id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTaskByIdAsync(int id)
        {
            try
            {
                await _taskService.DeleteTaskByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not found",
                    Detail = $"No record found with {id} id",
                });
            }
        }
    }
}
