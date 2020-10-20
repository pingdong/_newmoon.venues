using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PingDong.Azure.Function;
using PingDong.Http;
using PingDong.Newmoon.Venues.Services.Commands;
using PingDong.Newmoon.Venues.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueRegister : HttpCommandTrigger
    {
        private readonly IOptionsMonitor<AppSettings> _settings;

        public VenueRegister(
            TelemetryConfiguration telemetryConfiguration
            , IMediator mediator
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueClose> logger
            , IValidatorFactory validatorFactory
            , IOptionsMonitor<AppSettings> settings
        ) : base(telemetryConfiguration, accessor, requestHelper
            , mediator, logger, validatorFactory)
        {
            _settings = settings;
        }

        [FunctionName("Venue_Register")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "venue/register")] HttpRequest request
            , ExecutionContext context)
        {
            return await ProcessAsync<VenueRegisterCommand>(context, request, _settings.CurrentValue.SupportIdempotencyCheck).ConfigureAwait(false);
        }
    }
}
