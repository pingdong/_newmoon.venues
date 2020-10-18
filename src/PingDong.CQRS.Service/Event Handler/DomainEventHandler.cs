using MediatR;
using PingDong.Messages;
using System;
using System.Threading.Tasks;

namespace PingDong.CQRS.Services
{
    /// <summary>
    /// Provide the common functions for a DomainEvent handler
    /// </summary>
    public class DomainEventHandler
    {
        private readonly IMessagePublisher _publisher;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="publisher">The publisher to send a IntegrationEvent.</param>
        /// <param name="mediator">The mediator to send a Command</param>
        public DomainEventHandler(IMessagePublisher publisher, IMediator mediator)
        {
            _publisher = publisher.EnsureNotNull(nameof(publisher));
            _mediator = mediator.EnsureNotNull(nameof(mediator));
        }

        /// <summary>
        /// Publish an IntegrationEvent to external Event Bus
        /// </summary>
        /// <param name="metadata">The tracker</param>
        /// <param name="integrationEvent">The IntegrationEvent needs to be send</param>
        /// <returns></returns>
        protected async Task PublishAsync(IntegrationEvent integrationEvent, IRequestMetadata metadata = null)
        {
            if (integrationEvent == null)
                throw new ArgumentNullException(nameof(integrationEvent));

            if (metadata != null)
                integrationEvent.AppendTraceMetadata(metadata);
            
            await _publisher.PublishAsync(integrationEvent).ConfigureAwait(false);
        }

        /// <summary>
        /// Dispatch a new Command
        /// </summary>
        /// <typeparam name="TResponse">The result type of the execution of the Command</typeparam>
        /// <param name="command">The Command is going to be sent</param>
        /// <param name="metadata">The tracker</param>
        /// <returns></returns>
        protected async Task<TResponse> DispatchAsync<TResponse>(Command<TResponse> command, IRequestMetadata metadata = null)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (metadata != null)
                command.AppendTraceMetadata(metadata);
            
            return await _mediator.Send(command).ConfigureAwait(false);
        }
    }
}
