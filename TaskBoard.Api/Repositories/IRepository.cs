using System.Linq.Expressions;

namespace TaskBoard.Api.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object id, CancellationToken ct = default);
        Task<IEnumerable<TEntity>> ListAsync(CancellationToken ct = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(object id, CancellationToken ct = default);
    }
}
