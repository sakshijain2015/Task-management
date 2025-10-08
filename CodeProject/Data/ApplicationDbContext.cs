using CodeProject.Interfaces;
using CodeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Data
{
    /// <summary>
    /// EF core db context
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        /// <summary>
        /// app db context
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        /// <summary>
        /// Representing tasks table
        /// </summary>
        // Why: EF Core injects DbSet properties at runtime, so we use 'null!' to safely suppress nullable warnings.
        public DbSet<TaskModel> Tasks { get; set; } = null!;

        /// <summary>
        /// Save all changes made to the db
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
