using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class PlaceFreedDomainEventHandler : EventBusDomainEventHandler, INotificationHandler<PlaceFreedDomainEvent>
    {
        public PlaceFreedDomainEventHandler(IEventBusPublisher eventBus)
            : base(eventBus)
        {
        }

        public async Task Handle(PlaceFreedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new PlaceOccupiedIntegrationEvent(domainEvent.PlaceId, domainEvent.PlaceName);
            
            await PublishAsync(domainEvent, integrationEvent);
        }
    }
}