using System;
using MediatR;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOpenCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public PlaceOpenCommand(Guid id)
        {
            Id = id;
        }
    }
}
