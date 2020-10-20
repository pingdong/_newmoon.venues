
namespace PingDong
{
    public interface IRequestMetadata
    {
        string TenantId { get; }

        string CorrelationId { get; }

        void AppendTraceMetadata(string tenantId, string correlationId);

        void AppendTraceMetadata(IRequestMetadata metadata);
    }
}
