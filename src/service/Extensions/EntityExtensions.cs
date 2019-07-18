using System;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Service.Commands
{
    public static class EntityExtensions
    {
        public static T Prepare<T>(this T entity, Command<bool> command) where T : Entity<Guid>
        {
            entity.CorrelationId = command.CorrelationId;
            entity.TenantId = command.TenantId;

            return entity;
        }
    }
}
