using System;
using Microsoft.AspNetCore.Http;
using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Functions
{
    internal class TenantProvider : ITenantProvider<string>
    {
        private readonly string _tenantId;

        public TenantProvider(IHttpContextAccessor accessor)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));

            _tenantId = accessor.HttpContext.Request.GetTenantId();
        }

        public string GetTenantId()
        {
            return _tenantId;
        }
    }
}
