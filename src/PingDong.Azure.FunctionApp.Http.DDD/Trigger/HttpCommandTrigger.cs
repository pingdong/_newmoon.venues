using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.DDD;
using PingDong.DDD.Services;
using PingDong.Http;
using PingDong.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Azure.FunctionApp
{
    public class HttpCommandTrigger : HttpTrigger
    {
        private readonly IMediator _mediator;

        protected HttpCommandTrigger(
            TelemetryConfiguration telemetryConfiguration
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , IMediator mediator
            , ILogger logger
            , IValidatorFactory validatorFactory
            ) : base(telemetryConfiguration, accessor, requestHelper, logger, validatorFactory)
        {
            _mediator = mediator.EnsureNotNull(nameof(mediator));
        }

        protected async Task<HttpResponseMessage> ProcessAsync<TCommand>(
            ExecutionContext context
            , HttpRequest request
            , bool supportIdempotencyCheck = false
            ) where TCommand : Command<bool>
        {
            return await ProcessAsync(context, request, async () =>
            {
                var command = await request.DeserializeAsync<TCommand>().ConfigureAwait(false);
                command.AppendTraceMetadata(RequestHelper.GetTenantId(), RequestHelper.GetCorrelationId());

                bool result;
                if (supportIdempotencyCheck)
                {
                    var cmd = new IdentifiedCommand<bool, TCommand>(RequestHelper.GetRequestId(), command);

                    result = await _mediator.Send(cmd).ConfigureAwait(false);
                }
                else
                {
                    result = await _mediator.Send(command).ConfigureAwait(false);
                }

                var status = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return new HttpResponseMessage(status);
            }).ConfigureAwait(false);
        }

        protected override HttpResponseMessage DomainExceptionHandling(DomainException exception)
        {
            switch (exception)
            {
                case EntityException ee:
                    var failure = new FailedResult(ee.Message);

                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new JsonContent(failure, JsonSerializerProfile.Default)
                    };

                default:
                    return base.DomainExceptionHandling(exception);
            }
        }
    }
}
