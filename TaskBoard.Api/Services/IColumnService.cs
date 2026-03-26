using TaskBoard.Api.Models;

namespace TaskBoard.Api.Services
{
    public interface IColumnService
    {
        Task<Column> CreateColumnAsync(Column dto, CancellationToken ct = default);
        Task<IEnumerable<Column>> ListColumnsAsync(CancellationToken ct = default);
        Task<Column?> GetColumnAsync(Guid id, CancellationToken ct = default);
    }
}