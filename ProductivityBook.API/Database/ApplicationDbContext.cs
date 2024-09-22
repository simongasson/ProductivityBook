using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Features.TaskGroupFeature;
using ProductivityBook.API.Features.TaskItemFeature;

namespace ProductivityBook.API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<TaskGroup> TaskGroups { get; set; }
    }
}
