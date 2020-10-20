using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class ClientRequestRepository<TId> : IClientRequestRepository<TId>
    {
        private readonly DbContext _context;

        public ClientRequestRepository(DefaultDbContext context)
        {
            _context = context;
        }

        #region IClientRequestRepository

        public async Task AddAsync(ClientRequest<TId> request)
        {
            await _context.Set<ClientRequest<TId>>().AddAsync(request).ConfigureAwait(false);
        }

        public async Task<ClientRequest<TId>> FindByIdAsync(TId id)
        {
            return await _context.Set<ClientRequest<TId>>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion
    }
}
