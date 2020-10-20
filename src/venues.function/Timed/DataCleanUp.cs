using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Azure.Function;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues
{
    public class Venue_CleanUp : TimedTrigger
    {
        public Venue_CleanUp(
            TelemetryConfiguration telemetryConfiguration
            , ILogger<Venue_CleanUp> logger
            ) : base(telemetryConfiguration, logger)
        { }

        [FunctionName("DataCleanUp")]
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
