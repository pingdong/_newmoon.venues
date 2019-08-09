using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PingDong.Templates.Functions
{
    public class LoopWriteActivity
    {
        [FunctionName("OrchestrateFunction_Activity_Write_Loop")]
        public Task<bool> Execute(
            [ActivityTrigger] DurableActivityContext context
            , ILogger log)
        {
            // Extract passed value
            (Guid Id, string Name) argument = context.GetInput<(Guid, string)>();
            
            // Saving changes

            return Task.FromResult(true);
        }
    }
}