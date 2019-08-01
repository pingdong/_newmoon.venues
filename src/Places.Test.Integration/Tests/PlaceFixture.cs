using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PingDong.Azure.Functions.Testing;
using PingDong.Http.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.IntegrationTests
{
    [Collection("Integration Test")]
    public class PlaceFixture : IDisposable
    {
        private readonly FunctionHost _host;

        public PlaceFixture(FunctionHost host)
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

            var client = _host.GetClient()
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
            var client = _host.GetClient()
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
            var client = _host.GetClient()
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
            var client = _host.GetClient()
                                .AddRequestId()
                                .AddTenantId();
            
            var content = new StringContent("{ 'name': 'Test' }", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}
