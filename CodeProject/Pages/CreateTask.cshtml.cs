using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeProject.Pages
{
    public class CreateTaskModel : PageModel
    {
        private readonly ITaskService _taskService; 
        
        [BindProperty]
        public TaskModelDto TaskDto { get; set; } = new TaskModelDto();

        /// <summary>
        /// assign task service
        /// </summary>
        /// <param name="taskService"></param>
        public CreateTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get method
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// Async Post method
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
                await _taskService.CreateTaskAsync(TaskDto);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }            
        }
    }
}
