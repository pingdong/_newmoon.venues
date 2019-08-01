﻿using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceClosedDomainEvent : DomainEvent
    {
        public PlaceClosedDomainEvent(Guid placeId, string placeName)
        {
            PlaceId = placeId;
            PlaceName = placeName;
        }

        public Guid PlaceId { get; }
        public string PlaceName { get; }
    }
}