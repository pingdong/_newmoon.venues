using MediatR;
using Microsoft.Extensions.Options;
using PingDong.CQRS.Services;
using PingDong.Messages;
using PingDong.Newmoon.Venues.Services.Commands;
using PingDong.Newmoon.Venues.Settings;
using System;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class EventCanceledIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<EventCanceledIntegrationEvent>
    {
        public EventCanceledIntegrationEventHandler(IMediator mediator, IOptionsMonitor<AppSettings> settings)
            : base(mediator, settings.CurrentValue.SupportIdempotencyCheck)
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
            if (@event == null || @event.VenueId == Guid.Empty)
                throw new IntegrationEventException("The Event Canceled integration event is invalid", @event);

            var command = new VenueVacateCommand(@event.VenueId);
            command.AppendTraceMetadata(@event);

            await DispatchAsync(command, @event);
        }
    }
}

