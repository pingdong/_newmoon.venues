using System;
using PingDong.EventBus.Core;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class PlaceTemporaryClosedIntegrationEvent : IntegrationEvent
    {
        public PlaceTemporaryClosedIntegrationEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }

        public Guid PlaceId { get; }
        public string PlaceName { get; }
    }
}
