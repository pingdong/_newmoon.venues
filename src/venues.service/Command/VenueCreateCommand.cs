using PingDong.CQRS;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueCreateCommand : Command<bool>
    {
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
