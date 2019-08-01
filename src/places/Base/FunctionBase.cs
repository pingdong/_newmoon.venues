using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Functions
{
    public class FunctionBase
    {
        public async Task ExecuteAsync(ExecutionContext context, ILogger logger, Func<Task> func)
        {
            var functionName = context.FunctionName;

            #region Start

            var start = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processing a request from {start} (UTC)");
            
            #endregion

            try
            {
                // Execute function
                await func();
            }
            catch (Exception ex)
            {
                logger.LogError(EventIds.Failure, ex, ex.Message);

                throw;
            }

            #region End

            var end = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processed a request at {end} (UTC)");
            logger.LogInformation($"Time triggered function - '{functionName}' processed from {start} to {end}, took {(end - start).ToString()}");

            #endregion
        }
    }
}
