using System.Runtime.CompilerServices;
using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeProject.Pages
{
    public class UpdateTaskModel : PageModel
    {
        private readonly ITaskService _taskService;

        [BindProperty]
        public TaskModel Task { get; set; } = new TaskModel();

        /// <summary>
        /// update task mdoel
        /// </summary>
        /// <param name="taskService"></param>
        public UpdateTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get task to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return RedirectToPage("/Index");
                }

                // Binding to page model
                Task = task;

                return Page();
            }

            catch (KeyNotFoundException)
            {
                return RedirectToPage("/Index");
            }
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _taskService.UpdateTaskStatusByIdAsync(Task.Id, Task.Status);
                return RedirectToPage("/Index");
            }
            catch (KeyNotFoundException)
            {
                ModelState.AddModelError(string.Empty, $"Task with ID {Task.Id} not found.");
                return Page();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
