using System;
using System.Threading.Tasks;
using MediatR;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    internal class EventCanceledIntegrationEventHandler : CommandIntegrationEventHandler, IIntegrationEventHandler<EventCanceledIntegrationEvent>
    {
        public EventCanceledIntegrationEventHandler(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event">       
        /// </param>
        /// <returns></returns>
        public async Task Handle(EventCanceledIntegrationEvent @event)
        {
            if (@event == null || @event.PlaceId == Guid.Empty)
                return;

            var command = new PlaceFreeCommand(@event.PlaceId);

            await CommandDispatchAsync(@event, command);
        }
    }
}

