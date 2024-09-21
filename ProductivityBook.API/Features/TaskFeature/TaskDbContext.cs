using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Features.TaskFeature.Models;

namespace ProductivityBook.API.Features.TaskFeature
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
