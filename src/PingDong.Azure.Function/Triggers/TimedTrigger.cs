using Microsoft.Extensions.Logging;

namespace PingDong.Azure.Function
{
    public class TimedTrigger : TriggerBase
    {
        protected TimedTrigger(ILogger logger) : base(logger, null)
        {
        }
        // TODO: Refine error handling
    }
}
