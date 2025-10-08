using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CodeProject.Models
{
    /// <summary>
    /// Dedicated request model.
    /// </summary>
    public class TaskModelDto
    {
        /// <summary>
        /// Title of the task
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title should not be more than 100 characters")]
        public string Title {get; set;}

        /// <summary>
        /// Description of the task
        /// </summary>
        public string? Description {get; set;}

        /// <summary>
        /// Status of the task
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [StringLength(30, ErrorMessage = "Status cannot be more than 30 characters")]
        public string Status { get; set; }

        /// <summary>
        /// Due date of the task
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDateTime { get; set; }

        public TaskModelDto()
        {
            Title = string.Empty;
            Status = "New";
            DueDateTime = DateTime.MinValue;
        }
    }
}
