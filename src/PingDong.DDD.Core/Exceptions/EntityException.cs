using System;
using System.Runtime.Serialization;

namespace PingDong.DDD
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    [Serializable]
    public class EntityException : DomainException, IRequestMetadata
    {
        public EntityException(string message)
            : this(message, null, null)
        { }

        public EntityException(string message, IRequestMetadata metadata)
            : this(message, null, metadata)
        { }

        public EntityException(string message, Exception innerException, IRequestMetadata metadata)
            : base(message, innerException)
        {
            if (metadata != null)
            {
                TenantId = metadata.TenantId;
                CorrelationId = metadata.CorrelationId;
            }
        }

        protected EntityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #region Properties

        public string RequestId { get; set; }
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
