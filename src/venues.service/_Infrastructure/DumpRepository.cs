using PingDong.CQRS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class DumpRepository : IRepository<Guid, Venue>
    {
        private readonly DumpContext _context;

        public DumpRepository(DumpContext context)
        {
            _context = context.EnsureNotNull(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task<IList<Venue>> ListAsync()
        {
            return Task.FromResult(_context.Venues);
        }

        public Task AddAsync(Venue entity)
        {
            _context.Venues.Add(entity);

            return Task.CompletedTask;
        }

        public Task AddAsync(IList<Venue> entities)
        {
            foreach (var entity in entities)
                _context.Venues.Add(entity);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(Guid id)
        {
            var entity = _context.Venues.FirstOrDefault(s => s.Id == id);
            if (entity != null)
                _context.Venues.Remove(entity);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(IList<Guid> id)
        {
            foreach (var entityId in id)
            {
                var entity = _context.Venues.FirstOrDefault(s => s.Id == entityId);
                if (entity != null)
                    _context.Venues.Remove(entity);
            }
            
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Venue entity)
        {
            var existing = _context.Venues.FirstOrDefault(s => s.Id == entity.Id);
            if (existing != null)
                _context.Venues.Remove(existing);
            
            _context.Venues.Add(entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(IList<Venue> entities)
        {
            foreach (var entity in entities)
            {
                var existing = _context.Venues.FirstOrDefault(s => s.Id == entity.Id);
                if (existing != null)
                    _context.Venues.Remove(existing);

                _context.Venues.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task<Venue> FindByIdAsync(Guid id, bool throwIfMissing = true)
        {
            var entity = _context.Venues.FirstOrDefault(s => s.Id == id);
            if (entity == null && throwIfMissing)
                throw new NullReferenceException();

            return Task.FromResult(entity);
        }
    }
}
