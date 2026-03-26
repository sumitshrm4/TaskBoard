using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.Models;

namespace TaskBoard.Api.Repositories
{
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(AppDbContext db) : base(db) { }

        public async Task<Column?> GetWithTasksAsync(Guid id, CancellationToken ct = default) =>
            await _db.Columns.Include(c => c.Tasks).FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<IEnumerable<Column>> ListAllWithTasksAsync(CancellationToken ct = default) =>
            await _db.Columns.Include(c => c.Tasks).OrderBy(c => c.Order).ToListAsync(ct);
    }
}