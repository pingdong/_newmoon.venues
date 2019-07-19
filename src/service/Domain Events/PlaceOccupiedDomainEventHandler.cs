using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class PlaceOccupiedDomainEventHandler : DomainEventHandler, INotificationHandler<PlaceOccupiedDomainEvent>
    {
        public PlaceOccupiedDomainEventHandler(IEventBusPublisher eventBus, IMediator mediator)
            : base(eventBus, mediator)
        {
        }

        public async Task Handle(PlaceOccupiedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new PlaceOccupiedIntegrationEvent(domainEvent.PlaceId, domainEvent.PlaceName);
            
            await PublishAsync(integrationEvent, domainEvent);
        }
    }
}
