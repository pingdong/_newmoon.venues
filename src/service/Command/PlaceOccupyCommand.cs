using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceOccupyCommand : Command
    {
        public PlaceOccupyCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
