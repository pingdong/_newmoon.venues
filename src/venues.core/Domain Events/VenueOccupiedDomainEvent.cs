using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueOccupiedDomainEvent : DomainEvent
    {
        public VenueOccupiedDomainEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }

    }
}
