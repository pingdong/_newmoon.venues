using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using PingDong.Collections;

namespace PingDong.CQRS.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync<TId, TEntity>(this IMediator mediator, IEnumerable<TEntity> entities)
            where TEntity : Entity<TId>
        {
            var domainEntities = entities
                .Where(x => x.DomainEvents != null && x.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }

        public static async Task DispatchDomainEventsAsync<TId, TEntity>(this IMediator mediator, TEntity entity)
            where TEntity : Entity<TId>
        {
            if (entity.DomainEvents.IsNullOrEmpty())
                return;

            foreach (var domainEvent in entity.DomainEvents)
                await mediator.Publish(domainEvent);

            entity.ClearDomainEvents();
        }
    }
}
