using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Functions;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places
{
    public class PlaceCreate : CommandFunctionBase
    {
        public PlaceCreate(IHttpContextAccessor accessor, IMediator mediator, ITenantValidator tenantValidator) 
            : base(accessor, mediator, tenantValidator)
        {

        }

        [FunctionName("Place_Create")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "create")] HttpRequest request
            , ExecutionContext context
            , ILogger logger)
        {
            return await ExecuteAsync(context, request, logger, async () =>
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var command = JsonConvert.DeserializeObject<CreatePlaceCommand>(requestBody);

                return await CommandDispatchAsync(command);
            });
        }
    }
}
