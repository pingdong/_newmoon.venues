using System.Threading.Tasks;

namespace PingDong.Services
{
    public interface ITenantManager<in TTenantId>
    {
        Task CreateAsync(TTenantId tenantId);
    }
}
