using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceFreeCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceFreeCommand(Guid id)
        {
            Id = id;
        }
    }
}
