using PingDong.Messages;
using System;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class VenueClosedIntegrationEvent : IntegrationEvent
    {
        public VenueClosedIntegrationEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }
    }
}
