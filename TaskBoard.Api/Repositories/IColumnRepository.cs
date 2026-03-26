using TaskBoard.Api.Models;

namespace TaskBoard.Api.Repositories
{
    public interface IColumnRepository : IRepository<Column>
    {
        Task<Column?> GetWithTasksAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Column>> ListAllWithTasksAsync(CancellationToken ct = default);
    }
}