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
            var tid = request.HttpContext.User.FindFirst("tid")?.Value;

            // In production, tenant Id should only get from toke
            if (string.IsNullOrWhiteSpace(tid))
                tid = request.Headers["x-tenant-id"];

            return tid;
        }
    }
}
