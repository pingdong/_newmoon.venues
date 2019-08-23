using PingDong.CleanArchitect.Service;

namespace PingDong.Newmoon.Places.Service
{
    public class TenantValidator : ITenantValidator<string>
    {
        public bool IsValid(string tenantId)
        {
            // Demo purpose only
            // In real world, TenantId should check against real data
            return !string.IsNullOrWhiteSpace(tenantId);
        }
    }
}
