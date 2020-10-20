using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Core.Validation;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Infrastructure.SqlServer;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class GenericRepository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : Entity<TId>, IAggregateRoot 
    {
        private readonly DefaultDbContext _context;
        private readonly IEnumerable<IValidator<TEntity>> _validators;

        public GenericRepository(DefaultDbContext context, IEnumerable<IValidator<TEntity>> validators)
        {
            _context = context;
            _validators = validators;
        }

        #region IRepository
        
        public IUnitOfWork UnitOfWork => _context;

        public async Task<TEntity> FindByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IList<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }
        
        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!entity.IsTransient())
                return;

            _validators.Validate(entity);

            await _context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
        }
        
        public async Task RemoveAsync(TId id)
        {
            if(EqualityComparer<TId>.Default.Equals(id, default)) 
                throw new ArgumentNullException(nameof(id));

            var entity = await _context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            if (null == entity)
                throw new ArgumentOutOfRangeException(nameof(id));

            _context.Set<TEntity>().Remove(entity);
        }
        
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.IsTransient())
                throw new ArgumentException(nameof(entity));
            
            var existing = await _context.Set<TEntity>().FindAsync(entity.Id).ConfigureAwait(false);
            if (existing == null)
                throw new ArgumentOutOfRangeException(nameof(entity));

            _validators.Validate(entity);
            
            _context.Update(entity);
        }

        #endregion
    }
}
