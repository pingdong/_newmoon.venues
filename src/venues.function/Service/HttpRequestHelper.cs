﻿using Microsoft.AspNetCore.Http;
using PingDong.Http;
using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Http
{
    internal class HttpRequestHelper : IHttpRequestHelper
    {
        private const string RequestIdKey = "x-request-id";
        private const string CorrelationIdKey = "x-correlation-id";
        private const string TenantIdKey = "tid";

        private readonly IHttpContextAccessor _accessor;
        private readonly IRequestManager _requestManager;
        private readonly ITenantManager<string> _tenantManager;

        public HttpRequestHelper(IHttpContextAccessor accessor
            , IRequestManager requestManager
            , ITenantManager<string> tenantManager)
        {
            _accessor = accessor.EnsureNotNull(nameof(accessor));
            _requestManager = requestManager.EnsureNotNull(nameof(requestManager));
            _tenantManager = tenantManager.EnsureNotNull(nameof(tenantManager));
        }

        public Task IdempotencyValidationAsync()
        {
            return Task.CompletedTask;
        }

        public Task TenancyValidationAsync()
        {
            return Task.CompletedTask;
        }

        public string GetRequestId()
        {
            return _accessor.HttpContext.Request.Headers[RequestIdKey];
        }

        public string GetTenantId()
        {
            return _accessor.HttpContext.User.FindFirst(TenantIdKey)?.Value ?? "default-tenant";
        }

        public string GetCorrelationId()
        {
            return _accessor.HttpContext.Request.Headers[CorrelationIdKey];
        }

        public void SetCorrelationIdToResponse(string correlationId)
        {
            _accessor.HttpContext.Response.Headers.Add(CorrelationIdKey, correlationId);
        }
    }
}
