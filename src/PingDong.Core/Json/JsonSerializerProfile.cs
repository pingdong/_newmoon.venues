using System.Text.Json;
using System.Text.Json.Serialization;

namespace PingDong.Json
{
    public class JsonSerializerProfile
    {
        public static JsonSerializerOptions Default =>
            new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new JsonStringDateTimeConverter()
                }
            };
        //public static JsonSerializerOptions Cosmos =>
        //    new JsonSerializerOptions
        //    {
        //        IgnoreNullValues = true,
        //        WriteIndented = false,
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        PropertyNameCaseInsensitive = false,
        //        Converters =
        //        {
        //            new JsonStringDateTimeConverter()
        //        }
        //    };
    }
}
