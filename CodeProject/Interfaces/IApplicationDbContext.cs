using CodeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Interfaces
{
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Tasks table
        /// </summary>
        DbSet<TaskModel> Tasks { get; set; }

        /// <summary>
        /// Method to save changes to the db
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
