using System;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Service.IntegrationEvents
{
    internal class CommandIntegrationEventHandler
    {
        private readonly IMediator _mediator;

        public CommandIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<bool> CommandDispatchAsync<TCommand>(IntegrationEvent @event, TCommand command) where TCommand: Command<bool>
        {
            if (string.IsNullOrWhiteSpace(@event.RequestId))
                return false;

            if (Guid.TryParse(@event.RequestId, out Guid requestId))
                return false;

            command.CorrelationId = @event.CorrelationId;
            command.TenantId = @event.TenantId;

            var identifiedCommand = new IdentifiedCommand<Guid, bool, TCommand>(requestId, command);
            return await _mediator.Send(identifiedCommand).ConfigureAwait(false);
        }
    }
}
