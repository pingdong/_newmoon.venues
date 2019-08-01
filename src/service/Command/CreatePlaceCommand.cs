using PingDong.CleanArchitect.Core;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommand : Command<bool>
    {
        public CreatePlaceCommand(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; }

        public Address Address { get; }
    }
}
