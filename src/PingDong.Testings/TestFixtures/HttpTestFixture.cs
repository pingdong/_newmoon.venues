using PingDong.Http;
using PingDong.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Testings.TestFixtures
{
    public class HttpTestFixture : TestFixtureBase
    {
        private HttpClient _http;
        private string _baseAddress;
        
        public void Initialize(string baseAddress)
        {
            baseAddress.EnsureNotNullOrWhitespace(nameof(baseAddress));

            _http = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            _baseAddress = baseAddress;
        }

        protected override void DisposeManagedResource()
        {
            base.DisposeManagedResource();

            _http.Dispose();
        }

        #region Post

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            return await _http.PostAsync(endpoint, content).ConfigureAwait(false);
        }
        
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await _http.PostAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        public async Task PostAsync<TRequest>(string endpoint, TRequest request)
        {
            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await _http.PostAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
        }

        #endregion

        #region Put

        public async Task<HttpResponseMessage> PutAsync(string endpoint, HttpContent content)
        {
            return await _http.PutAsync(endpoint, content).ConfigureAwait(false);
        }
        
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await _http.PutAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        public async Task PutAsync<TRequest>(string endpoint, TRequest request)
        {
            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await _http.PutAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
        }

        #endregion

        #region Get

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _http.GetAsync(endpoint).ConfigureAwait(false);
        }

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            var result = await _http.GetAsync(endpoint).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        #endregion

        #region Delete

        public async Task DeleteAsync<TRequest>(string endpoint, TRequest request)
        {
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                Content = new JsonContent(request.Serialize()),
                RequestUri = new Uri(new Uri(_baseAddress), endpoint)
            };

            var result = await _http.SendAsync(message);
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}
