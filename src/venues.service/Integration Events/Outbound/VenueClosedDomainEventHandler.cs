using MediatR;
using PingDong.CQRS.Services;
using PingDong.Messages;
using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Newmoon.Venues.Services.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.DomainEvents
{
    public class VenueClosedDomainEventHandler : DomainEventHandler, INotificationHandler<VenueClosedDomainEvent>
    {
        public VenueClosedDomainEventHandler(IPublisher publisher, IMediator mediator)
            : base(publisher, mediator)
        {
        }

        public async Task Handle(VenueClosedDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            domainEvent.EnsureNotNull(nameof(domainEvent));

            var integrationEvent = new VenueClosedIntegrationEvent(domainEvent.VenueId);
            integrationEvent.AppendTraceMetadata(domainEvent);

            await PublishAsync(integrationEvent, domainEvent);
        }
    }
}