using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceFreedDomainEvent : DomainEvent
    {
        public PlaceFreedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }

        public Guid PlaceId { get; }
        public string PlaceName { get; }
    }
}
