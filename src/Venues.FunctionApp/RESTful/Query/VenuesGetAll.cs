using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PingDong.Azure.FunctionApp;
using PingDong.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenuesGetAll : HttpTrigger
    {
        private readonly IVenueQueryService _query;

        public VenuesGetAll(
            TelemetryConfiguration telemetryConfiguration
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenuesGetAll> logger
            , IValidatorFactory validatorFactory
            , IVenueQueryService query
        ) : base(telemetryConfiguration, accessor, requestHelper
            , logger, validatorFactory)
        {
            _query = query;
        }

        [FunctionName("Venues_GetAll")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venues")] HttpRequest request
            , ExecutionContext context)
        {
            return await GetAllAsync(context, async () => await _query.GetAllAsync()).ConfigureAwait(false);
        }
    }
}
