using PingDong.Messages;
using System;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class VenueOccupiedIntegrationEvent : IntegrationEvent
    {
        public VenueOccupiedIntegrationEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }
    }
}
