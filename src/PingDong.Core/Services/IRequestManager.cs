using System.Threading.Tasks;

namespace PingDong.Services
{
    public interface IRequestManager
    {
        Task CreateAsync(string requestId, bool suppressDuplicatedError);
    }
}
