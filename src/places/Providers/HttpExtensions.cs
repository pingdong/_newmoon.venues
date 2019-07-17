using System;
using Microsoft.AspNetCore.Http;

namespace PingDong.Newmoon.Places.Functions
{
    internal static class HttpExtensions
    {
        public static Guid GetRequestId(this HttpRequest request)
        {
            var value = request.Headers["x-request-id"];
            return Guid.TryParse(value, out var requestId) ? requestId : Guid.Empty;
        }
        public static Guid GetCorrelationId(this HttpRequest request)
        {
            var value = request.Headers["x-correlation-id"];
            return Guid.TryParse(value, out var correlationId) ? correlationId : Guid.Empty;
        }

        public static Guid GetTenantId(this HttpRequest request)
        {
            var value = request.HttpContext.User.FindFirst("tid")?.Value;
            return Guid.TryParse(value, out var tenantId) ? tenantId : Guid.Empty;
        }
    }
}
