using System;
using System.Net.Http;

namespace PingDong.Testings.TestFixtures
{
    public class HttpTestFixtureBase : TestFixtureBase
    {
        public void Initialize(string baseAddress)
        {
            BaseAddress = baseAddress.EnsureNotNullOrWhitespace(nameof(baseAddress));

            Client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public string BaseAddress { get; private set; }

        public HttpClient Client { get; private set; }

        protected override void DisposeManagedResource()
        {
            base.DisposeManagedResource();

            Client?.Dispose();
        }
    }
}
