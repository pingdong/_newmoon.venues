using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueOpenCommand : Command<bool>
    {
        public VenueOpenCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
