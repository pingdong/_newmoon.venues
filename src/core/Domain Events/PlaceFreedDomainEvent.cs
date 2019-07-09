using System;
using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceFreedDomainEvent : INotification
    {
        public Guid PlaceId { get; }
        public string PlaceName { get; }

        public PlaceFreedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
