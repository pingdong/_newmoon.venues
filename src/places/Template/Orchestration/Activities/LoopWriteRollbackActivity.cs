using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PingDong.Templates.Functions
{
    public class LoopWriteRollbackActivity
    {
        [FunctionName("OrchestrateFunction_Activity_Write_Loop_Rollback")]
        public Task Execute(
            [ActivityTrigger] DurableActivityContext context
            , ILogger log)
        {
            // Extract passed in value
            (Guid Id, string Name) argument = context.GetInput<(Guid, string)>();
            
            // Remedy changes

            return Task.CompletedTask;
        }
    }
}