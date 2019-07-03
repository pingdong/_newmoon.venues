using MediatR;

namespace PingDong.Newmoon.Places.Core
{
    public class PlaceUpdatedDomainEvent : INotification
    {
        public Place Place { get; }

        public PlaceUpdatedDomainEvent(Place place)
        {
            Place = place;
        }
    }
}
