using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskBoard.Api.Data;

namespace TaskBoard.Api.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> _set;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _set = _db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _set.AddAsync(entity, ct);
        }

        public virtual async Task DeleteAsync(object id, CancellationToken ct = default)
        {
            var entity = await _set.FindAsync(new object[] { id }, ct);
            if (entity != null) _set.Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return await _set.Where(predicate).ToListAsync(ct);
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken ct = default)
        {
            return await _set.FindAsync(new object[] { id }, ct);
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync(CancellationToken ct = default)
        {
            return await _set.ToListAsync(ct);
        }

        public virtual Task UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            return Task.CompletedTask;
        }
    }
}
