using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingDong.CQRS.Infrastructure
{
    public interface IRepository<TId, TEntity> where TEntity: Entity<TId>
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IList<TEntity>> ListAsync();

        Task AddAsync(TEntity entity);

        Task AddAsync(IList<TEntity> entities);

        Task RemoveAsync(TId id);

        Task RemoveAsync(IList<TId> id);

        Task UpdateAsync(TEntity entity);
        
        Task UpdateAsync(IList<TEntity> entities);

        Task<TEntity> FindByIdAsync(TId id, bool throwIfMissing);
    }
}
