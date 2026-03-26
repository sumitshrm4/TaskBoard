using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.Models;

namespace TaskBoard.Api.Repositories
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext db) : base(db) { }

        public async Task<IEnumerable<TaskItem>> ListByColumnAsync(Guid columnId, CancellationToken ct = default) =>
            await _set.Where(t => t.ColumnId == columnId)
                      .OrderByDescending(t => t.IsFavorited)
                      .ThenBy(t => t.SortOrder)
                      .ToListAsync(ct);
    }
}
