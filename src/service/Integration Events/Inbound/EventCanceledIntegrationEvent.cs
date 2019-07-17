using System;
using PingDong.EventBus.Core;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class EventCanceledIntegrationEvent : IntegrationEvent
    {
        public EventCanceledIntegrationEvent(Guid eventId, Guid placeId)
        {
            EventId = eventId;
            PlaceId = placeId;
        }

        public Guid EventId { get; }
        public Guid PlaceId { get; }
    }
}
