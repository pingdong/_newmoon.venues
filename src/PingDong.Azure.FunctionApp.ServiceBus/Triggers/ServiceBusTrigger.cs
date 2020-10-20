using FluentValidation;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Collections;
using PingDong.Json;
using PingDong.Services;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Azure.FunctionApp
{
    public class ServiceBusTrigger<TData> : TriggerBase
        where TData : class, new()
    {
        private readonly IProcessor<TData> _processor;

        protected ServiceBusTrigger(
            TelemetryConfiguration telemetryConfiguration
            , IProcessor<TData> processor
            , ILogger logger
            , IValidatorFactory validatorFactory
            ) : base(telemetryConfiguration, logger, validatorFactory)
        {
            _processor = processor.EnsureNotNull(nameof(processor));
        }

        protected async Task ExecuteAsync(
            ExecutionContext context
            , Message[] messages)
        {
            if (messages.IsNullOrEmpty())
                return;

            await ProcessAsync(context, async () =>
            {
                foreach (var message in messages)
                {
                    var data = message.Body.Deserialize<TData>();

                    await ValidateAsync(message).ConfigureAwait(false);

                    await _processor.ProcessAsync(data).ConfigureAwait(false);
                }
                // TODO: Refine error handling
            }).ConfigureAwait(false);
        }
    }
}
