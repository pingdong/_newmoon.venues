using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueOccupyCommand : Command<bool>
    {
        public VenueOccupyCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
