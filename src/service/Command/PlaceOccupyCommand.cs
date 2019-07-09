using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOccupyCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceOccupyCommand(Guid id)
        {
            Id = id;
        }
    }
}
