using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CodeProject.Pages
{
    public class ViewTaskModel : PageModel
    {
        private readonly ITaskService _taskService;

        public TaskModel Task { get; set; } = new TaskModel();

        public ViewTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Handle GET request and fetch the task details
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return NotFound();
                }

                Task = task;
                return Page();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}