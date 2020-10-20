using System.Text;
using System.Text.Json;

namespace PingDong.Json
{
    public static class JsonExtensions
    {
        public static T Deserialize<T>(this byte[] value)
        {
            return Encoding.UTF8.GetString(value).Deserialize<T>();
        }

        public static T Deserialize<T>(this string json)
        {
            return Deserialize<T>(json, JsonSerializerProfile.Default);
        }

        public static T Deserialize<T>(this string json, JsonSerializerOptions options)
        {
            json.EnsureNotNullOrWhitespace(nameof(json));

            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string Serialize(this object graph)
        {
            return JsonSerializer.Serialize(graph, JsonSerializerProfile.Default);
        }

        public static string Serialize(this object graph, JsonSerializerOptions options)
        {
            graph.EnsureNotNull(nameof(graph));

            return JsonSerializer.Serialize(graph, options);
        }
    }
}
