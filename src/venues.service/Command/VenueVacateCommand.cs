using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueVacateCommand : Command<bool>
    {
        public VenueVacateCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
    }
}
