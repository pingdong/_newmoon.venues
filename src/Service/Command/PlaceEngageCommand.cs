using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceEngageCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceEngageCommand(Guid id)
        {
            Id = id;
        }
    }
}
