using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Azure.FunctionApp;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues
{
    public class VenueCleanUp : TimedTrigger
    {
        public VenueCleanUp(
            TelemetryConfiguration telemetryConfiguration
            , ILogger<VenueCleanUp> logger
            ) : base(telemetryConfiguration, logger)
        { }

        [FunctionName("Venues_CleanUp")]
        public async Task Run(
            [TimerTrigger("0 0 0 1,5,10,15,20,25 * *")] TimerInfo timer
            , ExecutionContext context)
        {
            await ProcessAsync(context, async () =>
            {
                Logger.LogInformation($"Data Cleaning {timer.ScheduleStatus}");

                return Task.CompletedTask;
            }).ConfigureAwait(false);
        }
    }
}
