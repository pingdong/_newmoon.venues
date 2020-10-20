using MediatR;
using System;

namespace PingDong.DDD.Services
{
    public class IdentifiedCommand<TResponse, TCommand> : IRequest<TResponse> 
        where TCommand : IRequest<TResponse>
    {
        public TCommand Command { get; }

        public string RequestId { get; }

        public IdentifiedCommand(string requestId, TCommand command)
        {
            requestId.EnsureNotNullDefaultOrWhitespace(nameof(requestId));

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Command = command;
            RequestId = requestId;
        }
    }
}
