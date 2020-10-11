using MediatR;
using PingDong.CQRS.Services;
using PingDong.Messages;
using PingDong.Newmoon.Venues.DomainEvents;
using PingDong.Newmoon.Venues.Services.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.DomainEvents
{
    public class VenueVacatedDomainEventHandler : DomainEventHandler, INotificationHandler<VenueVacatedDomainEvent>
    {
        public VenueVacatedDomainEventHandler(IPublisher publisher, IMediator mediator)
            : base(publisher, mediator)
        {
        }

        public async Task Handle(VenueVacatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            domainEvent.EnsureNotNull(nameof(domainEvent));

            var integrationEvent = new VenueVacatedIntegrationEvent(domainEvent.VenueId);
            integrationEvent.AppendTraceMetadata(domainEvent);

            await PublishAsync(integrationEvent, domainEvent);
        }
    }
}