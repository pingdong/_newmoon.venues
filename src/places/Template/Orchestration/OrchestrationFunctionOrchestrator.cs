using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace PingDong.Templates.Functions
{
    public class OrchestrationFunctionOrchestrator
    {
        [FunctionName("OrchestrateFunction_Orchestrator")]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            // Saga Pattern

            // Variables are used for rollback
            var executed = new Dictionary<string, object>();

            try
            {
                // Extract argument passed in
                var argument = context.GetInput<dynamic>();

                // Activity that modify data
                var writableActivityName = "OrchestrateFunction_Activity_Write";
                var writableActivityArgument = argument;
                var result = await context.CallActivityAsync<dynamic>(writableActivityName, writableActivityArgument);
                //      ApplicationException is used here for demo purpose
                if (result == null)
                    throw new ApplicationException();
                executed.Add(writableActivityName, context);

                // Activity that only read data
                var results = await context.CallActivityAsync<IList<dynamic>>("OrchestrateFunction_Activity_ReadOnly", result);

                // Activities iteration
                foreach (var item in results)
                {
                    // Pass tuple as argument to pass into activity
                    var activityArgument = (item.Id, item.Value);
                    var activityName = "OrchestrateFunction_Activity_Write_Loop";

                    var invokeResult = await context.CallActivityAsync<bool>(activityName, activityArgument);

                    if (!invokeResult)
                        throw new ApplicationException();

                    // Added executed activity in history
                    executed.Add(activityName, argument);
                }
            }
            catch (ApplicationException)
            {
                // Rollback all executed activity
                if (executed.Any())
                    await RollbackAsync(context, executed);
            }

            // await MethodAsync only can be called from here
            //    after the last CallActivityAsync
            // If any await async call is called between two Activity, an exception is thrown.
            //
            // await NotificationAsync();
        }

        private async Task RollbackAsync(DurableOrchestrationContext context, Dictionary<string, object> executedActivities)
        {
            var rollbackActivities = executedActivities.Reverse();

            foreach (var (name, argument) in rollbackActivities)
            {
                // Assume the naming pattern of remedy action has a suffix _Rollback
                var rollbackActivity = $"{name}_Rollback";

                await context.CallActivityAsync(rollbackActivity,  argument);
            }
        }
    }
}