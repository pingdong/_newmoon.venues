using PingDong.CQRS;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueRegisterCommand : Command<bool>
    {
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
