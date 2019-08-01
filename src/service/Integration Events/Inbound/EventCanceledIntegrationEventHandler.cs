using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Service.Commands;

[assembly:InternalsVisibleTo("PingDong.Newmoon.Places.Service.Test")]
namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    internal class EventCanceledIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<EventCanceledIntegrationEvent>
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
                throw new IntegrationEventException(EventIds.InvalidIntegrationEvent, "The Event Canceled integration event is invalid", @event);

            var command = new PlaceFreeCommand(@event.PlaceId);

            await DispatchAsync(command, @event);
        }
    }
}

