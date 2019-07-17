using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Functions;

namespace PingDong.Newmoon.Places
{
    public class IntegrationEventSubscriber : FunctionBase
    {
        private readonly IMessageDispatcher<Message> _dispatcher;

        public IntegrationEventSubscriber(IMessageDispatcher<Message> dispatcher)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        [FunctionName("IntegrationEventSubscriber")]
        public async Task Run(
            [ServiceBusTrigger("events", "func_place", Connection = "EventBus_ConnectionString")]Message message
            , ExecutionContext context
            , ILogger logger)
        {
            await ExecuteAsync(context, logger, async () =>
            {
                await _dispatcher.DispatchAsync(message);
            });
        }
    }
}
