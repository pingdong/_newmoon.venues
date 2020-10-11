using FluentValidation;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Collections;
using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Azure.Function
{
    public class EventHubTrigger<TData> : TriggerBase
        where TData : class, new()
    {
        private readonly IProcessor<TData> _processor;

        protected EventHubTrigger(
            IProcessor<TData> processor
            , ILogger logger
            , IValidatorFactory validatorFactory
            ) : base(logger, validatorFactory)
        {
            _processor = processor.EnsureNotNull(nameof(processor));
        }

        protected async Task ExecuteAsync(
            ExecutionContext context
            , EventData[] events)
        {
            if (events.IsNullOrEmpty())
                return;

            await ProcessAsync(context, async () =>
            {
                foreach (var evt in events)
                {
                    var message = evt.Body.Array.FromBytes<TData>();

                    await ValidateAsync(message).ConfigureAwait(false);

                    await _processor.ProcessAsync(message).ConfigureAwait(false);
                }
                // TODO: Refine error handling
                // https://docs.microsoft.com/en-us/azure/azure-functions/functions-reliable-event-processing
            }).ConfigureAwait(false);
        }
    }
}
