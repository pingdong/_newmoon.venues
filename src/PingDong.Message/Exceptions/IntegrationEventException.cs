using System;
using System.Runtime.Serialization;

namespace PingDong.CQRS.Services
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    [Serializable]
    public class IntegrationEventException : DomainException
    {
        public IntegrationEventException(string message)
            : this(message, null, null)
        {
        }

        public IntegrationEventException(string message, IRequestMetadata metadata)
            : this(message, null, metadata)
        {
        }

        public IntegrationEventException(string message, Exception innerException, IRequestMetadata metadata)
            : base(message, innerException)
        {
            if (metadata != null)
            {
                TenantId = metadata.TenantId;
                CorrelationId = metadata.CorrelationId;
            }
        }

        protected IntegrationEventException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }


        string TenantId { get; }

        string CorrelationId { get; }
    }
}
