using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;

namespace PingDong.Azure.Function
{
    public class TimedTrigger : TriggerBase
    {
        protected TimedTrigger(
            TelemetryConfiguration telemetryConfiguration
            , ILogger logger
            ) : base(telemetryConfiguration, logger, null)
        {
        }
    }
}
