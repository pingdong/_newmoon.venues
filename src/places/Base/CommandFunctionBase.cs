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
        private readonly ITenantValidator<string> _tenantValidator;

        public CommandFunctionBase(IHttpContextAccessor accessor, IMediator mediator, ITenantValidator<string> tenantValidator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _tenantValidator = tenantValidator ?? throw new ArgumentNullException(nameof(tenantValidator));
        }

        public async Task<IActionResult> ExecuteAsync(ExecutionContext context, HttpRequest request, ILogger logger, Func<Task<IActionResult>> func)
        {
            var functionName = context.FunctionName;

            #region Start

            var start = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processing a request from {start} (UTC)");

            #endregion
            
            #region Pre-process

            #region Http
            
            // Extract RequestId
            _requestId = request.GetRequestId();
            // Extract correlationId
            _correlationId = request.GetCorrelationId();
            // Retrieve tenantId from User Token
            _tenantId = request.GetTenantId();
            
            if (_requestId == Guid.Empty)
                return new BadRequestErrorMessageResult("Missing x-request-id header or is invalid");
            // Remove this check if this is a single tenant application
            if (string.IsNullOrWhiteSpace(_tenantId))
                return new BadRequestErrorMessageResult("Missing tid in JWT or is invalid");

            #endregion

            #region Validation

            #region Tenant
            
            if (!_tenantValidator.IsValid(_tenantId))
            {
                logger.LogWarning("Missing tenantId or tenantId is invalid");

                return new BadRequestErrorMessageResult("Missing tenantId or tenantId is invalid");
            }

            #endregion

            #endregion

            #endregion

            IActionResult result;

            try
            {
                // Execute function
                result = await func();
            }
            catch (RequestDuplicatedException ex)
            {
                logger.LogWarning(ex.EventId, ex, ex.Message, ex.RequestId);

                result = new BadRequestErrorMessageResult(ex.Message);
            }
            catch (DomainException ex)
            {
                logger.LogError(ex.EventId, ex, ex.Message, ex.Tracker.CorrelationId);

                result = new BadRequestErrorMessageResult(ex.Message);
            }
            catch (System.Exception ex)
            {
                logger.LogError(EventIds.Failure, ex, ex.Message);

                result = new InternalServerErrorResult();
            }

            #region Post-process

            if (_correlationId != default)
                _accessor.HttpContext.Response.Headers.Add("x-correlation-id", _correlationId);

            _accessor.HttpContext.Response.ContentType = "application/json";

            #endregion

            #region End

            var end = DateTime.UtcNow;
            
            logger.LogInformation($"Time triggered function - '{functionName}' processed a request at {end} (UTC)");
            logger.LogInformation($"Time triggered function - '{functionName}' processed from {start} to {end}, took {(end - start).ToString()}");

            #endregion

            return result;
        }

        #region Extraction

        #endregion

        #region Command

        private string _tenantId;
        private string _correlationId;
        private Guid _requestId;

        protected async Task<IActionResult> CommandDispatchAsync<TCommand>(TCommand command) where TCommand: Command<bool>
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
