using PingDong.CQRS;
using System;

namespace PingDong.Newmoon.Venues.Services.Commands
{
    public class VenueUpdateCommand : Command<bool>
    {
        public VenueUpdateCommand(Guid id, string name, Address address)
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
