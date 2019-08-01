using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceOccupiedDomainEvent : DomainEvent
    {
        public PlaceOccupiedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }

        public Guid PlaceId { get; }
        public string PlaceName { get; }

    }
}
