using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.Newmoon.Places.Functions;

namespace PingDong.Templates.Functions
{
    public class OrchestrationFunction : FunctionBase
    {
        [FunctionName("OrchestrateFunction_Entrypoint")]
        public async Task Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "route")] HttpRequest request
                , [OrchestrationClient]DurableOrchestrationClient orchestrator
                , ExecutionContext context
                , ILogger logger)
        {
            await ExecuteAsync(context, logger, async () =>
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var content = JsonConvert.DeserializeObject<dynamic>(requestBody);

                await orchestrator.StartNewAsync("OrchestrateFunction_Orchestrator", content);
            });
        }
    }
}
