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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var ids = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            modelBuilder.Entity<TaskGroup>().HasData(
                new { Id = ids[0], Date = DateTimeOffset.UtcNow.AddDays(-3) },
                new { Id = ids[1], Date = DateTimeOffset.UtcNow.AddDays(-2) },
                new { Id = ids[2], Date = DateTimeOffset.UtcNow.AddDays(-1) },
                new { Id = ids[3], Date = DateTimeOffset.UtcNow }
             );

            modelBuilder.Entity<TaskItem>().HasData(
                new { Id = Guid.NewGuid(), TaskGroupId = ids[0], Title = "Task 1", IsCompleted = false },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[0], Title = "Task 2", IsCompleted = true },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[1], Title = "Task 3", IsCompleted = true },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[1], Title = "Task 4", IsCompleted = false },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[2], Title = "Task 5", IsCompleted = true },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[2], Title = "Task 6", IsCompleted = true },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[3], Title = "Task 7", IsCompleted = false },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[3], Title = "Task 8", IsCompleted = false },
                new { Id = Guid.NewGuid(), TaskGroupId = ids[3], Title = "Task 9", IsCompleted = false }
             );

        }
    }
}
