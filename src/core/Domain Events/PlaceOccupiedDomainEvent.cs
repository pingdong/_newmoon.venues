using System;
using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceOccupiedDomainEvent : INotification
    {
        public Guid PlaceId { get; }
        public string PlaceName { get; }

        public PlaceOccupiedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
