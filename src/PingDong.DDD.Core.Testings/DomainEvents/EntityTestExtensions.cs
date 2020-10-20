using System;
using System.Linq;

namespace PingDong.DDD.Core.Testings
{
    public static class EntityTestExtensions
    {
        public static bool HasDomainEvents<T>(this Entity<T> entity)
        {
            return entity.DomainEvents.Any();
        }

        public static int HasDomainEvent<T>(this Entity<T> entity, Type expectedType)
        {
            expectedType.EnsureNotNull(nameof(expectedType));

            var events = entity.DomainEvents.Where(t => t.GetType() == expectedType);

            return events.Count();
        }
    }
}