using System.Net.Http;

namespace PingDong.Http
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddRequestId(this HttpClient client, string requestId, string key = "x-request-id")
        {
            client.EnsureNotNull(nameof(client));
            requestId.EnsureNotNullOrWhitespace(nameof(requestId));

            if (client.DefaultRequestHeaders.Contains(key))
                client.DefaultRequestHeaders.Remove(key);
            client.DefaultRequestHeaders.Add(key, requestId);

            return client;
        }

        public static HttpClient AddCorrelationId(this HttpClient client, string correlationId, string key = "x-correlation-id")
        {
            client.EnsureNotNull(nameof(client));
            correlationId.EnsureNotNullOrWhitespace(nameof(correlationId));

            if (client.DefaultRequestHeaders.Contains(key))
                client.DefaultRequestHeaders.Remove(key);
            client.DefaultRequestHeaders.Add(key, correlationId);

            return client;
        }

        public static HttpClient AddTenantId(this HttpClient client, string tenantId, string key = "x-tenant-id")
        {
            client.EnsureNotNull(nameof(client));
            tenantId.EnsureNotNullOrWhitespace(nameof(tenantId));

            if (client.DefaultRequestHeaders.Contains(key))
                client.DefaultRequestHeaders.Remove(key);
            client.DefaultRequestHeaders.Add(key, tenantId);

            return client;
        }
    }
}