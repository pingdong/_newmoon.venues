using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.DomainEvents
{
    public class VenueUpdatedDomainEvent : DomainEvent
    {
        public VenueUpdatedDomainEvent(Guid venueId, string venueName)
        {
            VenueId = venueId;
            VenueName = venueName;
        }

        public Guid VenueId { get; }
        public string VenueName { get; }
        public Address Address { get; set; }
    }
}
