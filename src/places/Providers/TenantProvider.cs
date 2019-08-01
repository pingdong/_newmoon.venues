using System;
using Microsoft.AspNetCore.Http;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Functions
{
    internal class TenantProvider : ITenantProvider<string>
    {
        private readonly IHttpContextAccessor _accessor;

        public TenantProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public string GetTenantId()
        {
            return _accessor.HttpContext.Request.GetTenantId();
        }
    }
}
