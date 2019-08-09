using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PingDong.Http.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.IntegrationTests
{
    [Collection(nameof(IntegrationTestFixtures))]
    public class PlaceCreateFixture
    {
        private readonly IntegrationTestFixture _host;

        public PlaceCreateFixture(IntegrationTestFixture host)
        {
            _host = host;
        }
        
        [Theory]
        [InlineData("api/create")]
        public async Task Create_Place_Then_Response_Ok(string url)
        {
            const string body = @"
            {
	            'name': 'place 1',
	            'address': {
		            'no': '1',
		            'street': 'Queen st.',
		            'city': 'akl',
		            'state': 'AK',
		            'country': 'NZ',
		            'zipCode': '0626'
	            }
            }
            ";

            var client = _host.CreateClient()
                                .AddRequestId()
                                .AddCorrelationId()
                                .AddTenantId();
            
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
        }
        
        [Theory]
        [InlineData("api/create")]
        public async Task Create_Place_Fail_WithoutRequestId(string url)
        {
            var client = _host.CreateClient()
                                .AddCorrelationId()
                                .AddTenantId();
            
            var content = new StringContent("{ 'name': 'Test' }", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Theory]
        [InlineData("api/create")]
        public async Task Create_Place_Fail_WithoutCorrelationId(string url)
        {
            var client = _host.CreateClient()
                                .AddRequestId()
                                .AddTenantId();
            
            var content = new StringContent("{ 'name': 'Test' }", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Theory]
        [InlineData("api/create")]
        public async Task Create_Place_Fail_WithoutTenantId(string url)
        {
            var client = _host.CreateClient()
                                .AddRequestId()
                                .AddTenantId();
            
            var content = new StringContent("{ 'name': 'Test' }", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
