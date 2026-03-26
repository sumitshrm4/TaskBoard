using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Models;

namespace TaskBoard.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<Column> Columns => Set<Column>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Column>().HasData(
                new Column { Id = Guid.NewGuid(), Name = "ToDo", Order = 0 },
                new Column { Id = Guid.NewGuid(), Name = "In Progress", Order = 1 },
                new Column { Id = Guid.NewGuid(), Name = "Done", Order = 2 }
            );
        }
    }
}
