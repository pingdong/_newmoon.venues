﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PingDong.Services;

namespace PingDong.CQRS.Services
{
    /// <summary>
    /// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
    /// a requestId sent by client is used to detect duplicate requests.
    /// 
    /// https://github.com/aspnet/DependencyInjection/issues/531
    /// https://github.com/aspnet/Home/issues/2341
    /// </summary>
    /// <typeparam name="TCommand">Type of the command handler that performs the operation if request is not duplicated</typeparam>
    /// <typeparam name="TResponse">Return value of the inner command handler</typeparam>
    /// <typeparam name="TId"></typeparam>
    public class IdentifiedCommandHandler<TResponse, TCommand>
                : IRequestHandler<IdentifiedCommand<TResponse, TCommand>, TResponse>
                    where TCommand : IRequest<TResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly bool _suppressDuplicatedError;

        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, bool suppressDuplicatedError = false)
        {
            _mediator = mediator.EnsureNotNull(nameof(mediator));
            _requestManager = requestManager.EnsureNotNull(nameof(requestManager));

            _suppressDuplicatedError = suppressDuplicatedError;
        }
        
        /// <summary>
        /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
        /// just enqueue the original inner command.
        /// </summary>
        /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>
        public async Task<TResponse> Handle(IdentifiedCommand<TResponse, TCommand> message, CancellationToken cancellationToken = default)
        {
            await _requestManager.CreateAsync(message.RequestId, _suppressDuplicatedError);

            // Send the embedded business command to mediator so it runs its related CommandHandler 
            return await _mediator.Send(message.Command, cancellationToken);
        }
    }
}