using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PingDong.Templates.Functions
{
    public class WriteRollbackActivity
    {
        [FunctionName("OrchestrateFunction_Activity_Write_Rollback")]
        public Task Execute(
            [ActivityTrigger] DurableActivityContext context
            , ILogger log)
        {
            // Extract passed in value
            var argument = context.GetInput<dynamic>();
            
            // Remedy changes

            return Task.CompletedTask;
        }
    }
}