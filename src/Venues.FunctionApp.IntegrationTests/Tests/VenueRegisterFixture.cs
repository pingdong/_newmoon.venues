using PingDong.Http;
using PingDong.Http.Testings;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PingDong.Newmoon.Venues.IntegrationTests
{
    [Collection(nameof(IntegrationTestFixtures))]
    public class VenueRegisterFixture
    {
        private readonly HttpTestFixture _http;
        private readonly JsonTestFixture _json;

        private const string Endpoint = "/api/venue/register";

        public VenueRegisterFixture(
            HttpTestFixture http
            , JsonTestFixture json)
        {
            _http = http;
            _json = json;
        }
        
        [Theory]
        [InlineData("VenueRegister", "default.json")]
        public async Task Register_WithValue_ReturnOk(string folder, string file)
        {
            var body = _json.LoadFromResourceStream($"{folder}.{file}");
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _http.Client
                                        .AddRequestId(Guid.NewGuid().ToString())
                                        .AddCorrelationId(Guid.NewGuid().ToString())
                                        .Post1Async(Endpoint, content);

            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("VenueRegister", "default.json")]
        public async Task Register_MissingRequestId_ReturnBadRequest(string folder, string file)
        {
            var body = _json.LoadFromResourceStream($"{folder}.{file}");
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _http.Client
                .AddCorrelationId(Guid.NewGuid().ToString())
                .Post1Async(Endpoint, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("VenueRegister", "default.json")]
        public async Task Register_MissingCorrelationId_ReturnBadRequest(string folder, string file)
        {
            var body = _json.LoadFromResourceStream($"{folder}.{file}");
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _http.Client
                .AddRequestId(Guid.NewGuid().ToString())
                .Post1Async(Endpoint, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
