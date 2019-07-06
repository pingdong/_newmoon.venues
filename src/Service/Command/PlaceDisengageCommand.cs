using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceDisengageCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceDisengageCommand(Guid id)
        {
            Id = id;
        }
    }
}
