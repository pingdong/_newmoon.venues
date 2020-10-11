using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PingDong.Collections;

namespace PingDong
{
    public static class BytesExtensions
    {
        // TODO: Better Binary Serialization

        public static byte[] ToBytes(this object graph)
        {
            graph.EnsureNotNull(nameof(graph));

            return JsonSerializer.SerializeToUtf8Bytes(graph, DefaultSerializeOptions);
        }

        public static T FromBytes<T>(this byte[] bytes)
        {
            bytes.EnsureNotNullOrEmpty(nameof(bytes));

            var span = new ReadOnlySpan<byte>(bytes);
            return JsonSerializer.Deserialize<T>(span, DefaultSerializeOptions);
        }

        private static JsonSerializerOptions DefaultSerializeOptions =>
            new JsonSerializerOptions
            {
                WriteIndented = false,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
    }
}
