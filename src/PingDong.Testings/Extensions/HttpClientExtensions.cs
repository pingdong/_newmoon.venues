using System;
using System.Net.Http;
using System.Threading.Tasks;
using PingDong.Json;

namespace PingDong.Http.Testings
{
    public static class HttpClientExtensions
    {
        #region Post

        public static async Task<HttpResponseMessage> Post1Async(this HttpClient client, string endpoint, HttpContent content)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            return await client.PostAsync(endpoint, content).ConfigureAwait(false);
        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(this HttpClient client, string endpoint, TRequest request)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await client.PostAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        public static async Task<HttpResponseMessage> PostAsync<TRequest>(this HttpClient client, string endpoint, TRequest request)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var json = request.Serialize();
            var content = new JsonContent(json);

            return await client.PostAsync(endpoint, content).ConfigureAwait(false);
        }

        #endregion

        #region Put

        public static async Task<HttpResponseMessage> PutAsync(this HttpClient client, string endpoint, HttpContent content)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            return await client.PutAsync(endpoint, content).ConfigureAwait(false);
        }

        public static async Task<TResponse> PutAsync<TRequest, TResponse>(this HttpClient client, string endpoint, TRequest request)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var json = request.Serialize();
            var content = new JsonContent(json);

            var result = await client.PutAsync(endpoint, content).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        public static async Task<HttpResponseMessage> PutAsync<TRequest>(this HttpClient client, string endpoint, TRequest request)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var json = request.Serialize();
            var content = new JsonContent(json);

            return await client.PutAsync(endpoint, content).ConfigureAwait(false);
        }

        #endregion

        #region Get

        public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string endpoint)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            return await client.GetAsync(endpoint).ConfigureAwait(false);
        }

        public static async Task<TResponse> GetAsync<TResponse>(this HttpClient client, string endpoint)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var result = await client.GetAsync(endpoint).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resultContent.Deserialize<TResponse>();
        }

        #endregion

        #region Delete

        public static async Task<HttpResponseMessage> DeleteAsync<TRequest>(this HttpClient client, string endpoint, TRequest request)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                Content = new JsonContent(request.Serialize()),
                RequestUri = new Uri(client.BaseAddress, endpoint)
            };

            return await client.SendAsync(message);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string endpoint)
        {
            endpoint = endpoint.EnsureNotNullDefaultOrWhitespace(nameof(endpoint))
                                .EnsureBeginningForwardSlash();

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(client.BaseAddress, endpoint)
            };

            return await client.SendAsync(message);
        }

        #endregion
    }
}