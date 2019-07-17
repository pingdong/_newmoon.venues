using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Functions
{
    public class CommandFunctionBase : FunctionBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _accessor;

        public CommandFunctionBase(IHttpContextAccessor accessor, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<IActionResult> ExecuteAsync(ExecutionContext context, HttpRequest request, ILogger logger, Func<Task<IActionResult>> func)
        {
            var functionName = context.FunctionName;

            #region Start

            var start = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processing a request from {start} (UTC)");

            #endregion

            #region Pre-process

            // Extract RequestId
            _requestId = request.GetRequestId();
            // Extract correlationId
            _correlationId = request.GetCorrelationId();
            // Retrieve tenantId from User Token
            _tenantId = request.GetTenantId();
            
            if (_requestId == Guid.Empty)
                return new BadRequestErrorMessageResult("Missing x-request-id header or is invalid");
            // Remove this check if this is a single tenant application
            if (_tenantId == Guid.Empty)
                return new BadRequestErrorMessageResult("Missing tid in JWT or is invalid");
            // If correlationId is empty, create a new one
            if (_correlationId == Guid.Empty)
                _correlationId = Guid.NewGuid();

            #endregion

            #region Validation

            #region Tenant
            
            if (!ValidateTenantId(_tenantId))
            {
                logger.LogWarning("Missing tenantId or tenantId is invalid");

                return new BadRequestErrorMessageResult("Missing tenantId or tenantId is invalid");
            }

            #endregion

            #endregion

            // Execute function
            var result = await func();

            #region Post-process

            if (_correlationId != default)
                _accessor.HttpContext.Request.Headers.Add("x-correlation-id", _correlationId.ToString());
            if (_requestId != default)
                _accessor.HttpContext.Request.Headers.Add("x-request-id", _correlationId.ToString());

            _accessor.HttpContext.Response.ContentType = "application/json";

            #endregion

            #region End

            var end = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processed a request at {end} (UTC)");
            logger.LogInformation($"Time triggered function - '{functionName}' processed from {start} to {end}, took {(end - start).ToString()}");

            #endregion

            return result;
        }

        #region Validation

        private bool ValidateTenantId(Guid tenantId)
        {
            // TODO: check tenantId
            return Guid.Empty != tenantId;
        }

        #endregion

        #region Extraction

        #endregion

        #region Command

        private Guid _tenantId;
        private Guid _correlationId;
        private Guid _requestId;

        protected async Task<IActionResult> CommandDispatchAsync<TCommand>(TCommand command) where TCommand: Command
        {
            command.TenantId = _tenantId;
            command.CorrelationId = _correlationId;

            var identifiedCommand = new IdentifiedCommand<Guid, bool, TCommand>(_requestId, command);
            var result = await _mediator.Send(identifiedCommand).ConfigureAwait(false);

            return result ? (IActionResult)new OkResult() : new BadRequestResult();
        }

        #endregion
    }
}
