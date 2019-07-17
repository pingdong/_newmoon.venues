using System;
using PingDong.EventBus.Core;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    public class PlaceFreedIntegrationEvent : IntegrationEvent
    {
        public PlaceFreedIntegrationEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }
        
        public Guid PlaceId { get; }
        public string PlaceName { get; }
    }
}
