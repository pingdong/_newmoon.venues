using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public class PlaceFreeCommand : Command
    {
        public PlaceFreeCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
    }
}
