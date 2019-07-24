using System.Threading.Tasks;

namespace PingDong.Newmoon.Places.Core
{
    public interface ITenantValidator
    {
        Task<bool> IsValidAsync(string tenantId);
    }
}
