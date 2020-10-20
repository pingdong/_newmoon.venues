using MediatR;

namespace PingDong.DDD
{
    public class DomainEvent : INotification, IRequestMetadata
    {
        #region ctor

        public DomainEvent()
        {

        }

        public DomainEvent(string tenantId, string correlationId)
        {
            CorrelationId = correlationId;
            TenantId = tenantId;
        }

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
    }
}
