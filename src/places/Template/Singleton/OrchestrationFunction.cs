using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.Newmoon.Places.Functions;

namespace PingDong.Templates.Functions
{
    public class SingletonFunction : FunctionBase
    {
        [FunctionName("SingletoneFunction_Entrypoint")]
        public async Task<HttpResponseMessage> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "route")] HttpRequestMessage request
                , [OrchestrationClient]DurableOrchestrationClient orchestrator
                , ExecutionContext context
                , string instanceId
                , ILogger logger)
        {
            return await ExecuteAsync(context, logger, async () =>
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<dynamic>(requestBody);

                var existingOrchestrator = await orchestrator.GetStatusAsync(instanceId);
                if (existingOrchestrator == null)
                {
                    await orchestrator.StartNewAsync("SingletonFunction_Orchestrator", content);

                    return orchestrator.CreateCheckStatusResponse(request, instanceId);
                }
                else
                {
                    // An instance with the specified ID exists, don't create one.
                    return request.CreateErrorResponse(
                        HttpStatusCode.Conflict,
                        $"An instance with ID '{instanceId}' already exists.");
                }
            });
        }
    }
}
