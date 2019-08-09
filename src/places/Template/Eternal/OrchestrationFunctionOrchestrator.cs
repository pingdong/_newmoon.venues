using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace PingDong.Templates.Functions
{
    public class EternalFunctionOrchestrator
    {
        [FunctionName("EternalFunction_Orchestrator")]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            await context.CallActivityAsync("EternalFunction_Activity_Loop");

            // As the DTF keeps track of the history of each orchestration.
            // The history grows continuously as long as the orchestrator
            //    function continues to schedule new work.
            // The history could grow critically large.
            // Eternal Function was designed to mitigate this issue.
            
            
            // compare with timer-triggered function
            // Waiting after executing avoids overlapping that happens in timed job.
            DateTime nextCleanup = context.CurrentUtcDateTime.AddHours(1);
            await context.CreateTimer(nextCleanup, CancellationToken.None);
            
            // Instead of using infinite loops, orchestrator function reset its state
            //    by calling ContiuneAsNew.
            // When ContinueAsNew is called, the instance enqueues a message to itself
            //    before it exists. The message restarts the instance with the new value.
            //    The same instance ID is kept, but the history is effectively truncated.
            context.ContinueAsNew(null);
        }
    }
}