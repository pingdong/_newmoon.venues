using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PingDong.Azure.Function;
using PingDong.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueList : HttpTrigger
    {
        private readonly IVenueQueryService _query;

        public VenueList(
            IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueClose> logger
            , IValidatorFactory validatorFactory
            , IVenueQueryService query
        ) : base(accessor, requestHelper, logger, validatorFactory)
        {
            _query = query;
        }

        [FunctionName("Venue_GetById")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venues/id/{venueId}")] HttpRequest request
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
