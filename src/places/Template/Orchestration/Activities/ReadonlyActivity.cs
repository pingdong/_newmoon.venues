using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PingDong.Templates.Functions
{
    public class ReadonlyActivity
    {
        [FunctionName("OrchestrateFunction_Activity_ReadOnly")]
        public Task<IList<dynamic>> Execute(
            [ActivityTrigger] DurableActivityContext context
            , ILogger log)
        {
            // Extract passed in value
            var value = context.GetInput<dynamic>();

            // read data based on the argument

            // Return
            IList<dynamic> result = new List<dynamic>();
            return Task.FromResult(result);
        }
    }
}