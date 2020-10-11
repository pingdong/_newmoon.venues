using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PingDong.Testings.TestFixtures
{
    public class FunctionAppTestFixture : TestFixtureBase
    {
        private readonly HttpClient _http = new HttpClient();

        protected override void DisposeManagedResource()
        {
            base.DisposeManagedResource();

            _http.Dispose();
        }

        #region Post

        /*
         *  $FUNCTION_KEY = $(az rest --method post --uri "/subscripitons/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP/Providers
         *                    Microsoft.Web/sites/$FUNCTION_APP/host/default/listKeys?api-version=2018-11-01" --query functionKeys.default --output tsv)
         *
         *  Write-Host "##vso[task.setvariable variable=FUNCTIONAPPKEY;]$FUNCTION_KEY
         */

        public async Task TriggerAsync(string appName, string triggerName, string appKey)
        {
            appName.EnsureNotNullOrWhitespace(nameof(appName));
            triggerName.EnsureNotNullOrWhitespace(nameof(triggerName));
            appKey.EnsureNotNullOrWhitespace(nameof(appKey));

            var message = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://{appName}.azurewebsites.net/admin/functions/{triggerName}"),
                Method = HttpMethod.Post
            };
            message.Headers.Add("x-functions-key", appKey);
            message.Content = new StringContent("{'body': ''}", Encoding.UTF8, "application/json");

            var result = await _http.SendAsync(message).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}
