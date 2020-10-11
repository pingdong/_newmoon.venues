using FluentValidation;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Collections;
using PingDong.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingDong.Azure.Function
{
    public class CosmosChangeFeedTrigger<TData> : TriggerBase
        where TData : class, new()
    {
        private readonly IProcessor<TData> _processor;

        protected CosmosChangeFeedTrigger(
            IProcessor<TData> processor
            , ILogger logger
            , IValidatorFactory validatorFactory
            ) : base(logger, validatorFactory)
        {
            _processor = processor.EnsureNotNull(nameof(processor));
        }

        protected async Task ExecuteAsync(
            ExecutionContext context
            , IEnumerable<TData> requests)
        {
            var enumerable = requests as TData[] ?? requests.ToArray();
            if (enumerable.IsNullOrEmpty())
                return;

            await ProcessAsync(context, async () =>
            {
                foreach (var request in enumerable)
                {
                    await ValidateAsync(request).ConfigureAwait(false);

                    await _processor.ProcessAsync(request).ConfigureAwait(false);
                    // TODO: Refine error handling
                }
            }).ConfigureAwait(false);
        }
    }
}
