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
        public static string GetCorrelationId(this HttpRequest request)
        {
            return request.Headers["x-correlation-id"];
        }

        public static string GetTenantId(this HttpRequest request)
        {
            return request.HttpContext.User.FindFirst("tid")?.Value;
        }
    }
}
