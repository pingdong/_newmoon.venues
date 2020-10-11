using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueOpenedDomainEvent : DomainEvent
    {
        public VenueOpenedDomainEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }
    }
}
