using PingDong.Services;
using System.Threading.Tasks;

namespace PingDong.Newmoon.Venues.Services
{
    public class TenantManager : ITenantManager<string>
    {
        public Task CreateAsync(string tenantId)
        {
            // TODO: Implement
            return Task.CompletedTask;
        }
    }
}
