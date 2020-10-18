using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services
{
    public class TenantManager : ITenantManager<string>
    {
        public Task CreateAsync(string tenantId)
        {
            tenantId.EnsureNotNullDefaultOrWhitespace();

            // TODO: Implement
            return Task.CompletedTask;
        }
    }
}
