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
    }
}
