using System.Threading.Tasks;

namespace PingDong.Http
{
    public interface IHttpRequestHelper
    {
        Task IdempotencyValidationAsync();
        Task TenancyValidationAsync();

        string GetTenantId();
        string GetCorrelationId();
        string GetRequestId();

        void SetCorrelationIdToResponse(string correlationId);
    }
}
