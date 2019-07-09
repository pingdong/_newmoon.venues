using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceCloseCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceCloseCommand(Guid id)
        {
            Id = id;
        }
    }
}
