using System;
using PingDong.CQRS.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.CQRS.Extensions;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public class DumpContext : IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DumpContext(IMediator mediator)
        {
            _mediator = mediator.EnsureNotNull(nameof(mediator));
        }

        public IList<Venue> Venues { get; } = new List<Venue>();

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync<Guid, Venue>(Venues);

            return true;
        }

        public void Dispose()
        {
        }
    }
}
