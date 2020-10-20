using PingDong.Messages;
using System;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class EventCanceledIntegrationEvent : IntegrationEvent
    {
        public EventCanceledIntegrationEvent(Guid eventId, Guid venueId)
        {
            EventId = eventId;
            VenueId = venueId;
        }

        public Guid EventId { get; }
        public Guid VenueId { get; }
    }
}
