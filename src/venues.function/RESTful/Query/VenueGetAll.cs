using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PingDong.Azure.Function;
using PingDong.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueGetAll : HttpTrigger
    {
        private readonly IVenueQueryService _query;

        public VenueGetAll(
            TelemetryConfiguration telemetryConfiguration
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueClose> logger
            , IValidatorFactory validatorFactory
            , IVenueQueryService query
        ) : base(telemetryConfiguration, accessor, requestHelper
            , logger, validatorFactory)
        {
            _query = query;
        }

        [FunctionName("Venue_GetAll")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venues")] HttpRequest request
            , ExecutionContext context)
        {
            return await GetAllAsync(context, async () => await _query.GetAllAsync()).ConfigureAwait(false);
        }
    }
}
