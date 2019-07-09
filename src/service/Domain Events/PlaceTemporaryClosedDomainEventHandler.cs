using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.DomainPlaces
{
    public class PlaceTemporaryClosedDomainEventHandler : INotificationHandler<PlaceTemporaryClosedDomainEvent>
    {
        private readonly ILogger _logger;

        public PlaceTemporaryClosedDomainEventHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(PlaceTemporaryClosedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            // TODO: Send integration event to notify all interesting parties

            return Task.CompletedTask;
        }
    }
}