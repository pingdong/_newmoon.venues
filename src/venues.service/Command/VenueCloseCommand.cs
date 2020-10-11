using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCloseCommand : Command<bool>
    {
        public VenueCloseCommand()
        {
        }
        
        public Guid Id { get; set; }
    }
}
