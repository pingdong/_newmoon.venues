using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PingDong.Azure.FunctionApp;
using PingDong.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueGetById : HttpTrigger
    {
        private readonly IVenueQueryService _query;

        public VenueGetById(
            TelemetryConfiguration telemetryConfiguration
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueGetById> logger
            , IValidatorFactory validatorFactory
            , IVenueQueryService query
        ) : base(telemetryConfiguration, accessor, requestHelper
            , logger, validatorFactory)
        {
            _query = query;
        }

        [FunctionName("Venue_GetById")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venue/{venueId}")] HttpRequest request
            , ExecutionContext context
            , string venueId)
        {
            return await GetByIdAsync(context, venueId, async id =>
            {
                var objectId = Guid.Parse(id);

                return await _query.GetByIdAsync(objectId);
            }).ConfigureAwait(false);
        }
    }
}
