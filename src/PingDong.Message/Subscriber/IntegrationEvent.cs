using System;

namespace PingDong.Messages
{
    public class IntegrationEvent : IRequestMetadata
    {
        #region ctor

        public IntegrationEvent() 
            : this(string.Empty, string.Empty, string.Empty)
        {

        }

        public IntegrationEvent(string requestId, string tenantId, string correlationId)
        {
            RequestId = requestId;
            CreationDateInUtc = DateTime.UtcNow;

            CorrelationId = correlationId;
            TenantId = tenantId;
        }

        #endregion

        #region Properties

        public string RequestId  { get; set; }
        public DateTime CreationDateInUtc { get; }

        #endregion

        #region IRequestMetadata

        public string TenantId { get; private set; }

        public string CorrelationId { get; private set; }

        public void AppendTraceMetadata(string tenantId, string correlationId)
        {
            TenantId = tenantId;
            CorrelationId = correlationId;
        }

        public void AppendTraceMetadata(IRequestMetadata metadata)
        {
            TenantId = metadata.TenantId;
            CorrelationId = metadata.CorrelationId;
        }

        #endregion
    }
}
