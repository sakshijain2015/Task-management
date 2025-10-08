using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CodeProject.Models
{
    /// <summary>
    /// Dedicated update model.
    /// </summary>
    public class UpdateTaskModelDto
    {
        /// <summary>
        /// Status of the task
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [StringLength(30, ErrorMessage = "Status cannot be more than 30 characters")]
        public string Status { get; set; }

        public UpdateTaskModelDto()
        {
            Status = "New";
        }
    }
}
