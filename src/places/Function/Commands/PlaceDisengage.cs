using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PingDong.Newmoon.Places.Functions;
using PingDong.Newmoon.Places.Service.Commands;

namespace PingDong.Newmoon.Places
{
    public class PlaceDisengage : CommandFunctionBase
    {
        public PlaceDisengage(IHttpContextAccessor accessor, IMediator mediator) : base(accessor, mediator)
        {

        }

        [FunctionName("Place_Disengage")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "disengage")] HttpRequest request
            , ExecutionContext context
            , ILogger logger)
        {
            return await ExecuteAsync(context, request, logger, async () =>
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var command = JsonConvert.DeserializeObject<PlaceFreeCommand>(requestBody);
                
                return await CommandDispatchAsync(command);
            });
        }
    }
}
