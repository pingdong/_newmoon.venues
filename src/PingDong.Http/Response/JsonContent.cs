using System.Net.Http;
using System.Text;
using System.Text.Json;
using PingDong.Json;

namespace PingDong.Http
{
    public class JsonContent : StringContent
    {
        public JsonContent(object content)
            : this(content, JsonSerializerProfile.Default)
        {

        }
        public JsonContent(object content, JsonSerializerOptions options)
            : this(content.Serialize(options))
        {

        }

        public JsonContent(string content)
            : base(content, Encoding.UTF8, "application/json")
        {

        }
    }
}
