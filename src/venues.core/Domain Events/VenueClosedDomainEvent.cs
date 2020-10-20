using PingDong.DDD;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueClosedDomainEvent : DomainEvent
    {
        public VenueClosedDomainEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }
    }
}
