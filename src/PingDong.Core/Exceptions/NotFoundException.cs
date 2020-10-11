using System;
using System.Runtime.Serialization;

namespace PingDong
{
    [Serializable]
    public class NotFoundException : DomainException
    {
        public NotFoundException()
            : base("The request data doesn't exist")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string target, string id, string correlationId, string tenantId)
            : this(target, id, correlationId, tenantId, null)
        {
        }

        public NotFoundException(string target, string id, string correlationId, string tenantId, Exception inner)
            : base($"Cannot find {target} with id:'{id}' in the request:'{correlationId}' of tenant:'{tenantId}'", inner)
        {
            Target = target.EnsureNotNullOrWhitespace(nameof(target));
            Id = id.EnsureNotNullOrWhitespace(nameof(target));

            CorrelationId = correlationId;
            TenantId = tenantId;
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string Id { get; }
        public string Target { get; }
        public string CorrelationId { get; }
        public string TenantId { get; }
    }
}
