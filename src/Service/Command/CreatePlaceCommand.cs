using MediatR;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class CreatePlaceCommand : IRequest<bool>
    {
        public string Name { get; }

        public Address Address { get; }

        public CreatePlaceCommand(string name, Address address)
        {
            Name = name;
            Address = address;
        }
    }
}
