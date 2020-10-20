using PingDong.DDD;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueVacatedDomainEvent : DomainEvent
    {
        public VenueVacatedDomainEvent(Guid venueId)
        {
            VenueId = venueId;
        }

        public Guid VenueId { get; }
    }
}
