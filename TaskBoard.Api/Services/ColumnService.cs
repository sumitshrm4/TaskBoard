using TaskBoard.Api.Data;
using TaskBoard.Api.Models;
using TaskBoard.Api.Repositories;

namespace TaskBoard.Api.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columns;
        private readonly AppDbContext _db;

        public ColumnService(IColumnRepository columns, AppDbContext db)
        {
            _columns = columns;
            _db = db;
        }

        public async Task<Column> CreateColumnAsync(Column dto, CancellationToken ct = default)
        {
            dto.Id = Guid.NewGuid();
            await _columns.AddAsync(dto, ct);
            await _db.SaveChangesAsync(ct);
            return dto;
        }

        public async Task<IEnumerable<Column>> ListColumnsAsync(CancellationToken ct = default) =>
            await _columns.ListAllWithTasksAsync(ct);

        public async Task<Column?> GetColumnAsync(Guid id, CancellationToken ct = default) =>
            await _columns.GetByIdAsync(id, ct);
    }
}
