using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Http;
using PingDong.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using PingDong.Validations;

namespace PingDong.Azure.Function
{
    public class HttpTrigger : TriggerBase
    {
        #region Variables

        private readonly IHttpContextAccessor _accessor;

        #endregion

        #region ctor

        protected HttpTrigger(
            TelemetryConfiguration telemetryConfiguration
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger logger
            , IValidatorFactory validatorFactory
            ) : base(telemetryConfiguration, logger, validatorFactory)
        {
            _accessor = accessor.EnsureNotNull(nameof(accessor));

            RequestHelper = requestHelper.EnsureNotNull(nameof(requestHelper));
        }

        #endregion

        #region Methods

        protected async Task<HttpResponseMessage> UpdateAsync<TRequest>(
            ExecutionContext context
            , HttpRequest request
            , Func<TRequest, Task> func)
        {
            return await ProcessAsync(context, async () =>
            {
                var data = await request.DeserializeAsync<TRequest>().ConfigureAwait(false);

                // Validation
                await ValidateAsync(data).ConfigureAwait(false);

                await func(data).ConfigureAwait(false);

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DeleteAsync<TRequest>(
            ExecutionContext context
            , HttpRequest request
            , Func<TRequest, Task> func)
        {
            return await UpdateAsync(context, request, func).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> CreateAsync<TRequest, TResult>(
            ExecutionContext context
            , HttpRequest request
            , Func<TRequest, Task<TResult>> func)
        {
            return await ProcessAsync(context, async () =>
            {
                var data = await request.DeserializeAsync<TRequest>().ConfigureAwait(false);

                // Validation
                await ValidateAsync(data).ConfigureAwait(false);

                var value = await func(data).ConfigureAwait(false);

                // TODO: Specify the location of new resource
                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new JsonContent(value, JsonSerializerProfile.Default)
                };
            }).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> GetAllAsync<TResult>(
            ExecutionContext context
            , Func<Task<TResult>> func)
        {
            return await ProcessAsync(context, async () =>
            {
                var value = await func().ConfigureAwait(false);

                // Prepare response
                HttpResponseMessage response;

                if (value == null)
                {
                    response = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    var result = new SuccessResult(value);

                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new JsonContent(result, JsonSerializerProfile.Default)

                    };
                }

                return response;
            }).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> GetByIdAsync<TResult>(
            ExecutionContext context
            , string id
            , Func<string, Task<TResult>> func)
        {
            return await ProcessAsync(context, async () =>
            {
                var value = await func(id).ConfigureAwait(false);

                // Prepare response
                HttpResponseMessage response;

                if (value == null)
                {
                    response = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    var result = new SuccessResult(value);

                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new JsonContent(result, JsonSerializerProfile.Default)

                    };
                }

                return response;
            }).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> QueryAsync<TRequest, TResult>(
            ExecutionContext context
            , HttpRequest request
            , Func<TRequest, Task<TResult>> func)
        {
            return await ProcessAsync(context, async () =>
            {
                var data = await request.DeserializeAsync<TRequest>().ConfigureAwait(false);

                // Validation
                await ValidateAsync(data).ConfigureAwait(false);

                var value = await func(data).ConfigureAwait(false);

                // Prepare response
                HttpResponseMessage response;

                if (value == null)
                {
                    response = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    var result = new SuccessResult(value);

                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new JsonContent(result, JsonSerializerProfile.Default)

                    };
                }

                return response;
            }).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods

        protected async Task<HttpResponseMessage> ProcessAsync(
            ExecutionContext context
            , HttpRequest request
            , Func<Task<HttpResponseMessage>> func)
        {
            return await ProcessAsync(context, async () =>
            {
                HttpResponseMessage result;

                try
                {
                    // Idempotency Validator
                    await RequestHelper.IdempotencyValidationAsync().ConfigureAwait(false);

                    // Tenant Validation
                    await RequestHelper.TenancyValidationAsync().ConfigureAwait(false);

                    result = await func().ConfigureAwait(false);

                    // apply correlation to response
                    var correlationId = RequestHelper.GetCorrelationId();
                    if (correlationId != default)
                        RequestHelper.SetCorrelationIdToResponse(correlationId);
                }

                #region Error Handling

                catch (InvalidRequestException ex)
                {
                    var failure = new FailedResult(ex.Message);

                    result = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new JsonContent(failure, JsonSerializerProfile.Default)
                    };
                }
                catch (ValidationFailureException ex)
                {
                    var failure = new FailedResult("Validation Error", ex.Errors);

                    result = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new JsonContent(failure, JsonSerializerProfile.Default)
                    };
                }
                catch (FormatException)
                {
                    result = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                catch (NotFoundException)
                {
                    result = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                catch (DuplicatedException)
                {
                    result = new HttpResponseMessage(HttpStatusCode.Conflict);
                }
                catch (UnauthorizedAccessException)
                {
                    result = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                catch (JsonException)
                {
                    var failure = new FailedResult("Unable to deserialize the request");

                    result = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new JsonContent(failure, JsonSerializerProfile.Default)
                    };
                }
                catch (DomainException exception)
                {
                    result = DomainExceptionHandling(exception);
                }
                catch (Exception)
                {
                    result = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                #endregion

                _accessor.HttpContext.Response.ContentType = "application/json";

                return result;

            }).ConfigureAwait(false);
        }

        #endregion

        #region Protected Methods

        protected virtual HttpResponseMessage DomainExceptionHandling(DomainException exception)
        {
            var failure = new FailedResult(exception.Message);

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new JsonContent(failure, JsonSerializerProfile.Default)
            };
        }

        #endregion

        #region Properties

        protected IHttpRequestHelper RequestHelper { get; }

        #endregion
    }
}
