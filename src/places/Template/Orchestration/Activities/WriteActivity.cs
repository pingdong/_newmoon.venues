using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PingDong.Templates.Functions
{
    public class WriteActivity
    {
        [FunctionName("OrchestrateFunction_Activity_Write")]
        public Task<dynamic> Execute(
            [ActivityTrigger] DurableActivityContext context
            , ILogger log)
        {
            // Extract passed in value
            var argument = context.GetInput<dynamic>();
            
            // Saving changes
            dynamic dyn = 1;
            return Task.FromResult(dyn);
        }
    }
}