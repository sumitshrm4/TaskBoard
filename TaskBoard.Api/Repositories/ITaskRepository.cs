using TaskBoard.Api.Models;

namespace TaskBoard.Api.Repositories
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
         Task<IEnumerable<TaskItem>> ListByColumnAsync(Guid columnId, CancellationToken ct = default);
    }
}
