using System;
using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceStateChangedDomainEvent : INotification
    {
        public Guid PlaceId { get; }
        public string PlaceName { get; }
        public bool IsOccupied { get; }

        public PlaceStateChangedDomainEvent(Guid placeId, string placeName, bool isOccupied)
        {
            PlaceId = placeId;
            PlaceName = placeName;
            IsOccupied = isOccupied;
        }
    }
}
