using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.DomainPlaces
{
    public class PlaceOccupiedDomainEventHandler : INotificationHandler<PlaceOccupiedDomainEvent>
    {
        private readonly ILogger _logger;

        public PlaceOccupiedDomainEventHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(PlaceOccupiedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            // TODO: Send integration event to notify all interesting parties

            return Task.CompletedTask;
        }
    }
}
