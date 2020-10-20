using PingDong.DDD;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueRegisteredDomainEvent : DomainEvent
    {
        public VenueRegisteredDomainEvent(Guid venueId, string venueName)
        {
            VenueId = venueId;
            VenueName = venueName;
        }

        public Guid VenueId { get; }
        public string VenueName { get; }
        public Address Address { get; set; }
    }
}
