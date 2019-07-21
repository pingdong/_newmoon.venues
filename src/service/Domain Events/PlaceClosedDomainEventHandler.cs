using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class PlaceClosedDomainEventHandler : DomainEventHandler, INotificationHandler<PlaceClosedDomainEvent>
    {
        public PlaceClosedDomainEventHandler(IEventBusPublisher eventBus, IMediator mediator)
            : base(eventBus, mediator)
        {
        }

        public async Task Handle(PlaceClosedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new PlaceClosedIntegrationEvent(domainEvent.PlaceId, domainEvent.PlaceName);
            
            await PublishAsync(integrationEvent, domainEvent);
        }
    }
}