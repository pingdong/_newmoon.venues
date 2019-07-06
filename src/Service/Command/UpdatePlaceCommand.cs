using System;
using MediatR;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class UpdatePlaceCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public string Name { get; }

        public Address Address { get; }

        public UpdatePlaceCommand(Guid id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

    }
}
