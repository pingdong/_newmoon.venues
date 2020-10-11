using MediatR;
using PingDong.Messages;
using System;
using System.Threading.Tasks;

namespace PingDong.CQRS.Services
{
    /// <summary>
    /// Provide the common functions for an IntegrationEvent handler
    /// </summary>
    public class IntegrationEventHandler
    {
        private readonly IMediator _mediator;
        private readonly bool _supportIdempotencyCheck;

        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="mediator">The mediator to send a Command</param>
        public IntegrationEventHandler(IMediator mediator, bool supportIdempotencyCheck)
        {
            _mediator = mediator.EnsureNotNull(nameof(mediator));
            _supportIdempotencyCheck = supportIdempotencyCheck;
        }

        /// <summary>
        /// Dispatch a new Command
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="command">The Command is going to be sent</param>
        /// <param name="event">The IntegrationEvent</param>
        protected async Task<bool> DispatchAsync<TCommand>(TCommand command, IntegrationEvent @event)
            where TCommand : Command<bool>
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            command.AppendTraceMetadata(@event);

            IRequest<bool> cmd;
            if (_supportIdempotencyCheck)
                cmd = new IdentifiedCommand<bool, TCommand>(@event.RequestId, command);
            else
                cmd = command;

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }
    }
}