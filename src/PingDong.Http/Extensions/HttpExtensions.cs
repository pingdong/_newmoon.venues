using Microsoft.AspNetCore.Http;
using PingDong.Json;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingDong.Http
{
    public static class HttpExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpRequest request)
        {
            var body = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);

            return body.Deserialize<T>();
        }

        public static async Task<T> DeserializeAsync<T>(this HttpRequest request, JsonSerializerOptions options)
        {
            var body = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);

            return body.Deserialize<T>(options);
        }

        public static string Combine(this string url, string endpoint)
        {
            url.EnsureNotNullOrWhitespace(nameof(url));
            endpoint.EnsureNotNullOrWhitespace(nameof(endpoint));

            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);

            if (!endpoint.StartsWith("/"))
                endpoint = "/" + endpoint;

            return url + endpoint;
        }

        public static string RemoveEndingForwardSlash(this string url)
        {
            url.EnsureNotNullOrWhitespace(nameof(url));

            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);

            return url;
        }

        public static string EnsureBeginningForwardSlash(this string endpoint)
        {
            endpoint.EnsureNotNullOrWhitespace(nameof(endpoint));

            if (!endpoint.StartsWith("/"))
                endpoint = "/" + endpoint;

            return endpoint;
        }
    }
}
