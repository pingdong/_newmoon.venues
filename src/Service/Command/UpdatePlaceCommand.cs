using System;
using PingDong.CleanArchitect.Core;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class UpdatePlaceCommand : Command
    {
        public UpdatePlaceCommand(Guid id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
        
        public Guid Id { get; }
        public string Name { get; }
        public Address Address { get; }
    }
}
