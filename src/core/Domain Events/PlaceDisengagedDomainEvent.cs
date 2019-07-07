using System;
using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceDisengagedDomainEvent : INotification
    {
        public Guid PlaceId { get; }
        public string PlaceName { get; }

        public PlaceDisengagedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
    }
}
