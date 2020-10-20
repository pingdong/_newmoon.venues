using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PingDong.Azure.FunctionApp;
using PingDong.Http;
using PingDong.Newmoon.Venues.Services.Commands;
using PingDong.Newmoon.Venues.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueVacate : HttpCommandTrigger
    {
        private readonly IOptionsMonitor<AppSettings> _settings;

        public VenueVacate(
            TelemetryConfiguration telemetryConfiguration
            , IMediator mediator
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueVacate> logger
            , IValidatorFactory validatorFactory
            , IOptionsMonitor<AppSettings> settings
        ) : base(telemetryConfiguration, accessor, requestHelper
            , mediator, logger, validatorFactory)
        {
            _settings = settings;
        }

        [FunctionName("Venue_Vacate")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "venue/{venueId}/vacate")] HttpRequest request
            , ExecutionContext context
            , string venueId)
        {
            return await ProcessAsync<VenueVacateCommand>(context, request, _settings.CurrentValue.SupportIdempotencyCheck).ConfigureAwait(false);
        }
    }
}
