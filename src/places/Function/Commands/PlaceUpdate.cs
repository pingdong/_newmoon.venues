using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Functions;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places
{
    public class PlaceUpdate : CommandFunctionBase
    {
        public PlaceUpdate(IHttpContextAccessor accessor, IMediator mediator, ITenantValidator<string> tenantValidator) 
            : base(accessor, mediator, tenantValidator)
        {

        }

        [FunctionName("Place_Update")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "update")] HttpRequest request
            , ExecutionContext context
            , ILogger logger)
        {
            return await ExecuteAsync(context, request, logger, async () =>
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var command = JsonConvert.DeserializeObject<UpdatePlaceCommand>(requestBody);
                
                return await CommandDispatchAsync(command);
            });
        }
    }
}
