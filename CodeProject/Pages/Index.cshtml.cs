using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApplicationDbContext _context;
        private readonly ITaskService _taskService;

        //Property
        public List<TaskModel> tasks { get; set; } = new List<TaskModel>();

        public IndexModel(ILogger<IndexModel> logger, IApplicationDbContext context, ITaskService taskService)
        {
            _logger = logger;
            _context = context;
            _taskService = taskService;
        }

        public async Task OnGetAsync()
        {
            tasks = await _context.Tasks.ToListAsync();
        }

        public IActionResult OnPostDelete (int id)
        {
            try
            {
                _taskService.DeleteTaskByIdAsync(id);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                //Log the error with id info if deletion fails
                _logger.LogError(ex, "Error deleting task with id {id}", id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
