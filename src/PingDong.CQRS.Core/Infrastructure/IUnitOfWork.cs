using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.CQRS.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
