using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceCloseCommand : Command
    {
        public PlaceCloseCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
    }
}
