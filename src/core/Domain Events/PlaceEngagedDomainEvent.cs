using System;
using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceEngagedDomainEvent : INotification
    {
        public Guid PlaceId { get; }
        public string PlaceName { get; }

        public PlaceEngagedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
