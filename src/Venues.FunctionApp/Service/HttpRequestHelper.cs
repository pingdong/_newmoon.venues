using Microsoft.AspNetCore.Http;
using PingDong.Http;
using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Http
{
    internal class HttpRequestHelper : IHttpRequestHelper
    {
        public const string RequestIdKey = "x-request-id";
        public const string CorrelationIdKey = "x-correlation-id";
        public const string TenantIdKey = "tid";

        private readonly IHttpContextAccessor _accessor;
        private readonly IRequestManager _requestManager;
        private readonly ITenantManager<string> _tenantManager;

        public HttpRequestHelper(IHttpContextAccessor accessor
            , IRequestManager requestManager
            , ITenantManager<string> tenantManager)
        {
            _accessor = accessor.EnsureNotNull(nameof(accessor));
            _requestManager = requestManager.EnsureNotNull(nameof(requestManager));
            _tenantManager = tenantManager.EnsureNotNull(nameof(tenantManager));
        }

        public Task IdempotencyValidationAsync()
        {
            return Task.CompletedTask;
        }

        public Task TenancyValidationAsync()
        {
            return Task.CompletedTask;
        }

        public string GetRequestId()
        {
            if (!_accessor.HttpContext.Request.Headers.ContainsKey(RequestIdKey))
                throw new InvalidRequestException($"Cannot find the header: {RequestIdKey}");

            return _accessor.HttpContext.Request.Headers[RequestIdKey];
        }

        public string GetTenantId()
        {
            return _accessor.HttpContext.User.FindFirst(TenantIdKey)?.Value ?? "default-tenant";
        }

        public string GetCorrelationId()
        {
            if (!_accessor.HttpContext.Request.Headers.ContainsKey(CorrelationIdKey))
                throw new InvalidRequestException($"Cannot find the header: {CorrelationIdKey}");

            return _accessor.HttpContext.Request.Headers[CorrelationIdKey];
        }

        public void SetCorrelationIdToResponse(string correlationId)
        {
            _accessor.HttpContext.Response.Headers.Add(CorrelationIdKey, correlationId);
        }
    }
}
