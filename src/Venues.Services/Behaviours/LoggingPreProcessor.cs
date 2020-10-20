using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services.Logging
{
    public class LoggingPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<LoggingPreProcessor<TRequest>> _logger;

        public LoggingPreProcessor(ILogger<LoggingPreProcessor<TRequest>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{typeof(TRequest).Name} is processing");

            return Task.CompletedTask;
        }
    }
}
