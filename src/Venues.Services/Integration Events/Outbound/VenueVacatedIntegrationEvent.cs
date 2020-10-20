using PingDong.Messages;
using System;

namespace PingDong.Newmoon.Venues.Services.IntegrationEvents
{
    public class VenueVacatedIntegrationEvent : IntegrationEvent
    {
        public VenueVacatedIntegrationEvent(Guid venueId)
        {
            VenueId = venueId;
        }
        
        public Guid VenueId { get; }
    }
}
