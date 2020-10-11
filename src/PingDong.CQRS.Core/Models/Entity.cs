using System.Collections.Generic;

namespace PingDong.CQRS
{
    public abstract class Entity<TId> : IEntity<TId>, IRequestMetadata
    {
        #region Properties

        public virtual TId Id { get; protected set; }

        #endregion

        #region IRequestMetadata

        public string TenantId { get; private set; }

        public string CorrelationId { get; private set; }

        public void AppendTraceMetadata(string tenantId, string correlationId)
        {
            TenantId = tenantId.EnsureNotNullDefaultOrWhitespace(nameof(tenantId));
            CorrelationId = correlationId.EnsureNotNullOrWhitespace(nameof(correlationId));
        }

        public void AppendTraceMetadata(IRequestMetadata metadata)
        {
            metadata.EnsureNotNull(nameof(metadata));

            TenantId = metadata.TenantId;
            CorrelationId = metadata.CorrelationId;
        }

        #endregion

        #region Domain Events

        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(DomainEvent @event)
        {
            @event.AppendTraceMetadata(TenantId, CorrelationId);

            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        #region Object

        public bool IsTransient()
        {
            return EqualityComparer<TId>.Default.Equals(Id, default);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity<TId>)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            
            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_hashCode.HasValue)
                    // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                    _hashCode = Id.GetHashCode() ^ 31;

                return _hashCode.Value;
            }

            return default(int).GetHashCode();
        }
        private int? _hashCode;

        #endregion
    }
}
